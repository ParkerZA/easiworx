namespace Finx.App.Forms
{
    partial class frmAdminTasks
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panelFilter = new System.Windows.Forms.Panel();
            this.labelTime = new System.Windows.Forms.Label();
            this.xInputFilterAllocation = new Finx.App.UserControls.xInput();
            this.xInputFilterStatus = new Finx.App.UserControls.xInput();
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGrid_AdminTasks = new SourceGrid.DataGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chartTaskByStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.chartByAllocation = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartByDaysOutstanding = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.panelFilter.SuspendLayout();
            this.tbcMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTaskByStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartByAllocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartByDaysOutstanding)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.SystemColors.Control;
            this.panelFilter.Controls.Add(this.labelTime);
            this.panelFilter.Controls.Add(this.xInputFilterAllocation);
            this.panelFilter.Controls.Add(this.xInputFilterStatus);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 46);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1196, 37);
            this.panelFilter.TabIndex = 2;
            // 
            // labelTime
            // 
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelTime.Location = new System.Drawing.Point(0, 0);
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
            this.xInputFilterAllocation.Location = new System.Drawing.Point(636, 0);
            this.xInputFilterAllocation.MappedField = null;
            this.xInputFilterAllocation.Margin = new System.Windows.Forms.Padding(0);
            this.xInputFilterAllocation.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInputFilterAllocation.Model = null;
            this.xInputFilterAllocation.Name = "xInputFilterAllocation";
            this.xInputFilterAllocation.Padding = new System.Windows.Forms.Padding(1);
            this.xInputFilterAllocation.ReadOnly = false;
            this.xInputFilterAllocation.Size = new System.Drawing.Size(280, 37);
            this.xInputFilterAllocation.TabIndex = 1;
            this.xInputFilterAllocation.Value = null;
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
            this.xInputFilterStatus.LableText = "View";
            this.xInputFilterStatus.Location = new System.Drawing.Point(916, 0);
            this.xInputFilterStatus.MappedField = null;
            this.xInputFilterStatus.Margin = new System.Windows.Forms.Padding(0);
            this.xInputFilterStatus.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInputFilterStatus.Model = null;
            this.xInputFilterStatus.Name = "xInputFilterStatus";
            this.xInputFilterStatus.Padding = new System.Windows.Forms.Padding(1);
            this.xInputFilterStatus.ReadOnly = false;
            this.xInputFilterStatus.Size = new System.Drawing.Size(280, 37);
            this.xInputFilterStatus.TabIndex = 0;
            this.xInputFilterStatus.Value = null;
            this.xInputFilterStatus.ValueMember = "Value";
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
            this.tbcMain.Size = new System.Drawing.Size(1196, 592);
            this.tbcMain.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGrid_AdminTasks);
            this.tabPage1.Location = new System.Drawing.Point(23, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1169, 584);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "List View";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.dataGrid_AdminTasks.Size = new System.Drawing.Size(1163, 578);
            this.dataGrid_AdminTasks.TabIndex = 2;
            this.dataGrid_AdminTasks.TabStop = true;
            this.dataGrid_AdminTasks.ToolTipText = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(23, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1169, 584);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graphic View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1163, 578);
            this.splitContainer1.SplitterDistance = 344;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chartTaskByStatus);
            this.splitContainer2.Size = new System.Drawing.Size(1163, 344);
            this.splitContainer2.SplitterDistance = 591;
            this.splitContainer2.TabIndex = 0;
            // 
            // chartTaskByStatus
            // 
            chartArea1.AxisX.Title = "Years";
            chartArea1.AxisX2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY.Title = "Rands";
            chartArea1.AxisY2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea1";
            this.chartTaskByStatus.ChartAreas.Add(chartArea1);
            this.chartTaskByStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.MaximumAutoSize = 100F;
            legend1.Name = "Legend1";
            this.chartTaskByStatus.Legends.Add(legend1);
            this.chartTaskByStatus.Location = new System.Drawing.Point(0, 0);
            this.chartTaskByStatus.Name = "chartTaskByStatus";
            this.chartTaskByStatus.Size = new System.Drawing.Size(566, 342);
            this.chartTaskByStatus.TabIndex = 8;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.chartByAllocation);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.chartByDaysOutstanding);
            this.splitContainer3.Size = new System.Drawing.Size(1163, 230);
            this.splitContainer3.SplitterDistance = 589;
            this.splitContainer3.TabIndex = 0;
            // 
            // chartByAllocation
            // 
            chartArea2.AxisX.Title = "Years";
            chartArea2.AxisX2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisY.Title = "Rands";
            chartArea2.AxisY2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.Name = "ChartArea1";
            this.chartByAllocation.ChartAreas.Add(chartArea2);
            this.chartByAllocation.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.MaximumAutoSize = 100F;
            legend2.Name = "Legend1";
            this.chartByAllocation.Legends.Add(legend2);
            this.chartByAllocation.Location = new System.Drawing.Point(0, 0);
            this.chartByAllocation.Name = "chartByAllocation";
            this.chartByAllocation.Size = new System.Drawing.Size(587, 228);
            this.chartByAllocation.TabIndex = 9;
            // 
            // chartByDaysOutstanding
            // 
            chartArea3.AxisX.Title = "Years";
            chartArea3.AxisX2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisY.Title = "Rands";
            chartArea3.AxisY2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.Name = "ChartArea1";
            this.chartByDaysOutstanding.ChartAreas.Add(chartArea3);
            this.chartByDaysOutstanding.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend3.MaximumAutoSize = 100F;
            legend3.Name = "Legend1";
            this.chartByDaysOutstanding.Legends.Add(legend3);
            this.chartByDaysOutstanding.Location = new System.Drawing.Point(0, 0);
            this.chartByDaysOutstanding.Name = "chartByDaysOutstanding";
            this.chartByDaysOutstanding.Size = new System.Drawing.Size(568, 228);
            this.chartByDaysOutstanding.TabIndex = 9;
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.BackColor = System.Drawing.Color.Transparent;
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(0, 0);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(1196, 46);
            this.xToolBarMenu1.TabIndex = 0;
            // 
            // frmAdminTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 675);
            this.Controls.Add(this.tbcMain);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmAdminTasks";
            this.Text = "frmAdminTasks";
            this.Deactivate += new System.EventHandler(this.frmAdminTasks_Deactivate);
            this.Load += new System.EventHandler(this.frmAdminTasks_Load);
            this.panelFilter.ResumeLayout(false);
            this.tbcMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTaskByStatus)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartByAllocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartByDaysOutstanding)).EndInit();
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
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTaskByStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartByAllocation;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartByDaysOutstanding;
    }
}