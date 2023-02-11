
namespace Finx.App.Forms
{
    partial class frmMetroPolicyNotes
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
            this.metroPanel_PolicyNote = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_Select = new MetroFramework.Controls.MetroPanel();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            this.xInput_ShowCompletedTasks = new Finx.App.UserControls.xInput();
            this.dataGrid_Notes = new SourceGrid.DataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 106);
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
            this.splitContainer1.Size = new System.Drawing.Size(716, 485);
            this.splitContainer1.SplitterDistance = 233;
            this.splitContainer1.TabIndex = 7;
            // 
            // metroPanel_PolicyNote
            // 
            this.metroPanel_PolicyNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_PolicyNote.HorizontalScrollbarBarColor = true;
            this.metroPanel_PolicyNote.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_PolicyNote.HorizontalScrollbarSize = 10;
            this.metroPanel_PolicyNote.Location = new System.Drawing.Point(0, 60);
            this.metroPanel_PolicyNote.Name = "metroPanel_PolicyNote";
            this.metroPanel_PolicyNote.Size = new System.Drawing.Size(479, 425);
            this.metroPanel_PolicyNote.TabIndex = 9;
            this.metroPanel_PolicyNote.VerticalScrollbarBarColor = true;
            this.metroPanel_PolicyNote.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_PolicyNote.VerticalScrollbarSize = 10;
            // 
            // metroPanel_Select
            // 
            this.metroPanel_Select.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel_Select.HorizontalScrollbarBarColor = true;
            this.metroPanel_Select.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Select.HorizontalScrollbarSize = 10;
            this.metroPanel_Select.Location = new System.Drawing.Point(0, 0);
            this.metroPanel_Select.Name = "metroPanel_Select";
            this.metroPanel_Select.Size = new System.Drawing.Size(479, 60);
            this.metroPanel_Select.TabIndex = 8;
            this.metroPanel_Select.VerticalScrollbarBarColor = true;
            this.metroPanel_Select.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Select.VerticalScrollbarSize = 10;
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
            this.xToolBarMenu1.Size = new System.Drawing.Size(716, 46);
            this.xToolBarMenu1.TabIndex = 2;
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
            this.xInput_ShowCompletedTasks.MinimumSize = new System.Drawing.Size(50, 30);
            this.xInput_ShowCompletedTasks.Model = null;
            this.xInput_ShowCompletedTasks.Name = "xInput_ShowCompletedTasks";
            this.xInput_ShowCompletedTasks.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_ShowCompletedTasks.ReadOnly = false;
            this.xInput_ShowCompletedTasks.Size = new System.Drawing.Size(233, 30);
            this.xInput_ShowCompletedTasks.TabIndex = 6;
            this.xInput_ShowCompletedTasks.Value = null;
            this.xInput_ShowCompletedTasks.ValueMember = "Value";
            // 
            // dataGrid_Notes
            // 
            this.dataGrid_Notes.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.dataGrid_Notes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_Notes.EnableSort = false;
            this.dataGrid_Notes.FixedRows = 1;
            this.dataGrid_Notes.Location = new System.Drawing.Point(0, 30);
            this.dataGrid_Notes.Name = "dataGrid_Notes";
            this.dataGrid_Notes.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.dataGrid_Notes.Size = new System.Drawing.Size(233, 455);
            this.dataGrid_Notes.TabIndex = 7;
            this.dataGrid_Notes.TabStop = true;
            this.dataGrid_Notes.ToolTipText = "";
            // 
            // frmMetroPolicyNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 611);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.xToolBarMenu1);
            this.Name = "frmMetroPolicyNotes";
            this.Text = "frmMetroPolicyNotes";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroPanel metroPanel_PolicyNote;
        private MetroFramework.Controls.MetroPanel metroPanel_Select;
        private UserControls.xInput xInput_ShowCompletedTasks;
        private SourceGrid.DataGrid dataGrid_Notes;
    }
}