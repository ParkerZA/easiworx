using Finx.App.Extensions;
using Finx.App.UserControls;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Controls.Ext;
using mshtml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Management.Instrumentation;
using System.Windows.Forms;
using WindowsFormsCalendar;

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
            this.metroPanel_AdviceRecord = new MetroFramework.Controls.MetroPanel();
            this.tblPanel_RiskNeedsAndGoals = new System.Windows.Forms.TableLayoutPanel();
            this.metroLabel_ImplementationMotivation = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_ImplementationMotivationHint = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_ImplementationMotivation = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_ImplementationMotivation = new MetroFramework.Controls.MetroTextBox();
            this.metroPanel_ProductImplemented = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_ProductImplemented = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_ImplementationAdviceHint = new MetroFramework.Controls.MetroLabel();
            this.label_ImplementationAdvice = new System.Windows.Forms.Label();
            this.tblPanel_MedicalSchemeComparison = new System.Windows.Forms.TableLayoutPanel();
            this.metroLabel_ComparisonMedicalSchemeHint = new MetroFramework.Controls.MetroLabel();
            this.label_ComparisonMedicalScheme = new System.Windows.Forms.Label();
            this.metroPanel_Notes = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_Notes = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_Notes = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_OtherImportantInfo = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_OtherImportantInfo = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_productImplemented = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_OtherImportantInfoHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_OtherImportantInfo = new MetroFramework.Controls.MetroLabel();
            this.tblPanel_NeedsAndGoals = new System.Windows.Forms.TableLayoutPanel();
            this.metroLabel_RecommendedProductHint = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_Copayment = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_Copayment = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_CopaymentHint = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_LateJoiner = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_LateJoiner = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_LateJoinerHint = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_WaitingPeriods = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_WaitingPeriods = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_WaitingPeriodHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_LateJoiner = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_copayment = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_WaitingPeriod = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_ChronicConditions = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_ChronicConditions = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_ChronicConditionsHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_ChronicConditions = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_NeedsAndGoals = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_Hospitalisation = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_Hospitalisation = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel_HospitalisationHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_Hospitalisation = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_MedicalCover = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox_CurrentMedicalCover = new MetroFramework.Controls.MetroTextBox();
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
            this.metroTextBox_RecommendedFunds = new MetroFramework.Controls.MetroTextBox();
            this.label_InitialAdvice = new System.Windows.Forms.Label();
            this.metroLabel_RecommendedFund = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_OtherInformationHint = new MetroFramework.Controls.MetroLabel();
            this.metroLabel_OtherInformation = new MetroFramework.Controls.MetroLabel();
            this.metroPanel_OtherInformation = new MetroFramework.Controls.MetroPanel();
            this.metroPanel5 = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox4 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox_OtherInformation = new MetroFramework.Controls.MetroTextBox();
            this.metroPanel_Motivation = new MetroFramework.Controls.MetroPanel();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox2 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox_Motivation = new MetroFramework.Controls.MetroTextBox();
            this.metroPanel_FinancialSituation = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox_FinancialSituation = new MetroFramework.Controls.MetroTextBox();
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
            this.xInput_ShowCompletedTasks = new Finx.App.UserControls.xInput();
            this.xToolBarMenu1 = new Finx.App.UserControls.xToolBarMenu();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.metroPanel_AdviceRecord.SuspendLayout();
            this.metroPanel_ImplementationMotivation.SuspendLayout();
            this.metroPanel_ProductImplemented.SuspendLayout();
            this.metroPanel_Notes.SuspendLayout();
            this.metroPanel_OtherImportantInfo.SuspendLayout();
            this.metroPanel_Copayment.SuspendLayout();
            this.metroPanel_LateJoiner.SuspendLayout();
            this.metroPanel_WaitingPeriods.SuspendLayout();
            this.metroPanel_ChronicConditions.SuspendLayout();
            this.metroPanel_Hospitalisation.SuspendLayout();
            this.metroPanel_MedicalCover.SuspendLayout();
            this.metroPanel_MedicalConditions.SuspendLayout();
            this.metroPanel_RecomendedFunds.SuspendLayout();
            this.metroPanel7.SuspendLayout();
            this.metroPanel_OtherInformation.SuspendLayout();
            this.metroPanel5.SuspendLayout();
            this.metroPanel_Motivation.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            this.metroPanel_FinancialSituation.SuspendLayout();
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
            // metroPanel_AdviceRecord
            // 
            this.metroPanel_AdviceRecord.AutoScroll = true;
            this.metroPanel_AdviceRecord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel_AdviceRecord.Controls.Add(this.tblPanel_RiskNeedsAndGoals);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationMotivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationMotivationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_ImplementationMotivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_ProductImplemented);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationAdviceHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.label_ImplementationAdvice);
            this.metroPanel_AdviceRecord.Controls.Add(this.tblPanel_MedicalSchemeComparison);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ComparisonMedicalSchemeHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.label_ComparisonMedicalScheme);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Notes);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Notes);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherImportantInfo);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_productImplemented);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherImportantInfoHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherImportantInfo);
            this.metroPanel_AdviceRecord.Controls.Add(this.tblPanel_NeedsAndGoals);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_RecommendedProductHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Copayment);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_CopaymentHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_LateJoiner);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_LateJoinerHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_WaitingPeriods);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_WaitingPeriodHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_LateJoiner);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_copayment);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_WaitingPeriod);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_ChronicConditions);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ChronicConditionsHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ChronicConditions);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_NeedsAndGoals);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Hospitalisation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_HospitalisationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Hospitalisation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_MedicalCover);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalCoverHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalCover);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_MedicalConditions);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalConditions);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalConditionsHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MotivationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Motivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_RecomendedFunds);
            this.metroPanel_AdviceRecord.Controls.Add(this.label_InitialAdvice);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_RecommendedFund);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherInformation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Motivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_FinancialSituation);
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
            // 
            // tblPanel_RiskNeedsAndGoals
            // 
            this.tblPanel_RiskNeedsAndGoals.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblPanel_RiskNeedsAndGoals.ColumnCount = 6;
            this.tblPanel_RiskNeedsAndGoals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.77918F));
            this.tblPanel_RiskNeedsAndGoals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68411F));
            this.tblPanel_RiskNeedsAndGoals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68411F));
            this.tblPanel_RiskNeedsAndGoals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68411F));
            this.tblPanel_RiskNeedsAndGoals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68411F));
            this.tblPanel_RiskNeedsAndGoals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.48437F));
            this.tblPanel_RiskNeedsAndGoals.Location = new System.Drawing.Point(15, 3308);
            this.tblPanel_RiskNeedsAndGoals.Name = "tblPanel_RiskNeedsAndGoals";
            this.tblPanel_RiskNeedsAndGoals.RowCount = 8;
            this.tblPanel_RiskNeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tblPanel_RiskNeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_RiskNeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_RiskNeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_RiskNeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_RiskNeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_RiskNeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_RiskNeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_RiskNeedsAndGoals.Size = new System.Drawing.Size(1139, 392);
            this.tblPanel_RiskNeedsAndGoals.TabIndex = 65;
            // 
            // metroLabel_ImplementationMotivation
            // 
            this.metroLabel_ImplementationMotivation.AutoSize = true;
            this.metroLabel_ImplementationMotivation.Location = new System.Drawing.Point(30, 3017);
            this.metroLabel_ImplementationMotivation.Name = "metroLabel_ImplementationMotivation";
            this.metroLabel_ImplementationMotivation.Size = new System.Drawing.Size(216, 20);
            this.metroLabel_ImplementationMotivation.TabIndex = 64;
            this.metroLabel_ImplementationMotivation.Text = "Motivation for Recommendations:";
            // 
            // metroLabel_ImplementationMotivationHint
            // 
            this.metroLabel_ImplementationMotivationHint.AutoSize = true;
            this.metroLabel_ImplementationMotivationHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_ImplementationMotivationHint.Location = new System.Drawing.Point(33, 3217);
            this.metroLabel_ImplementationMotivationHint.Name = "metroLabel_ImplementationMotivationHint";
            this.metroLabel_ImplementationMotivationHint.Size = new System.Drawing.Size(820, 60);
            this.metroLabel_ImplementationMotivationHint.TabIndex = 63;
            this.metroLabel_ImplementationMotivationHint.Text = resources.GetString("metroLabel_ImplementationMotivationHint.Text");
            this.metroLabel_ImplementationMotivationHint.UseCustomForeColor = true;
            // 
            // metroPanel_ImplementationMotivation
            // 
            this.metroPanel_ImplementationMotivation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_ImplementationMotivation.Controls.Add(this.metroTextBox_ImplementationMotivation);
            this.metroPanel_ImplementationMotivation.HorizontalScrollbarBarColor = true;
            this.metroPanel_ImplementationMotivation.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_ImplementationMotivation.HorizontalScrollbarSize = 10;
            this.metroPanel_ImplementationMotivation.Location = new System.Drawing.Point(18, 3087);
            this.metroPanel_ImplementationMotivation.Name = "metroPanel_ImplementationMotivation";
            this.metroPanel_ImplementationMotivation.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_ImplementationMotivation.TabIndex = 62;
            this.metroPanel_ImplementationMotivation.VerticalScrollbarBarColor = true;
            this.metroPanel_ImplementationMotivation.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_ImplementationMotivation.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_ImplementationMotivation
            // 
            // 
            // 
            // 
            this.metroTextBox_ImplementationMotivation.CustomButton.Image = null;
            this.metroTextBox_ImplementationMotivation.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_ImplementationMotivation.CustomButton.Name = "";
            this.metroTextBox_ImplementationMotivation.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_ImplementationMotivation.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_ImplementationMotivation.CustomButton.TabIndex = 1;
            this.metroTextBox_ImplementationMotivation.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_ImplementationMotivation.CustomButton.UseSelectable = true;
            this.metroTextBox_ImplementationMotivation.CustomButton.Visible = false;
            this.metroTextBox_ImplementationMotivation.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_ImplementationMotivation.Lines = new string[0];
            this.metroTextBox_ImplementationMotivation.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_ImplementationMotivation.MaxLength = 32767;
            this.metroTextBox_ImplementationMotivation.Multiline = true;
            this.metroTextBox_ImplementationMotivation.Name = "metroTextBox_ImplementationMotivation";
            this.metroTextBox_ImplementationMotivation.PasswordChar = '\0';
            this.metroTextBox_ImplementationMotivation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_ImplementationMotivation.SelectedText = "";
            this.metroTextBox_ImplementationMotivation.SelectionLength = 0;
            this.metroTextBox_ImplementationMotivation.SelectionStart = 0;
            this.metroTextBox_ImplementationMotivation.ShortcutsEnabled = true;
            this.metroTextBox_ImplementationMotivation.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_ImplementationMotivation.TabIndex = 2;
            this.metroTextBox_ImplementationMotivation.UseCustomBackColor = true;
            this.metroTextBox_ImplementationMotivation.UseSelectable = true;
            this.metroTextBox_ImplementationMotivation.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_ImplementationMotivation.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroPanel_ProductImplemented
            // 
            this.metroPanel_ProductImplemented.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_ProductImplemented.Controls.Add(this.metroTextBox_ProductImplemented);
            this.metroPanel_ProductImplemented.HorizontalScrollbarBarColor = true;
            this.metroPanel_ProductImplemented.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_ProductImplemented.HorizontalScrollbarSize = 10;
            this.metroPanel_ProductImplemented.Location = new System.Drawing.Point(18, 2912);
            this.metroPanel_ProductImplemented.Name = "metroPanel_ProductImplemented";
            this.metroPanel_ProductImplemented.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_ProductImplemented.TabIndex = 61;
            this.metroPanel_ProductImplemented.VerticalScrollbarBarColor = true;
            this.metroPanel_ProductImplemented.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_ProductImplemented.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_ProductImplemented
            // 
            // 
            // 
            // 
            this.metroTextBox_ProductImplemented.CustomButton.Image = null;
            this.metroTextBox_ProductImplemented.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_ProductImplemented.CustomButton.Name = "";
            this.metroTextBox_ProductImplemented.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_ProductImplemented.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_ProductImplemented.CustomButton.TabIndex = 1;
            this.metroTextBox_ProductImplemented.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_ProductImplemented.CustomButton.UseSelectable = true;
            this.metroTextBox_ProductImplemented.CustomButton.Visible = false;
            this.metroTextBox_ProductImplemented.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_ProductImplemented.Lines = new string[0];
            this.metroTextBox_ProductImplemented.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_ProductImplemented.MaxLength = 32767;
            this.metroTextBox_ProductImplemented.Multiline = true;
            this.metroTextBox_ProductImplemented.Name = "metroTextBox_ProductImplemented";
            this.metroTextBox_ProductImplemented.PasswordChar = '\0';
            this.metroTextBox_ProductImplemented.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_ProductImplemented.SelectedText = "";
            this.metroTextBox_ProductImplemented.SelectionLength = 0;
            this.metroTextBox_ProductImplemented.SelectionStart = 0;
            this.metroTextBox_ProductImplemented.ShortcutsEnabled = true;
            this.metroTextBox_ProductImplemented.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_ProductImplemented.TabIndex = 2;
            this.metroTextBox_ProductImplemented.UseCustomBackColor = true;
            this.metroTextBox_ProductImplemented.UseSelectable = true;
            this.metroTextBox_ProductImplemented.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_ProductImplemented.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_ImplementationAdviceHint
            // 
            this.metroLabel_ImplementationAdviceHint.AutoSize = true;
            this.metroLabel_ImplementationAdviceHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_ImplementationAdviceHint.Location = new System.Drawing.Point(18, 2853);
            this.metroLabel_ImplementationAdviceHint.Name = "metroLabel_ImplementationAdviceHint";
            this.metroLabel_ImplementationAdviceHint.Size = new System.Drawing.Size(375, 20);
            this.metroLabel_ImplementationAdviceHint.TabIndex = 60;
            this.metroLabel_ImplementationAdviceHint.Text = "(Explain what was finally implemented and reasons thereof.)";
            this.metroLabel_ImplementationAdviceHint.UseCustomForeColor = true;
            // 
            // label_ImplementationAdvice
            // 
            this.label_ImplementationAdvice.AutoSize = true;
            this.label_ImplementationAdvice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ImplementationAdvice.Location = new System.Drawing.Point(13, 2808);
            this.label_ImplementationAdvice.Name = "label_ImplementationAdvice";
            this.label_ImplementationAdvice.Size = new System.Drawing.Size(367, 25);
            this.label_ImplementationAdvice.TabIndex = 59;
            this.label_ImplementationAdvice.Text = "Implementation Recommendation/Advice";
            // 
            // tblPanel_MedicalSchemeComparison
            // 
            this.tblPanel_MedicalSchemeComparison.AutoSize = true;
            this.tblPanel_MedicalSchemeComparison.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblPanel_MedicalSchemeComparison.ColumnCount = 3;
            this.tblPanel_MedicalSchemeComparison.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.92593F));
            this.tblPanel_MedicalSchemeComparison.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.03704F));
            this.tblPanel_MedicalSchemeComparison.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.03704F));
            this.tblPanel_MedicalSchemeComparison.Location = new System.Drawing.Point(15, 2242);
            this.tblPanel_MedicalSchemeComparison.Name = "tblPanel_MedicalSchemeComparison";
            this.tblPanel_MedicalSchemeComparison.RowCount = 11;
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_MedicalSchemeComparison.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_MedicalSchemeComparison.Size = new System.Drawing.Size(972, 527);
            this.tblPanel_MedicalSchemeComparison.TabIndex = 58;
            // 
            // metroLabel_ComparisonMedicalSchemeHint
            // 
            this.metroLabel_ComparisonMedicalSchemeHint.AutoSize = true;
            this.metroLabel_ComparisonMedicalSchemeHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_ComparisonMedicalSchemeHint.Location = new System.Drawing.Point(10, 2212);
            this.metroLabel_ComparisonMedicalSchemeHint.Name = "metroLabel_ComparisonMedicalSchemeHint";
            this.metroLabel_ComparisonMedicalSchemeHint.Size = new System.Drawing.Size(638, 20);
            this.metroLabel_ComparisonMedicalSchemeHint.TabIndex = 57;
            this.metroLabel_ComparisonMedicalSchemeHint.Text = "(Indicate whether a new medical scheme(s) is recommended or an existing scheme is" +
    " to be replaced.)";
            this.metroLabel_ComparisonMedicalSchemeHint.UseCustomForeColor = true;
            // 
            // label_ComparisonMedicalScheme
            // 
            this.label_ComparisonMedicalScheme.AutoSize = true;
            this.label_ComparisonMedicalScheme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ComparisonMedicalScheme.Location = new System.Drawing.Point(10, 2192);
            this.label_ComparisonMedicalScheme.Name = "label_ComparisonMedicalScheme";
            this.label_ComparisonMedicalScheme.Size = new System.Drawing.Size(429, 25);
            this.label_ComparisonMedicalScheme.TabIndex = 56;
            this.label_ComparisonMedicalScheme.Text = "Comparison for replacement of Medical Scheme";
            // 
            // metroPanel_Notes
            // 
            this.metroPanel_Notes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_Notes.Controls.Add(this.metroTextBox_Notes);
            this.metroPanel_Notes.HorizontalScrollbarBarColor = true;
            this.metroPanel_Notes.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Notes.HorizontalScrollbarSize = 10;
            this.metroPanel_Notes.Location = new System.Drawing.Point(15, 2092);
            this.metroPanel_Notes.Name = "metroPanel_Notes";
            this.metroPanel_Notes.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_Notes.TabIndex = 55;
            this.metroPanel_Notes.VerticalScrollbarBarColor = true;
            this.metroPanel_Notes.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Notes.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_Notes
            // 
            // 
            // 
            // 
            this.metroTextBox_Notes.CustomButton.Image = null;
            this.metroTextBox_Notes.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_Notes.CustomButton.Name = "";
            this.metroTextBox_Notes.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_Notes.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_Notes.CustomButton.TabIndex = 1;
            this.metroTextBox_Notes.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_Notes.CustomButton.UseSelectable = true;
            this.metroTextBox_Notes.CustomButton.Visible = false;
            this.metroTextBox_Notes.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_Notes.Lines = new string[0];
            this.metroTextBox_Notes.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_Notes.MaxLength = 32767;
            this.metroTextBox_Notes.Multiline = true;
            this.metroTextBox_Notes.Name = "metroTextBox_Notes";
            this.metroTextBox_Notes.PasswordChar = '\0';
            this.metroTextBox_Notes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_Notes.SelectedText = "";
            this.metroTextBox_Notes.SelectionLength = 0;
            this.metroTextBox_Notes.SelectionStart = 0;
            this.metroTextBox_Notes.ShortcutsEnabled = true;
            this.metroTextBox_Notes.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_Notes.TabIndex = 2;
            this.metroTextBox_Notes.UseCustomBackColor = true;
            this.metroTextBox_Notes.UseSelectable = true;
            this.metroTextBox_Notes.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_Notes.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_Notes
            // 
            this.metroLabel_Notes.AutoSize = true;
            this.metroLabel_Notes.Location = new System.Drawing.Point(10, 2059);
            this.metroLabel_Notes.Name = "metroLabel_Notes";
            this.metroLabel_Notes.Size = new System.Drawing.Size(49, 20);
            this.metroLabel_Notes.TabIndex = 54;
            this.metroLabel_Notes.Text = "Notes:";
            // 
            // metroPanel_OtherImportantInfo
            // 
            this.metroPanel_OtherImportantInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_OtherImportantInfo.Controls.Add(this.metroTextBox_OtherImportantInfo);
            this.metroPanel_OtherImportantInfo.HorizontalScrollbarBarColor = true;
            this.metroPanel_OtherImportantInfo.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_OtherImportantInfo.HorizontalScrollbarSize = 10;
            this.metroPanel_OtherImportantInfo.Location = new System.Drawing.Point(15, 1959);
            this.metroPanel_OtherImportantInfo.Name = "metroPanel_OtherImportantInfo";
            this.metroPanel_OtherImportantInfo.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_OtherImportantInfo.TabIndex = 37;
            this.metroPanel_OtherImportantInfo.VerticalScrollbarBarColor = true;
            this.metroPanel_OtherImportantInfo.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_OtherImportantInfo.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_OtherImportantInfo
            // 
            // 
            // 
            // 
            this.metroTextBox_OtherImportantInfo.CustomButton.Image = null;
            this.metroTextBox_OtherImportantInfo.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_OtherImportantInfo.CustomButton.Name = "";
            this.metroTextBox_OtherImportantInfo.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_OtherImportantInfo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_OtherImportantInfo.CustomButton.TabIndex = 1;
            this.metroTextBox_OtherImportantInfo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_OtherImportantInfo.CustomButton.UseSelectable = true;
            this.metroTextBox_OtherImportantInfo.CustomButton.Visible = false;
            this.metroTextBox_OtherImportantInfo.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_OtherImportantInfo.Lines = new string[0];
            this.metroTextBox_OtherImportantInfo.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_OtherImportantInfo.MaxLength = 32767;
            this.metroTextBox_OtherImportantInfo.Multiline = true;
            this.metroTextBox_OtherImportantInfo.Name = "metroTextBox_OtherImportantInfo";
            this.metroTextBox_OtherImportantInfo.PasswordChar = '\0';
            this.metroTextBox_OtherImportantInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_OtherImportantInfo.SelectedText = "";
            this.metroTextBox_OtherImportantInfo.SelectionLength = 0;
            this.metroTextBox_OtherImportantInfo.SelectionStart = 0;
            this.metroTextBox_OtherImportantInfo.ShortcutsEnabled = true;
            this.metroTextBox_OtherImportantInfo.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_OtherImportantInfo.TabIndex = 2;
            this.metroTextBox_OtherImportantInfo.UseCustomBackColor = true;
            this.metroTextBox_OtherImportantInfo.UseSelectable = true;
            this.metroTextBox_OtherImportantInfo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_OtherImportantInfo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_productImplemented
            // 
            this.metroLabel_productImplemented.AutoSize = true;
            this.metroLabel_productImplemented.Location = new System.Drawing.Point(18, 2889);
            this.metroLabel_productImplemented.Name = "metroLabel_productImplemented";
            this.metroLabel_productImplemented.Size = new System.Drawing.Size(204, 20);
            this.metroLabel_productImplemented.TabIndex = 53;
            this.metroLabel_productImplemented.Text = "Products/Funds recommended:";
            // 
            // metroLabel_OtherImportantInfoHint
            // 
            this.metroLabel_OtherImportantInfoHint.AutoSize = true;
            this.metroLabel_OtherImportantInfoHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_OtherImportantInfoHint.Location = new System.Drawing.Point(10, 1927);
            this.metroLabel_OtherImportantInfoHint.Name = "metroLabel_OtherImportantInfoHint";
            this.metroLabel_OtherImportantInfoHint.Size = new System.Drawing.Size(399, 20);
            this.metroLabel_OtherImportantInfoHint.TabIndex = 52;
            this.metroLabel_OtherImportantInfoHint.Text = "(Provide Details of what would not be coverd, annual limits etc.)";
            this.metroLabel_OtherImportantInfoHint.UseCustomForeColor = true;
            // 
            // metroLabel_OtherImportantInfo
            // 
            this.metroLabel_OtherImportantInfo.AutoSize = true;
            this.metroLabel_OtherImportantInfo.Location = new System.Drawing.Point(10, 1907);
            this.metroLabel_OtherImportantInfo.Name = "metroLabel_OtherImportantInfo";
            this.metroLabel_OtherImportantInfo.Size = new System.Drawing.Size(186, 20);
            this.metroLabel_OtherImportantInfo.TabIndex = 51;
            this.metroLabel_OtherImportantInfo.Text = "Other Important Information:";
            // 
            // tblPanel_NeedsAndGoals
            // 
            this.tblPanel_NeedsAndGoals.AutoSize = true;
            this.tblPanel_NeedsAndGoals.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblPanel_NeedsAndGoals.ColumnCount = 4;
            this.tblPanel_NeedsAndGoals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblPanel_NeedsAndGoals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblPanel_NeedsAndGoals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblPanel_NeedsAndGoals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblPanel_NeedsAndGoals.Location = new System.Drawing.Point(15, 837);
            this.tblPanel_NeedsAndGoals.Name = "tblPanel_NeedsAndGoals";
            this.tblPanel_NeedsAndGoals.RowCount = 9;
            this.tblPanel_NeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblPanel_NeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_NeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_NeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_NeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_NeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_NeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_NeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_NeedsAndGoals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPanel_NeedsAndGoals.Size = new System.Drawing.Size(972, 422);
            this.tblPanel_NeedsAndGoals.TabIndex = 39;
            // 
            // metroLabel_RecommendedProductHint
            // 
            this.metroLabel_RecommendedProductHint.AutoSize = true;
            this.metroLabel_RecommendedProductHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_RecommendedProductHint.Location = new System.Drawing.Point(10, 983);
            this.metroLabel_RecommendedProductHint.Name = "metroLabel_RecommendedProductHint";
            this.metroLabel_RecommendedProductHint.Size = new System.Drawing.Size(275, 20);
            this.metroLabel_RecommendedProductHint.TabIndex = 50;
            this.metroLabel_RecommendedProductHint.Text = "(Ensure all needs identified are addressed.)";
            this.metroLabel_RecommendedProductHint.UseCustomForeColor = true;
            // 
            // metroPanel_Copayment
            // 
            this.metroPanel_Copayment.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_Copayment.Controls.Add(this.metroTextBox_Copayment);
            this.metroPanel_Copayment.HorizontalScrollbarBarColor = true;
            this.metroPanel_Copayment.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Copayment.HorizontalScrollbarSize = 10;
            this.metroPanel_Copayment.Location = new System.Drawing.Point(15, 1807);
            this.metroPanel_Copayment.Name = "metroPanel_Copayment";
            this.metroPanel_Copayment.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_Copayment.TabIndex = 36;
            this.metroPanel_Copayment.VerticalScrollbarBarColor = true;
            this.metroPanel_Copayment.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Copayment.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_Copayment
            // 
            // 
            // 
            // 
            this.metroTextBox_Copayment.CustomButton.Image = null;
            this.metroTextBox_Copayment.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_Copayment.CustomButton.Name = "";
            this.metroTextBox_Copayment.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_Copayment.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_Copayment.CustomButton.TabIndex = 1;
            this.metroTextBox_Copayment.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_Copayment.CustomButton.UseSelectable = true;
            this.metroTextBox_Copayment.CustomButton.Visible = false;
            this.metroTextBox_Copayment.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_Copayment.Lines = new string[0];
            this.metroTextBox_Copayment.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_Copayment.MaxLength = 32767;
            this.metroTextBox_Copayment.Multiline = true;
            this.metroTextBox_Copayment.Name = "metroTextBox_Copayment";
            this.metroTextBox_Copayment.PasswordChar = '\0';
            this.metroTextBox_Copayment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_Copayment.SelectedText = "";
            this.metroTextBox_Copayment.SelectionLength = 0;
            this.metroTextBox_Copayment.SelectionStart = 0;
            this.metroTextBox_Copayment.ShortcutsEnabled = true;
            this.metroTextBox_Copayment.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_Copayment.TabIndex = 2;
            this.metroTextBox_Copayment.UseCustomBackColor = true;
            this.metroTextBox_Copayment.UseSelectable = true;
            this.metroTextBox_Copayment.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_Copayment.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_CopaymentHint
            // 
            this.metroLabel_CopaymentHint.AutoSize = true;
            this.metroLabel_CopaymentHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_CopaymentHint.Location = new System.Drawing.Point(10, 1775);
            this.metroLabel_CopaymentHint.Name = "metroLabel_CopaymentHint";
            this.metroLabel_CopaymentHint.Size = new System.Drawing.Size(333, 20);
            this.metroLabel_CopaymentHint.TabIndex = 49;
            this.metroLabel_CopaymentHint.Text = "(Indicate which first amounts payable are applicable.)";
            this.metroLabel_CopaymentHint.UseCustomForeColor = true;
            // 
            // metroPanel_LateJoiner
            // 
            this.metroPanel_LateJoiner.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_LateJoiner.Controls.Add(this.metroTextBox_LateJoiner);
            this.metroPanel_LateJoiner.HorizontalScrollbarBarColor = true;
            this.metroPanel_LateJoiner.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_LateJoiner.HorizontalScrollbarSize = 10;
            this.metroPanel_LateJoiner.Location = new System.Drawing.Point(15, 1655);
            this.metroPanel_LateJoiner.Name = "metroPanel_LateJoiner";
            this.metroPanel_LateJoiner.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_LateJoiner.TabIndex = 35;
            this.metroPanel_LateJoiner.VerticalScrollbarBarColor = true;
            this.metroPanel_LateJoiner.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_LateJoiner.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_LateJoiner
            // 
            // 
            // 
            // 
            this.metroTextBox_LateJoiner.CustomButton.Image = null;
            this.metroTextBox_LateJoiner.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_LateJoiner.CustomButton.Name = "";
            this.metroTextBox_LateJoiner.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_LateJoiner.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_LateJoiner.CustomButton.TabIndex = 1;
            this.metroTextBox_LateJoiner.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_LateJoiner.CustomButton.UseSelectable = true;
            this.metroTextBox_LateJoiner.CustomButton.Visible = false;
            this.metroTextBox_LateJoiner.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_LateJoiner.Lines = new string[0];
            this.metroTextBox_LateJoiner.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_LateJoiner.MaxLength = 32767;
            this.metroTextBox_LateJoiner.Multiline = true;
            this.metroTextBox_LateJoiner.Name = "metroTextBox_LateJoiner";
            this.metroTextBox_LateJoiner.PasswordChar = '\0';
            this.metroTextBox_LateJoiner.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_LateJoiner.SelectedText = "";
            this.metroTextBox_LateJoiner.SelectionLength = 0;
            this.metroTextBox_LateJoiner.SelectionStart = 0;
            this.metroTextBox_LateJoiner.ShortcutsEnabled = true;
            this.metroTextBox_LateJoiner.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_LateJoiner.TabIndex = 2;
            this.metroTextBox_LateJoiner.UseCustomBackColor = true;
            this.metroTextBox_LateJoiner.UseSelectable = true;
            this.metroTextBox_LateJoiner.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_LateJoiner.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_LateJoinerHint
            // 
            this.metroLabel_LateJoinerHint.AutoSize = true;
            this.metroLabel_LateJoinerHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_LateJoinerHint.Location = new System.Drawing.Point(10, 1622);
            this.metroLabel_LateJoinerHint.Name = "metroLabel_LateJoinerHint";
            this.metroLabel_LateJoinerHint.Size = new System.Drawing.Size(341, 20);
            this.metroLabel_LateJoinerHint.TabIndex = 48;
            this.metroLabel_LateJoinerHint.Text = "(Provide details of the late joiner penalty if applicable.)";
            this.metroLabel_LateJoinerHint.UseCustomForeColor = true;
            // 
            // metroPanel_WaitingPeriods
            // 
            this.metroPanel_WaitingPeriods.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_WaitingPeriods.Controls.Add(this.metroTextBox_WaitingPeriods);
            this.metroPanel_WaitingPeriods.HorizontalScrollbarBarColor = true;
            this.metroPanel_WaitingPeriods.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_WaitingPeriods.HorizontalScrollbarSize = 10;
            this.metroPanel_WaitingPeriods.Location = new System.Drawing.Point(15, 1502);
            this.metroPanel_WaitingPeriods.Name = "metroPanel_WaitingPeriods";
            this.metroPanel_WaitingPeriods.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_WaitingPeriods.TabIndex = 34;
            this.metroPanel_WaitingPeriods.VerticalScrollbarBarColor = true;
            this.metroPanel_WaitingPeriods.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_WaitingPeriods.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_WaitingPeriods
            // 
            // 
            // 
            // 
            this.metroTextBox_WaitingPeriods.CustomButton.Image = null;
            this.metroTextBox_WaitingPeriods.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_WaitingPeriods.CustomButton.Name = "";
            this.metroTextBox_WaitingPeriods.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_WaitingPeriods.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_WaitingPeriods.CustomButton.TabIndex = 1;
            this.metroTextBox_WaitingPeriods.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_WaitingPeriods.CustomButton.UseSelectable = true;
            this.metroTextBox_WaitingPeriods.CustomButton.Visible = false;
            this.metroTextBox_WaitingPeriods.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_WaitingPeriods.Lines = new string[0];
            this.metroTextBox_WaitingPeriods.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_WaitingPeriods.MaxLength = 32767;
            this.metroTextBox_WaitingPeriods.Multiline = true;
            this.metroTextBox_WaitingPeriods.Name = "metroTextBox_WaitingPeriods";
            this.metroTextBox_WaitingPeriods.PasswordChar = '\0';
            this.metroTextBox_WaitingPeriods.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_WaitingPeriods.SelectedText = "";
            this.metroTextBox_WaitingPeriods.SelectionLength = 0;
            this.metroTextBox_WaitingPeriods.SelectionStart = 0;
            this.metroTextBox_WaitingPeriods.ShortcutsEnabled = true;
            this.metroTextBox_WaitingPeriods.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_WaitingPeriods.TabIndex = 2;
            this.metroTextBox_WaitingPeriods.UseCustomBackColor = true;
            this.metroTextBox_WaitingPeriods.UseSelectable = true;
            this.metroTextBox_WaitingPeriods.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_WaitingPeriods.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_WaitingPeriodHint
            // 
            this.metroLabel_WaitingPeriodHint.AutoSize = true;
            this.metroLabel_WaitingPeriodHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_WaitingPeriodHint.Location = new System.Drawing.Point(10, 1470);
            this.metroLabel_WaitingPeriodHint.Name = "metroLabel_WaitingPeriodHint";
            this.metroLabel_WaitingPeriodHint.Size = new System.Drawing.Size(337, 20);
            this.metroLabel_WaitingPeriodHint.TabIndex = 47;
            this.metroLabel_WaitingPeriodHint.Text = "(Provide information of all applicable waiting periods.)";
            this.metroLabel_WaitingPeriodHint.UseCustomForeColor = true;
            // 
            // metroLabel_LateJoiner
            // 
            this.metroLabel_LateJoiner.AutoSize = true;
            this.metroLabel_LateJoiner.Location = new System.Drawing.Point(10, 1602);
            this.metroLabel_LateJoiner.Name = "metroLabel_LateJoiner";
            this.metroLabel_LateJoiner.Size = new System.Drawing.Size(127, 20);
            this.metroLabel_LateJoiner.TabIndex = 46;
            this.metroLabel_LateJoiner.Text = "Late Joiner Penalty:";
            // 
            // metroLabel_copayment
            // 
            this.metroLabel_copayment.AutoSize = true;
            this.metroLabel_copayment.Location = new System.Drawing.Point(10, 1755);
            this.metroLabel_copayment.Name = "metroLabel_copayment";
            this.metroLabel_copayment.Size = new System.Drawing.Size(94, 20);
            this.metroLabel_copayment.TabIndex = 45;
            this.metroLabel_copayment.Text = "Co-Payments:";
            // 
            // metroLabel_WaitingPeriod
            // 
            this.metroLabel_WaitingPeriod.AutoSize = true;
            this.metroLabel_WaitingPeriod.Location = new System.Drawing.Point(10, 1450);
            this.metroLabel_WaitingPeriod.Name = "metroLabel_WaitingPeriod";
            this.metroLabel_WaitingPeriod.Size = new System.Drawing.Size(106, 20);
            this.metroLabel_WaitingPeriod.TabIndex = 44;
            this.metroLabel_WaitingPeriod.Text = "Waiting Periods:";
            // 
            // metroPanel_ChronicConditions
            // 
            this.metroPanel_ChronicConditions.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_ChronicConditions.Controls.Add(this.metroTextBox_ChronicConditions);
            this.metroPanel_ChronicConditions.HorizontalScrollbarBarColor = true;
            this.metroPanel_ChronicConditions.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_ChronicConditions.HorizontalScrollbarSize = 10;
            this.metroPanel_ChronicConditions.Location = new System.Drawing.Point(15, 1350);
            this.metroPanel_ChronicConditions.Name = "metroPanel_ChronicConditions";
            this.metroPanel_ChronicConditions.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_ChronicConditions.TabIndex = 33;
            this.metroPanel_ChronicConditions.VerticalScrollbarBarColor = true;
            this.metroPanel_ChronicConditions.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_ChronicConditions.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_ChronicConditions
            // 
            // 
            // 
            // 
            this.metroTextBox_ChronicConditions.CustomButton.Image = null;
            this.metroTextBox_ChronicConditions.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_ChronicConditions.CustomButton.Name = "";
            this.metroTextBox_ChronicConditions.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_ChronicConditions.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_ChronicConditions.CustomButton.TabIndex = 1;
            this.metroTextBox_ChronicConditions.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_ChronicConditions.CustomButton.UseSelectable = true;
            this.metroTextBox_ChronicConditions.CustomButton.Visible = false;
            this.metroTextBox_ChronicConditions.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_ChronicConditions.Lines = new string[0];
            this.metroTextBox_ChronicConditions.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_ChronicConditions.MaxLength = 32767;
            this.metroTextBox_ChronicConditions.Multiline = true;
            this.metroTextBox_ChronicConditions.Name = "metroTextBox_ChronicConditions";
            this.metroTextBox_ChronicConditions.PasswordChar = '\0';
            this.metroTextBox_ChronicConditions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_ChronicConditions.SelectedText = "";
            this.metroTextBox_ChronicConditions.SelectionLength = 0;
            this.metroTextBox_ChronicConditions.SelectionStart = 0;
            this.metroTextBox_ChronicConditions.ShortcutsEnabled = true;
            this.metroTextBox_ChronicConditions.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_ChronicConditions.TabIndex = 2;
            this.metroTextBox_ChronicConditions.UseCustomBackColor = true;
            this.metroTextBox_ChronicConditions.UseSelectable = true;
            this.metroTextBox_ChronicConditions.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_ChronicConditions.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_ChronicConditionsHint
            // 
            this.metroLabel_ChronicConditionsHint.AutoSize = true;
            this.metroLabel_ChronicConditionsHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_ChronicConditionsHint.Location = new System.Drawing.Point(10, 1295);
            this.metroLabel_ChronicConditionsHint.Name = "metroLabel_ChronicConditionsHint";
            this.metroLabel_ChronicConditionsHint.Size = new System.Drawing.Size(817, 40);
            this.metroLabel_ChronicConditionsHint.TabIndex = 42;
            this.metroLabel_ChronicConditionsHint.Text = resources.GetString("metroLabel_ChronicConditionsHint.Text");
            this.metroLabel_ChronicConditionsHint.UseCustomForeColor = true;
            // 
            // metroLabel_ChronicConditions
            // 
            this.metroLabel_ChronicConditions.AutoSize = true;
            this.metroLabel_ChronicConditions.Location = new System.Drawing.Point(10, 1275);
            this.metroLabel_ChronicConditions.Name = "metroLabel_ChronicConditions";
            this.metroLabel_ChronicConditions.Size = new System.Drawing.Size(129, 20);
            this.metroLabel_ChronicConditions.TabIndex = 41;
            this.metroLabel_ChronicConditions.Text = "Chronic Conditions:";
            // 
            // metroLabel_NeedsAndGoals
            // 
            this.metroLabel_NeedsAndGoals.AutoSize = true;
            this.metroLabel_NeedsAndGoals.Location = new System.Drawing.Point(10, 811);
            this.metroLabel_NeedsAndGoals.Name = "metroLabel_NeedsAndGoals";
            this.metroLabel_NeedsAndGoals.Size = new System.Drawing.Size(179, 20);
            this.metroLabel_NeedsAndGoals.TabIndex = 40;
            this.metroLabel_NeedsAndGoals.Text = "Needs and Goals identified:";
            // 
            // metroPanel_Hospitalisation
            // 
            this.metroPanel_Hospitalisation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_Hospitalisation.Controls.Add(this.metroTextBox_Hospitalisation);
            this.metroPanel_Hospitalisation.HorizontalScrollbarBarColor = true;
            this.metroPanel_Hospitalisation.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Hospitalisation.HorizontalScrollbarSize = 10;
            this.metroPanel_Hospitalisation.Location = new System.Drawing.Point(15, 711);
            this.metroPanel_Hospitalisation.Name = "metroPanel_Hospitalisation";
            this.metroPanel_Hospitalisation.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_Hospitalisation.TabIndex = 38;
            this.metroPanel_Hospitalisation.VerticalScrollbarBarColor = true;
            this.metroPanel_Hospitalisation.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_Hospitalisation.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_Hospitalisation
            // 
            // 
            // 
            // 
            this.metroTextBox_Hospitalisation.CustomButton.Image = null;
            this.metroTextBox_Hospitalisation.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_Hospitalisation.CustomButton.Name = "";
            this.metroTextBox_Hospitalisation.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_Hospitalisation.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_Hospitalisation.CustomButton.TabIndex = 1;
            this.metroTextBox_Hospitalisation.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_Hospitalisation.CustomButton.UseSelectable = true;
            this.metroTextBox_Hospitalisation.CustomButton.Visible = false;
            this.metroTextBox_Hospitalisation.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_Hospitalisation.Lines = new string[0];
            this.metroTextBox_Hospitalisation.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_Hospitalisation.MaxLength = 32767;
            this.metroTextBox_Hospitalisation.Multiline = true;
            this.metroTextBox_Hospitalisation.Name = "metroTextBox_Hospitalisation";
            this.metroTextBox_Hospitalisation.PasswordChar = '\0';
            this.metroTextBox_Hospitalisation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_Hospitalisation.SelectedText = "";
            this.metroTextBox_Hospitalisation.SelectionLength = 0;
            this.metroTextBox_Hospitalisation.SelectionStart = 0;
            this.metroTextBox_Hospitalisation.ShortcutsEnabled = true;
            this.metroTextBox_Hospitalisation.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_Hospitalisation.TabIndex = 2;
            this.metroTextBox_Hospitalisation.UseCustomBackColor = true;
            this.metroTextBox_Hospitalisation.UseSelectable = true;
            this.metroTextBox_Hospitalisation.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_Hospitalisation.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_HospitalisationHint
            // 
            this.metroLabel_HospitalisationHint.AutoSize = true;
            this.metroLabel_HospitalisationHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_HospitalisationHint.Location = new System.Drawing.Point(10, 679);
            this.metroLabel_HospitalisationHint.Name = "metroLabel_HospitalisationHint";
            this.metroLabel_HospitalisationHint.Size = new System.Drawing.Size(746, 20);
            this.metroLabel_HospitalisationHint.TabIndex = 37;
            this.metroLabel_HospitalisationHint.Text = "(Whether the client or any dependant has been admitted to hospital in the last 12" +
    " months, any planned procedures etc.)";
            this.metroLabel_HospitalisationHint.UseCustomForeColor = true;
            // 
            // metroLabel_Hospitalisation
            // 
            this.metroLabel_Hospitalisation.AutoSize = true;
            this.metroLabel_Hospitalisation.Location = new System.Drawing.Point(10, 659);
            this.metroLabel_Hospitalisation.Name = "metroLabel_Hospitalisation";
            this.metroLabel_Hospitalisation.Size = new System.Drawing.Size(100, 20);
            this.metroLabel_Hospitalisation.TabIndex = 36;
            this.metroLabel_Hospitalisation.Text = "Hospitalisation:";
            // 
            // metroPanel_MedicalCover
            // 
            this.metroPanel_MedicalCover.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_MedicalCover.Controls.Add(this.metroTextBox_CurrentMedicalCover);
            this.metroPanel_MedicalCover.HorizontalScrollbarBarColor = true;
            this.metroPanel_MedicalCover.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_MedicalCover.HorizontalScrollbarSize = 10;
            this.metroPanel_MedicalCover.Location = new System.Drawing.Point(15, 407);
            this.metroPanel_MedicalCover.Name = "metroPanel_MedicalCover";
            this.metroPanel_MedicalCover.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_MedicalCover.TabIndex = 33;
            this.metroPanel_MedicalCover.VerticalScrollbarBarColor = true;
            this.metroPanel_MedicalCover.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_MedicalCover.VerticalScrollbarSize = 10;
            // 
            // metroTextBox_CurrentMedicalCover
            // 
            // 
            // 
            // 
            this.metroTextBox_CurrentMedicalCover.CustomButton.Image = null;
            this.metroTextBox_CurrentMedicalCover.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_CurrentMedicalCover.CustomButton.Name = "";
            this.metroTextBox_CurrentMedicalCover.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_CurrentMedicalCover.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_CurrentMedicalCover.CustomButton.TabIndex = 1;
            this.metroTextBox_CurrentMedicalCover.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_CurrentMedicalCover.CustomButton.UseSelectable = true;
            this.metroTextBox_CurrentMedicalCover.CustomButton.Visible = false;
            this.metroTextBox_CurrentMedicalCover.ForeColor = System.Drawing.Color.Firebrick;
            this.metroTextBox_CurrentMedicalCover.Lines = new string[0];
            this.metroTextBox_CurrentMedicalCover.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_CurrentMedicalCover.MaxLength = 32767;
            this.metroTextBox_CurrentMedicalCover.Multiline = true;
            this.metroTextBox_CurrentMedicalCover.Name = "metroTextBox_CurrentMedicalCover";
            this.metroTextBox_CurrentMedicalCover.PasswordChar = '\0';
            this.metroTextBox_CurrentMedicalCover.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_CurrentMedicalCover.SelectedText = "";
            this.metroTextBox_CurrentMedicalCover.SelectionLength = 0;
            this.metroTextBox_CurrentMedicalCover.SelectionStart = 0;
            this.metroTextBox_CurrentMedicalCover.ShortcutsEnabled = true;
            this.metroTextBox_CurrentMedicalCover.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_CurrentMedicalCover.TabIndex = 2;
            this.metroTextBox_CurrentMedicalCover.UseCustomBackColor = true;
            this.metroTextBox_CurrentMedicalCover.UseSelectable = true;
            this.metroTextBox_CurrentMedicalCover.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_CurrentMedicalCover.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel_MedicalCoverHint
            // 
            this.metroLabel_MedicalCoverHint.AutoSize = true;
            this.metroLabel_MedicalCoverHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_MedicalCoverHint.Location = new System.Drawing.Point(10, 375);
            this.metroLabel_MedicalCoverHint.Name = "metroLabel_MedicalCoverHint";
            this.metroLabel_MedicalCoverHint.Size = new System.Drawing.Size(825, 20);
            this.metroLabel_MedicalCoverHint.TabIndex = 35;
            this.metroLabel_MedicalCoverHint.Text = "(Information on the client\'s current medical scheme and benefits, period of unint" +
    "erupted cover, period of any breakage in cover etc.)";
            this.metroLabel_MedicalCoverHint.UseCustomForeColor = true;
            // 
            // metroLabel_MedicalCover
            // 
            this.metroLabel_MedicalCover.AutoSize = true;
            this.metroLabel_MedicalCover.Location = new System.Drawing.Point(10, 355);
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
            this.metroPanel_MedicalConditions.Location = new System.Drawing.Point(15, 255);
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
            this.metroLabel_MedicalConditions.Location = new System.Drawing.Point(10, 203);
            this.metroLabel_MedicalConditions.Name = "metroLabel_MedicalConditions";
            this.metroLabel_MedicalConditions.Size = new System.Drawing.Size(129, 20);
            this.metroLabel_MedicalConditions.TabIndex = 33;
            this.metroLabel_MedicalConditions.Text = "Medical Conditions:";
            // 
            // metroLabel_MedicalConditionsHint
            // 
            this.metroLabel_MedicalConditionsHint.AutoSize = true;
            this.metroLabel_MedicalConditionsHint.ForeColor = System.Drawing.Color.Gray;
            this.metroLabel_MedicalConditionsHint.Location = new System.Drawing.Point(10, 223);
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
            this.metroLabel_MotivationHint.Location = new System.Drawing.Point(10, 1167);
            this.metroLabel_MotivationHint.Name = "metroLabel_MotivationHint";
            this.metroLabel_MotivationHint.Size = new System.Drawing.Size(769, 60);
            this.metroLabel_MotivationHint.TabIndex = 31;
            this.metroLabel_MotivationHint.Text = resources.GetString("metroLabel_MotivationHint.Text");
            this.metroLabel_MotivationHint.UseCustomForeColor = true;
            // 
            // metroLabel_Motivation
            // 
            this.metroLabel_Motivation.AutoSize = true;
            this.metroLabel_Motivation.Location = new System.Drawing.Point(10, 1147);
            this.metroLabel_Motivation.Name = "metroLabel_Motivation";
            this.metroLabel_Motivation.Size = new System.Drawing.Size(216, 20);
            this.metroLabel_Motivation.TabIndex = 30;
            this.metroLabel_Motivation.Text = "Motivation for Recommendations:";
            // 
            // metroPanel_RecomendedFunds
            // 
            this.metroPanel_RecomendedFunds.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_RecomendedFunds.Controls.Add(this.metroPanel7);
            this.metroPanel_RecomendedFunds.Controls.Add(this.metroTextBox_RecommendedFunds);
            this.metroPanel_RecomendedFunds.HorizontalScrollbarBarColor = true;
            this.metroPanel_RecomendedFunds.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_RecomendedFunds.HorizontalScrollbarSize = 10;
            this.metroPanel_RecomendedFunds.Location = new System.Drawing.Point(15, 1047);
            this.metroPanel_RecomendedFunds.Name = "metroPanel_RecomendedFunds";
            this.metroPanel_RecomendedFunds.Size = new System.Drawing.Size(972, 84);
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
            // metroTextBox_RecommendedFunds
            // 
            // 
            // 
            // 
            this.metroTextBox_RecommendedFunds.CustomButton.Image = null;
            this.metroTextBox_RecommendedFunds.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_RecommendedFunds.CustomButton.Name = "";
            this.metroTextBox_RecommendedFunds.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_RecommendedFunds.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_RecommendedFunds.CustomButton.TabIndex = 1;
            this.metroTextBox_RecommendedFunds.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_RecommendedFunds.CustomButton.UseSelectable = true;
            this.metroTextBox_RecommendedFunds.CustomButton.Visible = false;
            this.metroTextBox_RecommendedFunds.Lines = new string[0];
            this.metroTextBox_RecommendedFunds.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_RecommendedFunds.MaxLength = 32767;
            this.metroTextBox_RecommendedFunds.Multiline = true;
            this.metroTextBox_RecommendedFunds.Name = "metroTextBox_RecommendedFunds";
            this.metroTextBox_RecommendedFunds.PasswordChar = '\0';
            this.metroTextBox_RecommendedFunds.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_RecommendedFunds.SelectedText = "";
            this.metroTextBox_RecommendedFunds.SelectionLength = 0;
            this.metroTextBox_RecommendedFunds.SelectionStart = 0;
            this.metroTextBox_RecommendedFunds.ShortcutsEnabled = true;
            this.metroTextBox_RecommendedFunds.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_RecommendedFunds.TabIndex = 2;
            this.metroTextBox_RecommendedFunds.UseCustomBackColor = true;
            this.metroTextBox_RecommendedFunds.UseCustomForeColor = true;
            this.metroTextBox_RecommendedFunds.UseSelectable = true;
            this.metroTextBox_RecommendedFunds.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_RecommendedFunds.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
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
            // 
            // metroLabel_RecommendedFund
            // 
            this.metroLabel_RecommendedFund.AutoSize = true;
            this.metroLabel_RecommendedFund.Location = new System.Drawing.Point(10, 1015);
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
            this.metroPanel_OtherInformation.Controls.Add(this.metroTextBox_OtherInformation);
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
            // metroTextBox_OtherInformation
            // 
            // 
            // 
            // 
            this.metroTextBox_OtherInformation.CustomButton.Image = null;
            this.metroTextBox_OtherInformation.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_OtherInformation.CustomButton.Name = "";
            this.metroTextBox_OtherInformation.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_OtherInformation.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_OtherInformation.CustomButton.TabIndex = 1;
            this.metroTextBox_OtherInformation.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_OtherInformation.CustomButton.UseSelectable = true;
            this.metroTextBox_OtherInformation.CustomButton.Visible = false;
            this.metroTextBox_OtherInformation.Lines = new string[0];
            this.metroTextBox_OtherInformation.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_OtherInformation.MaxLength = 32767;
            this.metroTextBox_OtherInformation.Multiline = true;
            this.metroTextBox_OtherInformation.Name = "metroTextBox_OtherInformation";
            this.metroTextBox_OtherInformation.PasswordChar = '\0';
            this.metroTextBox_OtherInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_OtherInformation.SelectedText = "";
            this.metroTextBox_OtherInformation.SelectionLength = 0;
            this.metroTextBox_OtherInformation.SelectionStart = 0;
            this.metroTextBox_OtherInformation.ShortcutsEnabled = true;
            this.metroTextBox_OtherInformation.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_OtherInformation.TabIndex = 2;
            this.metroTextBox_OtherInformation.UseCustomBackColor = true;
            this.metroTextBox_OtherInformation.UseCustomForeColor = true;
            this.metroTextBox_OtherInformation.UseSelectable = true;
            this.metroTextBox_OtherInformation.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_OtherInformation.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroPanel_Motivation
            // 
            this.metroPanel_Motivation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_Motivation.Controls.Add(this.metroPanel3);
            this.metroPanel_Motivation.Controls.Add(this.metroTextBox_Motivation);
            this.metroPanel_Motivation.HorizontalScrollbarBarColor = true;
            this.metroPanel_Motivation.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_Motivation.HorizontalScrollbarSize = 10;
            this.metroPanel_Motivation.Location = new System.Drawing.Point(15, 1245);
            this.metroPanel_Motivation.Name = "metroPanel_Motivation";
            this.metroPanel_Motivation.Size = new System.Drawing.Size(972, 84);
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
            // metroTextBox_Motivation
            // 
            // 
            // 
            // 
            this.metroTextBox_Motivation.CustomButton.Image = null;
            this.metroTextBox_Motivation.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_Motivation.CustomButton.Name = "";
            this.metroTextBox_Motivation.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_Motivation.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_Motivation.CustomButton.TabIndex = 1;
            this.metroTextBox_Motivation.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_Motivation.CustomButton.UseSelectable = true;
            this.metroTextBox_Motivation.CustomButton.Visible = false;
            this.metroTextBox_Motivation.Lines = new string[0];
            this.metroTextBox_Motivation.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_Motivation.MaxLength = 32767;
            this.metroTextBox_Motivation.Multiline = true;
            this.metroTextBox_Motivation.Name = "metroTextBox_Motivation";
            this.metroTextBox_Motivation.PasswordChar = '\0';
            this.metroTextBox_Motivation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_Motivation.SelectedText = "";
            this.metroTextBox_Motivation.SelectionLength = 0;
            this.metroTextBox_Motivation.SelectionStart = 0;
            this.metroTextBox_Motivation.ShortcutsEnabled = true;
            this.metroTextBox_Motivation.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_Motivation.TabIndex = 2;
            this.metroTextBox_Motivation.UseCustomBackColor = true;
            this.metroTextBox_Motivation.UseCustomForeColor = true;
            this.metroTextBox_Motivation.UseSelectable = true;
            this.metroTextBox_Motivation.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_Motivation.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroPanel_FinancialSituation
            // 
            this.metroPanel_FinancialSituation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.metroPanel_FinancialSituation.Controls.Add(this.metroPanel1);
            this.metroPanel_FinancialSituation.Controls.Add(this.metroTextBox_FinancialSituation);
            this.metroPanel_FinancialSituation.HorizontalScrollbarBarColor = true;
            this.metroPanel_FinancialSituation.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel_FinancialSituation.HorizontalScrollbarSize = 10;
            this.metroPanel_FinancialSituation.Location = new System.Drawing.Point(15, 711);
            this.metroPanel_FinancialSituation.Name = "metroPanel_FinancialSituation";
            this.metroPanel_FinancialSituation.Size = new System.Drawing.Size(972, 84);
            this.metroPanel_FinancialSituation.TabIndex = 20;
            this.metroPanel_FinancialSituation.VerticalScrollbarBarColor = true;
            this.metroPanel_FinancialSituation.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel_FinancialSituation.VerticalScrollbarSize = 10;
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
            // metroTextBox_FinancialSituation
            // 
            // 
            // 
            // 
            this.metroTextBox_FinancialSituation.CustomButton.Image = null;
            this.metroTextBox_FinancialSituation.CustomButton.Location = new System.Drawing.Point(890, 2);
            this.metroTextBox_FinancialSituation.CustomButton.Name = "";
            this.metroTextBox_FinancialSituation.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.metroTextBox_FinancialSituation.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_FinancialSituation.CustomButton.TabIndex = 1;
            this.metroTextBox_FinancialSituation.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_FinancialSituation.CustomButton.UseSelectable = true;
            this.metroTextBox_FinancialSituation.CustomButton.Visible = false;
            this.metroTextBox_FinancialSituation.Lines = new string[0];
            this.metroTextBox_FinancialSituation.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox_FinancialSituation.MaxLength = 32767;
            this.metroTextBox_FinancialSituation.Multiline = true;
            this.metroTextBox_FinancialSituation.Name = "metroTextBox_FinancialSituation";
            this.metroTextBox_FinancialSituation.PasswordChar = '\0';
            this.metroTextBox_FinancialSituation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox_FinancialSituation.SelectedText = "";
            this.metroTextBox_FinancialSituation.SelectionLength = 0;
            this.metroTextBox_FinancialSituation.SelectionStart = 0;
            this.metroTextBox_FinancialSituation.ShortcutsEnabled = true;
            this.metroTextBox_FinancialSituation.Size = new System.Drawing.Size(966, 78);
            this.metroTextBox_FinancialSituation.TabIndex = 2;
            this.metroTextBox_FinancialSituation.UseCustomBackColor = true;
            this.metroTextBox_FinancialSituation.UseCustomForeColor = true;
            this.metroTextBox_FinancialSituation.UseSelectable = true;
            this.metroTextBox_FinancialSituation.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_FinancialSituation.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
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
            this.metroCheckBox_completed.Location = new System.Drawing.Point(995, 15);
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
            this.label_ongoing.Location = new System.Drawing.Point(671, 7);
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
            this.label_Initial.Location = new System.Drawing.Point(427, 7);
            this.label_Initial.Name = "label_Initial";
            this.label_Initial.Size = new System.Drawing.Size(100, 30);
            this.label_Initial.TabIndex = 7;
            this.label_Initial.Text = "Initial";
            this.label_Initial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Fees
            // 
            this.label_Fees.Location = new System.Drawing.Point(354, 15);
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
            this.metroTextBox9.CustomButton.Location = new System.Drawing.Point(122, 2);
            this.metroTextBox9.CustomButton.Name = "";
            this.metroTextBox9.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBox9.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox9.CustomButton.TabIndex = 1;
            this.metroTextBox9.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox9.CustomButton.UseSelectable = true;
            this.metroTextBox9.CustomButton.Visible = false;
            this.metroTextBox9.Lines = new string[0];
            this.metroTextBox9.Location = new System.Drawing.Point(523, 7);
            this.metroTextBox9.MaxLength = 32767;
            this.metroTextBox9.Name = "metroTextBox9";
            this.metroTextBox9.PasswordChar = '\0';
            this.metroTextBox9.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox9.SelectedText = "";
            this.metroTextBox9.SelectionLength = 0;
            this.metroTextBox9.SelectionStart = 0;
            this.metroTextBox9.ShortcutsEnabled = true;
            this.metroTextBox9.Size = new System.Drawing.Size(150, 30);
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
            this.metroTextBox8.CustomButton.Location = new System.Drawing.Point(122, 2);
            this.metroTextBox8.CustomButton.Name = "";
            this.metroTextBox8.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBox8.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox8.CustomButton.TabIndex = 1;
            this.metroTextBox8.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox8.CustomButton.UseSelectable = true;
            this.metroTextBox8.CustomButton.Visible = false;
            this.metroTextBox8.Lines = new string[0];
            this.metroTextBox8.Location = new System.Drawing.Point(770, 7);
            this.metroTextBox8.MaxLength = 32767;
            this.metroTextBox8.Name = "metroTextBox8";
            this.metroTextBox8.PasswordChar = '\0';
            this.metroTextBox8.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox8.SelectedText = "";
            this.metroTextBox8.SelectionLength = 0;
            this.metroTextBox8.SelectionStart = 0;
            this.metroTextBox8.ShortcutsEnabled = true;
            this.metroTextBox8.Size = new System.Drawing.Size(150, 30);
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
            this.metroTextBox_NoteDate.CustomButton.Location = new System.Drawing.Point(147, 2);
            this.metroTextBox_NoteDate.CustomButton.Name = "";
            this.metroTextBox_NoteDate.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBox_NoteDate.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_NoteDate.CustomButton.TabIndex = 1;
            this.metroTextBox_NoteDate.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_NoteDate.CustomButton.UseSelectable = true;
            this.metroTextBox_NoteDate.CustomButton.Visible = false;
            this.metroTextBox_NoteDate.Lines = new string[0];
            this.metroTextBox_NoteDate.Location = new System.Drawing.Point(115, 7);
            this.metroTextBox_NoteDate.MaxLength = 32767;
            this.metroTextBox_NoteDate.Name = "metroTextBox_NoteDate";
            this.metroTextBox_NoteDate.PasswordChar = '\0';
            this.metroTextBox_NoteDate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox_NoteDate.SelectedText = "";
            this.metroTextBox_NoteDate.SelectionLength = 0;
            this.metroTextBox_NoteDate.SelectionStart = 0;
            this.metroTextBox_NoteDate.ShortcutsEnabled = true;
            this.metroTextBox_NoteDate.Size = new System.Drawing.Size(175, 30);
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
            this.metroPanel_ImplementationMotivation.ResumeLayout(false);
            this.metroPanel_ProductImplemented.ResumeLayout(false);
            this.metroPanel_Notes.ResumeLayout(false);
            this.metroPanel_OtherImportantInfo.ResumeLayout(false);
            this.metroPanel_Copayment.ResumeLayout(false);
            this.metroPanel_LateJoiner.ResumeLayout(false);
            this.metroPanel_WaitingPeriods.ResumeLayout(false);
            this.metroPanel_ChronicConditions.ResumeLayout(false);
            this.metroPanel_Hospitalisation.ResumeLayout(false);
            this.metroPanel_MedicalCover.ResumeLayout(false);
            this.metroPanel_MedicalConditions.ResumeLayout(false);
            this.metroPanel_RecomendedFunds.ResumeLayout(false);
            this.metroPanel7.ResumeLayout(false);
            this.metroPanel_OtherInformation.ResumeLayout(false);
            this.metroPanel5.ResumeLayout(false);
            this.metroPanel_Motivation.ResumeLayout(false);
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel_FinancialSituation.ResumeLayout(false);
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


        #region Additional form elements

        private void InitialiseMedicalTable()
        {
            List<Label> vTableLbls = new List<Label>(); //Table horizontal labels
            List<Label> hTableLbls = new List<Label>(); //Table vertical labels
            List<MetroComboBox> listBoxes = new List<MetroComboBox>(); //Table List boxes
            List<MetroTextBox> textBoxes = new List<MetroTextBox>(); //Table text boxes

            //Hospitilisation Cover
            vl_HospitalCover.Text = "Hospitalisation Cover";
            vTableLbls.Add(this.vl_HospitalCover);
            listBoxes.Add(this.cmb_HospitalDiscussed);
            listBoxes.Add(this.cmb_HospitalTaken);
            textBoxes.Add(this.tb_hospitalCover);

            //DayToDay
            vl_DayToDay.Text = "Day-to-Day Benefit";
            vTableLbls.Add(this.vl_DayToDay);
            listBoxes.Add(this.cmb_DayToDayDiscussed);
            listBoxes.Add(this.cmb_DayToDayTaken);
            textBoxes.Add(this.tb_dayToDay);

            //Threshold Benefit
            vl_Threshold.Text = "Threshold Benefit";
            vTableLbls.Add(this.vl_Threshold);
            listBoxes.Add(this.cmb_ThresholdBenefitDiscussed);
            listBoxes.Add(this.cmb_ThresholdBenefitTaken);
            textBoxes.Add(this.tb_threshold);

            //ChronicBenefit
            vl_ChronicBenefit.Text = "Chronic Benefit";
            vTableLbls.Add(this.vl_ChronicBenefit);
            listBoxes.Add(this.cmb_ChronicBenefitDiscussed);
            listBoxes.Add(this.cmb_ChronicBenefitTaken);
            textBoxes.Add(this.tb_chronic);

            //Savings account
            vl_SavingsAccount.Text = "Savings Account";
            vTableLbls.Add(this.vl_SavingsAccount);
            listBoxes.Add(this.cmb_SavingsDiscussed);
            listBoxes.Add(this.cmb_SavingsTaken);
            textBoxes.Add(this.tb_savingsAccount);

            //HospitalPreference
            vl_Preference.Text = "Hospital Preference";
            vTableLbls.Add(this.vl_Preference);
            listBoxes.Add(this.cmb_HospitalDiscussed);
            listBoxes.Add(this.cmb_HospitalTaken);
            textBoxes.Add(this.tb_hospitalPreference);

            //Gap Cover
            vl_GapCover.Text = "Gap Cover";
            vTableLbls.Add(this.vl_GapCover);
            listBoxes.Add(this.cmb_GapCoverDiscussed);
            listBoxes.Add(this.cmb_GapCoverTaken);
            textBoxes.Add(this.tb_gapCover);

            //Other
            vl_Other.Text = "Other";
            vTableLbls.Add(this.vl_Other);
            listBoxes.Add(this.cmb_OtherDiscussed);
            listBoxes.Add(this.cmb_OtherTaken);
            textBoxes.Add(this.tb_other);


            //Format Vertical headings
            foreach (Label lbl in vTableLbls)
            {
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
            }

            //Format list boxes
            foreach (MetroComboBox dropDown in listBoxes)
            {
                dropDown.DropDownStyle = ComboBoxStyle.DropDownList;
                dropDown.Dock = DockStyle.Fill;
                dropDown.Items.Add("");
                dropDown.Items.Add("Yes");
                dropDown.Items.Add("No");

            }

            //Format text boxes
            foreach (MetroTextBox tb in textBoxes)
            {
                tb.Dock = DockStyle.Fill; ;
                tb.Multiline = true;
                tb.ScrollBars = ScrollBars.Vertical;

            }


            /*Define Horizontal Labels*/

            //Cover
            this.hl_Cover.Text = "Cover";
            hTableLbls.Add(this.hl_Cover);

            //Cover Discussed
            
            hl_CoverDiscussed.Text = "Cover\n Discussed";
            hTableLbls.Add(this.hl_CoverDiscussed);

            //Cover Taken
            
            hl_CoverTaken.Text = "Cover\n Taken";
            hTableLbls.Add(this.hl_CoverTaken);

            //Comments
            
            hl_Comment.Text = "Comments:";
            hl_Comment.TextAlign = ContentAlignment.MiddleLeft;
            hTableLbls.Add(this.hl_Comment);   


            //Format Horizontal headings
            foreach (Label lbl in hTableLbls)
            {
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
            }
           

        }

        private void InitialiseMedicalAidComparisonTable()
        {
            List<Label> vTableLbls = new List<Label>(); //Table horizontal labels
            List<Label> hTableLbls = new List<Label>(); //Table vertical labels
            List<MetroTextBox> textBoxes = new List<MetroTextBox>(); //Table text boxes

            //Policy/Application number
            vl_PolicyNo.Text = "Policy/Application Number";
            vTableLbls.Add(vl_PolicyNo);
            textBoxes.Add(tbPolicyNo_Current);
            textBoxes.Add(tbPolicyNo_Replaced);

            //Insurer
            vl_Insurer.Text = "Insurer";
            vTableLbls.Add(vl_Insurer);
            textBoxes.Add(tbInsurer_Current);
            textBoxes.Add(tbInsurer_Replaced);

            //Product Name
            vl_ProductName.Text = "Product Name";
            vTableLbls.Add(vl_ProductName);
            textBoxes.Add(tbProductName_Current);
            textBoxes.Add(tbProductName_Replaced);

            //Premium
            vl_Premium.Text = "Premium";
            vTableLbls.Add(vl_Premium);
            textBoxes.Add(tbPremium_Current);
            textBoxes.Add(tbPremium_Replaced);

            //Benefits
            vl_Benefits.Text = "Benefits";
            vTableLbls.Add(vl_Benefits);
            textBoxes.Add(tbBenefits_Current);
            textBoxes.Add(tbBenefits_Replaced);

            //Savings Account
            vl_compSavings.Text = "Savings Account";
            vTableLbls.Add(vl_compSavings);
            textBoxes.Add(tbCompSavings_Current);
            textBoxes.Add(tbCompSavings_Replaced);

            //Chronic Benefits
            vl_compChronic.Text = "Chronic Benefits";
            vTableLbls.Add(vl_compChronic);
            textBoxes.Add(tbCompChronic_Current);
            textBoxes.Add(tbCompChronic_Replaced);

            //Hospital Cover
            vl_compHospitalCover.Text = "Hospital Cover";
            vTableLbls.Add(vl_compHospitalCover);
            textBoxes.Add(tbCompHospitalCover_Current);
            textBoxes.Add(tbCompHospitalCover_Replaced);

            //Limits On Cover
            vl_LimitsOnCover.Text = "Limits On Cover";
            vTableLbls.Add(vl_LimitsOnCover);
            textBoxes.Add(tbLimitsOnCover_Current);
            textBoxes.Add(tbLimitsOnCover_Replaced);

            //Other
            vl_compOther.Text = "Other";
            vTableLbls.Add(vl_compOther);
            textBoxes.Add(tbCompOther_Current);
            textBoxes.Add(tbCompOther_Replaced);

            //Format Vertical headings
            foreach (Label lbl in vTableLbls)
            {
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
            }

            //Format text boxes
            foreach (MetroTextBox tb in textBoxes)
            {
                tb.Dock = DockStyle.Fill; ;
                tb.Multiline = true;
                tb.ScrollBars = ScrollBars.Vertical;

            }

           


            // Horizontal Lables

            //Detail
            hl_Detail.Text = "Detail";
            hTableLbls.Add(hl_Detail);

            //Current Medical schemes or proposed medical schemes
            hl_CurrentMedScheme.Text = "Current Medical Scheme\n OR\n Proposed Medical Scheme";
            hTableLbls.Add(hl_CurrentMedScheme);

            //Replaced medical scheme or proposed medical scheme
            hl_ReplacedMedScheme.Text = "Replaced Medical Scheme\n OR\n Proposed Medical Scheme";
            hTableLbls.Add(hl_ReplacedMedScheme);

            //Format Horizontal headings
            foreach (Label lbl in hTableLbls)
            {
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
            }

        }

        private void InitialiseRiskNeedsAndGoalsTable()
        {
            List<Label> vTableLbls = new List<Label>(); //Table horizontal labels
            List<Label> hTableLbls = new List<Label>(); //Table vertical labels
            List<MetroTextBox> numBoxes = new List<MetroTextBox>(); //Table text boxes meant for numbers
            List<MetroTextBox> textBoxes = new List<MetroTextBox>(); //Table text boxes
            List<MetroComboBox> cmbBoxes = new List<MetroComboBox>(); //Table Combo boxes
            List<MetroDateTime> dateEditors = new List<MetroDateTime>(); //Table date editors

            //Vertical Labels

            //Life
            this.vl_Life.Text = "Life";
            vTableLbls.Add(vl_Life);
            numBoxes.Add(tb_LifeNeedsQuantified);
            textBoxes.Add(tb_LifeNeedsPriority);
            textBoxes.Add(tb_LifeShortfall);
            cmbBoxes.Add(cmb_Life);
            dateEditors.Add(dtp_LifeReviewDate);

            //Permanent Disability
            this.vl_PDIncomeProtection.Text = "Permanent Disability (Income Protection)";
            vTableLbls.Add(vl_PDIncomeProtection);
            numBoxes.Add(tb_PDIncomeProtectionNeedsQuantified);
            textBoxes.Add(tb_PDIncomeProtectionNeedsPriority);
            textBoxes.Add(tb_PDIncomeProtectionShortfall);
            cmbBoxes.Add(cmb_PDIncomeProtection);
            dateEditors.Add(dtp_PDIncomeProtectionReviewDate);


            //Permanent Disability Lump Sum
            this.vl_PDLumpSum.Text = "Permanent Disability (Lump Sum)";
            vTableLbls.Add(vl_PDLumpSum);
            numBoxes.Add(tb_PDLumpSumNeedsQuantified);
            textBoxes.Add(tb_PDLumpSumNeedsPriority);
            textBoxes.Add(tb_PDLumpSumShortfall);
            cmbBoxes.Add(cmb_PDLumpSum);
            dateEditors.Add(dtp_PDLumpSumReviewDate);


            //Temporary Disability
            this.vl_TemporaryDisability.Text = "Temporary Disability";
            vTableLbls.Add(vl_TemporaryDisability);
            numBoxes.Add(tb_TemporaryDisabilityNeedsQuantified);
            textBoxes.Add(tb_TemporaryDisabilityNeedsPriority);
            textBoxes.Add(tb_TemporaryDisabilityShortfall);
            cmbBoxes.Add(cmb_TemporaryDisability);
            dateEditors.Add(dtp_TemporaryDisabilityReviewDate);


            //Trauma
            this.vl_Trauma.Text = "Trauma/Illness";
            vTableLbls.Add(vl_Trauma);
            numBoxes.Add(tb_TraumaNeedsQuantified);
            textBoxes.Add(tb_TraumaNeedsPriority);
            textBoxes.Add(tb_TraumaShortfall);
            cmbBoxes.Add(cmb_Trauma);
            dateEditors.Add(dtp_TraumaReviewDate);


            //Funeral Cover
            this.vl_FuneralCover.Text = "Funeral Cover/Immediate Expenses";
            vTableLbls.Add(vl_FuneralCover);
            numBoxes.Add(tb_FuneralCoverNeedsQuantified);
            textBoxes.Add(tb_FuneralCoverNeedsPriority);
            textBoxes.Add(tb_FuneralCoverShortfall);
            cmbBoxes.Add(cmb_FuneralCover);
            dateEditors.Add(dtp_FuneralCoverReviewDate);


            //Other
            this.vl_RiskOther.Text = "Other";
            vTableLbls.Add(vl_RiskOther);
            numBoxes.Add(tb_RiskOtherNeedsQuantified);
            textBoxes.Add(tb_RiskOtherNeedsPriority);
            textBoxes.Add(tb_RiskOtherShortfall);
            cmbBoxes.Add(cmb_RiskOther);
            dateEditors.Add(dtp_RiskOtherReviewDate);

            //Format Vertical headings
            foreach (Label lbl in vTableLbls)
            {
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
            }

            //Format numBoxes
            foreach (MetroTextBox tb in numBoxes)
            {
                tb.Dock = DockStyle.Fill; ;
                tb.Multiline = false;
            }

            //Format TextBoxes
            foreach (MetroTextBox tb in textBoxes)
            {
                tb.Dock = DockStyle.Fill; ;
                tb.Multiline = true;
                tb.ScrollBars = ScrollBars.Vertical;
            }

            //Format combo boxes
            foreach (MetroComboBox dropDown in cmbBoxes)
            {
                dropDown.DropDownStyle = ComboBoxStyle.DropDownList;
                dropDown.Dock = DockStyle.Fill;
                dropDown.Items.Add("");
                dropDown.Items.Add("Yes");
                dropDown.Items.Add("No");
                dropDown.Items.Add("Partially");
                dropDown.Items.Add("Later");

            }

            foreach (MetroDateTime dateEditor in dateEditors)
            { 
                dateEditor.Dock = DockStyle.Fill;
                dateEditor.Theme = MetroThemeStyle.Light;
                dateEditor.CustomFormat = " ";
                dateEditor.Format = DateTimePickerFormat.Custom;
            }

            //dtp_LifeReviewDate.Checked = false;
            //dtp_LifeReviewDate.Height = 42;
            //dtp_LifeReviewDate.CalendarForeColor = Color.Red;


            //Horizontal headings

            //Financial planning need
            this.hl_FinancialPlannningNeed.Text = "Financial planning need";
            hTableLbls.Add(this.hl_FinancialPlannningNeed);

            //Needs Qualified
            this.hl_NeedsQualified.Text = "Needs Qualified";
            hTableLbls.Add(this.hl_NeedsQualified);

            //Priority of needs to be addressed
            this.hl_PriorityOfNeeds.Text = "Priority of needs to be addressed";
            hTableLbls.Add(hl_PriorityOfNeeds);

            //Was the need fully addressd
            this.hl_NeedFullyAddressed.Text = "Was the need fully addressed";
            hTableLbls.Add(hl_NeedFullyAddressed);

            //Shortfall
            this.hl_Shortfall.Text = "Shortfall";
            hTableLbls.Add(hl_Shortfall);

            //Review date if the need was addressed
            //partially or is to be addressed later
            this.hl_ReviewDate.Text = "Review date if the need was \n addressed partially or is to \n be addressed later";
            hTableLbls.Add(hl_ReviewDate);

            //Format Horizontal headings
            foreach (Label lbl in hTableLbls)
            {
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        #endregion

        #region Medical Aid Needs and Goals elements

            //Cover Type
        private Label hl_Cover = new Label();

        //Cover Discussed
        private Label hl_CoverDiscussed = new Label();

        //Cover Taken
        private Label hl_CoverTaken = new Label();

        //Comments
        private Label hl_Comment = new Label();


        //Hospitalisation Cover
        private MetroComboBox cmb_HospitalDiscussed = new MetroComboBox();
        private MetroComboBox cmb_HospitalTaken = new MetroComboBox();
        private Label vl_HospitalCover = new Label();
        private MetroTextBox tb_hospitalCover = new MetroTextBox();


        //DayToDay
        private MetroComboBox cmb_DayToDayDiscussed = new MetroComboBox();
        private MetroComboBox cmb_DayToDayTaken = new MetroComboBox();
        private Label vl_DayToDay = new Label();
        private MetroTextBox tb_dayToDay = new MetroTextBox();

        //Threshold Benefit
        private MetroComboBox cmb_ThresholdBenefitDiscussed = new MetroComboBox();
        private MetroComboBox cmb_ThresholdBenefitTaken= new MetroComboBox();
        private Label vl_Threshold = new Label();
        private MetroTextBox tb_threshold = new MetroTextBox();


        //ChronicBenefit
        private MetroComboBox cmb_ChronicBenefitDiscussed = new MetroComboBox();
        private MetroComboBox cmb_ChronicBenefitTaken = new MetroComboBox();
        private Label vl_ChronicBenefit = new Label();
        private MetroTextBox tb_chronic = new MetroTextBox();

        //Savings account
        private MetroComboBox cmb_SavingsDiscussed = new MetroComboBox();
        private MetroComboBox cmb_SavingsTaken = new MetroComboBox();
        private Label vl_SavingsAccount = new Label();
        private MetroTextBox tb_savingsAccount = new MetroTextBox();

        //HospitalPreference
        private MetroComboBox cmb_HospitalPreferenceDiscussed = new MetroComboBox();
        private MetroComboBox cmb_HospitalPreferenceTaken = new MetroComboBox();
        private Label vl_Preference = new Label();
        private MetroTextBox tb_hospitalPreference = new MetroTextBox();

        //Gap Cover
        private MetroComboBox cmb_GapCoverDiscussed = new MetroComboBox();
        private MetroComboBox cmb_GapCoverTaken = new MetroComboBox();
        private Label vl_GapCover = new Label();
        private MetroTextBox tb_gapCover = new MetroTextBox();

        //Other
        private MetroComboBox cmb_OtherDiscussed = new MetroComboBox();
        private MetroComboBox cmb_OtherTaken = new MetroComboBox();
        private Label vl_Other = new Label();
        private MetroTextBox tb_other = new MetroTextBox();

        #endregion

        #region Medical Aid Comparison Table Elements


        //Horizontal Labels

        //Detail
        private Label hl_Detail = new Label();


        //Current Medical schemes or proposed medical schemes
        private Label hl_CurrentMedScheme = new Label();

        //Replaced medical scheme or proposed medical scheme
        private Label hl_ReplacedMedScheme = new Label();



        //Table rows

        //Policy/Application number
        private Label vl_PolicyNo = new Label();
        private MetroTextBox tbPolicyNo_Current = new MetroTextBox();
        private MetroTextBox tbPolicyNo_Replaced = new MetroTextBox();

        //Insurer
        private Label vl_Insurer = new Label();
        private MetroTextBox tbInsurer_Current = new MetroTextBox();
        private MetroTextBox tbInsurer_Replaced = new MetroTextBox();
        //Product Name
        private Label vl_ProductName = new Label();
        private MetroTextBox tbProductName_Current = new MetroTextBox();
        private MetroTextBox tbProductName_Replaced = new MetroTextBox();
        //Premium
        private Label vl_Premium= new Label();
        private MetroTextBox tbPremium_Current = new MetroTextBox();
        private MetroTextBox tbPremium_Replaced = new MetroTextBox();
        //Benefits
        private Label vl_Benefits = new Label();
        private MetroTextBox tbBenefits_Current = new MetroTextBox();
        private MetroTextBox tbBenefits_Replaced = new MetroTextBox();
        //Savings Account
        private Label vl_compSavings= new Label();
        private MetroTextBox tbCompSavings_Current = new MetroTextBox();
        private MetroTextBox tbCompSavings_Replaced = new MetroTextBox();
        //Chronic Benefits
        private Label vl_compChronic = new Label();
        private MetroTextBox tbCompChronic_Current = new MetroTextBox();
        private MetroTextBox tbCompChronic_Replaced = new MetroTextBox();
        //Hospital Cover
        private Label vl_compHospitalCover = new Label();
        private MetroTextBox tbCompHospitalCover_Current = new MetroTextBox();
        private MetroTextBox tbCompHospitalCover_Replaced = new MetroTextBox();
        //Limits on Cover
        private Label vl_LimitsOnCover = new Label();
        private MetroTextBox tbLimitsOnCover_Current = new MetroTextBox();
        private MetroTextBox tbLimitsOnCover_Replaced = new MetroTextBox();
        //Other
        private Label vl_compOther = new Label();
        private MetroTextBox tbCompOther_Current = new MetroTextBox();
        private MetroTextBox tbCompOther_Replaced = new MetroTextBox();

        #endregion

        #region Risk Needs and Goals Identified Elements
        //Horizontal labels

        //Financial planning need
        private Label hl_FinancialPlannningNeed= new Label(); 

        //Needs Qualified
        private Label hl_NeedsQualified = new Label();

        //Priority of needs to be addressed
        private Label hl_PriorityOfNeeds = new Label();

        //Was the need fully addressed
        private Label hl_NeedFullyAddressed = new Label();

        //Shortfall
        private Label hl_Shortfall = new Label();

        //Review date if the need was addressed
        //partially or is to be addressed later
        private Label hl_ReviewDate = new Label();

        //Table rows

        //Life
        private Label vl_Life = new Label();
        private MetroTextBox tb_LifeNeedsQuantified = new MetroTextBox();
        private MetroTextBox tb_LifeNeedsPriority = new MetroTextBox();
        private MetroComboBox cmb_Life = new MetroComboBox();
        private MetroTextBox tb_LifeShortfall = new MetroTextBox();
        private MetroDateTime dtp_LifeReviewDate = new MetroDateTime();

        
       
        //MetroDateTimeEditor

        //Permanent Disability (Income Protection)
        private Label vl_PDIncomeProtection = new Label();
        private MetroTextBox tb_PDIncomeProtectionNeedsQuantified = new MetroTextBox();
        private MetroTextBox tb_PDIncomeProtectionNeedsPriority = new MetroTextBox();
        private MetroComboBox cmb_PDIncomeProtection = new MetroComboBox();
        private MetroTextBox tb_PDIncomeProtectionShortfall = new MetroTextBox();
        private MetroDateTime dtp_PDIncomeProtectionReviewDate = new MetroDateTime();

        //Permanent Disability (Lump Sum)
        private Label vl_PDLumpSum = new Label();
        private MetroTextBox tb_PDLumpSumNeedsQuantified = new MetroTextBox();
        private MetroTextBox tb_PDLumpSumNeedsPriority = new MetroTextBox();
        private MetroComboBox cmb_PDLumpSum = new MetroComboBox();
        private MetroTextBox tb_PDLumpSumShortfall = new MetroTextBox();
        private MetroDateTime dtp_PDLumpSumReviewDate = new MetroDateTime();

        //Temporary Disability
        private Label vl_TemporaryDisability= new Label();
        private MetroTextBox tb_TemporaryDisabilityNeedsQuantified = new MetroTextBox();
        private MetroTextBox tb_TemporaryDisabilityNeedsPriority = new MetroTextBox();
        private MetroComboBox cmb_TemporaryDisability = new MetroComboBox();
        private MetroTextBox tb_TemporaryDisabilityShortfall = new MetroTextBox();
        private MetroDateTime dtp_TemporaryDisabilityReviewDate = new MetroDateTime();

        //Trauma/Illness
        private Label vl_Trauma = new Label();
        private MetroTextBox tb_TraumaNeedsQuantified = new MetroTextBox();
        private MetroTextBox tb_TraumaNeedsPriority = new MetroTextBox();
        private MetroComboBox cmb_Trauma = new MetroComboBox();
        private MetroTextBox tb_TraumaShortfall = new MetroTextBox();
        private MetroDateTime dtp_TraumaReviewDate = new MetroDateTime();

        //Funeral Cover
        private Label vl_FuneralCover = new Label();
        private MetroTextBox tb_FuneralCoverNeedsQuantified = new MetroTextBox();
        private MetroTextBox tb_FuneralCoverNeedsPriority = new MetroTextBox();
        private MetroComboBox cmb_FuneralCover = new MetroComboBox();
        private MetroTextBox tb_FuneralCoverShortfall = new MetroTextBox();
        private MetroDateTime dtp_FuneralCoverReviewDate = new MetroDateTime();

        //Other
        private Label vl_RiskOther = new Label();
        private MetroTextBox tb_RiskOtherNeedsQuantified = new MetroTextBox();
        private MetroTextBox tb_RiskOtherNeedsPriority = new MetroTextBox();
        private MetroComboBox cmb_RiskOther = new MetroComboBox();
        private MetroTextBox tb_RiskOtherShortfall = new MetroTextBox();
        private MetroDateTime dtp_RiskOtherReviewDate = new MetroDateTime();

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
        private MetroPanel metroPanel_FinancialSituation;
        private MetroTextBox metroTextBox_FinancialSituation;
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
        private MetroTextBox metroTextBox_OtherInformation;
        private MetroPanel metroPanel_Motivation;
        private MetroPanel metroPanel3;
        private MetroTextBox metroTextBox2;
        private MetroTextBox metroTextBox_Motivation;
        private MetroLabel metroLabel_MotivationHint;
        private MetroLabel metroLabel_Motivation;
        private MetroPanel metroPanel_RecomendedFunds;
        private MetroPanel metroPanel7;
        private MetroTextBox metroTextBox6;
        private MetroTextBox metroTextBox_RecommendedFunds;
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
        private MetroTextBox metroTextBox_CurrentMedicalCover;
        private MetroLabel metroLabel_MedicalCoverHint;
        private MetroLabel metroLabel_MedicalCover;
        private MetroLabel metroLabel_HospitalisationHint;
        private MetroLabel metroLabel_Hospitalisation;
        private MetroPanel metroPanel_Hospitalisation;
        private MetroTextBox metroTextBox_Hospitalisation;
        private TableLayoutPanel tblPanel_NeedsAndGoals;
        private MetroLabel metroLabel_NeedsAndGoals;
        private MetroLabel metroLabel_ChronicConditionsHint;
        private MetroLabel metroLabel_ChronicConditions;
        private MetroPanel metroPanel_ChronicConditions;
        private MetroTextBox metroTextBox_ChronicConditions;
        private MetroLabel metroLabel_WaitingPeriodHint;
        private MetroLabel metroLabel_LateJoiner;
        private MetroLabel metroLabel_copayment;
        private MetroLabel metroLabel_WaitingPeriod;
        private MetroPanel metroPanel_WaitingPeriods;
        private MetroTextBox metroTextBox_WaitingPeriods;
        private MetroPanel metroPanel_LateJoiner;
        private MetroTextBox metroTextBox_LateJoiner;
        private MetroLabel metroLabel_LateJoinerHint;
        private MetroPanel metroPanel_Copayment;
        private MetroTextBox metroTextBox_Copayment;
        private MetroLabel metroLabel_CopaymentHint;
        private MetroLabel metroLabel_RecommendedProductHint;
        private MetroPanel metroPanel_OtherImportantInfo;
        private MetroTextBox metroTextBox_OtherImportantInfo;
        private MetroLabel metroLabel_productImplemented;
        private MetroLabel metroLabel_OtherImportantInfoHint;
        private MetroLabel metroLabel_OtherImportantInfo;
        private MetroPanel metroPanel_Notes;
        private MetroTextBox metroTextBox_Notes;
        private MetroLabel metroLabel_Notes;
        private MetroLabel metroLabel_ComparisonMedicalSchemeHint;
        private Label label_ComparisonMedicalScheme;
        private TableLayoutPanel tblPanel_MedicalSchemeComparison;
        private MetroLabel metroLabel_ImplementationAdviceHint;
        private Label label_ImplementationAdvice;
        private MetroPanel metroPanel_ProductImplemented;
        private MetroTextBox metroTextBox_ProductImplemented;
        private MetroLabel metroLabel_ImplementationMotivationHint;
        private MetroPanel metroPanel_ImplementationMotivation;
        private MetroTextBox metroTextBox_ImplementationMotivation;
        private MetroLabel metroLabel_ImplementationMotivation;
        private TableLayoutPanel tblPanel_RiskNeedsAndGoals;
    }
}