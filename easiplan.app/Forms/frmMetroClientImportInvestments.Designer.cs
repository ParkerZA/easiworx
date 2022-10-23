
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
            this.cmClientRecords = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.openClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportErrorRecordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCellContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelFileInfo = new MetroFramework.Controls.MetroPanel();
            this.lblLastImportDate = new System.Windows.Forms.Label();
            this.lblLastImportUser = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblImportStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cancelImport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblTotValErrors = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNewClientCnt = new System.Windows.Forms.Label();
            this.lblExistingClientCnt = new System.Windows.Forms.Label();
            this.lblNewClientCnt1 = new System.Windows.Forms.Label();
            this.lblExistingClientCnt1 = new System.Windows.Forms.Label();
            this.lblFileFormat = new System.Windows.Forms.Label();
            this.lblFileFormat1 = new System.Windows.Forms.Label();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.lblFileSize1 = new System.Windows.Forms.Label();
            this.lblFileDate = new System.Windows.Forms.Label();
            this.lblFileDate1 = new System.Windows.Forms.Label();
            this.lblRecCnt = new System.Windows.Forms.Label();
            this.lblRecCnt1 = new System.Windows.Forms.Label();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.lblSelectedFile1 = new System.Windows.Forms.Label();
            this.btnImportFile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.chkViewErrorRecords = new System.Windows.Forms.CheckBox();
            this.chkViewExistingRecords = new System.Windows.Forms.CheckBox();
            this.chkViewNewRecords = new System.Windows.Forms.CheckBox();
            this.kbtnOpenFile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cmbSelectLisp = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kgbFileDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbFileDetails.Panel)).BeginInit();
            this.kgbFileDetails.Panel.SuspendLayout();
            this.kgbFileDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileContents)).BeginInit();
            this.cmClientRecords.SuspendLayout();
            this.panelFileInfo.SuspendLayout();
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
            this.kgbFileDetails.Size = new System.Drawing.Size(1484, 732);
            this.kgbFileDetails.TabIndex = 11;
            this.kgbFileDetails.Values.Heading = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 64);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.panelGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelFileInfo);
            this.splitContainer1.Size = new System.Drawing.Size(1480, 664);
            this.splitContainer1.SplitterDistance = 1189;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 10;
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvFileContents);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 0);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(1189, 664);
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
            this.Premium});
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
            this.dgvFileContents.Margin = new System.Windows.Forms.Padding(4);
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
            this.dgvFileContents.Size = new System.Drawing.Size(1189, 664);
            this.dgvFileContents.StandardTab = true;
            this.dgvFileContents.TabIndex = 6;
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
            this.Product.DataPropertyName = "Product";
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
            // panelFileInfo
            // 
            this.panelFileInfo.AutoScroll = true;
            this.panelFileInfo.AutoSize = true;
            this.panelFileInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelFileInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFileInfo.Controls.Add(this.lblLastImportDate);
            this.panelFileInfo.Controls.Add(this.lblLastImportUser);
            this.panelFileInfo.Controls.Add(this.label8);
            this.panelFileInfo.Controls.Add(this.label7);
            this.panelFileInfo.Controls.Add(this.lblImportStatus);
            this.panelFileInfo.Controls.Add(this.label6);
            this.panelFileInfo.Controls.Add(this.cancelImport);
            this.panelFileInfo.Controls.Add(this.lblTotValErrors);
            this.panelFileInfo.Controls.Add(this.label2);
            this.panelFileInfo.Controls.Add(this.lblNewClientCnt);
            this.panelFileInfo.Controls.Add(this.lblExistingClientCnt);
            this.panelFileInfo.Controls.Add(this.lblNewClientCnt1);
            this.panelFileInfo.Controls.Add(this.lblExistingClientCnt1);
            this.panelFileInfo.Controls.Add(this.lblFileFormat);
            this.panelFileInfo.Controls.Add(this.lblFileFormat1);
            this.panelFileInfo.Controls.Add(this.lblFileSize);
            this.panelFileInfo.Controls.Add(this.lblFileSize1);
            this.panelFileInfo.Controls.Add(this.lblFileDate);
            this.panelFileInfo.Controls.Add(this.lblFileDate1);
            this.panelFileInfo.Controls.Add(this.lblRecCnt);
            this.panelFileInfo.Controls.Add(this.lblRecCnt1);
            this.panelFileInfo.Controls.Add(this.lblSelectedFile);
            this.panelFileInfo.Controls.Add(this.lblSelectedFile1);
            this.panelFileInfo.Controls.Add(this.btnImportFile);
            this.panelFileInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFileInfo.HorizontalScrollbar = true;
            this.panelFileInfo.HorizontalScrollbarBarColor = true;
            this.panelFileInfo.HorizontalScrollbarHighlightOnWheel = false;
            this.panelFileInfo.HorizontalScrollbarSize = 12;
            this.panelFileInfo.Location = new System.Drawing.Point(0, 0);
            this.panelFileInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelFileInfo.Name = "panelFileInfo";
            this.panelFileInfo.Size = new System.Drawing.Size(346, 664);
            this.panelFileInfo.TabIndex = 15;
            this.panelFileInfo.VerticalScrollbar = true;
            this.panelFileInfo.VerticalScrollbarBarColor = true;
            this.panelFileInfo.VerticalScrollbarHighlightOnWheel = false;
            this.panelFileInfo.VerticalScrollbarSize = 12;
            // 
            // lblLastImportDate
            // 
            this.lblLastImportDate.AutoSize = true;
            this.lblLastImportDate.Location = new System.Drawing.Point(146, 190);
            this.lblLastImportDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastImportDate.Name = "lblLastImportDate";
            this.lblLastImportDate.Size = new System.Drawing.Size(0, 16);
            this.lblLastImportDate.TabIndex = 45;
            // 
            // lblLastImportUser
            // 
            this.lblLastImportUser.AutoSize = true;
            this.lblLastImportUser.Location = new System.Drawing.Point(139, 216);
            this.lblLastImportUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastImportUser.Name = "lblLastImportUser";
            this.lblLastImportUser.Size = new System.Drawing.Size(0, 16);
            this.lblLastImportUser.TabIndex = 47;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 215);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 17);
            this.label8.TabIndex = 46;
            this.label8.Text = "Last Import User:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 189);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 17);
            this.label7.TabIndex = 44;
            this.label7.Text = "Last Import Date:";
            // 
            // lblImportStatus
            // 
            this.lblImportStatus.AutoSize = true;
            this.lblImportStatus.Location = new System.Drawing.Point(104, 162);
            this.lblImportStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImportStatus.Name = "lblImportStatus";
            this.lblImportStatus.Size = new System.Drawing.Size(57, 16);
            this.lblImportStatus.TabIndex = 43;
            this.lblImportStatus.Text = "Pending";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 162);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 17);
            this.label6.TabIndex = 42;
            this.label6.Text = "Import Status:";
            // 
            // cancelImport
            // 
            this.cancelImport.Enabled = false;
            this.cancelImport.Location = new System.Drawing.Point(181, 19);
            this.cancelImport.Margin = new System.Windows.Forms.Padding(4);
            this.cancelImport.Name = "cancelImport";
            this.cancelImport.Size = new System.Drawing.Size(159, 36);
            this.cancelImport.TabIndex = 4;
            this.cancelImport.Values.Text = "&Cancel Import";
            // 
            // lblTotValErrors
            // 
            this.lblTotValErrors.AutoSize = true;
            this.lblTotValErrors.Location = new System.Drawing.Point(155, 328);
            this.lblTotValErrors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotValErrors.Name = "lblTotValErrors";
            this.lblTotValErrors.Size = new System.Drawing.Size(14, 16);
            this.lblTotValErrors.TabIndex = 40;
            this.lblTotValErrors.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightPink;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 327);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 17);
            this.label2.TabIndex = 39;
            this.label2.Text = "Tot Validation Errors:";
            // 
            // lblNewClientCnt
            // 
            this.lblNewClientCnt.AutoSize = true;
            this.lblNewClientCnt.Location = new System.Drawing.Point(155, 300);
            this.lblNewClientCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNewClientCnt.Name = "lblNewClientCnt";
            this.lblNewClientCnt.Size = new System.Drawing.Size(14, 16);
            this.lblNewClientCnt.TabIndex = 38;
            this.lblNewClientCnt.Text = "0";
            // 
            // lblExistingClientCnt
            // 
            this.lblExistingClientCnt.AutoSize = true;
            this.lblExistingClientCnt.Location = new System.Drawing.Point(210, 275);
            this.lblExistingClientCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExistingClientCnt.Name = "lblExistingClientCnt";
            this.lblExistingClientCnt.Size = new System.Drawing.Size(14, 16);
            this.lblExistingClientCnt.TabIndex = 37;
            this.lblExistingClientCnt.Text = "0";
            // 
            // lblNewClientCnt1
            // 
            this.lblNewClientCnt1.AutoSize = true;
            this.lblNewClientCnt1.BackColor = System.Drawing.Color.LightGreen;
            this.lblNewClientCnt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewClientCnt1.Location = new System.Drawing.Point(9, 300);
            this.lblNewClientCnt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNewClientCnt1.Name = "lblNewClientCnt1";
            this.lblNewClientCnt1.Size = new System.Drawing.Size(135, 17);
            this.lblNewClientCnt1.TabIndex = 36;
            this.lblNewClientCnt1.Text = "New Client Records:";
            // 
            // lblExistingClientCnt1
            // 
            this.lblExistingClientCnt1.AutoSize = true;
            this.lblExistingClientCnt1.BackColor = System.Drawing.Color.LightBlue;
            this.lblExistingClientCnt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExistingClientCnt1.Location = new System.Drawing.Point(9, 275);
            this.lblExistingClientCnt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExistingClientCnt1.Name = "lblExistingClientCnt1";
            this.lblExistingClientCnt1.Size = new System.Drawing.Size(190, 17);
            this.lblExistingClientCnt1.TabIndex = 35;
            this.lblExistingClientCnt1.Text = "Existing Client Record Count:";
            // 
            // lblFileFormat
            // 
            this.lblFileFormat.AutoSize = true;
            this.lblFileFormat.Location = new System.Drawing.Point(94, 137);
            this.lblFileFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileFormat.Name = "lblFileFormat";
            this.lblFileFormat.Size = new System.Drawing.Size(30, 16);
            this.lblFileFormat.TabIndex = 34;
            this.lblFileFormat.Text = "Csv";
            // 
            // lblFileFormat1
            // 
            this.lblFileFormat1.AutoSize = true;
            this.lblFileFormat1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileFormat1.Location = new System.Drawing.Point(9, 137);
            this.lblFileFormat1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileFormat1.Name = "lblFileFormat1";
            this.lblFileFormat1.Size = new System.Drawing.Size(82, 17);
            this.lblFileFormat1.TabIndex = 33;
            this.lblFileFormat1.Text = "File Format:";
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(75, 113);
            this.lblFileSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(34, 16);
            this.lblFileSize.TabIndex = 32;
            this.lblFileSize.Text = "2MB";
            // 
            // lblFileSize1
            // 
            this.lblFileSize1.AutoSize = true;
            this.lblFileSize1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileSize1.Location = new System.Drawing.Point(9, 113);
            this.lblFileSize1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileSize1.Name = "lblFileSize1";
            this.lblFileSize1.Size = new System.Drawing.Size(65, 17);
            this.lblFileSize1.TabIndex = 31;
            this.lblFileSize1.Text = "File Size:";
            // 
            // lblFileDate
            // 
            this.lblFileDate.AutoSize = true;
            this.lblFileDate.Location = new System.Drawing.Point(74, 88);
            this.lblFileDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileDate.Name = "lblFileDate";
            this.lblFileDate.Size = new System.Drawing.Size(115, 16);
            this.lblFileDate.TabIndex = 30;
            this.lblFileDate.Text = "15 May 2022 06:00";
            // 
            // lblFileDate1
            // 
            this.lblFileDate1.AutoSize = true;
            this.lblFileDate1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileDate1.Location = new System.Drawing.Point(9, 88);
            this.lblFileDate1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileDate1.Name = "lblFileDate1";
            this.lblFileDate1.Size = new System.Drawing.Size(68, 17);
            this.lblFileDate1.TabIndex = 29;
            this.lblFileDate1.Text = "File Date:";
            // 
            // lblRecCnt
            // 
            this.lblRecCnt.AutoSize = true;
            this.lblRecCnt.Location = new System.Drawing.Point(109, 250);
            this.lblRecCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecCnt.Name = "lblRecCnt";
            this.lblRecCnt.Size = new System.Drawing.Size(14, 16);
            this.lblRecCnt.TabIndex = 28;
            this.lblRecCnt.Text = "0";
            // 
            // lblRecCnt1
            // 
            this.lblRecCnt1.AutoSize = true;
            this.lblRecCnt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecCnt1.Location = new System.Drawing.Point(9, 250);
            this.lblRecCnt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecCnt1.Name = "lblRecCnt1";
            this.lblRecCnt1.Size = new System.Drawing.Size(99, 17);
            this.lblRecCnt1.TabIndex = 27;
            this.lblRecCnt1.Text = "Record Count:";
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.Location = new System.Drawing.Point(83, 63);
            this.lblSelectedFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(63, 16);
            this.lblSelectedFile.TabIndex = 23;
            this.lblSelectedFile.Text = "Filename";
            // 
            // lblSelectedFile1
            // 
            this.lblSelectedFile1.AutoSize = true;
            this.lblSelectedFile1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedFile1.Location = new System.Drawing.Point(9, 63);
            this.lblSelectedFile1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedFile1.Name = "lblSelectedFile1";
            this.lblSelectedFile1.Size = new System.Drawing.Size(75, 17);
            this.lblSelectedFile1.TabIndex = 20;
            this.lblSelectedFile1.Text = "File Name:";
            // 
            // btnImportFile
            // 
            this.btnImportFile.Enabled = false;
            this.btnImportFile.Location = new System.Drawing.Point(12, 19);
            this.btnImportFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.Size = new System.Drawing.Size(159, 36);
            this.btnImportFile.TabIndex = 3;
            this.btnImportFile.Values.Text = "&Import File";
            this.btnImportFile.Click += new System.EventHandler(this.btnImportFile_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.chkViewErrorRecords);
            this.metroPanel1.Controls.Add(this.chkViewExistingRecords);
            this.metroPanel1.Controls.Add(this.chkViewNewRecords);
            this.metroPanel1.Controls.Add(this.kbtnOpenFile);
            this.metroPanel1.Controls.Add(this.cmbSelectLisp);
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 12;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1480, 64);
            this.metroPanel1.TabIndex = 4;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 12;
            // 
            // chkViewErrorRecords
            // 
            this.chkViewErrorRecords.AutoSize = true;
            this.chkViewErrorRecords.Enabled = false;
            this.chkViewErrorRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkViewErrorRecords.Location = new System.Drawing.Point(773, 24);
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
            this.chkViewExistingRecords.Location = new System.Drawing.Point(589, 24);
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
            this.chkViewNewRecords.Location = new System.Drawing.Point(415, 24);
            this.chkViewNewRecords.Name = "chkViewNewRecords";
            this.chkViewNewRecords.Size = new System.Drawing.Size(143, 20);
            this.chkViewNewRecords.TabIndex = 19;
            this.chkViewNewRecords.Text = "View New Records";
            this.chkViewNewRecords.UseVisualStyleBackColor = true;
            this.chkViewNewRecords.CheckedChanged += new System.EventHandler(this.chkViewNewRecords_CheckedChanged);
            // 
            // kbtnOpenFile
            // 
            this.kbtnOpenFile.Location = new System.Drawing.Point(225, 13);
            this.kbtnOpenFile.Margin = new System.Windows.Forms.Padding(4);
            this.kbtnOpenFile.Name = "kbtnOpenFile";
            this.kbtnOpenFile.Size = new System.Drawing.Size(156, 39);
            this.kbtnOpenFile.TabIndex = 2;
            this.kbtnOpenFile.Values.Text = "Select &File";
            this.kbtnOpenFile.Click += new System.EventHandler(this.kbtnOpenFile_Click);
            // 
            // cmbSelectLisp
            // 
            this.cmbSelectLisp.FormattingEnabled = true;
            this.cmbSelectLisp.ItemHeight = 24;
            this.cmbSelectLisp.Items.AddRange(new object[] {
            "Please select",
            "AllanGray",
            "Camissa",
            "Easiworx",
            "Momentum"});
            this.cmbSelectLisp.Location = new System.Drawing.Point(44, 14);
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
            this.metroLabel1.Location = new System.Drawing.Point(0, 15);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(36, 20);
            this.metroLabel1.TabIndex = 17;
            this.metroLabel1.Text = "Lisp:";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMetroClientImportInvestments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1534, 832);
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
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileContents)).EndInit();
            this.cmClientRecords.ResumeLayout(false);
            this.panelFileInfo.ResumeLayout(false);
            this.panelFileInfo.PerformLayout();
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
        private MetroFramework.Controls.MetroPanel panelFileInfo;
        private Label lblLastImportUser;
        private Label label8;
        private Label lblLastImportDate;
        private Label label7;
        private Label lblImportStatus;
        private Label label6;
        private ComponentFactory.Krypton.Toolkit.KryptonButton cancelImport;
        private Label lblTotValErrors;
        private Label label2;
        private Label lblNewClientCnt;
        private Label lblExistingClientCnt;
        private Label lblNewClientCnt1;
        private Label lblExistingClientCnt1;
        private Label lblFileFormat;
        private Label lblFileFormat1;
        private Label lblFileSize;
        private Label lblFileSize1;
        private Label lblFileDate;
        private Label lblFileDate1;
        private Label lblRecCnt;
        private Label lblRecCnt1;
        private Label lblSelectedFile;
        private Label lblSelectedFile1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnImportFile;
        private CheckBox chkViewErrorRecords;
        private CheckBox chkViewExistingRecords;
        private CheckBox chkViewNewRecords;
    }
}