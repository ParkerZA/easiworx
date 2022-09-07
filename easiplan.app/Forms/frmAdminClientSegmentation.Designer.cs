namespace Finx.App.Forms
{
    partial class frmAdminClientSegmentation
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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panelFilter = new System.Windows.Forms.Panel();
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGrid_AdminTasks = new SourceGrid.DataGrid();
            this.labelTimeImage = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.xInputFilterAllocation = new Finx.App.UserControls.xInput();
            this.xInputFilterStatus = new Finx.App.UserControls.xInput();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.panelFilter.SuspendLayout();
            this.tbcMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelFilter.Controls.Add(this.labelTime);
            this.panelFilter.Controls.Add(this.labelTimeImage);
            this.panelFilter.Controls.Add(this.xInputFilterAllocation);
            this.panelFilter.Controls.Add(this.xInputFilterStatus);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 46);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1105, 37);
            this.panelFilter.TabIndex = 2;
            // 
            // tbcMain
            // 
            this.tbcMain.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tbcMain.Controls.Add(this.tabPage1);
            this.tbcMain.Controls.Add(this.tabPage2);
            this.tbcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcMain.Location = new System.Drawing.Point(0, 83);
            this.tbcMain.Multiline = true;
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(1105, 592);
            this.tbcMain.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGrid_AdminTasks);
            this.tabPage1.Location = new System.Drawing.Point(23, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1078, 584);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "List View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(23, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1078, 584);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graphic View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGrid_AdminTasks
            // 
            this.dataGrid_AdminTasks.BackColor = System.Drawing.Color.White;
            this.dataGrid_AdminTasks.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_AdminTasks.DeleteRowsWithDeleteKey = false;
            this.dataGrid_AdminTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_AdminTasks.EnableSort = false;
            this.dataGrid_AdminTasks.FixedColumns = 1;
            this.dataGrid_AdminTasks.FixedRows = 1;
            this.dataGrid_AdminTasks.Location = new System.Drawing.Point(3, 3);
            this.dataGrid_AdminTasks.Name = "dataGrid_AdminTasks";
            this.dataGrid_AdminTasks.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_AdminTasks.Size = new System.Drawing.Size(1072, 578);
            this.dataGrid_AdminTasks.TabIndex = 2;
            this.dataGrid_AdminTasks.TabStop = true;
            this.dataGrid_AdminTasks.ToolTipText = "";
            // 
            // labelTimeImage
            // 
            this.labelTimeImage.BackColor = System.Drawing.SystemColors.ControlLight;
            this.labelTimeImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTimeImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelTimeImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelTimeImage.Image = global::Finx.App.Properties.Resources.icon_time;
            this.labelTimeImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTimeImage.Location = new System.Drawing.Point(0, 0);
            this.labelTimeImage.Name = "labelTimeImage";
            this.labelTimeImage.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.labelTimeImage.Size = new System.Drawing.Size(38, 37);
            this.labelTimeImage.TabIndex = 2;
            this.labelTimeImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTime
            // 
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelTime.Location = new System.Drawing.Point(38, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(296, 37);
            this.labelTime.TabIndex = 3;
            this.labelTime.Text = "label1";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xInputFilterAllocation
            // 
            this.xInputFilterAllocation.BackColor = System.Drawing.Color.Transparent;
            this.xInputFilterAllocation.ControlTypes = Finx.App.UserControls.ControlTypes.ComboBox;
            this.xInputFilterAllocation.ControlWidth = 200;
            this.xInputFilterAllocation.DataSource = null;
            this.xInputFilterAllocation.DisplayMember = "Text";
            this.xInputFilterAllocation.Dock = System.Windows.Forms.DockStyle.Right;
            this.xInputFilterAllocation.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInputFilterAllocation.LableText = "Allocation";
            this.xInputFilterAllocation.Location = new System.Drawing.Point(545, 0);
            this.xInputFilterAllocation.MappedField = null;
            this.xInputFilterAllocation.Margin = new System.Windows.Forms.Padding(0);
            this.xInputFilterAllocation.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInputFilterAllocation.Model = null;
            this.xInputFilterAllocation.Name = "xInputFilterAllocation";
            this.xInputFilterAllocation.Padding = new System.Windows.Forms.Padding(1);
            this.xInputFilterAllocation.ReadOnly = false;
            this.xInputFilterAllocation.Size = new System.Drawing.Size(280, 37);
            this.xInputFilterAllocation.TabIndex = 1;
            this.xInputFilterAllocation.ValueMember = "Value";
            // 
            // xInputFilterStatus
            // 
            this.xInputFilterStatus.BackColor = System.Drawing.Color.Transparent;
            this.xInputFilterStatus.ControlTypes = Finx.App.UserControls.ControlTypes.ComboList;
            this.xInputFilterStatus.ControlWidth = 200;
            this.xInputFilterStatus.DataSource = null;
            this.xInputFilterStatus.DisplayMember = "Text";
            this.xInputFilterStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.xInputFilterStatus.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInputFilterStatus.LableText = "Status";
            this.xInputFilterStatus.Location = new System.Drawing.Point(825, 0);
            this.xInputFilterStatus.MappedField = null;
            this.xInputFilterStatus.Margin = new System.Windows.Forms.Padding(0);
            this.xInputFilterStatus.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInputFilterStatus.Model = null;
            this.xInputFilterStatus.Name = "xInputFilterStatus";
            this.xInputFilterStatus.Padding = new System.Windows.Forms.Padding(1);
            this.xInputFilterStatus.ReadOnly = false;
            this.xInputFilterStatus.Size = new System.Drawing.Size(280, 37);
            this.xInputFilterStatus.TabIndex = 0;
            this.xInputFilterStatus.ValueMember = "Value";
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.BackColor = System.Drawing.Color.Transparent;
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(0, 0);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(1105, 46);
            this.xToolBarMenu1.TabIndex = 0;
            // 
            // frmAdminTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 675);
            this.Controls.Add(this.tbcMain);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmAdminTasks";
            this.Text = "frmAdminTasks";
            this.Load += new System.EventHandler(this.frmAdminTasks_Load);
            this.panelFilter.ResumeLayout(false);
            this.tbcMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panelFilter;
        private UserControls.xInput xInputFilterStatus;
        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private UserControls.xInput xInputFilterAllocation;
        private SourceGrid.DataGrid dataGrid_AdminTasks;
        private System.Windows.Forms.Label labelTimeImage;
        private System.Windows.Forms.Label labelTime;
    }
}