namespace Finx.App.Forms
{
    partial class frmServiceProviders
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Lisp = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGrid_Lisp = new SourceGrid.DataGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGrid_LispFunds = new SourceGrid.DataGrid();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label_RetirementFund = new System.Windows.Forms.Label();
            this.tabPage_Medical = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGrid_MedicalAid = new SourceGrid.DataGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGrid_MedicalAidPlans = new SourceGrid.DataGrid();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label_MedicalProviderFunds = new System.Windows.Forms.Label();
            this.tabPage_LifeInsurers = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataGrid_LifeInsurers = new SourceGrid.DataGrid();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dataGrid_LifeProducts = new SourceGrid.DataGrid();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label_LifeProducts = new System.Windows.Forms.Label();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.panel7.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_Lisp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tabPage_Medical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tabPage_LifeInsurers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(20, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(803, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tabControl1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(20, 106);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(803, 318);
            this.panel7.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Lisp);
            this.tabControl1.Controls.Add(this.tabPage_Medical);
            this.tabControl1.Controls.Add(this.tabPage_LifeInsurers);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(803, 318);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage_Lisp
            // 
            this.tabPage_Lisp.Controls.Add(this.splitContainer1);
            this.tabPage_Lisp.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Lisp.Name = "tabPage_Lisp";
            this.tabPage_Lisp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Lisp.Size = new System.Drawing.Size(795, 289);
            this.tabPage_Lisp.TabIndex = 0;
            this.tabPage_Lisp.Text = "Linked Investment Service Providers";
            this.tabPage_Lisp.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Panel1.Controls.Add(this.dataGrid_Lisp);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(789, 283);
            this.splitContainer1.SplitterDistance = 97;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGrid_Lisp
            // 
            this.dataGrid_Lisp.BackColor = System.Drawing.Color.White;
            this.dataGrid_Lisp.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_Lisp.DeleteRowsWithDeleteKey = false;
            this.dataGrid_Lisp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_Lisp.EnableSort = false;
            this.dataGrid_Lisp.FixedColumns = 1;
            this.dataGrid_Lisp.FixedRows = 1;
            this.dataGrid_Lisp.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_Lisp.Name = "dataGrid_Lisp";
            this.dataGrid_Lisp.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_Lisp.Size = new System.Drawing.Size(787, 95);
            this.dataGrid_Lisp.TabIndex = 7;
            this.dataGrid_Lisp.TabStop = true;
            this.dataGrid_Lisp.ToolTipText = "";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(787, 95);
            this.panel2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.dataGrid_LispFunds);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 180);
            this.panel1.TabIndex = 0;
            // 
            // dataGrid_LispFunds
            // 
            this.dataGrid_LispFunds.BackColor = System.Drawing.Color.White;
            this.dataGrid_LispFunds.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_LispFunds.DeleteRowsWithDeleteKey = false;
            this.dataGrid_LispFunds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_LispFunds.EnableSort = false;
            this.dataGrid_LispFunds.FixedColumns = 1;
            this.dataGrid_LispFunds.FixedRows = 1;
            this.dataGrid_LispFunds.Location = new System.Drawing.Point(0, 39);
            this.dataGrid_LispFunds.Name = "dataGrid_LispFunds";
            this.dataGrid_LispFunds.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_LispFunds.Size = new System.Drawing.Size(787, 141);
            this.dataGrid_LispFunds.TabIndex = 7;
            this.dataGrid_LispFunds.TabStop = true;
            this.dataGrid_LispFunds.ToolTipText = "";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label_RetirementFund);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(787, 39);
            this.panel8.TabIndex = 2;
            // 
            // label_RetirementFund
            // 
            this.label_RetirementFund.AutoSize = true;
            this.label_RetirementFund.Location = new System.Drawing.Point(4, 10);
            this.label_RetirementFund.Name = "label_RetirementFund";
            this.label_RetirementFund.Size = new System.Drawing.Size(55, 17);
            this.label_RetirementFund.TabIndex = 2;
            this.label_RetirementFund.Text = "Funds :";
            // 
            // tabPage_Medical
            // 
            this.tabPage_Medical.Controls.Add(this.splitContainer2);
            this.tabPage_Medical.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Medical.Name = "tabPage_Medical";
            this.tabPage_Medical.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Medical.Size = new System.Drawing.Size(795, 289);
            this.tabPage_Medical.TabIndex = 1;
            this.tabPage_Medical.Text = "Medical Aid Providers";
            this.tabPage_Medical.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGrid_MedicalAid);
            this.splitContainer2.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel4);
            this.splitContainer2.Size = new System.Drawing.Size(789, 283);
            this.splitContainer2.SplitterDistance = 97;
            this.splitContainer2.TabIndex = 1;
            // 
            // dataGrid_MedicalAid
            // 
            this.dataGrid_MedicalAid.BackColor = System.Drawing.Color.White;
            this.dataGrid_MedicalAid.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_MedicalAid.DeleteRowsWithDeleteKey = false;
            this.dataGrid_MedicalAid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_MedicalAid.EnableSort = false;
            this.dataGrid_MedicalAid.FixedColumns = 1;
            this.dataGrid_MedicalAid.FixedRows = 1;
            this.dataGrid_MedicalAid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_MedicalAid.Name = "dataGrid_MedicalAid";
            this.dataGrid_MedicalAid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_MedicalAid.Size = new System.Drawing.Size(787, 95);
            this.dataGrid_MedicalAid.TabIndex = 7;
            this.dataGrid_MedicalAid.TabStop = true;
            this.dataGrid_MedicalAid.ToolTipText = "";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(787, 95);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.dataGrid_MedicalAidPlans);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 180);
            this.panel4.TabIndex = 0;
            // 
            // dataGrid_MedicalAidPlans
            // 
            this.dataGrid_MedicalAidPlans.BackColor = System.Drawing.Color.White;
            this.dataGrid_MedicalAidPlans.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_MedicalAidPlans.DeleteRowsWithDeleteKey = false;
            this.dataGrid_MedicalAidPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_MedicalAidPlans.EnableSort = false;
            this.dataGrid_MedicalAidPlans.FixedColumns = 1;
            this.dataGrid_MedicalAidPlans.FixedRows = 1;
            this.dataGrid_MedicalAidPlans.Location = new System.Drawing.Point(0, 30);
            this.dataGrid_MedicalAidPlans.Name = "dataGrid_MedicalAidPlans";
            this.dataGrid_MedicalAidPlans.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_MedicalAidPlans.Size = new System.Drawing.Size(787, 150);
            this.dataGrid_MedicalAidPlans.TabIndex = 8;
            this.dataGrid_MedicalAidPlans.TabStop = true;
            this.dataGrid_MedicalAidPlans.ToolTipText = "";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label_MedicalProviderFunds);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(787, 30);
            this.panel9.TabIndex = 7;
            // 
            // label_MedicalProviderFunds
            // 
            this.label_MedicalProviderFunds.AutoSize = true;
            this.label_MedicalProviderFunds.Location = new System.Drawing.Point(4, 6);
            this.label_MedicalProviderFunds.Name = "label_MedicalProviderFunds";
            this.label_MedicalProviderFunds.Size = new System.Drawing.Size(51, 17);
            this.label_MedicalProviderFunds.TabIndex = 2;
            this.label_MedicalProviderFunds.Text = "Plans :";
            // 
            // tabPage_LifeInsurers
            // 
            this.tabPage_LifeInsurers.Controls.Add(this.splitContainer3);
            this.tabPage_LifeInsurers.Location = new System.Drawing.Point(4, 25);
            this.tabPage_LifeInsurers.Name = "tabPage_LifeInsurers";
            this.tabPage_LifeInsurers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_LifeInsurers.Size = new System.Drawing.Size(795, 289);
            this.tabPage_LifeInsurers.TabIndex = 2;
            this.tabPage_LifeInsurers.Text = "Life Insurers";
            this.tabPage_LifeInsurers.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dataGrid_LifeInsurers);
            this.splitContainer3.Panel1.Controls.Add(this.panel5);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.panel6);
            this.splitContainer3.Size = new System.Drawing.Size(789, 283);
            this.splitContainer3.SplitterDistance = 97;
            this.splitContainer3.TabIndex = 1;
            // 
            // dataGrid_LifeInsurers
            // 
            this.dataGrid_LifeInsurers.BackColor = System.Drawing.Color.White;
            this.dataGrid_LifeInsurers.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_LifeInsurers.DeleteRowsWithDeleteKey = false;
            this.dataGrid_LifeInsurers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_LifeInsurers.EnableSort = false;
            this.dataGrid_LifeInsurers.FixedColumns = 1;
            this.dataGrid_LifeInsurers.FixedRows = 1;
            this.dataGrid_LifeInsurers.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_LifeInsurers.Name = "dataGrid_LifeInsurers";
            this.dataGrid_LifeInsurers.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_LifeInsurers.Size = new System.Drawing.Size(787, 95);
            this.dataGrid_LifeInsurers.TabIndex = 7;
            this.dataGrid_LifeInsurers.TabStop = true;
            this.dataGrid_LifeInsurers.ToolTipText = "";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(787, 95);
            this.panel5.TabIndex = 5;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel6.Controls.Add(this.dataGrid_LifeProducts);
            this.panel6.Controls.Add(this.panel10);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(787, 180);
            this.panel6.TabIndex = 0;
            // 
            // dataGrid_LifeProducts
            // 
            this.dataGrid_LifeProducts.BackColor = System.Drawing.Color.White;
            this.dataGrid_LifeProducts.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_LifeProducts.DeleteRowsWithDeleteKey = false;
            this.dataGrid_LifeProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_LifeProducts.EnableSort = false;
            this.dataGrid_LifeProducts.FixedColumns = 1;
            this.dataGrid_LifeProducts.FixedRows = 1;
            this.dataGrid_LifeProducts.Location = new System.Drawing.Point(0, 34);
            this.dataGrid_LifeProducts.Name = "dataGrid_LifeProducts";
            this.dataGrid_LifeProducts.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_LifeProducts.Size = new System.Drawing.Size(787, 146);
            this.dataGrid_LifeProducts.TabIndex = 8;
            this.dataGrid_LifeProducts.TabStop = true;
            this.dataGrid_LifeProducts.ToolTipText = "";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.label_LifeProducts);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(787, 34);
            this.panel10.TabIndex = 7;
            // 
            // label_LifeProducts
            // 
            this.label_LifeProducts.AutoSize = true;
            this.label_LifeProducts.Location = new System.Drawing.Point(10, 10);
            this.label_LifeProducts.Name = "label_LifeProducts";
            this.label_LifeProducts.Size = new System.Drawing.Size(72, 17);
            this.label_LifeProducts.TabIndex = 2;
            this.label_LifeProducts.Text = "Products :";
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
            this.xToolBarMenu1.Size = new System.Drawing.Size(803, 46);
            this.xToolBarMenu1.TabIndex = 3;
            // 
            // frmServiceProviders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 466);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.xToolBarMenu1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmServiceProviders";
            this.Load += new System.EventHandler(this.frmListData_Load);
            this.panel7.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Lisp.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tabPage_Medical.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.tabPage_LifeInsurers.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private Finx.App.UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Lisp;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private SourceGrid.DataGrid dataGrid_Lisp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPage_Medical;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SourceGrid.DataGrid dataGrid_MedicalAid;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabPage tabPage_LifeInsurers;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private SourceGrid.DataGrid dataGrid_LifeInsurers;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private SourceGrid.DataGrid dataGrid_LispFunds;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label_RetirementFund;
        private SourceGrid.DataGrid dataGrid_MedicalAidPlans;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label_MedicalProviderFunds;
        private SourceGrid.DataGrid dataGrid_LifeProducts;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label_LifeProducts;
    }
}