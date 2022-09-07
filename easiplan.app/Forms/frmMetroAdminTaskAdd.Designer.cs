namespace Finx.App
{
    partial class frmMetroAdminTaskAdd
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
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.metroPanel_AdminTaskAdd = new MetroFramework.Controls.MetroPanel();
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
            this.xToolBarMenu1.Size = new System.Drawing.Size(549, 46);
            this.xToolBarMenu1.TabIndex = 2;
            // 
            // metroPanel_AdminTaskAdd
            // 
            this.metroPanel_AdminTaskAdd.AutoScroll = true;
            this.metroPanel_AdminTaskAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_AdminTaskAdd.HorizontalScrollbar = true;
            this.metroPanel_AdminTaskAdd.HorizontalScrollbarBarColor = false;
            this.metroPanel_AdminTaskAdd.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_AdminTaskAdd.HorizontalScrollbarSize = 10;
            this.metroPanel_AdminTaskAdd.Location = new System.Drawing.Point(20, 106);
            this.metroPanel_AdminTaskAdd.Name = "metroPanel_AdminTaskAdd";
            this.metroPanel_AdminTaskAdd.Size = new System.Drawing.Size(549, 394);
            this.metroPanel_AdminTaskAdd.TabIndex = 3;
            this.metroPanel_AdminTaskAdd.VerticalScrollbar = true;
            this.metroPanel_AdminTaskAdd.VerticalScrollbarBarColor = true;
            this.metroPanel_AdminTaskAdd.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_AdminTaskAdd.VerticalScrollbarSize = 10;
            // 
            // frmMetroAdminTaskAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 520);
            this.Controls.Add(this.metroPanel_AdminTaskAdd);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmMetroAdminTaskAdd";
            this.Text = "frmMetroAdminTaskAdd";
            this.Load += new System.EventHandler(this.frmMetroAdminTaskAdd_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private MetroFramework.Controls.MetroPanel metroPanel_AdminTaskAdd;
    }
}