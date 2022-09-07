namespace Finx.App
{
    partial class frmMetroClientCommunicationEmail
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
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_AdminTaskAdd = new MetroFramework.Controls.MetroPanel();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 106);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(574, 10);
            this.metroPanel1.TabIndex = 4;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel_AdminTaskAdd
            // 
            this.metroPanel_AdminTaskAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_AdminTaskAdd.HorizontalScrollbarBarColor = true;
            this.metroPanel_AdminTaskAdd.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_AdminTaskAdd.HorizontalScrollbarSize = 10;
            this.metroPanel_AdminTaskAdd.Location = new System.Drawing.Point(20, 116);
            this.metroPanel_AdminTaskAdd.Name = "metroPanel_AdminTaskAdd";
            this.metroPanel_AdminTaskAdd.Size = new System.Drawing.Size(574, 331);
            this.metroPanel_AdminTaskAdd.TabIndex = 6;
            this.metroPanel_AdminTaskAdd.VerticalScrollbarBarColor = true;
            this.metroPanel_AdminTaskAdd.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_AdminTaskAdd.VerticalScrollbarSize = 10;
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(20, 60);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(574, 46);
            this.xToolBarMenu1.TabIndex = 2;
            // 
            // frmMetroClientCommunicationEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 467);
            this.Controls.Add(this.metroPanel_AdminTaskAdd);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmMetroClientCommunicationEmail";
            this.Text = "frmMetroAdminTaskAdd";
            this.Load += new System.EventHandler(this.frmMetroAdminTaskAdd_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel_AdminTaskAdd;
    }
}