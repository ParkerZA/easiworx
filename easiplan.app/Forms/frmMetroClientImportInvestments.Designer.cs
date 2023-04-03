
using System.Windows.Forms;

namespace Finx.App.Forms
{
    partial class frmMetroClientImportInvestments
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.kgbFileDetails = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvFileContents = new MetroFramework.Controls.MetroGrid();
            this.RowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Firstname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lastname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegistrationNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PassportNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LISP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PolicyNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FundName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FundValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FundValueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Premium = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountFundAllocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmClientRecords = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.openClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportErrorRecordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCellContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.chkViewErrorRecords = new System.Windows.Forms.CheckBox();
            this.chkViewExistingRecords = new System.Windows.Forms.CheckBox();
            this.chkViewNewRecords = new System.Windows.Forms.CheckBox();
            this.kbtnOpenFile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblImportStatus = new System.Windows.Forms.Label();
            this.lblTotValErrors = new System.Windows.Forms.Label();
            this.cmbSelectLisp = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.lblNewClientCnt = new System.Windows.Forms.Label();
            this.cancelImport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblExistingClientCnt = new System.Windows.Forms.Label();
            this.btnImportFile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.lblFileDate = new System.Windows.Forms.Label();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.lblFileFormat = new System.Windows.Forms.Label();
            this.lblRecCnt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kgbFileDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbFileDetails.Panel)).BeginInit();
            this.kgbFileDetails.Panel.SuspendLayout();
            this.kgbFileDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileContents)).BeginInit();
            this.cmClientRecords.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // kgbFileDetails
            // 
            this.kgbFileDetails.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.kgbFileDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kgbFileDetails.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonForm;
            this.kgbFileDetails.Location = new System.Drawing.Point(25, 75);
            this.kgbFileDetails.Margin = new System.Windows.Forms.Padding(4);
            this.kgbFileDetails.Name = "kgbFileDetails";
            // 
            // kgbFileDetails.Panel
            // 
            this.kgbFileDetails.Panel.Controls.Add(this.splitContainer1);
            this.kgbFileDetails.Panel.Controls.Add(this.metroPanel1);
            this.kgbFileDetails.Size = new System.Drawing.Size(1689, 672);
            this.kgbFileDetails.TabIndex = 11;
            this.kgbFileDetails.Values.Heading = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 117);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.panelGrid);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1685, 551);
            this.splitContainer1.SplitterDistance = 993;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 10;
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvFileContents);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 0);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(2);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(1685, 551);
            this.panelGrid.TabIndex = 12;
            // 
            // dgvFileContents
            // 
            this.dgvFileContents.AllowUserToAddRows = false;
            this.dgvFileContents.AllowUserToDeleteRows = false;
            this.dgvFileContents.AllowUserToOrderColumns = true;
            this.dgvFileContents.AllowUserToResizeRows = false;
            this.dgvFileContents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFileContents.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvFileContents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFileContents.CausesValidation = false;
            this.dgvFileContents.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvFileContents.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFileContents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFileContents.ColumnHeadersHeight = 29;
            this.dgvFileContents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNo,
            this.Title,
            this.Firstname,
            this.Lastname,
            this.IDNumber,
            this.RegistrationNo,
            this.PassportNo,
            this.ClientNo,
            this.BirthDate,
            this.LISP,
            this.PolicyNo,
            this.Product,
            this.ProductType,
            this.FundName,
            this.FundValue,
            this.FundValueDate,
            this.Premium,
            this.AccountFundAllocation});
            this.dgvFileContents.ContextMenuStrip = this.cmClientRecords;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFileContents.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFileContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileContents.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvFileContents.EnableHeadersVisualStyles = false;
            this.dgvFileContents.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dgvFileContents.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvFileContents.Location = new System.Drawing.Point(0, 0);
            this.dgvFileContents.Margin = new System.Windows.Forms.Padding(4, 15, 4, 4);
            this.dgvFileContents.MultiSelect = false;
            this.dgvFileContents.Name = "dgvFileContents";
            this.dgvFileContents.ReadOnly = true;
            this.dgvFileContents.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFileContents.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFileContents.RowHeadersWidth = 51;
            this.dgvFileContents.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvFileContents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFileContents.Size = new System.Drawing.Size(1685, 551);
            this.dgvFileContents.StandardTab = true;
            this.dgvFileContents.TabIndex = 6;
            this.dgvFileContents.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFileContents_CellContentClick);
            // 
            // RowNo
            // 
            this.RowNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.RowNo.DataPropertyName = "RowNo";
            this.RowNo.HeaderText = "Row No";
            this.RowNo.MinimumWidth = 6;
            this.RowNo.Name = "RowNo";
            this.RowNo.ReadOnly = true;
            this.RowNo.Width = 77;
            // 
            // Title
            // 
            this.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Title.DataPropertyName = "Title";
            this.Title.HeaderText = "Title";
            this.Title.MinimumWidth = 6;
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 57;
            // 
            // Firstname
            // 
            this.Firstname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Firstname.DataPropertyName = "Firstname";
            this.Firstname.HeaderText = "Firstname";
            this.Firstname.MinimumWidth = 6;
            this.Firstname.Name = "Firstname";
            this.Firstname.ReadOnly = true;
            this.Firstname.Width = 87;
            // 
            // Lastname
            // 
            this.Lastname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Lastname.DataPropertyName = "Lastname";
            this.Lastname.HeaderText = "Lastname";
            this.Lastname.MinimumWidth = 6;
            this.Lastname.Name = "Lastname";
            this.Lastname.ReadOnly = true;
            this.Lastname.Width = 86;
            // 
            // IDNumber
            // 
            this.IDNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.IDNumber.DataPropertyName = "IDNumber";
            this.IDNumber.HeaderText = "ID No";
            this.IDNumber.MinimumWidth = 6;
            this.IDNumber.Name = "IDNumber";
            this.IDNumber.ReadOnly = true;
            this.IDNumber.Width = 65;
            // 
            // RegistrationNo
            // 
            this.RegistrationNo.DataPropertyName = "RegistrationNo";
            this.RegistrationNo.HeaderText = "RegistrationNo";
            this.RegistrationNo.MinimumWidth = 6;
            this.RegistrationNo.Name = "RegistrationNo";
            this.RegistrationNo.ReadOnly = true;
            this.RegistrationNo.Width = 114;
            // 
            // PassportNo
            // 
            this.PassportNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PassportNo.DataPropertyName = "PassportNo";
            this.PassportNo.HeaderText = "Passport No";
            this.PassportNo.MinimumWidth = 6;
            this.PassportNo.Name = "PassportNo";
            this.PassportNo.ReadOnly = true;
            this.PassportNo.Width = 99;
            // 
            // ClientNo
            // 
            this.ClientNo.DataPropertyName = "ClientNo";
            this.ClientNo.HeaderText = "ClientNo";
            this.ClientNo.MinimumWidth = 6;
            this.ClientNo.Name = "ClientNo";
            this.ClientNo.ReadOnly = true;
            this.ClientNo.Width = 82;
            // 
            // BirthDate
            // 
            this.BirthDate.DataPropertyName = "DateOfBirth";
            this.BirthDate.HeaderText = "BirthDate";
            this.BirthDate.MinimumWidth = 6;
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.ReadOnly = true;
            this.BirthDate.Width = 84;
            // 
            // LISP
            // 
            this.LISP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.LISP.DataPropertyName = "LISP";
            this.LISP.HeaderText = "LISP";
            this.LISP.MinimumWidth = 6;
            this.LISP.Name = "LISP";
            this.LISP.ReadOnly = true;
            this.LISP.Width = 57;
            // 
            // PolicyNo
            // 
            this.PolicyNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PolicyNo.DataPropertyName = "AccountNo";
            this.PolicyNo.HeaderText = "Policy No";
            this.PolicyNo.MinimumWidth = 6;
            this.PolicyNo.Name = "PolicyNo";
            this.PolicyNo.ReadOnly = true;
            this.PolicyNo.Width = 86;
            // 
            // Product
            // 
            this.Product.DataPropertyName = "ProductName";
            this.Product.HeaderText = "Product";
            this.Product.MinimumWidth = 6;
            this.Product.Name = "Product";
            this.Product.ReadOnly = true;
            this.Product.Width = 77;
            // 
            // ProductType
            // 
            this.ProductType.DataPropertyName = "ProductType";
            this.ProductType.HeaderText = "ProductType";
            this.ProductType.MinimumWidth = 6;
            this.ProductType.Name = "ProductType";
            this.ProductType.ReadOnly = true;
            this.ProductType.Width = 101;
            // 
            // FundName
            // 
            this.FundName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FundName.DataPropertyName = "FundName";
            this.FundName.HeaderText = "Fund Name";
            this.FundName.MinimumWidth = 6;
            this.FundName.Name = "FundName";
            this.FundName.ReadOnly = true;
            this.FundName.Width = 97;
            // 
            // FundValue
            // 
            this.FundValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.FundValue.DataPropertyName = "FundValue";
            this.FundValue.HeaderText = "Fund Value (ZAR)";
            this.FundValue.MinimumWidth = 6;
            this.FundValue.Name = "FundValue";
            this.FundValue.ReadOnly = true;
            this.FundValue.Width = 126;
            // 
            // FundValueDate
            // 
            this.FundValueDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.FundValueDate.DataPropertyName = "FundValueDate";
            this.FundValueDate.HeaderText = "Fund Value Date";
            this.FundValueDate.MinimumWidth = 6;
            this.FundValueDate.Name = "FundValueDate";
            this.FundValueDate.ReadOnly = true;
            this.FundValueDate.Width = 120;
            // 
            // Premium
            // 
            this.Premium.DataPropertyName = "MonthlyPremium";
            this.Premium.HeaderText = "Premium (ZAR)";
            this.Premium.MinimumWidth = 6;
            this.Premium.Name = "Premium";
            this.Premium.ReadOnly = true;
            this.Premium.Width = 117;
            // 
            // AccountFundAllocation
            // 
            this.AccountFundAllocation.DataPropertyName = "AccountFundAllocation";
            this.AccountFundAllocation.HeaderText = "Percentage Split";
            this.AccountFundAllocation.MinimumWidth = 6;
            this.AccountFundAllocation.Name = "AccountFundAllocation";
            this.AccountFundAllocation.ReadOnly = true;
            this.AccountFundAllocation.Width = 120;
            // 
            // cmClientRecords
            // 
            this.cmClientRecords.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmClientRecords.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmClientRecords.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openClientToolStripMenuItem,
            this.exportErrorRecordsToolStripMenuItem,
            this.copyCellContentToolStripMenuItem});
            this.cmClientRecords.Name = "cmClientRecords";
            this.cmClientRecords.Size = new System.Drawing.Size(194, 76);
            this.cmClientRecords.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmClientRecords.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cmClientRecords.Opening += new System.ComponentModel.CancelEventHandler(this.cmClientRecords_Opening);
            // 
            // openClientToolStripMenuItem
            // 
            this.openClientToolStripMenuItem.Enabled = false;
            this.openClientToolStripMenuItem.Name = "openClientToolStripMenuItem";
            this.openClientToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.openClientToolStripMenuItem.Text = "Open Client";
            this.openClientToolStripMenuItem.Click += new System.EventHandler(this.openClientToolStripMenuItem_Click);
            // 
            // exportErrorRecordsToolStripMenuItem
            // 
            this.exportErrorRecordsToolStripMenuItem.Name = "exportErrorRecordsToolStripMenuItem";
            this.exportErrorRecordsToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.exportErrorRecordsToolStripMenuItem.Visible = false;
            // 
            // copyCellContentToolStripMenuItem
            // 
            this.copyCellContentToolStripMenuItem.Name = "copyCellContentToolStripMenuItem";
            this.copyCellContentToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.copyCellContentToolStripMenuItem.Text = "Copy cell content";
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.label2);
            this.metroPanel1.Controls.Add(this.chkViewErrorRecords);
            this.metroPanel1.Controls.Add(this.chkViewExistingRecords);
            this.metroPanel1.Controls.Add(this.chkViewNewRecords);
            this.metroPanel1.Controls.Add(this.kbtnOpenFile);
            this.metroPanel1.Controls.Add(this.lblImportStatus);
            this.metroPanel1.Controls.Add(this.lblTotValErrors);
            this.metroPanel1.Controls.Add(this.cmbSelectLisp);
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.Controls.Add(this.lblNewClientCnt);
            this.metroPanel1.Controls.Add(this.cancelImport);
            this.metroPanel1.Controls.Add(this.lblExistingClientCnt);
            this.metroPanel1.Controls.Add(this.btnImportFile);
            this.metroPanel1.Controls.Add(this.lblSelectedFile);
            this.metroPanel1.Controls.Add(this.lblFileDate);
            this.metroPanel1.Controls.Add(this.lblFileSize);
            this.metroPanel1.Controls.Add(this.lblFileFormat);
            this.metroPanel1.Controls.Add(this.lblRecCnt);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 12;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1685, 117);
            this.metroPanel1.TabIndex = 4;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(411, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 45;
            this.label2.Text = "Records :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // chkViewErrorRecords
            // 
            this.chkViewErrorRecords.AutoSize = true;
            this.chkViewErrorRecords.Enabled = false;
            this.chkViewErrorRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkViewErrorRecords.Location = new System.Drawing.Point(566, 94);
            this.chkViewErrorRecords.Margin = new System.Windows.Forms.Padding(2);
            this.chkViewErrorRecords.Name = "chkViewErrorRecords";
            this.chkViewErrorRecords.Size = new System.Drawing.Size(145, 20);
            this.chkViewErrorRecords.TabIndex = 21;
            this.chkViewErrorRecords.Text = "View Error Records";
            this.chkViewErrorRecords.UseVisualStyleBackColor = true;
            this.chkViewErrorRecords.CheckedChanged += new System.EventHandler(this.chkViewErrorRecords_CheckedChanged);
            // 
            // chkViewExistingRecords
            // 
            this.chkViewExistingRecords.AutoSize = true;
            this.chkViewExistingRecords.Enabled = false;
            this.chkViewExistingRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkViewExistingRecords.Location = new System.Drawing.Point(566, 68);
            this.chkViewExistingRecords.Margin = new System.Windows.Forms.Padding(2);
            this.chkViewExistingRecords.Name = "chkViewExistingRecords";
            this.chkViewExistingRecords.Size = new System.Drawing.Size(162, 20);
            this.chkViewExistingRecords.TabIndex = 20;
            this.chkViewExistingRecords.Text = "View Existing Records";
            this.chkViewExistingRecords.UseVisualStyleBackColor = true;
            this.chkViewExistingRecords.CheckedChanged += new System.EventHandler(this.chkViewExistingRecords_CheckedChanged);
            // 
            // chkViewNewRecords
            // 
            this.chkViewNewRecords.AutoSize = true;
            this.chkViewNewRecords.Enabled = false;
            this.chkViewNewRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkViewNewRecords.Location = new System.Drawing.Point(566, 41);
            this.chkViewNewRecords.Margin = new System.Windows.Forms.Padding(2);
            this.chkViewNewRecords.Name = "chkViewNewRecords";
            this.chkViewNewRecords.Size = new System.Drawing.Size(143, 20);
            this.chkViewNewRecords.TabIndex = 19;
            this.chkViewNewRecords.Text = "View New Records";
            this.chkViewNewRecords.UseVisualStyleBackColor = true;
            this.chkViewNewRecords.CheckedChanged += new System.EventHandler(this.chkViewNewRecords_CheckedChanged);
            // 
            // kbtnOpenFile
            // 
            this.kbtnOpenFile.Location = new System.Drawing.Point(236, 12);
            this.kbtnOpenFile.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnOpenFile.Name = "kbtnOpenFile";
            this.kbtnOpenFile.Size = new System.Drawing.Size(156, 39);
            this.kbtnOpenFile.TabIndex = 2;
            this.kbtnOpenFile.Values.Text = "Select &File";
            this.kbtnOpenFile.Click += new System.EventHandler(this.kbtnOpenFile_Click);
            // 
            // lblImportStatus
            // 
            this.lblImportStatus.AutoSize = true;
            this.lblImportStatus.Location = new System.Drawing.Point(799, 42);
            this.lblImportStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImportStatus.Name = "lblImportStatus";
            this.lblImportStatus.Size = new System.Drawing.Size(57, 16);
            this.lblImportStatus.TabIndex = 43;
            this.lblImportStatus.Text = "Pending";
            // 
            // lblTotValErrors
            // 
            this.lblTotValErrors.AutoSize = true;
            this.lblTotValErrors.Location = new System.Drawing.Point(736, 94);
            this.lblTotValErrors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotValErrors.Name = "lblTotValErrors";
            this.lblTotValErrors.Size = new System.Drawing.Size(14, 16);
            this.lblTotValErrors.TabIndex = 40;
            this.lblTotValErrors.Text = "0";
            // 
            // cmbSelectLisp
            // 
            this.cmbSelectLisp.FormattingEnabled = true;
            this.cmbSelectLisp.ItemHeight = 24;
            this.cmbSelectLisp.Items.AddRange(new object[] {
            "Please select",
            "AstuteTemplate",
            "AllanGray",
            "Camissa",
            "EasiworxTemplate",
            "Momentum"});
            this.cmbSelectLisp.Location = new System.Drawing.Point(55, 12);
            this.cmbSelectLisp.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSelectLisp.Name = "cmbSelectLisp";
            this.cmbSelectLisp.Size = new System.Drawing.Size(173, 30);
            this.cmbSelectLisp.TabIndex = 1;
            this.cmbSelectLisp.UseSelectable = true;
            this.cmbSelectLisp.SelectedIndexChanged += new System.EventHandler(this.cmbSelectLisp_SelectedIndexChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(5, 15);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(36, 20);
            this.metroLabel1.TabIndex = 17;
            this.metroLabel1.Text = "Lisp:";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNewClientCnt
            // 
            this.lblNewClientCnt.AutoSize = true;
            this.lblNewClientCnt.Location = new System.Drawing.Point(736, 46);
            this.lblNewClientCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNewClientCnt.Name = "lblNewClientCnt";
            this.lblNewClientCnt.Size = new System.Drawing.Size(14, 16);
            this.lblNewClientCnt.TabIndex = 38;
            this.lblNewClientCnt.Text = "0";
            this.lblNewClientCnt.Click += new System.EventHandler(this.lblNewClientCnt_Click);
            // 
            // cancelImport
            // 
            this.cancelImport.Enabled = false;
            this.cancelImport.Location = new System.Drawing.Point(232, 68);
            this.cancelImport.Margin = new System.Windows.Forms.Padding(4);
            this.cancelImport.Name = "cancelImport";
            this.cancelImport.Size = new System.Drawing.Size(159, 36);
            this.cancelImport.TabIndex = 4;
            this.cancelImport.Values.Text = "&Cancel Import";
            // 
            // lblExistingClientCnt
            // 
            this.lblExistingClientCnt.AutoSize = true;
            this.lblExistingClientCnt.Location = new System.Drawing.Point(736, 69);
            this.lblExistingClientCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExistingClientCnt.Name = "lblExistingClientCnt";
            this.lblExistingClientCnt.Size = new System.Drawing.Size(14, 16);
            this.lblExistingClientCnt.TabIndex = 37;
            this.lblExistingClientCnt.Text = "0";
            this.lblExistingClientCnt.Click += new System.EventHandler(this.lblExistingClientCnt_Click);
            // 
            // btnImportFile
            // 
            this.btnImportFile.Enabled = false;
            this.btnImportFile.Location = new System.Drawing.Point(56, 68);
            this.btnImportFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.Size = new System.Drawing.Size(159, 36);
            this.btnImportFile.TabIndex = 3;
            this.btnImportFile.Values.Text = "&Import File";
            this.btnImportFile.Click += new System.EventHandler(this.btnImportFile_Click);
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedFile.Location = new System.Drawing.Point(411, 12);
            this.lblSelectedFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(85, 20);
            this.lblSelectedFile.TabIndex = 23;
            this.lblSelectedFile.Text = "Filename";
            this.lblSelectedFile.Click += new System.EventHandler(this.lblSelectedFile_Click);
            // 
            // lblFileDate
            // 
            this.lblFileDate.AutoSize = true;
            this.lblFileDate.Location = new System.Drawing.Point(411, 46);
            this.lblFileDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileDate.Name = "lblFileDate";
            this.lblFileDate.Size = new System.Drawing.Size(115, 16);
            this.lblFileDate.TabIndex = 30;
            this.lblFileDate.Text = "15 May 2022 06:00";
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(411, 69);
            this.lblFileSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(34, 16);
            this.lblFileSize.TabIndex = 32;
            this.lblFileSize.Text = "2MB";
            // 
            // lblFileFormat
            // 
            this.lblFileFormat.AutoSize = true;
            this.lblFileFormat.Location = new System.Drawing.Point(799, 68);
            this.lblFileFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileFormat.Name = "lblFileFormat";
            this.lblFileFormat.Size = new System.Drawing.Size(30, 16);
            this.lblFileFormat.TabIndex = 34;
            this.lblFileFormat.Text = "Csv";
            // 
            // lblRecCnt
            // 
            this.lblRecCnt.AutoSize = true;
            this.lblRecCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecCnt.Location = new System.Drawing.Point(484, 95);
            this.lblRecCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecCnt.Name = "lblRecCnt";
            this.lblRecCnt.Size = new System.Drawing.Size(17, 17);
            this.lblRecCnt.TabIndex = 28;
            this.lblRecCnt.Text = "0";
            // 
            // frmMetroClientImportInvestments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1739, 772);
            this.Controls.Add(this.kgbFileDetails);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMetroClientImportInvestments";
            this.Padding = new System.Windows.Forms.Padding(25, 75, 25, 25);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Silver;
            this.Text = "Import Client Investments";
            ((System.ComponentModel.ISupportInitialize)(this.kgbFileDetails.Panel)).EndInit();
            this.kgbFileDetails.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgbFileDetails)).EndInit();
            this.kgbFileDetails.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileContents)).EndInit();
            this.cmClientRecords.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }





        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kgbFileDetails;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroContextMenu cmClientRecords;
        private System.Windows.Forms.ToolStripMenuItem openClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportErrorRecordsToolStripMenuItem;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox cmbSelectLisp;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kbtnOpenFile;
        private System.Windows.Forms.ToolStripMenuItem copyCellContentToolStripMenuItem;
        private SplitContainer splitContainer1;
        private Panel panelGrid;
        private MetroFramework.Controls.MetroGrid dgvFileContents;
        private Label lblImportStatus;
        private ComponentFactory.Krypton.Toolkit.KryptonButton cancelImport;
        private Label lblTotValErrors;
        private Label lblNewClientCnt;
        private Label lblExistingClientCnt;
        private Label lblFileFormat;
        private Label lblFileSize;
        private Label lblFileDate;
        private Label lblRecCnt;
        private Label lblSelectedFile;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnImportFile;
        private CheckBox chkViewErrorRecords;
        private CheckBox chkViewExistingRecords;
        private CheckBox chkViewNewRecords;
        private DataGridViewTextBoxColumn RowNo;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn Firstname;
        private DataGridViewTextBoxColumn Lastname;
        private DataGridViewTextBoxColumn IDNumber;
        private DataGridViewTextBoxColumn RegistrationNo;
        private DataGridViewTextBoxColumn PassportNo;
        private DataGridViewTextBoxColumn ClientNo;
        private DataGridViewTextBoxColumn BirthDate;
        private DataGridViewTextBoxColumn LISP;
        private DataGridViewTextBoxColumn PolicyNo;
        private DataGridViewTextBoxColumn Product;
        private DataGridViewTextBoxColumn ProductType;
        private DataGridViewTextBoxColumn FundName;
        private DataGridViewTextBoxColumn FundValue;
        private DataGridViewTextBoxColumn FundValueDate;
        private DataGridViewTextBoxColumn Premium;
        private DataGridViewTextBoxColumn AccountFundAllocation;
        private Label label2;
    }
}