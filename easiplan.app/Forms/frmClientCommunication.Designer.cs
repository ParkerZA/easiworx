namespace Finx.App.Forms
{
    partial class frmClientCommunication
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientCommunication));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.miniToolStrip = new Finx.App.UserControls.ToolStripEx();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbFilter = new System.Windows.Forms.GroupBox();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpEmail = new System.Windows.Forms.TabPage();
            this.EmailEditor = new Finx.App.UserControls.xHtmlEditor();
            this.xInput_EmailTemplateSubject = new Finx.App.UserControls.xInput();
            this.toolStripEx2 = new Finx.App.UserControls.ToolStripEx();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbEmailTemplates = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.addNewEmailTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.updateEmailTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbEmailTemplateDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbSendEmailNow = new System.Windows.Forms.ToolStripButton();
            this.tsbEmailSendBday = new System.Windows.Forms.ToolStripButton();
            this.tbpSms = new System.Windows.Forms.TabPage();
            this.SMSEditor = new Finx.App.UserControls.xHtmlEditor();
            this.toolStripEx1 = new Finx.App.UserControls.ToolStripEx();
            this.tsbSendSMSNow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSmsSendBday = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbSmsTemplates = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton_AddTemplate = new System.Windows.Forms.ToolStripDropDownButton();
            this.addNewSmsTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.updateSmsTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSmsTemplateDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbSmsConfigure = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpEmail.SuspendLayout();
            this.toolStripEx2.SuspendLayout();
            this.tbpSms.SuspendLayout();
            this.toolStripEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "person_add_32.png");
            this.imageList1.Images.SetKeyName(1, "cog_48.png");
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AccessibleName = "New item selection";
            this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.ClickThrough = true;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.miniToolStrip.Location = new System.Drawing.Point(317, 10);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(582, 39);
            this.miniToolStrip.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbFilter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1199, 551);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // grbFilter
            // 
            this.grbFilter.Controls.Add(this.objectListView1);
            this.grbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbFilter.Location = new System.Drawing.Point(0, 0);
            this.grbFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grbFilter.Name = "grbFilter";
            this.grbFilter.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.grbFilter.Size = new System.Drawing.Size(414, 551);
            this.grbFilter.TabIndex = 1;
            this.grbFilter.TabStop = false;
            this.grbFilter.Text = "Select ...";
            // 
            // objectListView1
            // 
            this.objectListView1.CellEditUseWholeCell = false;
            this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListView1.HideSelection = false;
            this.objectListView1.Location = new System.Drawing.Point(13, 31);
            this.objectListView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.Size = new System.Drawing.Size(388, 508);
            this.objectListView1.TabIndex = 8;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(780, 551);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpEmail);
            this.tabControl1.Controls.Add(this.tbpSms);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(4, 19);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(772, 528);
            this.tabControl1.TabIndex = 1;
            // 
            // tbpEmail
            // 
            this.tbpEmail.Controls.Add(this.EmailEditor);
            this.tbpEmail.Controls.Add(this.xInput_EmailTemplateSubject);
            this.tbpEmail.Controls.Add(this.toolStripEx2);
            this.tbpEmail.Location = new System.Drawing.Point(4, 27);
            this.tbpEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpEmail.Name = "tbpEmail";
            this.tbpEmail.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpEmail.Size = new System.Drawing.Size(764, 497);
            this.tbpEmail.TabIndex = 1;
            this.tbpEmail.Text = "Email";
            this.tbpEmail.UseVisualStyleBackColor = true;
            // 
            // EmailEditor
            // 
            this.EmailEditor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EmailEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmailEditor.Changed = false;
            this.EmailEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmailEditor.InnerHtml = null;
            this.EmailEditor.Location = new System.Drawing.Point(4, 131);
            this.EmailEditor.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.EmailEditor.Name = "EmailEditor";
            this.EmailEditor.ShowAlignCenterButton = false;
            this.EmailEditor.ShowAlignLeftButton = false;
            this.EmailEditor.ShowAlignRightButton = false;
            this.EmailEditor.ShowBackColorButton = false;
            this.EmailEditor.ShowBoldButton = false;
            this.EmailEditor.ShowBulletButton = false;
            this.EmailEditor.ShowCopyButton = false;
            this.EmailEditor.ShowCutButton = false;
            this.EmailEditor.ShowFontFamilyButton = false;
            this.EmailEditor.ShowFontSizeButton = false;
            this.EmailEditor.ShowIndentButton = false;
            this.EmailEditor.ShowItalicButton = false;
            this.EmailEditor.ShowJustifyButton = false;
            this.EmailEditor.ShowLinkButton = false;
            this.EmailEditor.ShowNewButton = false;
            this.EmailEditor.ShowOrderedListButton = false;
            this.EmailEditor.ShowOutdentButton = false;
            this.EmailEditor.ShowPasteButton = false;
            this.EmailEditor.ShowPrintButton = false;
            this.EmailEditor.ShowTxtBGButton = false;
            this.EmailEditor.ShowTxtColorButton = false;
            this.EmailEditor.ShowUnderlineButton = false;
            this.EmailEditor.ShowUnlinkButton = false;
            this.EmailEditor.Size = new System.Drawing.Size(756, 362);
            this.EmailEditor.TabIndex = 11;
            // 
            // xInput_EmailTemplateSubject
            // 
            this.xInput_EmailTemplateSubject.BackColor = System.Drawing.Color.Transparent;
            this.xInput_EmailTemplateSubject.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_EmailTemplateSubject.ControlWidth = 200;
            this.xInput_EmailTemplateSubject.DataSource = null;
            this.xInput_EmailTemplateSubject.DisplayMember = "Text";
            this.xInput_EmailTemplateSubject.Dock = System.Windows.Forms.DockStyle.Top;
            this.xInput_EmailTemplateSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Document, ((byte)(0)));
            this.xInput_EmailTemplateSubject.LablePosition = Finx.App.UserControls.LablePosition.Top;
            this.xInput_EmailTemplateSubject.LableText = "Email Subject";
            this.xInput_EmailTemplateSubject.Location = new System.Drawing.Point(4, 53);
            this.xInput_EmailTemplateSubject.MappedField = "";
            this.xInput_EmailTemplateSubject.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_EmailTemplateSubject.MinimumSize = new System.Drawing.Size(67, 37);
            this.xInput_EmailTemplateSubject.Model = null;
            this.xInput_EmailTemplateSubject.Name = "xInput_EmailTemplateSubject";
            this.xInput_EmailTemplateSubject.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_EmailTemplateSubject.ReadOnly = false;
            this.xInput_EmailTemplateSubject.Size = new System.Drawing.Size(756, 78);
            this.xInput_EmailTemplateSubject.TabIndex = 10;
            this.xInput_EmailTemplateSubject.Value = "";
            this.xInput_EmailTemplateSubject.ValueMember = "Value";
            // 
            // toolStripEx2
            // 
            this.toolStripEx2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripEx2.ClickThrough = true;
            this.toolStripEx2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripEx2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tscbEmailTemplates,
            this.toolStripDropDownButton1,
            this.tsbEmailTemplateDelete,
            this.tsbSendEmailNow,
            this.tsbEmailSendBday});
            this.toolStripEx2.Location = new System.Drawing.Point(4, 4);
            this.toolStripEx2.Name = "toolStripEx2";
            this.toolStripEx2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripEx2.Size = new System.Drawing.Size(756, 49);
            this.toolStripEx2.TabIndex = 1;
            this.toolStripEx2.Text = "toolStripEx2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(122, 46);
            this.toolStripLabel2.Text = "Select Template :";
            // 
            // tscbEmailTemplates
            // 
            this.tscbEmailTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbEmailTemplates.DropDownWidth = 200;
            this.tscbEmailTemplates.Name = "tscbEmailTemplates";
            this.tscbEmailTemplates.Size = new System.Drawing.Size(160, 49);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewEmailTemplateToolStripMenuItem,
            this.toolStripSeparator7,
            this.updateEmailTemplateToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::easiplan.app.Properties.Resources.save2;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(34, 46);
            this.toolStripDropDownButton1.ToolTipText = "Save Template";
            // 
            // addNewEmailTemplateToolStripMenuItem
            // 
            this.addNewEmailTemplateToolStripMenuItem.Name = "addNewEmailTemplateToolStripMenuItem";
            this.addNewEmailTemplateToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.addNewEmailTemplateToolStripMenuItem.Text = "Save As New Template";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(240, 6);
            // 
            // updateEmailTemplateToolStripMenuItem
            // 
            this.updateEmailTemplateToolStripMenuItem.Name = "updateEmailTemplateToolStripMenuItem";
            this.updateEmailTemplateToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.updateEmailTemplateToolStripMenuItem.Text = "Update Template";
            // 
            // tsbEmailTemplateDelete
            // 
            this.tsbEmailTemplateDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEmailTemplateDelete.Image = global::easiplan.app.Properties.Resources.delete;
            this.tsbEmailTemplateDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEmailTemplateDelete.Name = "tsbEmailTemplateDelete";
            this.tsbEmailTemplateDelete.Size = new System.Drawing.Size(29, 46);
            this.tsbEmailTemplateDelete.ToolTipText = "Remove Template";
            // 
            // tsbSendEmailNow
            // 
            this.tsbSendEmailNow.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSendEmailNow.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tsbSendEmailNow.Image = global::easiplan.app.Properties.Resources.mail_b_42;
            this.tsbSendEmailNow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSendEmailNow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSendEmailNow.Name = "tsbSendEmailNow";
            this.tsbSendEmailNow.Size = new System.Drawing.Size(134, 46);
            this.tsbSendEmailNow.Text = "Send Now";
            this.tsbSendEmailNow.ToolTipText = "Send Email";
            this.tsbSendEmailNow.Click += new System.EventHandler(this.tsbSendEmailNow_Click_1);
            // 
            // tsbEmailSendBday
            // 
            this.tsbEmailSendBday.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbEmailSendBday.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tsbEmailSendBday.Image = global::easiplan.app.Properties.Resources.icon_time;
            this.tsbEmailSendBday.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbEmailSendBday.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEmailSendBday.Name = "tsbEmailSendBday";
            this.tsbEmailSendBday.Size = new System.Drawing.Size(154, 46);
            this.tsbEmailSendBday.Text = "Send on B\'Day";
            this.tsbEmailSendBday.ToolTipText = "Send Email on Birth Date";
            this.tsbEmailSendBday.Visible = false; //Hiding email on birthday funtion until further notice
            // 
            // tbpSms
            // 
            this.tbpSms.Controls.Add(this.SMSEditor);
            this.tbpSms.Controls.Add(this.toolStripEx1);
            this.tbpSms.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbpSms.Location = new System.Drawing.Point(4, 27);
            this.tbpSms.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpSms.Name = "tbpSms";
            this.tbpSms.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpSms.Size = new System.Drawing.Size(764, 495);
            this.tbpSms.TabIndex = 0;
            this.tbpSms.Text = "Sms";
            this.tbpSms.UseVisualStyleBackColor = true;
            // 
            // SMSEditor
            // 
            this.SMSEditor.Changed = false;
            this.SMSEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SMSEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SMSEditor.InnerHtml = null;
            this.SMSEditor.Location = new System.Drawing.Point(4, 43);
            this.SMSEditor.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.SMSEditor.Name = "SMSEditor";
            this.SMSEditor.ShowAlignCenterButton = false;
            this.SMSEditor.ShowAlignLeftButton = false;
            this.SMSEditor.ShowAlignRightButton = false;
            this.SMSEditor.ShowBackColorButton = false;
            this.SMSEditor.ShowBoldButton = false;
            this.SMSEditor.ShowBulletButton = false;
            this.SMSEditor.ShowCopyButton = false;
            this.SMSEditor.ShowCutButton = false;
            this.SMSEditor.ShowFontFamilyButton = false;
            this.SMSEditor.ShowFontSizeButton = false;
            this.SMSEditor.ShowIndentButton = false;
            this.SMSEditor.ShowItalicButton = false;
            this.SMSEditor.ShowJustifyButton = false;
            this.SMSEditor.ShowLinkButton = false;
            this.SMSEditor.ShowNewButton = false;
            this.SMSEditor.ShowOrderedListButton = false;
            this.SMSEditor.ShowOutdentButton = false;
            this.SMSEditor.ShowPasteButton = false;
            this.SMSEditor.ShowPrintButton = false;
            this.SMSEditor.ShowTxtBGButton = false;
            this.SMSEditor.ShowTxtColorButton = false;
            this.SMSEditor.ShowUnderlineButton = false;
            this.SMSEditor.ShowUnlinkButton = false;
            this.SMSEditor.Size = new System.Drawing.Size(756, 448);
            this.SMSEditor.TabIndex = 1;
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripEx1.ClickThrough = true;
            this.toolStripEx1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripEx1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSendSMSNow,
            this.toolStripSeparator1,
            this.tsbSmsSendBday,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.tscbSmsTemplates,
            this.toolStripButton_AddTemplate,
            this.tsbSmsTemplateDelete,
            this.tsbSmsConfigure});
            this.toolStripEx1.Location = new System.Drawing.Point(4, 4);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripEx1.Size = new System.Drawing.Size(756, 39);
            this.toolStripEx1.TabIndex = 0;
            this.toolStripEx1.Text = "toolStripEx1";
            // 
            // tsbSendSMSNow
            // 
            this.tsbSendSMSNow.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSendSMSNow.Image = global::easiplan.app.Properties.Resources.upload_file;
            this.tsbSendSMSNow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSendSMSNow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSendSMSNow.Name = "tsbSendSMSNow";
            this.tsbSendSMSNow.Size = new System.Drawing.Size(113, 36);
            this.tsbSendSMSNow.Text = "Send Now";
            this.tsbSendSMSNow.ToolTipText = "Send SMS Now";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbSmsSendBday
            // 
            this.tsbSmsSendBday.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSmsSendBday.Image = global::easiplan.app.Properties.Resources.icon_time;
            this.tsbSmsSendBday.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSmsSendBday.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSmsSendBday.Name = "tsbSmsSendBday";
            this.tsbSmsSendBday.Size = new System.Drawing.Size(136, 36);
            this.tsbSmsSendBday.Text = "Send on b\'day";
            this.tsbSmsSendBday.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsbSmsSendBday.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbSmsSendBday.ToolTipText = "Send SMS on birthday";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(122, 36);
            this.toolStripLabel1.Text = "Select Template :";
            // 
            // tscbSmsTemplates
            // 
            this.tscbSmsTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbSmsTemplates.DropDownWidth = 200;
            this.tscbSmsTemplates.Name = "tscbSmsTemplates";
            this.tscbSmsTemplates.Size = new System.Drawing.Size(160, 39);
            // 
            // toolStripButton_AddTemplate
            // 
            this.toolStripButton_AddTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_AddTemplate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewSmsTemplateToolStripMenuItem,
            this.toolStripMenuItem1,
            this.updateSmsTemplateToolStripMenuItem});
            this.toolStripButton_AddTemplate.Image = global::easiplan.app.Properties.Resources.save2;
            this.toolStripButton_AddTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_AddTemplate.Name = "toolStripButton_AddTemplate";
            this.toolStripButton_AddTemplate.Size = new System.Drawing.Size(34, 36);
            this.toolStripButton_AddTemplate.ToolTipText = "Save Template";
            // 
            // addNewSmsTemplateToolStripMenuItem
            // 
            this.addNewSmsTemplateToolStripMenuItem.Name = "addNewSmsTemplateToolStripMenuItem";
            this.addNewSmsTemplateToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.addNewSmsTemplateToolStripMenuItem.Text = "Save As New Template";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(240, 6);
            // 
            // updateSmsTemplateToolStripMenuItem
            // 
            this.updateSmsTemplateToolStripMenuItem.Name = "updateSmsTemplateToolStripMenuItem";
            this.updateSmsTemplateToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.updateSmsTemplateToolStripMenuItem.Text = "Update Template";
            // 
            // tsbSmsTemplateDelete
            // 
            this.tsbSmsTemplateDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSmsTemplateDelete.Image = global::easiplan.app.Properties.Resources.delete;
            this.tsbSmsTemplateDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSmsTemplateDelete.Name = "tsbSmsTemplateDelete";
            this.tsbSmsTemplateDelete.Size = new System.Drawing.Size(29, 36);
            this.tsbSmsTemplateDelete.ToolTipText = "Remove Template";
            // 
            // tsbSmsConfigure
            // 
            this.tsbSmsConfigure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSmsConfigure.Image = global::easiplan.app.Properties.Resources.cog_321;
            this.tsbSmsConfigure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSmsConfigure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSmsConfigure.Name = "tsbSmsConfigure";
            this.tsbSmsConfigure.Size = new System.Drawing.Size(36, 36);
            this.tsbSmsConfigure.Text = "toolStripButton1";
            this.tsbSmsConfigure.ToolTipText = "Configure SMS provider";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(27, 74);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(1199, 551);
            this.splitContainer2.SplitterDistance = 244;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 5;
            // 
            // frmClientCommunication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 650);
            this.Controls.Add(this.splitContainer2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmClientCommunication";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.SubTitle = "";
            this.Text = "Client Communications";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grbFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbpEmail.ResumeLayout(false);
            this.tbpEmail.PerformLayout();
            this.toolStripEx2.ResumeLayout(false);
            this.toolStripEx2.PerformLayout();
            this.tbpSms.ResumeLayout(false);
            this.tbpSms.PerformLayout();
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ImageList imageList1;
        private UserControls.ToolStripEx miniToolStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grbFilter;
        private BrightIdeasSoftware.ObjectListView objectListView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpSms;
        private UserControls.xHtmlEditor SMSEditor;
        private UserControls.ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripButton tsbSendSMSNow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSmsSendBday;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbSmsTemplates;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton_AddTemplate;
        private System.Windows.Forms.ToolStripMenuItem addNewSmsTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem updateSmsTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbSmsTemplateDelete;
        private System.Windows.Forms.TabPage tbpEmail;
        private UserControls.xHtmlEditor EmailEditor;
        private UserControls.xInput xInput_EmailTemplateSubject;
        private UserControls.ToolStripEx toolStripEx2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tscbEmailTemplates;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem addNewEmailTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem updateEmailTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbEmailTemplateDelete;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripButton tsbSendEmailNow;
        private System.Windows.Forms.ToolStripButton tsbEmailSendBday;
        private System.Windows.Forms.ToolStripButton tsbSmsConfigure;
    }
}