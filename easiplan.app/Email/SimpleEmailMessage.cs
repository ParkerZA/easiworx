using System;

namespace easiplan.app.Models
{
    public class SimpleEmailMessage
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public string Body { get; set; }
    }
}