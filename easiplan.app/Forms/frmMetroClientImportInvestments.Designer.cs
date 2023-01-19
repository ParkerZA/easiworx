
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.kgbFileDetails.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlGroupBox;
            this.kgbFileDetails.Location = new System.Drawing.Point(20, 60);
            this.kgbFileDetails.Name = "kgbFileDetails";
            // 
            // kgbFileDetails.Panel
            // 
            this.kgbFileDetails.Panel.Controls.Add(this.splitContainer1);
            this.kgbFileDetails.Panel.Controls.Add(this.metroPanel1);
            this.kgbFileDetails.Size = new System.Drawing.Size(1351, 646);
            this.kgbFileDetails.TabIndex = 11;
            this.kgbFileDetails.Values.Heading = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
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
            this.splitContainer1.Size = new System.Drawing.Size(1347, 591);
            this.splitContainer1.SplitterDistance = 1081;
            this.splitContainer1.TabIndex = 10;
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvFileContents);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 0);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(2);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(1081, 591);
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFileContents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFileContents.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvFileContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileContents.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvFileContents.EnableHeadersVisualStyles = false;
            this.dgvFileContents.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dgvFileContents.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvFileContents.Location = new System.Drawing.Point(0, 0);
            this.dgvFileContents.MultiSelect = false;
            this.dgvFileContents.Name = "dgvFileContents";
            this.dgvFileContents.ReadOnly = true;
            this.dgvFileContents.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFileContents.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvFileContents.RowHeadersWidth = 51;
            this.dgvFileContents.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvFileContents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFileContents.Size = new System.Drawing.Size(1081, 591);
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
            this.RowNo.Width = 72;
            // 
            // Title
            // 
            this.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Title.DataPropertyName = "Title";
            this.Title.HeaderText = "Title";
            this.Title.MinimumWidth = 6;
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 53;
            // 
            // Firstname
            // 
            this.Firstname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Firstname.DataPropertyName = "Firstname";
            this.Firstname.HeaderText = "Firstname";
            this.Firstname.MinimumWidth = 6;
            this.Firstname.Name = "Firstname";
            this.Firstname.ReadOnly = true;
            this.Firstname.Width = 81;
            // 
            // Lastname
            // 
            this.Lastname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Lastname.DataPropertyName = "Lastname";
            this.Lastname.HeaderText = "Lastname";
            this.Lastname.MinimumWidth = 6;
            this.Lastname.Name = "Lastname";
            this.Lastname.ReadOnly = true;
            this.Lastname.Width = 79;
            // 
            // IDNumber
            // 
            this.IDNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.IDNumber.DataPropertyName = "IDNumber";
            this.IDNumber.HeaderText = "ID No";
            this.IDNumber.MinimumWidth = 6;
            this.IDNumber.Name = "IDNumber";
            this.IDNumber.ReadOnly = true;
            this.IDNumber.Width = 60;
            // 
            // RegistrationNo
            // 
            this.RegistrationNo.DataPropertyName = "RegistrationNo";
            this.RegistrationNo.HeaderText = "RegistrationNo";
            this.RegistrationNo.MinimumWidth = 6;
            this.RegistrationNo.Name = "RegistrationNo";
            this.RegistrationNo.ReadOnly = true;
            this.RegistrationNo.Width = 109;
            // 
            // PassportNo
            // 
            this.PassportNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PassportNo.DataPropertyName = "PassportNo";
            this.PassportNo.HeaderText = "Passport No";
            this.PassportNo.MinimumWidth = 6;
            this.PassportNo.Name = "PassportNo";
            this.PassportNo.ReadOnly = true;
            this.PassportNo.Width = 93;
            // 
            // ClientNo
            // 
            this.ClientNo.DataPropertyName = "ClientNo";
            this.ClientNo.HeaderText = "ClientNo";
            this.ClientNo.MinimumWidth = 6;
            this.ClientNo.Name = "ClientNo";
            this.ClientNo.ReadOnly = true;
            this.ClientNo.Width = 76;
            // 
            // BirthDate
            // 
            this.BirthDate.DataPropertyName = "DateOfBirth";
            this.BirthDate.HeaderText = "BirthDate";
            this.BirthDate.MinimumWidth = 6;
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.ReadOnly = true;
            this.BirthDate.Width = 79;
            // 
            // LISP
            // 
            this.LISP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.LISP.DataPropertyName = "LISP";
            this.LISP.HeaderText = "LISP";
            this.LISP.MinimumWidth = 6;
            this.LISP.Name = "LISP";
            this.LISP.ReadOnly = true;
            this.LISP.Width = 51;
            // 
            // PolicyNo
            // 
            this.PolicyNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PolicyNo.DataPropertyName = "AccountNo";
            this.PolicyNo.HeaderText = "Policy No";
            this.PolicyNo.MinimumWidth = 6;
            this.PolicyNo.Name = "PolicyNo";
            this.PolicyNo.ReadOnly = true;
            this.PolicyNo.Width = 78;
            // 
            // Product
            // 
            this.Product.DataPropertyName = "Product";
            this.Product.HeaderText = "Product";
            this.Product.MinimumWidth = 6;
            this.Product.Name = "Product";
            this.Product.ReadOnly = true;
            this.Product.Width = 71;
            // 
            // ProductType
            // 
            this.ProductType.DataPropertyName = "ProductType";
            this.ProductType.HeaderText = "ProductType";
            this.ProductType.MinimumWidth = 6;
            this.ProductType.Name = "ProductType";
            this.ProductType.ReadOnly = true;
            this.ProductType.Width = 94;
            // 
            // FundName
            // 
            this.FundName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FundName.DataPropertyName = "FundName";
            this.FundName.HeaderText = "Fund Name";
            this.FundName.MinimumWidth = 6;
            this.FundName.Name = "FundName";
            this.FundName.ReadOnly = true;
            this.FundName.Width = 90;
            // 
            // FundValue
            // 
            this.FundValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.FundValue.DataPropertyName = "FundValue";
            this.FundValue.HeaderText = "Fund Value (ZAR)";
            this.FundValue.MinimumWidth = 6;
            this.FundValue.Name = "FundValue";
            this.FundValue.ReadOnly = true;
            this.FundValue.Width = 118;
            // 
            // FundValueDate
            // 
            this.FundValueDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.FundValueDate.DataPropertyName = "FundValueDate";
            this.FundValueDate.HeaderText = "Fund Value Date";
            this.FundValueDate.MinimumWidth = 6;
            this.FundValueDate.Name = "FundValueDate";
            this.FundValueDate.ReadOnly = true;
            this.FundValueDate.Width = 116;
            // 
            // Premium
            // 
            this.Premium.DataPropertyName = "MonthlyPremium";
            this.Premium.HeaderText = "Premium (ZAR)";
            this.Premium.MinimumWidth = 6;
            this.Premium.Name = "Premium";
            this.Premium.ReadOnly = true;
            this.Premium.Width = 104;
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
            this.cmClientRecords.Size = new System.Drawing.Size(168, 70);
            this.cmClientRecords.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmClientRecords.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cmClientRecords.Opening += new System.ComponentModel.CancelEventHandler(this.cmClientRecords_Opening);
            // 
            // openClientToolStripMenuItem
            // 
            this.openClientToolStripMenuItem.Enabled = false;
            this.openClientToolStripMenuItem.Name = "openClientToolStripMenuItem";
            this.openClientToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.openClientToolStripMenuItem.Text = "Open Client";
            this.openClientToolStripMenuItem.Click += new System.EventHandler(this.openClientToolStripMenuItem_Click);
            // 
            // exportErrorRecordsToolStripMenuItem
            // 
            this.exportErrorRecordsToolStripMenuItem.Name = "exportErrorRecordsToolStripMenuItem";
            this.exportErrorRecordsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.exportErrorRecordsToolStripMenuItem.Visible = false;
            // 
            // copyCellContentToolStripMenuItem
            // 
            this.copyCellContentToolStripMenuItem.Name = "copyCellContentToolStripMenuItem";
            this.copyCellContentToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
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
            this.panelFileInfo.HorizontalScrollbarSize = 10;
            this.panelFileInfo.Location = new System.Drawing.Point(0, 0);
            this.panelFileInfo.Name = "panelFileInfo";
            this.panelFileInfo.Size = new System.Drawing.Size(277, 591);
            this.panelFileInfo.TabIndex = 15;
            this.panelFileInfo.VerticalScrollbar = true;
            this.panelFileInfo.VerticalScrollbarBarColor = true;
            this.panelFileInfo.VerticalScrollbarHighlightOnWheel = false;
            this.panelFileInfo.VerticalScrollbarSize = 10;
            // 
            // lblLastImportDate
            // 
            this.lblLastImportDate.AutoSize = true;
            this.lblLastImportDate.Location = new System.Drawing.Point(152, 167);
            this.lblLastImportDate.Name = "lblLastImportDate";
            this.lblLastImportDate.Size = new System.Drawing.Size(0, 13);
            this.lblLastImportDate.TabIndex = 45;
            // 
            // lblLastImportUser
            // 
            this.lblLastImportUser.AutoSize = true;
            this.lblLastImportUser.Location = new System.Drawing.Point(152, 188);
            this.lblLastImportUser.Name = "lblLastImportUser";
            this.lblLastImportUser.Size = new System.Drawing.Size(0, 13);
            this.lblLastImportUser.TabIndex = 47;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "Last Import User:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Last Import Date:";
            // 
            // lblImportStatus
            // 
            this.lblImportStatus.AutoSize = true;
            this.lblImportStatus.Location = new System.Drawing.Point(152, 146);
            this.lblImportStatus.Name = "lblImportStatus";
            this.lblImportStatus.Size = new System.Drawing.Size(46, 13);
            this.lblImportStatus.TabIndex = 43;
            this.lblImportStatus.Text = "Pending";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Import Status:";
            // 
            // cancelImport
            // 
            this.cancelImport.Enabled = false;
            this.cancelImport.Location = new System.Drawing.Point(145, 15);
            this.cancelImport.Name = "cancelImport";
            this.cancelImport.Size = new System.Drawing.Size(127, 29);
            this.cancelImport.TabIndex = 4;
            this.cancelImport.Values.Text = "&Cancel Import";
            // 
            // lblTotValErrors
            // 
            this.lblTotValErrors.AutoSize = true;
            this.lblTotValErrors.Location = new System.Drawing.Point(152, 278);
            this.lblTotValErrors.Name = "lblTotValErrors";
            this.lblTotValErrors.Size = new System.Drawing.Size(13, 13);
            this.lblTotValErrors.TabIndex = 40;
            this.lblTotValErrors.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightPink;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Tot Validation Errors:";
            // 
            // lblNewClientCnt
            // 
            this.lblNewClientCnt.AutoSize = true;
            this.lblNewClientCnt.Location = new System.Drawing.Point(152, 256);
            this.lblNewClientCnt.Name = "lblNewClientCnt";
            this.lblNewClientCnt.Size = new System.Drawing.Size(13, 13);
            this.lblNewClientCnt.TabIndex = 38;
            this.lblNewClientCnt.Text = "0";
            // 
            // lblExistingClientCnt
            // 
            this.lblExistingClientCnt.AutoSize = true;
            this.lblExistingClientCnt.Location = new System.Drawing.Point(152, 236);
            this.lblExistingClientCnt.Name = "lblExistingClientCnt";
            this.lblExistingClientCnt.Size = new System.Drawing.Size(13, 13);
            this.lblExistingClientCnt.TabIndex = 37;
            this.lblExistingClientCnt.Text = "0";
            // 
            // lblNewClientCnt1
            // 
            this.lblNewClientCnt1.AutoSize = true;
            this.lblNewClientCnt1.BackColor = System.Drawing.Color.LightGreen;
            this.lblNewClientCnt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewClientCnt1.Location = new System.Drawing.Point(7, 256);
            this.lblNewClientCnt1.Name = "lblNewClientCnt1";
            this.lblNewClientCnt1.Size = new System.Drawing.Size(104, 13);
            this.lblNewClientCnt1.TabIndex = 36;
            this.lblNewClientCnt1.Text = "New Client Records:";
            // 
            // lblExistingClientCnt1
            // 
            this.lblExistingClientCnt1.AutoSize = true;
            this.lblExistingClientCnt1.BackColor = System.Drawing.Color.LightBlue;
            this.lblExistingClientCnt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExistingClientCnt1.Location = new System.Drawing.Point(7, 236);
            this.lblExistingClientCnt1.Name = "lblExistingClientCnt1";
            this.lblExistingClientCnt1.Size = new System.Drawing.Size(144, 13);
            this.lblExistingClientCnt1.TabIndex = 35;
            this.lblExistingClientCnt1.Text = "Existing Client Record Count:";
            // 
            // lblFileFormat
            // 
            this.lblFileFormat.AutoSize = true;
            this.lblFileFormat.Location = new System.Drawing.Point(152, 126);
            this.lblFileFormat.Name = "lblFileFormat";
            this.lblFileFormat.Size = new System.Drawing.Size(25, 13);
            this.lblFileFormat.TabIndex = 34;
            this.lblFileFormat.Text = "Csv";
            // 
            // lblFileFormat1
            // 
            this.lblFileFormat1.AutoSize = true;
            this.lblFileFormat1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileFormat1.Location = new System.Drawing.Point(7, 126);
            this.lblFileFormat1.Name = "lblFileFormat1";
            this.lblFileFormat1.Size = new System.Drawing.Size(61, 13);
            this.lblFileFormat1.TabIndex = 33;
            this.lblFileFormat1.Text = "File Format:";
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(152, 106);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(29, 13);
            this.lblFileSize.TabIndex = 32;
            this.lblFileSize.Text = "2MB";
            // 
            // lblFileSize1
            // 
            this.lblFileSize1.AutoSize = true;
            this.lblFileSize1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileSize1.Location = new System.Drawing.Point(7, 106);
            this.lblFileSize1.Name = "lblFileSize1";
            this.lblFileSize1.Size = new System.Drawing.Size(49, 13);
            this.lblFileSize1.TabIndex = 31;
            this.lblFileSize1.Text = "File Size:";
            // 
            // lblFileDate
            // 
            this.lblFileDate.AutoSize = true;
            this.lblFileDate.Location = new System.Drawing.Point(152, 86);
            this.lblFileDate.Name = "lblFileDate";
            this.lblFileDate.Size = new System.Drawing.Size(99, 13);
            this.lblFileDate.TabIndex = 30;
            this.lblFileDate.Text = "15 May 2022 06:00";
            // 
            // lblFileDate1
            // 
            this.lblFileDate1.AutoSize = true;
            this.lblFileDate1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileDate1.Location = new System.Drawing.Point(7, 86);
            this.lblFileDate1.Name = "lblFileDate1";
            this.lblFileDate1.Size = new System.Drawing.Size(52, 13);
            this.lblFileDate1.TabIndex = 29;
            this.lblFileDate1.Text = "File Date:";
            // 
            // lblRecCnt
            // 
            this.lblRecCnt.AutoSize = true;
            this.lblRecCnt.Location = new System.Drawing.Point(152, 216);
            this.lblRecCnt.Name = "lblRecCnt";
            this.lblRecCnt.Size = new System.Drawing.Size(13, 13);
            this.lblRecCnt.TabIndex = 28;
            this.lblRecCnt.Text = "0";
            // 
            // lblRecCnt1
            // 
            this.lblRecCnt1.AutoSize = true;
            this.lblRecCnt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecCnt1.Location = new System.Drawing.Point(7, 216);
            this.lblRecCnt1.Name = "lblRecCnt1";
            this.lblRecCnt1.Size = new System.Drawing.Size(76, 13);
            this.lblRecCnt1.TabIndex = 27;
            this.lblRecCnt1.Text = "Record Count:";
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.Location = new System.Drawing.Point(152, 66);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(49, 13);
            this.lblSelectedFile.TabIndex = 23;
            this.lblSelectedFile.Text = "Filename";
            // 
            // lblSelectedFile1
            // 
            this.lblSelectedFile1.AutoSize = true;
            this.lblSelectedFile1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedFile1.Location = new System.Drawing.Point(7, 66);
            this.lblSelectedFile1.Name = "lblSelectedFile1";
            this.lblSelectedFile1.Size = new System.Drawing.Size(57, 13);
            this.lblSelectedFile1.TabIndex = 20;
            this.lblSelectedFile1.Text = "File Name:";
            // 
            // btnImportFile
            // 
            this.btnImportFile.Enabled = false;
            this.btnImportFile.Location = new System.Drawing.Point(10, 15);
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.Size = new System.Drawing.Size(127, 29);
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
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1347, 51);
            this.metroPanel1.TabIndex = 4;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // chkViewErrorRecords
            // 
            this.chkViewErrorRecords.AutoSize = true;
            this.chkViewErrorRecords.Enabled = false;
            this.chkViewErrorRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkViewErrorRecords.Location = new System.Drawing.Point(618, 19);
            this.chkViewErrorRecords.Margin = new System.Windows.Forms.Padding(2);
            this.chkViewErrorRecords.Name = "chkViewErrorRecords";
            this.chkViewErrorRecords.Size = new System.Drawing.Size(117, 17);
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
            this.chkViewExistingRecords.Location = new System.Drawing.Point(471, 19);
            this.chkViewExistingRecords.Margin = new System.Windows.Forms.Padding(2);
            this.chkViewExistingRecords.Name = "chkViewExistingRecords";
            this.chkViewExistingRecords.Size = new System.Drawing.Size(131, 17);
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
            this.chkViewNewRecords.Location = new System.Drawing.Point(332, 19);
            this.chkViewNewRecords.Margin = new System.Windows.Forms.Padding(2);
            this.chkViewNewRecords.Name = "chkViewNewRecords";
            this.chkViewNewRecords.Size = new System.Drawing.Size(117, 17);
            this.chkViewNewRecords.TabIndex = 19;
            this.chkViewNewRecords.Text = "View New Records";
            this.chkViewNewRecords.UseVisualStyleBackColor = true;
            this.chkViewNewRecords.CheckedChanged += new System.EventHandler(this.chkViewNewRecords_CheckedChanged);
            // 
            // kbtnOpenFile
            // 
            this.kbtnOpenFile.Location = new System.Drawing.Point(180, 10);
            this.kbtnOpenFile.Name = "kbtnOpenFile";
            this.kbtnOpenFile.Size = new System.Drawing.Size(125, 31);
            this.kbtnOpenFile.TabIndex = 2;
            this.kbtnOpenFile.Values.Text = "Select &File";
            this.kbtnOpenFile.Click += new System.EventHandler(this.kbtnOpenFile_Click);
            // 
            // cmbSelectLisp
            // 
            this.cmbSelectLisp.FormattingEnabled = true;
            this.cmbSelectLisp.ItemHeight = 23;
            this.cmbSelectLisp.Items.AddRange(new object[] {
            "Please select",
            "AllanGray",
            "Camissa",
            "EasiworxTemplate",
            "Momentum"});
            this.cmbSelectLisp.Location = new System.Drawing.Point(35, 11);
            this.cmbSelectLisp.Name = "cmbSelectLisp";
            this.cmbSelectLisp.Size = new System.Drawing.Size(139, 29);
            this.cmbSelectLisp.TabIndex = 1;
            this.cmbSelectLisp.UseSelectable = true;
            this.cmbSelectLisp.SelectedIndexChanged += new System.EventHandler(this.cmbSelectLisp_SelectedIndexChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(0, 12);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(34, 19);
            this.metroLabel1.TabIndex = 17;
            this.metroLabel1.Text = "Lisp:";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMetroClientImportInvestments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1391, 726);
            this.Controls.Add(this.kgbFileDetails);
            this.Name = "frmMetroClientImportInvestments";
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