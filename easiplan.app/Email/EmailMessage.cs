using easiplan.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.Email
{

    public class EmailAddress
    {
        public string RecipientAddress { get; set; }
        public string RecipientName { get; set; }

        public SendAs SendAs { get; set; }

        public EmailAddress()
        {
            SendAs = SendAs.TO;
        }
    }

    public class EmailAttachment
    {
        public string AttachmentName { get; set; }
        public byte[] AttachmentData { get; set; }

        public AttachmentType AttachmentType { get; set; }

        public EmailAttachment()
        {
            AttachmentType = AttachmentType.External;
        }
    }

    public enum SendAs
    {
        TO,
        BC,
        CC,
    }

    public enum AttachmentType
    {
        External = 1,
        OleItem = 6,
        InLine = 5,
    }

    public class ContactDetails
    {
        public string CustomerID { get; set; }
        public DateTime CreationTime { get; }

        public string Title { get; set; }
        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Language { get; set; }
        public string JobTitle { get; set; }
        public string Profession { get; set; }
        public string Spouse { get; set; }
        public string GovernmentIDNumber { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }


        public string HomeAddress { get; set; }
        public string HomeAddressCity { get; set; }
        public string HomeAddressCountry { get; set; }
        public string HomeAddressPostalCode { get; set; }
        public string HomeAddressPostOfficeBox { get; set; }
        public string HomeAddressState { get; set; }
        public string HomeAddressStreet { get; set; }

        public string MailingAddress { get; set; }
        public string MailingAddressCity { get; set; }
        public string MailingAddressCountry { get; set; }
        public string MailingAddressPostalCode { get; set; }
        public string MailingAddressPostOfficeBox { get; set; }
        public string MailingAddressState { get; set; }
        public string MailingAddressStreet { get; set; }

        public string Email1Address { get; set; }


        public virtual IList<ContactDetail> OtherContacts { get; set; }

        public string HomeFaxNumber { get; set; }
        public string HomeTelephoneNumber { get; set; }
        public string MobileTelephoneNumber { get; set; }
        public string BusinessTelephoneNumber { get; set; }

        public string OfficeLocation { get; set; }
        public string Body { get; set; }



    }

    public class EmailMessage
    {
        public DateTime SendDate { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public string RTFBody { get; set; }
        public IList<EmailAddress> EmailAddresses { get; set; }
        public IList<EmailAttachment> EmailAttachments { get; set; }
        public string ReferenceId { get; set; }
        public int ClientId { get; set; }

    }
}
