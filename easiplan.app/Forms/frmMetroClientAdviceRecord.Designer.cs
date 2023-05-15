using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Controls.Ext;
using System.Drawing;
using System.Management.Instrumentation;
using System.Windows.Forms;

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
            this.xInput_ShowCompletedTasks = new Finx.App.UserControls.xInput();
            this.metroPanel_PolicyNote = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_FinancialSolution = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_FinancialSolution = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_FinancialSolutionHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_FinancialSolution = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_needAndObj = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_NeedAndObjective = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_Summary = new System.Windows.Forms.Label();
            this.metroPanel_AccessCapital = new MetroFramework.Controls.MetroPanel();
            this.AtCchk3 = new System.Windows.Forms.Button();
            this.AtCchk2 = new System.Windows.Forms.Button();
            this.AtCchk1 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.metroPanel_PKE = new MetroFramework.Controls.MetroPanel();
            this.PKEchk2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.PKEchk10 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PKEchk9 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.PKEchk8 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.PKEchk7 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.PKEchk6 = new System.Windows.Forms.Button();
            this.PKEchk1 = new System.Windows.Forms.Button();
            this.PKEchk5 = new System.Windows.Forms.Button();
            this.PKEchk4 = new System.Windows.Forms.Button();
            this.PKEchk3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.metroPanel_InvestHorizen = new MetroFramework.Controls.MetroPanel();
            this.IHchk4 = new System.Windows.Forms.Button();
            this.IHchk3 = new System.Windows.Forms.Button();
            this.IHchk2 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.IHchk1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.metroLabel_NeedsAndObjHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_NeedsAndObj = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_AccessToCapitalHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_AccessToCapital = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_InvestHorizonHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_InvestHorizen = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_PKEHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_PKE = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_Select = new MetroFramework.Controls.MetroPanel();
            this.multiLineTextEditor = new MetroFramework.Controls.MetroTextBox();
            this.panel = new MetroFramework.Controls.MetroPanel();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.metroPanel_PolicyNote.SuspendLayout();
            this.metroPanel_FinancialSolution.SuspendLayout();
            this.metroPanel_needAndObj.SuspendLayout();
            this.metroPanel_AccessCapital.SuspendLayout();
            this.metroPanel_PKE.SuspendLayout();
            this.metroPanel_InvestHorizen.SuspendLayout();
            this.panel.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(1685, 946);
            this.splitContainer1.SplitterDistance = 600;
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
            this.dataGrid_Notes.Size = new System.Drawing.Size(600, 909);
            this.dataGrid_Notes.TabIndex = 7;
            this.dataGrid_Notes.TabStop = true;
            this.dataGrid_Notes.ToolTipText = "";
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
            this.xInput_ShowCompletedTasks.LableText = "Policy Type:";
            this.xInput_ShowCompletedTasks.Location = new System.Drawing.Point(0, 0);
            this.xInput_ShowCompletedTasks.MappedField = null;
            this.xInput_ShowCompletedTasks.Margin = new System.Windows.Forms.Padding(0);
            this.xInput_ShowCompletedTasks.MinimumSize = new System.Drawing.Size(67, 37);
            this.xInput_ShowCompletedTasks.Model = null;
            this.xInput_ShowCompletedTasks.Name = "xInput_ShowCompletedTasks";
            this.xInput_ShowCompletedTasks.Padding = new System.Windows.Forms.Padding(1);
            this.xInput_ShowCompletedTasks.ReadOnly = false;
            this.xInput_ShowCompletedTasks.Size = new System.Drawing.Size(600, 37);
            this.xInput_ShowCompletedTasks.TabIndex = 6;
            this.xInput_ShowCompletedTasks.Value = null;
            this.xInput_ShowCompletedTasks.ValueMember = "Value";
            // 
            // metroPanel_PolicyNote
            // 
            this.metroPanel_PolicyNote.AutoScroll = true;
            this.metroPanel_PolicyNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel_PolicyNote.Controls.Add(this.metroPanel_FinancialSolution);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_FinancialSolutionHint);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_FinancialSolution);
            this.metroPanel_PolicyNote.Controls.Add(this.metroPanel_needAndObj);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_Summary);
            this.metroPanel_PolicyNote.Controls.Add(this.metroPanel_AccessCapital);
            this.metroPanel_PolicyNote.Controls.Add(this.metroPanel_PKE);
            this.metroPanel_PolicyNote.Controls.Add(this.metroPanel_InvestHorizen);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_NeedsAndObjHint);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_NeedsAndObj);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_AccessToCapitalHint);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_AccessToCapital);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_InvestHorizonHint);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_InvestHorizen);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_PKEHint);
            this.metroPanel_PolicyNote.Controls.Add(this.metroLabel_PKE);
            this.metroPanel_PolicyNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_PolicyNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroPanel_PolicyNote.HorizontalScrollbar = true;
            this.metroPanel_PolicyNote.HorizontalScrollbarBarColor = true;
            this.metroPanel_PolicyNote.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_PolicyNote.HorizontalScrollbarSize = 12;
            this.metroPanel_PolicyNote.Location = new System.Drawing.Point(0, 74);
            this.metroPanel_PolicyNote.Margin = new System.Windows.Forms.Padding(4);
            this.metroPanel_PolicyNote.Name = "metroPanel_PolicyNote";
            this.metroPanel_PolicyNote.Size = new System.Drawing.Size(1080, 872);
            this.metroPanel_PolicyNote.TabIndex = 9;
            this.metroPanel_PolicyNote.VerticalScrollbar = true;
            this.metroPanel_PolicyNote.VerticalScrollbarBarColor = true;
            this.metroPanel_PolicyNote.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_PolicyNote.VerticalScrollbarSize = 13;
            this.metroPanel_PolicyNote.Paint += new System.Windows.Forms.PaintEventHandler(this.metroPanel_PolicyNote_Paint);
            // 
            // metroPanel_FinancialSolution
            // 
            this.metroPanel_FinancialSolution.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_FinancialSolution.Controls.Add(this.metroTextBox_FinancialSolution);
            this.metroPanel_FinancialSolution.HorizontalScrollbarBarColor = true;
            this.metroPanel_FinancialSolution.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_FinancialSolution.HorizontalScrollbarSize = 10;
            this.metroPanel_FinancialSolution.Location = new System.Drawing.Point(15, 775);
            this.metroPanel_FinancialSolution.Name = "metroPanel_FinancialSolution";
            this.metroPanel_FinancialSolution.Size = new System.Drawing.Size(1055, 96);
            this.metroPanel_FinancialSolution.TabIndex = 20;
            this.metroPanel_FinancialSolution.VerticalScrollbarBarColor = true;
            this.metroPanel_FinancialSolution.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_FinancialSolution.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_FinancialSolution
            // 
            // 
            // 
            // 
            this.metroTextBox_FinancialSolution.CustomButton.Image = null;
            this.metroTextBox_FinancialSolution.CustomButton.Location = new System.Drawing.Point(961, 2);
            this.metroTextBox_FinancialSolution.CustomButton.Name = "";
            this.metroTextBox_FinancialSolution.CustomButton.Size = new System.Drawing.Size(85, 85);
            this.metroTextBox_FinancialSolution.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_FinancialSolution.CustomButton.TabIndex = 1;
            this.metroTextBox_FinancialSolution.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_FinancialSolution.CustomButton.UseSelectable = true;
            this.metroTextBox_FinancialSolution.CustomButton.Visible = false;
            this.metroTextBox_FinancialSolution.Lines = new string[0];
            this.metroTextBox_FinancialSolution.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_FinancialSolution.MaxLength = 32767;
            this.metroTextBox_FinancialSolution.Multiline = true;
            this.metroTextBox_FinancialSolution.Name = "metroTextBox_FinancialSolution";
            this.metroTextBox_FinancialSolution.PasswordChar = '\0';
            this.metroTextBox_FinancialSolution.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox_FinancialSolution.SelectedText = "";
            this.metroTextBox_FinancialSolution.SelectionLength = 0;
            this.metroTextBox_FinancialSolution.SelectionStart = 0;
            this.metroTextBox_FinancialSolution.ShortcutsEnabled = true;
            this.metroTextBox_FinancialSolution.Size = new System.Drawing.Size(1049, 90);
            this.metroTextBox_FinancialSolution.TabIndex = 2;
            this.metroTextBox_FinancialSolution.UseCustomBackColor = true;
            this.metroTextBox_FinancialSolution.UseSelectable = true;
            this.metroTextBox_FinancialSolution.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_FinancialSolution.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_FinancialSolutionHint
            // 
            this.metroLabel_FinancialSolutionHint.AutoSize = true;
            this.metroLabel_FinancialSolutionHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_FinancialSolutionHint.Location = new System.Drawing.Point(10, 741);
            this.metroLabel_FinancialSolutionHint.Name = "metroLabel_FinancialSolutionHint";
            this.metroLabel_FinancialSolutionHint.Size = new System.Drawing.Size(368, 20);
            this.metroLabel_FinancialSolutionHint.TabIndex = 21;
            this.metroLabel_FinancialSolutionHint.Text = "(Set out summary of the client\'s current financial situation.)";
            this.metroLabel_FinancialSolutionHint.UseCustomForeColor = true;
            // 
            // metroLabel_FinancialSolution
            // 
            this.metroLabel_FinancialSolution.AutoSize = true;
            this.metroLabel_FinancialSolution.Location = new System.Drawing.Point(10, 721);
            this.metroLabel_FinancialSolution.Name = "metroLabel_FinancialSolution";
            this.metroLabel_FinancialSolution.Size = new System.Drawing.Size(118, 20);
            this.metroLabel_FinancialSolution.TabIndex = 20;
            this.metroLabel_FinancialSolution.Text = "Financial Solution:";
            // 
            // metroPanel_needAndObj
            // 
            this.metroPanel_needAndObj.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_needAndObj.Controls.Add(this.metroTextBox_NeedAndObjective);
            this.metroPanel_needAndObj.HorizontalScrollbarBarColor = true;
            this.metroPanel_needAndObj.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_needAndObj.HorizontalScrollbarSize = 10;
            this.metroPanel_needAndObj.Location = new System.Drawing.Point(15, 605);
            this.metroPanel_needAndObj.Name = "metroPanel_needAndObj";
            this.metroPanel_needAndObj.Size = new System.Drawing.Size(1055, 96);
            this.metroPanel_needAndObj.TabIndex = 19;
            this.metroPanel_needAndObj.VerticalScrollbarBarColor = true;
            this.metroPanel_needAndObj.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_needAndObj.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_NeedAndObjective
            // 
            // 
            // 
            // 
            this.metroTextBox_NeedAndObjective.CustomButton.Image = null;
            this.metroTextBox_NeedAndObjective.CustomButton.Location = new System.Drawing.Point(961, 2);
            this.metroTextBox_NeedAndObjective.CustomButton.Name = "";
            this.metroTextBox_NeedAndObjective.CustomButton.Size = new System.Drawing.Size(85, 85);
            this.metroTextBox_NeedAndObjective.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_NeedAndObjective.CustomButton.TabIndex = 1;
            this.metroTextBox_NeedAndObjective.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_NeedAndObjective.CustomButton.UseSelectable = true;
            this.metroTextBox_NeedAndObjective.CustomButton.Visible = false;
            this.metroTextBox_NeedAndObjective.Lines = new string[0];
            this.metroTextBox_NeedAndObjective.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_NeedAndObjective.MaxLength = 32767;
            this.metroTextBox_NeedAndObjective.Multiline = true;
            this.metroTextBox_NeedAndObjective.Name = "metroTextBox_NeedAndObjective";
            this.metroTextBox_NeedAndObjective.PasswordChar = '\0';
            this.metroTextBox_NeedAndObjective.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox_NeedAndObjective.SelectedText = "";
            this.metroTextBox_NeedAndObjective.SelectionLength = 0;
            this.metroTextBox_NeedAndObjective.SelectionStart = 0;
            this.metroTextBox_NeedAndObjective.ShortcutsEnabled = true;
            this.metroTextBox_NeedAndObjective.Size = new System.Drawing.Size(1049, 90);
            this.metroTextBox_NeedAndObjective.TabIndex = 2;
            this.metroTextBox_NeedAndObjective.UseCustomBackColor = true;
            this.metroTextBox_NeedAndObjective.UseSelectable = true;
            this.metroTextBox_NeedAndObjective.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_NeedAndObjective.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_Summary
            // 
            this.metroLabel_Summary.AutoSize = true;
            this.metroLabel_Summary.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroLabel_Summary.Location = new System.Drawing.Point(474, 4);
            this.metroLabel_Summary.Name = "metroLabel_Summary";
            this.metroLabel_Summary.Size = new System.Drawing.Size(141, 32);
            this.metroLabel_Summary.TabIndex = 18;
            this.metroLabel_Summary.Text = "Summary";
            // 
            // metroPanel_AccessCapital
            // 
            this.metroPanel_AccessCapital.Controls.Add(this.AtCchk3);
            this.metroPanel_AccessCapital.Controls.Add(this.AtCchk2);
            this.metroPanel_AccessCapital.Controls.Add(this.AtCchk1);
            this.metroPanel_AccessCapital.Controls.Add(this.label16);
            this.metroPanel_AccessCapital.Controls.Add(this.label17);
            this.metroPanel_AccessCapital.Controls.Add(this.label15);
            this.metroPanel_AccessCapital.HorizontalScrollbarBarColor = true;
            this.metroPanel_AccessCapital.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_AccessCapital.HorizontalScrollbarSize = 10;
            this.metroPanel_AccessCapital.Location = new System.Drawing.Point(15, 445);
            this.metroPanel_AccessCapital.Name = "metroPanel_AccessCapital";
            this.metroPanel_AccessCapital.Size = new System.Drawing.Size(606, 84);
            this.metroPanel_AccessCapital.TabIndex = 17;
            this.metroPanel_AccessCapital.VerticalScrollbarBarColor = true;
            this.metroPanel_AccessCapital.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_AccessCapital.VerticalScrollbarSize = 10;
            // 
            // AtCchk3
            // 
            this.AtCchk3.BackColor = System.Drawing.Color.White;
            this.AtCchk3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AtCchk3.Location = new System.Drawing.Point(400, 40);
            this.AtCchk3.Name = "AtCchk3";
            this.AtCchk3.Size = new System.Drawing.Size(203, 40);
            this.AtCchk3.TabIndex = 21;
            this.AtCchk3.UseVisualStyleBackColor = false;
            this.AtCchk3.Click += new System.EventHandler(this.AtCchk3_Click);
            // 
            // AtCchk2
            // 
            this.AtCchk2.BackColor = System.Drawing.Color.White;
            this.AtCchk2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AtCchk2.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            this.AtCchk2.Location = new System.Drawing.Point(200, 40);
            this.AtCchk2.Name = "AtCchk2";
            this.AtCchk2.Size = new System.Drawing.Size(203, 40);
            this.AtCchk2.TabIndex = 22;
            this.AtCchk2.UseVisualStyleBackColor = false;
            this.AtCchk2.Click += new System.EventHandler(this.AtCchk2_Click);
            // 
            // AtCchk1
            // 
            this.AtCchk1.BackColor = System.Drawing.Color.White;
            this.AtCchk1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AtCchk1.Location = new System.Drawing.Point(0, 40);
            this.AtCchk1.Name = "AtCchk1";
            this.AtCchk1.Size = new System.Drawing.Size(203, 40);
            this.AtCchk1.TabIndex = 23;
            this.AtCchk1.UseVisualStyleBackColor = false;
            this.AtCchk1.Click += new System.EventHandler(this.AtCchk1_Click);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(400, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(203, 40);
            this.label16.TabIndex = 23;
            this.label16.Text = "Do not require access to\r\ncapital for 5 years";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(200, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(203, 40);
            this.label17.TabIndex = 24;
            this.label17.Text = "Always require access to \r\ncapital";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(203, 40);
            this.label15.TabIndex = 22;
            this.label15.Text = "Need to draw an Income";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroPanel_PKE
            // 
            this.metroPanel_PKE.Controls.Add(this.PKEchk2);
            this.metroPanel_PKE.Controls.Add(this.label10);
            this.metroPanel_PKE.Controls.Add(this.PKEchk10);
            this.metroPanel_PKE.Controls.Add(this.label9);
            this.metroPanel_PKE.Controls.Add(this.label8);
            this.metroPanel_PKE.Controls.Add(this.label7);
            this.metroPanel_PKE.Controls.Add(this.label6);
            this.metroPanel_PKE.Controls.Add(this.PKEchk9);
            this.metroPanel_PKE.Controls.Add(this.label5);
            this.metroPanel_PKE.Controls.Add(this.PKEchk8);
            this.metroPanel_PKE.Controls.Add(this.label4);
            this.metroPanel_PKE.Controls.Add(this.PKEchk7);
            this.metroPanel_PKE.Controls.Add(this.label3);
            this.metroPanel_PKE.Controls.Add(this.PKEchk6);
            this.metroPanel_PKE.Controls.Add(this.PKEchk1);
            this.metroPanel_PKE.Controls.Add(this.PKEchk5);
            this.metroPanel_PKE.Controls.Add(this.PKEchk4);
            this.metroPanel_PKE.Controls.Add(this.PKEchk3);
            this.metroPanel_PKE.Controls.Add(this.label2);
            this.metroPanel_PKE.Controls.Add(this.label1);
            this.metroPanel_PKE.HorizontalScrollbarBarColor = true;
            this.metroPanel_PKE.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_PKE.HorizontalScrollbarSize = 10;
            this.metroPanel_PKE.Location = new System.Drawing.Point(15, 137);
            this.metroPanel_PKE.Name = "metroPanel_PKE";
            this.metroPanel_PKE.Size = new System.Drawing.Size(825, 84);
            this.metroPanel_PKE.TabIndex = 16;
            this.metroPanel_PKE.VerticalScrollbarBarColor = true;
            this.metroPanel_PKE.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_PKE.VerticalScrollbarSize = 10;
            // 
            // PKEchk2
            // 
            this.PKEchk2.BackColor = System.Drawing.Color.White;
            this.PKEchk2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PKEchk2.Location = new System.Drawing.Point(81, 40);
            this.PKEchk2.Name = "PKEchk2";
            this.PKEchk2.Size = new System.Drawing.Size(85, 40);
            this.PKEchk2.TabIndex = 22;
            this.PKEchk2.UseVisualStyleBackColor = false;
            this.PKEchk2.Click += new System.EventHandler(this.PKEchk2_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(737, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 40);
            this.label10.TabIndex = 27;
            this.label10.Text = "10\r\nHigh";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PKEchk10
            // 
            this.PKEchk10.BackColor = System.Drawing.Color.White;
            this.PKEchk10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PKEchk10.Location = new System.Drawing.Point(737, 40);
            this.PKEchk10.Name = "PKEchk10";
            this.PKEchk10.Size = new System.Drawing.Size(85, 40);
            this.PKEchk10.TabIndex = 30;
            this.PKEchk10.UseVisualStyleBackColor = false;
            this.PKEchk10.Click += new System.EventHandler(this.PKEchk10_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(655, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 40);
            this.label9.TabIndex = 26;
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(572, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 40);
            this.label8.TabIndex = 25;
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Location = new System.Drawing.Point(492, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 40);
            this.label7.TabIndex = 24;
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(411, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 40);
            this.label6.TabIndex = 23;
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PKEchk9
            // 
            this.PKEchk9.BackColor = System.Drawing.Color.White;
            this.PKEchk9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PKEchk9.Location = new System.Drawing.Point(655, 40);
            this.PKEchk9.Name = "PKEchk9";
            this.PKEchk9.Size = new System.Drawing.Size(85, 40);
            this.PKEchk9.TabIndex = 29;
            this.PKEchk9.UseVisualStyleBackColor = false;
            this.PKEchk9.Click += new System.EventHandler(this.PKEchk9_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(329, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 40);
            this.label5.TabIndex = 22;
            this.label5.Text = "5\r\nModerate";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PKEchk8
            // 
            this.PKEchk8.BackColor = System.Drawing.Color.White;
            this.PKEchk8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PKEchk8.Location = new System.Drawing.Point(572, 40);
            this.PKEchk8.Name = "PKEchk8";
            this.PKEchk8.Size = new System.Drawing.Size(85, 40);
            this.PKEchk8.TabIndex = 28;
            this.PKEchk8.UseVisualStyleBackColor = false;
            this.PKEchk8.Click += new System.EventHandler(this.PKEchk8_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(247, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 40);
            this.label4.TabIndex = 21;
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PKEchk7
            // 
            this.PKEchk7.BackColor = System.Drawing.Color.White;
            this.PKEchk7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PKEchk7.Location = new System.Drawing.Point(492, 40);
            this.PKEchk7.Name = "PKEchk7";
            this.PKEchk7.Size = new System.Drawing.Size(85, 40);
            this.PKEchk7.TabIndex = 27;
            this.PKEchk7.UseVisualStyleBackColor = false;
            this.PKEchk7.Click += new System.EventHandler(this.PKEchk7_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(165, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 40);
            this.label3.TabIndex = 20;
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PKEchk6
            // 
            this.PKEchk6.BackColor = System.Drawing.Color.White;
            this.PKEchk6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PKEchk6.Location = new System.Drawing.Point(411, 40);
            this.PKEchk6.Name = "PKEchk6";
            this.PKEchk6.Size = new System.Drawing.Size(85, 40);
            this.PKEchk6.TabIndex = 26;
            this.PKEchk6.UseVisualStyleBackColor = false;
            this.PKEchk6.Click += new System.EventHandler(this.PKEchk6_Click);
            // 
            // PKEchk1
            // 
            this.PKEchk1.BackColor = System.Drawing.Color.White;
            this.PKEchk1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PKEchk1.Location = new System.Drawing.Point(0, 40);
            this.PKEchk1.Name = "PKEchk1";
            this.PKEchk1.Size = new System.Drawing.Size(85, 40);
            this.PKEchk1.TabIndex = 21;
            this.PKEchk1.UseVisualStyleBackColor = false;
            this.PKEchk1.Click += new System.EventHandler(this.PKEchk1_Click);
            // 
            // PKEchk5
            // 
            this.PKEchk5.BackColor = System.Drawing.Color.White;
            this.PKEchk5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PKEchk5.Location = new System.Drawing.Point(329, 40);
            this.PKEchk5.Name = "PKEchk5";
            this.PKEchk5.Size = new System.Drawing.Size(85, 40);
            this.PKEchk5.TabIndex = 25;
            this.PKEchk5.UseVisualStyleBackColor = false;
            this.PKEchk5.Click += new System.EventHandler(this.PKEchk5_Click);
            // 
            // PKEchk4
            // 
            this.PKEchk4.BackColor = System.Drawing.Color.White;
            this.PKEchk4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PKEchk4.Location = new System.Drawing.Point(247, 40);
            this.PKEchk4.Name = "PKEchk4";
            this.PKEchk4.Size = new System.Drawing.Size(85, 40);
            this.PKEchk4.TabIndex = 23;
            this.PKEchk4.UseVisualStyleBackColor = false;
            this.PKEchk4.Click += new System.EventHandler(this.PKEchk4_Click);
            // 
            // PKEchk3
            // 
            this.PKEchk3.BackColor = System.Drawing.Color.White;
            this.PKEchk3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PKEchk3.Location = new System.Drawing.Point(165, 40);
            this.PKEchk3.Name = "PKEchk3";
            this.PKEchk3.Size = new System.Drawing.Size(85, 40);
            this.PKEchk3.TabIndex = 24;
            this.PKEchk3.UseVisualStyleBackColor = false;
            this.PKEchk3.Click += new System.EventHandler(this.PKEchk3_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(81, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 40);
            this.label2.TabIndex = 19;
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 40);
            this.label1.TabIndex = 18;
            this.label1.Text = "1 \r\nLow";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // metroPanel_InvestHorizen
            // 
            this.metroPanel_InvestHorizen.Controls.Add(this.IHchk4);
            this.metroPanel_InvestHorizen.Controls.Add(this.IHchk3);
            this.metroPanel_InvestHorizen.Controls.Add(this.IHchk2);
            this.metroPanel_InvestHorizen.Controls.Add(this.label14);
            this.metroPanel_InvestHorizen.Controls.Add(this.IHchk1);
            this.metroPanel_InvestHorizen.Controls.Add(this.label12);
            this.metroPanel_InvestHorizen.Controls.Add(this.label13);
            this.metroPanel_InvestHorizen.Controls.Add(this.label11);
            this.metroPanel_InvestHorizen.HorizontalScrollbarBarColor = true;
            this.metroPanel_InvestHorizen.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_InvestHorizen.HorizontalScrollbarSize = 10;
            this.metroPanel_InvestHorizen.Location = new System.Drawing.Point(15, 289);
            this.metroPanel_InvestHorizen.Name = "metroPanel_InvestHorizen";
            this.metroPanel_InvestHorizen.Size = new System.Drawing.Size(334, 84);
            this.metroPanel_InvestHorizen.TabIndex = 15;
            this.metroPanel_InvestHorizen.VerticalScrollbarBarColor = true;
            this.metroPanel_InvestHorizen.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_InvestHorizen.VerticalScrollbarSize = 10;
            // 
            // IHchk4
            // 
            this.IHchk4.BackColor = System.Drawing.Color.White;
            this.IHchk4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IHchk4.Location = new System.Drawing.Point(247, 40);
            this.IHchk4.Name = "IHchk4";
            this.IHchk4.Size = new System.Drawing.Size(85, 40);
            this.IHchk4.TabIndex = 20;
            this.IHchk4.UseVisualStyleBackColor = false;
            this.IHchk4.Click += new System.EventHandler(this.IHchk4_Click);
            // 
            // IHchk3
            // 
            this.IHchk3.BackColor = System.Drawing.Color.White;
            this.IHchk3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IHchk3.Location = new System.Drawing.Point(165, 40);
            this.IHchk3.Name = "IHchk3";
            this.IHchk3.Size = new System.Drawing.Size(85, 40);
            this.IHchk3.TabIndex = 21;
            this.IHchk3.UseVisualStyleBackColor = false;
            this.IHchk3.Click += new System.EventHandler(this.IHchk3_Click);
            // 
            // IHchk2
            // 
            this.IHchk2.BackColor = System.Drawing.Color.White;
            this.IHchk2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IHchk2.Location = new System.Drawing.Point(81, 40);
            this.IHchk2.Name = "IHchk2";
            this.IHchk2.Size = new System.Drawing.Size(85, 40);
            this.IHchk2.TabIndex = 22;
            this.IHchk2.UseVisualStyleBackColor = false;
            this.IHchk2.Click += new System.EventHandler(this.IHchk2_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(247, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 40);
            this.label14.TabIndex = 25;
            this.label14.Text = "10\r\nYears +";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // IHchk1
            // 
            this.IHchk1.BackColor = System.Drawing.Color.White;
            this.IHchk1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IHchk1.Location = new System.Drawing.Point(0, 40);
            this.IHchk1.Name = "IHchk1";
            this.IHchk1.Size = new System.Drawing.Size(85, 40);
            this.IHchk1.TabIndex = 19;
            this.IHchk1.UseVisualStyleBackColor = false;
            this.IHchk1.Click += new System.EventHandler(this.IHchk1_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(81, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 40);
            this.label12.TabIndex = 23;
            this.label12.Text = "2-5\r\nYears";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(163, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 40);
            this.label13.TabIndex = 24;
            this.label13.Text = "5-9\r\nYears";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 40);
            this.label11.TabIndex = 22;
            this.label11.Text = "0-2\r\nYears";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // metroLabel_NeedsAndObjHint
            // 
            this.metroLabel_NeedsAndObjHint.AutoSize = true;
            this.metroLabel_NeedsAndObjHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_NeedsAndObjHint.Location = new System.Drawing.Point(10, 566);
            this.metroLabel_NeedsAndObjHint.Name = "metroLabel_NeedsAndObjHint";
            this.metroLabel_NeedsAndObjHint.Size = new System.Drawing.Size(665, 20);
            this.metroLabel_NeedsAndObjHint.TabIndex = 14;
            this.metroLabel_NeedsAndObjHint.Text = "(What are the client\'s needs and what does the client wish to achieve by purchasi" +
    "ng this financial product?)";
            this.metroLabel_NeedsAndObjHint.UseCustomForeColor = true;
            // 
            // metroLabel_NeedsAndObj
            // 
            this.metroLabel_NeedsAndObj.AutoSize = true;
            this.metroLabel_NeedsAndObj.Location = new System.Drawing.Point(10, 546);
            this.metroLabel_NeedsAndObj.Name = "metroLabel_NeedsAndObj";
            this.metroLabel_NeedsAndObj.Size = new System.Drawing.Size(149, 20);
            this.metroLabel_NeedsAndObj.TabIndex = 13;
            this.metroLabel_NeedsAndObj.Text = "Needs and Objectives:";
            this.metroLabel_NeedsAndObj.Click += new System.EventHandler(this.metroLabel8_Click);
            // 
            // metroLabel_AccessToCapitalHint
            // 
            this.metroLabel_AccessToCapitalHint.AutoSize = true;
            this.metroLabel_AccessToCapitalHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_AccessToCapitalHint.Location = new System.Drawing.Point(10, 411);
            this.metroLabel_AccessToCapitalHint.Name = "metroLabel_AccessToCapitalHint";
            this.metroLabel_AccessToCapitalHint.Size = new System.Drawing.Size(314, 20);
            this.metroLabel_AccessToCapitalHint.TabIndex = 12;
            this.metroLabel_AccessToCapitalHint.Text = "(The accessibility and liquidity of the invesement.)";
            this.metroLabel_AccessToCapitalHint.UseCustomForeColor = true;
            // 
            // metroLabel_AccessToCapital
            // 
            this.metroLabel_AccessToCapital.AutoSize = true;
            this.metroLabel_AccessToCapital.Location = new System.Drawing.Point(10, 391);
            this.metroLabel_AccessToCapital.Name = "metroLabel_AccessToCapital";
            this.metroLabel_AccessToCapital.Size = new System.Drawing.Size(116, 20);
            this.metroLabel_AccessToCapital.TabIndex = 11;
            this.metroLabel_AccessToCapital.Text = "Access to Capital:";
            // 
            // metroLabel_InvestHorizonHint
            // 
            this.metroLabel_InvestHorizonHint.AutoSize = true;
            this.metroLabel_InvestHorizonHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_InvestHorizonHint.Location = new System.Drawing.Point(10, 256);
            this.metroLabel_InvestHorizonHint.Name = "metroLabel_InvestHorizonHint";
            this.metroLabel_InvestHorizonHint.Size = new System.Drawing.Size(634, 20);
            this.metroLabel_InvestHorizonHint.TabIndex = 10;
            this.metroLabel_InvestHorizonHint.Text = "(For how long the client expects his/her money to be invested before he/she would" +
    " like to cash it in?)";
            this.metroLabel_InvestHorizonHint.UseCustomForeColor = true;
            // 
            // metroLabel_InvestHorizen
            // 
            this.metroLabel_InvestHorizen.AutoSize = true;
            this.metroLabel_InvestHorizen.Location = new System.Drawing.Point(10, 236);
            this.metroLabel_InvestHorizen.Name = "metroLabel_InvestHorizen";
            this.metroLabel_InvestHorizen.Size = new System.Drawing.Size(133, 20);
            this.metroLabel_InvestHorizen.TabIndex = 9;
            this.metroLabel_InvestHorizen.Text = "Investment Horizon:";
            // 
            // metroLabel_PKEHint
            // 
            this.metroLabel_PKEHint.AutoSize = true;
            this.metroLabel_PKEHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_PKEHint.Location = new System.Drawing.Point(10, 103);
            this.metroLabel_PKEHint.Name = "metroLabel_PKEHint";
            this.metroLabel_PKEHint.Size = new System.Drawing.Size(526, 20);
            this.metroLabel_PKEHint.TabIndex = 8;
            this.metroLabel_PKEHint.Text = "(Describe the client\'s level of knowledge and experience of the product purchased" +
    ".)";
            this.metroLabel_PKEHint.UseCustomForeColor = true;
            // 
            // metroLabel_PKE
            // 
            this.metroLabel_PKE.AutoSize = true;
            this.metroLabel_PKE.Location = new System.Drawing.Point(10, 83);
            this.metroLabel_PKE.Name = "metroLabel_PKE";
            this.metroLabel_PKE.Size = new System.Drawing.Size(233, 20);
            this.metroLabel_PKE.TabIndex = 7;
            this.metroLabel_PKE.Text = "Product Knowledge and Experience:";
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
            this.metroPanel_Select.Size = new System.Drawing.Size(1080, 74);
            this.metroPanel_Select.TabIndex = 8;
            this.metroPanel_Select.VerticalScrollbarBarColor = true;
            this.metroPanel_Select.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Select.VerticalScrollbarSize = 13;
            // 
            // multiLineTextEditor
            // 
            // 
            // 
            // 
            this.multiLineTextEditor.CustomButton.Image = null;
            this.multiLineTextEditor.CustomButton.Location = new System.Drawing.Point(-20, 2);
            this.multiLineTextEditor.CustomButton.Name = "";
            this.multiLineTextEditor.CustomButton.Size = new System.Drawing.Size(17, 17);
            this.multiLineTextEditor.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.multiLineTextEditor.CustomButton.TabIndex = 1;
            this.multiLineTextEditor.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.multiLineTextEditor.CustomButton.UseSelectable = true;
            this.multiLineTextEditor.CustomButton.Visible = false;
            this.multiLineTextEditor.Lines = new string[0];
            this.multiLineTextEditor.Location = new System.Drawing.Point(0, 0);
            this.multiLineTextEditor.MaxLength = 32767;
            this.multiLineTextEditor.Multiline = true;
            this.multiLineTextEditor.Name = "multiLineTextEditor";
            this.multiLineTextEditor.PasswordChar = '\0';
            this.multiLineTextEditor.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.multiLineTextEditor.SelectedText = "";
            this.multiLineTextEditor.SelectionLength = 0;
            this.multiLineTextEditor.SelectionStart = 0;
            this.multiLineTextEditor.ShortcutsEnabled = true;
            this.multiLineTextEditor.Size = new System.Drawing.Size(0, 22);
            this.multiLineTextEditor.TabIndex = 2;
            this.multiLineTextEditor.UseSelectable = true;
            this.multiLineTextEditor.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.multiLineTextEditor.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.multiLineTextEditor);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.HorizontalScrollbarBarColor = true;
            this.panel.HorizontalScrollbarHighlightOnWheel = false;
            this.panel.HorizontalScrollbarSize = 10;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 100);
            this.panel.TabIndex = 0;
            this.panel.VerticalScrollbarBarColor = true;
            this.panel.VerticalScrollbarHighlightOnWheel = false;
            this.panel.VerticalScrollbarSize = 10;
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
            this.ClientSize = new System.Drawing.Size(1739, 1102);
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
            this.metroPanel_FinancialSolution.ResumeLayout(false);
            this.metroPanel_needAndObj.ResumeLayout(false);
            this.metroPanel_AccessCapital.ResumeLayout(false);
            this.metroPanel_PKE.ResumeLayout(false);
            this.metroPanel_InvestHorizen.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroPanel metroPanel_PolicyNote;
        private MetroFramework.Controls.MetroPanel metroPanel_Select;
        private UserControls.xInput xInput_ShowCompletedTasks;
        private SourceGrid.DataGrid dataGrid_Notes;
        private MetroFramework.Controls.MetroLabel metroLabel_NeedsAndObj;
        private MetroFramework.Controls.MetroLabel metroLabel_AccessToCapitalHint;
        private MetroFramework.Controls.MetroLabel metroLabel_AccessToCapital;
        private MetroFramework.Controls.MetroLabel metroLabel_InvestHorizonHint;
        private MetroFramework.Controls.MetroLabel metroLabel_InvestHorizen;
        private MetroFramework.Controls.MetroLabel metroLabel_PKEHint;
        private MetroFramework.Controls.MetroLabel metroLabel_PKE;
        private MetroFramework.Controls.MetroLabel metroLabel_NeedsAndObjHint;
        private MetroFramework.Controls.MetroPanel metroPanel_AccessCapital;
        private MetroFramework.Controls.MetroPanel metroPanel_PKE;
        private MetroFramework.Controls.MetroPanel metroPanel_InvestHorizen;
        private System.Windows.Forms.Label metroLabel_Summary;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button IHchk1;
        private System.Windows.Forms.Button PKEchk10;
        private System.Windows.Forms.Button PKEchk9;
        private System.Windows.Forms.Button PKEchk8;
        private System.Windows.Forms.Button PKEchk7;
        private System.Windows.Forms.Button PKEchk6;
        private System.Windows.Forms.Button PKEchk5;
        private System.Windows.Forms.Button PKEchk4;
        private System.Windows.Forms.Button PKEchk3;
        private System.Windows.Forms.Button PKEchk2;
        private System.Windows.Forms.Button PKEchk1;
        private System.Windows.Forms.Button IHchk4;
        private System.Windows.Forms.Button IHchk3;
        private System.Windows.Forms.Button IHchk2;
        private System.Windows.Forms.Button AtCchk3;
        private System.Windows.Forms.Button AtCchk2;
        private System.Windows.Forms.Button AtCchk1;
        private MetroFramework.Controls.MetroPanel metroPanel_needAndObj;
        private MetroTextBox metroTextBox_NeedAndObjective;
        private MetroTextBox multiLineTextEditor;
        private MetroPanel panel;
        private MetroPanel metroPanel_FinancialSolution;
        private MetroTextBox metroTextBox_FinancialSolution;
        private MetroLabel metroLabel_FinancialSolutionHint;
        private MetroLabel metroLabel_FinancialSolution;
    }
}