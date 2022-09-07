namespace Finx.App.Forms
{
    partial class frmLogon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogon));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Logon = new System.Windows.Forms.TabPage();
            this.htmlPanel1 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xInput_Username = new Finx.App.UserControls.xInput();
            this.htmlPanel5 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.btnLogon = new System.Windows.Forms.Button();
            this.checkBox_RememberMe = new System.Windows.Forms.CheckBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.xInput_Password = new Finx.App.UserControls.xInput();
            this.checkBox_WorkOffline = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.htmlPanel2 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.htmlLabel_RegistrationStatus = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.xInput_LicenseKey = new Finx.App.UserControls.xInput();
            this.xInput_MachineKey = new Finx.App.UserControls.xInput();
            this.button_LicenseKey = new System.Windows.Forms.Button();
            this.button_Register = new System.Windows.Forms.Button();
            this.tabPage_Database = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.htmlPanel4 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl_Configuration = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button_ApplyDB = new System.Windows.Forms.Button();
            this.xInput_DbAdminPwd = new Finx.App.UserControls.xInput();
            this.xInput_DbAdminUser = new Finx.App.UserControls.xInput();
            this.xInput_DBCatalog = new Finx.App.UserControls.xInput();
            this.xInput_DbPort = new Finx.App.UserControls.xInput();
            this.xInput_DbType = new Finx.App.UserControls.xInput();
            this.xInput_DbServer = new Finx.App.UserControls.xInput();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btn_ShowLog = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.htmlPanel3 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPage_Logon.SuspendLayout();
            this.htmlPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage_Database.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl_Configuration.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage_Logon);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage_Database);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(20, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 15);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(569, 254);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_Logon
            // 
            this.tabPage_Logon.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage_Logon.Controls.Add(this.htmlPanel1);
            this.tabPage_Logon.Location = new System.Drawing.Point(4, 55);
            this.tabPage_Logon.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage_Logon.Name = "tabPage_Logon";
            this.tabPage_Logon.Size = new System.Drawing.Size(561, 195);
            this.tabPage_Logon.TabIndex = 0;
            this.tabPage_Logon.Text = "Logon";
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.BackColor = System.Drawing.Color.Transparent;
            this.htmlPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("htmlPanel1.BackgroundImage")));
            this.htmlPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.htmlPanel1.BaseStylesheet = "";
            this.htmlPanel1.Controls.Add(this.panel1);
            this.htmlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel1.Location = new System.Drawing.Point(0, 0);
            this.htmlPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(561, 195);
            this.htmlPanel1.TabIndex = 10;
            this.htmlPanel1.TabStop = false;
            this.htmlPanel1.Text = null;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.xInput_Username);
            this.panel1.Controls.Add(this.htmlPanel5);
            this.panel1.Controls.Add(this.btnLogon);
            this.panel1.Controls.Add(this.checkBox_RememberMe);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.xInput_Password);
            this.panel1.Controls.Add(this.checkBox_WorkOffline);
            this.panel1.Location = new System.Drawing.Point(409, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 275);
            this.panel1.TabIndex = 24;
            // 
            // xInput_Username
            // 
            this.xInput_Username.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Username.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_Username.ControlWidth = 150;
            this.xInput_Username.DataSource = null;
            this.xInput_Username.DisplayMember = "Text";
            this.xInput_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput_Username.ForeColor = System.Drawing.Color.Black;
            this.xInput_Username.LablePosition = Finx.App.UserControls.LablePosition.Top;
            this.xInput_Username.LableText = "Username";
            this.xInput_Username.Location = new System.Drawing.Point(16, 97);
            this.xInput_Username.MappedField = null;
            this.xInput_Username.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Username.MaximumSize = new System.Drawing.Size(500, 35);
            this.xInput_Username.MinimumSize = new System.Drawing.Size(30, 60);
            this.xInput_Username.Model = null;
            this.xInput_Username.Name = "xInput_Username";
            this.xInput_Username.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Username.ReadOnly = false;
            this.xInput_Username.Size = new System.Drawing.Size(162, 60);
            this.xInput_Username.TabIndex = 22;
            this.xInput_Username.Value = "";
            this.xInput_Username.ValueMember = "Value";
            // 
            // htmlPanel5
            // 
            this.htmlPanel5.AutoScroll = true;
            this.htmlPanel5.AutoScrollMinSize = new System.Drawing.Size(10, 0);
            this.htmlPanel5.BackColor = System.Drawing.Color.Transparent;
            this.htmlPanel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.htmlPanel5.BaseStylesheet = "";
            this.htmlPanel5.Location = new System.Drawing.Point(1, 1);
            this.htmlPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.htmlPanel5.Name = "htmlPanel5";
            this.htmlPanel5.Padding = new System.Windows.Forms.Padding(5);
            this.htmlPanel5.Size = new System.Drawing.Size(192, 96);
            this.htmlPanel5.TabIndex = 22;
            this.htmlPanel5.TabStop = false;
            this.htmlPanel5.Text = null;
            this.htmlPanel5.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // btnLogon
            // 
            this.btnLogon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogon.Location = new System.Drawing.Point(103, 241);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(75, 34);
            this.btnLogon.TabIndex = 20;
            this.btnLogon.Text = "Logon";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
            // 
            // checkBox_RememberMe
            // 
            this.checkBox_RememberMe.AutoSize = true;
            this.checkBox_RememberMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_RememberMe.ForeColor = System.Drawing.Color.Black;
            this.checkBox_RememberMe.Location = new System.Drawing.Point(25, 215);
            this.checkBox_RememberMe.Name = "checkBox_RememberMe";
            this.checkBox_RememberMe.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox_RememberMe.Size = new System.Drawing.Size(117, 20);
            this.checkBox_RememberMe.TabIndex = 19;
            this.checkBox_RememberMe.Text = "Remember Me";
            this.checkBox_RememberMe.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(16, 241);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 34);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // xInput_Password
            // 
            this.xInput_Password.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Password.ControlTypes = Finx.App.UserControls.ControlTypes.PasswordBox;
            this.xInput_Password.ControlWidth = 150;
            this.xInput_Password.DataSource = null;
            this.xInput_Password.DisplayMember = "Text";
            this.xInput_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput_Password.ForeColor = System.Drawing.Color.Black;
            this.xInput_Password.LablePosition = Finx.App.UserControls.LablePosition.Top;
            this.xInput_Password.LableText = "Password";
            this.xInput_Password.Location = new System.Drawing.Point(16, 156);
            this.xInput_Password.MappedField = null;
            this.xInput_Password.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Password.MaximumSize = new System.Drawing.Size(500, 35);
            this.xInput_Password.MinimumSize = new System.Drawing.Size(30, 60);
            this.xInput_Password.Model = null;
            this.xInput_Password.Name = "xInput_Password";
            this.xInput_Password.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Password.ReadOnly = false;
            this.xInput_Password.Size = new System.Drawing.Size(162, 60);
            this.xInput_Password.TabIndex = 15;
            this.xInput_Password.Value = "";
            this.xInput_Password.ValueMember = "Value";
            // 
            // checkBox_WorkOffline
            // 
            this.checkBox_WorkOffline.AutoSize = true;
            this.checkBox_WorkOffline.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_WorkOffline.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_WorkOffline.Location = new System.Drawing.Point(196, 3);
            this.checkBox_WorkOffline.Name = "checkBox_WorkOffline";
            this.checkBox_WorkOffline.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox_WorkOffline.Size = new System.Drawing.Size(99, 20);
            this.checkBox_WorkOffline.TabIndex = 16;
            this.checkBox_WorkOffline.Text = "Work Offline";
            this.checkBox_WorkOffline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_WorkOffline.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 55);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(601, 275);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Register";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.htmlPanel2);
            this.splitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.Size = new System.Drawing.Size(601, 275);
            this.splitContainer2.SplitterDistance = 273;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 3;
            this.splitContainer2.TabStop = false;
            // 
            // htmlPanel2
            // 
            this.htmlPanel2.AutoScroll = true;
            this.htmlPanel2.AutoScrollMinSize = new System.Drawing.Size(273, 20);
            this.htmlPanel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.htmlPanel2.BaseStylesheet = null;
            this.htmlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel2.Location = new System.Drawing.Point(0, 0);
            this.htmlPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.htmlPanel2.Name = "htmlPanel2";
            this.htmlPanel2.Size = new System.Drawing.Size(273, 275);
            this.htmlPanel2.TabIndex = 13;
            this.htmlPanel2.TabStop = false;
            this.htmlPanel2.Text = "htmlPanel2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.htmlLabel_RegistrationStatus);
            this.groupBox2.Controls.Add(this.xInput_LicenseKey);
            this.groupBox2.Controls.Add(this.xInput_MachineKey);
            this.groupBox2.Controls.Add(this.button_LicenseKey);
            this.groupBox2.Controls.Add(this.button_Register);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(327, 275);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "REGISTRATION";
            // 
            // htmlLabel_RegistrationStatus
            // 
            this.htmlLabel_RegistrationStatus.AutoSize = false;
            this.htmlLabel_RegistrationStatus.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.htmlLabel_RegistrationStatus.BaseStylesheet = null;
            this.htmlLabel_RegistrationStatus.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.htmlLabel_RegistrationStatus.Location = new System.Drawing.Point(8, 162);
            this.htmlLabel_RegistrationStatus.Name = "htmlLabel_RegistrationStatus";
            this.htmlLabel_RegistrationStatus.Size = new System.Drawing.Size(310, 47);
            this.htmlLabel_RegistrationStatus.TabIndex = 13;
            this.htmlLabel_RegistrationStatus.Text = "Registration Status :";
            // 
            // xInput_LicenseKey
            // 
            this.xInput_LicenseKey.BackColor = System.Drawing.Color.Transparent;
            this.xInput_LicenseKey.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_LicenseKey.ControlWidth = 300;
            this.xInput_LicenseKey.DataSource = null;
            this.xInput_LicenseKey.DisplayMember = "Text";
            this.xInput_LicenseKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput_LicenseKey.LablePosition = Finx.App.UserControls.LablePosition.Top;
            this.xInput_LicenseKey.LableText = "Enter License Key and click Activate";
            this.xInput_LicenseKey.Location = new System.Drawing.Point(8, 85);
            this.xInput_LicenseKey.MappedField = null;
            this.xInput_LicenseKey.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_LicenseKey.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_LicenseKey.Model = null;
            this.xInput_LicenseKey.Name = "xInput_LicenseKey";
            this.xInput_LicenseKey.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_LicenseKey.ReadOnly = false;
            this.xInput_LicenseKey.Size = new System.Drawing.Size(310, 53);
            this.xInput_LicenseKey.TabIndex = 12;
            this.xInput_LicenseKey.Value = "";
            this.xInput_LicenseKey.ValueMember = "Value";
            // 
            // xInput_MachineKey
            // 
            this.xInput_MachineKey.BackColor = System.Drawing.Color.Transparent;
            this.xInput_MachineKey.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_MachineKey.ControlWidth = 300;
            this.xInput_MachineKey.DataSource = null;
            this.xInput_MachineKey.DisplayMember = "Text";
            this.xInput_MachineKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput_MachineKey.LablePosition = Finx.App.UserControls.LablePosition.Top;
            this.xInput_MachineKey.LableText = "Machine Key";
            this.xInput_MachineKey.Location = new System.Drawing.Point(8, 29);
            this.xInput_MachineKey.MappedField = null;
            this.xInput_MachineKey.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_MachineKey.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_MachineKey.Model = null;
            this.xInput_MachineKey.Name = "xInput_MachineKey";
            this.xInput_MachineKey.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_MachineKey.ReadOnly = true;
            this.xInput_MachineKey.Size = new System.Drawing.Size(310, 56);
            this.xInput_MachineKey.TabIndex = 9;
            this.xInput_MachineKey.Value = "";
            this.xInput_MachineKey.ValueMember = "Value";
            // 
            // button_LicenseKey
            // 
            this.button_LicenseKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_LicenseKey.Location = new System.Drawing.Point(7, 240);
            this.button_LicenseKey.Name = "button_LicenseKey";
            this.button_LicenseKey.Size = new System.Drawing.Size(117, 27);
            this.button_LicenseKey.TabIndex = 11;
            this.button_LicenseKey.Text = "Register Online";
            this.button_LicenseKey.UseVisualStyleBackColor = true;
            this.button_LicenseKey.Click += new System.EventHandler(this.button_LicenseKey_Click);
            // 
            // button_Register
            // 
            this.button_Register.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Register.Location = new System.Drawing.Point(216, 240);
            this.button_Register.Name = "button_Register";
            this.button_Register.Size = new System.Drawing.Size(103, 27);
            this.button_Register.TabIndex = 4;
            this.button_Register.Text = "Activate";
            this.button_Register.UseVisualStyleBackColor = true;
            this.button_Register.Click += new System.EventHandler(this.button_Register_Click);
            // 
            // tabPage_Database
            // 
            this.tabPage_Database.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage_Database.Controls.Add(this.splitContainer3);
            this.tabPage_Database.Location = new System.Drawing.Point(4, 55);
            this.tabPage_Database.Name = "tabPage_Database";
            this.tabPage_Database.Size = new System.Drawing.Size(601, 275);
            this.tabPage_Database.TabIndex = 1;
            this.tabPage_Database.Text = "Configure";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.htmlPanel4);
            this.splitContainer3.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer3.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer3.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer3.Size = new System.Drawing.Size(601, 275);
            this.splitContainer3.SplitterDistance = 273;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 7;
            this.splitContainer3.TabStop = false;
            // 
            // htmlPanel4
            // 
            this.htmlPanel4.AutoScroll = true;
            this.htmlPanel4.AutoScrollMinSize = new System.Drawing.Size(273, 20);
            this.htmlPanel4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.htmlPanel4.BaseStylesheet = null;
            this.htmlPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel4.Location = new System.Drawing.Point(0, 0);
            this.htmlPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.htmlPanel4.Name = "htmlPanel4";
            this.htmlPanel4.Size = new System.Drawing.Size(273, 275);
            this.htmlPanel4.TabIndex = 14;
            this.htmlPanel4.TabStop = false;
            this.htmlPanel4.Text = "htmlPanel4";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl_Configuration);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox3.Size = new System.Drawing.Size(327, 275);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CONFIGURATION";
            // 
            // tabControl_Configuration
            // 
            this.tabControl_Configuration.Controls.Add(this.tabPage3);
            this.tabControl_Configuration.Controls.Add(this.tabPage4);
            this.tabControl_Configuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Configuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl_Configuration.Location = new System.Drawing.Point(5, 27);
            this.tabControl_Configuration.Name = "tabControl_Configuration";
            this.tabControl_Configuration.SelectedIndex = 0;
            this.tabControl_Configuration.Size = new System.Drawing.Size(317, 243);
            this.tabControl_Configuration.TabIndex = 14;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage3.Controls.Add(this.button_ApplyDB);
            this.tabPage3.Controls.Add(this.xInput_DbAdminPwd);
            this.tabPage3.Controls.Add(this.xInput_DbAdminUser);
            this.tabPage3.Controls.Add(this.xInput_DBCatalog);
            this.tabPage3.Controls.Add(this.xInput_DbPort);
            this.tabPage3.Controls.Add(this.xInput_DbType);
            this.tabPage3.Controls.Add(this.xInput_DbServer);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(309, 215);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Database";
            // 
            // button_ApplyDB
            // 
            this.button_ApplyDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ApplyDB.Location = new System.Drawing.Point(65, 183);
            this.button_ApplyDB.Name = "button_ApplyDB";
            this.button_ApplyDB.Size = new System.Drawing.Size(177, 29);
            this.button_ApplyDB.TabIndex = 20;
            this.button_ApplyDB.Text = "Apply";
            this.button_ApplyDB.UseVisualStyleBackColor = true;
            this.button_ApplyDB.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // xInput_DbAdminPwd
            // 
            this.xInput_DbAdminPwd.BackColor = System.Drawing.Color.Transparent;
            this.xInput_DbAdminPwd.ControlTypes = Finx.App.UserControls.ControlTypes.PasswordBoxNoPreview;
            this.xInput_DbAdminPwd.ControlWidth = 140;
            this.xInput_DbAdminPwd.DataSource = null;
            this.xInput_DbAdminPwd.DisplayMember = "Text";
            this.xInput_DbAdminPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput_DbAdminPwd.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_DbAdminPwd.LableText = "DB Password";
            this.xInput_DbAdminPwd.Location = new System.Drawing.Point(6, 153);
            this.xInput_DbAdminPwd.MappedField = "DbAdminPwd";
            this.xInput_DbAdminPwd.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_DbAdminPwd.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_DbAdminPwd.Model = null;
            this.xInput_DbAdminPwd.Name = "xInput_DbAdminPwd";
            this.xInput_DbAdminPwd.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_DbAdminPwd.ReadOnly = false;
            this.xInput_DbAdminPwd.Size = new System.Drawing.Size(290, 30);
            this.xInput_DbAdminPwd.TabIndex = 19;
            this.xInput_DbAdminPwd.Value = "";
            this.xInput_DbAdminPwd.ValueMember = "Value";
            // 
            // xInput_DbAdminUser
            // 
            this.xInput_DbAdminUser.BackColor = System.Drawing.Color.Transparent;
            this.xInput_DbAdminUser.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_DbAdminUser.ControlWidth = 140;
            this.xInput_DbAdminUser.DataSource = null;
            this.xInput_DbAdminUser.DisplayMember = "Text";
            this.xInput_DbAdminUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput_DbAdminUser.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_DbAdminUser.LableText = "DB User";
            this.xInput_DbAdminUser.Location = new System.Drawing.Point(6, 123);
            this.xInput_DbAdminUser.MappedField = "DbAdminUser";
            this.xInput_DbAdminUser.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_DbAdminUser.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_DbAdminUser.Model = null;
            this.xInput_DbAdminUser.Name = "xInput_DbAdminUser";
            this.xInput_DbAdminUser.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_DbAdminUser.ReadOnly = false;
            this.xInput_DbAdminUser.Size = new System.Drawing.Size(290, 30);
            this.xInput_DbAdminUser.TabIndex = 18;
            this.xInput_DbAdminUser.Value = "";
            this.xInput_DbAdminUser.ValueMember = "Value";
            // 
            // xInput_DBCatalog
            // 
            this.xInput_DBCatalog.BackColor = System.Drawing.Color.Transparent;
            this.xInput_DBCatalog.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_DBCatalog.ControlWidth = 140;
            this.xInput_DBCatalog.DataSource = null;
            this.xInput_DBCatalog.DisplayMember = "Text";
            this.xInput_DBCatalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput_DBCatalog.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_DBCatalog.LableText = "DB Name";
            this.xInput_DBCatalog.Location = new System.Drawing.Point(6, 93);
            this.xInput_DBCatalog.MappedField = "DbCatalog";
            this.xInput_DBCatalog.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_DBCatalog.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_DBCatalog.Model = null;
            this.xInput_DBCatalog.Name = "xInput_DBCatalog";
            this.xInput_DBCatalog.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_DBCatalog.ReadOnly = false;
            this.xInput_DBCatalog.Size = new System.Drawing.Size(290, 30);
            this.xInput_DBCatalog.TabIndex = 17;
            this.xInput_DBCatalog.Value = "";
            this.xInput_DBCatalog.ValueMember = "Value";
            // 
            // xInput_DbPort
            // 
            this.xInput_DbPort.BackColor = System.Drawing.Color.Transparent;
            this.xInput_DbPort.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_DbPort.ControlWidth = 140;
            this.xInput_DbPort.DataSource = null;
            this.xInput_DbPort.DisplayMember = "Text";
            this.xInput_DbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput_DbPort.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_DbPort.LableText = "Port";
            this.xInput_DbPort.Location = new System.Drawing.Point(6, 63);
            this.xInput_DbPort.MappedField = "DbPort";
            this.xInput_DbPort.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_DbPort.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_DbPort.Model = null;
            this.xInput_DbPort.Name = "xInput_DbPort";
            this.xInput_DbPort.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_DbPort.ReadOnly = false;
            this.xInput_DbPort.Size = new System.Drawing.Size(290, 30);
            this.xInput_DbPort.TabIndex = 16;
            this.xInput_DbPort.Value = "";
            this.xInput_DbPort.ValueMember = "Value";
            // 
            // xInput_DbType
            // 
            this.xInput_DbType.BackColor = System.Drawing.Color.Transparent;
            this.xInput_DbType.ControlTypes = Finx.App.UserControls.ControlTypes.ComboList;
            this.xInput_DbType.ControlWidth = 140;
            this.xInput_DbType.DataSource = null;
            this.xInput_DbType.DisplayMember = "Text";
            this.xInput_DbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput_DbType.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_DbType.LableText = "DB Type";
            this.xInput_DbType.Location = new System.Drawing.Point(6, 3);
            this.xInput_DbType.MappedField = "DbType";
            this.xInput_DbType.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_DbType.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_DbType.Model = null;
            this.xInput_DbType.Name = "xInput_DbType";
            this.xInput_DbType.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_DbType.ReadOnly = false;
            this.xInput_DbType.Size = new System.Drawing.Size(290, 30);
            this.xInput_DbType.TabIndex = 15;
            this.xInput_DbType.Value = null;
            this.xInput_DbType.ValueMember = "Value";
            // 
            // xInput_DbServer
            // 
            this.xInput_DbServer.BackColor = System.Drawing.Color.Transparent;
            this.xInput_DbServer.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_DbServer.ControlWidth = 140;
            this.xInput_DbServer.DataSource = null;
            this.xInput_DbServer.DisplayMember = "Text";
            this.xInput_DbServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput_DbServer.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_DbServer.LableText = "Server";
            this.xInput_DbServer.Location = new System.Drawing.Point(6, 33);
            this.xInput_DbServer.MappedField = "DbServer";
            this.xInput_DbServer.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_DbServer.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_DbServer.Model = null;
            this.xInput_DbServer.Name = "xInput_DbServer";
            this.xInput_DbServer.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_DbServer.ReadOnly = false;
            this.xInput_DbServer.Size = new System.Drawing.Size(290, 30);
            this.xInput_DbServer.TabIndex = 14;
            this.xInput_DbServer.Value = "";
            this.xInput_DbServer.ValueMember = "Value";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage4.Controls.Add(this.btn_ShowLog);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(86, 0);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Logs";
            // 
            // btn_ShowLog
            // 
            this.btn_ShowLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ShowLog.Location = new System.Drawing.Point(16, 15);
            this.btn_ShowLog.Name = "btn_ShowLog";
            this.btn_ShowLog.Size = new System.Drawing.Size(272, 29);
            this.btn_ShowLog.TabIndex = 21;
            this.btn_ShowLog.Text = "Show Error Log file.";
            this.btn_ShowLog.UseVisualStyleBackColor = true;
            this.btn_ShowLog.Click += new System.EventHandler(this.btn_ShowLog_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer4);
            this.tabPage1.Location = new System.Drawing.Point(4, 55);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(601, 275);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "About";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer4.Panel1Collapsed = true;
            this.splitContainer4.Panel1MinSize = 0;
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer4.Panel2.Controls.Add(this.htmlPanel3);
            this.splitContainer4.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer4.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer4.Size = new System.Drawing.Size(601, 275);
            this.splitContainer4.SplitterDistance = 25;
            this.splitContainer4.TabIndex = 4;
            this.splitContainer4.TabStop = false;
            // 
            // htmlPanel3
            // 
            this.htmlPanel3.AutoScroll = true;
            this.htmlPanel3.AutoScrollMinSize = new System.Drawing.Size(591, 20);
            this.htmlPanel3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.htmlPanel3.BaseStylesheet = null;
            this.htmlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel3.Location = new System.Drawing.Point(5, 5);
            this.htmlPanel3.Name = "htmlPanel3";
            this.htmlPanel3.Size = new System.Drawing.Size(591, 265);
            this.htmlPanel3.TabIndex = 1;
            this.htmlPanel3.Text = "htmlPanel3";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(20, 60);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(569, 56);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.ForeColor = System.Drawing.Color.Navy;
            this.toolStripLabel1.Image = global::Finx.App.Properties.Resources.finworks_b_42;
            this.toolStripLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(3);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(234, 50);
            this.toolStripLabel1.Text = "finWorks v1.0.0.0";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = global::Finx.App.Properties.Resources.info_32;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(53, 53);
            this.toolStripButton1.Text = "ABOUT";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = global::Finx.App.Properties.Resources.config_32;
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(58, 53);
            this.toolStripButton2.Text = "CONFIG";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton3.Image = global::Finx.App.Properties.Resources.notes_32;
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(68, 53);
            this.toolStripButton3.Text = "REGISTER";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton4.Image = global::Finx.App.Properties.Resources.computer_32;
            this.toolStripButton4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(57, 53);
            this.toolStripButton4.Text = "LOGON";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // frmLogon
            // 
            this.AcceptButton = this.btnLogon;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(609, 334);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogon";
            this.Text = "Mojoe Software Solutions CC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfigure_FormClosing);
            this.Load += new System.EventHandler(this.frmLogon_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Logon.ResumeLayout(false);
            this.htmlPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage_Database.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControl_Configuration.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Logon;
        private System.Windows.Forms.TabPage tabPage_Database;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_Register;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private UserControls.xInput xInput_MachineKey;
        private System.Windows.Forms.Button button_LicenseKey;
        private System.Windows.Forms.TabControl tabControl_Configuration;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button_ApplyDB;
        private UserControls.xInput xInput_DbAdminPwd;
        private UserControls.xInput xInput_DbAdminUser;
        private UserControls.xInput xInput_DBCatalog;
        private UserControls.xInput xInput_DbPort;
        private UserControls.xInput xInput_DbType;
        private UserControls.xInput xInput_DbServer;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel3;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel2;
        private UserControls.xInput xInput_LicenseKey;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel htmlLabel_RegistrationStatus;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel4;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btn_ShowLog;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel1;
        private System.Windows.Forms.Panel panel1;
        private UserControls.xInput xInput_Username;
        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.CheckBox checkBox_RememberMe;
        private System.Windows.Forms.Button btnExit;
        private UserControls.xInput xInput_Password;
        private System.Windows.Forms.CheckBox checkBox_WorkOffline;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel5;
    }
}