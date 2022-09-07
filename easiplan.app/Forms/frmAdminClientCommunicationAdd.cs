using Finx.App.Extensions;
using Finx.App.Office;
using Finx.App.UserControls;
using Finx.Domain.Entities;
using Finx.Domain.Services;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.Forms
{
    public partial class frmAdminClientCommunicationAdd : Form
    {
        IList<ClientDetails> model;
        EmailTemplate currentEmailTemplate = new EmailTemplate();
        EmailTemplateService emailTemplateService = new EmailTemplateService(Program.Repository);

        public frmAdminClientCommunicationAdd(IList<ClientDetails> clientDetails)
        {
            InitializeComponent();

            model = clientDetails;

         

            this.xInput_TemplateName.TabIndex = 1;

            xInput_TemplateName.ControlTypes = ControlTypes.ComboList;
            xInput_TemplateName.comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            IList<EmailTemplate> templatesList = (IList<EmailTemplate>)emailTemplateService.List(x => x.Status == "True");
            templatesList.Insert(0,new EmailTemplate() { TemplateName = "" });

            xInput_TemplateName.ValueMember = "TemplateName";
            xInput_TemplateName.DisplayMember = "TemplateName";
            xInput_TemplateName.DataSource = templatesList;
            xInput_TemplateName.Model = currentEmailTemplate;
           
            this.xInput_TemplateSubject.TabIndex = 2;
            this.xInput_TemplateSubject.Model = currentEmailTemplate;

            this.xHtmlEditor1.TabIndex = 3;
            this.xHtmlEditor1.ShowPrintButton = false;
            this.xHtmlEditor1.SetPlaceholders(typeof(ClientDetails));

            this.xInput_Preview.TabIndex = 4;
            this.xInput_Preview.ControlTypes = ControlTypes.CheckBox;
            this.xInput_Preview.InitialiseControl();
            this.xInput_Preview.Value = true;

        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo.SelectedItem != null)
            {
                 currentEmailTemplate = cbo.SelectedItem as EmailTemplate;
                //if (!string.IsNullOrEmpty(currentEmailTemplate.TemplateName))
                //{
                    this.xInput_TemplateSubject.Model = currentEmailTemplate;

                    this.xHtmlEditor1.DataBindings.Clear();
                    this.xHtmlEditor1.DataBindings.Add(new Binding("InnerHtml", currentEmailTemplate, "TemplateBody", false, DataSourceUpdateMode.OnPropertyChanged));
                    this.xHtmlEditor1.Refresh();
                //}
            }
        }

        private void frmManageTemplatesAdd_Load(object sender, EventArgs e)
        {

        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Send via Outlook 2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                //send email via Outlook
                OutlookProxy outlook = new OutlookProxy();

                foreach (var client in model)
                {
                    IList<EmailAddress> recipients = new List<EmailAddress>();
                    recipients.Add(new EmailAddress() { RecipientAddress = client.RecipientAddress, RecipientName = string.Format("{0} {1}", client.LastName,client.FirstName) });// "yjattiem@oldmutual.com"

                    string mailSubject = string.Format(this.xInput_TemplateSubject.Value.ToString().ReplacePlaceholders<ClientDetails>(client));
                    string htmlBody = String.Format("{0}", this.xHtmlEditor1.getHTML(client));

                    outlook.SendEmail(recipients, mailSubject, htmlBody, PreviewBeforeSend: (bool)this.xInput_Preview.Value);
                }

                // this.Close();
            }
            catch (MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            try
            {
              
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }

        }

        private void button_Preview_Click(object sender, EventArgs e)
        {
            try
            {

                //send email via Outlook
                OutlookProxy outlook = new OutlookProxy();

                IList<EmailAddress> recipients = new List<EmailAddress>();
                recipients.Add(new EmailAddress() { RecipientAddress = "test-email@somewhere.com", RecipientName = string.Format("{0}", "Test Email") });// "yjattiem@oldmutual.com"

                string mailSubject = string.Format(this.xInput_TemplateSubject.Value.ToString().ReplacePlaceholders<ClientDetails>(model.FirstOrDefault()));
                string htmlBody = String.Format("{0}", this.xHtmlEditor1.getHTML(model.FirstOrDefault()));

                outlook.SendEmail(recipients, mailSubject, htmlBody,PreviewBeforeSend:true);


            }
            catch (WarningException wx)
            {
                MessageBoxExt.ShowWarning(wx.Message);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
        }
    }
}
