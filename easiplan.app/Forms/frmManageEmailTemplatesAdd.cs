using Finx.App.Extensions;
using Finx.App.Office;
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
    public partial class frmManageEmailTemplatesAdd : Form
    {
        EmailTemplate model;
        EmailTemplateService templateService = new EmailTemplateService(Program.Repository);

        public frmManageEmailTemplatesAdd(EmailTemplate template)
        {
            InitializeComponent();

            model = template;


            if (model.Status == "1") model.Status = "True";
            if (string.IsNullOrEmpty(model.Status) || model.Status == "0") model.Status = "False";

            this.xInput_TemplateName.TabIndex = 1;
            this.xInput_TemplateSubject.TabIndex = 2;
            
            this.xInput_IsActive.TabIndex = 4;

            this.xInput_TemplateName.Model = model;
            this.xInput_TemplateSubject.Model = model;
            
            this.xInput_IsActive.Model = model;

            this.xHtmlEditor1.TabIndex = 3;
            this.xHtmlEditor1.ShowPrintButton = false;
            
            this.xHtmlEditor1.SetPlaceholders(typeof(ClientDetails));
            this.xHtmlEditor1.DataBindings.Add(new Binding("InnerHtml", template, "TemplateBody", false, DataSourceUpdateMode.OnPropertyChanged));
            this.xHtmlEditor1.Refresh();

        }

        private void frmManageTemplatesAdd_Load(object sender, EventArgs e)
        {

        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            if (model.Id > 0)
                model = templateService.Get(model.Id);

            model.Refresh();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (model.Id == 0)
                {
                    templateService.Add(model);
                }

                
                templateService.Update(model);
               

                this.Close();
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
                if (MessageBoxExt.ShowQuestion("Are you sure you wish to delete this template ?"))
                {
                    templateService.Remove(model.Id);


                    this.Close();
                }
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

                ClientDetails clientDetails = Program.Repository.Get<ClientDetails, int>(1);
                clientDetails.Calculate();

                IList<EmailAddress> recipients = new List<EmailAddress>();
                recipients.Add(new EmailAddress() { RecipientAddress = "test-email@somewhere.com", RecipientName = string.Format("{0}", "Test Email") });// "yjattiem@oldmutual.com"

                string mailSubject = string.Format(model.TemplateSubject.ReplacePlaceholders<ClientDetails>(clientDetails));
                string htmlBody = String.Format("{0}", this.xHtmlEditor1.getHTML(clientDetails));

                outlook.SendEmail(recipients, mailSubject, htmlBody);
               
               
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
