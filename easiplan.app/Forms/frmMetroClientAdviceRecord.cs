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
using System.Text.RegularExpressions;

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
        RiskCoverNeed RiskCoverNeed = null;

        //Note lists for standard CAR
        IList<ClientAdviceRecord> Notes = new List<ClientAdviceRecord>();
        IList<ClientAdviceRecord> filteredNotes = new List<ClientAdviceRecord>();

        //Note lists for Medical Aid CAR
        IList<MedicalAidAdviceRecord> MedicalNotes = new List<MedicalAidAdviceRecord>();
        IList<MedicalAidAdviceRecord> medicalFilteredNotes = new List<MedicalAidAdviceRecord>();

        //Note lists for Risk CAR
        IList<RiskAdviceRecord> RiskNotes = new List<RiskAdviceRecord>();
        IList<RiskAdviceRecord> riskFilteredNotes = new List<RiskAdviceRecord>();

        //Note lists for Archived Notes
        IList<Note> ArchiveNotes = new List<Note>();
        IList<Note> filteredArchiveNotes = new List<Note>();

        Note selectedArchiveNote = null;
        ClientAdviceRecord selectedNote = null;
        ClientAdviceRecord mostRecentRecord = null;
        PolicyAction Action = PolicyAction.AmendPolicy;

        int initialSplitterDistance;

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

            //this.Text = string.Empty;
            //this.SubTitle = string.Format("{0}", "Policy Notes");

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ResizeRedraw = true;

            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);

            this.DisplayHeader = false;
            #endregion

            #region xToolBarMenu1

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", Title);
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


            //Set currency Columns
            tbPremium_Current.KeyPress += CurrencyValidation_KeyPress;
            tbPremium_Replaced.KeyPress += CurrencyValidation_KeyPress;

            tb_LifeNeedsQuantified.KeyPress += CurrencyValidation_KeyPress;
            tb_PDIncomeProtectionNeedsQuantified.KeyPress += CurrencyValidation_KeyPress;
            tb_PDLumpSumNeedsQuantified.KeyPress += CurrencyValidation_KeyPress;
            tb_TemporaryDisabilityNeedsQuantified.KeyPress += CurrencyValidation_KeyPress;
            tb_TraumaNeedsQuantified.KeyPress += CurrencyValidation_KeyPress;
            tb_FuneralCoverNeedsQuantified.KeyPress += CurrencyValidation_KeyPress;
            tb_RiskOtherNeedsQuantified.KeyPress += CurrencyValidation_KeyPress;

            tb_LifeShortfall.KeyPress += CurrencyValidation_KeyPress;
            tb_PDIncomeProtectionShortfall.KeyPress += CurrencyValidation_KeyPress;
            tb_PDLumpSumShortfall.KeyPress += CurrencyValidation_KeyPress;
            tb_TemporaryDisabilityShortfall.KeyPress += CurrencyValidation_KeyPress;
            tb_TraumaShortfall.KeyPress += CurrencyValidation_KeyPress;
            tb_FuneralCoverShortfall.KeyPress += CurrencyValidation_KeyPress;
            tb_RiskOtherShortfall.KeyPress += CurrencyValidation_KeyPress;

            #endregion

            #region Event hanlders for Medical Aid needs and goals combo boxes

            //Set ComboBox event handers
            this.cmb_HospitalDiscussed.SelectedIndexChanged += HospitalDiscussed_SelectedIndexChanged;
            this.cmb_HospitalTaken.SelectedIndexChanged += HospitalTaken_SelectedIndexChanged;

            this.cmb_DayToDayDiscussed.SelectedIndexChanged += DayToDayDiscussed_SelectedIndexChanged;
            this.cmb_DayToDayTaken.SelectedIndexChanged += DayToDayTaken_SelectedIndexChanged;

            this.cmb_ThresholdBenefitDiscussed.SelectedIndexChanged += ThresholdBenefitDiscussed_SelectedIndexChanged;
            this.cmb_ThresholdBenefitTaken.SelectedIndexChanged += ThresholdBenefitTaken_SelectedIndexChanged;

            this.cmb_ChronicBenefitDiscussed.SelectedIndexChanged += ChronicBenefitDiscussed_SelectedIndexChanged;
            this.cmb_ChronicBenefitTaken.SelectedIndexChanged += ChronicBenefitTaken_SelectedIndexChanged;

            this.cmb_SavingsDiscussed.SelectedIndexChanged += SavingsAccountDiscussed_SelectedIndexChanged;
            this.cmb_SavingsTaken.SelectedIndexChanged += SavingsAccountTaken_SelectedIndexChanged;

            this.cmb_HospitalPreferenceDiscussed.SelectedIndexChanged += HospitalPreferenceDiscussed_SelectedIndexChanged;
            this.cmb_HospitalPreferenceTaken.SelectedIndexChanged += HospitalPreferenceTaken_SelectedIndexChanged;

            this.cmb_GapCoverDiscussed.SelectedIndexChanged += GapCoverDiscussed_SelectedIndexChanged;
            this.cmb_GapCoverTaken.SelectedIndexChanged += GapCoverTaken_SelectedIndexChanged;

            this.cmb_OtherDiscussed.SelectedIndexChanged += OtherDiscussed_SelectedIndexChanged;
            this.cmb_OtherTaken.SelectedIndexChanged += OtherTaken_SelectedIndexChanged;

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

            initialSplitterDistance = 500;
            //this.splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            //this.splitContainer2.SplitterMoved += splitContainer1_SplitterMoved;

            this.metroCheckBox_completed.CheckedChanged += XInput_ShowCompletedTask_KeyPressed;
        }

        
        //Retirement constructor
        public frmMetroClientAdviceRecord(Retirement model, bool readOnly, PolicyAction action) : this($"{model.Description} [{model.ReferenceNo}] - {model.Type}", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Retirement = model;
            InvestmentType = "Retirement";

            Initialise_SelectPanel(model.AdviceRecords);
            Initialise_ArchiveNotes(model.Notes);
           

        }

        //Non retirement constructor
        public frmMetroClientAdviceRecord(Investment model, bool readOnly, PolicyAction action) : this($"{model.Description} [{model.ReferenceNo}] - {model.Type}", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");


            Investment = model;
            InvestmentType = "Investment";

            Initialise_SelectPanel(model.AdviceRecords);
            Initialise_ArchiveNotes(model.Notes);


        }

        //Education constructor
        public frmMetroClientAdviceRecord(Education model, bool readOnly, PolicyAction action) : this($"{model.Description} [{model.ReferenceNo}] - {model.Type}", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Education = model;
            InvestmentType = "Education";
            Initialise_SelectPanel(model.AdviceRecords);
            Initialise_ArchiveNotes(model.Notes);

        }

        //Medical Aid constructor
        public frmMetroClientAdviceRecord(Medical model, bool readOnly, PolicyAction action) : this($"{model.Description} [{model.ReferenceNo}] - {model.Type}", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Medical = model;
            InvestmentType = "Medical";

            InitialiseMedicalTable();
            InitialiseMedicalAidComparisonTable();
            Initialise_MedicalSelectPanel(model.AdviceRecords);
            Initialise_ArchiveNotes(model.Notes);

        }
        
        //Risk constructor
        public frmMetroClientAdviceRecord(Life model, bool readOnly, PolicyAction action) : this($"{model.Description} [{model.ReferenceNo}] - {model.Type}", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Life = model;
            InvestmentType = "risk";
            InitialiseRiskNeedsAndGoalsTable();
            Initialise_RiskSelectPanel(model.AdviceRecords);
            Initialise_ArchiveNotes(model.Notes);

        }

        //Income Asset Constructor
        public frmMetroClientAdviceRecord(IncomeAsset model, bool readOnly, PolicyAction action) : this($"{model.Description} [{model.ReferenceNo}] - {model.Type}", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            IncomeAsset = model;
            InvestmentType = "IncomeAsset";
            Initialise_SelectPanel(model.AdviceRecords);
            Initialise_ArchiveNotes(model.Notes);

        }
        
        //Retirement FNA What I need constructor
        public frmMetroClientAdviceRecord(Need model, bool readOnly, PolicyAction action) : this($"{model.Description} - {model.Type}", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Need = model;
            InvestmentType = "Need";

            Initialise_SelectPanel(model.AdviceRecords);
            Initialise_ArchiveNotes(model.Notes);

        }

        //Non Retirement FNA Education Need
        public frmMetroClientAdviceRecord(EducationNeed model, bool readOnly, PolicyAction action) : this($"{model.Description} - {model.Type}", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            EducationNeed = model;
            InvestmentType = "EducationNeed";

            Initialise_SelectPanel(model.AdviceRecords);
            Initialise_ArchiveNotes(model.Notes);

        }

        //Non Retirement FNA Investment Need
        public frmMetroClientAdviceRecord(InvestmentNeed model, bool readOnly, PolicyAction action) : this($"{model.Description} - {model.Type}", readOnly, action)
        {
            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            InvestmentNeed = model;
            InvestmentType = "InvestmentNeed";

            Initialise_SelectPanel(model.AdviceRecords);
            Initialise_ArchiveNotes(model.Notes);

        }

        //Non Retirement FNA Risk Need
        public frmMetroClientAdviceRecord(RiskCoverNeed model, bool readOnly, PolicyAction action) : this($"{model.Description} - {model.Type}", readOnly, action)
        {
            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            RiskCoverNeed = model;
            InvestmentType = "RiskCoverNeed";

            InitialiseRiskNeedsAndGoalsTable();
            Initialise_RiskSelectPanel(model.RiskAdviceRecords);

        }

        /*
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



        #region Initialisation select panels
        //These select panels are coded very mesily.. should refactor to make fewer lines of code repeat

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
                column.For(c => c.IsCompleted, "Completed");
                column.For(c => c.AdviceDate, "Note Date", new DateEditor(), MinWidth: 100);
                column.For(x => x.UpdateDate, "Last Date", new DateEditor());
                column.For(x => x.UpdateBy, "Updated By", new StringEditor());
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
                column.For(c => c.IsCompleted, "Completed");
                column.For(c => c.AdviceDate, "Note Date", new DateEditor(), MinWidth: 100);
                column.For(x => x.UpdateDate, "Last Date", new DateEditor());
                column.For(x => x.UpdateBy, "Updated By", new StringEditor());
                

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
                column.For(c => c.IsCompleted, "Completed");
                column.For(c => c.AdviceDate, "Note Date", new DateEditor(), MinWidth: 100);
                column.For(x => x.UpdateDate, "Last Date", new DateEditor());
                column.For(x => x.UpdateBy, "Updated By", new StringEditor());

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


        void Initialise_ArchiveNotes(IList<Note> notes = null)
        {
            if (notes != null)
                ArchiveNotes = notes;

            if (ArchiveNotes == null)
                filteredArchiveNotes = new List<Note>();
            else
                filteredArchiveNotes = ArchiveNotes.Where(x => x.CreateDate != null).ToList();

            #region Notes Grid
            this.dataGrid_ArchiveNotes.Initialise1(filteredArchiveNotes, column =>
            {

                column.For(c => c.NoteDate, "Note Date", new DateEditor(), MinWidth: 100);
                column.For(x => x.Text, "Note", new StringEditor());
                column.For(c => c.IsCompleted, "Completed");
                column.For(x => x.UpdateDate, "Last Date", new DateEditor());
                column.For(x => x.UpdateBy, "Updt By", new StringEditor());

            },
            RowSelectEventHandler: ArchiveNotes_RowSelectEventHandlerChanged,
            ReadOnly: true,
            AllowDelete: false)
            .Format1(true, fixedCols: 1);
            #endregion

            if (ArchiveNotes.Count > 0)
                selectedArchiveNote = filteredArchiveNotes.LastOrDefault();
            else
                selectedArchiveNote = new Note();

            Initialise_ArchiveNotePanel();
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
                    this.Text = "Client Advice Record [CAR] - Retirement Portfolio";
                    
                    mostRecentRecord = Retirement.AdviceRecords.LastOrDefault();
                    record.InitialFee = Retirement.InitialFee;
                    record.OngoingFee = Retirement.OngoingFee;

                    populateRetirement(record);
                    InitializeStandardPortfolio();
                    

                    break;
                case "investment":
                    this.Text = "Client Advice Record [CAR] - Non-retirement Portfolio";

                    mostRecentRecord = Investment.AdviceRecords.LastOrDefault();
                    record.InitialFee = Investment.InitialFee;
                    record.OngoingFee = Investment.OngoingFee;

                    populateRetirement(record);
                    InitializeStandardPortfolio();

                    break;
                case "medical":
                    this.Text = "Client Advice Record [CAR] - Medical Portfolio";

                    mostRecentRecord = Medical.AdviceRecords.LastOrDefault();
                    record.InitialFee = Medical.InitialFee;
                    record.OngoingFee = Medical.OngoingFee;

                    populateMedical((MedicalAidAdviceRecord)record);
                    initialiseMedicalPortfolio();

                    break;
                case "education":
                    this.Text = "Client Advice Record [CAR] - Education Portfolio";

                    mostRecentRecord = Education.AdviceRecords.LastOrDefault();
                    record.InitialFee = Education.InitialFee;
                    record.OngoingFee = Education.OngoingFee;

                    populateRetirement(record);
                    InitializeStandardPortfolio();
                    
                    
                    break;
                case "risk":
                    this.Text = "Client Advice Record [CAR] - Risk Portfolio";

                    mostRecentRecord = Life.AdviceRecords.LastOrDefault();
                    record.InitialFee = Life.InitialFee;
                    record.OngoingFee = Life.OngoingFee;

                    populateRisk((RiskAdviceRecord)record);
                    initialiseRiskPortfolio();
                    
                    break;
                case "incomeasset":
                    this.Text = "Client Advice Record [CAR] - Income Assets Portfolio";

                    mostRecentRecord = IncomeAsset.AdviceRecords.LastOrDefault();
                    record.InitialFee = IncomeAsset.InitialFee;
                    record.OngoingFee = IncomeAsset.OngoingFee;

                    populateRetirement(record);
                    InitializeStandardPortfolio();
                    
                    break;
                case "need":
                    this.Text = "Client Advice Record [CAR] - Need";
                    mostRecentRecord = Need.AdviceRecords.LastOrDefault();

                    populateRetirement(record);
                    InitializeStandardPortfolio();

                    break;
                case "investmentneed":
                    this.Text = "Client Advice Record [CAR] - Inventment Need";
                    mostRecentRecord = InvestmentNeed.AdviceRecords.LastOrDefault();

                    populateRetirement(record);
                    InitializeStandardPortfolio();

                    break;
                case "educationneed":
                    this.Text = "Client Advice Record [CAR] - Education Need";
                    mostRecentRecord = EducationNeed.AdviceRecords.LastOrDefault();

                    populateRetirement(record);
                    InitializeStandardPortfolio();

                    break;
                case "riskcoverneed":
                    this.Text = "Client Advice Record [CAR] - Risk Cover Need";
                    mostRecentRecord = RiskCoverNeed.RiskAdviceRecords.LastOrDefault();

                    populateRisk((RiskAdviceRecord)record);
                    initialiseRiskPortfolio();

                    break;
            }

            
            selectedNote.IsLoading = false;
            metroTextBox_NoteDate.Text = selectedNote.AdviceDate.ToString("dd-MMM-yyyy hh:mm");
            metroTextBox_InitialFee.Text = selectedNote.InitialFee.ToString();
            metroTextBox_InitialFee.Text = selectedNote.OngoingFee.ToString();


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
                    this.xToolBarMenu1.SetCAREdit(true);
                }
            }
            else
            {
                //Open form if not
                lockForm(false);
                this.xToolBarMenu1.SetCAREdit(true);
            }
        }

        //Initialise panel for Archived Notes
        void Initialise_ArchiveNotePanel()
        {
            this.metroPanel_ArchiveSelect.Controls.Clear();


            if (selectedArchiveNote == null)
                return;

            selectedArchiveNote.IsLoading = true;

            this.metroPanel_ArchiveSelect.Initialise(selectedArchiveNote, cntr =>
            {
                cntr.For(x => x.NoteDate, "Note Date ...", new MetroTextBoxEditor(160).ReadOnly(true));
               // cntr.For(x => x.IsCompleted, "Completed", new MetroCheckBoxEditor().ReadOnly(ReadOnly));
            }, left: 10, top: 5, labelWidth: 120, controlsLayout: ControlsLayout.Horizontal, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

            this.metroTextBox_ArchiveNote.Text = selectedArchiveNote.Text;

            selectedArchiveNote.IsLoading = false;
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

        //Form lock methods to prevent editing of completed records
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
                    lockTextPanel(tp, state);
                }
            }

            //Lock Tables
            this.tblPanel_NeedsAndGoals.Enabled = !state;
            this.tblPanel_MedicalSchemeComparison.Enabled = !state;
            this.tblPanel_RiskNeedsAndGoals.Enabled = !state;

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
       
        #region Event handlers

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
                    
                    if (this.Need != null)
                    {
                        this.Need.AdviceRecords.Add(selectedNote);

                        instructionType = InstructionType.RETIRE_POLICY_NOTE;
                        referenceId = this.Need.Id;
                        referenceNumber = this.Need.ReferenceNo;
                        comment = this.Need.Description;
                    }

                    if (this.EducationNeed != null)
                    {
                        this.EducationNeed.AdviceRecords.Add(selectedNote);

                        instructionType = InstructionType.EDU_POLICY_NOTE;
                        referenceId = this.EducationNeed.Id;
                        referenceNumber = this.EducationNeed.ReferenceNo;
                        comment = this.EducationNeed.Description;
                    }

                    if (this.InvestmentNeed != null)
                    {
                        this.InvestmentNeed.AdviceRecords.Add(selectedNote);

                        instructionType = InstructionType.INVEST_POLICY_NOTE;
                        referenceId = this.InvestmentNeed.Id;
                        referenceNumber = this.InvestmentNeed.ReferenceNo;
                        comment = this.InvestmentNeed.Description;
                    }

                    if (this.RiskCoverNeed != null)
                    {
                        this.RiskCoverNeed.RiskAdviceRecords.Add((RiskAdviceRecord)selectedNote);

                        //instructionType = InstructionType.;
                        referenceId = this.RiskCoverNeed.Id;
                        referenceNumber = this.RiskCoverNeed.ReferenceNo;
                        comment = this.RiskCoverNeed.Description;
                    }

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
                if (mostRecentRecord.IsCompleted)
                {
                    this.xToolBarMenu1.CarModeAddRecord(true);
                }
                else
                {
                    this.xToolBarMenu1.SetCAREdit(true);
                }
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

        #region Delete click Methods

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
                    }
                    if (this.Need != null)
                    {
                        this.Need.AdviceRecords.Remove(selectedNote);
                        Program.Repository.Update<Need, int>(this.Need);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Need.AdviceRecords);
                    }
                    if (this.EducationNeed != null)
                    {
                        this.EducationNeed.AdviceRecords.Remove(selectedNote);
                        Program.Repository.Update<EducationNeed, int>(this.EducationNeed);

                        selectedNote = null;
                        Initialise_SelectPanel(this.EducationNeed.AdviceRecords);
                    }
                    if (this.InvestmentNeed != null)
                    {
                        this.InvestmentNeed.AdviceRecords.Remove(selectedNote);
                        Program.Repository.Update<InvestmentNeed, int>(this.InvestmentNeed);

                        selectedNote = null;
                        Initialise_SelectPanel(this.InvestmentNeed.AdviceRecords);
                    }
                    if (this.RiskCoverNeed != null)
                    {
                        this.RiskCoverNeed.RiskAdviceRecords.Remove((RiskAdviceRecord)selectedNote);
                        Program.Repository.Update<RiskCoverNeed, int>(this.RiskCoverNeed);

                        selectedNote = null;
                        Initialise_RiskSelectPanel(this.RiskCoverNeed.RiskAdviceRecords);
                    }
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
            }
            if (this.Need != null)
            {
                Program.Repository.Update<Need, int>(this.Need);
                Initialise_SelectPanel(this.Need.AdviceRecords);
            }
            if (this.EducationNeed != null)
            {
                Program.Repository.Update<EducationNeed, int>(this.EducationNeed);
                Initialise_SelectPanel(this.EducationNeed.AdviceRecords);
            }
            if (this.InvestmentNeed != null)
            {
                Program.Repository.Update<InvestmentNeed, int>(this.InvestmentNeed);
                Initialise_SelectPanel(this.InvestmentNeed.AdviceRecords);
            }
            if (this.RiskCoverNeed != null)
            {
                Program.Repository.Update<RiskCoverNeed, int>(this.RiskCoverNeed);
                Initialise_RiskSelectPanel(this.RiskCoverNeed.RiskAdviceRecords);
            }
            /*
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

        #endregion


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

        #region Hospital Cover

        private void HospitalDiscussed_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).HospitalCoverInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void HospitalTaken_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).HospitalCoverInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        #endregion

        #region Day To Day Benefit

        private void DayToDayDiscussed_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).DayToDayBenefitInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void DayToDayTaken_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).DayToDayBenefitInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        #endregion

        #region Threshold Benefit

        private void ThresholdBenefitDiscussed_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).ThresholdBenefitInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void ThresholdBenefitTaken_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).ThresholdBenefitInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        #endregion

        #region Chronic Benefit

        private void ChronicBenefitDiscussed_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).ChronicBenefitInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void ChronicBenefitTaken_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).ChronicBenefitInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        #endregion

        #region Savings Account

        private void SavingsAccountDiscussed_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).SavingsAccountInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void SavingsAccountTaken_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).SavingsAccountInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        #endregion

        #region Hospital Preference

        private void HospitalPreferenceDiscussed_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).HospitalPreferenceInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void HospitalPreferenceTaken_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).HospitalPreferenceInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        #endregion

        #region Gap Cover

        private void GapCoverDiscussed_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).GapCoverInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void GapCoverTaken_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).GapCoverInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        #endregion

        #region Other

        private void OtherDiscussed_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).OtherInfo.CoverDiscussed = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        private void OtherTaken_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ((MedicalAidAdviceRecord)selectedNote).OtherInfo.CoverTaken = comboBox.SelectedItem.ToString();

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        #endregion

        #endregion

        #region Medical Aid Needs and Goals Table Comment box event handlers

        private void CoverComment_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);
                MetroTextBox tb = (MetroTextBox)sender;
                ((MedicalAidAdviceRecord)selectedNote).HospitalCoverInfo.Comments = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).DayToDayBenefitInfo.Comments = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).ThresholdBenefitInfo.Comments = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).ChronicBenefitInfo.Comments = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).SavingsAccountInfo.Comments = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).HospitalPreferenceInfo.Comments = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).GapCoverInfo.Comments = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).OtherInfo.Comments = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).PolicyNumberComparison.CurrentMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).InsurerComparison.CurrentMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).ProductNameComparison.CurrentMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).PremiumComparison.CurrentMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).BenefitsComparison.CurrentMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).SavingsAccountComparison.CurrentMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).ChronicBenefitComparison.CurrentMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).HospitalCoverComparison.CurrentMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).LimitsOnCoverComparison.CurrentMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).OtherComparison.CurrentMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).PolicyNumberComparison.ReplacedMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).InsurerComparison.ReplacedMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).ProductNameComparison.ReplacedMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).PremiumComparison.ReplacedMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).BenefitsComparison.ReplacedMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).SavingsAccountComparison.ReplacedMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).ChronicBenefitComparison.ReplacedMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).HospitalCoverComparison.ReplacedMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).LimitsOnCoverComparison.ReplacedMedicalScheme = tb.Text;
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
                ((MedicalAidAdviceRecord)selectedNote).OtherComparison.ReplacedMedicalScheme = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).LifeInfo.NeedsQuantified = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).LifeInfo.NeedsPriority = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).LifeInfo.NeedAddressed = cmb.Text;
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
                ((RiskAdviceRecord)selectedNote).LifeInfo.Shortfall = tb.Text;
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

                ((RiskAdviceRecord)selectedNote).LifeInfo.ReviewDate = dt.Text;
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
                ((RiskAdviceRecord)selectedNote).IncomeProtectionInfo.NeedsQuantified = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).IncomeProtectionInfo.NeedsPriority = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).IncomeProtectionInfo.NeedAddressed = cmb.Text;
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
                ((RiskAdviceRecord)selectedNote).IncomeProtectionInfo.Shortfall = tb.Text;
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

                ((RiskAdviceRecord)selectedNote).IncomeProtectionInfo.ReviewDate = dt.Text;
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
                ((RiskAdviceRecord)selectedNote).LumpSumInfo.NeedsQuantified = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).LumpSumInfo.NeedsPriority = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).LumpSumInfo.NeedAddressed = cmb.Text;
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
                ((RiskAdviceRecord)selectedNote).LumpSumInfo.Shortfall = tb.Text;
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

                ((RiskAdviceRecord)selectedNote).LumpSumInfo.ReviewDate = dt.Text;
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
                ((RiskAdviceRecord)selectedNote).TemporaryDisabilityInfo.NeedsQuantified = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).TemporaryDisabilityInfo.NeedsPriority = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).TemporaryDisabilityInfo.NeedAddressed = cmb.Text;
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
                ((RiskAdviceRecord)selectedNote).TemporaryDisabilityInfo.Shortfall = tb.Text;
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

                ((RiskAdviceRecord)selectedNote).TemporaryDisabilityInfo.ReviewDate = dt.Text;
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
                ((RiskAdviceRecord)selectedNote).TraumaAndIllnessInfo.NeedsQuantified = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).TraumaAndIllnessInfo.NeedsPriority = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).TraumaAndIllnessInfo.NeedAddressed = cmb.Text;
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
                ((RiskAdviceRecord)selectedNote).TraumaAndIllnessInfo.Shortfall = tb.Text;
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

                ((RiskAdviceRecord)selectedNote).TraumaAndIllnessInfo.ReviewDate = dt.Text;
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
                ((RiskAdviceRecord)selectedNote).FuneralCoverInfo.NeedsQuantified = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).FuneralCoverInfo.NeedsPriority = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).FuneralCoverInfo.NeedAddressed = cmb.Text;
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
                ((RiskAdviceRecord)selectedNote).FuneralCoverInfo.Shortfall = tb.Text;
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

                ((RiskAdviceRecord)selectedNote).FuneralCoverInfo.ReviewDate = dt.Text;
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
                ((RiskAdviceRecord)selectedNote).OtherInfo.NeedsQuantified = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).OtherInfo.NeedsPriority = tb.Text;
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
                ((RiskAdviceRecord)selectedNote).OtherInfo.NeedAddressed = cmb.Text;
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
                ((RiskAdviceRecord)selectedNote).OtherInfo.Shortfall = tb.Text;
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

                ((RiskAdviceRecord)selectedNote).OtherInfo.ReviewDate = dt.Text;
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
        //Possibly refactor to prevent repeating code
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

        private void ArchiveNotes_RowSelectEventHandlerChanged(object sender, SourceGrid.RowEventArgs e)
        {

            SourceGrid.Selection.RowSelection selection = sender as SourceGrid.Selection.RowSelection;
            if (selection == null)
                return;

            if (filteredArchiveNotes.Count == 0 || e.Row > filteredArchiveNotes.Count)
                selectedArchiveNote = null;
            else
                selectedArchiveNote = filteredArchiveNotes[e.Row - 1];


            Initialise_ArchiveNotePanel();
        }

        #endregion

        #region Completed Check Box Event Handler

        //Event handler for when "Completed" check box is clicked
        private void XInput_ShowCompletedTask_KeyPressed(object sender, EventArgs e)
        {
            MetroCheckBox cb = (MetroCheckBox)sender;
            selectedNote.IsCompleted= cb.Checked;
            
            
        }

        #endregion

        #endregion

        #region Form Events

        private void textBox_AppendNewLineDate( object sender, KeyEventArgs e )
        {
            MetroTextBox tb = (MetroTextBox)sender;
            if (e.Control && e.KeyCode == Keys.D)
            {
          
                if (string.IsNullOrEmpty(tb.Text))
                {
                    tb.AppendText(DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - ");
                }
                else
                {
                    tb.AppendText(Environment.NewLine + DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - ");
                }
              
                e.SuppressKeyPress = true; // Prevents the 'D' character from being entered into the text box
            }
        }

        private void CurrencyValidation_KeyPress(object sender, KeyPressEventArgs e)
        {
            // The regular expression pattern to match a currency amount
            string pattern = @"^\d*\.?\d{0,2}$";

            // Match the input against the pattern
            if (!Regex.IsMatch(e.KeyChar.ToString(), pattern) && e.KeyChar != (char)Keys.Back)
            {
                // Suppress the key press event if the input is invalid
                e.Handled = true;
            }
        }


        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            int barrier = initialSplitterDistance + 20; // Set the barrier position

            if (splitContainer1.SplitterDistance > barrier)
            {
                // The splitter has moved beyond the barrier
                // Set the SplitterDistance to the barrier position
                splitContainer1.SplitterDistance = barrier;
            }
        }


        #endregion

        #region Medical Aid Needs and Goals Table Elements

        //These messages populate the blank Medical Aid Needs and Goals table template with the necessary lables and controls

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
            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_HospitalDiscussed, 1, 1);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_HospitalTaken, 2, 1);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_DayToDayDiscussed, 1, 2);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_DayToDayTaken, 2, 2);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_ThresholdBenefitDiscussed, 1, 3);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_ThresholdBenefitTaken, 2, 3);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_ChronicBenefitDiscussed, 1, 4);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_ChronicBenefitTaken, 2, 4);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_SavingsDiscussed, 1, 5);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_SavingsTaken, 2, 5);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_HospitalPreferenceDiscussed, 1, 6);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_HospitalPreferenceTaken, 2, 6);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_GapCoverDiscussed, 1, 7);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_GapCoverTaken, 2, 7);

            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_OtherDiscussed, 1, 8);
            this.tblPanel_NeedsAndGoals.Controls.Add(this.cmb_OtherTaken, 2, 8);

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

        //These messages populate the blank Medical scheme comparison table template with the necessary lables and controls

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

        //These messages populate the blank Risk Portfolio Needs and Goals table template with the necessary lables and controls

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

        //These are the methods used to populate the CAR forms with the information that has been saved in the data base

        //Sets the state of the Completed check box
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

            if (record.HospitalCoverInfo != null)
            {
                //Hospital Cover
                this.cmb_HospitalDiscussed.Text = record.HospitalCoverInfo.CoverDiscussed;
                this.cmb_HospitalTaken.Text = record.HospitalCoverInfo.CoverTaken;
                this.tb_hospitalCover.Text = record.HospitalCoverInfo.Comments;
            }

            //Day to Day
            if (record.DayToDayBenefitInfo != null)
            {
                this.cmb_DayToDayDiscussed.Text = record.DayToDayBenefitInfo.CoverDiscussed;
                this.cmb_DayToDayTaken.Text = record.DayToDayBenefitInfo.CoverTaken;
                this.tb_dayToDay.Text = record.DayToDayBenefitInfo.Comments;
            }

            //Threshold Benefits
            if (record.ThresholdBenefitInfo != null)
            {
                this.cmb_ThresholdBenefitDiscussed.Text = record.ThresholdBenefitInfo.CoverDiscussed;
                this.cmb_ThresholdBenefitTaken.Text = record.ThresholdBenefitInfo.CoverTaken;
                this.tb_threshold.Text = record.ThresholdBenefitInfo.Comments;
            }

            //Chronic Benefits
            if (record.ChronicBenefitInfo != null)
            {
                this.cmb_ChronicBenefitDiscussed.Text = record.ChronicBenefitInfo.CoverDiscussed;
                this.cmb_ChronicBenefitTaken.Text = record.ChronicBenefitInfo.CoverTaken;
                this.tb_chronic.Text = record.ChronicBenefitInfo.Comments;
            }

            //Savings Account
            if (record.SavingsAccountInfo != null)
            {
                this.cmb_SavingsDiscussed.Text = record.SavingsAccountInfo.CoverDiscussed;
                this.cmb_SavingsTaken.Text = record.SavingsAccountInfo.CoverTaken;
                this.tb_savingsAccount.Text = record.SavingsAccountInfo.Comments;
            }

            //Hospital Preference
            if (record.HospitalPreferenceInfo != null)
            {
                this.cmb_HospitalPreferenceDiscussed.Text = record.HospitalPreferenceInfo.CoverDiscussed;
                this.cmb_HospitalPreferenceTaken.Text = record.HospitalPreferenceInfo.CoverTaken;
                this.tb_hospitalPreference.Text = record.HospitalPreferenceInfo.Comments;
            }

            //Gap Cover
            if (record.GapCoverInfo != null)
            {
                this.cmb_GapCoverDiscussed.Text = record.GapCoverInfo.CoverDiscussed;
                this.cmb_GapCoverTaken.Text = record.GapCoverInfo.CoverTaken;
                this.tb_gapCover.Text = record.GapCoverInfo.Comments;
            }
            //Other
            if (record.OtherInfo != null)
            {
                this.cmb_OtherDiscussed.Text = record.OtherInfo.CoverDiscussed;
                this.cmb_OtherTaken.Text = record.OtherInfo.CoverTaken;
                this.tb_other.Text = record.OtherInfo.Comments;
            }
        }

        //Populate values for Medical Scheme comparison table in medical CAR
        private void PopulateMedicalSchemeTable(MedicalAidAdviceRecord record)
        {
            //Load Form values from given record

            //Policy/Application Number
            if (record.PolicyNumberComparison != null)
            {
                this.tbPolicyNo_Current.Text = record.PolicyNumberComparison.CurrentMedicalScheme;
                this.tbPolicyNo_Replaced.Text = record.PolicyNumberComparison.ReplacedMedicalScheme;
            }

            //Insurer
            if (record.InsurerComparison != null)
            {
                this.tbInsurer_Current.Text = record.InsurerComparison.CurrentMedicalScheme;
                this.tbInsurer_Replaced.Text = record.InsurerComparison.ReplacedMedicalScheme;
            }

            //Product Name
            if (record.ProductNameComparison != null)
            {
                this.tbProductName_Current.Text = record.ProductNameComparison.CurrentMedicalScheme;
                this.tbProductName_Replaced.Text = record.ProductNameComparison.ReplacedMedicalScheme;
            }

            //Premium
            if (record.PremiumComparison != null)
            {
                this.tbPremium_Current.Text = record.ProductNameComparison.CurrentMedicalScheme;
                this.tbPremium_Replaced.Text = record.ProductNameComparison.ReplacedMedicalScheme;
            }

            //Benefits
            if (record.BenefitsComparison != null)
            {
                this.tbBenefits_Current.Text = record.BenefitsComparison.CurrentMedicalScheme;
                this.tbBenefits_Replaced.Text = record.BenefitsComparison.ReplacedMedicalScheme;
            }

            //Savings Account
            if (record.SavingsAccountComparison != null)
            {
                this.tbCompSavings_Current.Text = record.SavingsAccountComparison.CurrentMedicalScheme;
                this.tbCompSavings_Replaced.Text = record.SavingsAccountComparison.ReplacedMedicalScheme;
            }

            //Chronic Benefits
            if (record.ChronicBenefitComparison != null)
            {
                this.tbCompChronic_Current.Text = record.ChronicBenefitComparison.CurrentMedicalScheme;
                this.tbCompChronic_Replaced.Text = record.ChronicBenefitComparison.ReplacedMedicalScheme;
            }

            //Hospital Cover
            if (record.HospitalCoverComparison != null)
            {
                this.tbCompHospitalCover_Current.Text = record.HospitalCoverComparison.CurrentMedicalScheme;
                this.tbCompHospitalCover_Replaced.Text = record.HospitalCoverComparison.ReplacedMedicalScheme;
            }

            //Limits On Cover
            if (record.LimitsOnCoverComparison != null)
            {
                this.tbLimitsOnCover_Current.Text = record.LimitsOnCoverComparison.CurrentMedicalScheme;
                this.tbLimitsOnCover_Replaced.Text = record.LimitsOnCoverComparison.ReplacedMedicalScheme;
            }

            //Other
            if (record.OtherComparison != null)
            {
                this.tbCompOther_Current.Text = record.OtherComparison.CurrentMedicalScheme;
                this.tbCompOther_Replaced.Text = record.OtherComparison.ReplacedMedicalScheme;
            }
        }


        //Populate values for Needs and Goals table in Risk CAR
        private void PopulateRiskNeedsAndGoals(RiskAdviceRecord record)
        {
            DateTime rvDate;

            //Life
            if (record.LifeInfo != null)
            {
                this.tb_LifeNeedsQuantified.Text = "R " + record.LifeInfo.NeedsQuantified;
                this.tb_LifeNeedsPriority.Text = record.LifeInfo.NeedsPriority;
                this.cmb_Life.Text = record.LifeInfo.NeedAddressed;
                this.tb_LifeShortfall.Text = record.LifeInfo.Shortfall;

                if (DateTime.TryParseExact(record.LifeInfo.ReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
                {
                    this.dtp_LifeReviewDate.Value = rvDate;
                    this.dtp_LifeReviewDate.CustomFormat = "dd MMM yyyy";
                }
                else
                {
                    this.dtp_LifeReviewDate.CustomFormat = " ";
                }
            }

            //Income Protection
            if (record.IncomeProtectionInfo != null)
            {
                this.tb_PDIncomeProtectionNeedsQuantified.Text = "R " + record.IncomeProtectionInfo.NeedsQuantified;
                this.tb_PDIncomeProtectionNeedsPriority.Text = record.IncomeProtectionInfo.NeedsPriority;
                this.cmb_PDIncomeProtection.Text = record.IncomeProtectionInfo.NeedAddressed;
                this.tb_PDIncomeProtectionShortfall.Text = record.IncomeProtectionInfo.Shortfall;

                if (DateTime.TryParseExact(record.IncomeProtectionInfo.ReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
                {
                    this.dtp_PDIncomeProtectionReviewDate.Value = rvDate;
                    this.dtp_PDIncomeProtectionReviewDate.CustomFormat = "dd MMM yyyy";
                }
                else
                {
                    this.dtp_PDIncomeProtectionReviewDate.CustomFormat = " ";
                }
            }

            //Lump Sum
            if (record.LumpSumInfo != null)
            {
                this.tb_PDLumpSumNeedsQuantified.Text = "R " + record.LumpSumInfo.NeedsQuantified;
                this.tb_PDLumpSumNeedsPriority.Text = record.LumpSumInfo.NeedsPriority;
                this.cmb_PDLumpSum.Text = record.LumpSumInfo.NeedAddressed;
                this.tb_PDLumpSumShortfall.Text = record.LumpSumInfo.Shortfall;

                if (DateTime.TryParseExact(record.LumpSumInfo.ReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
                {
                    this.dtp_PDLumpSumReviewDate.Value = rvDate;
                    this.dtp_PDLumpSumReviewDate.CustomFormat = "dd MMM yyyy";
                }
                else
                {
                    this.dtp_TraumaReviewDate.CustomFormat = " ";
                }
            }

            //Temporary Disability
            if (record.TemporaryDisabilityInfo != null)
            {
                this.tb_TemporaryDisabilityNeedsQuantified.Text = "R " + record.TemporaryDisabilityInfo.NeedsQuantified;
                this.tb_TemporaryDisabilityNeedsPriority.Text = record.TemporaryDisabilityInfo.NeedsPriority;
                this.cmb_TemporaryDisability.Text = record.TemporaryDisabilityInfo.NeedAddressed;
                this.tb_TemporaryDisabilityShortfall.Text = record.TemporaryDisabilityInfo.Shortfall;

                if (DateTime.TryParseExact(record.TemporaryDisabilityInfo.ReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
                {
                    this.dtp_TemporaryDisabilityReviewDate.Value = rvDate;
                    this.dtp_TemporaryDisabilityReviewDate.CustomFormat = "dd MMM yyyy";
                }
                else
                {
                    this.dtp_TraumaReviewDate.CustomFormat = " ";
                }
            }

            //Trauma/Illness
            if (record.TraumaAndIllnessInfo != null)
            {
                this.tb_TraumaNeedsQuantified.Text = "R " + record.TraumaAndIllnessInfo.NeedsQuantified;
                this.tb_TraumaNeedsPriority.Text = record.TraumaAndIllnessInfo.NeedsPriority;
                this.cmb_Trauma.Text = record.TraumaAndIllnessInfo.NeedAddressed;
                this.tb_TraumaShortfall.Text = record.TraumaAndIllnessInfo.Shortfall;

                if (DateTime.TryParseExact(record.TraumaAndIllnessInfo.ReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
                {
                    this.dtp_TraumaReviewDate.Value = rvDate;
                    this.dtp_TraumaReviewDate.CustomFormat = "dd MMM yyyy";
                }
                else
                {
                    this.dtp_TraumaReviewDate.CustomFormat = " ";
                }
            }

            //Funeral Cover/Immediate Expenses
            if (record.FuneralCoverInfo != null)
            {
                this.tb_FuneralCoverNeedsQuantified.Text = "R " + record.FuneralCoverInfo.NeedsQuantified;
                this.tb_FuneralCoverNeedsPriority.Text = record.FuneralCoverInfo.NeedsPriority;
                this.cmb_FuneralCover.Text = record.FuneralCoverInfo.NeedAddressed;
                this.tb_FuneralCoverShortfall.Text = record.FuneralCoverInfo.Shortfall;

                if (DateTime.TryParseExact(record.FuneralCoverInfo.ReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
                {
                    this.dtp_FuneralCoverReviewDate.Value = rvDate;
                    this.dtp_FuneralCoverReviewDate.CustomFormat = "dd MMM yyyy";
                }
                else
                {
                    this.dtp_FuneralCoverReviewDate.CustomFormat = " ";
                }
            }

            //Other
            if (record.OtherInfo != null)
            {
                this.tb_RiskOtherNeedsQuantified.Text = "R " + record.OtherInfo.NeedsQuantified;
                this.tb_RiskOtherNeedsPriority.Text = record.OtherInfo.NeedsPriority;
                this.cmb_RiskOther.Text = record.OtherInfo.NeedAddressed;
                this.tb_RiskOtherShortfall.Text = record.OtherInfo.Shortfall;

                if (DateTime.TryParseExact(record.OtherInfo.ReviewDate, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rvDate))
                {
                    this.dtp_RiskOtherReviewDate.Value = rvDate;
                    this.dtp_RiskOtherReviewDate.CustomFormat = "dd MMM yyyy";
                }
                else
                {
                    this.dtp_RiskOtherReviewDate.CustomFormat = " ";
                }
            }
        }

        #endregion

        #region Populate CAR forms

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

        #endregion

        #region Clear methods

        //This is a method to remove all controls from the metro panel with exception to the scrollbar
        //(Had an issue where the vertical scrollbar would not reappear after the panel controls were cleared)

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

        //Method to reset the values of the custom made check boxes 
        private void clearCheckBoxControls()
        {
            clearProductKnlgeAndExperience();
            clearInvestmentHorizon();
            clearAccessToCapital();
        }




        #endregion

        private void metroLabel_PKEHint_Click(object sender, EventArgs e)
        {

        }
    }

}
