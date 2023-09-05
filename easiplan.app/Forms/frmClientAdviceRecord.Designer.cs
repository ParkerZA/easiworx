namespace easiplan.app.Forms
{
    partial class frmClientAdviceRecord
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
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.metroPanel_Notes_History = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_Notes = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_Notes_Header = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_CAR_header = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_CAR = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_Car_History = new MetroFramework.Controls.MetroPanel();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.xToolBarMenu1.BackColor = System.Drawing.Color.White;
            this.xToolBarMenu1.CausesValidation = false;
            this.xToolBarMenu1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(0, 0);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(882, 46);
            this.xToolBarMenu1.TabIndex = 3;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(0, 46);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(882, 479);
            this.metroTabControl1.TabIndex = 5;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.splitContainer2);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 0;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(874, 437);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "metroTabPage2";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.White;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.metroPanel_Car_History);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.metroPanel_CAR);
            this.splitContainer2.Panel2.Controls.Add(this.metroPanel_CAR_header);
            this.splitContainer2.Size = new System.Drawing.Size(874, 437);
            this.splitContainer2.SplitterDistance = 129;
            this.splitContainer2.TabIndex = 9;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.splitContainer1);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 0;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(874, 437);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "metroTabPage1";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.metroPanel_Notes_History);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.metroPanel_Notes);
            this.splitContainer1.Panel2.Controls.Add(this.metroPanel_Notes_Header);
            this.splitContainer1.Size = new System.Drawing.Size(874, 437);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 2;
            // 
            // metroPanel_Notes_History
            // 
            this.metroPanel_Notes_History.AutoScroll = true;
            this.metroPanel_Notes_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_Notes_History.HorizontalScrollbar = true;
            this.metroPanel_Notes_History.HorizontalScrollbarBarColor = false;
            this.metroPanel_Notes_History.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Notes_History.HorizontalScrollbarSize = 10;
            this.metroPanel_Notes_History.Location = new System.Drawing.Point(0, 0);
            this.metroPanel_Notes_History.Name = "metroPanel_Notes_History";
            this.metroPanel_Notes_History.Size = new System.Drawing.Size(291, 437);
            this.metroPanel_Notes_History.TabIndex = 8;
            this.metroPanel_Notes_History.VerticalScrollbar = true;
            this.metroPanel_Notes_History.VerticalScrollbarBarColor = true;
            this.metroPanel_Notes_History.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Notes_History.VerticalScrollbarSize = 10;
            // 
            // metroPanel_Notes
            // 
            this.metroPanel_Notes.AutoScroll = true;
            this.metroPanel_Notes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_Notes.HorizontalScrollbar = true;
            this.metroPanel_Notes.HorizontalScrollbarBarColor = false;
            this.metroPanel_Notes.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Notes.HorizontalScrollbarSize = 10;
            this.metroPanel_Notes.Location = new System.Drawing.Point(0, 70);
            this.metroPanel_Notes.Name = "metroPanel_Notes";
            this.metroPanel_Notes.Size = new System.Drawing.Size(579, 367);
            this.metroPanel_Notes.TabIndex = 9;
            this.metroPanel_Notes.VerticalScrollbar = true;
            this.metroPanel_Notes.VerticalScrollbarBarColor = true;
            this.metroPanel_Notes.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Notes.VerticalScrollbarSize = 10;
            // 
            // metroPanel_Notes_Header
            // 
            this.metroPanel_Notes_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel_Notes_Header.HorizontalScrollbarBarColor = true;
            this.metroPanel_Notes_Header.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Notes_Header.HorizontalScrollbarSize = 10;
            this.metroPanel_Notes_Header.Location = new System.Drawing.Point(0, 0);
            this.metroPanel_Notes_Header.Name = "metroPanel_Notes_Header";
            this.metroPanel_Notes_Header.Size = new System.Drawing.Size(579, 70);
            this.metroPanel_Notes_Header.TabIndex = 6;
            this.metroPanel_Notes_Header.VerticalScrollbarBarColor = true;
            this.metroPanel_Notes_Header.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Notes_Header.VerticalScrollbarSize = 10;
            // 
            // metroPanel_CAR_header
            // 
            this.metroPanel_CAR_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel_CAR_header.HorizontalScrollbarBarColor = true;
            this.metroPanel_CAR_header.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_CAR_header.HorizontalScrollbarSize = 10;
            this.metroPanel_CAR_header.Location = new System.Drawing.Point(0, 0);
            this.metroPanel_CAR_header.Name = "metroPanel_CAR_header";
            this.metroPanel_CAR_header.Size = new System.Drawing.Size(741, 78);
            this.metroPanel_CAR_header.TabIndex = 11;
            this.metroPanel_CAR_header.VerticalScrollbarBarColor = true;
            this.metroPanel_CAR_header.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_CAR_header.VerticalScrollbarSize = 10;
            // 
            // metroPanel_CAR
            // 
            this.metroPanel_CAR.AutoScroll = true;
            this.metroPanel_CAR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_CAR.HorizontalScrollbar = true;
            this.metroPanel_CAR.HorizontalScrollbarBarColor = false;
            this.metroPanel_CAR.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_CAR.HorizontalScrollbarSize = 0;
            this.metroPanel_CAR.Location = new System.Drawing.Point(0, 78);
            this.metroPanel_CAR.Name = "metroPanel_CAR";
            this.metroPanel_CAR.Size = new System.Drawing.Size(741, 359);
            this.metroPanel_CAR.TabIndex = 12;
            this.metroPanel_CAR.VerticalScrollbar = true;
            this.metroPanel_CAR.VerticalScrollbarBarColor = true;
            this.metroPanel_CAR.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_CAR.VerticalScrollbarSize = 15;
            // 
            // metroPanel_Car_History
            // 
            this.metroPanel_Car_History.AutoScroll = true;
            this.metroPanel_Car_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_Car_History.HorizontalScrollbar = true;
            this.metroPanel_Car_History.HorizontalScrollbarBarColor = false;
            this.metroPanel_Car_History.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Car_History.HorizontalScrollbarSize = 10;
            this.metroPanel_Car_History.Location = new System.Drawing.Point(0, 0);
            this.metroPanel_Car_History.Name = "metroPanel_Car_History";
            this.metroPanel_Car_History.Size = new System.Drawing.Size(129, 437);
            this.metroPanel_Car_History.TabIndex = 9;
            this.metroPanel_Car_History.VerticalScrollbar = true;
            this.metroPanel_Car_History.VerticalScrollbarBarColor = true;
            this.metroPanel_Car_History.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Car_History.VerticalScrollbarSize = 10;
            // 
            // frmClientAdviceRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 525);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmClientAdviceRecord";
            this.Text = "Form2";
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Finx.App.UserControls.xToolBarMenu xToolBarMenu1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;        
        private MetroFramework.Controls.MetroPanel metroPanel_Notes_Header;
        private MetroFramework.Controls.MetroPanel metroPanel_Notes_History;
        private MetroFramework.Controls.MetroPanel metroPanel_Notes;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private MetroFramework.Controls.MetroPanel metroPanel_CAR;
        private MetroFramework.Controls.MetroPanel metroPanel_CAR_header;
        private MetroFramework.Controls.MetroPanel metroPanel_Car_History;
    }
}