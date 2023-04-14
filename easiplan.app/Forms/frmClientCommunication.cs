using BrightIdeasSoftware;
//using Finx.App.Email;
using Finx.App.Enums;
using Finx.App.Extensions;
using Finx.App.Sms;
using Finx.App.UserControls;
using easiplan.domain;
using easiplan.domain.Entities;
using easiplan.domain.Services;
using MetroFramework.Forms;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using easiplan.domain.Views;
using System.Threading;
using za.co.easiworx.office365.net.models;

namespace Finx.App.Forms
{
    public partial class frmClientCommunication : MetroForm
    {
        List<ClientDetailsView> filteredList = new List<ClientDetailsView>();
        List<Instruction> instructionList = new List<Instruction>();

        CommunicationAction _action;

        //Sms Portal
        SmsPortal smsPortal = new SmsPortal(Program.smsConfiguration);
        SmsTemplate _smsTemplate = new SmsTemplate();
        SmsTemplateService smsTemplateService = new SmsTemplateService(Program.Repository);
        BindingList<SmsTemplate> smsTemplateList = new BindingList<SmsTemplate>();

        //Email Outlook
        EmailTemplate _emailTemplate = new EmailTemplate();
        EmailTemplateService emailTemplateService = new EmailTemplateService(Program.Repository);
        BindingList<EmailTemplate> emailTemplateList = new BindingList<EmailTemplate>();

        public frmClientCommunication(List<ClientDetailsView> filteredList, CommunicationAction action)
        {
            InitializeComponent();

            _action = action;

            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;

            switch (action)
            {
                case CommunicationAction.SendEmail:
                    this.Text = "Send Email";
                    tabControl1.TabPages.RemoveAt(1);
                    //filteredList.RemoveAll(c => string.IsNullOrEmpty(c.RecipientAddress) == true);
                    filteredList.Select(c => { c.IsSelected = true; return c; }).ToList();
                    break;
                case CommunicationAction.SendSMS:
                    this.Text = "Send SMS";
                    tabControl1.TabPages.RemoveAt(0);
                    //filteredList.RemoveAll(c => string.IsNullOrEmpty(c.RecipientCell) == true);
                    filteredList.Select(c => { c.IsSelected = true; return c; }).ToList();
                    break;
               
                default:
                    this.Text = "Send Email/SMS";
                    filteredList.Select(c => { c.IsSelected = true; return c; }).ToList();
                    break;
            }

            #region Search Fields
            this.grbFilter.Text = "Recipients";
            this.groupBox1.Text = "Send using Template ...";
            this.groupBox1.Font = this.grbFilter.Font;

            #endregion

            #region  SMS Toolbar
            tsbSmsConfigure.Click += tsbShowConfiguration;
            tsbSmsSendBday.Click += tsbSendSMSBday_Click;
            tsbSendSMSNow.Click += tsbSendSMSNow_Click;

            tsbSmsTemplateDelete.Click += deleteSmsTemplateToolStripMenuItem_Click;

            smsTemplateList = new BindingList<SmsTemplate>(smsTemplateService.List(null).ToList());
            smsTemplateList.Insert(0, new SmsTemplate() { Id = -1, TemplateName = "", TemplateBody = "" });//the first option

            tscbSmsTemplates.ComboBox.DataSource = smsTemplateList;
            tscbSmsTemplates.ComboBox.ValueMember = "TemplateBody";
            tscbSmsTemplates.ComboBox.DisplayMember = "TemplateName";

            tscbSmsTemplates.SelectedIndexChanged += TscbSmsTemplates_SelectedIndexChanged;
            addNewSmsTemplateToolStripMenuItem.Click += addNewSmsTemplateToolStripMenuItem_Click;
            updateSmsTemplateToolStripMenuItem.Click += updateSmsTemplateToolStripMenuItem_Click;

            this.addNewSmsTemplateToolStripMenuItem.Enabled = true;
            this.updateSmsTemplateToolStripMenuItem.Enabled = false;
            this.tsbSmsTemplateDelete.Enabled = false;

            #endregion

            #region  Email Toolbar
            // tsbEmailConfigure.Click += tsbShowConfiguration;
            tsbEmailSendBday.Click += tsbSendEmailBday_Click;
            tsbSendEmailNow.Click += tsbSendEmailNow_Click;
            // sendWithoutPreviewToolStripMenuItem.Click += sendWithoutPreviewToolStripMenuItem_Click;
            // sendWithPreviewToolStripMenuItem.Click += sendWithPreviewToolStripMenuItem_Click;

            tsbEmailTemplateDelete.Click += deleteEmailTemplateToolStripMenuItem_Click;

            emailTemplateList = new BindingList<EmailTemplate>(emailTemplateService.List(null).ToList());
            emailTemplateList.Insert(0, new EmailTemplate() { Id = -1, TemplateName = "", TemplateBody = "" });//the first option

            tscbEmailTemplates.ComboBox.DataSource = emailTemplateList;
            tscbEmailTemplates.ComboBox.ValueMember = "TemplateBody";
            tscbEmailTemplates.ComboBox.DisplayMember = "TemplateName";

            tscbEmailTemplates.SelectedIndexChanged += tscbEmailTemplates_SelectedIndexChanged;

            addNewEmailTemplateToolStripMenuItem.Click += addNewEmailTemplateToolStripMenuItem_Click;
            updateEmailTemplateToolStripMenuItem.Click += updateEmailTemplateToolStripMenuItem_Click;

            this.xInput_EmailTemplateSubject.MappedField = "TemplateSubject";
            this.xInput_EmailTemplateSubject.Model = _emailTemplate;
            this.xInput_EmailTemplateSubject.Label.Font = new Font("Verdana", 9F);

            this.addNewEmailTemplateToolStripMenuItem.Enabled = true;
            this.updateEmailTemplateToolStripMenuItem.Enabled = false;
            this.tsbEmailTemplateDelete.Enabled = false;

            #endregion

            #region ObjectListView
            //http://objectlistview.sourceforge.net/cs/recipes.html
            OLVColumn col0 = new OLVColumn()
            {
                AspectName = "IsSelected",

                CheckBoxes = true,
                HeaderCheckBox = true,
                TriStateCheckBoxes = false,
                MinimumWidth = 20,
                MaximumWidth = 20,
            };
            OLVColumn col1 = new OLVColumn("Client Name", "Fullname")
            {
                IsTileViewColumn = true,
                FreeSpaceProportion = 50,
                FillsFreeSpace = true,
                MinimumWidth = 100,
                MaximumWidth = 500

            };
            OLVColumn col2 = new OLVColumn("Birth Date", "DateOfBirth")
            {
                AspectToStringFormat = "{0:d}",
                IsTileViewColumn = true,
                DataType = typeof(DateTime),
                FreeSpaceProportion = 24,
                FillsFreeSpace = true,

            };
            if (action == CommunicationAction.SendEmail)
            {
                OLVColumn col3 = new OLVColumn("Email", "RecipientAddress")
                {
                    IsTileViewColumn = true,
                    FreeSpaceProportion = 24,
                    FillsFreeSpace = true,
                };

                this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { col0, col1, col3, col2 });
            }
            if (action == CommunicationAction.SendSMS)
            {
                OLVColumn col4 = new OLVColumn("Cellphone", "RecipientCell")
                {
                    IsTileViewColumn = true,
                    FreeSpaceProportion = 24,
                    FillsFreeSpace = true,
                };

                this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { col0, col1, col4, col2 });
            }
            if (action == CommunicationAction.SendAll)
            {
                OLVColumn col3 = new OLVColumn("Email", "RecipientAddress")
                {
                    IsTileViewColumn = true,
                    FreeSpaceProportion = 24,
                    FillsFreeSpace = true,
                    Width = 100
                };
                OLVColumn col4 = new OLVColumn("Cellphone", "RecipientCell")
                {
                    IsTileViewColumn = true,
                    FreeSpaceProportion = 24,
                    FillsFreeSpace = true,
                };
                this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { col0, col1, col3, col4 });
            }
            //OLVColumn col5 = new OLVColumn("Message", "CommunicationMessage")
            //{
            //    IsTileViewColumn = false,
            //     FreeSpaceProportion = 10,
            //    FillsFreeSpace = true,
            //};

            //col5.ImageGetter = new ImageGetterDelegate(StatusImageGetter);

            //col2.GroupKeyGetter = delegate (object rowObject) {
            //    ClientDetails clientDetails = (ClientDetails)rowObject;
            //    return new DateTime(clientDetails.DateOfBirth.Year, clientDetails.DateOfBirth.Month, clientDetails.DateOfBirth.Day);
            //};

            //col2.GroupKeyToTitleConverter = delegate (object groupKey) {
            //    return ((DateTime)groupKey).ToString("dd MMMM");
            //};


                this.objectListView1.ShowGroups = false;

            this.objectListView1.UseAlternatingBackColors = false;

            this.objectListView1.View = View.Details;

            this.objectListView1.SmallImageList = this.imageList1;

            objectListView1.CheckBoxes = true;
            objectListView1.CheckedAspectName = "IsSelected";

            this.objectListView1.Font = new Font(FontFamily.GenericSansSerif, 10F);

            this.filteredList = filteredList;

            this.objectListView1.SetObjects(filteredList);

            #endregion

            #region xHTMLEditor SMS Editor

            //this.SMSEditor.ShowAlignCenterButton = true;
            //this.SMSEditor.ShowAlignLeftButton = true;
            //this.SMSEditor.ShowAlignRightButton = true;
            this.SMSEditor.ShowBoldButton = true;
            //this.SMSEditor.ShowBulletButton = true;
            this.SMSEditor.ShowCopyButton = true;
            this.SMSEditor.ShowCutButton = true;
            //this.SMSEditor.ShowFontFamilyButton = true;
            //this.SMSEditor.ShowFontSizeButton = true;
            //this.SMSEditor.ShowIndentButton = true;
            //this.SMSEditor.ShowItalicButton = true;
            //this.SMSEditor.ShowJustifyButton = true;
            //this.SMSEditor.ShowLinkButton = true;
            this.SMSEditor.ShowPasteButton = true;
            this.SMSEditor.ShowPrintButton = true;
            this.SMSEditor.ShowUnderlineButton = true;
            //this.SMSEditor.ShowUnlinkButton = true;

            this.SMSEditor.SetPlaceholders(typeof(ClientDetailsView));
            //this.SMSEditor.DataBindings.Add(new Binding("InnerHtml", _smsTemplate, "TemplateBody", false, DataSourceUpdateMode.OnPropertyChanged));
            this.SMSEditor.Refresh();
            #endregion

            #region xHTMLEditor Email Editor


            //this.EmailEditor.ShowAlignCenterButton = true;
            //this.EmailEditor.ShowAlignLeftButton = true;
            //this.SMSEditor.ShowAlignRightButton = true;
            this.EmailEditor.ShowBoldButton = true;
            //this.EmailEditor.ShowBulletButton = true;
            this.EmailEditor.ShowCopyButton = true;
            this.EmailEditor.ShowCutButton = true;
            //this.EmailEditor.ShowFontFamilyButton = true;
            //this.EmailEditor.ShowFontSizeButton = true;
            //this.EmailEditor.ShowIndentButton = true;
            //this.EmailEditor.ShowItalicButton = true;
            //this.EmailEditor.ShowJustifyButton = true;
            //this.EmailEditor.ShowLinkButton = true;
            // this.EmailEditor.ShowPasteButton = true;
            // this.EmailEditor.ShowPrintButton = true;
            // this.EmailEditor.ShowUnderlineButton = true;
            //this.EmailEditor.ShowUnlinkButton = true;

            this.EmailEditor.SetPlaceholders(typeof(ClientDetailsView));
            this.EmailEditor.DataBindings.Add(new Binding("InnerHtml", _emailTemplate, "TemplateBody", false, DataSourceUpdateMode.OnPropertyChanged));
            this.EmailEditor.Refresh();
            #endregion
        }

        #region SMS
        private void tsbShowConfiguration(object sender, EventArgs e)
        {
            frmConfigure frm = new frmConfigure();
            frm.ShowDialog(this);

        }

        private void tsbSendSMSBday_Click(object sender, EventArgs e)
        {
            try
            {
                using (new AppWaitCursor(sender))
                {
                    IList<SmsMessage> messages = new List<SmsMessage>();

                    instructionList = new List<Instruction>();

                    if (CanSendSMS(messages))
                    {
                        Cursor = Cursors.WaitCursor;

                        //group messages by send date
                        var grpBirthdayList = messages
                            .GroupBy(u => u.SendDate)
                            .Select(grp => grp.ToList())
                            .ToList();

                        foreach (var grp in grpBirthdayList)
                        {
                            DateTime _deliveryDt = DateTime.Parse(grp[0].SendDate);

                            if (_deliveryDt >= DateTime.Now && _deliveryDt <= DateTime.Now.AddMonths(3))
                            {
                                SmsSendOptions sendOptions = new SmsSendOptions()
                                {
                                    checkOptOuts = true,
                                    //duplicateCheck = "true",
                                    senderId = Program.User.Id.ToString(),
                                    startDeliveryUtc = _deliveryDt.ToUTCString(),
                                    campaignName=""

                                };

                                //Send SMS in batches of 99
                                for (int i = 0; i < grp.Count; i = i + 99)
                                {
                                    smsPortal.SendBulkMessages(sendOptions, grp.Skip(i).Take(99).ToList());
                                }

                                MessageBoxExt.ShowInformation("Messages have been scheduled.");
                            }
                            else
                            {
                                MessageBoxExt.ShowInformation(string.Format("Cannot send sms on birth date : {0} .The max date is 3 months in advance.", _deliveryDt));

                            }

                        }

                        UpdateInstructionList();
                        //reset checked client list
                        this.objectListView1.CheckedObjects = null;
                    }
                }

            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void tsbSendSMSNow_Click(object sender, EventArgs e)
        {
            try
            {
                using (new AppWaitCursor(sender))
                {
                    IList<SmsMessage> messages = new List<SmsMessage>();

                    instructionList = new List<Instruction>();

                    if (CanSendSMS(messages))
                    {
                        Cursor = Cursors.WaitCursor;

                        SmsSendOptions sendOptions = new SmsSendOptions()
                        {
                            checkOptOuts = true,
                            //duplicateCheck = "true",
                            senderId = Program.User.Id.ToString(),
                            startDeliveryUtc = DateTime.Now.ToUTCString(),
                            campaignName = ""
                        };

                        //Send SMS in batches of 99
                        for (int i = 0; i < messages.Count; i = i + 99)
                        {
                            smsPortal.SendBulkMessages(sendOptions, messages.Skip(i).Take(99).ToList());
                        }

                        UpdateInstructionList();
                        //reset checked client list
                        this.objectListView1.CheckedObjects = null;

                        MessageBoxExt.ShowInformation("Messages have been sent");
                    }
                }

            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowException(ax);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool CanSendSMS(IList<SmsMessage> messages)
        {
            try
            {
                var selectedList = filteredList.Where(x => x.IsSelected == true);
                if (selectedList.Count() == 0)
                    throw new ApplicationException("No clients were selected. \r\n\r\nPlease select at least one client from the list on the left");

                var message = SMSEditor.getPlainText();

                if (string.IsNullOrEmpty(message))
                    throw new ApplicationException("No message was added. \r\n\r\nPlease add a message to send.");

                if (message.Length > 160)
                    if (!MessageBoxExt.ShowQuestion("The message contains more than 160 characters. Multiple sms's will be sent. \r\n\r\n Do you wish to continue?"))
                        return false;

                messages.Clear();
                foreach (var client in selectedList)
                {
                    if (!string.IsNullOrEmpty(client.RecipientCell))
                    {
                        //Calc next birth date
                        int _yr = DateTime.Now.Year;
                        int _mnth = client.DateOfBirth.Month;
                        int _day = client.DateOfBirth.Day;

                        if (_mnth < DateTime.Now.Month)
                            _yr += 1;
                        else if ((_mnth == DateTime.Now.Month)&& (_day < DateTime.Now.Day)) //If you want to revert this just removed the if month = date part
                            _yr += 1;

                        DateTime _birthDt = DateTime.Parse(string.Format("{0}-{1}-{2} 08:00:00 AM", _yr, _mnth, _day));

                        var msg = new SmsMessage()
                        {
                            SendDate = _birthDt.ToUTCString(),
                            content = message.ReplacePlaceholders<ClientDetailsView>(client),
                            customerId = string.Format("{0}", string.IsNullOrEmpty(client.IdentificationNo) ? client.IdentificationNo : client.IdentificationNo),
                            destination = client.RecipientCell
                        };

                        messages.Add(msg);

                        //Add Instruction
                        AddInstruction(client, "Send SMS", msg.content, InstructionStatus.Completed.ToText(), msg.content);

                    }

                }

                if (messages.Count <= 0)
                    throw new ApplicationException(string.Format("No valid clients were selected. \r\n\r\nPlease select clients with a valid cellphone number."));
                var _balance = smsPortal.GetBalance();
                if (_balance.Balance < messages.Count)
                    throw new ApplicationException(string.Format("You only have '{0}' credits, not enough to send these '{1}' sms's. \r\n\r\nPlease purchase additional credits or select fewer clients.", _balance.Balance, messages.Count));

                if (!MessageBoxExt.ShowQuestion(string.Format("Are you sure you wish to send these {0} sms's ?", messages.Count)))
                    return false;

                return true;
            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);
                return false;
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
                return false;
            }
        }
        #endregion

        #region Sms Template Select
        private void TscbSmsTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ToolStripComboBox tscbo = sender as ToolStripComboBox;

                _smsTemplate = tscbo.SelectedItem as SmsTemplate;

                this.SMSEditor.setHTML(_smsTemplate.TemplateBody);

                this.addNewSmsTemplateToolStripMenuItem.Enabled = false;
                this.updateSmsTemplateToolStripMenuItem.Enabled = false;
                this.tsbSmsTemplateDelete.Enabled = false;

                if (_smsTemplate.Id == -1)//Adding a new Template
                    this.addNewSmsTemplateToolStripMenuItem.Enabled = true;
                else //updating a template
                {
                    this.updateSmsTemplateToolStripMenuItem.Enabled = true;
                    this.tsbSmsTemplateDelete.Enabled = true;
                }

            }
            catch (Exception x) { }

        }

        private void addNewSmsTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var message = SMSEditor.getPlainText();

                if (string.IsNullOrEmpty(message))
                    throw new ApplicationException("No Sms message was added. \r\n\r\nPlease add a message to save.");

                string templateName = null;

                if (MessageBoxExt.ShowInput(this, "Please provide a Name for this Template", ref templateName) == DialogResult.OK)
                    if (null != templateName)
                    {
                        var smsTemplate = new SmsTemplate() { TemplateName = templateName.ToString(), TemplateBody = message };

                        smsTemplateService.Add(smsTemplate);

                        smsTemplateList.Add(smsTemplate);

                        tscbSmsTemplates.SelectedItem = smsTemplate;
                    }


            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }
            finally
            {
            }
        }

        private void updateSmsTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_smsTemplate.TemplateName))
                    return;

                var message = SMSEditor.getPlainText();

                if (string.IsNullOrEmpty(message))
                    throw new ApplicationException("No Sms message was added. \r\n\r\nPlease add a message to save.");

                _smsTemplate.TemplateBody = message;

                smsTemplateService.Update(_smsTemplate);
            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }

        }

        private void deleteSmsTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(_smsTemplate.TemplateName))
                    return;

                if (MessageBoxExt.ShowQuestion("Are you sure you wish to delete this template ?"))
                {
                    smsTemplateService.Remove(_smsTemplate.Id);

                    smsTemplateList.Remove(_smsTemplate);

                    _smsTemplate = smsTemplateList.FirstOrDefault();
                }
            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }
            finally
            {

                tscbSmsTemplates.SelectedItem = _smsTemplate;
            }
        }

        #endregion

        #region Email
        private void tsbSendEmailBday_Click(object sender, EventArgs e)
        {
            try
            {
                using (new AppWaitCursor(sender))
                {

                    IList<EmailMessage> messages = new List<EmailMessage>();

                    instructionList = new List<Instruction>();

                    if (CanSendEmail(messages, true))
                    {

                        MetroProgressWindow progress = new MetroProgressWindow();
                        progress.SetCaption("Sending Emails. Please wait ...");
                        progress.DataObject = messages;
                        System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(SendEmailAsync), progress);
                        progress.ShowDialog(this);
                        progress.Close();

                        //Update Instruction List
                        UpdateInstructionList();

                        //reset checked client list
                        this.objectListView1.CheckedObjects = null;
                    }

                }
            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void tsbSendEmailNow_Click(object sender, EventArgs e)
        {
            try
            {
                using (new AppWaitCursor(sender))
                {

                    IList<EmailMessage> messages = new List<EmailMessage>();

                    instructionList = new List<Instruction>();

                    if (CanSendEmail(messages))
                    {

                        MetroProgressWindow progress = new MetroProgressWindow();
                        progress.SetCaption("Sending Emails. Please wait ...");
                        progress.DataObject = messages;
                        System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(SendEmailAsync), progress);
                        progress.ShowDialog(this);
                        progress.Close();
                       
                        //Update Instruction List
                        UpdateInstructionList();

                        //reset checked client list
                        this.objectListView1.CheckedObjects = null;

                        MessageBoxExt.ShowInformation("Emails have been sent");
                    }

                }
            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private bool CanSendEmail(IList<EmailMessage> messages, bool SendOnBirthDt = false)
        {
            try
            {
                var selectedList = filteredList.Where(x => x.IsSelected == true);
                if (selectedList.Count() == 0)
                    throw new ApplicationException("No clients were selected. \r\n\r\nPlease select at least one client from the list on the left");

                if (string.IsNullOrEmpty(_emailTemplate.TemplateSubject))
                    throw new ApplicationException("No subject was added. \r\n\r\nPlease add a subject to send.");

                var message = EmailEditor.getHTML();

                if (string.IsNullOrEmpty(message))
                    throw new ApplicationException("No message was added. \r\n\r\nPlease add a message to send.");

                _emailTemplate.TemplateBody = message;

                messages.Clear();

                foreach (var client in selectedList)
                {
                    if (!string.IsNullOrEmpty(client.RecipientAddress))
                    {
                        //Calc next birth date
                        int _yr = DateTime.Now.Year;
                        int _mnth = client.DateOfBirth.Month;
                        int _day = client.DateOfBirth.Day;

                        if (_mnth < DateTime.Now.Month)
                            _yr += 1;
                        else if (_day < DateTime.Now.Day)
                            _yr += 1;

                        DateTime sendDate = SendOnBirthDt ? DateTime.Parse(string.Format("{0}-{1}-{2} 08:00:00 AM", _yr, _mnth, _day)) : DateTime.Now;

                        IList<EmailAddress> _emailAddresses = new List<EmailAddress>();

                        _emailAddresses.Add(new EmailAddress() { RecipientAddress = client.RecipientAddress, RecipientName = client.Fullname, SendAs = SendAs.TO });

                        IList<EmailAttachment> _emailAttachments = new List<EmailAttachment>();
                        //_emailAttachments.Add(new EmailAttachment() { AttachmentData };

                        var msg = new EmailMessage()
                        {
                            SendDate = sendDate,
                            Subject = _emailTemplate.TemplateSubject.ReplacePlaceholders<ClientDetailsView>(client),
                            HtmlBody = _emailTemplate.TemplateBody.ReplacePlaceholders<ClientDetailsView>(client),
                            RTFBody = null,
                            ReferenceId = string.Format("{0}", string.IsNullOrEmpty(client.IdentificationNo) ? client.IdentificationNo : client.IdentificationNo),
                            EmailAddresses = _emailAddresses,
                            EmailAttachments = _emailAttachments,

                        };

                        //Add Message
                        messages.Add(msg);

                        //Add Instruction
                        AddInstruction(client, "Send Email", EmailEditor.getPlainText<ClientDetailsView>(client), InstructionStatus.Completed.ToText(), msg.Subject);
                    }

                }

                if (messages.Count <= 0)
                    throw new ApplicationException(string.Format("No valid clients were selected. \r\n\r\nPlease select clients with a valid email address."));


                if (!MessageBoxExt.ShowQuestion(string.Format("Are you sure you wish to send these {0} emails ?", messages.Count)))
                    return false;

                return true;
            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);
                return false;
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
                return false;
            }
        }
        private void SendEmailAsync(object status)
        {
           
            IProgressDataObjectCallback callback = status as IProgressDataObjectCallback;
            IList<EmailMessage> messages = callback.DataObject as List<EmailMessage>;

            callback.Begin(0, messages.Count);

            foreach (EmailMessage msg in messages)
            {
                callback.SetText(string.Format("sending to ..." + msg.EmailAddresses[0].RecipientAddress));

                Program.OutlookProxy.SendMailAsync(msg, msg.SendDate);

                Thread.Sleep(1000);

                callback.Increment(1);
            }

            if (callback != null)
                callback.End();

        }
        #endregion

        #region Email Template Select
        private void tscbEmailTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ToolStripComboBox tscbo = sender as ToolStripComboBox;

                if (_emailTemplate.Equals(tscbo.SelectedItem as EmailTemplate))
                    return;

                _emailTemplate = tscbo.SelectedItem as EmailTemplate;

                this.xInput_EmailTemplateSubject.Model = _emailTemplate;
                this.xInput_EmailTemplateSubject.Label.Font = new Font("Verdana", 9F);

                this.EmailEditor.setHTML(_emailTemplate.TemplateBody);

                this.addNewEmailTemplateToolStripMenuItem.Enabled = false;
                this.updateEmailTemplateToolStripMenuItem.Enabled = false;
                this.tsbEmailTemplateDelete.Enabled = false;

                if (_emailTemplate.Id <= 0)//Adding a new Template
                    this.addNewEmailTemplateToolStripMenuItem.Enabled = true;
                else //updating a template
                {
                    this.updateEmailTemplateToolStripMenuItem.Enabled = true;
                    this.tsbEmailTemplateDelete.Enabled = true;
                }

            }
            catch (Exception x) { }

        }

        private void addNewEmailTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _emailTemplate.TemplateBody = EmailEditor.getHTML();

                if (string.IsNullOrEmpty(_emailTemplate.TemplateBody))
                    throw new ApplicationException("No Email message was added. \r\n\r\nPlease add a message to save.");

                if (string.IsNullOrEmpty(_emailTemplate.TemplateSubject))
                    throw new ApplicationException("No Email subject was added. \r\n\r\nPlease add a subject to save.");

                string templateName = null;

                if (MessageBoxExt.ShowInput(this, "Please provide a Name for this Template", ref templateName) == DialogResult.OK)
                    if (null != templateName)
                    {
                        var emailTemplate = new EmailTemplate() { TemplateName = templateName.ToString(), TemplateSubject = _emailTemplate.TemplateSubject, TemplateBody = _emailTemplate.TemplateBody };

                        emailTemplateService.Add(emailTemplate);

                        emailTemplateList.Add(emailTemplate);

                        tscbEmailTemplates.SelectedItem = emailTemplate;
                    }


            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }
            finally
            {
            }
        }

        private void updateEmailTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_emailTemplate.TemplateName))
                    return;

                var message = EmailEditor.getHTML();

                if (string.IsNullOrEmpty(message))
                    throw new ApplicationException("No Email message was added. \r\n\r\nPlease add a message to save.");

                var subject = _emailTemplate.TemplateSubject;

                if (string.IsNullOrEmpty(subject))
                    throw new ApplicationException("No Email subject was added. \r\n\r\nPlease add a subject to save.");


                _emailTemplate.TemplateSubject = subject;
                _emailTemplate.TemplateBody = message;

                emailTemplateService.Update(_emailTemplate);
            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }

        }

        private void deleteEmailTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(_emailTemplate.TemplateName))
                    return;

                if (MessageBoxExt.ShowQuestion("Are you sure you wish to delete this template ?"))
                {
                    emailTemplateService.Remove(_emailTemplate.Id);

                    emailTemplateList.Remove(_emailTemplate);

                    _emailTemplate = emailTemplateList.FirstOrDefault();
                }
            }
            catch (ApplicationException ax)
            {
                MessageBoxExt.ShowWarning(ax.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }
            finally
            {

                tscbEmailTemplates.SelectedItem = _emailTemplate;
            }
        }


        #endregion

        #region Instruction
        private void AddInstruction(ClientDetailsView clientDetails, string type, string comment = "", string status = "", string name = "")
        {
            Instruction instruction = new Instruction();
            instruction.ClientId = clientDetails.ClientId;
            instruction.Description = clientDetails.Fullname;
            instruction.InstructionType = InstructionType.CUSTOMTASK;
            instruction.Type = type;
            instruction.Comment = comment;
            instruction.CreateDate = DateTime.Now;
            instruction.Status = status;
            instruction.Name = name;
            instruction.UpdateDate = DateTime.Now;
            instruction.UpdateBy = Program.User.Username;

            instructionList.Add(instruction);
        }

        private void UpdateInstructionList()
        {
            foreach (var instruction in instructionList)
                Program.Repository.Add<Instruction, int>(instruction);

            instructionList.Clear();
        }
        #endregion

        #region Delete Clients

        #endregion

        private void tsbSendEmailNow_Click_1(object sender, EventArgs e)
        {

        }

        private void tsbSmsSendBday_Click(object sender, EventArgs e)
        {

        }
    }

}
