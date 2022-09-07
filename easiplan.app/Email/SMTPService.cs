using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.Email
{
    /// <summary>
    /// TO DO : Configure SMTP services to send email
    /// </summary>
    public class SMTPService
    {
        public bool IsConfigured;

        public bool PreviewBeforeSend = true;
        public bool SendEmail(IList<EmailAddress> EmailAddresses, string Subject, string HtmlMessage, string RTFMessage = null, IList<EmailAttachment> EmailAttachments = null, DateTime? SendDate = null, string outputPath = null)
        {


            return false;
        }

        public void SendEmail(IList<EmailMessage> messages, DateTime? SendDate = null)
        {

            
        }
    }
}
