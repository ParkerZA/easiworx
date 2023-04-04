using Finx.App.Extensions;
using easiplan.domain.Entities;
using my.domain.lib.core.Registry;
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
using Finx.App.Sms;
using MetroFramework.Forms;

namespace Finx.App.Forms
{
    public partial class frmConfigure : MetroForm
    {
        string BackgroundImage_Filename = string.Empty;

        public frmConfigure()
        {
            InitializeComponent();

            this.Text = "Setup and Configuration";

            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;

        }

        private void frmConfigure_Load(object sender, EventArgs e)
        {
            try
            {
               

                xInput_CompanyName.MappedField = "TradeName"; //xInput_CompanyName.ReadOnly = !xToolBarMenu1.IsInEditMode;
                xInput_CompanyName.Model = Program.Company;
               

                xInput_Caption.MappedField = "Caption"; //xInput_Caption.ReadOnly = !xToolBarMenu1.IsInEditMode;
                xInput_Caption.Model = Program.Company;
               

                xInput_RegistrationNumber.MappedField = "RegistrationNumber"; //xInput_RegistrationNumber.ReadOnly = !xToolBarMenu1.IsInEditMode;
                xInput_RegistrationNumber.Model = Program.Company;
                

                xInput_FSBNo.MappedField = "FSBNumber"; //xInput_FSBNo.ReadOnly = !xToolBarMenu1.IsInEditMode;
                xInput_FSBNo.Model = Program.Company;

                BackgroundImage_Filename = RegistryWrapper.ReadRegistry(Global.RegistryKey, "BackgroundImage");
                if (!string.IsNullOrEmpty(BackgroundImage_Filename))
                    htmlPanel_BackgroundImg.BackgroundImage = Image.FromFile(BackgroundImage_Filename, true);

                #region SMS Provider
                HtmlUtils.LoadHtmlPanel(this.htmlPanel1, "easiplan.app.Html.SmsPortal.html");

                xInput_SMSUri.MappedField = "baseRestUri"; xInput_SMSUri.ReadOnly = !Program.User.IsAdministrator; xInput_SMSUri.ControlWidth = 260;
                xInput_SMSUri.Model = Program.smsConfiguration;
                xInput_SMSClientKey.MappedField = "ClientKey"; xInput_SMSClientKey.ReadOnly = !Program.User.IsAdministrator; xInput_SMSClientKey.ControlWidth = 260;
                xInput_SMSClientKey.Model = Program.smsConfiguration;
                xInput_SMSSecretKey.MappedField = "SecretKey"; xInput_SMSSecretKey.ReadOnly = !Program.User.IsAdministrator; xInput_SMSSecretKey.ControlTypes = UserControls.ControlTypes.PasswordBox; xInput_SMSSecretKey.ControlWidth = 260;
                xInput_SMSSecretKey.Model = Program.smsConfiguration;

                #endregion

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
        }

        private void button_LoadPicture_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Image Files|*.bmp;*.png;*.jpg";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {

                htmlPanel_BackgroundImg.BackgroundImage = Image.FromFile(openFileDialog.FileName, true);

                BackgroundImage_Filename = openFileDialog.FileName;

                RegistryWrapper.WriteRegistry(Global.RegistryKey, "BackgroundImage", BackgroundImage_Filename);
            }
        }

        private void button_SaveSMSConfig_Click(object sender, EventArgs e)
        {
            try
            {
                SmsPortal portal = new SmsPortal(Program.smsConfiguration);
                var balance = portal.GetBalance();

                MessageBoxExt.ShowInformation(string.Format("Success ...You have {0} credits available",balance.Balance ));

                RegistryWrapper.WriteRegistry(Global.RegistryKey, "SmsUri", Program.smsConfiguration.baseRestUri);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "ClientKey", Program.smsConfiguration.ClientKey);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "Secret", Program.smsConfiguration.SecretKey.Encrypt(Global.RegistryKey));


            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }

        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            RegistryWrapper.WriteRegistry(Global.RegistryKey, "BackgroundImage", "");
            htmlPanel_BackgroundImg.BackgroundImage = null;
        }
    }
}
