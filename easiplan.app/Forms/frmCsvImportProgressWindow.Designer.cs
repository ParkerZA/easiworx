namespace Finx.App.Forms
{
    partial class frmCsvImportProgressWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCsvImportProgressWindow));
            this.metroLabel_Caption = new MetroFramework.Controls.MetroLabel();
            this.metroProgressBar1 = new MetroFramework.Controls.MetroProgressBar();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.metroLabel_Status = new MetroFramework.Controls.MetroLabel();
            this.metroButton_Cancel = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel_Caption
            // 
            this.metroLabel_Caption.AutoSize = true;
            this.metroLabel_Caption.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel_Caption.Location = new System.Drawing.Point(35, 64);
            this.metroLabel_Caption.Name = "metroLabel_Caption";
            this.metroLabel_Caption.Size = new System.Drawing.Size(71, 25);
            this.metroLabel_Caption.TabIndex = 0;
            this.metroLabel_Caption.Text = "Caption";
            // 
            // metroProgressBar1
            // 
            this.metroProgressBar1.Location = new System.Drawing.Point(14, 144);
            this.metroProgressBar1.Name = "metroProgressBar1";
            this.metroProgressBar1.Size = new System.Drawing.Size(65, 23);
            this.metroProgressBar1.TabIndex = 1;
            this.metroProgressBar1.Visible = false;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(324, 64);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(54, 45);
            this.metroProgressSpinner1.TabIndex = 2;
            this.metroProgressSpinner1.UseSelectable = true;
            // 
            // metroLabel_Status
            // 
            this.metroLabel_Status.Location = new System.Drawing.Point(23, 100);
            this.metroLabel_Status.Name = "metroLabel_Status";
            this.metroLabel_Status.Size = new System.Drawing.Size(295, 41);
            this.metroLabel_Status.TabIndex = 3;
            this.metroLabel_Status.Text = "please wait ...";
            this.metroLabel_Status.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel_Status.WrapToLine = true;
            // 
            // metroButton_Cancel
            // 
            this.metroButton_Cancel.Location = new System.Drawing.Point(130, 144);
            this.metroButton_Cancel.Name = "metroButton_Cancel";
            this.metroButton_Cancel.Size = new System.Drawing.Size(139, 30);
            this.metroButton_Cancel.TabIndex = 4;
            this.metroButton_Cancel.Text = "Cancel";
            this.metroButton_Cancel.UseSelectable = true;
            this.metroButton_Cancel.Click += new System.EventHandler(this.metroButton_Cancel_Click);
            // 
            // frmCsvImportProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 184);
            this.ControlBox = false;
            this.Controls.Add(this.metroButton_Cancel);
            this.Controls.Add(this.metroLabel_Status);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.metroProgressBar1);
            this.Controls.Add(this.metroLabel_Caption);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCsvImportProgressWindow";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "FinX";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel_Caption;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar1;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private MetroFramework.Controls.MetroLabel metroLabel_Status;
        private MetroFramework.Controls.MetroButton metroButton_Cancel;
    }
}