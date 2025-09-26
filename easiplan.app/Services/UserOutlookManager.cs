using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Graph;
using za.co.easiworx.office365.net;
using za.co.easiworx.office365.net.Exceptions;
using za.co.easiworx.office365.net.models;

namespace easiplan.app.Services  // <-- Put it in easiplan.app.Services
{
    public class UserOutlookManager
    {
        // Store authentication per EasiWorx user (by username)
        private static Dictionary<string, UserOutlookSession> _userSessions = new Dictionary<string, UserOutlookSession>();

        /// <summary>
        /// Get or create Outlook proxy for specific EasiWorx user
        /// </summary>
        public static async Task<OutlookProxy> GetOutlookProxyForUserAsync(string easiWorxUsername)
        {
            if (_userSessions.ContainsKey(easiWorxUsername))
            {
                var session = _userSessions[easiWorxUsername];

                // Check if session is still valid
                if (await session.IsValidAsync())
                {
                    return session.OutlookProxy;
                }
                else
                {
                    // Session expired, remove it
                    session.Dispose();
                    _userSessions.Remove(easiWorxUsername);
                }
            }

            // Create new session - this will prompt for authentication
            var newSession = await CreateNewSessionAsync(easiWorxUsername);
            _userSessions[easiWorxUsername] = newSession;
            return newSession.OutlookProxy;
        }

        private static async Task<UserOutlookSession> CreateNewSessionAsync(string easiWorxUsername)
        {
            try
            {
                string clientId = "37fdee96-acfa-4ac1-afcd-968cc3c7a5fd";

                var options = new InteractiveBrowserCredentialOptions
                {
                    RedirectUri = new Uri("msal37fdee96-acfa-4ac1-afcd-968cc3c7a5fd://auth"),
                    AuthorityHost = new Uri("https://login.microsoftonline.com/common"),
                    ClientId = clientId
                    // Note: No tenant restriction - works with any Microsoft account
                };

                var tokenCredential = new InteractiveBrowserCredential(options);
                var graphClient = new GraphServiceClient(tokenCredential);

                // Test the connection
                var user = await graphClient.Me.Request().GetAsync();

                var session = new UserOutlookSession
                {
                    EasiWorxUsername = easiWorxUsername,
                    OutlookEmail = user.Mail ?? user.UserPrincipalName,
                    OutlookDisplayName = user.DisplayName,
                    GraphClient = graphClient,
                    AuthenticatedAt = DateTime.Now,
                    LastUsed = DateTime.Now
                };

                return session;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to authenticate {easiWorxUsername} with Outlook: {ex.Message}", ex);
            }
        }

        public static void SignOutUser(string easiWorxUsername)
        {
            if (_userSessions.ContainsKey(easiWorxUsername))
            {
                _userSessions[easiWorxUsername].Dispose();
                _userSessions.Remove(easiWorxUsername);
            }
        }

        public static void SignOutAllUsers()
        {
            foreach (var session in _userSessions.Values)
            {
                session.Dispose();
            }
            _userSessions.Clear();
        }

        public static bool IsUserAuthenticated(string easiWorxUsername)
        {
            return _userSessions.ContainsKey(easiWorxUsername);
        }

        public static string GetUserOutlookEmail(string easiWorxUsername)
        {
            return _userSessions.ContainsKey(easiWorxUsername) ?
                   _userSessions[easiWorxUsername].OutlookEmail : null;
        }

        public static DateTime? GetUserLastAuthentication(string easiWorxUsername)
        {
            if (_userSessions.ContainsKey(easiWorxUsername))
            {
                return _userSessions[easiWorxUsername].AuthenticatedAt;
            }
            return null;
        }
    }

    public class UserOutlookSession : IDisposable
    {
        public string EasiWorxUsername { get; set; }
        public string OutlookEmail { get; set; }
        public string OutlookDisplayName { get; set; }
        public OutlookProxy OutlookProxy { get; set; }
        public GraphServiceClient GraphClient { get; set; }
        public DateTime AuthenticatedAt { get; set; }
        public DateTime LastUsed { get; set; }

        public async Task<bool> IsValidAsync()
        {
            try
            {
                // Update last used time
                LastUsed = DateTime.Now;

                // Test if we can still access the user's profile
                // This will automatically refresh tokens if needed
                await GraphClient.Me.Request().GetAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            OutlookProxy?.Dispose();
            GraphClient = null;
        }
    }
}