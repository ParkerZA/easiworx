namespace Finx.App.Forms
{
    partial class frmClientManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientManagement));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbFilter = new System.Windows.Forms.GroupBox();
            this.xInput_Advisor = new Finx.App.UserControls.xInput();
            this.xInput_BirthdayDay = new Finx.App.UserControls.xInput();
            this.xInput_BirthdayMonth = new Finx.App.UserControls.xInput();
            this.xInput1_Identification = new Finx.App.UserControls.xInput();
            this.xInput_ClientSegment = new Finx.App.UserControls.xInput();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xInput_ShowCompletedTasks = new Finx.App.UserControls.xInput();
            this.metroButton_Refresh = new MetroFramework.Controls.MetroButton();
            this.xInput_Surname = new Finx.App.UserControls.xInput();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGrid_ClientDetails = new SourceGrid.DataGrid();
            this.toolStripEx1 = new Finx.App.UserControls.ToolStripEx();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Download = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Email = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_SMS = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblClientCaption = new System.Windows.Forms.ToolStripLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGrid_ClientInstructions = new SourceGrid.DataGrid();
            this.toolStripEx2 = new Finx.App.UserControls.ToolStripEx();
            this.toolStripLabel_Client = new System.Windows.Forms.ToolStripLabel();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbClientRating = new System.Windows.Forms.ToolStripButton();
            this.miniToolStrip = new Finx.App.UserControls.ToolStripEx();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grbFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStripEx1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.toolStripEx2.SuspendLayout();
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
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(20, 60);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(1097, 483);
            this.splitContainer2.SplitterDistance = 163;
            this.splitContainer2.TabIndex = 5;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbFilter);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1097, 163);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 5;
            // 
            // grbFilter
            // 
            this.grbFilter.Controls.Add(this.xInput_Advisor);
            this.grbFilter.Controls.Add(this.xInput_BirthdayDay);
            this.grbFilter.Controls.Add(this.xInput_BirthdayMonth);
            this.grbFilter.Controls.Add(this.xInput1_Identification);
            this.grbFilter.Controls.Add(this.xInput_ClientSegment);
            this.grbFilter.Controls.Add(this.panel2);
            this.grbFilter.Controls.Add(this.xInput_Surname);
            this.grbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbFilter.Location = new System.Drawing.Point(0, 0);
            this.grbFilter.Name = "grbFilter";
            this.grbFilter.Padding = new System.Windows.Forms.Padding(10);
            this.grbFilter.Size = new System.Drawing.Size(1097, 163);
            this.grbFilter.TabIndex = 1;
            this.grbFilter.TabStop = false;
            this.grbFilter.Text = "Search Clients by ...";
            // 
            // xInput_Advisor
            // 
            this.xInput_Advisor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xInput_Advisor.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Advisor.ControlTypes = Finx.App.UserControls.ControlTypes.ComboBox;
            this.xInput_Advisor.ControlWidth = 175;
            this.xInput_Advisor.DataSource = null;
            this.xInput_Advisor.DisplayMember = "Text";
            this.xInput_Advisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Document, ((byte)(0)));
            this.xInput_Advisor.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_Advisor.LableText = "Advisor";
            this.xInput_Advisor.Location = new System.Drawing.Point(552, 56);
            this.xInput_Advisor.MappedField = "";
            this.xInput_Advisor.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Advisor.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_Advisor.Model = null;
            this.xInput_Advisor.Name = "xInput_Advisor";
            this.xInput_Advisor.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Advisor.ReadOnly = false;
            this.xInput_Advisor.Size = new System.Drawing.Size(289, 30);
            this.xInput_Advisor.TabIndex = 28;
            this.xInput_Advisor.Value = null;
            this.xInput_Advisor.ValueMember = "Value";
            // 
            // xInput_BirthdayDay
            // 
            this.xInput_BirthdayDay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xInput_BirthdayDay.BackColor = System.Drawing.Color.Transparent;
            this.xInput_BirthdayDay.ControlTypes = Finx.App.UserControls.ControlTypes.DatePicker;
            this.xInput_BirthdayDay.ControlWidth = 80;
            this.xInput_BirthdayDay.DataSource = null;
            this.xInput_BirthdayDay.DisplayMember = "Text";
            this.xInput_BirthdayDay.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_BirthdayDay.LableText = "Birth Day";
            this.xInput_BirthdayDay.Location = new System.Drawing.Point(332, 56);
            this.xInput_BirthdayDay.MappedField = null;
            this.xInput_BirthdayDay.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_BirthdayDay.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_BirthdayDay.Model = null;
            this.xInput_BirthdayDay.Name = "xInput_BirthdayDay";
            this.xInput_BirthdayDay.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_BirthdayDay.ReadOnly = false;
            this.xInput_BirthdayDay.Size = new System.Drawing.Size(220, 30);
            this.xInput_BirthdayDay.TabIndex = 27;
            this.xInput_BirthdayDay.Value = null;
            this.xInput_BirthdayDay.ValueMember = "Value";
            // 
            // xInput_BirthdayMonth
            // 
            this.xInput_BirthdayMonth.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xInput_BirthdayMonth.BackColor = System.Drawing.Color.Transparent;
            this.xInput_BirthdayMonth.ControlTypes = Finx.App.UserControls.ControlTypes.DatePicker;
            this.xInput_BirthdayMonth.ControlWidth = 80;
            this.xInput_BirthdayMonth.DataSource = null;
            this.xInput_BirthdayMonth.DisplayMember = "Text";
            this.xInput_BirthdayMonth.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_BirthdayMonth.LableText = "Birth Month";
            this.xInput_BirthdayMonth.Location = new System.Drawing.Point(332, 26);
            this.xInput_BirthdayMonth.MappedField = null;
            this.xInput_BirthdayMonth.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_BirthdayMonth.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_BirthdayMonth.Model = null;
            this.xInput_BirthdayMonth.Name = "xInput_BirthdayMonth";
            this.xInput_BirthdayMonth.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_BirthdayMonth.ReadOnly = false;
            this.xInput_BirthdayMonth.Size = new System.Drawing.Size(220, 30);
            this.xInput_BirthdayMonth.TabIndex = 26;
            this.xInput_BirthdayMonth.Value = null;
            this.xInput_BirthdayMonth.ValueMember = "Value";
            // 
            // xInput1_Identification
            // 
            this.xInput1_Identification.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xInput1_Identification.BackColor = System.Drawing.Color.Transparent;
            this.xInput1_Identification.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput1_Identification.ControlWidth = 180;
            this.xInput1_Identification.DataSource = null;
            this.xInput1_Identification.DisplayMember = "Text";
            this.xInput1_Identification.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Document, ((byte)(0)));
            this.xInput1_Identification.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput1_Identification.LableText = "ID Number";
            this.xInput1_Identification.Location = new System.Drawing.Point(10, 56);
            this.xInput1_Identification.MappedField = "";
            this.xInput1_Identification.Margin = new System.Windows.Forms.Padding(0);
            this.xInput1_Identification.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput1_Identification.Model = null;
            this.xInput1_Identification.Name = "xInput1_Identification";
            this.xInput1_Identification.Padding = new System.Windows.Forms.Padding(1);
            this.xInput1_Identification.ReadOnly = false;
            this.xInput1_Identification.Size = new System.Drawing.Size(320, 30);
            this.xInput1_Identification.TabIndex = 20;
            this.xInput1_Identification.Value = "";
            this.xInput1_Identification.ValueMember = "Value";
            // 
            // xInput_ClientSegment
            // 
            this.xInput_ClientSegment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xInput_ClientSegment.BackColor = System.Drawing.Color.Transparent;
            this.xInput_ClientSegment.ControlTypes = Finx.App.UserControls.ControlTypes.ComboBox;
            this.xInput_ClientSegment.ControlWidth = 175;
            this.xInput_ClientSegment.DataSource = null;
            this.xInput_ClientSegment.DisplayMember = "Text";
            this.xInput_ClientSegment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Document, ((byte)(0)));
            this.xInput_ClientSegment.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_ClientSegment.LableText = "Segment";
            this.xInput_ClientSegment.Location = new System.Drawing.Point(552, 26);
            this.xInput_ClientSegment.MappedField = "";
            this.xInput_ClientSegment.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_ClientSegment.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_ClientSegment.Model = null;
            this.xInput_ClientSegment.Name = "xInput_ClientSegment";
            this.xInput_ClientSegment.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_ClientSegment.ReadOnly = false;
            this.xInput_ClientSegment.Size = new System.Drawing.Size(289, 30);
            this.xInput_ClientSegment.TabIndex = 19;
            this.xInput_ClientSegment.Value = null;
            this.xInput_ClientSegment.ValueMember = "Value";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xInput_ShowCompletedTasks);
            this.panel2.Controls.Add(this.metroButton_Refresh);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(10, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1077, 26);
            this.panel2.TabIndex = 13;
            // 
            // xInput_ShowCompletedTasks
            // 
            this.xInput_ShowCompletedTasks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xInput_ShowCompletedTasks.BackColor = System.Drawing.Color.Transparent;
            this.xInput_ShowCompletedTasks.ControlTypes = Finx.App.UserControls.ControlTypes.CheckBox;
            this.xInput_ShowCompletedTasks.ControlWidth = 40;
            this.xInput_ShowCompletedTasks.DataSource = null;
            this.xInput_ShowCompletedTasks.DisplayMember = "Text";
            this.xInput_ShowCompletedTasks.Dock = System.Windows.Forms.DockStyle.Right;
            this.xInput_ShowCompletedTasks.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_ShowCompletedTasks.LableText = "Show Completed Tasks";
            this.xInput_ShowCompletedTasks.Location = new System.Drawing.Point(733, 0);
            this.xInput_ShowCompletedTasks.MappedField = null;
            this.xInput_ShowCompletedTasks.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_ShowCompletedTasks.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_ShowCompletedTasks.Model = null;
            this.xInput_ShowCompletedTasks.Name = "xInput_ShowCompletedTasks";
            this.xInput_ShowCompletedTasks.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_ShowCompletedTasks.ReadOnly = false;
            this.xInput_ShowCompletedTasks.Size = new System.Drawing.Size(231, 30);
            this.xInput_ShowCompletedTasks.TabIndex = 5;
            this.xInput_ShowCompletedTasks.Value = null;
            this.xInput_ShowCompletedTasks.ValueMember = "Value";
            // 
            // metroButton_Refresh
            // 
            this.metroButton_Refresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.metroButton_Refresh.Location = new System.Drawing.Point(964, 0);
            this.metroButton_Refresh.Name = "metroButton_Refresh";
            this.metroButton_Refresh.Size = new System.Drawing.Size(113, 26);
            this.metroButton_Refresh.TabIndex = 4;
            this.metroButton_Refresh.Text = "Refresh";
            this.metroButton_Refresh.UseSelectable = true;
            this.metroButton_Refresh.Click += new System.EventHandler(this.metroButton_Refresh_Click_1);
            // 
            // xInput_Surname
            // 
            this.xInput_Surname.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xInput_Surname.BackColor = System.Drawing.Color.Transparent;
            this.xInput_Surname.ControlTypes = Finx.App.UserControls.ControlTypes.TextBox;
            this.xInput_Surname.ControlWidth = 180;
            this.xInput_Surname.DataSource = null;
            this.xInput_Surname.DisplayMember = "Text";
            this.xInput_Surname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Document, ((byte)(0)));
            this.xInput_Surname.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_Surname.LableText = "Surname";
            this.xInput_Surname.Location = new System.Drawing.Point(10, 26);
            this.xInput_Surname.MappedField = "";
            this.xInput_Surname.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_Surname.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_Surname.Model = null;
            this.xInput_Surname.Name = "xInput_Surname";
            this.xInput_Surname.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_Surname.ReadOnly = false;
            this.xInput_Surname.Size = new System.Drawing.Size(320, 30);
            this.xInput_Surname.TabIndex = 9;
            this.xInput_Surname.Value = "";
            this.xInput_Surname.ValueMember = "Value";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panel3);
            this.splitContainer3.Panel1.Controls.Add(this.toolStripEx1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.panel4);
            this.splitContainer3.Panel2.Controls.Add(this.toolStripEx2);
            this.splitContainer3.Size = new System.Drawing.Size(1097, 316);
            this.splitContainer3.SplitterDistance = 548;
            this.splitContainer3.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGrid_ClientDetails);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(548, 277);
            this.panel3.TabIndex = 12;
            // 
            // dataGrid_ClientDetails
            // 
            this.dataGrid_ClientDetails.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_ClientDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_ClientDetails.EnableSort = false;
            this.dataGrid_ClientDetails.FixedRows = 1;
            this.dataGrid_ClientDetails.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_ClientDetails.Name = "dataGrid_ClientDetails";
            this.dataGrid_ClientDetails.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_ClientDetails.Size = new System.Drawing.Size(548, 277);
            this.dataGrid_ClientDetails.TabIndex = 1;
            this.dataGrid_ClientDetails.TabStop = true;
            this.dataGrid_ClientDetails.ToolTipText = "";
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.BackColor = System.Drawing.Color.White;
            this.toolStripEx1.ClickThrough = true;
            this.toolStripEx1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripEx1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.tsb_Download,
            this.toolStripSeparator2,
            this.tsb_Email,
            this.toolStripSeparator3,
            this.tsb_SMS,
            this.toolStripSeparator1,
            this.lblClientCaption});
            this.toolStripEx1.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripEx1.Size = new System.Drawing.Size(548, 39);
            this.toolStripEx1.TabIndex = 10;
            this.toolStripEx1.Text = "toolStripEx1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // tsb_Download
            // 
            this.tsb_Download.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsb_Download.Image = global::easiplan.app.Properties.Resources.notes_b_42;
            this.tsb_Download.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Download.Name = "tsb_Download";
            this.tsb_Download.Size = new System.Drawing.Size(97, 36);
            this.tsb_Download.Text = "Download";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tsb_Email
            // 
            this.tsb_Email.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsb_Email.Image = global::easiplan.app.Properties.Resources.mail_b_42;
            this.tsb_Email.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Email.Name = "tsb_Email";
            this.tsb_Email.Size = new System.Drawing.Size(72, 36);
            this.tsb_Email.Text = "Email";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tsb_SMS
            // 
            this.tsb_SMS.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsb_SMS.Image = global::easiplan.app.Properties.Resources.twitter_b_42;
            this.tsb_SMS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SMS.Name = "tsb_SMS";
            this.tsb_SMS.Size = new System.Drawing.Size(66, 36);
            this.tsb_SMS.Text = "SMS";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // lblClientCaption
            // 
            this.lblClientCaption.Name = "lblClientCaption";
            this.lblClientCaption.Size = new System.Drawing.Size(93, 36);
            this.lblClientCaption.Text = "lblClientCaption";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dataGrid_ClientInstructions);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 39);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(545, 277);
            this.panel4.TabIndex = 13;
            // 
            // dataGrid_ClientInstructions
            // 
            this.dataGrid_ClientInstructions.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_ClientInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_ClientInstructions.EnableSort = false;
            this.dataGrid_ClientInstructions.FixedRows = 1;
            this.dataGrid_ClientInstructions.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_ClientInstructions.Name = "dataGrid_ClientInstructions";
            this.dataGrid_ClientInstructions.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_ClientInstructions.Size = new System.Drawing.Size(545, 277);
            this.dataGrid_ClientInstructions.TabIndex = 0;
            this.dataGrid_ClientInstructions.TabStop = true;
            this.dataGrid_ClientInstructions.ToolTipText = "";
            // 
            // toolStripEx2
            // 
            this.toolStripEx2.BackColor = System.Drawing.Color.White;
            this.toolStripEx2.ClickThrough = true;
            this.toolStripEx2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripEx2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripEx2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel_Client,
            this.tsbNew,
            this.tsbClientRating});
            this.toolStripEx2.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx2.Name = "toolStripEx2";
            this.toolStripEx2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripEx2.Size = new System.Drawing.Size(545, 39);
            this.toolStripEx2.TabIndex = 11;
            this.toolStripEx2.Text = "toolStripEx2";
            // 
            // toolStripLabel_Client
            // 
            this.toolStripLabel_Client.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel_Client.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel_Client.Image = global::easiplan.app.Properties.Resources.person_b_42;
            this.toolStripLabel_Client.Name = "toolStripLabel_Client";
            this.toolStripLabel_Client.Size = new System.Drawing.Size(123, 36);
            this.toolStripLabel_Client.Text = "Client Name";
            // 
            // tsbNew
            // 
            this.tsbNew.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbNew.Image = global::easiplan.app.Properties.Resources.notes_32;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(112, 36);
            this.tsbNew.Text = "Custom Task";
            // 
            // tsbClientRating
            // 
            this.tsbClientRating.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClientRating.Image = global::easiplan.app.Properties.Resources.notes_32;
            this.tsbClientRating.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClientRating.Name = "tsbClientRating";
            this.tsbClientRating.Size = new System.Drawing.Size(100, 36);
            this.tsbClientRating.Text = "Rate Client";
            this.tsbClientRating.Click += new System.EventHandler(this.tsbClientRating_Click);
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
            this.miniToolStrip.Location = new System.Drawing.Point(317, 10);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(582, 39);
            this.miniToolStrip.TabIndex = 0;
            // 
            // frmClientManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 563);
            this.Controls.Add(this.splitContainer2);
            this.Name = "frmClientManagement";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.SubTitle = "";
            this.Text = "Client Management";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grbFilter.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.toolStripEx2.ResumeLayout(false);
            this.toolStripEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grbFilter;
        private System.Windows.Forms.Panel panel2;
        private UserControls.xInput xInput_Surname;
        private UserControls.ToolStripEx miniToolStrip;
        private MetroFramework.Controls.MetroButton metroButton_Refresh;
        private UserControls.xInput xInput_ClientSegment;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel3;
        private UserControls.ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripButton tsb_Download;
        private System.Windows.Forms.Panel panel4;
        private UserControls.ToolStripEx toolStripEx2;
        private UserControls.xInput xInput1_Identification;
        private UserControls.xInput xInput_BirthdayMonth;
        private System.Windows.Forms.ToolStripButton tsb_SMS;
        private System.Windows.Forms.ToolStripButton tsb_Email;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_Client;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private SourceGrid.DataGrid dataGrid_ClientInstructions;
        private SourceGrid.DataGrid dataGrid_ClientDetails;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private UserControls.xInput xInput_ShowCompletedTasks;
        private UserControls.xInput xInput_BirthdayDay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel lblClientCaption;
        private System.Windows.Forms.ToolStripButton tsbClientRating;
        private UserControls.xInput xInput_Advisor;
    }
}