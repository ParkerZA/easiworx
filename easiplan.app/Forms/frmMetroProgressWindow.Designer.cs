namespace Finx.App.Forms
{
    partial class MetroProgressWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetroProgressWindow));
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
            this.metroLabel_Caption.Location = new System.Drawing.Point(47, 79);
            this.metroLabel_Caption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel_Caption.Name = "metroLabel_Caption";
            this.metroLabel_Caption.Size = new System.Drawing.Size(71, 25);
            this.metroLabel_Caption.TabIndex = 0;
            this.metroLabel_Caption.Text = "Caption";
            // 
            // metroProgressBar1
            // 
            this.metroProgressBar1.Location = new System.Drawing.Point(19, 177);
            this.metroProgressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.metroProgressBar1.Name = "metroProgressBar1";
            this.metroProgressBar1.Size = new System.Drawing.Size(87, 28);
            this.metroProgressBar1.TabIndex = 1;
            this.metroProgressBar1.Visible = false;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(432, 79);
            this.metroProgressSpinner1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(72, 55);
            this.metroProgressSpinner1.TabIndex = 2;
            this.metroProgressSpinner1.UseSelectable = true;
            // 
            // metroLabel_Status
            // 
            this.metroLabel_Status.Location = new System.Drawing.Point(31, 123);
            this.metroLabel_Status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel_Status.Name = "metroLabel_Status";
            this.metroLabel_Status.Size = new System.Drawing.Size(393, 50);
            this.metroLabel_Status.TabIndex = 3;
            this.metroLabel_Status.Text = "please wait ...";
            this.metroLabel_Status.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel_Status.WrapToLine = true;
            // 
            // metroButton_Cancel
            // 
            this.metroButton_Cancel.Location = new System.Drawing.Point(173, 177);
            this.metroButton_Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.metroButton_Cancel.Name = "metroButton_Cancel";
            this.metroButton_Cancel.Size = new System.Drawing.Size(185, 37);
            this.metroButton_Cancel.TabIndex = 4;
            this.metroButton_Cancel.Text = "Cancel";
            this.metroButton_Cancel.UseSelectable = true;
            this.metroButton_Cancel.Click += new System.EventHandler(this.metroButton_Cancel_Click);
            // 
            // MetroProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 226);
            this.ControlBox = false;
            this.Controls.Add(this.metroButton_Cancel);
            this.Controls.Add(this.metroLabel_Status);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.metroProgressBar1);
            this.Controls.Add(this.metroLabel_Caption);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MetroProgressWindow";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
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