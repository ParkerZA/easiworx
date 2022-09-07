using Finx.App.Enums;
using Finx.App.Extensions;
using Finx.App.Office;
using Finx.Domain.Entities;
using Finx.Domain.Services;
using MetroFramework;
using MetroFramework.Controls.Ext;
using MetroFramework.Forms;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App
{
    public partial class frmMetroClientCommunicationEmail : MetroForm
    {
        IList<ClientDetails> clientList;
        ClientDetails ClientDetails = null;

        EmailTemplate EmailTemplate = new EmailTemplate();
        EmailTemplate currentEmailTemplate = new EmailTemplate();
        EmailTemplateService emailTemplateService = new EmailTemplateService(Program.Repository);
        IList<EmailTemplate> emailTemplates;

        Control TemplateNameCntrl;
        Control TemplateSubjectCntrl;
        Control TemplateBodyCntrl;

        public frmMetroClientCommunicationEmail(IList<ClientDetails> model)
        {
            InitializeComponent();

            if (model != null)
                clientList = model;

            ClientDetails = new ClientDetails();

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ResizeRedraw = true;

            this.Text = "Email";

            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);

            this.DisplayHeader = false;
            #endregion

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", " Send Email");
            this.xToolBarMenu1.tbCaptionImage.Image = Properties.Resources.mail_b_42;

            xToolBarMenu1.tbRefresh.Visible = false; xToolBarMenu1.tbRefresh.Enabled = true;
            //xToolBarMenu1.RefreshClicked += toolStripButton_Refresh_Click; ;

            xToolBarMenu1.tbSave.Visible = true; xToolBarMenu1.tbSave.Text = "Send";
            xToolBarMenu1.SaveClicked += toolStripButton_Email_Click;

            xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "Preview";
            xToolBarMenu1.EditClicked += toolStripButton_Preview_Click;

            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;


            //Load the list of available email Templates
            emailTemplates = emailTemplateService.List(null).ToList();
            emailTemplates.Add(new EmailTemplate() { TemplateName = "-none-", TemplateBody = "", TemplateSubject = "" });
        }

        public frmMetroClientCommunicationEmail()
        {
            InitializeComponent();

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ResizeRedraw = true;

            this.Text = "Email";

            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);

            this.DisplayHeader = false;
            #endregion

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", " Manage Email Templates");
            this.xToolBarMenu1.tbCaptionImage.Image = Properties.Resources.mail_b_42;

            xToolBarMenu1.tbRefresh.Visible = true; xToolBarMenu1.tbRefresh.Enabled = true; xToolBarMenu1.tbRefresh.Text = "Preview";
            xToolBarMenu1.RefreshClicked += toolStripButton_Preview_Click; ;

            xToolBarMenu1.tbSave.Visible = true; xToolBarMenu1.tbSave.Text = "Save";
            xToolBarMenu1.SaveClicked += toolStripButton_Save_Click;

            xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "Delete";
            xToolBarMenu1.EditClicked += toolStripButton_Delete_Click;

            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;

            //Load the list of available email Templates
            emailTemplates = emailTemplateService.List(null).ToList();
            emailTemplates.Add(new EmailTemplate() { TemplateName = " New Template ", TemplateBody = "", TemplateSubject = "" });

        }

        private void frmMetroAdminTaskAdd_Load(object sender, EventArgs e)
        {
           

            EmailTemplate.IsLoading = true;
            currentEmailTemplate.IsLoading = true;

            this.metroPanel_AdminTaskAdd.Controls.Clear();

            if (ClientDetails != null)
            {
                this.metroPanel_AdminTaskAdd.Initialise(ClientDetails, cntr =>
                {
                    cntr.For(x => x.RecipientAddress, "Send To : ...", new MetroComboListEditor(Width: this.Width - 155).DataSourceList(clientList.ToListDataItem("RecipientAddress", "RecipientAddress")));
                }, left: 5, top: 5, labelWidth: 100, controlsLayout: ControlsLayout.Horizontal).Format();

                this.metroPanel_AdminTaskAdd.Initialise(currentEmailTemplate, cntr =>
                {
                    cntr.For(x => x.TemplateName, "Template ...", new MetroComboListEditor(Width: 280).DataSourceList(emailTemplates.ToListDataItem("TemplateName", "TemplateName")));
                }, left: 5, top: 35, labelWidth: 100, PropertyChangedHandler: propertyChanged_TemplateName_EventHandler, controlsLayout: ControlsLayout.Horizontal, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

            }
            else
            {
                this.metroPanel_AdminTaskAdd.Initialise(currentEmailTemplate, cntr =>
                {
                    cntr.For(x => x.TemplateName, "Template ...", new MetroComboListEditor(Width: 280).DataSourceList(emailTemplates.ToListDataItem("TemplateName", "TemplateName")));
                }, left: 5, top: 5, labelWidth: 120, PropertyChangedHandler: propertyChanged_TemplateName_EventHandler, controlsLayout: ControlsLayout.Horizontal, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

                this.metroPanel_AdminTaskAdd.Initialise(EmailTemplate, cntr =>
                {
                    cntr.For(x => x.TemplateName, "Template Name", new MetroTextBoxEditor(Width: 280));
                }, left: 5, top: 35, labelWidth: 120, PropertyChangedHandler: propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

                TemplateNameCntrl = metroPanel_AdminTaskAdd.Controls["editor_MetroTextBoxEditor_TemplateName"];
                TemplateNameCntrl.Enabled = false;
            }

            this.metroPanel_AdminTaskAdd.Initialise(currentEmailTemplate, cntr =>
            {
                //cntr.For(x => x.TemplateName, "Select the eMail Template to use ...", new MetroComboListEditor(Width: 280).DataSourceList(emailTemplates.ToListDataItem("TemplateName", "TemplateName"))); 
                cntr.For(x => x.TemplateSubject, "Subject", new MetroTextBoxEditor(Width: this.Width - 50));
                cntr.For(x => x.TemplateBody, "Message", new MetroHtmlEditor(Width: this.Width - 50, Height: this.Height - 260, placeHolderType:typeof(ClientDetails)));
            }, left: 5, top: 65, labelWidth: 220, PropertyChangedHandler: propertyChanged_EventHandler, controlsLayout: ControlsLayout.Vertical, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

            TemplateSubjectCntrl = metroPanel_AdminTaskAdd.Controls["editor_MetroTextBoxEditor_TemplateSubject"];
            TemplateBodyCntrl = metroPanel_AdminTaskAdd.Controls["editor_MetroHtmlEditor_TemplateBody"];
            

            metroPanel_AdminTaskAdd.AutoScroll = true;
            metroPanel_AdminTaskAdd.VerticalMetroScrollbar.Visible = true;

            EmailTemplate.IsLoading = false;
            currentEmailTemplate.IsLoading = false;
        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentEmailTemplate.Id == 0)
                {
                    var result = MessageBoxExt.ShowInput("Enter a Template Name");
                    if (result != null)
                    {
                        currentEmailTemplate.TemplateName = result as string;
                        emailTemplateService.Add(currentEmailTemplate);
                    }
                }
                else
                    emailTemplateService.Update(currentEmailTemplate);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
        }

        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentEmailTemplate.Id > 0)
                {
                    if (MessageBoxExt.ShowQuestion("Are you sure to Delete this Template"))
                    {
                        emailTemplateService.Remove(currentEmailTemplate.Id);

                        frmMetroAdminTaskAdd_Load(sender, e);
                    }
                }
                
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
        }
        //Send Email
        private void toolStripButton_Email_Click(object sender, EventArgs e)
        {
            try
            {
                //send email via Outlook
                OutlookProxy outlook = new OutlookProxy();

                IList<EmailAddress> recipients = new List<EmailAddress>();
              
                foreach (var client in clientList)
                {
                   
                    recipients.Add(new EmailAddress() { RecipientAddress = client.RecipientAddress, RecipientName = string.Format("{0} {1}", client.LastName, client.FirstName) });// "yjattiem@oldmutual.com"

                    string mailSubject = string.Format(currentEmailTemplate.TemplateSubject.ReplacePlaceholders<ClientDetails>(client));
                    //string htmlBody = String.Format("{0}", this.xHtmlEditor1.getHTML(client));

                    //outlook.SendEmail(recipients, mailSubject, htmlBody, PreviewBeforeSend: (bool)this.xInput_Preview.Value);
                }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                xToolBarMenu1.SetEditMode(false);
            }
        }

        //Preview Email
        private void toolStripButton_Preview_Click(object sender, EventArgs e)
        {
            try
            {
                //send email via Outlook
                OutlookProxy outlook = new OutlookProxy();

                foreach (var client in clientList)
                {
                    IList<EmailAddress> recipients = new List<EmailAddress>();
                    recipients.Add(new EmailAddress() { RecipientAddress = client.RecipientAddress, RecipientName = string.Format("{0} {1}", client.LastName, client.FirstName) });// "yjattiem@oldmutual.com"

                    string mailSubject = string.Format(currentEmailTemplate.TemplateSubject.ReplacePlaceholders<ClientDetails>(client));
                    //string htmlBody = String.Format("{0}", this.xHtmlEditor1.getHTML(client));

                    //outlook.SendEmail(recipients, mailSubject, htmlBody, PreviewBeforeSend: (bool)this.xInput_Preview.Value);
                };
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                xToolBarMenu1.SetEditMode(false);
            }
        }

        private void propertyChanged_EventHandler(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                this.xToolBarMenu1.SetEditMode(true);
                             
            }
            catch (Exception x)
            {
               
            }
        }

        private void propertyChanged_TemplateName_EventHandler(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                currentEmailTemplate.IsLoading = true;
                EmailTemplate.IsLoading = true;

                this.xToolBarMenu1.SetEditMode(true);
                if (e.PropertyName == "TemplateName")
                {

                    var _currentEmailTemplate = emailTemplates.Where(x => x.TemplateName == currentEmailTemplate.TemplateName).FirstOrDefault();
                    _currentEmailTemplate.IsLoading = true;

                    if (_currentEmailTemplate != null)
                        _currentEmailTemplate.CopyTo(currentEmailTemplate);
                  

                    if (currentEmailTemplate.TemplateName == " New Template ")
                    {
                        if(TemplateNameCntrl!=null)
                            TemplateNameCntrl.Enabled = true;

                        currentEmailTemplate.Id = 0;
                        currentEmailTemplate.TemplateName = "";
                        currentEmailTemplate.TemplateSubject = "";
                        currentEmailTemplate.TemplateBody = "";

                        EmailTemplate.TemplateName = "";
                    }
                    else
                    {
                        if (TemplateNameCntrl != null)
                            TemplateNameCntrl.Enabled = false;

                        EmailTemplate.TemplateName = currentEmailTemplate.TemplateName;
                    }

                }


            }
            catch (Exception x)
            {
                currentEmailTemplate.TemplateName = "";
                currentEmailTemplate.TemplateSubject = "";
                currentEmailTemplate.TemplateBody = "";
            }
            finally
            {
                currentEmailTemplate.IsLoading = false;
                EmailTemplate.IsLoading = false;

            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (TemplateSubjectCntrl != null)
            {
                TemplateSubjectCntrl.Width = this.Width - 50;
            }

            if (TemplateBodyCntrl != null)
            {
                TemplateBodyCntrl.Width = this.Width - 50;
                TemplateBodyCntrl.Height = this.Height - 260;
            }

            base.OnResize(e);
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
        }
    }
}
