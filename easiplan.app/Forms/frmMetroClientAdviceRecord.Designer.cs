namespace Finx.App.Forms
{
    partial class frmMetroClientAdviceRecord
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGrid_Notes = new SourceGrid.DataGrid();
            this.metroPanel_PolicyNote = new MetroFramework.Controls.MetroPanel();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.metroLabel21 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel22 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel24 = new MetroFramework.Controls.MetroLabel();
            this.metroButton9 = new MetroFramework.Controls.MetroButton();
            this.metroButton10 = new MetroFramework.Controls.MetroButton();
            this.metroButton12 = new MetroFramework.Controls.MetroButton();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.metroLabel15 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel16 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel17 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel19 = new MetroFramework.Controls.MetroLabel();
            this.metroButton5 = new MetroFramework.Controls.MetroButton();
            this.metroButton6 = new MetroFramework.Controls.MetroButton();
            this.metroButton7 = new MetroFramework.Controls.MetroButton();
            this.metroButton8 = new MetroFramework.Controls.MetroButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroLabel14 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel13 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel12 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroButton4 = new MetroFramework.Controls.MetroButton();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_Select = new MetroFramework.Controls.MetroPanel();
            this.metroLabel18 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel28 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel29 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel30 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel31 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel32 = new MetroFramework.Controls.MetroLabel();
            this.metroButton14 = new MetroFramework.Controls.MetroButton();
            this.metroButton15 = new MetroFramework.Controls.MetroButton();
            this.metroButton16 = new MetroFramework.Controls.MetroButton();
            this.metroButton17 = new MetroFramework.Controls.MetroButton();
            this.metroButton18 = new MetroFramework.Controls.MetroButton();
            this.metroButton19 = new MetroFramework.Controls.MetroButton();
            this.xInput_ShowCompletedTasks = new Finx.App.UserControls.xInput();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.metroPanel_PolicyNote.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(27, 131);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGrid_Notes);
            this.splitContainer1.Panel1.Controls.Add(this.xInput_ShowCompletedTasks);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.metroPanel_PolicyNote);
            this.splitContainer1.Panel2.Controls.Add(this.metroPanel_Select);
            this.splitContainer1.Size = new System.Drawing.Size(1685, 931);
            this.splitContainer1.SplitterDistance = 307;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 7;
            // 
            // dataGrid_Notes
            // 
            this.dataGrid_Notes.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_Notes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_Notes.EnableSort = false;
            this.dataGrid_Notes.FixedRows = 1;
            this.dataGrid_Notes.Location = new System.Drawing.Point(0, 37);
            this.dataGrid_Notes.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid_Notes.Name = "dataGrid_Notes";
            this.dataGrid_Notes.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_Notes.Size = new System.Drawing.Size(307, 894);
            this.dataGrid_Notes.TabIndex = 7;
            this.dataGrid_Notes.TabStop = true;
            this.dataGrid_Notes.ToolTipText = "";
            // 
            // metroPanel_PolicyNote
            // 
            this.metroPanel_PolicyNote.Controls.Add(this.metroPanel3);
            this.metroPanel_PolicyNote.Controls.Add(this.metroPanel2);
            this.metroPanel_PolicyNote.Controls.Add(this.metroPanel1);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel9);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel8);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel7);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel6);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel5);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel4);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel3);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel2);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel1);
            this.metroPanel_PolicyNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_PolicyNote.HorizontalScrollbarBarColor = true;
            this.metroPanel_PolicyNote.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_PolicyNote.HorizontalScrollbarSize = 12;
            this.metroPanel_PolicyNote.Location = new System.Drawing.Point(0, 74);
            this.metroPanel_PolicyNote.Margin = new System.Windows.Forms.Padding(4);
            this.metroPanel_PolicyNote.Name = "metroPanel_PolicyNote";
            this.metroPanel_PolicyNote.Size = new System.Drawing.Size(1373, 857);
            this.metroPanel_PolicyNote.TabIndex = 9;
            this.metroPanel_PolicyNote.VerticalScrollbarBarColor = true;
            this.metroPanel_PolicyNote.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_PolicyNote.VerticalScrollbarSize = 13;
            this.metroPanel_PolicyNote.Paint += new System.Windows.Forms.PaintEventHandler(this.metroPanel_PolicyNote_Paint);
            // 
            // metroPanel3
            // 
            this.metroPanel3.Controls.Add(this.metroButton12);
            this.metroPanel3.Controls.Add(this.metroLabel21);
            this.metroPanel3.Controls.Add(this.metroLabel22);
            this.metroPanel3.Controls.Add(this.metroLabel24);
            this.metroPanel3.Controls.Add(this.metroButton9);
            this.metroPanel3.Controls.Add(this.metroButton10);
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(65, 480);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(606, 93);
            this.metroPanel3.TabIndex = 17;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // metroLabel21
            // 
            this.metroLabel21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel21.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel21.Location = new System.Drawing.Point(400, 0);
            this.metroLabel21.Name = "metroLabel21";
            this.metroLabel21.Size = new System.Drawing.Size(203, 45);
            this.metroLabel21.TabIndex = 17;
            this.metroLabel21.Text = "Do not require access to \r\ncapital for 5 years";
            this.metroLabel21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel21.UseCustomBackColor = true;
            // 
            // metroLabel22
            // 
            this.metroLabel22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel22.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel22.Location = new System.Drawing.Point(200, 0);
            this.metroLabel22.Name = "metroLabel22";
            this.metroLabel22.Size = new System.Drawing.Size(203, 45);
            this.metroLabel22.TabIndex = 17;
            this.metroLabel22.Text = "Always require access to\r\ncapital";
            this.metroLabel22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel22.UseCustomBackColor = true;
            // 
            // metroLabel24
            // 
            this.metroLabel24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel24.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel24.Location = new System.Drawing.Point(0, 0);
            this.metroLabel24.Name = "metroLabel24";
            this.metroLabel24.Size = new System.Drawing.Size(203, 45);
            this.metroLabel24.TabIndex = 16;
            this.metroLabel24.Text = "Need to draw an Income";
            this.metroLabel24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel24.UseCustomBackColor = true;
            // 
            // metroButton9
            // 
            this.metroButton9.BackColor = System.Drawing.Color.White;
            this.metroButton9.ForeColor = System.Drawing.Color.Black;
            this.metroButton9.Location = new System.Drawing.Point(200, 47);
            this.metroButton9.Name = "metroButton9";
            this.metroButton9.Size = new System.Drawing.Size(203, 45);
            this.metroButton9.TabIndex = 2;
            this.metroButton9.UseCustomBackColor = true;
            this.metroButton9.UseCustomForeColor = true;
            this.metroButton9.UseSelectable = true;
            // 
            // metroButton10
            // 
            this.metroButton10.BackColor = System.Drawing.Color.White;
            this.metroButton10.ForeColor = System.Drawing.Color.Black;
            this.metroButton10.Location = new System.Drawing.Point(0, 47);
            this.metroButton10.Name = "metroButton10";
            this.metroButton10.Size = new System.Drawing.Size(203, 45);
            this.metroButton10.TabIndex = 5;
            this.metroButton10.UseCustomBackColor = true;
            this.metroButton10.UseCustomForeColor = true;
            this.metroButton10.UseSelectable = true;
            // 
            // metroButton12
            // 
            this.metroButton12.BackColor = System.Drawing.Color.White;
            this.metroButton12.ForeColor = System.Drawing.Color.Black;
            this.metroButton12.Location = new System.Drawing.Point(400, 47);
            this.metroButton12.Name = "metroButton12";
            this.metroButton12.Size = new System.Drawing.Size(203, 45);
            this.metroButton12.TabIndex = 3;
            this.metroButton12.UseCustomBackColor = true;
            this.metroButton12.UseCustomForeColor = true;
            this.metroButton12.UseSelectable = true;
            // 
            // metroPanel2
            // 
            this.metroPanel2.Controls.Add(this.metroButton14);
            this.metroPanel2.Controls.Add(this.metroButton15);
            this.metroPanel2.Controls.Add(this.metroButton16);
            this.metroPanel2.Controls.Add(this.metroButton17);
            this.metroPanel2.Controls.Add(this.metroButton18);
            this.metroPanel2.Controls.Add(this.metroButton7);
            this.metroPanel2.Controls.Add(this.metroButton19);
            this.metroPanel2.Controls.Add(this.metroLabel18);
            this.metroPanel2.Controls.Add(this.metroLabel28);
            this.metroPanel2.Controls.Add(this.metroLabel29);
            this.metroPanel2.Controls.Add(this.metroLabel30);
            this.metroPanel2.Controls.Add(this.metroLabel31);
            this.metroPanel2.Controls.Add(this.metroLabel32);
            this.metroPanel2.Controls.Add(this.metroLabel15);
            this.metroPanel2.Controls.Add(this.metroLabel16);
            this.metroPanel2.Controls.Add(this.metroLabel17);
            this.metroPanel2.Controls.Add(this.metroLabel19);
            this.metroPanel2.Controls.Add(this.metroButton5);
            this.metroPanel2.Controls.Add(this.metroButton6);
            this.metroPanel2.Controls.Add(this.metroButton8);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(72, 126);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(825, 84);
            this.metroPanel2.TabIndex = 16;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // metroLabel15
            // 
            this.metroLabel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel15.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel15.Location = new System.Drawing.Point(247, 0);
            this.metroLabel15.Name = "metroLabel15";
            this.metroLabel15.Size = new System.Drawing.Size(85, 41);
            this.metroLabel15.TabIndex = 17;
            this.metroLabel15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel15.UseCustomBackColor = true;
            // 
            // metroLabel16
            // 
            this.metroLabel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel16.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel16.Location = new System.Drawing.Point(165, 0);
            this.metroLabel16.Name = "metroLabel16";
            this.metroLabel16.Size = new System.Drawing.Size(85, 41);
            this.metroLabel16.TabIndex = 17;
            this.metroLabel16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel16.UseCustomBackColor = true;
            // 
            // metroLabel17
            // 
            this.metroLabel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel17.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel17.Location = new System.Drawing.Point(81, 0);
            this.metroLabel17.Name = "metroLabel17";
            this.metroLabel17.Size = new System.Drawing.Size(85, 41);
            this.metroLabel17.TabIndex = 17;
            this.metroLabel17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel17.UseCustomBackColor = true;
            // 
            // metroLabel19
            // 
            this.metroLabel19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel19.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel19.Location = new System.Drawing.Point(0, 0);
            this.metroLabel19.Name = "metroLabel19";
            this.metroLabel19.Size = new System.Drawing.Size(85, 41);
            this.metroLabel19.TabIndex = 16;
            this.metroLabel19.Text = "1\r\nLow";
            this.metroLabel19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel19.UseCustomBackColor = true;
            // 
            // metroButton5
            // 
            this.metroButton5.BackColor = System.Drawing.Color.White;
            this.metroButton5.ForeColor = System.Drawing.Color.Black;
            this.metroButton5.Location = new System.Drawing.Point(81, 43);
            this.metroButton5.Name = "metroButton5";
            this.metroButton5.Size = new System.Drawing.Size(85, 41);
            this.metroButton5.TabIndex = 2;
            this.metroButton5.UseCustomBackColor = true;
            this.metroButton5.UseCustomForeColor = true;
            this.metroButton5.UseSelectable = true;
            // 
            // metroButton6
            // 
            this.metroButton6.BackColor = System.Drawing.Color.White;
            this.metroButton6.ForeColor = System.Drawing.Color.Black;
            this.metroButton6.Location = new System.Drawing.Point(0, 43);
            this.metroButton6.Name = "metroButton6";
            this.metroButton6.Size = new System.Drawing.Size(85, 41);
            this.metroButton6.TabIndex = 5;
            this.metroButton6.UseCustomBackColor = true;
            this.metroButton6.UseCustomForeColor = true;
            this.metroButton6.UseSelectable = true;
            // 
            // metroButton7
            // 
            this.metroButton7.BackColor = System.Drawing.Color.White;
            this.metroButton7.ForeColor = System.Drawing.Color.Black;
            this.metroButton7.Location = new System.Drawing.Point(329, 43);
            this.metroButton7.Name = "metroButton7";
            this.metroButton7.Size = new System.Drawing.Size(85, 41);
            this.metroButton7.TabIndex = 4;
            this.metroButton7.UseCustomBackColor = true;
            this.metroButton7.UseCustomForeColor = true;
            this.metroButton7.UseSelectable = true;
            // 
            // metroButton8
            // 
            this.metroButton8.BackColor = System.Drawing.Color.White;
            this.metroButton8.ForeColor = System.Drawing.Color.Black;
            this.metroButton8.Location = new System.Drawing.Point(165, 43);
            this.metroButton8.Name = "metroButton8";
            this.metroButton8.Size = new System.Drawing.Size(85, 41);
            this.metroButton8.TabIndex = 3;
            this.metroButton8.UseCustomBackColor = true;
            this.metroButton8.UseCustomForeColor = true;
            this.metroButton8.UseSelectable = true;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroButton2);
            this.metroPanel1.Controls.Add(this.metroLabel14);
            this.metroPanel1.Controls.Add(this.metroLabel13);
            this.metroPanel1.Controls.Add(this.metroLabel12);
            this.metroPanel1.Controls.Add(this.metroButton1);
            this.metroPanel1.Controls.Add(this.metroLabel10);
            this.metroPanel1.Controls.Add(this.metroButton3);
            this.metroPanel1.Controls.Add(this.metroButton4);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(67, 302);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(334, 85);
            this.metroPanel1.TabIndex = 15;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroLabel14
            // 
            this.metroLabel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel14.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel14.Location = new System.Drawing.Point(247, 0);
            this.metroLabel14.Name = "metroLabel14";
            this.metroLabel14.Size = new System.Drawing.Size(85, 41);
            this.metroLabel14.TabIndex = 17;
            this.metroLabel14.Text = "10\r\nYears +";
            this.metroLabel14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel14.UseCustomBackColor = true;
            // 
            // metroLabel13
            // 
            this.metroLabel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel13.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel13.Location = new System.Drawing.Point(165, 0);
            this.metroLabel13.Name = "metroLabel13";
            this.metroLabel13.Size = new System.Drawing.Size(85, 41);
            this.metroLabel13.TabIndex = 17;
            this.metroLabel13.Text = "5-9\r\nYears";
            this.metroLabel13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel13.UseCustomBackColor = true;
            // 
            // metroLabel12
            // 
            this.metroLabel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel12.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel12.Location = new System.Drawing.Point(81, 0);
            this.metroLabel12.Name = "metroLabel12";
            this.metroLabel12.Size = new System.Drawing.Size(85, 41);
            this.metroLabel12.TabIndex = 17;
            this.metroLabel12.Text = "2-5\r\nYears";
            this.metroLabel12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel12.UseCustomBackColor = true;
            // 
            // metroLabel10
            // 
            this.metroLabel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel10.Location = new System.Drawing.Point(0, 0);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(85, 41);
            this.metroLabel10.TabIndex = 16;
            this.metroLabel10.Text = "0-2\r\nYears";
            this.metroLabel10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel10.UseCustomBackColor = true;
            // 
            // metroButton1
            // 
            this.metroButton1.BackColor = System.Drawing.Color.White;
            this.metroButton1.ForeColor = System.Drawing.Color.Black;
            this.metroButton1.Location = new System.Drawing.Point(247, 43);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(85, 41);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.UseCustomBackColor = true;
            this.metroButton1.UseCustomForeColor = true;
            this.metroButton1.UseSelectable = true;
            // 
            // metroButton4
            // 
            this.metroButton4.BackColor = System.Drawing.Color.White;
            this.metroButton4.ForeColor = System.Drawing.Color.Black;
            this.metroButton4.Location = new System.Drawing.Point(163, 43);
            this.metroButton4.Name = "metroButton4";
            this.metroButton4.Size = new System.Drawing.Size(85, 41);
            this.metroButton4.TabIndex = 5;
            this.metroButton4.UseCustomBackColor = true;
            this.metroButton4.UseCustomForeColor = true;
            this.metroButton4.UseSelectable = true;
            // 
            // metroButton3
            // 
            this.metroButton3.BackColor = System.Drawing.Color.White;
            this.metroButton3.ForeColor = System.Drawing.Color.Black;
            this.metroButton3.Location = new System.Drawing.Point(0, 43);
            this.metroButton3.Name = "metroButton3";
            this.metroButton3.Size = new System.Drawing.Size(85, 41);
            this.metroButton3.TabIndex = 4;
            this.metroButton3.UseCustomBackColor = true;
            this.metroButton3.UseCustomForeColor = true;
            this.metroButton3.UseSelectable = true;
            // 
            // metroButton2
            // 
            this.metroButton2.BackColor = System.Drawing.Color.White;
            this.metroButton2.ForeColor = System.Drawing.Color.Black;
            this.metroButton2.Location = new System.Drawing.Point(81, 43);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(85, 41);
            this.metroButton2.TabIndex = 3;
            this.metroButton2.UseCustomBackColor = true;
            this.metroButton2.UseCustomForeColor = true;
            this.metroButton2.UseSelectable = true;
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.ForeColor = System.Drawing.Color.DarkGray;
            this.metroLabel9.Location = new System.Drawing.Point(65, 635);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(665, 20);
            this.metroLabel9.TabIndex = 14;
            this.metroLabel9.Text = "(What are the client\'s needs and what does the client wish to achieve by purchasi" +
    "ng this financial product?)";
            this.metroLabel9.UseCustomForeColor = true;
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(65, 615);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(149, 20);
            this.metroLabel8.TabIndex = 13;
            this.metroLabel8.Text = "Needs and Objectives:";
            this.metroLabel8.Click += new System.EventHandler(this.metroLabel8_Click);
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.ForeColor = System.Drawing.Color.DarkGray;
            this.metroLabel7.Location = new System.Drawing.Point(67, 457);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(314, 20);
            this.metroLabel7.TabIndex = 12;
            this.metroLabel7.Text = "(The accessibility and liquidity of the invesement.)";
            this.metroLabel7.UseCustomForeColor = true;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(67, 437);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(116, 20);
            this.metroLabel6.TabIndex = 11;
            this.metroLabel6.Text = "Access to Capital:";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.ForeColor = System.Drawing.Color.DarkGray;
            this.metroLabel5.Location = new System.Drawing.Point(69, 279);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(634, 20);
            this.metroLabel5.TabIndex = 10;
            this.metroLabel5.Text = "(For how long the client expects his/her money to be invested before he/she would" +
    " like to cash it in?)";
            this.metroLabel5.UseCustomForeColor = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(67, 259);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(132, 20);
            this.metroLabel4.TabIndex = 9;
            this.metroLabel4.Text = "Investment Horison:";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.ForeColor = System.Drawing.Color.DarkGray;
            this.metroLabel3.Location = new System.Drawing.Point(69, 103);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(526, 20);
            this.metroLabel3.TabIndex = 8;
            this.metroLabel3.Text = "(Describe the client\'s level of knowledge and experience of the product purchased" +
    ".)";
            this.metroLabel3.UseCustomForeColor = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(69, 83);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(233, 20);
            this.metroLabel2.TabIndex = 7;
            this.metroLabel2.Text = "Product Knowledge and Experience:";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Large;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(527, 24);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(68, 20);
            this.metroLabel1.TabIndex = 6;
            this.metroLabel1.Text = "Summary";
            this.metroLabel1.Click += new System.EventHandler(this.metroLabel1_Click);
            // 
            // metroPanel_Select
            // 
            this.metroPanel_Select.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel_Select.HorizontalScrollbarBarColor = true;
            this.metroPanel_Select.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Select.HorizontalScrollbarSize = 12;
            this.metroPanel_Select.Location = new System.Drawing.Point(0, 0);
            this.metroPanel_Select.Margin = new System.Windows.Forms.Padding(4);
            this.metroPanel_Select.Name = "metroPanel_Select";
            this.metroPanel_Select.Size = new System.Drawing.Size(1373, 74);
            this.metroPanel_Select.TabIndex = 8;
            this.metroPanel_Select.VerticalScrollbarBarColor = true;
            this.metroPanel_Select.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Select.VerticalScrollbarSize = 13;
            // 
            // metroLabel18
            // 
            this.metroLabel18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel18.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel18.Location = new System.Drawing.Point(737, 0);
            this.metroLabel18.Name = "metroLabel18";
            this.metroLabel18.Size = new System.Drawing.Size(85, 41);
            this.metroLabel18.TabIndex = 18;
            this.metroLabel18.Text = "10\r\nHigh";
            this.metroLabel18.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel18.UseCustomBackColor = true;
            // 
            // metroLabel28
            // 
            this.metroLabel28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel28.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel28.Location = new System.Drawing.Point(655, 0);
            this.metroLabel28.Name = "metroLabel28";
            this.metroLabel28.Size = new System.Drawing.Size(85, 41);
            this.metroLabel28.TabIndex = 19;
            this.metroLabel28.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel28.UseCustomBackColor = true;
            // 
            // metroLabel29
            // 
            this.metroLabel29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel29.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel29.Location = new System.Drawing.Point(573, 0);
            this.metroLabel29.Name = "metroLabel29";
            this.metroLabel29.Size = new System.Drawing.Size(85, 41);
            this.metroLabel29.TabIndex = 20;
            this.metroLabel29.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel29.UseCustomBackColor = true;
            // 
            // metroLabel30
            // 
            this.metroLabel30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel30.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel30.Location = new System.Drawing.Point(492, 0);
            this.metroLabel30.Name = "metroLabel30";
            this.metroLabel30.Size = new System.Drawing.Size(85, 41);
            this.metroLabel30.TabIndex = 21;
            this.metroLabel30.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel30.UseCustomBackColor = true;
            // 
            // metroLabel31
            // 
            this.metroLabel31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel31.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel31.Location = new System.Drawing.Point(411, 0);
            this.metroLabel31.Name = "metroLabel31";
            this.metroLabel31.Size = new System.Drawing.Size(85, 41);
            this.metroLabel31.TabIndex = 22;
            this.metroLabel31.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel31.UseCustomBackColor = true;
            // 
            // metroLabel32
            // 
            this.metroLabel32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroLabel32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel32.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel32.Location = new System.Drawing.Point(329, 0);
            this.metroLabel32.Name = "metroLabel32";
            this.metroLabel32.Size = new System.Drawing.Size(85, 41);
            this.metroLabel32.TabIndex = 23;
            this.metroLabel32.Text = "5\r\nModerate";
            this.metroLabel32.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel32.UseCustomBackColor = true;
            // 
            // metroButton14
            // 
            this.metroButton14.BackColor = System.Drawing.Color.White;
            this.metroButton14.ForeColor = System.Drawing.Color.Black;
            this.metroButton14.Location = new System.Drawing.Point(737, 43);
            this.metroButton14.Name = "metroButton14";
            this.metroButton14.Size = new System.Drawing.Size(85, 41);
            this.metroButton14.TabIndex = 19;
            this.metroButton14.UseCustomBackColor = true;
            this.metroButton14.UseCustomForeColor = true;
            this.metroButton14.UseSelectable = true;
            // 
            // metroButton15
            // 
            this.metroButton15.BackColor = System.Drawing.Color.White;
            this.metroButton15.ForeColor = System.Drawing.Color.Black;
            this.metroButton15.Location = new System.Drawing.Point(655, 43);
            this.metroButton15.Name = "metroButton15";
            this.metroButton15.Size = new System.Drawing.Size(85, 41);
            this.metroButton15.TabIndex = 20;
            this.metroButton15.UseCustomBackColor = true;
            this.metroButton15.UseCustomForeColor = true;
            this.metroButton15.UseSelectable = true;
            // 
            // metroButton16
            // 
            this.metroButton16.BackColor = System.Drawing.Color.White;
            this.metroButton16.ForeColor = System.Drawing.Color.Black;
            this.metroButton16.Location = new System.Drawing.Point(572, 43);
            this.metroButton16.Name = "metroButton16";
            this.metroButton16.Size = new System.Drawing.Size(85, 41);
            this.metroButton16.TabIndex = 21;
            this.metroButton16.UseCustomBackColor = true;
            this.metroButton16.UseCustomForeColor = true;
            this.metroButton16.UseSelectable = true;
            // 
            // metroButton17
            // 
            this.metroButton17.BackColor = System.Drawing.Color.White;
            this.metroButton17.ForeColor = System.Drawing.Color.Black;
            this.metroButton17.Location = new System.Drawing.Point(492, 43);
            this.metroButton17.Name = "metroButton17";
            this.metroButton17.Size = new System.Drawing.Size(85, 41);
            this.metroButton17.TabIndex = 22;
            this.metroButton17.UseCustomBackColor = true;
            this.metroButton17.UseCustomForeColor = true;
            this.metroButton17.UseSelectable = true;
            // 
            // metroButton18
            // 
            this.metroButton18.BackColor = System.Drawing.Color.White;
            this.metroButton18.ForeColor = System.Drawing.Color.Black;
            this.metroButton18.Location = new System.Drawing.Point(411, 43);
            this.metroButton18.Name = "metroButton18";
            this.metroButton18.Size = new System.Drawing.Size(85, 41);
            this.metroButton18.TabIndex = 23;
            this.metroButton18.UseCustomBackColor = true;
            this.metroButton18.UseCustomForeColor = true;
            this.metroButton18.UseSelectable = true;
            // 
            // metroButton19
            // 
            this.metroButton19.BackColor = System.Drawing.Color.White;
            this.metroButton19.ForeColor = System.Drawing.Color.Black;
            this.metroButton19.Location = new System.Drawing.Point(247, 43);
            this.metroButton19.Name = "metroButton19";
            this.metroButton19.Size = new System.Drawing.Size(85, 41);
            this.metroButton19.TabIndex = 24;
            this.metroButton19.UseCustomBackColor = true;
            this.metroButton19.UseCustomForeColor = true;
            this.metroButton19.UseSelectable = true;
            // 
            // xInput_ShowCompletedTasks
            // 
            this.xInput_ShowCompletedTasks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xInput_ShowCompletedTasks.BackColor = System.Drawing.Color.Transparent;
            this.xInput_ShowCompletedTasks.ControlTypes = Finx.App.UserControls.ControlTypes.CheckBox;
            this.xInput_ShowCompletedTasks.ControlWidth = 40;
            this.xInput_ShowCompletedTasks.DataSource = null;
            this.xInput_ShowCompletedTasks.DisplayMember = "Text";
            this.xInput_ShowCompletedTasks.Dock = System.Windows.Forms.DockStyle.Top;
            this.xInput_ShowCompletedTasks.LablePosition = Finx.App.UserControls.LablePosition.Left;
            this.xInput_ShowCompletedTasks.LableText = "Show Completed Notes";
            this.xInput_ShowCompletedTasks.Location = new System.Drawing.Point(0, 0);
            this.xInput_ShowCompletedTasks.MappedField = null;
            this.xInput_ShowCompletedTasks.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_ShowCompletedTasks.MinimumSize = new System.Drawing.Size(67, 37);
            this.xInput_ShowCompletedTasks.Model = null;
            this.xInput_ShowCompletedTasks.Name = "xInput_ShowCompletedTasks";
            this.xInput_ShowCompletedTasks.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_ShowCompletedTasks.ReadOnly = false;
            this.xInput_ShowCompletedTasks.Size = new System.Drawing.Size(307, 37);
            this.xInput_ShowCompletedTasks.TabIndex = 6;
            this.xInput_ShowCompletedTasks.Value = null;
            this.xInput_ShowCompletedTasks.ValueMember = "Value";
            // 
            // xToolBarMenu1
            // 
            this.xToolBarMenu1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.xToolBarMenu1.BackColor = System.Drawing.Color.White;
            this.xToolBarMenu1.CausesValidation = false;
            this.xToolBarMenu1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.xToolBarMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xToolBarMenu1.Location = new System.Drawing.Point(27, 74);
            this.xToolBarMenu1.Margin = new System.Windows.Forms.Padding(4);
            this.xToolBarMenu1.Name = "xToolBarMenu1";
            this.xToolBarMenu1.Size = new System.Drawing.Size(1685, 57);
            this.xToolBarMenu1.TabIndex = 2;
            // 
            // frmMetroClientAdviceRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1739, 1087);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.xToolBarMenu1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMetroClientAdviceRecord";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Text = "frmMetroClientAdviceRecord";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.metroPanel_PolicyNote.ResumeLayout(false);
            this.metroPanel_PolicyNote.PerformLayout();
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroPanel metroPanel_PolicyNote;
        private MetroFramework.Controls.MetroPanel metroPanel_Select;
        private UserControls.xInput xInput_ShowCompletedTasks;
        private SourceGrid.DataGrid dataGrid_Notes;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton metroButton4;
        private MetroFramework.Controls.MetroButton metroButton3;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private MetroFramework.Controls.MetroLabel metroLabel21;
        private MetroFramework.Controls.MetroLabel metroLabel22;
        private MetroFramework.Controls.MetroLabel metroLabel24;
        private MetroFramework.Controls.MetroButton metroButton9;
        private MetroFramework.Controls.MetroButton metroButton10;
        private MetroFramework.Controls.MetroButton metroButton12;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroLabel metroLabel15;
        private MetroFramework.Controls.MetroLabel metroLabel16;
        private MetroFramework.Controls.MetroLabel metroLabel17;
        private MetroFramework.Controls.MetroLabel metroLabel19;
        private MetroFramework.Controls.MetroButton metroButton5;
        private MetroFramework.Controls.MetroButton metroButton6;
        private MetroFramework.Controls.MetroButton metroButton7;
        private MetroFramework.Controls.MetroButton metroButton8;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel metroLabel14;
        private MetroFramework.Controls.MetroLabel metroLabel13;
        private MetroFramework.Controls.MetroLabel metroLabel12;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroButton metroButton14;
        private MetroFramework.Controls.MetroButton metroButton15;
        private MetroFramework.Controls.MetroButton metroButton16;
        private MetroFramework.Controls.MetroButton metroButton17;
        private MetroFramework.Controls.MetroButton metroButton18;
        private MetroFramework.Controls.MetroButton metroButton19;
        private MetroFramework.Controls.MetroLabel metroLabel18;
        private MetroFramework.Controls.MetroLabel metroLabel28;
        private MetroFramework.Controls.MetroLabel metroLabel29;
        private MetroFramework.Controls.MetroLabel metroLabel30;
        private MetroFramework.Controls.MetroLabel metroLabel31;
        private MetroFramework.Controls.MetroLabel metroLabel32;
    }
}