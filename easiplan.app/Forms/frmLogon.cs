using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
//http://www.quantumsoftware.com.au/Products/WindowsFormsComponents.aspx
using QSS.Components.Windows.Forms;

using Finx.App.Extensions;
using Finx.Domain.Entities;

using my.domain.lib.core.Repository;
using my.domain.lib.core.Registry;
using Finx.App.UserControls;


//https://htmlrenderer.codeplex.com/
using TheArtOfDev.HtmlRenderer.WinForms;
using my.domain.lib.core.Extensions;


namespace Finx.App.Forms
{
    public partial class frmLogon : MetroFramework.Forms.MetroForm
    {
        static bool _IsFormClosing = false;

        #region Form Events
        public frmLogon()
        {
            InitializeComponent();

            // Set the forms icon
            //Color transparentColor = Color.FromArgb(0, 255, 0);
            //Bitmap balloonToolTipBitmap = new Bitmap(typeof(BalloonToolTip).Assembly.GetManifestResourceStream("QSS.Components.Windows.Forms.BalloonToolTip.bmp"));
            //balloonToolTipBitmap.MakeTransparent(transparentColor);
            //base.Icon = Icon.FromHandle(balloonToolTipBitmap.GetHicon());

            //Set the main Caption
            this.toolStripLabel1.Text = string.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);
            this.Text = string.Format("{0} ", Application.CompanyName);

            //Set Tooltips text
            this.btnLogon.SetTooltip("Information", "Hello " + Program.User.Username, "Enter your Username and Password and click here to logon.");
            this.button_LicenseKey.SetTooltip("Information", "Register", "Click here to register your installation online.");
            this.button_Register.SetTooltip("Information", "Activate", "Click here to enter your License Key.");
            this.button_ApplyDB.SetTooltip("Information", "Apply Config", "Click here to apply the database configuration.");

            //load Html Panels
            HtmlUtils.LoadHtmlPanel(this.htmlPanel1, "Finx.App.Html.Welcome.html");
            HtmlUtils.LoadHtmlPanel(this.htmlPanel2, "Finx.App.Html.Registration.html");
            HtmlUtils.LoadHtmlPanel(this.htmlPanel3, "Finx.App.Html.About.html");
            HtmlUtils.LoadHtmlPanel(this.htmlPanel4, "Finx.App.Html.Configuration.html");
            HtmlUtils.LoadHtmlPanel(this.htmlPanel5, "Finx.App.Html.Caption.html");

            // Display Licensing Status
            if (Program.Licensing.IsValid())
                this.htmlLabel_RegistrationStatus.Text = string.Format("<b>License</b> : {0}", Program.Licensing.InstallType);
            else
                this.htmlLabel_RegistrationStatus.Text = string.Format("<b>License</b> : Expired");

            this.htmlLabel_RegistrationStatus.Text += string.Format(" <br/><b>Expire</b> : {0} days", Program.Licensing.ExpireDays);


        }
        private void frmLogon_Load(object sender, EventArgs e)
        {

            #region Logon
            Program.User.PropertyChanged += User_PropertyChanged;

            xInput_Username.MappedField = "Username";
            xInput_Username.Model = Program.User;

            xInput_Password.MappedField = "Password";
            xInput_Password.Model = Program.User;
            
            this.checkBox_RememberMe.Checked = !string.IsNullOrEmpty(Program.User.Username);
            #endregion
            
            #region Registration
            xInput_MachineKey.MappedField = "MachineKey";
            xInput_MachineKey.Model = Program.Licensing;

            xInput_LicenseKey.MappedField = "LicenseKey";
            xInput_LicenseKey.Model = Program.Licensing;

            #endregion

            #region Configuration
            xInput_DbType.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.DBTypes);
            xInput_DbType.MappedField = "ConnectionType";

            xInput_DbServer.MappedField = "ServerName";
            xInput_DbPort.MappedField = "PortNumber";
            xInput_DBCatalog.MappedField = "DbName";
            xInput_DbAdminUser.MappedField = "UserName";
            xInput_DbAdminPwd.MappedField = "UserPwd";

            xInput_DbType.Model = Program.ConnectionInfo;
            xInput_DbServer.Model = Program.ConnectionInfo;
            xInput_DbPort.Model = Program.ConnectionInfo;
            xInput_DBCatalog.Model = Program.ConnectionInfo;
            xInput_DbAdminUser.Model = Program.ConnectionInfo;
            xInput_DbAdminPwd.Model = Program.ConnectionInfo;

            button_ApplyDB.Text = "Configure";
            #endregion

            xInput_Password.textBox.Focus();
        }

        void User_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.btnLogon.HideTooltip();
        }

        private void frmConfigure_FormClosing(object sender, FormClosingEventArgs e)
        {

            //In case windows is trying to shut down, don't hold the process up
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (!_IsFormClosing)
            {
                Application.Exit();
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }
        #endregion

        #region Logon
        private void btnLogon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkBox_WorkOffline.Checked)
                {
                    if (string.IsNullOrEmpty(Program.User.Username))
                    {
                        this.xInput_Username.SetEditTooltip("Required", "Please enter a valid Username");
                        return;
                    }
                    if (string.IsNullOrEmpty(Program.User.Password))
                    {
                        this.xInput_Password.SetEditTooltip("Required", "Please enter a valid Password");
                        return;
                    }

                    if (!Program.Repository.IsConfigured)
                        throw new Exception("Your database configuration is either incorrect or incomplete. Please contact your database adminstrator.");

                    //Check valid Username/Password has been entered
                    IEnumerable<User> _users = null;
                    try
                    {
                        //Passwords is encrypted with salt so cannot be used in query
                        _users = Program.Repository.List<User, int>(x => x.Username == Program.User.Username  ); // && x.IsActive=true && && x.Password == Program.User.Password

                    }
                    //catch (SchemaException sx)
                    //{
                    //    //possible update to schema required
                    //    ProgressWindow progress = new ProgressWindow();
                    //    progress.Text = "Updating Database";
                    //    progress.Caption.Text = "Your database is out of date... Please wait while we update your configuration.";

                    //    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Program.ConfigureDatabase), progress);

                    //    progress.ShowDialog();
                    //}
                    catch (Exception x)
                    {
                        MessageBoxExt.ShowException(x);
                        return;
                    }

                    //Check user list
                    if (_users == null|| _users.Count()==0)
                    {
                       // MessageBoxExt.ShowWarning("Invalid Username or Password");
                        this.btnLogon.SetTooltip("Error","Logon Failure","An invalid Username or Password was entered.",true);
                        return;
                    }

                    bool UserIsValid = false;
                 
                    foreach (var _user in _users)
                    {
                        if (_user.Password == Program.User.Password)
                        {
                            Program.User = _user;
                            UserIsValid = true;
                            break;
                        }
                    }

                    if (!UserIsValid)
                    {
                        this.btnLogon.SetTooltip("Error", "Logon Failure", "An invalid Username or Password was entered.",true);
                        return;
                    }

                    if (!Program.User.IsActive)
                    {
                        this.btnLogon.SetTooltip("Error", "Logon Failure", "Your account has been de-activated.", true);
                        return;
                    }

                    //if (!Program.User.computeHash().SequenceEqual(Program.User.Hash))
                    //{
                    //    //MessageBoxExt.ShowWarning("Possible database tampering detected. Please contact your database administrator.");
                    //    this.btnLogon.SetTooltip("Error", "Logon Failure", "Possible database tampering detected. Please contact your database administrator.",true);
                    //    return;
                    //}


                    if (this.checkBox_RememberMe.Checked)
                        RegistryWrapper.WriteRegistry(Global.RegistryKey, "Username", Program.User.Username);
                    else
                        RegistryWrapper.WriteRegistry(Global.RegistryKey, "Username", string.Empty);

                    //Check if the application is up-to-date
                    try
                    {
                        DbVersion dbVersion = Program.Repository.List<DbVersion, int>(null).FirstOrDefault();

                        if (int.Parse(dbVersion.Version) > int.Parse(Global.CurrentAppVersion))
                            throw new WarningException();

                        //if (int.Parse(dbVersion.Version) < int.Parse(Global.CurrentAppVersion))
                        //    throw new OutOfDateException(null);

                    }
                    catch (WarningException x)
                    {
                        MessageBoxExt.ShowWarning("Your application is out of date... Please connect to the internet and restart the application to download the latest version.");
                        return;
                    }
                    //catch (OutOfDateException x1)
                    //{

                    //    ProgressWindow progress = new ProgressWindow();
                    //    progress.Text = "Updating Database";
                    //    progress.Caption.Text = "Your database is out of date... Please wait while we update your configuration.";

                    //    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Program.ConfigureDatabase), progress);
                    //    progress.ShowDialog();
                    //}
                    catch (Exception x2)
                    {
                        MessageBoxExt.ShowException(x2, "Error checking database version.");
                        return;
                    }

                    //Get the default Company Details
                    Program.Company = Program.Repository.List<Company,int>(null).FirstOrDefault();
                    if (Program.Company == null)
                        Program.Company = new Company() { };

                    //Set Repository Context UserName
                    Program.Repository.UserName = Program.User.Username;
                }

                //Check Licensing
                if (!Program.Licensing.IsValid())
                {
                    if (MessageBoxExt.ShowQuestion("Your license key has expired. Click 'Yes' to renew your license key or 'No' to continue with reduced functionality."))
                    {
                        this.tabControl1.SelectedIndex = 1;//Registration Tab
                        return;
                    }
                }
                else
                {
                    if (Program.Licensing.ExpireDays < 30)
                        MessageBoxExt.ShowWarning(string.Format("Your license key will expire in {0} days. Please concider renewing your license soon.", Program.Licensing.ExpireDays));
                }


                _IsFormClosing = true;

                this.Hide();

                //load the main form
                var m = new mdiMain();
                m.Show();
                
               // this.Close();
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x,"Error trying to logon.");
            }

        }

        private void checkBox_WorkOffline_CheckedChanged(object sender, EventArgs e)
        {
           // CheckBox chk = (CheckBox)sender;
        }
        #endregion

        #region Registration
        private void button_Register_Click(object sender, EventArgs e)
        {
           
            EditBalloonToolTip editBalloonToolTip = new EditBalloonToolTip();
            try
            {
                if (string.IsNullOrEmpty(Program.Licensing.LicenseKey))
                {
                    editBalloonToolTip.Icon = BalloonIcon.Error;
                    editBalloonToolTip.Title = "Required";
                    editBalloonToolTip.SetToolTip(this.xInput_LicenseKey.textBox, "Please enter a valid License Key");
                    return;
                }

                Program.Licensing.Validate();

                if (MessageBoxExt.ShowQuestion(string.Format("The license key is a {0} license and valid for {1} days. By clicking Yes, you accept the Terms and Conditions stated in the About section. \r\n \r\n   Do you wish to continue?", Program.Licensing.InstallType, Program.Licensing.ExpireDays)))
                {
                    Program.Licensing.WriteRegistry("license", Program.Licensing.LicenseKey);
                    Program.Licensing.WriteRegistry("install", Program.Licensing.InstallDt.ToString());
                       
                    return;
                }

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }

        }

        private void button_LicenseKey_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Global.RegisterUrl);
              
            }
            catch (Exception x)
            {

                MessageBoxExt.ShowWarning(x.Message);

            }
        }

        #endregion

        #region Configuration
        private void btnApply_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            btn.Text = "Configuring...Please wait.";

            this.Enabled = false;

            try
            {
                bool UpdateSchema = true;//TO DO: Check when to update schema
                bool NewSchema = true; ////TO DO: Check when to add schema

                Program.ConfigureDatabase(NewSchema, UpdateSchema);

                //Update the registry
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbType", Program.ConnectionInfo.ConnectionType);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbServer", Program.ConnectionInfo.ServerName);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbCatalog", Program.ConnectionInfo.DbName);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbSchema", Program.ConnectionInfo.DbSchema);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbPort", Program.ConnectionInfo.PortNumber);
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbAdminUser", Program.ConnectionInfo.UserName.Encrypt(Global.RegistryKey));
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbAdminPwd", Program.ConnectionInfo.UserPwd.Encrypt(Global.RegistryKey));

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            finally
            {
                btn.Text = "Configure";
                this.Enabled = true;
              
            }
        }

        private void btn_ShowLog_Click(object sender, EventArgs e)
        {
            Program.ShowLog();
        }
        #endregion

        #region TabControl Select
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
            xInput_Password.textBox.Focus();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 2;
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 3;
        }
        #endregion

        #region Html Panel
        private void htmlPanel1_Paint(object sender, PaintEventArgs e)
        {
           
            //Bitmap bmp = new Bitmap(10, 10);

            //using (Graphics g = Graphics.FromImage(bmp))
            //{
            //    g.Clear(Color.White);
              
            //    g.FillRectangle(SystemBrushes.Control, new Rectangle(0, 0, 5, 5));
            //    g.FillRectangle(SystemBrushes.Control, new Rectangle(5, 5, 5, 5));
            //}

            //e.Graphics.DrawImage(bmp, PointF.Empty);

            //using (TextureBrush b = new TextureBrush(bmp, System.Drawing.Drawing2D.WrapMode.Tile))
            //{
            //    e.Graphics.FillRectangle(b, htmlPanel1.ClientRectangle);
            //}

            //bmp.Dispose();
        }


        #endregion

    }
}
