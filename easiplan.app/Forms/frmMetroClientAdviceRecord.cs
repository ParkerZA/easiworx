using DevAge.ComponentModel;
using Finx.App.Enums;
using Finx.App.Extensions;
using easiplan.domain.Entities;
using MetroFramework;
using MetroFramework.Controls.Ext;
using MetroFramework.Controls;
using MetroFramework.Forms;
using my.domain.lib.core.Domain;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Finx.App.UserControls;
using easiplan.domain;
using System.Management;
using DocumentFormat.OpenXml.EMMA;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using Microsoft.Graph;
using Google.Protobuf.WellKnownTypes;
using System.Globalization;

namespace Finx.App.Forms
{
    public partial class frmMetroClientAdviceRecord : MetroForm
    {
        ClientSearchModel searchModel = new ClientSearchModel();

        #region Delegates / Events
        public delegate Instruction GetInstructionEventHandler(object sender, EventArgs args);
        public event GetInstructionEventHandler GetInstructionEvent;
        public event EventHandler AddClientInstructionEvent;
        public event EventHandler UpdateClientInstructionEvent;
        public event EventHandler DeleteClientInstructionEvent;
        #endregion

        #region Locals
        bool ReadOnly = false;

        string InvestmentType = "";
        Retirement Retirement = null;
        Investment Investment = null;
        Education Education = null;
        Medical Medical = null;
        Life Life = null;
        IncomeAsset IncomeAsset = null;

        Instruction Instruction = null;//Reference to an Admin Task

        Need Need = null;
        EducationNeed EducationNeed = null;
        InvestmentNeed InvestmentNeed = null;

        //Note lists for standard CAR
        IList<ClientAdviceRecord> Notes = new List<ClientAdviceRecord>();
        IList<ClientAdviceRecord> filteredNotes = new List<ClientAdviceRecord>();

        //Note lists for Medical Aid CAR
        IList<MedicalAidAdviceRecord> MedicalNotes = new List<MedicalAidAdviceRecord>();
        IList<MedicalAidAdviceRecord> medicalFilteredNotes = new List<MedicalAidAdviceRecord>();

        //Note lists for Risk CAR
        IList<RiskAdviceRecord> RiskNotes = new List<RiskAdviceRecord>();
        IList<RiskAdviceRecord> riskFilteredNotes = new List<RiskAdviceRecord>();


        ClientAdviceRecord selectedNote = null;
        ClientAdviceRecord mostRecentRecord = null;
        PolicyAction Action = PolicyAction.AmendPolicy;


        #endregion

        #region Public Variables
        public Client Client { get; set; }
        public object SelectedItem { get; set; }




        #endregion

        #region Constructors

        //Base Constructor
        public frmMetroClientAdviceRecord(string Title, bool readOnly, PolicyAction action)
        {
            InitializeComponent();
            


            ReadOnly = readOnly;
            Action = action;

            this.Text = Title;// string.Empty;
            //this.SubTitle = string.Format("{0}", "Policy Notes");

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ResizeRedraw = true;

            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);

            this.DisplayHeader = false;
            #endregion

            #region xToolBarMenu1

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "Client Advice Record");
            this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.notes_32;

            xToolBarMenu1.tbRefresh.Visible = !ReadOnly && Action == PolicyAction.AmendPolicy; xToolBarMenu1.tbRefresh.Text = "Delete";
            xToolBarMenu1.RefreshClicked += toolStripButton_Delete_Click;
            xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;

            xToolBarMenu1.tbSave.Visible = !ReadOnly;
            xToolBarMenu1.SaveClicked += toolStripButton_Save_Click;

            xToolBarMenu1.tbEdit.Visible = !ReadOnly && Action == PolicyAction.AmendPolicy; xToolBarMenu1.tbEdit.Text = "New Note";
            xToolBarMenu1.EditClicked += toolStripButton_Add_Click;

            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;
            
            #endregion

            //Assign Event Handlers

            #region Assign event handlers for all form text boxes

            //Standard template text boxes
            metroTextBox_NeedAndObjective.TextChanged += NeedAndObv_propertyChanged_EventHandler; //Needs and Objectives text box
            metroTextBox_FinancialSituation.TextChanged += FinancialSituation_propertyChanged_EventHandler; //Financial Situation text box
            metroTextBox_OtherInformation.TextChanged += OtherInformation_propertyChanged_EventHandler; //Financial Situation text box
            metroTextBox_RecommendedFunds.TextChanged += RecommendedFunds_propertyChanged_EventHandler; //Recommended funds text box
            metroTextBox_Motivation.TextChanged += Motivation_propertyChanged_EventHandler; //Motivation text box

            //Medical Aid Text Boxes
            metroTextBox_MedicalConditions.TextChanged += MedicalConditions_propertyChanged_EventHandler; //Medical Conditions text box
            metroTextBox_CurrentMedicalCover.TextChanged += MedicalCover_propertyChanged_EventHandler; //Current Medical Cover text box
            metroTextBox_Hospitalisation.TextChanged += Hospitalisation_propertyChanged_EventHandler; //Hospitalisation text box
            metroTextBox_ChronicConditions.TextChanged += ChronicConditions_propertyChanged_EventHandler; //Chronic Conditions text box
            metroTextBox_WaitingPeriods.TextChanged += WaitingPeriod_propertyChanged_EventHandler; //Waiting Periods text box
            metroTextBox_LateJoiner.TextChanged += LateJoyner_propertyChanged_EventHandler; //Late Joiner Penalty text box
            metroTextBox_Copayment.TextChanged += CoPayments_propertyChanged_EventHandler; //Co-Payment text box
            metroTextBox_OtherImportantInfo.TextChanged += OtherImportantInformation_propertyChanged_EventHandler; //Other important information text box
            metroTextBox_Notes.TextChanged+= Notes_propertyChanged_EventHandler; //Notes text box
            metroTextBox_ProductImplemented.TextChanged += ImplementedProduct_propertyChanged_EventHandler; // Implemented product text box
            metroTextBox_ImplementationMotivation.TextChanged += ImplementedMotivation_propertyChanged_EventHandler; // Implemented motivaion text box

            #endregion

            #region Event handlers for Medical Aid needs and goals table comment boxes

            //Needs and Goals Table text box changed handlers
            tb_hospitalCover.TextChanged += CoverComment_propertyChanged_EventHandler; //Hospitalisation Cover comment box
            tb_dayToDay.TextChanged += DayToDayComment_propertyChanged_EventHandler; //Day To Day comment box
            tb_threshold.TextChanged += ThresholdComment_propertyChanged_EventHandler; //Threshold Benefit comment box
            tb_chronic.TextChanged += ChronicComment_propertyChanged_EventHandler; //Chronic Benefit comment box
            tb_savingsAccount.TextChanged += SavingsComment_propertyChanged_EventHandler; //Savings Account comment box
            tb_hospitalPreference.TextChanged += HospitalPreferenceComment_propertyChanged_EventHandler; //Hospital Preference comment box
            tb_gapCover.TextChanged += GapCoverComment_propertyChanged_EventHandler; //Gap Cover comment box
            tb_other.TextChanged += OtherComment_propertyChanged_EventHandler; //Other comment box

            #endregion

            #region Event hanlders for Medical scheme comparison table text boxes

            //Current medical schemes
            tbPolicyNo_Current.TextChanged += PolicyNo_Current_propertyChanged_EventHandler;
            tbInsurer_Current.TextChanged += Insurer_Current_propertyChanged_EventHandler;
            tbProductName_Current.TextChanged += ProductName_Current_propertyChanged_EventHandler;
            tbPremium_Current.TextChanged += Premium_Current_propertyChanged_EventHandler;
            tbBenefits_Current.TextChanged += Benefits_Current_propertyChanged_EventHandler;
            tbCompSavings_Current.TextChanged += SavingsAccount_Current_propertyChanged_EventHandler;
            tbCompChronic_Current.TextChanged += ChronicBenefit_Current_propertyChanged_EventHandler;
            tbCompHospitalCover_Current.TextChanged += HospitalCover_Current_propertyChanged_EventHandler;
            tbLimitsOnCover_Current.TextChanged += LimitsOnCover_Current_propertyChanged_EventHandler;
            tbCompOther_Current.TextChanged += Other_Current_propertyChanged_EventHandler;

            //Replaced medical schemes
            tbPolicyNo_Replaced.TextChanged += PolicyNo_Replaced_propertyChanged_EventHandler;
            tbInsurer_Replaced.TextChanged += Insurer_Replaced_propertyChanged_EventHandler;
            tbProductName_Replaced.TextChanged += ProductName_Replaced_propertyChanged_EventHandler;
            tbPremium_Replaced.TextChanged += Premium_Replaced_propertyChanged_EventHandler;
            tbBenefits_Replaced.TextChanged += Benefits_Replaced_propertyChanged_EventHandler;
            tbCompSavings_Replaced.TextChanged += SavingsAccount_Replaced_propertyChanged_EventHandler;
            tbCompChronic_Replaced.TextChanged += ChronicBenefit_Replaced_propertyChanged_EventHandler;
            tbCompHospitalCover_Replaced.TextChanged += HospitalCover_Replaced_propertyChanged_EventHandler;
            tbLimitsOnCover_Replaced.TextChanged += LimitsOnCover_Replaced_propertyChanged_EventHandler;
            tbCompOther_Replaced.TextChanged += Other_Replaced_propertyChanged_EventHandler;

            #endregion

            #region Event hanlders for Medical Aid needs and goals combo boxes

            //SetComboBox event handers

            //Rename these variables when you get a chance so that they are less confusing
            this.Cover1.SelectedIndexChanged += Cover1_SelectedIndexChanged;
            this.Cover2.SelectedIndexChanged += Cover2_SelectedIndexChanged;

            this.DayToDay1.SelectedIndexChanged += DayToDay1_SelectedIndexChanged;
            this.DayToDay2.SelectedIndexChanged += DayToDay2_SelectedIndexChanged;

            this.Threshold1.SelectedIndexChanged += Threshold1_SelectedIndexChanged;
            this.Threshold2.SelectedIndexChanged += Threshold2_SelectedIndexChanged;

            this.ChronicBenefit1.SelectedIndexChanged += ChronicBenefit1_SelectedIndexChanged;
            this.ChronicBenefit2.SelectedIndexChanged += ChronicBenefit2_SelectedIndexChanged;

            this.Savings1.SelectedIndexChanged += SavingsAccount1_SelectedIndexChanged;
            this.Savings2.SelectedIndexChanged += SavingsAccount2_SelectedIndexChanged;

            this.HospitalPreference1.SelectedIndexChanged += HospitalPreference1_SelectedIndexChanged;
            this.HospitalPreference2.SelectedIndexChanged += HospitalPreference2_SelectedIndexChanged;

            this.GapCover1.SelectedIndexChanged += GapCover1_SelectedIndexChanged;
            this.GapCover2.SelectedIndexChanged += GapCover2_SelectedIndexChanged;

            this.Other1.SelectedIndexChanged += Other1_SelectedIndexChanged;
            this.Other2.SelectedIndexChanged += Other2_SelectedIndexChanged;

            #endregion

            #region Event handlers for risk needs and goals table

            //Life row
            this.tb_LifeNeedsQuantified.TextChanged += Life_NeedsQuantified_propertyChanged_EventHandler;
            this.tb_LifeNeedsPriority.TextChanged += Life_NeedsPriority_propertyChanged_EventHandler;
            this.cmb_Life.SelectedIndexChanged += Life_NeedAddressed_propertyChanged_EventHandler;
            this.tb_LifeShortfall.TextChanged += Life_Shortfall_propertyChanged_EventHandler;
            this.dtp_LifeReviewDate.ValueChanged += Life_ReviewDate_propertyChanged_EventHandler;

            //Income Protection row
            this.tb_PDIncomeProtectionNeedsQuantified.TextChanged += PDIncomeProtection_NeedsQuantified_propertyChanged_EventHandler;
            this.tb_PDIncomeProtectionNeedsPriority.TextChanged += PDIncomeProtection_NeedsPriority_propertyChanged_EventHandler;
            this.cmb_PDIncomeProtection.SelectedIndexChanged += PDIncomeProtection_NeedAddressed_propertyChanged_EventHandler;
            this.tb_PDIncomeProtectionShortfall.TextChanged += PDIncomeProtection_Shortfall_propertyChanged_EventHandler;
            this.dtp_PDIncomeProtectionReviewDate.ValueChanged += PDIncomeProtection_ReviewDate_propertyChanged_EventHandler;

            //Lump sum row
            this.tb_PDLumpSumNeedsQuantified.TextChanged += PDLumpSum_NeedsQuantified_propertyChanged_EventHandler;
            this.tb_PDLumpSumNeedsPriority.TextChanged += PDLumpSum_NeedsPriority_propertyChanged_EventHandler;
            this.cmb_PDLumpSum.SelectedIndexChanged += PDLumpSum_NeedAddressed_propertyChanged_EventHandler;
            this.tb_PDLumpSumShortfall.TextChanged += PDLumpSum_Shortfall_propertyChanged_EventHandler;
            this.dtp_PDLumpSumReviewDate.ValueChanged += PDLumpSum_ReviewDate_propertyChanged_EventHandler;

            //Temporary Disability row
            this.tb_TemporaryDisabilityNeedsQuantified.TextChanged += TemporaryDisability_NeedsQuantified_propertyChanged_EventHandler;
            this.tb_TemporaryDisabilityNeedsPriority.TextChanged += TemporaryDisability_NeedsPriority_propertyChanged_EventHandler;
            this.cmb_TemporaryDisability.SelectedIndexChanged += TemporaryDisability_NeedAddressed_propertyChanged_EventHandler;
            this.tb_TemporaryDisabilityShortfall.TextChanged += TemporaryDisability_Shortfall_propertyChanged_EventHandler;
            this.dtp_TemporaryDisabilityReviewDate.ValueChanged += TemporaryDisability_ReviewDate_propertyChanged_EventHandler;

            //Trauma row
            this.tb_TraumaNeedsQuantified.TextChanged += TraumaAndIllness_NeedsQuantified_propertyChanged_EventHandler;
            this.tb_TraumaNeedsPriority.TextChanged += TraumaAndIllness_NeedsPriority_propertyChanged_EventHandler;
            this.cmb_Trauma.SelectedIndexChanged += TraumaAndIllness_NeedAddressed_propertyChanged_EventHandler;
            this.tb_TraumaShortfall.TextChanged += TraumaAndIllness_Shortfall_propertyChanged_EventHandler;
            this.dtp_TraumaReviewDate.ValueChanged += TraumaAndIllness_ReviewDate_propertyChanged_EventHandler;

            //Funeral Cover row
            this.tb_FuneralCoverNeedsQuantified.TextChanged += FuneralCover_NeedsQuantified_propertyChanged_EventHandler;
            this.tb_FuneralCoverNeedsPriority.TextChanged += FuneralCover_NeedsPriority_propertyChanged_EventHandler;
            this.cmb_FuneralCover.SelectedIndexChanged += FuneralCover_NeedAddressed_propertyChanged_EventHandler;
            this.tb_FuneralCoverShortfall.TextChanged += FuneralCover_Shortfall_propertyChanged_EventHandler;
            this.dtp_FuneralCoverReviewDate.ValueChanged += FuneralCover_ReviewDate_propertyChanged_EventHandler;

            //Other row
            this.tb_RiskOtherNeedsQuantified.TextChanged += Other_NeedsQuantified_propertyChanged_EventHandler;
            this.tb_RiskOtherNeedsPriority.TextChanged += Other_NeedsPriority_propertyChanged_EventHandler;
            this.cmb_RiskOther.SelectedIndexChanged += Other_NeedAddressed_propertyChanged_EventHandler;
            this.tb_RiskOtherShortfall.TextChanged += Other_Shortfall_propertyChanged_EventHandler;
            this.dtp_RiskOtherReviewDate.ValueChanged += Other_ReviewDate_propertyChanged_EventHandler;

            #endregion

            #region Assign key down event to allow for datestamp in text boxes

            //Assigning key down methods to allow for adding date to text boxes with cntrl D
            metroTextBox_FinancialSituation.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_NeedAndObjective.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_OtherInformation.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_RecommendedFunds.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_Motivation.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_ProductImplemented.KeyDown += textBox_AppendNewLineDate; 
            metroTextBox_ImplementationMotivation.KeyDown += textBox_AppendNewLineDate;

            metroTextBox_MedicalConditions.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_CurrentMedicalCover.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_Hospitalisation.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_ChronicConditions.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_WaitingPeriods.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_LateJoiner.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_Copayment.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_OtherImportantInfo.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_Notes.KeyDown += textBox_AppendNewLineDate;

            #endregion

            this.xInput_ShowCompletedTasks.ControlTypes = ControlTypes.CheckBox;
            this.xInput_ShowCompletedTasks.MappedField = "ShowCompletedTask";
            this.xInput_ShowCompletedTasks.LableText = "Show Completed notes";
            this.xInput_ShowCompletedTasks.Model = searchModel;
            this.xInput_ShowCompletedTasks.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInput_ShowCompletedTasks.chkBox.CheckedChanged += XInput_ShowCompletedTask_KeyPressed;

            this.metroCheckBox_completed.CheckedChanged += XInput_ShowCompletedTask_KeyPressed;
        }

        
        //Retirement constructor
        public frmMetroClientAdviceRecord(Retirement model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Retirement = model;
            InvestmentType = "Retirement";

            Initialise_SelectPanel(model.AdviceRecords);
           

        }

        //Non retirement constructor
        public frmMetroClientAdviceRecord(Investment model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");


            Investment = model;
            InvestmentType = "Investment";

            Initialise_SelectPanel(model.AdviceRecords);


        }

        //Education constructor
        public frmMetroClientAdviceRecord(Education model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Education = model;
            InvestmentType = "Education";
            Initialise_SelectPanel(model.AdviceRecords);

        }

        //Medical Aid constructor
        public frmMetroClientAdviceRecord(Medical model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Medical = model;
            InvestmentType = "Medical";

            InitialiseMedicalTable();
            InitialiseMedicalAidComparisonTable();
            Initialise_MedicalSelectPanel(model.AdviceRecords);

        }
        
        //Risk constructor
        public frmMetroClientAdviceRecord(Life model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Life = model;
            InvestmentType = "risk";
            InitialiseRiskNeedsAndGoalsTable();
            Initialise_RiskSelectPanel(model.AdviceRecords);

        }

        //Income Asset Constructor
        public frmMetroClientAdviceRecord(IncomeAsset model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            IncomeAsset = model;
            InvestmentType = "IncomeAsset";
            Initialise_SelectPanel(model.AdviceRecords);

        }
        /*
        public frmMetroClientAdviceRecord(Need model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            Need = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroClientAdviceRecord(EducationNeed model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            EducationNeed = model;

            Initialise_SelectPanel(model.Notes);

        }

        public frmMetroClientAdviceRecord(InvestmentNeed model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            InvestmentNeed = model;

            Initialise_SelectPanel(model.Notes);

        }
        
        public frmMetroClientAdviceRecord(Instruction model, bool readOnly, PolicyAction action) : this(model.Name, readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Advice has not yet been saved. Please Update advice");

            Instruction = model;

            if (Instruction.InstructionType == InstructionType.UNKNOWN)
                Instruction.InstructionType = InstructionTypeExt.ToType(Instruction.Type);

            switch (Instruction.InstructionType)
            {

                case InstructionType.ASSET_POLICY_NOTE:
                    IncomeAsset = Program.Repository.Get<IncomeAsset, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(IncomeAsset.Notes);
                    break;
                case InstructionType.EDU_POLICY_NOTE:
                    Education = Program.Repository.Get<Education, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(Education.Notes);
                    break;
                case InstructionType.INVEST_POLICY_NOTE:
                    Investment = Program.Repository.Get<Investment, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(Investment.Notes);
                    break;
                case InstructionType.LIFE_POLICY_NOTE:
                    Life = Program.Repository.Get<Life, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(Life.Notes);
                    break;
                case InstructionType.MEDICAL_POLICY_NOTE:
                    Medical = Program.Repository.Get<Medical, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(Medical.Notes);
                    break;
                case InstructionType.RETIRE_POLICY_NOTE:
                    Retirement = Program.Repository.Get<Retirement, int>(Instruction.ReferenceId);
                    Initialise_SelectPanel(Retirement.Notes);
                    break;
                default:
                    throw new MyValidationException("Unknown Instruction Type");

            }
        



        }*/
        #endregion

        //These select panels are coded very mesily.. should refactor to make fewer lines of code repeat
        #region Initialisation select panels

        //Initialise the select panel for regular CAR template
        void Initialise_SelectPanel(IList<ClientAdviceRecord> notes = null)
        {
            if (notes != null)
                Notes = notes;

            if (Notes == null)
                filteredNotes = new List<ClientAdviceRecord>();
            else
                filteredNotes = Notes.Where(x => x.CreateDate != null).ToList();

            #region Notes Grid
            this.dataGrid_Notes.Initialise1(filteredNotes, column =>
            {

                column.For(c => c.AdviceDate, "Note Date", new DateEditor(), MinWidth: 100);
                column.For(x => x.UpdateDate, "Last Date", new DateEditor());
                column.For(x => x.UpdateBy, "Updated By", new StringEditor());
                column.For(c => c.IsCompleted, "Completed");
            },
            RowSelectEventHandler: Notes_RowSelectEventHandlerChanged,
            ReadOnly: true,
            AllowDelete: false)
            .Format1(true, fixedCols: 1);
            #endregion

            if (Notes.Count > 0)
                selectedNote = filteredNotes.LastOrDefault();
            else
                selectedNote = new ClientAdviceRecord();

            //Select Bar

            metroTextBox_NoteDate.Text = selectedNote.AdviceDate.ToString("dd-MMM-yyyy hh:mm");


            Initialise_PolicyNotePanel(selectedNote);
        }

        //Initialise select panel for medical aid CAR template
        void Initialise_MedicalSelectPanel(IList<MedicalAidAdviceRecord> notes = null)
        {
            if (notes != null)
                MedicalNotes = notes;

            if (MedicalNotes == null)
                medicalFilteredNotes = new List<MedicalAidAdviceRecord>();
            else
                medicalFilteredNotes = MedicalNotes.Where(x => x.CreateDate != null).ToList();

            #region Notes Grid
            this.dataGrid_Notes.Initialise1(medicalFilteredNotes, column =>
            {

                column.For(c => c.AdviceDate, "Note Date", new DateEditor(), MinWidth: 100);
                column.For(x => x.UpdateDate, "Last Date", new DateEditor());
                column.For(x => x.UpdateBy, "Updated By", new StringEditor());
                column.For(c => c.IsCompleted, "Completed");

            },
            RowSelectEventHandler: MedicalNotes_RowSelectEventHandlerChanged,
            ReadOnly: true,
            AllowDelete: false)
            .Format1(true, fixedCols: 1);
            #endregion


            if (MedicalNotes.Count > 0)
                selectedNote = medicalFilteredNotes.LastOrDefault();
            else
                selectedNote = new MedicalAidAdviceRecord();

            //Select Bar

            metroTextBox_NoteDate.Text = selectedNote.AdviceDate.ToString("dd-MMM-yyyy hh:mm");


            Initialise_PolicyNotePanel((ClientAdviceRecord)selectedNote);
        }

        //Initialise the Risk CAR select panel
        void Initialise_RiskSelectPanel(IList<RiskAdviceRecord> notes = null)
        {
            if (notes != null)
                RiskNotes = notes;

            if (RiskNotes == null)
                riskFilteredNotes = new List<RiskAdviceRecord>();
            else
                riskFilteredNotes = RiskNotes.Where(x => x.CreateDate != null).ToList();

            #region Notes Grid
            this.dataGrid_Notes.Initialise1(riskFilteredNotes, column =>
            {

                column.For(c => c.AdviceDate, "Note Date", new DateEditor(), MinWidth: 100);
                column.For(x => x.UpdateDate, "Last Date", new DateEditor());
                column.For(x => x.UpdateBy, "Updated By", new StringEditor());
                column.For(c => c.IsCompleted, "Completed");

            },
            RowSelectEventHandler: RiskNotes_RowSelectEventHandlerChanged,
            ReadOnly: true,
            AllowDelete: false)
            .Format1(true, fixedCols: 1);
            #endregion


            if (RiskNotes.Count > 0)
                selectedNote = riskFilteredNotes.LastOrDefault();
            else
                selectedNote = new RiskAdviceRecord();

            //Select Bar

            metroTextBox_NoteDate.Text = selectedNote.AdviceDate.ToString("dd-MMM-yyyy hh:mm");


            Initialise_PolicyNotePanel((ClientAdviceRecord)selectedNote);
        }

        //Initialise the policynote panel
        void Initialise_PolicyNotePanel(ClientAdviceRecord record)
        {
            //this.metroPanel_Select.Controls.Clear();
            ClearMetroPanel(this.metroPanel_AdviceRecord);

            if (selectedNote == null)
                return;

            selectedNote.IsLoading = true;


            switch (InvestmentType.ToLower())
            {
                case "retirement":
                    this.xToolBarMenu1.tbCaption.Text = "Client Advice Record [CAR] - Retirement Portfolio";
                    mostRecentRecord = Retirement.AdviceRecords.LastOrDefault();
                    populateRetirement(record);
                    InitializeStandardPortfolio();
                    

                    break;
                case "investment":
                    this.xToolBarMenu1.tbCaption.Text = "Client Advice Record [CAR] - Investment Portfolio";
                    mostRecentRecord = Investment.AdviceRecords.LastOrDefault();
                    
                    populateRetirement(record);
                    InitializeStandardPortfolio();

                    break;
                case "medical":
                    this.xToolBarMenu1.tbCaption.Text = "Client Advice Record [CAR] - Medical Portfolio";
                    mostRecentRecord = Medical.AdviceRecords.LastOrDefault();
                    
                    populateMedical((MedicalAidAdviceRecord)record);
                    initialiseMedicalPortfolio();

                    break;
                case "education":
                    this.xToolBarMenu1.tbCaption.Text = "Client Advice Record [CAR] - Education Portfolio";
                    mostRecentRecord = Education.AdviceRecords.LastOrDefault();
                    
                    populateRetirement(record);
                    InitializeStandardPortfolio();
                    
                    
                    break;
                case "risk":
                    this.xToolBarMenu1.tbCaption.Text = "Client Advice Record [CAR] - Risk Portfolio";
                    mostRecentRecord = Life.AdviceRecords.LastOrDefault();

                    populateRisk((RiskAdviceRecord)record);
                    initialiseRiskPortfolio();
                    


                    break;
                case "incomeasset":
                    this.xToolBarMenu1.tbCaption.Text = "Client Advice Record [CAR] - Income Assets Portfolio";
                    mostRecentRecord = IncomeAsset.AdviceRecords.LastOrDefault();

                    populateRetirement(record);
                    InitializeStandardPortfolio();
                    
                    
                    break;
            }

            
            selectedNote.IsLoading = false;


            //Check set the Tool Bar options depending on completetion status of the CAR 
            if (selectedNote.IsCompleted)
            {
                //Lock the form if note is completed
                lockForm(true);
                if (mostRecentRecord.IsCompleted)
                {
                    this.xToolBarMenu1.CarModeAddRecord(true);
                }
                else 
                {
                    this.xToolBarMenu1.CarModeDelete(true);
                }
            }
            else
            {
                //Open form if not
                lockForm(false);
                this.xToolBarMenu1.SetCAREdit(true);
            }
        }

        #endregion

        #region Build Screens
        //Initialise the form screen for Retirement, Non-retirement, education, and income asset portfolios

        //Build the regular CAR template
        void InitializeStandardPortfolio()
        {
            //When comparing the form designer to the actual form in the running application, the pixel distance
            //between elements on the CAR screen ended up being different from each other. This meant when I specified
            //pixel locations to place a control on the screen, it would not go where I placed it. I created the variables
            //below to programically determine the correct locations for the controls

            //Messy code though... should refactor

            #region Distance calculations for form element placement
            //Distance calculations to allow for the placement of form elements

            //X value distance between a heading or hint and the left hand margin
            int xTextMarginLeft = this.metroLabel_MedicalCover.Location.X;

            //X value Distance between a Panel and the left hand margin
            int xPanelMarginLeft = this.metroPanel_MedicalCover.Location.X;

            //Y value distance between a panel and the next heading beneath it
            int ySpaceAfterPanel = this.metroPanel_MedicalCover.Height + (this.metroLabel_NeedsAndObj.Location.Y - (this.metroPanel_AccessCapital.Location.Y + this.metroPanel_AccessCapital.Height));

            //Y value distance between a heading and the hint beneath it
            int ySpaceAfterHeading = this.metroLabel_NeedsAndObjHint.Location.Y - this.metroLabel_NeedsAndObj.Location.Y;

            //Y value distance between a hint and the panel beneath it
            int ySpaceAfterHint = this.metroPanel_needAndObj.Location.Y - this.metroLabel_NeedsAndObjHint.Location.Y;
            #endregion

            //Summary (Title)
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Summary);

            //Product Knowledge and Experience
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_PKE);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_PKEHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_PKE);

            //Investment Horizon
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_InvestHorizen);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_InvestHorizonHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_InvestHorizen);

            //Access to Capital
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_AccessToCapital);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_AccessToCapitalHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_AccessCapital);

            //Needs and Objectives
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_NeedsAndObj);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_NeedsAndObjHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_needAndObj);

            //Financial Solution
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_FinancialSolution);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_FinancialSolutionHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_FinancialSituation);

            //Other Information
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherInformation);

            //Recommended Product
            this.metroPanel_AdviceRecord.Controls.Add(this.label_InitialAdvice);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_RecommendedProductHint);
            
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_RecommendedFund);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_RecomendedFunds);
            //this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherInformation);

            //Motivation 
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Motivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MotivationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Motivation);

            //this.metroPanel_PolicyNote.Controls.Add(new MetroScrollBar );


            //Implemented Recommendation Advice
            this.label_ImplementationAdvice.Location = new Point(xTextMarginLeft, metroPanel_Motivation.Location.Y + ySpaceAfterPanel);
            this.metroLabel_ImplementationAdviceHint.Location = new Point(xTextMarginLeft, this.label_ImplementationAdvice.Location.Y + ySpaceAfterHeading);

            this.metroPanel_AdviceRecord.Controls.Add(this.label_ImplementationAdvice);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationAdviceHint);

            // Implemented Recomended product
            this.metroLabel_productImplemented.Location = new Point(xTextMarginLeft, this.metroLabel_ImplementationAdviceHint.Location.Y + ySpaceAfterHint);
            this.metroPanel_ProductImplemented.Location = new Point(xPanelMarginLeft, metroLabel_productImplemented.Location.Y + ySpaceAfterHint);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_productImplemented);
            this.metroPanel_AdviceRecord.Controls.Add(metroPanel_ProductImplemented);

            // Implemented Motivation
            this.metroLabel_ImplementationMotivation.Location = new Point(xTextMarginLeft, metroPanel_ProductImplemented.Location.Y + ySpaceAfterPanel);
            this.metroLabel_ImplementationMotivationHint.Location = new Point(xTextMarginLeft, this.metroLabel_ImplementationMotivation.Location.Y + ySpaceAfterHeading);
            this.metroPanel_ImplementationMotivation.Location = new Point(xPanelMarginLeft, this.metroLabel_ImplementationMotivationHint.Location.Y + this.metroLabel_ImplementationMotivationHint.Height + ySpaceAfterHeading);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationMotivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationMotivationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_ImplementationMotivation);



            this.metroPanel_AdviceRecord.PerformLayout();
        }

        //Build the Medical Aid CAR template
        void initialiseMedicalPortfolio()
        {
            //Distance calculations to allow for the placement of form elements

            #region Distance calculations

            //X value distance between a heading or hint and the left hand margin
            int xTextMarginLeft = this.metroLabel_MedicalCover.Location.X;

            //X value Distance between a Panel and the left hand margin
            int xPanelMarginLeft = this.metroPanel_MedicalCover.Location.X; 

            //Y value distance between a panel and the next heading beneath it
            int ySpaceAfterPanel = this.metroPanel_MedicalCover.Height + (this.metroLabel_MedicalConditions.Location.Y - (this.metroPanel_PKE.Location.Y + this.metroPanel_PKE.Height)); 

            //Y value distance between a heading and the hint beneath it
            int ySpaceAfterHeading = this.metroLabel_MedicalCoverHint.Location.Y - this.metroLabel_MedicalCover.Location.Y; 

            //Y value distance between a hint and the panel beneath it
            int ySpaceAfterHint = this.metroPanel_MedicalCover.Location.Y - this.metroLabel_MedicalCoverHint.Location.Y; 
            
            //Location of Initial Recomendation heading on medical aid panel
            int YInitialRecommendation = this.tblPanel_MedicalSchemeComparison.Location.Y + this.tblPanel_MedicalSchemeComparison.Height + (this.metroLabel_MedicalConditions.Location.Y - (this.metroPanel_PKE.Location.Y + this.metroPanel_PKE.Height));

            #endregion

            //Adding controls into correct positions

            //Summary (Title)
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Summary);

            //Product Knowledge and Experience
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_PKE);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_PKEHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_PKE);

            //Medical Conditions
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalConditions);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalConditionsHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_MedicalConditions);

            //Current Medical Cover
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalCover);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalCoverHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_MedicalCover);
            
            //Other Information
            this.metroLabel_OtherInformation.Location = new Point(xTextMarginLeft, this.metroPanel_MedicalCover.Location.Y+ ySpaceAfterPanel);
            this.metroLabel_OtherInformationHint.Location = new System.Drawing.Point(xTextMarginLeft, this.metroLabel_OtherInformation.Location.Y+ ySpaceAfterHeading);
            this.metroPanel_OtherInformation.Location = new System.Drawing.Point(xPanelMarginLeft, this.metroLabel_OtherInformationHint.Location.Y + ySpaceAfterHint);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherInformation);

            //Hospitalisation
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Hospitalisation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_HospitalisationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Hospitalisation);

            //Needs and goals identified table
            NeedsAndGoalsTable_AddLabels();
            NeedsAndGoalsTable_AddComboBoxes();
            NeedsAndGoalsTable_AddTextBoxes();

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_NeedsAndGoals);
            this.metroPanel_AdviceRecord.Controls.Add(tblPanel_NeedsAndGoals);

            //Chronic Conditions
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ChronicConditions);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ChronicConditionsHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_ChronicConditions);

            //Waiting periods
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_WaitingPeriod);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_WaitingPeriodHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_WaitingPeriods);

            //Late Joiner Penalty
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_LateJoiner);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_LateJoinerHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_LateJoiner);

            //Co-Payments
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_copayment);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_CopaymentHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Copayment);

            //Other important Information
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherImportantInfo);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherImportantInfoHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherImportantInfo);

            //Notes
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Notes);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Notes);

            //Medical Scheme Comparison Table
            ComparisonTable_AddLabels();
            ComparisonTable_AddTextBoxes();

            this.metroPanel_AdviceRecord.Controls.Add(this.label_ComparisonMedicalScheme);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ComparisonMedicalSchemeHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.tblPanel_MedicalSchemeComparison);

            //Initial Recommendation Advice
            this.label_InitialAdvice.Location = new Point(xTextMarginLeft, YInitialRecommendation);
            this.metroLabel_RecommendedProductHint.Location = new Point(xTextMarginLeft, this.label_InitialAdvice.Location.Y+ ySpaceAfterHeading);

            this.metroPanel_AdviceRecord.Controls.Add(this.label_InitialAdvice);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_RecommendedProductHint);

            // Initial Recomended product
            this.metroLabel_RecommendedFund.Location = new Point(xTextMarginLeft, this.metroLabel_RecommendedProductHint.Location.Y + ySpaceAfterHint);
            this.metroPanel_RecomendedFunds.Location = new Point(xPanelMarginLeft, this.metroLabel_RecommendedFund.Location.Y + ySpaceAfterHint);
            
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_RecommendedFund);
            this.metroPanel_AdviceRecord.Controls.Add(metroPanel_RecomendedFunds);

            // Initial Motivation
            this.metroLabel_Motivation.Location=new Point(xTextMarginLeft, this.metroPanel_RecomendedFunds.Location.Y + ySpaceAfterPanel);
            this.metroLabel_MotivationHint.Location = new Point(xTextMarginLeft, this.metroLabel_Motivation.Location.Y + ySpaceAfterHeading);
            this.metroPanel_Motivation.Location = new Point(xPanelMarginLeft, this.metroLabel_MotivationHint.Location.Y+this.metroLabel_MotivationHint.Height+ ySpaceAfterHeading);
            
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Motivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MotivationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Motivation);

            //Implemented Recommendation Advice
            this.label_ImplementationAdvice.Location = new Point(xTextMarginLeft, metroPanel_Motivation.Location.Y+ ySpaceAfterPanel);
            this.metroLabel_ImplementationAdviceHint.Location = new Point(xTextMarginLeft, this.label_ImplementationAdvice.Location.Y + ySpaceAfterHeading);

            this.metroPanel_AdviceRecord.Controls.Add(this.label_ImplementationAdvice);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationAdviceHint);

            // Implemented Recomended product
            this.metroLabel_productImplemented.Location = new Point(xTextMarginLeft, this.metroLabel_ImplementationAdviceHint.Location.Y + ySpaceAfterHint);
            this.metroPanel_ProductImplemented.Location = new Point(xPanelMarginLeft, metroLabel_productImplemented.Location.Y + ySpaceAfterHint);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_productImplemented);
            this.metroPanel_AdviceRecord.Controls.Add(metroPanel_ProductImplemented);

            // Implemented Motivation
            this.metroLabel_ImplementationMotivation.Location = new Point(xTextMarginLeft, metroPanel_ProductImplemented.Location.Y + ySpaceAfterPanel);
            this.metroLabel_ImplementationMotivationHint.Location = new Point(xTextMarginLeft, this.metroLabel_ImplementationMotivation.Location.Y + ySpaceAfterHeading);
            this.metroPanel_ImplementationMotivation.Location = new Point(xPanelMarginLeft, this.metroLabel_ImplementationMotivationHint.Location.Y + this.metroLabel_ImplementationMotivationHint.Height + ySpaceAfterHeading);
            
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationMotivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationMotivationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_ImplementationMotivation);

            this.metroPanel_AdviceRecord.PerformLayout();
        }

        //Build Risk CAR template
        void initialiseRiskPortfolio()
        {

            #region Distance calculations for form setup

            //Distance calculations to allow for the placement of form elements

            //X value distance between a heading or hint and the left hand margin
            int xTextMarginLeft = this.metroLabel_MedicalCover.Location.X;

            //X value Distance between a Panel and the left hand margin
            int xPanelMarginLeft = this.metroPanel_MedicalCover.Location.X;

            //Y value distance between a panel and the next heading beneath it
            int ySpaceAfterPanel = this.metroPanel_PKE.Height + (this.metroLabel_PKE.Location.Y - (this.metroLabel_Summary.Location.Y + this.metroLabel_Summary.Height));

            //Y value distance between a heading and the hint beneath it
            int ySpaceAfterHeading = this.metroLabel_MedicalCoverHint.Location.Y - this.metroLabel_MedicalCover.Location.Y;

            //Y value distance between a hint and the panel beneath it
            int ySpaceAfterHint = this.metroPanel_MedicalCover.Location.Y - this.metroLabel_MedicalCoverHint.Location.Y; 

            #endregion

            //Adding controls into correct positions

            //Summary (Title)
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Summary);

            //Product Knowledge and Experience
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_PKE);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_PKEHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_PKE);

            //Needs and Objectives
            this.metroLabel_NeedsAndObj.Location = new Point(xTextMarginLeft, this.metroPanel_PKE.Location.Y + ySpaceAfterPanel);
            this.metroLabel_NeedsAndObjHint.Location = new Point(xTextMarginLeft, this.metroLabel_NeedsAndObj.Location.Y + ySpaceAfterHeading);
            this.metroPanel_needAndObj.Location = new Point(xPanelMarginLeft, this.metroLabel_NeedsAndObjHint.Location.Y + ySpaceAfterHint);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_NeedsAndObj);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_NeedsAndObjHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_needAndObj);

            //Financial Solution
            this.metroLabel_FinancialSolution.Location = new Point(xTextMarginLeft, this.metroPanel_needAndObj.Location.Y + ySpaceAfterPanel);
            this.metroLabel_FinancialSolutionHint.Location = new Point(xTextMarginLeft, this.metroLabel_FinancialSolution.Location.Y + ySpaceAfterHeading);
            this.metroPanel_FinancialSituation.Location = new Point(xPanelMarginLeft, this.metroLabel_FinancialSolutionHint.Location.Y + ySpaceAfterHint);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_FinancialSolution);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_FinancialSolutionHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_FinancialSituation);

            //Other Information
            this.metroLabel_OtherInformation.Location = new Point(xTextMarginLeft, this.metroPanel_FinancialSituation.Location.Y + ySpaceAfterPanel);
            this.metroLabel_OtherInformationHint.Location = new Point(xTextMarginLeft, this.metroLabel_OtherInformation.Location.Y + ySpaceAfterHeading);
            this.metroPanel_OtherInformation.Location = new Point(xPanelMarginLeft, this.metroLabel_OtherInformationHint.Location.Y + ySpaceAfterHint);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherInformation);

            //Financial Needs and goals
            this.metroLabel_NeedsAndGoals.Location = new Point(xTextMarginLeft, this.metroPanel_OtherInformation.Location.Y + ySpaceAfterPanel);
            this.tblPanel_RiskNeedsAndGoals.Location = new Point(xPanelMarginLeft, this.metroLabel_NeedsAndGoals.Location.Y + ySpaceAfterHint);

            RiskNeedsAndGoalsAddLabels();
            RiskNeedsAndGoalsAddControls();

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_NeedsAndGoals);
            this.metroPanel_AdviceRecord.Controls.Add(this.tblPanel_RiskNeedsAndGoals);

            //Initial Recommendation Advice
            //Location of Initial Recomendation heading on medical aid panel
            int YInitialRecommendation = tblPanel_RiskNeedsAndGoals.Location.Y + tblPanel_RiskNeedsAndGoals.Height + (this.metroLabel_PKE.Location.Y - (this.metroLabel_Summary.Location.Y + this.metroLabel_Summary.Height));


            this.label_InitialAdvice.Location = new Point(xTextMarginLeft, YInitialRecommendation);//+ ySpaceAfterPanel);
            this.metroLabel_RecommendedProductHint.Location = new Point(xTextMarginLeft, this.label_InitialAdvice.Location.Y + ySpaceAfterHeading);

            this.metroPanel_AdviceRecord.Controls.Add(this.label_InitialAdvice);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_RecommendedProductHint);

            // Initial Recomended product
            this.metroLabel_RecommendedFund.Location = new Point(xTextMarginLeft, this.metroLabel_RecommendedProductHint.Location.Y + ySpaceAfterHint);
            this.metroPanel_RecomendedFunds.Location = new Point(xPanelMarginLeft, this.metroLabel_RecommendedFund.Location.Y + ySpaceAfterHint);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_RecommendedFund);
            this.metroPanel_AdviceRecord.Controls.Add(metroPanel_RecomendedFunds);

            // Initial Motivation
            this.metroLabel_Motivation.Location = new Point(xTextMarginLeft, this.metroPanel_RecomendedFunds.Location.Y + ySpaceAfterPanel);
            this.metroLabel_MotivationHint.Location = new Point(xTextMarginLeft, this.metroLabel_Motivation.Location.Y + ySpaceAfterHeading);
            this.metroPanel_Motivation.Location = new Point(xPanelMarginLeft, this.metroLabel_MotivationHint.Location.Y + this.metroLabel_MotivationHint.Height + ySpaceAfterHeading);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Motivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MotivationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Motivation);

            //Implemented Recommendation Advice
            this.label_ImplementationAdvice.Location = new Point(xTextMarginLeft, metroPanel_Motivation.Location.Y + ySpaceAfterPanel);
            this.metroLabel_ImplementationAdviceHint.Location = new Point(xTextMarginLeft, this.label_ImplementationAdvice.Location.Y + ySpaceAfterHeading);

            this.metroPanel_AdviceRecord.Controls.Add(this.label_ImplementationAdvice);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationAdviceHint);

            // Implemented Recomended product
            this.metroLabel_productImplemented.Location = new Point(xTextMarginLeft, this.metroLabel_ImplementationAdviceHint.Location.Y + ySpaceAfterHint);
            this.metroPanel_ProductImplemented.Location = new Point(xPanelMarginLeft, metroLabel_productImplemented.Location.Y + ySpaceAfterHint);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_productImplemented);
            this.metroPanel_AdviceRecord.Controls.Add(metroPanel_ProductImplemented);

            // Implemented Motivation
            this.metroLabel_ImplementationMotivation.Location = new Point(xTextMarginLeft, metroPanel_ProductImplemented.Location.Y + ySpaceAfterPanel);
            this.metroLabel_ImplementationMotivationHint.Location = new Point(xTextMarginLeft, this.metroLabel_ImplementationMotivation.Location.Y + ySpaceAfterHeading);
            this.metroPanel_ImplementationMotivation.Location = new Point(xPanelMarginLeft, this.metroLabel_ImplementationMotivationHint.Location.Y + this.metroLabel_ImplementationMotivationHint.Height + ySpaceAfterHeading);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationMotivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_ImplementationMotivationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_ImplementationMotivation);

            this.metroPanel_AdviceRecord.PerformLayout();

        }



        #endregion

        #region Lock Form control methods controls

        private void lockForm(bool state)
        {
            //Lock Product Knowledge
            lockButtons(metroPanel_PKE, state);

            //Lock investment Horizon
            lockButtons(metroPanel_InvestHorizen, state);

            //Lock Access to Capital
            lockButtons(metroPanel_AccessCapital, state);

            //Lock metro panel
            foreach (Control control in metroPanel_AdviceRecord.Controls)
            {
                if (control is MetroPanel tp)
                {
                    // Perform actions with the MetroTextBox
                    // textBox.Text will give you the text value of the MetroTextBox
                    lockTextPanel(tp, state);
                }
            }

            this.tblPanel_NeedsAndGoals.Enabled = !state;
            this.tblPanel_MedicalSchemeComparison.Enabled = !state;

        }

        //Used to lock a group of buttons
        private void lockButtons(MetroPanel btnGroup, bool state)
        {
            foreach (Control control in btnGroup.Controls)
            {
                if (control is Button) // Assuming you want to make textboxes uneditable
                {
                    Button button = (Button)control;
                    button.Enabled = !state;
                }
            }
        }

        //Used to lock the text box placed inside a panel
        private void lockTextPanel(MetroPanel txtGroup, bool state)
        {
            foreach (Control control in txtGroup.Controls)
            {
                if (control is MetroTextBox) // Assuming you want to make textboxes uneditable
                {
                    MetroTextBox tb = (MetroTextBox)control;
                    tb.Enabled = !state;
                }
            }
        }

        #endregion

       
        #region toolStripButton Events
        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedNote.Id == 0)
                {
                    InstructionType instructionType = InstructionType.UNKNOWN;
                    int referenceId = 0;
                    string referenceNumber = "";
                    string comment = "";

                    if (this.Retirement != null)
                    {
                        this.Retirement.AdviceRecords.Add(selectedNote);

                        instructionType = InstructionType.RETIRE_POLICY_NOTE;
                        referenceId = this.Retirement.Id;
                        referenceNumber = this.Retirement.ReferenceNo;
                        comment = this.Retirement.Description;
                    }
                    
                    if (this.Investment != null)
                    {
                        this.Investment.AdviceRecords.Add(selectedNote);

                        instructionType = InstructionType.INVEST_POLICY_NOTE; ;
                        referenceId = this.Investment.Id;
                        referenceNumber = this.Investment.ReferenceNo;
                        comment = this.Investment.Description;
                    }
                    if (this.Education != null)
                    {
                        this.Education.AdviceRecords.Add(selectedNote);

                        instructionType = InstructionType.EDU_POLICY_NOTE;
                        referenceId = this.Education.Id;
                        referenceNumber = this.Education.ReferenceNo;
                        comment = this.Education.Description;

                    }
                    
                    if (this.Medical != null)
                    {
                        //MedicalTable_Save((MedicalAidAdviceRecord)selectedNote);
                        //MedicalSchemeComparisonSave((MedicalAidAdviceRecord)selectedNote);
                        this.Medical.AdviceRecords.Add((MedicalAidAdviceRecord)selectedNote);

                        instructionType = InstructionType.MEDICAL_POLICY_NOTE;
                        referenceId = this.Medical.Id;
                        referenceNumber = this.Medical.ReferenceNo;
                        comment = this.Medical.Description;
                    }
                    if (this.Life != null)
                    {
                        this.Life.AdviceRecords.Add((RiskAdviceRecord)selectedNote);
                        //Add risk save here
                        instructionType = InstructionType.LIFE_POLICY_NOTE;
                        referenceId = this.Life.Id;
                        referenceNumber = this.Life.ReferenceNo;
                        comment = this.Life.Description;

                    }
                    if (this.IncomeAsset != null)
                    {
                        this.IncomeAsset.AdviceRecords.Add(selectedNote);

                        instructionType = InstructionType.ASSET_POLICY_NOTE;
                        referenceId = this.IncomeAsset.Id;
                        referenceNumber = this.IncomeAsset.ReferenceNo;
                        comment = this.IncomeAsset.Description;

                    }
                    /*
                    if (this.Need != null)
                    {
                        this.Need.Notes.Add(selectedNote);

                        instructionType = InstructionType.RETIRE_POLICY_NOTE;
                        referenceId = this.Need.Id;
                        referenceNumber = this.Need.ReferenceNo;
                        comment = this.Need.Description;
                    }

                    if (this.EducationNeed != null)
                    {
                        this.EducationNeed.Notes.Add(selectedNote);

                        instructionType = InstructionType.EDU_POLICY_NOTE;
                        referenceId = this.EducationNeed.Id;
                        referenceNumber = this.EducationNeed.ReferenceNo;
                        comment = this.EducationNeed.Description;
                    }

                    if (this.InvestmentNeed != null)
                    {
                        this.InvestmentNeed.Notes.Add(selectedNote);

                        instructionType = InstructionType.INVEST_POLICY_NOTE;
                        referenceId = this.InvestmentNeed.Id;
                        referenceNumber = this.InvestmentNeed.ReferenceNo;
                        comment = this.InvestmentNeed.Description;
                    }
                    */
                    /*
                    if (MessageBoxExt.ShowQuestion("Do you wish to create a Task for this note?"))
                    {
                        Instruction = new Instruction()
                        {
                            Status = InstructionStatus.UpdatePending.ToText(),
                            InstructionType = instructionType,
                            Type = instructionType.ToText(),
                            Comment = comment,
                            ReferenceId = referenceId,
                            ReferenceNo = referenceNumber,

                            UpdateDate = DateTime.Now,
                            UpdateBy = Program.User.Username
                        };

                        //Add the Admin Task
                        AddClientInstructionEvent?.Invoke(Instruction, new EventArgs());

                        selectedNote.InstructionId = Instruction.Id;
                    }*/
                }

                UpdateNote();

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                //xToolBarMenu1.CarModeDelete(true);
            }
        }
        private void toolStripButton_Add_Click(object sender, EventArgs e)
        {
            try
            {

                if (selectedNote != null)
                {
                    if (selectedNote.Id == 0)
                        return;
                }

                if (InvestmentType.ToLower().Contains("medical"))
                {
                    
                    //mostRecentRecord = (MedicalAidAdviceRecord)mostRecentRecord;

                    if (mostRecentRecord != null)
                    {
                        //Console.WriteLine("theres a recent record");

                        selectedNote = new MedicalAidAdviceRecord((MedicalAidAdviceRecord)mostRecentRecord);
                        Initialise_PolicyNotePanel(selectedNote);
                    }
                    else
                    {
                        selectedNote = new MedicalAidAdviceRecord();
                        Initialise_PolicyNotePanel(selectedNote);
                    }

                }
                else if (InvestmentType.ToLower().Contains("risk"))
                {
                    
                    //mostRecentRecord = (MedicalAidAdviceRecord)mostRecentRecord;

                    if (mostRecentRecord != null)
                    {
                        //Console.WriteLine("theres a recent record");

                        selectedNote = new RiskAdviceRecord((RiskAdviceRecord)mostRecentRecord);
                        Initialise_PolicyNotePanel(selectedNote);
                    }
                    else
                    {
                        selectedNote = new RiskAdviceRecord();
                        Initialise_PolicyNotePanel(selectedNote);
                    }

                }
                else
                {
                    

                    if (mostRecentRecord != null)
                    {
                        //Console.WriteLine("theres a recent record");

                        selectedNote = new ClientAdviceRecord(mostRecentRecord);
                        Initialise_PolicyNotePanel(selectedNote);
                    }
                    else
                    {
                        selectedNote = new ClientAdviceRecord();
                        Initialise_PolicyNotePanel(selectedNote);
                    }

                }
                //clearAllElements();

                

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                xToolBarMenu1.SetCAREditBeforeSave(true);
            }
        }

        

        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedNote.Id != 0)
                {
                    if (!Program.User.IsAdministrator)
                    {
                        MessageBoxExt.ShowWarning("You must have Administrator role to remove this Note");
                        return;
                    }

                    if (!MessageBoxExt.ShowQuestion("Are you sure you wish to delete this note ?"))
                        return;

                    //Fix once know whats klapping
                    //Update the Admin Task
                    /*
                    if (selectedNote.InstructionId > 0)
                    {
                        Instruction = GetInstructionEvent(selectedNote.InstructionId, new EventArgs());
                        if (Instruction != null)
                        {
                            //Instruction.Status = InstructionStatus.Cancelled.ToText();
                            DeleteClientInstructionEvent?.Invoke(Instruction, new EventArgs());

                        }
                    }*/

                    if (this.Retirement != null)
                    {
                        this.Retirement.AdviceRecords.Remove(selectedNote);
                        Program.Repository.Update<Retirement, int>(this.Retirement);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Retirement.AdviceRecords);
                    }
                    
                    if (this.Investment != null)
                    {
                        this.Investment.AdviceRecords.Remove(selectedNote);
                        Program.Repository.Update<Investment, int>(this.Investment);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Investment.AdviceRecords);

                    }
                    if (this.Education != null)
                    {
                        this.Education.AdviceRecords.Remove(selectedNote);
                        Program.Repository.Update<Education, int>(this.Education);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Education.AdviceRecords);

                    }
                    if (this.Medical != null)
                    {
                        this.Medical.AdviceRecords.Remove((MedicalAidAdviceRecord)selectedNote);
                        Program.Repository.Update<Medical, int>(this.Medical);

                        selectedNote = null;
                        Initialise_MedicalSelectPanel(this.Medical.AdviceRecords);
                    }
                    if (this.Life != null)
                    {
                        this.Life.AdviceRecords.Remove((RiskAdviceRecord)selectedNote);
                        Program.Repository.Update<Life, int>(this.Life);

                        selectedNote = null;
                        Initialise_RiskSelectPanel(this.Life.AdviceRecords);

                    }
                    if (this.IncomeAsset != null)
                    {
                        this.IncomeAsset.AdviceRecords.Remove(selectedNote);
                        Program.Repository.Update<IncomeAsset, int>(this.IncomeAsset);

                        selectedNote = null;
                        Initialise_SelectPanel(this.IncomeAsset.AdviceRecords);
                    }/*
                    if (this.Need != null)
                    {
                        this.Need.Notes.Remove(selectedNote);
                        Program.Repository.Update<Need, int>(this.Need);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Need.Notes);
                    }
                    if (this.EducationNeed != null)
                    {
                        this.EducationNeed.Notes.Remove(selectedNote);
                        Program.Repository.Update<EducationNeed, int>(this.EducationNeed);

                        selectedNote = null;
                        Initialise_SelectPanel(this.EducationNeed.Notes);
                    }
                    if (this.InvestmentNeed != null)
                    {
                        this.InvestmentNeed.Notes.Remove(selectedNote);
                        Program.Repository.Update<InvestmentNeed, int>(this.InvestmentNeed);

                        selectedNote = null;
                        Initialise_SelectPanel(this.InvestmentNeed.Notes);
                    }*/
                }


            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                xToolBarMenu1.SetCAREdit(true);
                xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;
            }
        }

        private void UpdateNote()
        {
            searchModel.ShowCompletedTask = false;

            if (this.Retirement != null)
            {
                Program.Repository.Update<Retirement, int>(this.Retirement);
                Initialise_SelectPanel(this.Retirement.AdviceRecords);
            }
            if (this.Investment != null)
            {
                Program.Repository.Update<Investment, int>(this.Investment);
                Initialise_SelectPanel(this.Investment.AdviceRecords);
            }
            if (this.Education != null)
            {
                Program.Repository.Update<Education, int>(this.Education);
                Initialise_SelectPanel(this.Education.AdviceRecords);
            }
            if (this.Medical != null)
            {
                Program.Repository.Update<Medical, int>(this.Medical);
                Initialise_MedicalSelectPanel(this.Medical.AdviceRecords);
            }
            if (this.Life != null)
            {
                Program.Repository.Update<Life, int>(this.Life);
                Initialise_RiskSelectPanel(this.Life.AdviceRecords);
            }
            if (this.IncomeAsset != null)
            {
                Program.Repository.Update<IncomeAsset, int>(this.IncomeAsset);
                Initialise_SelectPanel(this.IncomeAsset.AdviceRecords);
            }/*
            if (this.Need != null)
            {
                Program.Repository.Update<Need, int>(this.Need);
                Initialise_SelectPanel(this.Need.Notes);
            }
            if (this.EducationNeed != null)
            {
                Program.Repository.Update<EducationNeed, int>(this.EducationNeed);
                Initialise_SelectPanel(this.EducationNeed.Notes);
            }
            if (this.InvestmentNeed != null)
            {
                Program.Repository.Update<InvestmentNeed, int>(this.InvestmentNeed);
                Initialise_SelectPanel(this.InvestmentNeed.Notes);
            }

            //Update the Admin Task
            if (Instruction != null)
            {
                if (selectedNote.IsCompleted)
                    Instruction.Status = InstructionStatus.Completed.ToText();

                UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());
            }
            else
            {
                if (selectedNote.InstructionId > 0)
                {
                    Instruction = GetInstructionEvent(selectedNote.InstructionId, new EventArgs());
                    if (Instruction != null)
                    {
                        if (selectedNote.IsCompleted)
                            Instruction.Status = InstructionStatus.Completed.ToText();

                        UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());
                    }
                }
            }*/

        }
        #endregion

        #region Event handlers
        private void AdviceRecord_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #region Form elements event changed handlers

        private void NeedAndObv_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                selectedNote.NeedsAndObjectives = metroTextBox_NeedAndObjective.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void FinancialSituation_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                selectedNote.FinancialSituation = metroTextBox_FinancialSituation.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void OtherInformation_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                selectedNote.OtherInformation = metroTextBox_OtherInformation.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void RecommendedFunds_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                selectedNote.RecommendedFunds = metroTextBox_RecommendedFunds.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Motivation_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                selectedNote.Motivation = metroTextBox_Motivation.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }
        #endregion

        #region Medical Aid form event handlers

        private void MedicalConditions_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                ((MedicalAidAdviceRecord)selectedNote).MedicalConditions = metroTextBox_MedicalConditions.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void MedicalCover_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                ((MedicalAidAdviceRecord)selectedNote).MedicalCover = metroTextBox_CurrentMedicalCover.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Hospitalisation_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                ((MedicalAidAdviceRecord)selectedNote).Hospitalisation = metroTextBox_Hospitalisation.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void ChronicConditions_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                ((MedicalAidAdviceRecord)selectedNote).ChronicConditions = metroTextBox_ChronicConditions.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void WaitingPeriod_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                ((MedicalAidAdviceRecord)selectedNote).WaitingPeriods = metroTextBox_WaitingPeriods.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void LateJoyner_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                ((MedicalAidAdviceRecord)selectedNote).LateJoynerPenalty = metroTextBox_LateJoiner.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void CoPayments_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                ((MedicalAidAdviceRecord)selectedNote).CoPayments = metroTextBox_Copayment.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void OtherImportantInformation_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                ((MedicalAidAdviceRecord)selectedNote).OtherImportantInformation = metroTextBox_OtherImportantInfo.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Notes_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                ((MedicalAidAdviceRecord)selectedNote).Notes = metroTextBox_Notes.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void ImplementedProduct_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                selectedNote.ImplementedProduct = metroTextBox_ProductImplemented.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void ImplementedMotivation_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                selectedNote.ImplementedMotivation = metroTextBox_ImplementationMotivation.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #region Medical Aid Needs and Goals Table Combobox change handlers

        private void Cover1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox comboBox = (ComboBox)sender;
            //((MedicalAidAdviceRecord)selectedNote).hcCoverDiscussed = comboBox.SelectedItem.ToString();
            ((MedicalAidAdviceRecord)selectedNote).hospitalCoverInfo.CoverDiscussed = Cover1.SelectedItem.ToString();
            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }
        private void Cover2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).hospitalCoverInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }
        private void DayToDay1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).dayToDayBenefitInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }
        private void DayToDay2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).dayToDayBenefitInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }
        private void Threshold1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).thresholdBenefitInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }
        private void Threshold2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).thresholdBenefitInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }
        private void ChronicBenefit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).chronicBenefitInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }
        private void ChronicBenefit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).chronicBenefitInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }
        private void SavingsAccount1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).savingsAccountInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void SavingsAccount2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).savingsAccountInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void HospitalPreference1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).hospitalCoverInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void HospitalPreference2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).hospitalPreferenceInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void GapCover1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).gapCoverInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void GapCover2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).gapCoverInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void Other1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).otherInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void Other2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).otherInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }


        #endregion

        #region Medical Aid Needs and Goals Table Comment box event handlers

        private void CoverComment_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                //MetroTextBox tb = (MetroTextBox)sender;
                //((MedicalAidAdviceRecord)selectedNote).hcComments = tb.Text;
                ((MedicalAidAdviceRecord)selectedNote).hospitalCoverInfo.Comments = this.tb_hospitalCover.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void DayToDayComment_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).dayToDayBenefitInfo.Comments = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void ThresholdComment_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).thresholdBenefitInfo.Comments = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void ChronicComment_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).chronicBenefitInfo.Comments = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void SavingsComment_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).savingsAccountInfo.Comments = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void HospitalPreferenceComment_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).hospitalPreferenceInfo.Comments = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void GapCoverComment_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).gapCoverInfo.Comments = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void OtherComment_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).otherInfo.Comments = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #region Medical Scheme Comparison Table event handlers

        #region Current Medical Scheme event handlers
        private void PolicyNo_Current_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).PolicyNumberCurrent = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Insurer_Current_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).InsurerCurrent = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void ProductName_Current_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).ProductNameCurrent = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Premium_Current_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).PremiumCurrent = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Benefits_Current_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).BenefitsCurrent = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void SavingsAccount_Current_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).SavingsAccountCurrent = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void ChronicBenefit_Current_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).ChronicBenefitCurrent = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void HospitalCover_Current_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).HospitalCoverCurrent = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void LimitsOnCover_Current_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).LimitsOnCoverCurrent = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Other_Current_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).OtherCurrent = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #region Replaced Medical Scheme Event Handlers

        private void PolicyNo_Replaced_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).PolicyNumberReplaced = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Insurer_Replaced_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).InsurerReplaced = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void ProductName_Replaced_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).ProductNameReplaced = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Premium_Replaced_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).PremiumReplaced = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Benefits_Replaced_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).BenefitsReplaced = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void SavingsAccount_Replaced_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).SavingsAccountReplaced = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void ChronicBenefit_Replaced_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).ChronicBenefitReplaced = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void HospitalCover_Replaced_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).HospitalCoverReplaced = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void LimitsOnCover_Replaced_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).LimitsOnCoverReplaced = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Other_Replaced_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).OtherReplaced = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #endregion

        #region Risk Needs and Goals Table event handlers

        #region Life row event handlers

        private void Life_NeedsQuantified_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).LifeNeedsQuantified = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Life_NeedsPriority_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).LifeNeedsPriority = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Life_NeedAddressed_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroComboBox cmb = (MetroComboBox)sender;
                ((RiskAdviceRecord)selectedNote).LifeNeedAddressed = cmb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Life_Shortfall_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).LifeShortfall = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Life_ReviewDate_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroDateTime dt = (MetroDateTime)sender;
                dt.CustomFormat = "dd MMM yyyy";

                ((RiskAdviceRecord)selectedNote).LifeReviewDate = dt.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #region Income Protection row event handlers

        private void PDIncomeProtection_NeedsQuantified_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).IncomeProtectionNeedsQuantified = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void PDIncomeProtection_NeedsPriority_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).IncomeProtectionNeedsPriority = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void PDIncomeProtection_NeedAddressed_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroComboBox cmb = (MetroComboBox)sender;
                ((RiskAdviceRecord)selectedNote).IncomeProtectionNeedAddressed = cmb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void PDIncomeProtection_Shortfall_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).IncomeProtectionShortfall = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }


        private void PDIncomeProtection_ReviewDate_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroDateTime dt = (MetroDateTime)sender;
                dt.CustomFormat = "dd MMM yyyy";

                ((RiskAdviceRecord)selectedNote).IncomeProtectionReviewDate = dt.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #region Lump sum row event handlers

        private void PDLumpSum_NeedsQuantified_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).LumpSumNeedsQuantified = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void PDLumpSum_NeedsPriority_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).LumpSumNeedsPriority = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void PDLumpSum_NeedAddressed_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroComboBox cmb = (MetroComboBox)sender;
                ((RiskAdviceRecord)selectedNote).LumpSumNeedAddressed = cmb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void PDLumpSum_Shortfall_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).LumpSumShortfall = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void PDLumpSum_ReviewDate_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroDateTime dt = (MetroDateTime)sender;
                dt.CustomFormat = "dd MMM yyyy";

                ((RiskAdviceRecord)selectedNote).LumpSumReviewDate = dt.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #region Temporary Disability row event handlers

        private void TemporaryDisability_NeedsQuantified_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).TemporaryDisabilityNeedsQuantified = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void TemporaryDisability_NeedsPriority_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).TemporaryDisabilityNeedsPriority = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void TemporaryDisability_NeedAddressed_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroComboBox cmb = (MetroComboBox)sender;
                ((RiskAdviceRecord)selectedNote).TemporaryDisabilityNeedAddressed = cmb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void TemporaryDisability_Shortfall_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).TemporaryDisabilityShortfall = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void TemporaryDisability_ReviewDate_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroDateTime dt = (MetroDateTime)sender;
                dt.CustomFormat = "dd MMM yyyy";

                ((RiskAdviceRecord)selectedNote).TemporaryDisabilityReviewDate = dt.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #region Trauma/illness row event handlers

        private void TraumaAndIllness_NeedsQuantified_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).TraumaAndIllnessNeedsQuantified = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void TraumaAndIllness_NeedsPriority_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).TraumaAndIllnessNeedsPriority = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void TraumaAndIllness_NeedAddressed_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroComboBox cmb = (MetroComboBox)sender;
                ((RiskAdviceRecord)selectedNote).TraumaAndIllnessNeedAddressed = cmb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void TraumaAndIllness_Shortfall_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).TraumaAndIllnessShortfall = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void TraumaAndIllness_ReviewDate_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroDateTime dt = (MetroDateTime)sender;
                dt.CustomFormat = "dd MMM yyyy";

                ((RiskAdviceRecord)selectedNote).TraumaAndIllnessReviewDate = dt.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #region Funeral Cover/Immediate expenses row event handlers

        private void FuneralCover_NeedsQuantified_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).FuneralCoverNeedsQuantified = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void FuneralCover_NeedsPriority_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).FuneralCoverNeedsPriority = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void FuneralCover_NeedAddressed_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroComboBox cmb = (MetroComboBox)sender;
                ((RiskAdviceRecord)selectedNote).FuneralCoverNeedAddressed = cmb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void FuneralCover_Shortfall_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).FuneralCoverShortfall = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void FuneralCover_ReviewDate_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroDateTime dt = (MetroDateTime)sender;
                dt.CustomFormat = "dd MMM yyyy";

                ((RiskAdviceRecord)selectedNote).FuneralCoverReviewDate = dt.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #region Other row event handlers

        private void Other_NeedsQuantified_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).OtherNeedsQuantified = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Other_NeedsPriority_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).OtherNeedsPriority = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Other_NeedAddressed_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroComboBox cmb = (MetroComboBox)sender;
                ((RiskAdviceRecord)selectedNote).OtherNeedAddressed = cmb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }


        private void Other_Shortfall_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((RiskAdviceRecord)selectedNote).OtherShortfall = tb.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        private void Other_ReviewDate_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroDateTime dt = (MetroDateTime)sender;
                dt.CustomFormat = "dd MMM yyyy";

                ((RiskAdviceRecord)selectedNote).OtherReviewDate = dt.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        #endregion

        #endregion

        #region Notes row select event handlers

        //Hanlder for row selection in standard CAR
        private void Notes_RowSelectEventHandlerChanged(object sender, SourceGrid.RowEventArgs e)
        {

            SourceGrid.Selection.RowSelection selection = sender as SourceGrid.Selection.RowSelection;
            if (selection == null)
                return;

            if (filteredNotes.Count == 0 || e.Row > filteredNotes.Count)
                selectedNote = null;
            else
                selectedNote = filteredNotes[e.Row - 1];


            Initialise_PolicyNotePanel(selectedNote);
        }

        //Handler for row selection in medical aid CAR
        private void MedicalNotes_RowSelectEventHandlerChanged(object sender, SourceGrid.RowEventArgs e)
        {

            SourceGrid.Selection.RowSelection selection = sender as SourceGrid.Selection.RowSelection;
            if (selection == null)
                return;

            if (medicalFilteredNotes.Count == 0 || e.Row > medicalFilteredNotes.Count)
                selectedNote = null;
            else
                selectedNote = medicalFilteredNotes[e.Row - 1];


            Initialise_PolicyNotePanel(selectedNote);
        }

        private void RiskNotes_RowSelectEventHandlerChanged(object sender, SourceGrid.RowEventArgs e)
        {

            SourceGrid.Selection.RowSelection selection = sender as SourceGrid.Selection.RowSelection;
            if (selection == null)
                return;

            if (riskFilteredNotes.Count == 0 || e.Row > riskFilteredNotes.Count)
                selectedNote = null;
            else
                selectedNote = riskFilteredNotes[e.Row - 1];


            Initialise_PolicyNotePanel(selectedNote);
        }

        #endregion

        //Event handler for when "Completed" check box is clicked
        private void XInput_ShowCompletedTask_KeyPressed(object sender, EventArgs e)
        {
            MetroCheckBox cb = (MetroCheckBox)sender;
            selectedNote.IsCompleted= cb.Checked;
            
            
        }
        #endregion

        #region Form Events

        //This method likely does not work so keep an eye out and delete it if not needed
        protected override void OnResizeEnd(EventArgs e)
        {
            Initialise_PolicyNotePanel(selectedNote);

            base.OnResizeEnd(e);
        }


        private void textBox_AppendNewLineDate( object sender, KeyEventArgs e )
        {
            MetroTextBox tb = (MetroTextBox)sender;
            if (e.Control && e.KeyCode == Keys.D)
            {
          
                if (string.IsNullOrEmpty(tb.Text))
                {
                    tb.AppendText(DateTime.Today.ToString("dd/MM/yyyy") + " - ");
                }
                else
                {
                    tb.AppendText(Environment.NewLine + DateTime.Today.ToString("dd/MM/yyyy") + " - ");
                }
              
                e.SuppressKeyPress = true; // Prevents the 'D' character from being entered into the text box
            }
        }




        #endregion

        #region Medical Aid Needs and Goals Table Elements

        #region Labels
        private void NeedsAndGoalsTable_AddLabels()
        {
            

            //Add labels to table

            //Horizontal labels
            this.tblPanel_NeedsAndGoals.Controls.Add(this.hl_Cover, 0, 0);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.hl_CoverDiscussed, 1, 0);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.hl_CoverTaken, 2, 0);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.hl_Comment, 3, 0);

            //Vertical labels
            this.tblPanel_NeedsAndGoals.Controls.Add(this.vl_HospitalCover, 0, 1);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.vl_DayToDay, 0, 2);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.vl_Threshold, 0, 3);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.vl_ChronicBenefit, 0, 4);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.vl_SavingsAccount, 0, 5);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.vl_Preference, 0, 6);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.vl_GapCover, 0, 7);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.vl_Other, 0, 8);
        }
        #endregion

        #region ComboBoxes
      

        private void NeedsAndGoalsTable_AddComboBoxes()
        {

            //Add Combo boxes to Table
            this.tblPanel_NeedsAndGoals.Controls.Add(this.Cover1, 1, 1);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.Cover2, 2, 1);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.DayToDay1, 1, 2);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.DayToDay2, 2, 2);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.Threshold1, 1, 3);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.Threshold2, 2, 3);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.ChronicBenefit1, 1, 4);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.ChronicBenefit2, 2, 4);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.Savings1, 1, 5);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.Savings2, 2, 5);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.HospitalPreference1, 1, 6);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.HospitalPreference2, 2, 6);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.GapCover1, 1, 7);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.GapCover2, 2, 7);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.Other1, 1, 8);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.Other2, 2, 8);

        }

        #endregion

        #region Text edit
        private void NeedsAndGoalsTable_AddTextBoxes()
        {
            //Add text boxes to form            

            this.tblPanel_NeedsAndGoals.Controls.Add(this.tb_hospitalCover, 3, 1);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.tb_dayToDay, 3, 2);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.tb_threshold, 3, 3);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.tb_chronic, 3, 4);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.tb_savingsAccount, 3, 5);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.tb_hospitalPreference, 3, 6);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.tb_gapCover, 3, 7);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.tb_other, 3, 8);

        }


        #endregion

        #endregion

        #region Medical Aid Comparison Table

        private void ComparisonTable_AddLabels()
        {
            //Add labels to the comparison tabl

            //Horizontal labels
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.hl_Detail, 0, 0);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.hl_CurrentMedScheme, 1, 0);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.hl_ReplacedMedScheme, 2, 0);

            //Vertical labels
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.vl_PolicyNo, 0, 1);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.vl_Insurer, 0, 2);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.vl_ProductName, 0, 3);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.vl_Premium, 0, 4);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.vl_Benefits, 0, 5);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.vl_compSavings, 0, 6);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.vl_compChronic, 0, 7);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.vl_compHospitalCover, 0, 8);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.vl_LimitsOnCover, 0, 9);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.vl_compOther, 0, 10);
        }

        private void ComparisonTable_AddTextBoxes()
        {
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbPolicyNo_Current, 1, 1);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbPolicyNo_Replaced, 2, 1);

            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbInsurer_Current, 1, 2);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbInsurer_Replaced, 2, 2);

            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbProductName_Current, 1, 3);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbProductName_Replaced, 2, 3);

            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbPremium_Current, 1, 4);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbPremium_Replaced, 2, 4);

            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbBenefits_Current, 1, 5);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbBenefits_Replaced, 2, 5);

            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbCompSavings_Current, 1, 6);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbCompSavings_Replaced, 2, 6);

            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbCompChronic_Current, 1, 7);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbCompChronic_Replaced, 2, 7);

            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbCompHospitalCover_Current, 1, 8);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbCompHospitalCover_Replaced, 2, 8);

            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbLimitsOnCover_Current, 1, 9);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbLimitsOnCover_Replaced, 2, 9);

            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbCompOther_Current, 1, 10);
            this.tblPanel_MedicalSchemeComparison.Controls.Add(this.tbCompOther_Replaced, 2, 10);
        }

        #endregion

        #region Risk Needs and Goals table setup

        private void RiskNeedsAndGoalsAddLabels()
        {
            //Horizontal Labels
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.hl_FinancialPlannningNeed, 0, 0);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.hl_NeedsQualified, 1, 0);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.hl_PriorityOfNeeds, 2, 0);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.hl_NeedFullyAddressed, 3, 0);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.hl_Shortfall, 4, 0);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.hl_ReviewDate, 5, 0);

            //Vertical Labels
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.vl_Life, 0, 1);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.vl_PDIncomeProtection, 0, 2);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.vl_PDLumpSum, 0, 3);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.vl_TemporaryDisability, 0 ,4);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.vl_Trauma, 0, 5);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.vl_FuneralCover, 0, 6);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.vl_RiskOther, 0, 7);
        }

        private void RiskNeedsAndGoalsAddControls()
        {
            //Add Controls

            //Life
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_LifeNeedsQuantified, 1, 1);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_LifeNeedsPriority, 2, 1);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.cmb_Life, 3, 1);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_LifeShortfall, 4, 1);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.dtp_LifeReviewDate, 5, 1);

            //Permanent Disability
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_PDIncomeProtectionNeedsQuantified, 1, 2);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_PDIncomeProtectionNeedsPriority, 2, 2);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.cmb_PDIncomeProtection, 3, 2);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_PDIncomeProtectionShortfall, 4, 2);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.dtp_PDIncomeProtectionReviewDate, 5, 2);


            //Permanent Disability Lump Sum
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_PDLumpSumNeedsQuantified, 1, 3);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_PDLumpSumNeedsPriority, 2, 3);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.cmb_PDLumpSum, 3, 3);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_PDLumpSumShortfall, 4, 3);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.dtp_PDLumpSumReviewDate, 5, 3);


            //Temporary Disability
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_TemporaryDisabilityNeedsQuantified, 1, 4);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_TemporaryDisabilityNeedsPriority, 2, 4);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.cmb_TemporaryDisability, 3, 4);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_TemporaryDisabilityShortfall, 4, 4);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.dtp_TemporaryDisabilityReviewDate, 5, 4);


            //Trauma
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_TraumaNeedsQuantified, 1, 5);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_TraumaNeedsPriority, 2, 5);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.cmb_Trauma, 3, 5);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_TraumaShortfall, 4, 5);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.dtp_TraumaReviewDate, 5, 5);


            //Funeral Cover
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_FuneralCoverNeedsQuantified, 1, 6);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_FuneralCoverNeedsPriority, 2, 6);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.cmb_FuneralCover, 3, 6);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_FuneralCoverShortfall, 4, 6);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.dtp_FuneralCoverReviewDate, 5, 6);


            //Other
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_RiskOtherNeedsQuantified, 1, 7);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_RiskOtherNeedsPriority, 2, 7);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.cmb_RiskOther, 3, 7);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.tb_RiskOtherShortfall, 4, 7);
            this.tblPanel_RiskNeedsAndGoals.Controls.Add(this.dtp_RiskOtherReviewDate, 5, 7);

        }

        #endregion

        public class ClientSearchModel : BaseEntity<int>
        {
            public bool ShowCompletedTask { get; set; }
        }

       
        #region Check buttons

        #region Investment Horizen buttons
        private void IHchk1_Click(object sender, EventArgs e)
        {
            if (this.IHchk1.Image != null)
            {
                this.IHchk1.Image = null;
            }
            else
            {
                this.IHchk1.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.IHchk2.Image = null;
                this.IHchk3.Image = null;
                this.IHchk4.Image = null;
                selectedNote.InvestmentHorizen = "0-2";
                
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void IHchk2_Click(object sender, EventArgs e)
        {
            if (this.IHchk2.Image != null)
            {
                this.IHchk2.Image = null;
            }
            else
            {
                this.IHchk1.Image = null;
                this.IHchk2.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.IHchk3.Image = null;
                this.IHchk4.Image = null;
                selectedNote.InvestmentHorizen = "2-5";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void IHchk3_Click(object sender, EventArgs e)
        {
            if (this.IHchk3.Image != null)
            {
                this.IHchk3.Image = null;
            }
            else
            {
                this.IHchk1.Image = null;
                this.IHchk2.Image = null;
                this.IHchk3.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.IHchk4.Image = null;
                selectedNote.InvestmentHorizen = "5-9";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void IHchk4_Click(object sender, EventArgs e)
        {
            if (this.IHchk4.Image != null)
            {
                this.IHchk4.Image = null;
            }
            else
            {
                this.IHchk1.Image = null;
                this.IHchk2.Image = null;
                this.IHchk3.Image = null;
                this.IHchk4.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                selectedNote.InvestmentHorizen = "10+";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }
        #endregion

        #region Product Knowledge and Experience buttons
        private void PKEchk1_Click(object sender, EventArgs e)
        {
            if (this.PKEchk1.Image != null)
            {
                this.PKEchk1.Image = null;

                selectedNote.ProductKnowledge = "";
            }
            else
            {
                this.PKEchk1.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.PKEchk2.Image = null;
                this.PKEchk3.Image = null;
                this.PKEchk4.Image = null;
                this.PKEchk5.Image = null;
                this.PKEchk6.Image = null;
                this.PKEchk7.Image = null;
                this.PKEchk8.Image = null;
                this.PKEchk9.Image = null;
                this.PKEchk10.Image = null;

                selectedNote.ProductKnowledge = "1";

            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void PKEchk2_Click(object sender, EventArgs e)
        {
            if (this.PKEchk2.Image != null)
            {
                this.PKEchk2.Image = null;

                selectedNote.ProductKnowledge = "";
            }
            else
            {
                this.PKEchk1.Image = null;
                this.PKEchk2.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.PKEchk3.Image = null;
                this.PKEchk4.Image = null;
                this.PKEchk5.Image = null;
                this.PKEchk6.Image = null;
                this.PKEchk7.Image = null;
                this.PKEchk8.Image = null;
                this.PKEchk9.Image = null;
                this.PKEchk10.Image = null;

                selectedNote.ProductKnowledge = "2";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void PKEchk3_Click(object sender, EventArgs e)
        {
            if (this.PKEchk3.Image != null)
            {
                this.PKEchk3.Image = null;

                selectedNote.ProductKnowledge = "";
            }
            else
            {
                this.PKEchk1.Image = null;
                this.PKEchk2.Image = null;
                this.PKEchk3.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.PKEchk4.Image = null;
                this.PKEchk5.Image = null;
                this.PKEchk6.Image = null;
                this.PKEchk7.Image = null;
                this.PKEchk8.Image = null;
                this.PKEchk9.Image = null;
                this.PKEchk10.Image = null;

                selectedNote.ProductKnowledge = "3";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void PKEchk4_Click(object sender, EventArgs e)
        {
            if (this.PKEchk4.Image != null)
            {
                this.PKEchk4.Image = null;

                selectedNote.ProductKnowledge = "";
            }
            else
            {
                this.PKEchk1.Image = null;
                this.PKEchk2.Image = null;
                this.PKEchk3.Image = null;
                this.PKEchk4.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.PKEchk5.Image = null;
                this.PKEchk6.Image = null;
                this.PKEchk7.Image = null;
                this.PKEchk8.Image = null;
                this.PKEchk9.Image = null;
                this.PKEchk10.Image = null;

                selectedNote.ProductKnowledge = "4";
            }
                
            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void PKEchk5_Click(object sender, EventArgs e)
        {
            if (this.PKEchk5.Image != null)
            {
                this.PKEchk5.Image = null;

                selectedNote.ProductKnowledge = "";
            }
            else
            {
                this.PKEchk1.Image = null;
                this.PKEchk2.Image = null;
                this.PKEchk3.Image = null;
                this.PKEchk4.Image = null;
                this.PKEchk5.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.PKEchk6.Image = null;
                this.PKEchk7.Image = null;
                this.PKEchk8.Image = null;
                this.PKEchk9.Image = null;
                this.PKEchk10.Image = null;

                selectedNote.ProductKnowledge = "5";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void PKEchk6_Click(object sender, EventArgs e)
        {
            if (this.PKEchk6.Image != null)
            {
                this.PKEchk6.Image = null;

                selectedNote.ProductKnowledge = "";
            }
            else
            {
                this.PKEchk1.Image = null;
                this.PKEchk2.Image = null;
                this.PKEchk3.Image = null;
                this.PKEchk4.Image = null;
                this.PKEchk5.Image = null;
                this.PKEchk6.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.PKEchk7.Image = null;
                this.PKEchk8.Image = null;
                this.PKEchk9.Image = null;
                this.PKEchk10.Image = null;

                selectedNote.ProductKnowledge = "6";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void PKEchk7_Click(object sender, EventArgs e)
        {
            if (this.PKEchk7.Image != null)
            {
                this.PKEchk7.Image = null;

                selectedNote.ProductKnowledge = "";
            }
            else
            {
                this.PKEchk1.Image = null;
                this.PKEchk2.Image = null;
                this.PKEchk3.Image = null;
                this.PKEchk4.Image = null;
                this.PKEchk5.Image = null;
                this.PKEchk6.Image = null;
                this.PKEchk7.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.PKEchk8.Image = null;
                this.PKEchk9.Image = null;
                this.PKEchk10.Image = null;

                selectedNote.ProductKnowledge = "7";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void PKEchk8_Click(object sender, EventArgs e)
        {
            if (this.PKEchk8.Image != null)
            {
                this.PKEchk8.Image = null;

                selectedNote.ProductKnowledge = "";
            }
            else
            {
                this.PKEchk1.Image = null;
                this.PKEchk2.Image = null;
                this.PKEchk3.Image = null;
                this.PKEchk4.Image = null;
                this.PKEchk5.Image = null;
                this.PKEchk6.Image = null;
                this.PKEchk7.Image = null;
                this.PKEchk8.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.PKEchk9.Image = null;
                this.PKEchk10.Image = null;

                selectedNote.ProductKnowledge = "8";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void PKEchk9_Click(object sender, EventArgs e)
        {
            if (this.PKEchk9.Image != null)
            {
                this.PKEchk9.Image = null;

                selectedNote.ProductKnowledge = "";
            }
            else
            {
                this.PKEchk1.Image = null;
                this.PKEchk2.Image = null;
                this.PKEchk3.Image = null;
                this.PKEchk4.Image = null;
                this.PKEchk5.Image = null;
                this.PKEchk6.Image = null;
                this.PKEchk7.Image = null;
                this.PKEchk8.Image = null;
                this.PKEchk9.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.PKEchk10.Image = null;

                selectedNote.ProductKnowledge = "9";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void PKEchk10_Click(object sender, EventArgs e)
        {
            if (this.PKEchk10.Image != null)
            {
                this.PKEchk10.Image = null;

                selectedNote.ProductKnowledge = "";
            }
            else
            {
                this.PKEchk1.Image = null;
                this.PKEchk2.Image = null;
                this.PKEchk3.Image = null;
                this.PKEchk4.Image = null;
                this.PKEchk5.Image = null;
                this.PKEchk6.Image = null;
                this.PKEchk7.Image = null;
                this.PKEchk8.Image = null;
                this.PKEchk9.Image = null;
                this.PKEchk10.Image = global::easiplan.app.Properties.Resources.checkmark_small;

                selectedNote.ProductKnowledge = "10";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }
        #endregion

        #region Access to Capital buttons

        private void AtCchk1_Click(object sender, EventArgs e)
        {
            if (this.AtCchk1.Image != null)
            {
                this.AtCchk1.Image = null;
            }
            else
            {
                this.AtCchk1.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.AtCchk2.Image = null;
                this.AtCchk3.Image = null;
                selectedNote.AccessToCapital = "Need to draw an Income";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void AtCchk2_Click(object sender, EventArgs e)
        {
            if (this.AtCchk2.Image != null)
            {
                this.AtCchk2.Image = null;
            }
            else
            {
                this.AtCchk1.Image = null;
                this.AtCchk2.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                this.AtCchk3.Image = null;
                selectedNote.AccessToCapital = "Always require access to capital";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void AtCchk3_Click(object sender, EventArgs e)
        {
            if (this.AtCchk3.Image != null)
            {
                this.AtCchk3.Image = null;
            }
            else
            {
                this.AtCchk1.Image = null;
                this.AtCchk2.Image = null;
                this.AtCchk3.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                selectedNote.AccessToCapital = "Do not require access to capital for 5 years";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        #endregion

        #endregion

        #region Populate methods

        private void SetIsComplete(ClientAdviceRecord record)
        {
            
                this.metroCheckBox_completed.Checked = record.IsCompleted;
                lockForm(record.IsCompleted);

        }

        #region Product Knowledge and Experience methods
        private void populateProductAndExperience(ClientAdviceRecord record)
        {
            if (string.IsNullOrEmpty(record.ProductKnowledge))
            { 
            return; 
            }

            if (record.ProductKnowledge.Contains("1"))
            {
                if (record.ProductKnowledge.Contains("0"))
                {
                    PKEchk10.PerformClick();
                    this.PKEchk10.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                }
                else
                {
                    PKEchk1.PerformClick();
                    this.PKEchk1.Image = global::easiplan.app.Properties.Resources.checkmark_small;
                }
            }
            else if (record.ProductKnowledge.Contains("2"))
            {
                PKEchk2.PerformClick();
                this.PKEchk2.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.ProductKnowledge.Contains("3"))
            {
                PKEchk3.PerformClick();
                this.PKEchk3.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.ProductKnowledge.Contains("4"))
            {
                
                PKEchk4.PerformClick();
                this.PKEchk4.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.ProductKnowledge.Contains("5"))
            {
                PKEchk5.PerformClick();
                this.PKEchk5.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.ProductKnowledge.Contains("6"))
            {
                PKEchk6.PerformClick();
                this.PKEchk6.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.ProductKnowledge.Contains("7"))
            {
                PKEchk7.PerformClick();
                this.PKEchk7.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.ProductKnowledge.Contains("8"))
            {
                PKEchk8.PerformClick();
                this.PKEchk8.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.ProductKnowledge.Contains("9"))
            {
                PKEchk9.PerformClick();
                this.PKEchk9.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
        }
        #endregion

        #region Populate Button controls

        private void populateInvestmentHorizon(ClientAdviceRecord record)
        {
            Console.WriteLine(record.InvestmentHorizen);
            if (string.IsNullOrEmpty(record.InvestmentHorizen))
            {
                return;
            }

            if (record.InvestmentHorizen.Contains("0-2"))
            {
                IHchk1.PerformClick();
                this.IHchk1.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.InvestmentHorizen.Contains("2-5"))
            {
                IHchk2.PerformClick();
                this.IHchk2.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.InvestmentHorizen.Contains("5-9"))
            {
                IHchk3.PerformClick();
                this.IHchk3.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.InvestmentHorizen.Contains("10"))
            {
                IHchk4.PerformClick();
                this.IHchk4.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
        }

        private void populateAccessToCapital(ClientAdviceRecord record)
        {

            if (string.IsNullOrEmpty(record.AccessToCapital))
            {
                return;
            }

            if (record.AccessToCapital.ToLower().Contains("income"))
            {
                AtCchk1.PerformClick();
                this.AtCchk1.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.AccessToCapital.ToLower().Contains("always"))
            {
                AtCchk2.PerformClick();
                this.AtCchk2.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
            else if (record.AccessToCapital.ToLower().Contains("5"))
            {
                AtCchk3.PerformClick();
                this.AtCchk3.Image = global::easiplan.app.Properties.Resources.checkmark_small;
            }
        }

        #endregion

        #region Populate generic form text boxes

        private void populateNeedsAndObjectives(ClientAdviceRecord record)
        {
            if (record.NeedsAndObjectives != null)
            {
                this.metroTextBox_NeedAndObjective.Text = record.NeedsAndObjectives;
            }
        }

        private void populateFinancialSolution(ClientAdviceRecord record)
        {
            if (record.FinancialSituation != null)
            {
                this.metroTextBox_FinancialSituation.Text = record.FinancialSituation;
            }
        }

        private void populateOtherInformation(ClientAdviceRecord record)
        {
            if (record.OtherInformation != null)
            {
                this.metroTextBox_OtherInformation.Text = record.OtherInformation;
            }
        }

        private void populateRecommendedFunds(ClientAdviceRecord record)
        {
            if (record.RecommendedFunds != null)
            {
                this.metroTextBox_RecommendedFunds.Text = record.RecommendedFunds;
            }
        }

        private void populateImplementedMotivation(ClientAdviceRecord record)
        {
            if (record.ImplementedMotivation != null)
            {
                this.metroTextBox_ImplementationMotivation.Text = record.ImplementedMotivation;
            }
        }

        private void populateImplementedRecommendedFunds(ClientAdviceRecord record)
        {
            if (record.ImplementedProduct != null)
            {
                this.metroTextBox_ProductImplemented.Text = record.ImplementedProduct;
            }
        }

        private void populateMotivation(ClientAdviceRecord record)
        {
            if (record.Motivation != null)
            {
                this.metroTextBox_Motivation.Text = record.Motivation;
            }
        }

        #endregion

        #region Populate medical form text boxes

        private void populateMedicalConditions(MedicalAidAdviceRecord record)
        {
            if (record.MedicalConditions != null)
            {
                this.metroTextBox_MedicalConditions.Text = record.MedicalConditions;
                
            }
        }

        private void populateMedicalCover(MedicalAidAdviceRecord record)
        {
            if (record.MedicalCover != null)
            {
                this.metroTextBox_CurrentMedicalCover.Text = record.MedicalCover;
            }
        }

        private void populateHospitalisation(MedicalAidAdviceRecord record)
        {
            if (record.Hospitalisation != null)
            {
                this.metroTextBox_Hospitalisation.Text = record.Hospitalisation;
            }
        }

        private void populateChronicConditions(MedicalAidAdviceRecord record)
        {
            if (record.ChronicConditions != null)
            {
                this.metroTextBox_ChronicConditions.Text = record.ChronicConditions;
            }
        }

        private void populateWaitingPeriods(MedicalAidAdviceRecord record)
        {
            if (record.WaitingPeriods != null)
            {
                this.metroTextBox_WaitingPeriods.Text = record.WaitingPeriods;
            }
        }

        private void populateLateJoyner(MedicalAidAdviceRecord record)
        {
            if (record.LateJoynerPenalty != null)
            {
                this.metroTextBox_LateJoiner.Text = record.LateJoynerPenalty;
            }
        }

        private void populateCoPayments(MedicalAidAdviceRecord record)
        {
            if (record.CoPayments != null)
            {
                this.metroTextBox_Copayment.Text = record.CoPayments;
            }
        }

        private void populateOtherImportantInformation(MedicalAidAdviceRecord record)
        {
            if (record.OtherImportantInformation != null)
            {
                this.metroTextBox_OtherImportantInfo.Text = record.OtherImportantInformation;
            }
        }

        private void populateNotes(MedicalAidAdviceRecord record)
        {
            if (record.Notes != null)
            {
                this.metroTextBox_Notes.Text = record.Notes;
            }
        }

        #endregion

        #region Populate Table methods

        //Populate values for Needs and Goals table on Medical CAR
        private void PopulateMedicalTable(MedicalAidAdviceRecord record)
        {
            //Load Form values from given record

            //Hospital Cover
            this.Cover1.Text = record.hospitalCoverInfo.CoverDiscussed;
            this.Cover2.Text = record.hospitalCoverInfo.CoverTaken;
            this.tb_hospitalCover.Text = record.hospitalCoverInfo.Comments;
            
            //Day to Day 
            this.DayToDay1.Text = record.dayToDayBenefitInfo.CoverDiscussed;
            this.DayToDay2.Text = record.dayToDayBenefitInfo.CoverTaken;
            this.tb_dayToDay.Text = record.dayToDayBenefitInfo.Comments;

            //Threshold Benefits
            this.Threshold1.Text = record.thresholdBenefitInfo.CoverDiscussed;
            this.Threshold2.Text = record.thresholdBenefitInfo.CoverTaken;
            this.tb_threshold.Text = record.thresholdBenefitInfo.Comments;

            //Chronic Benefits
            this.ChronicBenefit1.Text = record.chronicBenefitInfo.CoverDiscussed;
            this.ChronicBenefit2.Text = record.chronicBenefitInfo.CoverTaken;
            this.tb_chronic.Text = record.chronicBenefitInfo.Comments;

            //Savings Account
            this.Savings1.Text = record.savingsAccountInfo.CoverDiscussed;
            this.Savings2.Text = record.savingsAccountInfo.CoverTaken;
            this.tb_savingsAccount.Text = record.savingsAccountInfo.Comments;

            //Hospital Preference
            this.HospitalPreference1.Text = record.hospitalPreferenceInfo.CoverDiscussed;
            this.HospitalPreference2.Text = record.hospitalPreferenceInfo.CoverTaken;
            this.tb_hospitalPreference.Text = record.hospitalPreferenceInfo.Comments;

            //Gap Cover
            this.GapCover1.Text = record.gapCoverInfo.CoverDiscussed;
            this.GapCover2.Text = record.gapCoverInfo.CoverTaken;
            this.tb_gapCover.Text = record.gapCoverInfo.Comments;

            //Other
            this.Other1.Text = record.otherInfo.CoverDiscussed;
            this.Other2.Text = record.otherInfo.CoverTaken;
            this.tb_other.Text = record.otherInfo.Comments;
        }
     
        //Populate values for Medical Scheme comparison table in medical CAR
        private void PopulateMedicalSchemeTable(MedicalAidAdviceRecord record)
        {
            //Load Form values from given record

            //Policy/Application Number
            this.tbPolicyNo_Current.Text = record.PolicyNumberCurrent;
            this.tbPolicyNo_Replaced.Text = record.PolicyNumberReplaced;

            //Insurer 
            this.tbInsurer_Current.Text = record.InsurerCurrent;
            this.tbInsurer_Replaced.Text = record.InsurerReplaced;

            //Product Name
            this.tbProductName_Current.Text = record.ProductNameCurrent;
            this.tbProductName_Replaced.Text = record.ProductNameReplaced;

            //Premium
            this.tbPremium_Current.Text = record.PremiumCurrent;
            this.tbPremium_Replaced.Text = record.PremiumReplaced;

            //Benefits
            this.tbBenefits_Current.Text = record.BenefitsCurrent;
            this.tbBenefits_Replaced.Text = record.BenefitsReplaced;
            
            //Savings Account
            this.tbCompSavings_Current.Text = record.SavingsAccountCurrent;
            this.tbCompSavings_Replaced.Text = record.SavingsAccountReplaced;

            //Chronic Benefits
            this.tbCompChronic_Current.Text = record.ChronicBenefitCurrent;
            this.tbCompChronic_Replaced.Text = record.ChronicBenefitReplaced;

            //Hospital Cover
            this.tbCompHospitalCover_Current.Text = record.HospitalCoverCurrent;
            this.tbCompHospitalCover_Replaced.Text = record.HospitalCoverReplaced;

            //Limits On Cover
            this.tbLimitsOnCover_Current.Text = record.LimitsOnCoverCurrent;
            this.tbLimitsOnCover_Replaced.Text = record.LimitsOnCoverReplaced;

            //Other
            this.tbCompOther_Current.Text = record.OtherCurrent;
            this.tbCompOther_Replaced.Text = record.OtherReplaced;
        }


        //Populate values for Needs and Goals table in Risk CAR
        private void PopulateRiskNeedsAndGoals(RiskAdviceRecord record)
        {
            DateTime rvDate;

            //Life
            this.tb_LifeNeedsQuantified.Text = "R " +record.LifeNeedsQuantified;
            this.tb_LifeNeedsPriority.Text = record.LifeNeedsPriority;
            this.cmb_Life.Text = record.LifeNeedAddressed;
            this.tb_LifeShortfall.Text = record.LifeShortfall;

            if (DateTime.TryParseExact(record.LifeReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
            {
                this.dtp_LifeReviewDate.Value = rvDate;
                this.dtp_LifeReviewDate.CustomFormat = "dd MMM yyyy";
            }
            else
            {
                this.dtp_LifeReviewDate.CustomFormat = " ";
            }

            //Income Protection
            this.tb_PDIncomeProtectionNeedsQuantified.Text = "R " + record.IncomeProtectionNeedsQuantified;
            this.tb_PDIncomeProtectionNeedsPriority.Text = record.IncomeProtectionNeedsPriority;
            this.cmb_PDIncomeProtection.Text = record.IncomeProtectionNeedAddressed;
            this.tb_PDIncomeProtectionShortfall.Text = record.IncomeProtectionShortfall;

            if (DateTime.TryParseExact(record.IncomeProtectionReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
            {
                this.dtp_PDIncomeProtectionReviewDate.Value = rvDate;
                this.dtp_PDIncomeProtectionReviewDate.CustomFormat = "dd MMM yyyy";
            }
            else
            {
                this.dtp_PDIncomeProtectionReviewDate.CustomFormat = " ";
            }

            //Lump Sum
            this.tb_PDLumpSumNeedsQuantified.Text = "R " + record.LumpSumNeedsQuantified;
            this.tb_PDLumpSumNeedsPriority.Text = record.LumpSumNeedsPriority;
            this.cmb_PDLumpSum.Text = record.LumpSumNeedAddressed;
            this.tb_PDLumpSumShortfall.Text = record.LumpSumShortfall;

            if (DateTime.TryParseExact(record.LumpSumReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
            {
                this.dtp_PDLumpSumReviewDate.Value = rvDate;
                this.dtp_PDLumpSumReviewDate.CustomFormat = "dd MMM yyyy";
            }
            else
            {
                this.dtp_TraumaReviewDate.CustomFormat = " ";
            }

            //Temporary Disability
            this.tb_TemporaryDisabilityNeedsQuantified.Text = "R " + record.TemporaryDisabilityNeedsQuantified;
            this.tb_TemporaryDisabilityNeedsPriority.Text = record.TemporaryDisabilityNeedsPriority;
            this.cmb_TemporaryDisability.Text = record.TemporaryDisabilityNeedAddressed;
            this.tb_TemporaryDisabilityShortfall.Text = record.TemporaryDisabilityShortfall;

            if (DateTime.TryParseExact(record.TemporaryDisabilityReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
            {
                this.dtp_TemporaryDisabilityReviewDate.Value = rvDate;
                this.dtp_TemporaryDisabilityReviewDate.CustomFormat = "dd MMM yyyy";
            }
            else
            {
                this.dtp_TraumaReviewDate.CustomFormat = " ";
            }

            //Trauma/Illness
            this.tb_TraumaNeedsQuantified.Text = "R " + record.TraumaAndIllnessNeedsQuantified;
            this.tb_TraumaNeedsPriority.Text = record.TraumaAndIllnessNeedsPriority;
            this.cmb_Trauma.Text = record.TraumaAndIllnessNeedAddressed;
            this.tb_TraumaShortfall.Text = record.TraumaAndIllnessShortfall;

            if (DateTime.TryParseExact(record.TraumaAndIllnessReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
            {
                this.dtp_TraumaReviewDate.Value = rvDate;
                this.dtp_TraumaReviewDate.CustomFormat = "dd MMM yyyy";
            }
            else
            {
                this.dtp_TraumaReviewDate.CustomFormat = " ";
            }

            //Funeral Cover/Immediate Expenses
            this.tb_FuneralCoverNeedsQuantified.Text = "R " + record.FuneralCoverNeedsQuantified;
            this.tb_FuneralCoverNeedsPriority.Text = record.FuneralCoverNeedsPriority;
            this.cmb_FuneralCover.Text = record.FuneralCoverNeedAddressed;
            this.tb_FuneralCoverShortfall.Text = record.FuneralCoverShortfall;

            if (DateTime.TryParseExact(record.FuneralCoverReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
            {
                this.dtp_FuneralCoverReviewDate.Value = rvDate;
                this.dtp_FuneralCoverReviewDate.CustomFormat = "dd MMM yyyy";
            }
            else
            {
                this.dtp_FuneralCoverReviewDate.CustomFormat = " ";
            }

            //Other
            this.tb_RiskOtherNeedsQuantified.Text = "R " + record.OtherNeedsQuantified;
            this.tb_RiskOtherNeedsPriority.Text = record.OtherNeedsPriority;
            this.cmb_RiskOther.Text = record.OtherNeedAddressed;
            this.tb_RiskOtherShortfall.Text = record.OtherShortfall;

            if (DateTime.TryParseExact(record.OtherReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
            {
                this.dtp_RiskOtherReviewDate.Value = rvDate;
                this.dtp_RiskOtherReviewDate.CustomFormat = "dd MMM yyyy";
            }
            else
            {
                this.dtp_RiskOtherReviewDate.CustomFormat = " ";
            }
        }


        #endregion

        #region Save table value methods

        //Possibly redundant code, delete if you find no errors
        /*
        private void MedicalTable_Save(MedicalAidAdviceRecord record)
        {
            //Load Form values from given record

            //Hospital Cover
            record.hcCoverDiscussed = this.Cover1.Text;
            record.hcCoverTaken = this.Cover2.Text;
            record.hcComments = this.tb_hospitalCover.Text;

            //Day to Day 
            record.ddCoverDiscussed = this.DayToDay1.Text;
            record.ddCoverTaken = this.DayToDay2.Text;  
            record.ddComments = this.tb_dayToDay.Text;

            //Threshold Benefits
            record.tbCoverDiscussed = this.Threshold1.Text;
            record.tbCoverTaken = this.Threshold2.Text;
            record.tbComments = this.tb_threshold.Text;

            //Chronic Benefits
            record.cbCoverDiscussed = this.ChronicBenefit1.Text;
            record.cbCoverTaken = this.ChronicBenefit2.Text;
            record.cbComments = this.tb_chronic.Text;

            //Savings Account
            record.saCoverDiscussed = this.Savings1.Text;
            record.saCoverTaken = this.Savings2.Text;
            record.saComments = this.tb_savingsAccount.Text;

            //Hospital Preference
            record.hpCoverDiscussed = this.HospitalPreference1.Text;
            record.hpCoverTaken = this.HospitalPreference2.Text;
            record.hpComments = this.tb_hospitalPreference.Text;

            //Gap Cover
            record.gcCoverDiscussed = this.GapCover1.Text;
            record.gcCoverTaken = this.GapCover2.Text;
            record.gcComments = this.tb_gapCover.Text;

            //Other
            record.oCoverDiscussed = this.Other1.Text;
            record.oCoverTaken = this.Other2.Text;
            record.oComments = this.tb_other.Text;
        }

        private void MedicalSchemeComparisonSave(MedicalAidAdviceRecord record)
        {
            //Policy/Application Number
            record.PolicyNumberCurrent = this.tbPolicyNo_Current.Text;
            record.PolicyNumberReplaced = this.tbPolicyNo_Replaced.Text;

            //Insurer 
            record.InsurerCurrent = this.tbInsurer_Current.Text;
            record.InsurerReplaced = this.tbInsurer_Replaced.Text;

            //Product Name
            record.ProductNameCurrent = this.tbProductName_Current.Text;
            record.ProductNameReplaced = this.tbProductName_Replaced.Text;

            //Premium
            record.PremiumCurrent = this.tbPremium_Current.Text;
            record.PremiumReplaced = this.tbPremium_Replaced.Text;

            //Benefits
            record.BenefitsCurrent = this.tbBenefits_Current.Text;
            record.BenefitsReplaced = this.tbBenefits_Replaced.Text;
            
            //Savings Account
            record.SavingsAccountCurrent = this.tbCompSavings_Current.Text;
            record.SavingsAccountReplaced = this.tbCompSavings_Replaced.Text;

            //Chronic Benefits
            record.ChronicBenefitCurrent = this.tbCompChronic_Current.Text;
            record.ChronicBenefitReplaced = this.tbCompChronic_Replaced.Text;

            //Hospital Cover
            record.HospitalCoverCurrent = this.tbCompHospitalCover_Current.Text;
            record.HospitalCoverReplaced = this.tbCompHospitalCover_Replaced.Text;

            //Limits On Cover
            record.LimitsOnCoverCurrent = this.tbLimitsOnCover_Current.Text;
            record.LimitsOnCoverReplaced = this.tbLimitsOnCover_Replaced.Text;

            //Other
            record.OtherCurrent = this.tbCompOther_Current.Text;
            record.OtherReplaced = this.tbCompOther_Replaced.Text;
        }*/

        #endregion

        //Call all methods to populate the standard CAR template
        private void populateRetirement(ClientAdviceRecord advRecord)
        {
            SetIsComplete(selectedNote);
            clearCheckBoxControls();
            populateProductAndExperience(advRecord);
            populateInvestmentHorizon(advRecord);
            populateAccessToCapital(advRecord);
            populateNeedsAndObjectives(advRecord);
            populateFinancialSolution(advRecord);
            populateOtherInformation(advRecord);
            
            populateRecommendedFunds(advRecord);
            populateMotivation(advRecord);
            populateImplementedRecommendedFunds(advRecord);
            populateImplementedMotivation(advRecord);
        }

        //Call all methods to populate the medical Aid CAR
        private void populateMedical(MedicalAidAdviceRecord advRecord)
        {
            SetIsComplete(selectedNote);
            clearProductKnlgeAndExperience();
            populateProductAndExperience(advRecord);

            populateMedicalConditions(advRecord);
            populateMedicalCover(advRecord);
            populateOtherInformation(advRecord);
            populateHospitalisation(advRecord);
            populateChronicConditions(advRecord);
            populateWaitingPeriods(advRecord);
            populateLateJoyner(advRecord);
            populateCoPayments(advRecord);
            populateOtherImportantInformation(advRecord);
            populateNotes(advRecord);
            populateRecommendedFunds(advRecord);
            populateMotivation(advRecord);

            PopulateMedicalTable(advRecord);
            PopulateMedicalSchemeTable(advRecord);

            populateImplementedRecommendedFunds(advRecord);
            populateImplementedMotivation(advRecord);
        }

        private void populateRisk(RiskAdviceRecord advRecord)
        {
            SetIsComplete(selectedNote);
            clearProductKnlgeAndExperience();

            populateProductAndExperience(advRecord);
            
            populateNeedsAndObjectives(advRecord);
            populateFinancialSolution(advRecord);
            populateOtherInformation(advRecord);

            PopulateRiskNeedsAndGoals(advRecord);
            
            populateRecommendedFunds(advRecord);
            populateMotivation(advRecord);
            populateImplementedRecommendedFunds(advRecord);
            populateImplementedMotivation(advRecord);
        }

        #endregion

        #region Clear methods

        private void ClearMetroPanel(MetroPanel metroPanel)
        {
            List<Control> controlsToRemove = new List<Control>();

            foreach (Control control in metroPanel.Controls)
            {
                if (!(control is MetroScrollBar))
                {
                    controlsToRemove.Add(control);
                }
            }

            foreach (Control control in controlsToRemove)
            {
                metroPanel.Controls.Remove(control);
            }
        }

        #region Clear Check boxes
        private void clearProductKnlgeAndExperience()
        {
            this.PKEchk1.Image = null;
            this.PKEchk2.Image = null;
            this.PKEchk3.Image = null;
            this.PKEchk4.Image = null;
            this.PKEchk5.Image = null;
            this.PKEchk6.Image = null;
            this.PKEchk7.Image = null;
            this.PKEchk8.Image = null;
            this.PKEchk9.Image = null;
            this.PKEchk10.Image = null;
        }


        private void clearInvestmentHorizon()
        {
            this.IHchk1.Image = null;
            this.IHchk2.Image = null;
            this.IHchk3.Image = null;
            this.IHchk4.Image = null;
        }
        private void clearAccessToCapital()
        {
            this.AtCchk1.Image = null;
            this.AtCchk2.Image = null;
            this.AtCchk3.Image = null;
        }
        #endregion

        #endregion

        #region methods to clear entire forms

        private void clearCheckBoxControls()
        {
            clearProductKnlgeAndExperience();
            clearInvestmentHorizon();
            clearAccessToCapital();
        }

        private void clearMedicalForm()
        {
            clearProductKnlgeAndExperience();
        }
        #endregion

       


    }

}
