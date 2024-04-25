using easiplan.app.Forms;
using easiplan.domain;
using easiplan.domain.Entities;
using easiplan.domain.Services;
using Finx.App.Enums;
using Finx.App.Extensions;
using Finx.App.Models;
using MetroFramework.Controls.Ext;
using my.domain.lib.core.Extensions;
using my.domain.lib.core.Registry;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Finx.App.Forms
{
    public partial class frmMetroLogin : MetroFramework.Forms.MetroForm
    {
        MetroPasswordEditor pwdEditor = new MetroPasswordEditor(220);
        public frmMetroLogin()
        {
            InitializeComponent();

            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;

            this.ForeColor = Global.DFLT_PRIM_CLR;


            this.Text = string.Format("		{0}", Program.ApplicationVersion());
            this.BackImage = global::easiplan.app.Properties.Resources.finworks_b_42;
            this.BackImagePadding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.BackMaxSize = 50;

            ImageList imageList = new ImageList();
            imageList.Images.Add(global::easiplan.app.Properties.Resources.computer_32);
            imageList.Images.Add(global::easiplan.app.Properties.Resources.config_32);

            metroTabControl_Main.Format(imageList);

            metroTabControl_Main.TabPages[0].Format("Logon", padding: 0);
            metroTabControl_Main.TabPages[1].Format("Register", padding: 0);
            metroTabControl_Main.TabPages[2].Format("Configure", padding: 0);
            metroTabControl_Main.TabPages[3].Format("Terms of Use", padding: 0);

            this.metroTabControl_Main.SelectedIndex = 0;

          //  metroTabControl_Main.TabPages.RemoveAt(2);//Remove confguration tab

            HtmlUtils.LoadHtmlPanel(this.htmlPanel1, "easiplan.app.Html.ForgotPassword.html");
            HtmlUtils.LoadHtmlPanel(this.htmlPanel2, "easiplan.app.Html.Registration.html");
            HtmlUtils.LoadHtmlPanel(this.htmlPanel3, "easiplan.app.Html.Configuration.html");
            HtmlUtils.LoadHtmlPanel(this.htmlPanel4, "easiplan.app.Html.TermsOfUse.html");
            HtmlUtils.LoadHtmlPanel(this.htmlPanel5, "easiplan.app.Html.Welcome.html");
            
            pwdEditor.EnterKeyClicked += PwdEditor_EnterKeyClicked; 

            this.metroPanel_Username.Initialise<User>(Program.User, cntr =>
            {
                cntr.For(x => x.Username, "User Name / Email", new MetroTextBoxEditor(220).ReadOnly(false));
                cntr.For(x => x.Password, "Password", pwdEditor);
            }, ControlsLayout.Vertical, top: 10, left: 30);

            metroCheckBox_RememberMe.Checked = false;// !string.IsNullOrEmpty(Program.User.Username);
            metroCheckBox_RememberMe.CheckedChanged += MetroCheckBox_RememberMe_CheckedChanged;
            metroCheckBox_RememberMe.Left = 30;// 130;
            metroCheckBox_RememberMe.Text = "Show Password";
            MetroCheckBox_RememberMe_CheckedChanged(null, new EventArgs());

            //lbl_LOGON.ForeColor = Global.DFLT_PRIM_CLR;
            lbl_Register.ForeColor = Global.DFLT_PRIM_CLR;
            lbl_DbConfig.ForeColor = Global.DFLT_PRIM_CLR; ;

            this.metroButton_Logon.Width = 175;
            this.metroButton_Logon.Left = 30;// 130;

            metroButton_Logon.BackColor = Global.DFLT_SEC_CLR;

            this.metroPanel_Register.Initialise<LicenseModel>(Program.Licensing, cntr =>
            {
                cntr.For(x => x.MachineKey, "Machine Key", new MetroTextBoxEditor(320).ReadOnly(true));
                cntr.For(x => x.LicenseKey, "License Key", new MetroTextBoxEditor(320));
                cntr.For(x => x.Type, "License Type", new MetroText(150));
                cntr.For(x => x.Status, "License Status", new MetroText(150));
                cntr.For(x => x.ExpiryDate, "Expiry Date", new MetroText(150));

            }, ControlsLayout.Vertical, top: 40);

            Program.ConnectionInfo.UserPwd = ""; //Program.ConnectionInfo.UserName = "";
            this.metroPanel_Configuration.Initialise<MySqlConnectionInfo>(Program.ConnectionInfo, cntr =>
            {
                cntr.For(x => x.ConnectionType, "Db Type", new MetroComboBoxEditor().DataSourceList(Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.DBTypes).ToList()));
                cntr.For(x => x.ServerName, "Server IP", new MetroTextBoxEditor());
                cntr.For(x => x.PortNumber, "Port No", new MetroTextBoxEditor());
                cntr.For(x => x.DbName, "Database Name", new MetroTextBoxEditor());
                cntr.For(x => x.UserName, "Username", new MetroTextBoxEditor());
                cntr.For(x => x.UserPwd, "Password", new MetroPasswordEditor());

            }, top: 50);

            //Hook up the button click events
            this.metroButton_Logon.Click += MetroButton_Logon_Click;
            this.metroButton_Register.Click += MetroButton_Register_Click;
            this.metroButton_CopyMachineKey.Click += MetroButton_CopyMachineKey_Click;
            this.metroButton_Configure.Click += MetroButton_Configure_Click;

            this.metroButton_Logon.CausesValidation = true;//this for the password Control dataSourceUpdateMode set to OnValidation;

            SetLogonEnabled();

        }

        private void MetroCheckBox_RememberMe_CheckedChanged(object sender, EventArgs e)
        {
            pwdEditor.ShowPassword(!metroCheckBox_RememberMe.Checked);
        }

        private void PwdEditor_EnterKeyClicked(object sender, KeyEventArgs e)
        {
            this.metroButton_Logon.Focus();//must do this for the onvalidation event to fire
            MetroButton_Logon_Click(metroButton_Logon, e);
        }

        private void SetLogonEnabled()
        {
            this.metroButton_Logon.Enabled = false;

            HtmlUtils.LoadHtmlPanel(this.htmlPanel1, "easiplan.app.Html.ForgotPassword.html");

#if !DEV && !MOJAFF
            //if (string.IsNullOrEmpty(Program.Licensing.LicenseKey))
            //{
            //    HtmlUtils.LoadHtmlPanel(this.htmlPanel1, "easiplan.app.Html.LicenseKeyRequired.html");
            //    this.metroTabControl_Main.SelectedIndex = 1;
            //    return;
            //};

            //if (!Program.Licensing.Status.Equals("Active"))
            //{
            //    HtmlUtils.LoadHtmlPanel(this.htmlPanel1, "easiplan.app.Html.LicenseKeyRequired.html");
            //    this.metroTabControl_Main.SelectedIndex = 1;
            //    return;
            //};
#endif
            //if (string.IsNullOrEmpty(Program.Licensing.LicenseKey))
            //{
            //    HtmlUtils.LoadHtmlPanel(this.htmlPanel1, "easiplan.app.Html.LicenseKeyRequired.html");
            //    this.metroTabControl_Main.SelectedIndex = 1;
            //    return;
            //};

            if (!Program.Repository.IsConfigured)
            {
                HtmlUtils.LoadHtmlPanel(this.htmlPanel1, "easiplan.app.Html.DBNotConfigured.html");
                return;
            }

            this.metroButton_Logon.Enabled = true;

            this.metroTabControl_Main.SelectedIndex = 0;
        }

        private void MetroButton_Configure_Click(object sender, EventArgs e)
        {
            try
            {
                Program.ConnectionInfo.Validate();

                //Configure Database
                MetroProgressWindow progress2 = new MetroProgressWindow();
                progress2.SetCaption("Configuring Database");
                progress2.SetText("Configuring Database... Please wait.");
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Program.ConfigureDatabase), progress2);
                progress2.ShowDialog();
                progress2.Close();

                //Update the registry
                if (Program.Repository.IsConfigured && !Program.Repository.IsInError)
                {
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbType", Program.ConnectionInfo.ConnectionType);
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbServer", Program.ConnectionInfo.ServerName);
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbCatalog", Program.ConnectionInfo.DbName);
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbSchema", Program.ConnectionInfo.DbSchema);
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbPort", Program.ConnectionInfo.PortNumber);
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbAdminUser", Program.ConnectionInfo.UserName.Encrypt(Global.RegistryKey));
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbAdminPwd", Program.ConnectionInfo.UserPwd.Encrypt(Global.RegistryKey));

                    MessageBoxExt.ShowInformation("Your database has successfully been configured.");
                }
                else
                {
                    MessageBoxExt.ShowWarning("Your database could not be configured due to incorrect or missing configuration parameters. Please check your parameters and re-try.");
                }
            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }

        }

        private void MetroButton_CopyMachineKey_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(Program.Licensing.MachineKey);

                MessageBoxExt.ShowInformation("Your machine key has been copied to the clipboard. Select Paste to insert the text into your application.");
            }
            catch (Exception x)
            {
            }
        }

        private void MetroButton_Register_Click(object sender, EventArgs e)
        {
            try
            {
                using (new AppWaitCursor(this.metroButton_Register))
                {
                    Program.Licensing.Activate().Wait();

                    if (MessageBoxExt.ShowQuestion(string.Format("The license key is a {0} license and expires on {1}. \r\n \r\n By clicking Yes, you accept the conditions stated in the Terms of Use section. \r\n \r\nDo you wish to continue?", Program.Licensing.licenseKeyModel.Type, Program.Licensing.licenseKeyModel.ExpiryDate)))
                    {
                        MetroButton_Configure_Click(sender, e);
                        //Program.ConnectionInfo.Validate();

                        ////Configure Database
                        //MetroProgressWindow progress2 = new MetroProgressWindow();
                        //progress2.SetCaption("Configuring Database");
                        //progress2.SetText("Configuring Database... Please wait.");
                        //System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Program.ConfigureDatabase), progress2);
                        //progress2.ShowDialog();
                        //progress2.Close();

                        SetLogonEnabled();

                    }
                }
            }
            catch (AggregateException wx)
            {
                MessageBoxExt.ShowWarning(wx);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }

        }

        private void MetroButton_Logon_Click(object sender, EventArgs e)
        {
            bool _closeForm = false;

            using (new AppWaitCursor(this.metroButton_Logon))
            {
                try
                {
                    if (!Program.Repository.IsConfigured)
                        throw new WarningException("Your database has not yet been configured.\r\n Please consult your Company Administrator.");

                    if (Program.UserServices == null)
                        Program.UserServices = new UserService(Program.Repository);

//#if MOJAFF || DEV
                Program.UserServices.ValidateUserPassword(ref Program.User);               

//#else
                    ////YJ 2021-09-29 Replace UserPassword Validation with call to easiworx api               
                    //Program.UserAuthenticationService.Authenticate(ref Program.User);
                    //Program.UserServices.VerifyUser(ref Program.User);
//#endif
                    //Set Repository Context UserName
                    Program.Repository.UserName = Program.User.Username;

                    //Set up the Client Details Services for this user
                    Program.ClientDetailsService = new ClientDetailsService(Program.Repository, Program.User);

                    //Remember Username on next Logon
                    //if (this.metroCheckBox_RememberMe.Checked)
                        RegistryWrapper.WriteRegistry(Global.RegistryKey, "Username", Program.User.Username);
                   // else
                      //  RegistryWrapper.WriteRegistry(Global.RegistryKey, "Username", string.Empty);

                    _closeForm = true;

                    //load the main form
                    var m = new metroMdiMain();
                    m.Show();
                }
                catch (AggregateException wx)
                {
                    MessageBoxExt.ShowWarning(wx);
                }
                catch (WarningException zx)
                {
                    MessageBoxExt.ShowWarning(zx);
                }
                catch (Exception x)
                {
                    MessageBoxExt.ShowException(x);
                }
                finally
                {
                    if (_closeForm)
                        this.Hide();
                }
            };
        }

        private void baseForm_Load(object sender, EventArgs e)
        {
        }

        private void metroButton_LogFile_Click(object sender, EventArgs e)
        {
            Program.ShowLog();
        }

       
    }
}
