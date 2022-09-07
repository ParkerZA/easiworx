namespace Finx.App.Forms
{
    partial class frmMetroClientSegmentation
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
            this.metroPanel_Rating = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_Main = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.btnUpdate = new MetroFramework.Controls.MetroButton();
            this.metroPanel_Main.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel_Rating
            // 
            this.metroPanel_Rating.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel_Rating.HorizontalScrollbarBarColor = true;
            this.metroPanel_Rating.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Rating.HorizontalScrollbarSize = 10;
            this.metroPanel_Rating.Location = new System.Drawing.Point(20, 60);
            this.metroPanel_Rating.Name = "metroPanel_Rating";
            this.metroPanel_Rating.Size = new System.Drawing.Size(390, 45);
            this.metroPanel_Rating.TabIndex = 4;
            this.metroPanel_Rating.VerticalScrollbarBarColor = true;
            this.metroPanel_Rating.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Rating.VerticalScrollbarSize = 10;
            // 
            // metroPanel_Main
            // 
            this.metroPanel_Main.Controls.Add(this.metroPanel1);
            this.metroPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_Main.HorizontalScrollbarBarColor = true;
            this.metroPanel_Main.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Main.HorizontalScrollbarSize = 10;
            this.metroPanel_Main.Location = new System.Drawing.Point(20, 105);
            this.metroPanel_Main.Name = "metroPanel_Main";
            this.metroPanel_Main.Size = new System.Drawing.Size(390, 375);
            this.metroPanel_Main.TabIndex = 5;
            this.metroPanel_Main.VerticalScrollbarBarColor = true;
            this.metroPanel_Main.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Main.VerticalScrollbarSize = 10;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.btnUpdate);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 330);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(390, 45);
            this.metroPanel1.TabIndex = 5;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(257, 7);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(118, 30);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Save && Close";
            this.btnUpdate.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnUpdate.UseSelectable = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmMetroClientSegmentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 500);
            this.Controls.Add(this.metroPanel_Main);
            this.Controls.Add(this.metroPanel_Rating);
            this.Name = "frmMetroClientSegmentation";
            this.Text = "Client Rating";
            this.metroPanel_Main.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel_Rating;
        private MetroFramework.Controls.MetroPanel metroPanel_Main;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton btnUpdate;
    }
}