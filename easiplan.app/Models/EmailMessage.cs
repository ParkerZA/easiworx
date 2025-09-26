using System;
using System.Collections.Generic;

namespace za.co.easiworx.office365.net.models
{
    public class EmailMessage
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public List<string> ToAddresses { get; set; } = new List<string>();
        public List<string> CcAddresses { get; set; } = new List<string>();
        public List<string> BccAddresses { get; set; } = new List<string>();
        public DateTime ReceivedDateTime { get; set; }
        public DateTime SentDateTime { get; set; }
        public bool IsRead { get; set; }
        public bool HasAttachments { get; set; }
        public string Importance { get; set; }
        public string ConversationId { get; set; }
        public string Direction { get; set; } = "Unknown"; // "Incoming", "Outgoing", or "Unknown"

        // Helper properties for display
        public string ToAddressesString => string.Join("; ", ToAddresses);
        public string CcAddressesString => string.Join("; ", CcAddresses);
        public string FormattedDateTime => ReceivedDateTime.ToString("dd-MMM-yyyy HH:mm");
        public string ShortBody => Body?.Length > 100 ? Body.Substring(0, 100) + "..." : Body;
        public string FromDisplayName => !string.IsNullOrEmpty(FromName) ?
                                        $"{FromName} <{FromAddress}>" : FromAddress;
    }
}