namespace Finx.App.Forms
{
    partial class frmConfigure
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl_Configuration = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.htmlPanel_BackgroundImg = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.button_loadPicture = new System.Windows.Forms.Button();
            this.xInput_Caption = new Finx.App.UserControls.xInput();
            this.xInput_FSBNo = new Finx.App.UserControls.xInput();
            this.xInput_RegistrationNumber = new Finx.App.UserControls.xInput();
            this.xInput_CompanyName = new Finx.App.UserControls.xInput();
            this.tabPage_SMS = new System.Windows.Forms.TabPage();
            this.htmlPanel1 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.button_TestSMSConfig = new System.Windows.Forms.Button();
            this.xInput_SMSUri = new Finx.App.UserControls.xInput();
            this.xInput_SMSSecretKey = new Finx.App.UserControls.xInput();
            this.xInput_SMSClientKey = new Finx.App.UserControls.xInput();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.tabControl_Configuration.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.htmlPanel_BackgroundImg.SuspendLayout();
            this.tabPage_SMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl_Configuration);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(20, 60);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox3.Size = new System.Drawing.Size(540, 327);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // tabControl_Configuration
            // 
            this.tabControl_Configuration.Controls.Add(this.tabPage4);
            this.tabControl_Configuration.Controls.Add(this.tabPage_SMS);
            this.tabControl_Configuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Configuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl_Configuration.Location = new System.Drawing.Point(5, 27);
            this.tabControl_Configuration.Name = "tabControl_Configuration";
            this.tabControl_Configuration.SelectedIndex = 0;
            this.tabControl_Configuration.Size = new System.Drawing.Size(530, 295);
            this.tabControl_Configuration.TabIndex = 14;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.Controls.Add(this.htmlPanel_BackgroundImg);
            this.tabPage4.Controls.Add(this.xInput_Caption);
            this.tabPage4.Controls.Add(this.xInput_FSBNo);
            this.tabPage4.Controls.Add(this.xInput_RegistrationNumber);
            this.tabPage4.Controls.Add(this.xInput_CompanyName);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(522, 267);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Background Image";
            // 
            // htmlPanel_BackgroundImg
            // 
            this.htmlPanel_BackgroundImg.AutoScroll = true;
            this.htmlPanel_BackgroundImg.BackColor = System.Drawing.Color.WhiteSmoke;
            this.htmlPanel_BackgroundImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.htmlPanel_BackgroundImg.BaseStylesheet = "";
            this.htmlPanel_BackgroundImg.Controls.Add(this.btnClearImage);
            this.htmlPanel_BackgroundImg.Controls.Add(this.button_loadPicture);
            this.htmlPanel_BackgroundImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel_BackgroundImg.Location = new System.Drawing.Point(3, 3);
            this.htmlPanel_BackgroundImg.Margin = new System.Windows.Forms.Padding(0);
            this.htmlPanel_BackgroundImg.Name = "htmlPanel_BackgroundImg";
            this.htmlPanel_BackgroundImg.Size = new System.Drawing.Size(516, 261);
            this.htmlPanel_BackgroundImg.TabIndex = 25;
            this.htmlPanel_BackgroundImg.TabStop = false;
            this.htmlPanel_BackgroundImg.Text = null;
            // 
            // button_loadPicture
            // 
            this.button_loadPicture.BackColor = System.Drawing.Color.White;
            this.button_loadPicture.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_loadPicture.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_loadPicture.Location = new System.Drawing.Point(386, 6);
            this.button_loadPicture.Name = "button_loadPicture";
            this.button_loadPicture.Size = new System.Drawing.Size(115, 34);
            this.button_loadPicture.TabIndex = 20;
            this.button_loadPicture.Text = "Load New Image";
            this.button_loadPicture.UseVisualStyleBackColor = false;
            this.button_loadPicture.Click += new System.EventHandler(this.button_LoadPicture_Click);
            // 
            // xInput_Caption
            // 
            this.xInput_Caption.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Caption.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_Caption.ControlWidth = 200;
            this.xInput_Caption.DataSource = null;
            this.xInput_Caption.DisplayMember = "Text";
            this.xInput_Caption.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_Caption.LableText = "Caption";
            this.xInput_Caption.Location = new System.Drawing.Point(3, 48);
            this.xInput_Caption.MappedField = null;
            this.xInput_Caption.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Caption.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_Caption.Model = null;
            this.xInput_Caption.Name = "xInput_Caption";
            this.xInput_Caption.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Caption.ReadOnly = false;
            this.xInput_Caption.Size = new System.Drawing.Size(376, 33);
            this.xInput_Caption.TabIndex = 3;
            this.xInput_Caption.Value = "";
            this.xInput_Caption.ValueMember = "Value";
            // 
            // xInput_FSBNo
            // 
            this.xInput_FSBNo.BackColor = System.Drawing.Color.Transparent;
            this.xInput_FSBNo.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_FSBNo.ControlWidth = 200;
            this.xInput_FSBNo.DataSource = null;
            this.xInput_FSBNo.DisplayMember = "Text";
            this.xInput_FSBNo.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_FSBNo.LableText = "FSB Number";
            this.xInput_FSBNo.Location = new System.Drawing.Point(3, 114);
            this.xInput_FSBNo.MappedField = null;
            this.xInput_FSBNo.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_FSBNo.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_FSBNo.Model = null;
            this.xInput_FSBNo.Name = "xInput_FSBNo";
            this.xInput_FSBNo.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_FSBNo.ReadOnly = false;
            this.xInput_FSBNo.Size = new System.Drawing.Size(376, 33);
            this.xInput_FSBNo.TabIndex = 2;
            this.xInput_FSBNo.Value = "";
            this.xInput_FSBNo.ValueMember = "Value";
            // 
            // xInput_RegistrationNumber
            // 
            this.xInput_RegistrationNumber.BackColor = System.Drawing.Color.Transparent;
            this.xInput_RegistrationNumber.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_RegistrationNumber.ControlWidth = 200;
            this.xInput_RegistrationNumber.DataSource = null;
            this.xInput_RegistrationNumber.DisplayMember = "Text";
            this.xInput_RegistrationNumber.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_RegistrationNumber.LableText = "Company Reg. No";
            this.xInput_RegistrationNumber.Location = new System.Drawing.Point(3, 81);
            this.xInput_RegistrationNumber.MappedField = null;
            this.xInput_RegistrationNumber.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_RegistrationNumber.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_RegistrationNumber.Model = null;
            this.xInput_RegistrationNumber.Name = "xInput_RegistrationNumber";
            this.xInput_RegistrationNumber.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_RegistrationNumber.ReadOnly = false;
            this.xInput_RegistrationNumber.Size = new System.Drawing.Size(376, 33);
            this.xInput_RegistrationNumber.TabIndex = 1;
            this.xInput_RegistrationNumber.Value = "";
            this.xInput_RegistrationNumber.ValueMember = "Value";
            // 
            // xInput_CompanyName
            // 
            this.xInput_CompanyName.BackColor = System.Drawing.Color.Transparent;
            this.xInput_CompanyName.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_CompanyName.ControlWidth = 200;
            this.xInput_CompanyName.DataSource = null;
            this.xInput_CompanyName.DisplayMember = "Text";
            this.xInput_CompanyName.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_CompanyName.LableText = "Company Name";
            this.xInput_CompanyName.Location = new System.Drawing.Point(3, 15);
            this.xInput_CompanyName.MappedField = null;
            this.xInput_CompanyName.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_CompanyName.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_CompanyName.Model = null;
            this.xInput_CompanyName.Name = "xInput_CompanyName";
            this.xInput_CompanyName.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_CompanyName.ReadOnly = false;
            this.xInput_CompanyName.Size = new System.Drawing.Size(376, 33);
            this.xInput_CompanyName.TabIndex = 0;
            this.xInput_CompanyName.Value = "";
            this.xInput_CompanyName.ValueMember = "Value";
            // 
            // tabPage_SMS
            // 
            this.tabPage_SMS.Controls.Add(this.htmlPanel1);
            this.tabPage_SMS.Controls.Add(this.button_TestSMSConfig);
            this.tabPage_SMS.Controls.Add(this.xInput_SMSUri);
            this.tabPage_SMS.Controls.Add(this.xInput_SMSSecretKey);
            this.tabPage_SMS.Controls.Add(this.xInput_SMSClientKey);
            this.tabPage_SMS.Location = new System.Drawing.Point(4, 24);
            this.tabPage_SMS.Name = "tabPage_SMS";
            this.tabPage_SMS.Size = new System.Drawing.Size(192, 72);
            this.tabPage_SMS.TabIndex = 3;
            this.tabPage_SMS.Text = "SMS Configuration";
            this.tabPage_SMS.UseVisualStyleBackColor = true;
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanel1.BaseStylesheet = null;
            this.htmlPanel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.htmlPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.htmlPanel1.Location = new System.Drawing.Point(0, 0);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(192, 105);
            this.htmlPanel1.TabIndex = 23;
            this.htmlPanel1.Text = null;
            // 
            // button_TestSMSConfig
            // 
            this.button_TestSMSConfig.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_TestSMSConfig.Location = new System.Drawing.Point(391, 222);
            this.button_TestSMSConfig.Name = "button_TestSMSConfig";
            this.button_TestSMSConfig.Size = new System.Drawing.Size(115, 34);
            this.button_TestSMSConfig.TabIndex = 21;
            this.button_TestSMSConfig.Text = "Save ...";
            this.button_TestSMSConfig.UseVisualStyleBackColor = true;
            this.button_TestSMSConfig.Click += new System.EventHandler(this.button_SaveSMSConfig_Click);
            // 
            // xInput_SMSUri
            // 
            this.xInput_SMSUri.BackColor = System.Drawing.Color.Transparent;
            this.xInput_SMSUri.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_SMSUri.ControlWidth = 200;
            this.xInput_SMSUri.DataSource = null;
            this.xInput_SMSUri.DisplayMember = "Text";
            this.xInput_SMSUri.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_SMSUri.LableText = "SMS Portal URI";
            this.xInput_SMSUri.Location = new System.Drawing.Point(54, 108);
            this.xInput_SMSUri.MappedField = null;
            this.xInput_SMSUri.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_SMSUri.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_SMSUri.Model = null;
            this.xInput_SMSUri.Name = "xInput_SMSUri";
            this.xInput_SMSUri.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_SMSUri.ReadOnly = false;
            this.xInput_SMSUri.Size = new System.Drawing.Size(452, 33);
            this.xInput_SMSUri.TabIndex = 3;
            this.xInput_SMSUri.Value = "";
            this.xInput_SMSUri.ValueMember = "Value";
            // 
            // xInput_SMSSecretKey
            // 
            this.xInput_SMSSecretKey.BackColor = System.Drawing.Color.Transparent;
            this.xInput_SMSSecretKey.ControlTypes = Finx.App.UserControls.ControlTypes.PasswordBox;
            this.xInput_SMSSecretKey.ControlWidth = 200;
            this.xInput_SMSSecretKey.DataSource = null;
            this.xInput_SMSSecretKey.DisplayMember = "Text";
            this.xInput_SMSSecretKey.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_SMSSecretKey.LableText = "Secret Key";
            this.xInput_SMSSecretKey.Location = new System.Drawing.Point(54, 174);
            this.xInput_SMSSecretKey.MappedField = null;
            this.xInput_SMSSecretKey.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_SMSSecretKey.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_SMSSecretKey.Model = null;
            this.xInput_SMSSecretKey.Name = "xInput_SMSSecretKey";
            this.xInput_SMSSecretKey.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_SMSSecretKey.ReadOnly = false;
            this.xInput_SMSSecretKey.Size = new System.Drawing.Size(452, 33);
            this.xInput_SMSSecretKey.TabIndex = 2;
            this.xInput_SMSSecretKey.Value = "";
            this.xInput_SMSSecretKey.ValueMember = "Value";
            // 
            // xInput_SMSClientKey
            // 
            this.xInput_SMSClientKey.BackColor = System.Drawing.Color.Transparent;
            this.xInput_SMSClientKey.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_SMSClientKey.ControlWidth = 200;
            this.xInput_SMSClientKey.DataSource = null;
            this.xInput_SMSClientKey.DisplayMember = "Text";
            this.xInput_SMSClientKey.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_SMSClientKey.LableText = "Client ID";
            this.xInput_SMSClientKey.Location = new System.Drawing.Point(54, 141);
            this.xInput_SMSClientKey.MappedField = null;
            this.xInput_SMSClientKey.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_SMSClientKey.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_SMSClientKey.Model = null;
            this.xInput_SMSClientKey.Name = "xInput_SMSClientKey";
            this.xInput_SMSClientKey.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_SMSClientKey.ReadOnly = false;
            this.xInput_SMSClientKey.Size = new System.Drawing.Size(452, 33);
            this.xInput_SMSClientKey.TabIndex = 1;
            this.xInput_SMSClientKey.Value = "";
            this.xInput_SMSClientKey.ValueMember = "Value";
            // 
            // btnClearImage
            // 
            this.btnClearImage.BackColor = System.Drawing.Color.White;
            this.btnClearImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearImage.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearImage.Location = new System.Drawing.Point(265, 6);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(115, 34);
            this.btnClearImage.TabIndex = 21;
            this.btnClearImage.Text = "Clear Image";
            this.btnClearImage.UseVisualStyleBackColor = false;
            this.btnClearImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // frmConfigure
            // 
            this.ClientSize = new System.Drawing.Size(580, 407);
            this.Controls.Add(this.groupBox3);
            this.Name = "frmConfigure";
            this.Load += new System.EventHandler(this.frmConfigure_Load);
            this.groupBox3.ResumeLayout(false);
            this.tabControl_Configuration.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.htmlPanel_BackgroundImg.ResumeLayout(false);
            this.tabPage_SMS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabControl_Configuration;
        private System.Windows.Forms.TabPage tabPage4;
        private UserControls.xInput xInput_Caption;
        private UserControls.xInput xInput_FSBNo;
        private UserControls.xInput xInput_RegistrationNumber;
        private UserControls.xInput xInput_CompanyName;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel_BackgroundImg;
        private System.Windows.Forms.Button button_loadPicture;
        private System.Windows.Forms.TabPage tabPage_SMS;
        private System.Windows.Forms.Button button_TestSMSConfig;
        private UserControls.xInput xInput_SMSUri;
        private UserControls.xInput xInput_SMSSecretKey;
        private UserControls.xInput xInput_SMSClientKey;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel1;
        private System.Windows.Forms.Button btnClearImage;
    }
}