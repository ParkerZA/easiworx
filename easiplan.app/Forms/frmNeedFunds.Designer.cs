namespace Finx.App.Forms
{
    partial class frmNeedFunds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNeedFunds));
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGrid_RetirementPortfolio = new SourceGrid.DataGrid();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dataGrid_RetirementFunds = new SourceGrid.DataGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_RetirementFund = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(0, 0);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(946, 46);
            this.xToolBarMenu1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(946, 286);
            this.panel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(946, 286);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGrid_RetirementPortfolio);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel6);
            this.splitContainer2.Panel2.Controls.Add(this.panel4);
            this.splitContainer2.Size = new System.Drawing.Size(946, 286);
            this.splitContainer2.SplitterDistance = 93;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 6;
            this.splitContainer2.TabStop = false;
            // 
            // dataGrid_RetirementPortfolio
            // 
            this.dataGrid_RetirementPortfolio.BackColor = System.Drawing.Color.White;
            this.dataGrid_RetirementPortfolio.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_RetirementPortfolio.DeleteRowsWithDeleteKey = false;
            this.dataGrid_RetirementPortfolio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_RetirementPortfolio.EnableSort = false;
            this.dataGrid_RetirementPortfolio.FixedColumns = 1;
            this.dataGrid_RetirementPortfolio.FixedRows = 1;
            this.dataGrid_RetirementPortfolio.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_RetirementPortfolio.Name = "dataGrid_RetirementPortfolio";
            this.dataGrid_RetirementPortfolio.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_RetirementPortfolio.Size = new System.Drawing.Size(944, 91);
            this.dataGrid_RetirementPortfolio.TabIndex = 3;
            this.dataGrid_RetirementPortfolio.TabStop = true;
            this.dataGrid_RetirementPortfolio.ToolTipText = "";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dataGrid_RetirementFunds);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 34);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(944, 151);
            this.panel6.TabIndex = 6;
            // 
            // dataGrid_RetirementFunds
            // 
            this.dataGrid_RetirementFunds.BackColor = System.Drawing.Color.White;
            this.dataGrid_RetirementFunds.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_RetirementFunds.DeleteRowsWithDeleteKey = false;
            this.dataGrid_RetirementFunds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_RetirementFunds.EnableSort = false;
            this.dataGrid_RetirementFunds.FixedColumns = 1;
            this.dataGrid_RetirementFunds.FixedRows = 1;
            this.dataGrid_RetirementFunds.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_RetirementFunds.Name = "dataGrid_RetirementFunds";
            this.dataGrid_RetirementFunds.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_RetirementFunds.Size = new System.Drawing.Size(944, 151);
            this.dataGrid_RetirementFunds.TabIndex = 5;
            this.dataGrid_RetirementFunds.TabStop = true;
            this.dataGrid_RetirementFunds.ToolTipText = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label_RetirementFund);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(944, 34);
            this.panel4.TabIndex = 5;
            // 
            // label_RetirementFund
            // 
            this.label_RetirementFund.AutoSize = true;
            this.label_RetirementFund.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RetirementFund.Location = new System.Drawing.Point(19, 9);
            this.label_RetirementFund.Name = "label_RetirementFund";
            this.label_RetirementFund.Size = new System.Drawing.Size(119, 17);
            this.label_RetirementFund.TabIndex = 0;
            this.label_RetirementFund.Text = "Select Funds for :";
            // 
            // frmNeedFunds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 332);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.xToolBarMenu1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNeedFunds";
            this.Text = "frmRetirementFunds";
            this.Load += new System.EventHandler(this.frmNeedFunds_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Finx.App.UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SourceGrid.DataGrid dataGrid_RetirementPortfolio;
        private System.Windows.Forms.Panel panel6;
        private SourceGrid.DataGrid dataGrid_RetirementFunds;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label_RetirementFund;
    }
}