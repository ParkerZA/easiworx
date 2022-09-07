
using Finx.App.Email;
using Finx.App.Extensions;
using easiplan.domain.Entities;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Finx.App.Office
{
    public class OutlookProxy:IDisposable
    {
        public bool IsConfigured;
        Microsoft.Office.Interop.Outlook.Application oApp = null;
        public OutlookProxy()
        {
            //try create an outlook proxy
            try
            {
                // Create the Outlook application.
                oApp = new Microsoft.Office.Interop.Outlook.Application();

                if (oApp == null)
                    throw new System.Exception("Could not create Outlook application");

                try
                {
                   NameSpace ns = oApp.GetNamespace("mapi");                     
                   ns.Logon(null, null, true, true);

                    IsConfigured = true;
                }
                catch (System.Exception x1)
                {
                    throw new System.Exception("Could not logon to Outlook . \r\nPlease refer to https://www.fieldstonsoftware.com/support/support_gsyncit_8002801D.shtml for more information.");
                }

            }
            catch (System.Exception x)
            {
               // throw new System.Exception(x.Message + " \r\nYou may not have the correct version of MS Outlook 2010 installed. Please try another email application.");
               
            }
        }
        ~OutlookProxy()
        {
          
        }

        public bool PreviewBeforeSend = true;
        public bool SendEmail(IList<EmailAddress> EmailAddresses, string Subject, string HtmlMessage, string RTFMessage=null, IList<EmailAttachment> EmailAttachments = null,DateTime? SendDate = null,string outputPath=null)
        {

            try
            {
                if (!IsConfigured)
                    throw new System.Exception("Outlook 2010+ is not installed on this machine or there was an error logging on to Outlook.");

                // Microsoft.Office.Interop.Outlook.MAPIFolder fldContacts = (Microsoft.Office.Interop.Outlook.MAPIFolder)oApp.Session.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts);

                // Create a new mail item.
                Microsoft.Office.Interop.Outlook.MailItem oMsg = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                //Create the subject line 
                oMsg.Subject = Subject;

                //Add attachments.
                if(EmailAttachments!=null)
                    {
                    int iPos = 1;
                    foreach (EmailAttachment attch in EmailAttachments)
                    {
                        int iPosition = (int)oMsg.Body.Length + iPos;
                        // int iAttachType = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;

                        Microsoft.Office.Interop.Outlook.Attachment oAttach = oMsg.Attachments.Add(attch.AttachmentData,
                            (int)attch.AttachmentType
                            , iPosition,
                            attch.AttachmentName);

                        oMsg.Attachments.Add(oAttach);

                        iPos++;
                    }
                }

                //Add Recipients
                Microsoft.Office.Interop.Outlook.Recipients oRecips = (Microsoft.Office.Interop.Outlook.Recipients)oMsg.Recipients;
                foreach (EmailAddress addr in EmailAddresses)
                {
                    
                    switch (addr.SendAs)
                    {
                        case SendAs.TO:
                            Microsoft.Office.Interop.Outlook.Recipient oRecip1 = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add(addr.RecipientAddress);
                            oRecip1.Resolve();
                            break;
                        case SendAs.CC:
                            Microsoft.Office.Interop.Outlook.Recipient oRecip2 = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add(addr.RecipientAddress);
                            oRecip2.Resolve();
                            break;
                        case SendAs.BC:
                            Microsoft.Office.Interop.Outlook.Recipient oRecip3 = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add(addr.RecipientAddress);
                            oRecip3.Resolve();
                            break;
                    }
                  
                }

                #region Automatically Inserting the default signature
                //Outlook Inspectore
                var inspector = oMsg.GetInspector; // less pervasive method, does not show the message

                //   oMsg.Display(oMsg); // more pervasive method to briefly show the message
                #endregion

                // Set HTMLBody. 
                //add the body of the email
                if (!string.IsNullOrEmpty(HtmlMessage))
                {
                    //node.Attributes["src"].Value = string.Format("cid:{0}", contentid);
                    oMsg.HTMLBody = oMsg.FormatHtmlBody( HtmlMessage) + oMsg.HTMLBody; // the signature portion
                    oMsg.BodyFormat = OlBodyFormat.olFormatHTML;
                }

                if (!string.IsNullOrEmpty(RTFMessage))
                    oMsg.RTFBody += RTFMessage;

                //Send Date
                if (SendDate != null)
                    oMsg.DeferredDeliveryTime = (DateTime)SendDate;

                // Send.
                if (!PreviewBeforeSend)
                    oMsg.Send();
                else//Display
                    oMsg.Display(oMsg);

                //Save Email
                if(!string.IsNullOrEmpty(outputPath))
                    oMsg.SaveAs(outputPath, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);

                // Clean up.

                oRecips = null;
                oMsg = null;
              
                return true;
            }
            catch (System.Exception ex)
            {
                //ERROR: Outlook TYPE_E_LIBNOTREGISTERED / TYPE_E_CANTLOADLIBRARY Error
                //https://www.fieldstonsoftware.com/support/support_gsyncit_8002801D.shtml

                throw ex;
            }//end of catch
            finally
            {


            }
           
        }

        public void SendEmail(IList<EmailMessage> messages, DateTime? SendDate = null)
        {
            try {

                if (!IsConfigured)
                    throw new System.Exception("Outlook 2010+ is not installed on this machine or there was an error logging on to Outlook.");

                foreach (var msg in messages)
                    SendEmail(msg.EmailAddresses, msg.Subject, msg.HtmlBody, msg.RTFBody, msg.EmailAttachments, SendDate==null? msg.SendDate:(DateTime)SendDate);


            } catch(System.Exception x) { throw x; }

        }
        /// <summary>
        /// Bypass Outlook Security Message
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        //internal Microsoft.Office.Interop.Outlook.Recipients GetRecipients(Microsoft.Office.Interop.Outlook.MailItem item)
        //{
        //    try
        //    {
        //        Microsoft.Office.Interop.Outlook.MailItem tempItem = (Microsoft.Office.Interop.Outlook.MailItem)Globals.ThisAddIn.Application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

        //        return (Microsoft.Office.Interop.Outlook.Recipients)tempItem.Recipients;

        //    }
        //    catch (Exception x)
        //    {
        //    }

        //    return null;
        //}

        public bool AddContactDetails(ContactDetails ContactDetails)
        {
            try
            {
                if (!IsConfigured)
                    throw new System.Exception("Outlook 2010+ is not installed on this machine or there was an error logging on to Outlook.");

                // Create the Outlook application.
                //Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();


                Microsoft.Office.Interop.Outlook.MAPIFolder contactsFolder = (Microsoft.Office.Interop.Outlook.MAPIFolder)oApp.Session
                                                .GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts);

               // string sStr = String.Format("[FirstName]='{0}' and [LastName]='{1}'", ContactDetails.FirstName, ContactDetails.LastName);

                string sStr = String.Format("[CustomerID]='{0}' ", ContactDetails.CustomerID);
                
                Microsoft.Office.Interop.Outlook._ContactItem contactItem = contactsFolder.Items.Find(sStr);

                if (contactItem == null)
                    contactItem = contactsFolder.Items.Add();

                contactItem.CustomerID = ContactDetails.CustomerID;

                contactItem.Title = ContactDetails.Title;
                contactItem.Initials = ContactDetails.Initials;
                contactItem.FirstName = ContactDetails.FirstName;
                contactItem.MiddleName = ContactDetails.MiddleName;
                contactItem.LastName = ContactDetails.LastName;

                contactItem.Birthday = ContactDetails.Birthday;
                contactItem.Language = ContactDetails.Language;

                
                contactItem.GovernmentIDNumber = ContactDetails.GovernmentIDNumber;

               // contactItem.HomeAddress = ContactDetails.HomeAddress;
                contactItem.HomeAddressStreet = ContactDetails.HomeAddressStreet;
                contactItem.HomeAddressCity = ContactDetails.HomeAddressCity;
                contactItem.HomeAddressCountry = ContactDetails.HomeAddressCountry;
                contactItem.HomeAddressPostalCode = ContactDetails.HomeAddressPostalCode;

                contactItem.BusinessAddressStreet = ContactDetails.HomeAddressStreet;
                contactItem.BusinessAddressCity = ContactDetails.HomeAddressCity;
                contactItem.BusinessAddressCountry = ContactDetails.HomeAddressCountry;
                contactItem.BusinessAddressPostalCode = ContactDetails.HomeAddressPostalCode;

                //  contactItem.MailingAddress = ContactDetails.MailingAddress;
                contactItem.MailingAddressStreet = ContactDetails.MailingAddressStreet;
                contactItem.MailingAddressCity = ContactDetails.MailingAddressCity;
                contactItem.MailingAddressCountry = ContactDetails.MailingAddressCountry;
                contactItem.MailingAddressPostalCode = ContactDetails.MailingAddressPostalCode;

                contactItem.HomeTelephoneNumber = ContactDetails.HomeTelephoneNumber;
                contactItem.MobileTelephoneNumber = ContactDetails.MobileTelephoneNumber;
                contactItem.Email1Address = ContactDetails.Email1Address;
                contactItem.Email1DisplayName = ContactDetails.FullName;
                contactItem.BusinessTelephoneNumber = ContactDetails.BusinessTelephoneNumber;

                contactItem.Spouse = ContactDetails.Spouse;

                contactItem.Profession = ContactDetails.Profession;
                contactItem.JobTitle = ContactDetails.Profession;

                contactItem.Save();

                contactItem.Display();

                return true;
            }
            catch (System.Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
            }
            return false;


        }

        public bool AddMeetingRequest(ContactDetails ContactDetails,ClientMeetings Meeting)
        {
            try
            {
                if (!IsConfigured)
                    throw new System.Exception("Outlook 2010+ is not installed on this machine or there was an error logging on to Outlook.");

                Microsoft.Office.Interop.Outlook.AppointmentItem agendaMeeting = (Microsoft.Office.Interop.Outlook.AppointmentItem)
                     this.oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.
                     olAppointmentItem);

                            if (agendaMeeting != null)
                            {
                                agendaMeeting.MeetingStatus =
                                    Microsoft.Office.Interop.Outlook.OlMeetingStatus.olMeeting;
                                agendaMeeting.Location = Meeting.Venue;
                                agendaMeeting.Subject = Meeting.MeetingType;
                                agendaMeeting.Body = " ";
                                agendaMeeting.Start = Meeting.TimeFrom;
                               // agendaMeeting.Duration = (int)Meeting._Duration*60;
                                agendaMeeting.End = Meeting.TimeTo;

                                Microsoft.Office.Interop.Outlook.Recipient recipient =
                                    agendaMeeting.Recipients.Add(ContactDetails.Email1Address);
                                recipient.Type =
                                    (int)Microsoft.Office.Interop.Outlook.OlMeetingRecipientType.olRequired;
                                ((Microsoft.Office.Interop.Outlook._AppointmentItem)agendaMeeting).Send();
                            }
                return true;
            }
            catch (System.Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
            }
            return false;


        }
        public void Dispose()
        {
            if (oApp != null)
            {
                oApp.Quit();
                oApp = null;
            }
        }
    }

}
