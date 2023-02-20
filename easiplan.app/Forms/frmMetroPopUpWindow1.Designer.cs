namespace Finx.App.Forms
{
    partial class MetroPopUpWindow
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
            this.metroButton_OK = new MetroFramework.Controls.MetroButton();
            this.metroLabel_Caption = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // metroButton_OK
            // 
            this.metroButton_OK.Location = new System.Drawing.Point(170, 165);
            this.metroButton_OK.Margin = new System.Windows.Forms.Padding(4);
            this.metroButton_OK.Name = "metroButton_OK";
            this.metroButton_OK.Size = new System.Drawing.Size(185, 37);
            this.metroButton_OK.TabIndex = 5;
            this.metroButton_OK.Text = "OK";
            this.metroButton_OK.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroButton_OK.UseSelectable = true;
            this.metroButton_OK.Click += new System.EventHandler(this.metroButton_OK_Click);
            // 
            // metroLabel_Caption
            // 
            this.metroLabel_Caption.AutoSize = true;
            this.metroLabel_Caption.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel_Caption.Location = new System.Drawing.Point(170, 106);
            this.metroLabel_Caption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel_Caption.Name = "metroLabel_Caption";
            this.metroLabel_Caption.Size = new System.Drawing.Size(71, 25);
            this.metroLabel_Caption.TabIndex = 6;
            this.metroLabel_Caption.Text = "Caption";
            // 
            // MetroPopUpWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 226);
            this.Controls.Add(this.metroLabel_Caption);
            this.Controls.Add(this.metroButton_OK);
            this.Name = "MetroPopUpWindow";
            this.Text = "Alert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton metroButton_OK;
        private MetroFramework.Controls.MetroLabel metroLabel_Caption;
    }
}