namespace Finx.App.UserControls
{
    partial class xToolBarMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbStrip = new Finx.App.UserControls.ToolStripEx();
            this.tbCaptionImage = new System.Windows.Forms.ToolStripButton();
            this.tbCaption = new System.Windows.Forms.ToolStripLabel();
            this.tbClose = new System.Windows.Forms.ToolStripButton();
            this.tbSave = new System.Windows.Forms.ToolStripButton();
            this.tbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tbEdit = new System.Windows.Forms.ToolStripButton();

            //Note dropdown creation 
            //this.tbNewNote = new System.Windows.Forms.ToolStripDropDownButton();


            this.tbNotes = new System.Windows.Forms.ToolStripButton();
            this.tbStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbStrip
            // 
            this.tbStrip.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbStrip.ClickThrough = false;
            this.tbStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tbStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbCaptionImage,
            this.tbCaption,
            this.tbClose,
            this.tbSave,
            this.tbRefresh,
            this.tbEdit,
            //this.tbNewNote,
            this.tbNotes});
            this.tbStrip.Location = new System.Drawing.Point(0, 0);
            this.tbStrip.Name = "tbStrip";
            this.tbStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tbStrip.Size = new System.Drawing.Size(650, 46);
            this.tbStrip.Stretch = true;
            this.tbStrip.TabIndex = 4;
            this.tbStrip.Text = "toolStrip1";
            // 
            // tbCaptionImage
            // 
            this.tbCaptionImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbCaptionImage.Image = global::easiplan.app.Properties.Resources.person_b_42;
            this.tbCaptionImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbCaptionImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbCaptionImage.Margin = new System.Windows.Forms.Padding(0);
            this.tbCaptionImage.Name = "tbCaptionImage";
            this.tbCaptionImage.Size = new System.Drawing.Size(46, 46);
            this.tbCaptionImage.Text = "toolStripButton1";
            // 
            // tbCaption
            // 
            this.tbCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.tbCaption.Name = "tbCaption";
            this.tbCaption.Size = new System.Drawing.Size(70, 43);
            this.tbCaption.Text = "Caption";
            // 
            // tbClose
            // 
            this.tbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbClose.Image = global::easiplan.app.Properties.Resources.exit_b_42;
            this.tbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbClose.Margin = new System.Windows.Forms.Padding(0);
            this.tbClose.Name = "tbClose";
            this.tbClose.Padding = new System.Windows.Forms.Padding(5);
            this.tbClose.Size = new System.Drawing.Size(99, 46);
            this.tbClose.Text = "Close";
            this.tbClose.Click += new System.EventHandler(this.tbClose_Click);
            // 
            // tbSave
            // 
            this.tbSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbSave.Image = global::easiplan.app.Properties.Resources.database_b_42;
            this.tbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbSave.ImageTransparentColor = System.Drawing.Color.Honeydew;
            this.tbSave.Margin = new System.Windows.Forms.Padding(0);
            this.tbSave.Name = "tbSave";
            this.tbSave.Padding = new System.Windows.Forms.Padding(5);
            this.tbSave.Size = new System.Drawing.Size(96, 46);
            this.tbSave.Text = "Save";
            this.tbSave.ToolTipText = "Open form for edit.";
            this.tbSave.Click += new System.EventHandler(this.tbSave_Click);
            // 
            // tbRefresh
            // 
            this.tbRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbRefresh.Image = global::easiplan.app.Properties.Resources.refresh_b_42;
            this.tbRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbRefresh.ImageTransparentColor = System.Drawing.Color.Honeydew;
            this.tbRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.tbRefresh.Name = "tbRefresh";
            this.tbRefresh.Padding = new System.Windows.Forms.Padding(5);
            this.tbRefresh.Size = new System.Drawing.Size(114, 46);
            this.tbRefresh.Text = "Refresh";
            this.tbRefresh.ToolTipText = "Open form for edit.";
            this.tbRefresh.Click += new System.EventHandler(this.tbRefresh_Click);
            // 
            // tbEdit
            // 
            this.tbEdit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbEdit.Image = global::easiplan.app.Properties.Resources.form_b_42;
            this.tbEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbEdit.ImageTransparentColor = System.Drawing.Color.Gainsboro;
            this.tbEdit.Margin = new System.Windows.Forms.Padding(0);
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Padding = new System.Windows.Forms.Padding(5);
            this.tbEdit.Size = new System.Drawing.Size(88, 46);
            this.tbEdit.Text = "Edit";
            this.tbEdit.ToolTipText = "Open form for edit.";
            this.tbEdit.Click += new System.EventHandler(this.tbEdit_Click);
            // 
           
            /*
            // tbNewNote //Note button for drop down if required
            // 
            this.tbNewNote.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbNewNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbNewNote.Image = global::easiplan.app.Properties.Resources.form_b_42;
            this.tbNewNote.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbNewNote.ImageTransparentColor = System.Drawing.Color.Gainsboro;
            this.tbNewNote.Margin = new System.Windows.Forms.Padding(0);
            this.tbNewNote.Name = "tbNewNote";
            this.tbNewNote.Padding = new System.Windows.Forms.Padding(5);
            this.tbNewNote.Size = new System.Drawing.Size(88, 46);
            this.tbNewNote.Text = "New Note";
            this.tbNewNote.ToolTipText = "Add a new note.";
            this.tbNewNote.Visible= false;
            //this.tbNewNote.Click += new System.EventHandler(this.tbEdit_Click);
            //
            */
            //
            // tbNotes
            // 
            this.tbNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbNotes.Image = global::easiplan.app.Properties.Resources.notes_b_42;
            this.tbNotes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbNotes.ImageTransparentColor = System.Drawing.Color.Gainsboro;
            this.tbNotes.Margin = new System.Windows.Forms.Padding(0);
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Padding = new System.Windows.Forms.Padding(5);
            this.tbNotes.Size = new System.Drawing.Size(101, 46);
            this.tbNotes.Text = "Notes";
            this.tbNotes.ToolTipText = "Open form for edit.";
            this.tbNotes.Visible = false;
            this.tbNotes.Click += new System.EventHandler(this.tbNotes_Click);
            // 
            // xToolBarMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.CausesValidation = false;
            this.Controls.Add(this.tbStrip);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "xToolBarMenu";
            this.Size = new System.Drawing.Size(650, 46);
            this.tbStrip.ResumeLayout(false);
            this.tbStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Finx.App.UserControls.ToolStripEx tbStrip;
        internal System.Windows.Forms.ToolStripLabel tbCaption;
        internal System.Windows.Forms.ToolStripButton tbClose;
        internal System.Windows.Forms.ToolStripButton tbSave;
        internal System.Windows.Forms.ToolStripButton tbRefresh;
        internal System.Windows.Forms.ToolStripButton tbEdit;

        //Note dropdown decleration
        //internal System.Windows.Forms.ToolStripDropDownButton tbNewNote;


        internal System.Windows.Forms.ToolStripButton tbCaptionImage;
        internal System.Windows.Forms.ToolStripButton tbNotes;
    }
}
