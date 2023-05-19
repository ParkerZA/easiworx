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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMetroClientAdviceRecord));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGrid_Notes = new SourceGrid.DataGrid();
            this.xInput_ShowCompletedTasks = new Finx.App.UserControls.xInput();
            this.metroPanel_AdviceRecord = new MetroFramework.Controls.MetroPanel();
            this.metroPanel_MedicalCover = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox10 = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_MedicalCoverHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_MedicalCover = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_MedicalConditions = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_MedicalConditions = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_MedicalConditions = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_MedicalConditionsHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_MotivationHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_Motivation = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_RecomendedFunds = new MetroFramework.Controls.MetroPanel();
            this.metroPanel7 = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox6 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox7 = new MetroFramework.Controls.MetroTextBox();
            this.label_InitialAdviceHint = new System.Windows.Forms.Label();
            this.label_InitialAdvice = new System.Windows.Forms.Label();
            this.metroLabel_RecommendedFund = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_OtherInformationHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_OtherInformation = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_OtherInformation = new MetroFramework.Controls.MetroPanel();
            this.metroPanel5 = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox4 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox5 = new MetroFramework.Controls.MetroTextBox();
            this.metroPanel_Motivation = new MetroFramework.Controls.MetroPanel();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox2 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox3 = new MetroFramework.Controls.MetroTextBox();
            this.metroPanel_FinancialSolution = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
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
            this.metroCheckBox_completed = new MetroFramework.Controls.MetroCheckBox();
            this.label_ongoing = new System.Windows.Forms.Label();
            this.label_Initial = new System.Windows.Forms.Label();
            this.label_Fees = new System.Windows.Forms.Label();
            this.metroTextBox9 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox8 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox_NoteDate = new MetroFramework.Controls.MetroTextBox();
            this.label_Date = new System.Windows.Forms.Label();
            this.multiLineTextEditor = new MetroFramework.Controls.MetroTextBox();
            this.panel = new MetroFramework.Controls.MetroPanel();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.metroPanel_AdviceRecord.SuspendLayout();
            this.metroPanel_MedicalCover.SuspendLayout();
            this.metroPanel_MedicalConditions.SuspendLayout();
            this.metroPanel_RecomendedFunds.SuspendLayout();
            this.metroPanel7.SuspendLayout();
            this.metroPanel_OtherInformation.SuspendLayout();
            this.metroPanel5.SuspendLayout();
            this.metroPanel_Motivation.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            this.metroPanel_FinancialSolution.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.metroPanel_needAndObj.SuspendLayout();
            this.metroPanel_AccessCapital.SuspendLayout();
            this.metroPanel_PKE.SuspendLayout();
            this.metroPanel_InvestHorizen.SuspendLayout();
            this.metroPanel_Select.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.metroPanel_AdviceRecord);
            this.splitContainer1.Panel2.Controls.Add(this.metroPanel_Select);
            this.splitContainer1.Size = new System.Drawing.Size(1685, 946);
            this.splitContainer1.SplitterDistance = 500;
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
            this.dataGrid_Notes.Size = new System.Drawing.Size(500, 909);
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
            this.xInput_ShowCompletedTasks.Size = new System.Drawing.Size(500, 37);
            this.xInput_ShowCompletedTasks.TabIndex = 6;
            this.xInput_ShowCompletedTasks.Value = null;
            this.xInput_ShowCompletedTasks.ValueMember = "Value";
            // 
            // metroPanel_AdviceRecord
            // 
            this.metroPanel_AdviceRecord.AutoScroll = true;
            this.metroPanel_AdviceRecord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_MedicalCover);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalCoverHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalCover);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_MedicalConditions);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalConditions);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalConditionsHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MotivationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Motivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_RecomendedFunds);
            this.metroPanel_AdviceRecord.Controls.Add(this.label_InitialAdviceHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.label_InitialAdvice);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_RecommendedFund);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherInformation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Motivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_FinancialSolution);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_FinancialSolutionHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_FinancialSolution);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_needAndObj);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Summary);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_AccessCapital);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_PKE);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_InvestHorizen);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_NeedsAndObjHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_NeedsAndObj);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_AccessToCapitalHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_AccessToCapital);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_InvestHorizonHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_InvestHorizen);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_PKEHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_PKE);
            this.metroPanel_AdviceRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel_AdviceRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroPanel_AdviceRecord.HorizontalScrollbar = true;
            this.metroPanel_AdviceRecord.HorizontalScrollbarBarColor = true;
            this.metroPanel_AdviceRecord.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_AdviceRecord.HorizontalScrollbarSize = 12;
            this.metroPanel_AdviceRecord.Location = new System.Drawing.Point(0, 74);
            this.metroPanel_AdviceRecord.Margin = new System.Windows.Forms.Padding(4);
            this.metroPanel_AdviceRecord.Name = "metroPanel_AdviceRecord";
            this.metroPanel_AdviceRecord.Size = new System.Drawing.Size(1180, 872);
            this.metroPanel_AdviceRecord.TabIndex = 9;
            this.metroPanel_AdviceRecord.VerticalScrollbar = true;
            this.metroPanel_AdviceRecord.VerticalScrollbarBarColor = true;
            this.metroPanel_AdviceRecord.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_AdviceRecord.VerticalScrollbarSize = 13;
            this.metroPanel_AdviceRecord.Paint += new System.Windows.Forms.PaintEventHandler(this.metroPanel_PolicyNote_Paint);
            // 
            // metroPanel_MedicalCover
            // 
            this.metroPanel_MedicalCover.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_MedicalCover.Controls.Add(this.metroTextBox10);
            this.metroPanel_MedicalCover.HorizontalScrollbarBarColor = true;
            this.metroPanel_MedicalCover.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_MedicalCover.HorizontalScrollbarSize = 10;
            this.metroPanel_MedicalCover.Location = new System.Drawing.Point(85, 1483);
            this.metroPanel_MedicalCover.Name = "metroPanel_MedicalCover";
            this.metroPanel_MedicalCover.Size = new System.Drawing.Size(972, 96);
            this.metroPanel_MedicalCover.TabIndex = 33;
            this.metroPanel_MedicalCover.VerticalScrollbarBarColor = true;
            this.metroPanel_MedicalCover.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_MedicalCover.VerticalScrollbarSize = 10;
            // 
            // metroTextBox10
            // 
            // 
            // 
            // 
            this.metroTextBox10.CustomButton.Image = null;
            this.metroTextBox10.CustomButton.Location = new System.Drawing.Point(878, 2);
            this.metroTextBox10.CustomButton.Name = "";
            this.metroTextBox10.CustomButton.Size = new System.Drawing.Size(85, 85);
            this.metroTextBox10.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox10.CustomButton.TabIndex = 1;
            this.metroTextBox10.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox10.CustomButton.UseSelectable = true;
            this.metroTextBox10.CustomButton.Visible = false;
            this.metroTextBox10.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox10.Lines = new string[0];
            this.metroTextBox10.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox10.MaxLength = 32767;
            this.metroTextBox10.Multiline = true;
            this.metroTextBox10.Name = "metroTextBox10";
            this.metroTextBox10.PasswordChar = '\0';
            this.metroTextBox10.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox10.SelectedText = "";
            this.metroTextBox10.SelectionLength = 0;
            this.metroTextBox10.SelectionStart = 0;
            this.metroTextBox10.ShortcutsEnabled = true;
            this.metroTextBox10.Size = new System.Drawing.Size(966, 90);
            this.metroTextBox10.TabIndex = 2;
            this.metroTextBox10.UseCustomBackColor = true;
            this.metroTextBox10.UseSelectable = true;
            this.metroTextBox10.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox10.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_MedicalCoverHint
            // 
            this.metroLabel_MedicalCoverHint.AutoSize = true;
            this.metroLabel_MedicalCoverHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_MedicalCoverHint.Location = new System.Drawing.Point(249, 1630);
            this.metroLabel_MedicalCoverHint.Name = "metroLabel_MedicalCoverHint";
            this.metroLabel_MedicalCoverHint.Size = new System.Drawing.Size(648, 40);
            this.metroLabel_MedicalCoverHint.TabIndex = 35;
            this.metroLabel_MedicalCoverHint.Text = "(Information on the client\'s current medical scheme and benifits, period of unint" +
    "erupted cover, period of\r\nany breakage in cover etc.)";
            this.metroLabel_MedicalCoverHint.UseCustomForeColor = true;
            // 
            // metroLabel_MedicalCover
            // 
            this.metroLabel_MedicalCover.AutoSize = true;
            this.metroLabel_MedicalCover.Location = new System.Drawing.Point(798, 1795);
            this.metroLabel_MedicalCover.Name = "metroLabel_MedicalCover";
            this.metroLabel_MedicalCover.Size = new System.Drawing.Size(153, 20);
            this.metroLabel_MedicalCover.TabIndex = 34;
            this.metroLabel_MedicalCover.Text = "Current Medical Cover:";
            // 
            // metroPanel_MedicalConditions
            // 
            this.metroPanel_MedicalConditions.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_MedicalConditions.Controls.Add(this.metroTextBox_MedicalConditions);
            this.metroPanel_MedicalConditions.HorizontalScrollbarBarColor = true;
            this.metroPanel_MedicalConditions.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_MedicalConditions.HorizontalScrollbarSize = 10;
            this.metroPanel_MedicalConditions.Location = new System.Drawing.Point(761, 1373);
            this.metroPanel_MedicalConditions.Name = "metroPanel_MedicalConditions";
            this.metroPanel_MedicalConditions.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_MedicalConditions.TabIndex = 32;
            this.metroPanel_MedicalConditions.VerticalScrollbarBarColor = true;
            this.metroPanel_MedicalConditions.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_MedicalConditions.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_MedicalConditions
            // 
            // 
            // 
            // 
            this.metroTextBox_MedicalConditions.CustomButton.Image = null;
            this.metroTextBox_MedicalConditions.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_MedicalConditions.CustomButton.Name = "";
            this.metroTextBox_MedicalConditions.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_MedicalConditions.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_MedicalConditions.CustomButton.TabIndex = 1;
            this.metroTextBox_MedicalConditions.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_MedicalConditions.CustomButton.UseSelectable = true;
            this.metroTextBox_MedicalConditions.CustomButton.Visible = false;
            this.metroTextBox_MedicalConditions.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_MedicalConditions.Lines = new string[0];
            this.metroTextBox_MedicalConditions.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_MedicalConditions.MaxLength = 32767;
            this.metroTextBox_MedicalConditions.Multiline = true;
            this.metroTextBox_MedicalConditions.Name = "metroTextBox_MedicalConditions";
            this.metroTextBox_MedicalConditions.PasswordChar = '\0';
            this.metroTextBox_MedicalConditions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_MedicalConditions.SelectedText = "";
            this.metroTextBox_MedicalConditions.SelectionLength = 0;
            this.metroTextBox_MedicalConditions.SelectionStart = 0;
            this.metroTextBox_MedicalConditions.ShortcutsEnabled = true;
            this.metroTextBox_MedicalConditions.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_MedicalConditions.TabIndex = 2;
            this.metroTextBox_MedicalConditions.UseCustomBackColor = true;
            this.metroTextBox_MedicalConditions.UseSelectable = true;
            this.metroTextBox_MedicalConditions.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_MedicalConditions.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_MedicalConditions
            // 
            this.metroLabel_MedicalConditions.AutoSize = true;
            this.metroLabel_MedicalConditions.Location = new System.Drawing.Point(615, 1460);
            this.metroLabel_MedicalConditions.Name = "metroLabel_MedicalConditions";
            this.metroLabel_MedicalConditions.Size = new System.Drawing.Size(129, 20);
            this.metroLabel_MedicalConditions.TabIndex = 33;
            this.metroLabel_MedicalConditions.Text = "Medical Conditions:";
            // 
            // metroLabel_MedicalConditionsHint
            // 
            this.metroLabel_MedicalConditionsHint.AutoSize = true;
            this.metroLabel_MedicalConditionsHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_MedicalConditionsHint.Location = new System.Drawing.Point(71, 1411);
            this.metroLabel_MedicalConditionsHint.Name = "metroLabel_MedicalConditionsHint";
            this.metroLabel_MedicalConditionsHint.Size = new System.Drawing.Size(547, 20);
            this.metroLabel_MedicalConditionsHint.TabIndex = 32;
            this.metroLabel_MedicalConditionsHint.Text = "(Chronic conditions, chronic medications, other medical conditions and medication" +
    " etc.)";
            this.metroLabel_MedicalConditionsHint.UseCustomForeColor = true;
            // 
            // metroLabel_MotivationHint
            // 
            this.metroLabel_MotivationHint.AutoSize = true;
            this.metroLabel_MotivationHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_MotivationHint.Location = new System.Drawing.Point(30, 1167);
            this.metroLabel_MotivationHint.Name = "metroLabel_MotivationHint";
            this.metroLabel_MotivationHint.Size = new System.Drawing.Size(769, 60);
            this.metroLabel_MotivationHint.TabIndex = 31;
            this.metroLabel_MotivationHint.Text = resources.GetString("metroLabel_MotivationHint.Text");
            this.metroLabel_MotivationHint.UseCustomForeColor = true;
            // 
            // metroLabel_Motivation
            // 
            this.metroLabel_Motivation.AutoSize = true;
            this.metroLabel_Motivation.Location = new System.Drawing.Point(30, 1147);
            this.metroLabel_Motivation.Name = "metroLabel_Motivation";
            this.metroLabel_Motivation.Size = new System.Drawing.Size(216, 20);
            this.metroLabel_Motivation.TabIndex = 30;
            this.metroLabel_Motivation.Text = "Motivation for Recommendations:";
            // 
            // metroPanel_RecomendedFunds
            // 
            this.metroPanel_RecomendedFunds.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_RecomendedFunds.Controls.Add(this.metroPanel7);
            this.metroPanel_RecomendedFunds.Controls.Add(this.metroTextBox7);
            this.metroPanel_RecomendedFunds.HorizontalScrollbarBarColor = true;
            this.metroPanel_RecomendedFunds.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_RecomendedFunds.HorizontalScrollbarSize = 10;
            this.metroPanel_RecomendedFunds.Location = new System.Drawing.Point(35, 1047);
            this.metroPanel_RecomendedFunds.Name = "metroPanel_RecomendedFunds";
            this.metroPanel_RecomendedFunds.Size = new System.Drawing.Size(954, 84);
            this.metroPanel_RecomendedFunds.TabIndex = 25;
            this.metroPanel_RecomendedFunds.VerticalScrollbarBarColor = true;
            this.metroPanel_RecomendedFunds.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_RecomendedFunds.VerticalScrollbarSize = 10;
            // 
            // metroPanel7
            // 
            this.metroPanel7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel7.Controls.Add(this.metroTextBox6);
            this.metroPanel7.HorizontalScrollbarBarColor = true;
            this.metroPanel7.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel7.HorizontalScrollbarSize = 10;
            this.metroPanel7.Location = new System.Drawing.Point(15, 1011);
            this.metroPanel7.Name = "metroPanel7";
            this.metroPanel7.Size = new System.Drawing.Size(1055, 96);
            this.metroPanel7.TabIndex = 22;
            this.metroPanel7.VerticalScrollbarBarColor = true;
            this.metroPanel7.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel7.VerticalScrollbarSize = 10;
            // 
            // metroTextBox6
            // 
            // 
            // 
            // 
            this.metroTextBox6.CustomButton.Image = null;
            this.metroTextBox6.CustomButton.Location = new System.Drawing.Point(961, 2);
            this.metroTextBox6.CustomButton.Name = "";
            this.metroTextBox6.CustomButton.Size = new System.Drawing.Size(85, 85);
            this.metroTextBox6.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox6.CustomButton.TabIndex = 1;
            this.metroTextBox6.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox6.CustomButton.UseSelectable = true;
            this.metroTextBox6.CustomButton.Visible = false;
            this.metroTextBox6.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox6.Lines = new string[0];
            this.metroTextBox6.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox6.MaxLength = 32767;
            this.metroTextBox6.Multiline = true;
            this.metroTextBox6.Name = "metroTextBox6";
            this.metroTextBox6.PasswordChar = '\0';
            this.metroTextBox6.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox6.SelectedText = "";
            this.metroTextBox6.SelectionLength = 0;
            this.metroTextBox6.SelectionStart = 0;
            this.metroTextBox6.ShortcutsEnabled = true;
            this.metroTextBox6.Size = new System.Drawing.Size(1049, 90);
            this.metroTextBox6.TabIndex = 2;
            this.metroTextBox6.UseCustomBackColor = true;
            this.metroTextBox6.UseSelectable = true;
            this.metroTextBox6.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox6.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox7
            // 
            // 
            // 
            // 
            this.metroTextBox7.CustomButton.Image = null;
            this.metroTextBox7.CustomButton.Location = new System.Drawing.Point(872, 2);
            this.metroTextBox7.CustomButton.Name = "";
            this.metroTextBox7.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox7.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox7.CustomButton.TabIndex = 1;
            this.metroTextBox7.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox7.CustomButton.UseSelectable = true;
            this.metroTextBox7.CustomButton.Visible = false;
            this.metroTextBox7.Lines = new string[0];
            this.metroTextBox7.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox7.MaxLength = 32767;
            this.metroTextBox7.Multiline = true;
            this.metroTextBox7.Name = "metroTextBox7";
            this.metroTextBox7.PasswordChar = '\0';
            this.metroTextBox7.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox7.SelectedText = "";
            this.metroTextBox7.SelectionLength = 0;
            this.metroTextBox7.SelectionStart = 0;
            this.metroTextBox7.ShortcutsEnabled = true;
            this.metroTextBox7.Size = new System.Drawing.Size(948, 78);
            this.metroTextBox7.TabIndex = 2;
            this.metroTextBox7.UseCustomBackColor = true;
            this.metroTextBox7.UseCustomForeColor = true;
            this.metroTextBox7.UseSelectable = true;
            this.metroTextBox7.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox7.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label_InitialAdviceHint
            // 
            this.label_InitialAdviceHint.AutoSize = true;
            this.label_InitialAdviceHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_InitialAdviceHint.ForeColor = System.Drawing.Color.Gray;
            this.label_InitialAdviceHint.Location = new System.Drawing.Point(10, 983);
            this.label_InitialAdviceHint.Name = "label_InitialAdviceHint";
            this.label_InitialAdviceHint.Size = new System.Drawing.Size(334, 20);
            this.label_InitialAdviceHint.TabIndex = 29;
            this.label_InitialAdviceHint.Text = "(Ensure all needs identified are addressed.)";
            // 
            // label_InitialAdvice
            // 
            this.label_InitialAdvice.AutoSize = true;
            this.label_InitialAdvice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_InitialAdvice.Location = new System.Drawing.Point(10, 963);
            this.label_InitialAdvice.Name = "label_InitialAdvice";
            this.label_InitialAdvice.Size = new System.Drawing.Size(279, 25);
            this.label_InitialAdvice.TabIndex = 28;
            this.label_InitialAdvice.Text = "Initial Recommendation/Advice";
            this.label_InitialAdvice.Click += new System.EventHandler(this.label18_Click);
            // 
            // metroLabel_RecommendedFund
            // 
            this.metroLabel_RecommendedFund.AutoSize = true;
            this.metroLabel_RecommendedFund.Location = new System.Drawing.Point(30, 1015);
            this.metroLabel_RecommendedFund.Name = "metroLabel_RecommendedFund";
            this.metroLabel_RecommendedFund.Size = new System.Drawing.Size(198, 20);
            this.metroLabel_RecommendedFund.TabIndex = 27;
            this.metroLabel_RecommendedFund.Text = "Product/Funds recommended:";
            // 
            // metroLabel_OtherInformationHint
            // 
            this.metroLabel_OtherInformationHint.AutoSize = true;
            this.metroLabel_OtherInformationHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_OtherInformationHint.Location = new System.Drawing.Point(10, 831);
            this.metroLabel_OtherInformationHint.Name = "metroLabel_OtherInformationHint";
            this.metroLabel_OtherInformationHint.Size = new System.Drawing.Size(684, 20);
            this.metroLabel_OtherInformationHint.TabIndex = 26;
            this.metroLabel_OtherInformationHint.Text = "(The client\'s retirement age, premium increases, expected growth rates, tax consi" +
    "derations, specific goals etc.)";
            this.metroLabel_OtherInformationHint.UseCustomForeColor = true;
            // 
            // metroLabel_OtherInformation
            // 
            this.metroLabel_OtherInformation.AutoSize = true;
            this.metroLabel_OtherInformation.Location = new System.Drawing.Point(10, 811);
            this.metroLabel_OtherInformation.Name = "metroLabel_OtherInformation";
            this.metroLabel_OtherInformation.Size = new System.Drawing.Size(122, 20);
            this.metroLabel_OtherInformation.TabIndex = 25;
            this.metroLabel_OtherInformation.Text = "Other Information:";
            // 
            // metroPanel_OtherInformation
            // 
            this.metroPanel_OtherInformation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_OtherInformation.Controls.Add(this.metroPanel5);
            this.metroPanel_OtherInformation.Controls.Add(this.metroTextBox5);
            this.metroPanel_OtherInformation.HorizontalScrollbarBarColor = true;
            this.metroPanel_OtherInformation.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_OtherInformation.HorizontalScrollbarSize = 10;
            this.metroPanel_OtherInformation.Location = new System.Drawing.Point(15, 863);
            this.metroPanel_OtherInformation.Name = "metroPanel_OtherInformation";
            this.metroPanel_OtherInformation.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_OtherInformation.TabIndex = 24;
            this.metroPanel_OtherInformation.VerticalScrollbarBarColor = true;
            this.metroPanel_OtherInformation.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_OtherInformation.VerticalScrollbarSize = 10;
            // 
            // metroPanel5
            // 
            this.metroPanel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel5.Controls.Add(this.metroTextBox4);
            this.metroPanel5.HorizontalScrollbarBarColor = true;
            this.metroPanel5.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel5.HorizontalScrollbarSize = 10;
            this.metroPanel5.Location = new System.Drawing.Point(15, 1011);
            this.metroPanel5.Name = "metroPanel5";
            this.metroPanel5.Size = new System.Drawing.Size(1055, 96);
            this.metroPanel5.TabIndex = 22;
            this.metroPanel5.VerticalScrollbarBarColor = true;
            this.metroPanel5.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel5.VerticalScrollbarSize = 10;
            // 
            // metroTextBox4
            // 
            // 
            // 
            // 
            this.metroTextBox4.CustomButton.Image = null;
            this.metroTextBox4.CustomButton.Location = new System.Drawing.Point(961, 2);
            this.metroTextBox4.CustomButton.Name = "";
            this.metroTextBox4.CustomButton.Size = new System.Drawing.Size(85, 85);
            this.metroTextBox4.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox4.CustomButton.TabIndex = 1;
            this.metroTextBox4.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox4.CustomButton.UseSelectable = true;
            this.metroTextBox4.CustomButton.Visible = false;
            this.metroTextBox4.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox4.Lines = new string[0];
            this.metroTextBox4.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox4.MaxLength = 32767;
            this.metroTextBox4.Multiline = true;
            this.metroTextBox4.Name = "metroTextBox4";
            this.metroTextBox4.PasswordChar = '\0';
            this.metroTextBox4.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox4.SelectedText = "";
            this.metroTextBox4.SelectionLength = 0;
            this.metroTextBox4.SelectionStart = 0;
            this.metroTextBox4.ShortcutsEnabled = true;
            this.metroTextBox4.Size = new System.Drawing.Size(1049, 90);
            this.metroTextBox4.TabIndex = 2;
            this.metroTextBox4.UseCustomBackColor = true;
            this.metroTextBox4.UseSelectable = true;
            this.metroTextBox4.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox4.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox5
            // 
            // 
            // 
            // 
            this.metroTextBox5.CustomButton.Image = null;
            this.metroTextBox5.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox5.CustomButton.Name = "";
            this.metroTextBox5.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox5.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox5.CustomButton.TabIndex = 1;
            this.metroTextBox5.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox5.CustomButton.UseSelectable = true;
            this.metroTextBox5.CustomButton.Visible = false;
            this.metroTextBox5.Lines = new string[0];
            this.metroTextBox5.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox5.MaxLength = 32767;
            this.metroTextBox5.Multiline = true;
            this.metroTextBox5.Name = "metroTextBox5";
            this.metroTextBox5.PasswordChar = '\0';
            this.metroTextBox5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox5.SelectedText = "";
            this.metroTextBox5.SelectionLength = 0;
            this.metroTextBox5.SelectionStart = 0;
            this.metroTextBox5.ShortcutsEnabled = true;
            this.metroTextBox5.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox5.TabIndex = 2;
            this.metroTextBox5.UseCustomBackColor = true;
            this.metroTextBox5.UseCustomForeColor = true;
            this.metroTextBox5.UseSelectable = true;
            this.metroTextBox5.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox5.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroPanel_Motivation
            // 
            this.metroPanel_Motivation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_Motivation.Controls.Add(this.metroPanel3);
            this.metroPanel_Motivation.Controls.Add(this.metroTextBox3);
            this.metroPanel_Motivation.HorizontalScrollbarBarColor = true;
            this.metroPanel_Motivation.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Motivation.HorizontalScrollbarSize = 10;
            this.metroPanel_Motivation.Location = new System.Drawing.Point(35, 1239);
            this.metroPanel_Motivation.Name = "metroPanel_Motivation";
            this.metroPanel_Motivation.Size = new System.Drawing.Size(954, 84);
            this.metroPanel_Motivation.TabIndex = 23;
            this.metroPanel_Motivation.VerticalScrollbarBarColor = true;
            this.metroPanel_Motivation.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Motivation.VerticalScrollbarSize = 10;
            // 
            // metroPanel3
            // 
            this.metroPanel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel3.Controls.Add(this.metroTextBox2);
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(15, 1011);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(1055, 96);
            this.metroPanel3.TabIndex = 22;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // metroTextBox2
            // 
            // 
            // 
            // 
            this.metroTextBox2.CustomButton.Image = null;
            this.metroTextBox2.CustomButton.Location = new System.Drawing.Point(961, 2);
            this.metroTextBox2.CustomButton.Name = "";
            this.metroTextBox2.CustomButton.Size = new System.Drawing.Size(85, 85);
            this.metroTextBox2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox2.CustomButton.TabIndex = 1;
            this.metroTextBox2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox2.CustomButton.UseSelectable = true;
            this.metroTextBox2.CustomButton.Visible = false;
            this.metroTextBox2.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox2.Lines = new string[0];
            this.metroTextBox2.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox2.MaxLength = 32767;
            this.metroTextBox2.Multiline = true;
            this.metroTextBox2.Name = "metroTextBox2";
            this.metroTextBox2.PasswordChar = '\0';
            this.metroTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox2.SelectedText = "";
            this.metroTextBox2.SelectionLength = 0;
            this.metroTextBox2.SelectionStart = 0;
            this.metroTextBox2.ShortcutsEnabled = true;
            this.metroTextBox2.Size = new System.Drawing.Size(1049, 90);
            this.metroTextBox2.TabIndex = 2;
            this.metroTextBox2.UseCustomBackColor = true;
            this.metroTextBox2.UseSelectable = true;
            this.metroTextBox2.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox2.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox3
            // 
            // 
            // 
            // 
            this.metroTextBox3.CustomButton.Image = null;
            this.metroTextBox3.CustomButton.Location = new System.Drawing.Point(872, 2);
            this.metroTextBox3.CustomButton.Name = "";
            this.metroTextBox3.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox3.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox3.CustomButton.TabIndex = 1;
            this.metroTextBox3.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox3.CustomButton.UseSelectable = true;
            this.metroTextBox3.CustomButton.Visible = false;
            this.metroTextBox3.Lines = new string[0];
            this.metroTextBox3.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox3.MaxLength = 32767;
            this.metroTextBox3.Multiline = true;
            this.metroTextBox3.Name = "metroTextBox3";
            this.metroTextBox3.PasswordChar = '\0';
            this.metroTextBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox3.SelectedText = "";
            this.metroTextBox3.SelectionLength = 0;
            this.metroTextBox3.SelectionStart = 0;
            this.metroTextBox3.ShortcutsEnabled = true;
            this.metroTextBox3.Size = new System.Drawing.Size(948, 78);
            this.metroTextBox3.TabIndex = 2;
            this.metroTextBox3.UseCustomBackColor = true;
            this.metroTextBox3.UseCustomForeColor = true;
            this.metroTextBox3.UseSelectable = true;
            this.metroTextBox3.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox3.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroPanel_FinancialSolution
            // 
            this.metroPanel_FinancialSolution.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_FinancialSolution.Controls.Add(this.metroPanel1);
            this.metroPanel_FinancialSolution.Controls.Add(this.metroTextBox_FinancialSolution);
            this.metroPanel_FinancialSolution.HorizontalScrollbarBarColor = true;
            this.metroPanel_FinancialSolution.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_FinancialSolution.HorizontalScrollbarSize = 10;
            this.metroPanel_FinancialSolution.Location = new System.Drawing.Point(15, 711);
            this.metroPanel_FinancialSolution.Name = "metroPanel_FinancialSolution";
            this.metroPanel_FinancialSolution.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_FinancialSolution.TabIndex = 20;
            this.metroPanel_FinancialSolution.VerticalScrollbarBarColor = true;
            this.metroPanel_FinancialSolution.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_FinancialSolution.VerticalScrollbarSize = 10;
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel1.Controls.Add(this.metroTextBox1);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(15, 1011);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1055, 96);
            this.metroPanel1.TabIndex = 22;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroTextBox1
            // 
            // 
            // 
            // 
            this.metroTextBox1.CustomButton.Image = null;
            this.metroTextBox1.CustomButton.Location = new System.Drawing.Point(961, 2);
            this.metroTextBox1.CustomButton.Name = "";
            this.metroTextBox1.CustomButton.Size = new System.Drawing.Size(85, 85);
            this.metroTextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox1.CustomButton.TabIndex = 1;
            this.metroTextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox1.CustomButton.UseSelectable = true;
            this.metroTextBox1.CustomButton.Visible = false;
            this.metroTextBox1.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox1.Lines = new string[0];
            this.metroTextBox1.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox1.MaxLength = 32767;
            this.metroTextBox1.Multiline = true;
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.PasswordChar = '\0';
            this.metroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox1.SelectedText = "";
            this.metroTextBox1.SelectionLength = 0;
            this.metroTextBox1.SelectionStart = 0;
            this.metroTextBox1.ShortcutsEnabled = true;
            this.metroTextBox1.Size = new System.Drawing.Size(1049, 90);
            this.metroTextBox1.TabIndex = 2;
            this.metroTextBox1.UseCustomBackColor = true;
            this.metroTextBox1.UseSelectable = true;
            this.metroTextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox_FinancialSolution
            // 
            // 
            // 
            // 
            this.metroTextBox_FinancialSolution.CustomButton.Image = null;
            this.metroTextBox_FinancialSolution.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_FinancialSolution.CustomButton.Name = "";
            this.metroTextBox_FinancialSolution.CustomButton.Size = new System.Drawing.Size(73, 73);
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
            this.metroTextBox_FinancialSolution.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_FinancialSolution.SelectedText = "";
            this.metroTextBox_FinancialSolution.SelectionLength = 0;
            this.metroTextBox_FinancialSolution.SelectionStart = 0;
            this.metroTextBox_FinancialSolution.ShortcutsEnabled = true;
            this.metroTextBox_FinancialSolution.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_FinancialSolution.TabIndex = 2;
            this.metroTextBox_FinancialSolution.UseCustomBackColor = true;
            this.metroTextBox_FinancialSolution.UseCustomForeColor = true;
            this.metroTextBox_FinancialSolution.UseSelectable = true;
            this.metroTextBox_FinancialSolution.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_FinancialSolution.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_FinancialSolutionHint
            // 
            this.metroLabel_FinancialSolutionHint.AutoSize = true;
            this.metroLabel_FinancialSolutionHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_FinancialSolutionHint.Location = new System.Drawing.Point(10, 679);
            this.metroLabel_FinancialSolutionHint.Name = "metroLabel_FinancialSolutionHint";
            this.metroLabel_FinancialSolutionHint.Size = new System.Drawing.Size(368, 20);
            this.metroLabel_FinancialSolutionHint.TabIndex = 21;
            this.metroLabel_FinancialSolutionHint.Text = "(Set out summary of the client\'s current financial situation.)";
            this.metroLabel_FinancialSolutionHint.UseCustomForeColor = true;
            // 
            // metroLabel_FinancialSolution
            // 
            this.metroLabel_FinancialSolution.AutoSize = true;
            this.metroLabel_FinancialSolution.Location = new System.Drawing.Point(10, 659);
            this.metroLabel_FinancialSolution.Name = "metroLabel_FinancialSolution";
            this.metroLabel_FinancialSolution.Size = new System.Drawing.Size(121, 20);
            this.metroLabel_FinancialSolution.TabIndex = 20;
            this.metroLabel_FinancialSolution.Text = "Financial Situation:";
            // 
            // metroPanel_needAndObj
            // 
            this.metroPanel_needAndObj.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_needAndObj.Controls.Add(this.metroTextBox_NeedAndObjective);
            this.metroPanel_needAndObj.HorizontalScrollbarBarColor = true;
            this.metroPanel_needAndObj.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_needAndObj.HorizontalScrollbarSize = 10;
            this.metroPanel_needAndObj.Location = new System.Drawing.Point(15, 559);
            this.metroPanel_needAndObj.Name = "metroPanel_needAndObj";
            this.metroPanel_needAndObj.Size = new System.Drawing.Size(972, 84);
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
            this.metroTextBox_NeedAndObjective.CustomButton.Location = new System.Drawing.Point(890, 1);
            this.metroTextBox_NeedAndObjective.CustomButton.Name = "";
            this.metroTextBox_NeedAndObjective.CustomButton.Size = new System.Drawing.Size(75, 75);
            this.metroTextBox_NeedAndObjective.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_NeedAndObjective.CustomButton.TabIndex = 1;
            this.metroTextBox_NeedAndObjective.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_NeedAndObjective.CustomButton.UseSelectable = true;
            this.metroTextBox_NeedAndObjective.CustomButton.Visible = false;
            this.metroTextBox_NeedAndObjective.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_NeedAndObjective.Lines = new string[0];
            this.metroTextBox_NeedAndObjective.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_NeedAndObjective.MaxLength = 32767;
            this.metroTextBox_NeedAndObjective.Multiline = true;
            this.metroTextBox_NeedAndObjective.Name = "metroTextBox_NeedAndObjective";
            this.metroTextBox_NeedAndObjective.PasswordChar = '\0';
            this.metroTextBox_NeedAndObjective.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_NeedAndObjective.SelectedText = "";
            this.metroTextBox_NeedAndObjective.SelectionLength = 0;
            this.metroTextBox_NeedAndObjective.SelectionStart = 0;
            this.metroTextBox_NeedAndObjective.ShortcutsEnabled = true;
            this.metroTextBox_NeedAndObjective.Size = new System.Drawing.Size(966, 77);
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
            this.metroLabel_Summary.Location = new System.Drawing.Point(316, 3);
            this.metroLabel_Summary.Name = "metroLabel_Summary";
            this.metroLabel_Summary.Size = new System.Drawing.Size(439, 32);
            this.metroLabel_Summary.TabIndex = 18;
            this.metroLabel_Summary.Text = "Summary of Clients Information";
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
            this.metroPanel_AccessCapital.Location = new System.Drawing.Point(15, 407);
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
            this.metroPanel_PKE.Location = new System.Drawing.Point(15, 103);
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
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(655, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 40);
            this.label9.TabIndex = 26;
            this.label9.Text = "9";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(572, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 40);
            this.label8.TabIndex = 25;
            this.label8.Text = "8";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(492, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 40);
            this.label7.TabIndex = 24;
            this.label7.Text = "7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(411, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 40);
            this.label6.TabIndex = 23;
            this.label6.Text = "6";
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
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(247, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 40);
            this.label4.TabIndex = 21;
            this.label4.Text = "4";
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
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(165, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 40);
            this.label3.TabIndex = 20;
            this.label3.Text = "3";
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
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 40);
            this.label2.TabIndex = 19;
            this.label2.Text = "2";
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
            this.metroPanel_InvestHorizen.Location = new System.Drawing.Point(15, 255);
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
            this.metroLabel_NeedsAndObjHint.Location = new System.Drawing.Point(10, 527);
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
            this.metroLabel_NeedsAndObj.Location = new System.Drawing.Point(10, 507);
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
            this.metroLabel_AccessToCapitalHint.Location = new System.Drawing.Point(10, 375);
            this.metroLabel_AccessToCapitalHint.Name = "metroLabel_AccessToCapitalHint";
            this.metroLabel_AccessToCapitalHint.Size = new System.Drawing.Size(314, 20);
            this.metroLabel_AccessToCapitalHint.TabIndex = 12;
            this.metroLabel_AccessToCapitalHint.Text = "(The accessibility and liquidity of the invesement.)";
            this.metroLabel_AccessToCapitalHint.UseCustomForeColor = true;
            // 
            // metroLabel_AccessToCapital
            // 
            this.metroLabel_AccessToCapital.AutoSize = true;
            this.metroLabel_AccessToCapital.Location = new System.Drawing.Point(10, 355);
            this.metroLabel_AccessToCapital.Name = "metroLabel_AccessToCapital";
            this.metroLabel_AccessToCapital.Size = new System.Drawing.Size(116, 20);
            this.metroLabel_AccessToCapital.TabIndex = 11;
            this.metroLabel_AccessToCapital.Text = "Access to Capital:";
            // 
            // metroLabel_InvestHorizonHint
            // 
            this.metroLabel_InvestHorizonHint.AutoSize = true;
            this.metroLabel_InvestHorizonHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_InvestHorizonHint.Location = new System.Drawing.Point(10, 223);
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
            this.metroLabel_InvestHorizen.Location = new System.Drawing.Point(10, 203);
            this.metroLabel_InvestHorizen.Name = "metroLabel_InvestHorizen";
            this.metroLabel_InvestHorizen.Size = new System.Drawing.Size(133, 20);
            this.metroLabel_InvestHorizen.TabIndex = 9;
            this.metroLabel_InvestHorizen.Text = "Investment Horizon:";
            // 
            // metroLabel_PKEHint
            // 
            this.metroLabel_PKEHint.AutoSize = true;
            this.metroLabel_PKEHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_PKEHint.Location = new System.Drawing.Point(10, 71);
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
            this.metroLabel_PKE.Location = new System.Drawing.Point(10, 51);
            this.metroLabel_PKE.Name = "metroLabel_PKE";
            this.metroLabel_PKE.Size = new System.Drawing.Size(233, 20);
            this.metroLabel_PKE.TabIndex = 7;
            this.metroLabel_PKE.Text = "Product Knowledge and Experience:";
            // 
            // metroPanel_Select
            // 
            this.metroPanel_Select.Controls.Add(this.metroCheckBox_completed);
            this.metroPanel_Select.Controls.Add(this.label_ongoing);
            this.metroPanel_Select.Controls.Add(this.label_Initial);
            this.metroPanel_Select.Controls.Add(this.label_Fees);
            this.metroPanel_Select.Controls.Add(this.metroTextBox9);
            this.metroPanel_Select.Controls.Add(this.metroTextBox8);
            this.metroPanel_Select.Controls.Add(this.metroTextBox_NoteDate);
            this.metroPanel_Select.Controls.Add(this.label_Date);
            this.metroPanel_Select.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel_Select.HorizontalScrollbarBarColor = true;
            this.metroPanel_Select.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Select.HorizontalScrollbarSize = 12;
            this.metroPanel_Select.Location = new System.Drawing.Point(0, 0);
            this.metroPanel_Select.Margin = new System.Windows.Forms.Padding(4);
            this.metroPanel_Select.Name = "metroPanel_Select";
            this.metroPanel_Select.Size = new System.Drawing.Size(1180, 74);
            this.metroPanel_Select.TabIndex = 8;
            this.metroPanel_Select.VerticalScrollbarBarColor = true;
            this.metroPanel_Select.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Select.VerticalScrollbarSize = 13;
            // 
            // metroCheckBox_completed
            // 
            this.metroCheckBox_completed.AutoSize = true;
            this.metroCheckBox_completed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.metroCheckBox_completed.Location = new System.Drawing.Point(828, 10);
            this.metroCheckBox_completed.Name = "metroCheckBox_completed";
            this.metroCheckBox_completed.Size = new System.Drawing.Size(88, 17);
            this.metroCheckBox_completed.TabIndex = 9;
            this.metroCheckBox_completed.Text = "Completed";
            this.metroCheckBox_completed.UseSelectable = true;
            // 
            // label_ongoing
            // 
            this.label_ongoing.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label_ongoing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_ongoing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ongoing.Location = new System.Drawing.Point(522, 7);
            this.label_ongoing.Name = "label_ongoing";
            this.label_ongoing.Size = new System.Drawing.Size(100, 30);
            this.label_ongoing.TabIndex = 8;
            this.label_ongoing.Text = "Ongoing";
            this.label_ongoing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Initial
            // 
            this.label_Initial.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label_Initial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Initial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Initial.Location = new System.Drawing.Point(308, 7);
            this.label_Initial.Name = "label_Initial";
            this.label_Initial.Size = new System.Drawing.Size(100, 30);
            this.label_Initial.TabIndex = 7;
            this.label_Initial.Text = "Initial";
            this.label_Initial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Fees
            // 
            this.label_Fees.Location = new System.Drawing.Point(260, 15);
            this.label_Fees.Name = "label_Fees";
            this.label_Fees.Size = new System.Drawing.Size(65, 23);
            this.label_Fees.TabIndex = 6;
            this.label_Fees.Text = "Fees";
            // 
            // metroTextBox9
            // 
            // 
            // 
            // 
            this.metroTextBox9.CustomButton.Image = null;
            this.metroTextBox9.CustomButton.Location = new System.Drawing.Point(99, 2);
            this.metroTextBox9.CustomButton.Name = "";
            this.metroTextBox9.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBox9.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox9.CustomButton.TabIndex = 1;
            this.metroTextBox9.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox9.CustomButton.UseSelectable = true;
            this.metroTextBox9.CustomButton.Visible = false;
            this.metroTextBox9.Lines = new string[0];
            this.metroTextBox9.Location = new System.Drawing.Point(401, 7);
            this.metroTextBox9.MaxLength = 32767;
            this.metroTextBox9.Name = "metroTextBox9";
            this.metroTextBox9.PasswordChar = '\0';
            this.metroTextBox9.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox9.SelectedText = "";
            this.metroTextBox9.SelectionLength = 0;
            this.metroTextBox9.SelectionStart = 0;
            this.metroTextBox9.ShortcutsEnabled = true;
            this.metroTextBox9.Size = new System.Drawing.Size(127, 30);
            this.metroTextBox9.TabIndex = 5;
            this.metroTextBox9.UseSelectable = true;
            this.metroTextBox9.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox9.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox8
            // 
            // 
            // 
            // 
            this.metroTextBox8.CustomButton.Image = null;
            this.metroTextBox8.CustomButton.Location = new System.Drawing.Point(99, 2);
            this.metroTextBox8.CustomButton.Name = "";
            this.metroTextBox8.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBox8.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox8.CustomButton.TabIndex = 1;
            this.metroTextBox8.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox8.CustomButton.UseSelectable = true;
            this.metroTextBox8.CustomButton.Visible = false;
            this.metroTextBox8.Lines = new string[0];
            this.metroTextBox8.Location = new System.Drawing.Point(616, 7);
            this.metroTextBox8.MaxLength = 32767;
            this.metroTextBox8.Name = "metroTextBox8";
            this.metroTextBox8.PasswordChar = '\0';
            this.metroTextBox8.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox8.SelectedText = "";
            this.metroTextBox8.SelectionLength = 0;
            this.metroTextBox8.SelectionStart = 0;
            this.metroTextBox8.ShortcutsEnabled = true;
            this.metroTextBox8.Size = new System.Drawing.Size(127, 30);
            this.metroTextBox8.TabIndex = 4;
            this.metroTextBox8.UseSelectable = true;
            this.metroTextBox8.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox8.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox_NoteDate
            // 
            // 
            // 
            // 
            this.metroTextBox_NoteDate.CustomButton.Image = null;
            this.metroTextBox_NoteDate.CustomButton.Location = new System.Drawing.Point(99, 2);
            this.metroTextBox_NoteDate.CustomButton.Name = "";
            this.metroTextBox_NoteDate.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBox_NoteDate.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_NoteDate.CustomButton.TabIndex = 1;
            this.metroTextBox_NoteDate.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_NoteDate.CustomButton.UseSelectable = true;
            this.metroTextBox_NoteDate.CustomButton.Visible = false;
            this.metroTextBox_NoteDate.Lines = new string[0];
            this.metroTextBox_NoteDate.Location = new System.Drawing.Point(97, 7);
            this.metroTextBox_NoteDate.MaxLength = 32767;
            this.metroTextBox_NoteDate.Name = "metroTextBox_NoteDate";
            this.metroTextBox_NoteDate.PasswordChar = '\0';
            this.metroTextBox_NoteDate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox_NoteDate.SelectedText = "";
            this.metroTextBox_NoteDate.SelectionLength = 0;
            this.metroTextBox_NoteDate.SelectionStart = 0;
            this.metroTextBox_NoteDate.ShortcutsEnabled = true;
            this.metroTextBox_NoteDate.Size = new System.Drawing.Size(127, 30);
            this.metroTextBox_NoteDate.TabIndex = 3;
            this.metroTextBox_NoteDate.UseSelectable = true;
            this.metroTextBox_NoteDate.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_NoteDate.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label_Date
            // 
            this.label_Date.Location = new System.Drawing.Point(16, 15);
            this.label_Date.Name = "label_Date";
            this.label_Date.Size = new System.Drawing.Size(100, 23);
            this.label_Date.TabIndex = 2;
            this.label_Date.Text = "Note Date...";
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
            this.metroPanel_AdviceRecord.ResumeLayout(false);
            this.metroPanel_AdviceRecord.PerformLayout();
            this.metroPanel_MedicalCover.ResumeLayout(false);
            this.metroPanel_MedicalConditions.ResumeLayout(false);
            this.metroPanel_RecomendedFunds.ResumeLayout(false);
            this.metroPanel7.ResumeLayout(false);
            this.metroPanel_OtherInformation.ResumeLayout(false);
            this.metroPanel5.ResumeLayout(false);
            this.metroPanel_Motivation.ResumeLayout(false);
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel_FinancialSolution.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel_needAndObj.ResumeLayout(false);
            this.metroPanel_AccessCapital.ResumeLayout(false);
            this.metroPanel_PKE.ResumeLayout(false);
            this.metroPanel_InvestHorizen.ResumeLayout(false);
            this.metroPanel_Select.ResumeLayout(false);
            this.metroPanel_Select.PerformLayout();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.xToolBarMenu xToolBarMenu1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroPanel metroPanel_AdviceRecord;
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
        private MetroPanel metroPanel1;
        private MetroTextBox metroTextBox1;
        private Label label_InitialAdvice;
        private MetroLabel metroLabel_RecommendedFund;
        private MetroLabel metroLabel_OtherInformationHint;
        private MetroLabel metroLabel_OtherInformation;
        private MetroPanel metroPanel_OtherInformation;
        private MetroPanel metroPanel5;
        private MetroTextBox metroTextBox4;
        private MetroTextBox metroTextBox5;
        private MetroPanel metroPanel_Motivation;
        private MetroPanel metroPanel3;
        private MetroTextBox metroTextBox2;
        private MetroTextBox metroTextBox3;
        private MetroLabel metroLabel_MotivationHint;
        private MetroLabel metroLabel_Motivation;
        private MetroPanel metroPanel_RecomendedFunds;
        private MetroPanel metroPanel7;
        private MetroTextBox metroTextBox6;
        private MetroTextBox metroTextBox7;
        private Label label_InitialAdviceHint;
        private Label label_ongoing;
        private Label label_Initial;
        private Label label_Fees;
        private MetroTextBox metroTextBox9;
        private MetroTextBox metroTextBox8;
        private MetroTextBox metroTextBox_NoteDate;
        private Label label_Date;
        private MetroCheckBox metroCheckBox_completed;
        private MetroLabel metroLabel_MedicalConditions;
        private MetroLabel metroLabel_MedicalConditionsHint;
        private MetroPanel metroPanel_MedicalConditions;
        private MetroTextBox metroTextBox_MedicalConditions;
        private MetroPanel metroPanel_MedicalCover;
        private MetroTextBox metroTextBox10;
        private MetroLabel metroLabel_MedicalCoverHint;
        private MetroLabel metroLabel_MedicalCover;
    }
}