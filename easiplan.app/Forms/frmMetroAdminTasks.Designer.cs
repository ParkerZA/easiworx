namespace Finx.App.Forms
{
    partial class frmMetroAdminTasks
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_SearchTasks = new MetroFramework.Controls.MetroPanel();
            this.metroTabControl_AdminTasks = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGrid_AdminTasks = new SourceGrid.DataGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chartTaskByStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.chartByAllocation = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartByDaysOutstanding = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.metroPanel1.SuspendLayout();
            this.metroTabControl_AdminTasks.SuspendLayout();
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
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.xToolBarMenu1.BackColor = System.Drawing.Color.White;
            this.xToolBarMenu1.CausesValidation = false;
            this.xToolBarMenu1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(20, 60);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(1238, 46);
            this.xToolBarMenu1.TabIndex = 1;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroPanel_SearchTasks);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 106);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1238, 50);
            this.metroPanel1.TabIndex = 2;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel_SearchTasks
            // 
            this.metroPanel_SearchTasks.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_SearchTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_SearchTasks.HorizontalScrollbarBarColor = true;
            this.metroPanel_SearchTasks.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_SearchTasks.HorizontalScrollbarSize = 10;
            this.metroPanel_SearchTasks.Location = new System.Drawing.Point(0, 0);
            this.metroPanel_SearchTasks.Name = "metroPanel_SearchTasks";
            this.metroPanel_SearchTasks.Size = new System.Drawing.Size(1238, 50);
            this.metroPanel_SearchTasks.TabIndex = 2;
            this.metroPanel_SearchTasks.VerticalScrollbarBarColor = true;
            this.metroPanel_SearchTasks.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_SearchTasks.VerticalScrollbarSize = 10;
            // 
            // metroTabControl_AdminTasks
            // 
            this.metroTabControl_AdminTasks.Controls.Add(this.tabPage1);
            this.metroTabControl_AdminTasks.Controls.Add(this.tabPage2);
            this.metroTabControl_AdminTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl_AdminTasks.Location = new System.Drawing.Point(20, 156);
            this.metroTabControl_AdminTasks.Name = "metroTabControl_AdminTasks";
            this.metroTabControl_AdminTasks.SelectedIndex = 0;
            this.metroTabControl_AdminTasks.Size = new System.Drawing.Size(1238, 416);
            this.metroTabControl_AdminTasks.TabIndex = 3;
            this.metroTabControl_AdminTasks.UseSelectable = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGrid_AdminTasks);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1230, 374);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "List View";
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
            this.dataGrid_AdminTasks.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_AdminTasks.Name = "dataGrid_AdminTasks";
            this.dataGrid_AdminTasks.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_AdminTasks.Size = new System.Drawing.Size(1230, 374);
            this.dataGrid_AdminTasks.TabIndex = 7;
            this.dataGrid_AdminTasks.TabStop = true;
            this.dataGrid_AdminTasks.ToolTipText = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1168, 374);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graphic View";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainer1.Size = new System.Drawing.Size(1168, 374);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.TabIndex = 1;
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
            this.splitContainer2.Size = new System.Drawing.Size(1168, 221);
            this.splitContainer2.SplitterDistance = 593;
            this.splitContainer2.TabIndex = 0;
            // 
            // chartTaskByStatus
            // 
            chartArea4.AxisX.Title = "Years";
            chartArea4.AxisX2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea4.AxisY.Title = "Rands";
            chartArea4.AxisY2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea4.Name = "ChartArea1";
            this.chartTaskByStatus.ChartAreas.Add(chartArea4);
            this.chartTaskByStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend4.MaximumAutoSize = 100F;
            legend4.Name = "Legend1";
            this.chartTaskByStatus.Legends.Add(legend4);
            this.chartTaskByStatus.Location = new System.Drawing.Point(0, 0);
            this.chartTaskByStatus.Name = "chartTaskByStatus";
            this.chartTaskByStatus.Size = new System.Drawing.Size(569, 219);
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
            this.splitContainer3.Size = new System.Drawing.Size(1168, 149);
            this.splitContainer3.SplitterDistance = 591;
            this.splitContainer3.TabIndex = 0;
            // 
            // chartByAllocation
            // 
            chartArea5.AxisX.Title = "Years";
            chartArea5.AxisX2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea5.AxisY.Title = "Rands";
            chartArea5.AxisY2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea5.Name = "ChartArea1";
            this.chartByAllocation.ChartAreas.Add(chartArea5);
            this.chartByAllocation.Dock = System.Windows.Forms.DockStyle.Fill;
            legend5.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend5.MaximumAutoSize = 100F;
            legend5.Name = "Legend1";
            this.chartByAllocation.Legends.Add(legend5);
            this.chartByAllocation.Location = new System.Drawing.Point(0, 0);
            this.chartByAllocation.Name = "chartByAllocation";
            this.chartByAllocation.Size = new System.Drawing.Size(589, 147);
            this.chartByAllocation.TabIndex = 9;
            // 
            // chartByDaysOutstanding
            // 
            chartArea6.AxisX.Title = "Years";
            chartArea6.AxisX2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea6.AxisY.Title = "Rands";
            chartArea6.AxisY2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea6.Name = "ChartArea1";
            this.chartByDaysOutstanding.ChartAreas.Add(chartArea6);
            this.chartByDaysOutstanding.Dock = System.Windows.Forms.DockStyle.Fill;
            legend6.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend6.MaximumAutoSize = 100F;
            legend6.Name = "Legend1";
            this.chartByDaysOutstanding.Legends.Add(legend6);
            this.chartByDaysOutstanding.Location = new System.Drawing.Point(0, 0);
            this.chartByDaysOutstanding.Name = "chartByDaysOutstanding";
            this.chartByDaysOutstanding.Size = new System.Drawing.Size(571, 147);
            this.chartByDaysOutstanding.TabIndex = 9;
            // 
            // frmMetroAdminTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 592);
            this.Controls.Add(this.metroTabControl_AdminTasks);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmMetroAdminTasks";
            this.Text = "frmMetroAdminTasks";
            this.Load += new System.EventHandler(this.frmMetroAdminTasks_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroTabControl_AdminTasks.ResumeLayout(false);
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
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel_SearchTasks;
        private MetroFramework.Controls.MetroTabControl metroTabControl_AdminTasks;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private SourceGrid.DataGrid dataGrid_AdminTasks;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTaskByStatus;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartByAllocation;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartByDaysOutstanding;
    }
}