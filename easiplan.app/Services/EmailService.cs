using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Finx.App;

namespace easiplan.app.Services
{
    /// <summary>
    /// Enhanced EmailService that works with the centralized OutlookAuthenticationService
    /// </summary>
    public class EmailService
    {
        private GraphServiceClient graphClient;

        public EmailService()
        {
            InitializeFromOutlookAuthenticationService();
        }

        /// <summary>
        /// Initialize from the centralized authentication service
        /// </summary>
        private void InitializeFromOutlookAuthenticationService()
        {
            try
            {
                if (!OutlookAuthenticationService.IsAuthenticated)
                    throw new InvalidOperationException("Outlook is not authenticated. Please connect to Outlook first.");

                var outlookProxy = OutlookAuthenticationService.OutlookProxy;
                if (outlookProxy == null)
                    throw new InvalidOperationException("OutlookProxy is not available");

                // Use reflection to access the private GraphServiceClient
                var field = typeof(za.co.easiworx.office365.net.OutlookProxy)
                    .GetField("client", BindingFlags.NonPublic | BindingFlags.Instance);

                if (field != null)
                {
                    graphClient = (GraphServiceClient)field.GetValue(outlookProxy);
                }
                else
                {
                    throw new InvalidOperationException("Could not access GraphServiceClient from OutlookProxy");
                }

                if (graphClient == null)
                    throw new InvalidOperationException("GraphServiceClient is null");

                Program.Logger?.Info("EmailService initialized successfully");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Failed to initialize EmailService", ex);
                throw new InvalidOperationException($"Failed to initialize EmailService: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get emails for a specific client - ENHANCED with better error handling
        /// </summary>
        public async Task<List<EmailMessage>> GetEmailsForClient(string clientEmailAddress, int maxEmails = 50)
        {
            if (graphClient == null)
            {
                Program.Logger?.Error("GraphClient is null in GetEmailsForClient");
                throw new InvalidOperationException("Email service not properly initialized");
            }

            if (string.IsNullOrEmpty(clientEmailAddress))
                throw new ArgumentException("Client email address cannot be empty");

            try
            {
                var emails = new List<EmailMessage>();

                Program.Logger?.Info($"Searching for emails with client {clientEmailAddress}");

                // 1. Get recent emails from inbox
                var inboxMessages = await GetInboxMessages(clientEmailAddress, maxEmails);
                emails.AddRange(inboxMessages);

                // 2. Get recent emails from sent items
                var sentMessages = await GetSentMessages(clientEmailAddress, maxEmails);
                emails.AddRange(sentMessages);

                // Sort by date (newest first) and limit results
                emails = emails.OrderByDescending(e => e.ReceivedDate).Take(maxEmails).ToList();

                Program.Logger?.Info($"Found {emails.Count} emails for client {clientEmailAddress}");

                return emails;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error searching emails for client {clientEmailAddress}: {ex.Message}");
                throw new InvalidOperationException($"Error searching emails: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get inbox messages related to the client
        /// </summary>
        private async Task<List<EmailMessage>> GetInboxMessages(string clientEmailAddress, int maxEmails = 0)
        {
            var emails = new List<EmailMessage>();
            try
            {
                var searchQuery = $"\"{clientEmailAddress}\"";
                Program.Logger?.Info($"Fetching inbox emails using server-side search: {searchQuery}");

                var request = graphClient.Me.MailFolders.Inbox.Messages
                    .Request(new List<QueryOption>
                    {
                new QueryOption("$search", searchQuery)
                    })
                    .Header("ConsistencyLevel", "eventual")
                    .Select("id,subject,from,receivedDateTime,isRead,bodyPreview,webLink,hasAttachments,toRecipients")
                    .Top(50);  // Page size (50 per request)

                do
                {
                    var page = await request.GetAsync();
                    Program.Logger?.Info($"Retrieved {page.Count} inbox messages (page)");

                    foreach (var message in page.CurrentPage)
                    {
                        var emailMsg = ConvertToEmailMessage(message);
                        if (emailMsg != null)
                        {
                            emailMsg.Direction = "Received";
                            emails.Add(emailMsg);
                        }
                    }

                    request = page.NextPageRequest;

                    // Optional: Stop if we hit a specific limit
                    if (maxEmails > 0 && emails.Count >= maxEmails)
                        break;
                }
                while (request != null);  // Keep going until no more pages

                Program.Logger?.Info($"Total inbox emails found: {emails.Count}");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error searching inbox messages: {ex.Message}");
            }

            return emails;
        }

        private async Task<List<EmailMessage>> GetSentMessages(string clientEmailAddress, int maxEmails)
        {
            var emails = new List<EmailMessage>();

            try
            {
                var searchQuery = $"\"{clientEmailAddress}\"";
                Program.Logger?.Info($"Fetching sent emails using server-side search: {searchQuery}");

                var request = graphClient.Me.MailFolders.SentItems.Messages
                    .Request(new List<QueryOption>
                    {
                new QueryOption("$search", searchQuery)
                    })
                    .Header("ConsistencyLevel", "eventual")
                    .Select("id,subject,from,toRecipients,sentDateTime,isRead,bodyPreview,webLink,hasAttachments")
                    .Top(Math.Min(maxEmails, 50));

                do
                {
                    var page = await request.GetAsync();
                    Program.Logger?.Info($"Retrieved {page.Count} sent messages (page)");

                    foreach (var message in page.CurrentPage)
                    {
                        var emailMsg = ConvertToEmailMessage(message);
                        if (emailMsg != null)
                        {
                            emailMsg.Direction = "Sent";
                            emails.Add(emailMsg);
                        }
                    }

                    request = page.NextPageRequest;
                }
                while (request != null && emails.Count < maxEmails);

                Program.Logger?.Info($"Total sent emails found: {emails.Count}");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error searching sent messages: {ex.Message}");
            }

            return emails;
        }




        /// <summary>
        /// Check if a message is from the specified client
        /// </summary>
        private bool IsFromClient(Message message, string clientEmail)
        {
            try
            {
                return message.From?.EmailAddress?.Address?.Equals(clientEmail, StringComparison.OrdinalIgnoreCase) == true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Open email in Outlook
        /// </summary>
        // Add this method to your EmailService class in easiplan.app.Services
        public async Task<string> GetEmailWebLink(string emailId)
        {
            try
            {
                if (graphClient == null)
                    throw new InvalidOperationException("GraphClient not initialized");

                var message = await graphClient.Me.Messages[emailId].Request().GetAsync();
                return message.WebLink;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error getting email web link for {emailId}: {ex.Message}", ex);
                return null;
            }
        }

        /// <summary>
        /// Check if a message is to the specified client
        /// </summary>
        private bool IsToClient(Message message, string clientEmail)
        {
            try
            {
                return message.ToRecipients?.Any(r =>
                    r.EmailAddress?.Address?.Equals(clientEmail, StringComparison.OrdinalIgnoreCase) == true) == true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Convert Graph API Message to our EmailMessage model
        /// </summary>
        private EmailMessage ConvertToEmailMessage(Message message)
        {
            try
            {
                return new EmailMessage
                {
                    Id = message.Id ?? "unknown",
                    Subject = message.Subject ?? "(No Subject)",
                    FromName = message.From?.EmailAddress?.Name ?? message.From?.EmailAddress?.Address ?? "Unknown",
                    ToAddresses = string.Join("; ", message.ToRecipients?.Select(r => r.EmailAddress?.Address) ?? new string[0]),
                    ReceivedDate = message.ReceivedDateTime?.DateTime ?? message.SentDateTime?.DateTime ?? DateTime.MinValue,
                    Body = message.Body?.Content ?? "",
                    BodyPreview = message.BodyPreview ?? "",
                    IsRead = message.IsRead ?? false,
                    HasAttachments = message.HasAttachments ?? false,
                    Direction = "Unknown", // Will be set by caller,
                    WebLink = message.WebLink ?? ConstructOutlookWebLink(message.Id) // Add this line

                };
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error converting message {message.Id}: {ex.Message}");
                return null;
            }
        }

        private string ConstructOutlookWebLink(string messageId)
        {
            try
            {
                return $"https://outlook.office365.com/owa/?ItemID={Uri.EscapeDataString(messageId)}&exvsurl=1&viewmodel=ReadMessageItem";
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error constructing Outlook web link", ex);
                return null;
            }
        }

        /// <summary>
        /// Get the current authenticated user's email address
        /// </summary>
        public async Task<string> GetCurrentUserEmail()
        {
            try
            {
                return OutlookAuthenticationService.UserEmail ?? "Unknown";
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error getting current user email", ex);
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Check if the email service is available and ready to use
        /// </summary>
        public static bool IsAvailable()
        {
            return OutlookAuthenticationService.IsAuthenticated;
        }

        /// <summary>
        /// Test the email service connection
        /// </summary>
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                if (graphClient == null)
                    return false;

                // Try to get user profile to test connection
                return await OutlookAuthenticationService.TestConnectionAsync();
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error testing email service connection", ex);
                return false;
            }
        }

        /// <summary>
        /// Get basic statistics about emails for a client
        /// </summary>
        public async Task<EmailStatistics> GetEmailStatisticsForClient(string clientEmailAddress)
        {
            try
            {
                var emails = await GetEmailsForClient(clientEmailAddress, 100);

                return new EmailStatistics
                {
                    TotalEmails = emails.Count,
                    ReceivedEmails = emails.Count(e => e.Direction == "Received"),
                    SentEmails = emails.Count(e => e.Direction == "Sent"),
                    UnreadEmails = emails.Count(e => !e.IsRead),
                    EmailsWithAttachments = emails.Count(e => e.HasAttachments),
                    MostRecentEmail = emails.FirstOrDefault()?.ReceivedDate ?? DateTime.MinValue,
                    OldestEmail = emails.LastOrDefault()?.ReceivedDate ?? DateTime.MinValue
                };
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error getting email statistics for {clientEmailAddress}", ex);
                return new EmailStatistics();
            }
        }
    }



    /// <summary>
    /// Email message model for the application
    /// </summary>
    public class EmailMessage
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string FromName { get; set; }
        public string ToAddresses { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string Body { get; set; }
        public string BodyPreview { get; set; }
        public bool IsRead { get; set; }
        public bool HasAttachments { get; set; }
        public string Direction { get; set; }
        public object EntryID { get; internal set; }
        public object StoreID { get; internal set; }
        public string WebLink { get; internal set; }
    }

    /// <summary>
    /// Email statistics for a client
    /// </summary>
    public class EmailStatistics
    {
        public int TotalEmails { get; set; }
        public int ReceivedEmails { get; set; }
        public int SentEmails { get; set; }
        public int UnreadEmails { get; set; }
        public int EmailsWithAttachments { get; set; }
        public DateTime MostRecentEmail { get; set; }
        public DateTime OldestEmail { get; set; }
    }
}