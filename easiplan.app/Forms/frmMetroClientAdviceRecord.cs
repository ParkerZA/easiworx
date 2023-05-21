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

        IList<ClientAdviceRecord> Notes = new List<ClientAdviceRecord>();
        IList<ClientAdviceRecord> filteredNotes = new List<ClientAdviceRecord>();
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

            //Form text box methods
            metroTextBox_NeedAndObjective.TextChanged += NeedAndObv_propertyChanged_EventHandler;
            metroTextBox_FinancialSolution.TextChanged += FinancialSolution_propertyChanged_EventHandler;

            //Allowing for adding date to text boxes
            metroTextBox_FinancialSolution.KeyDown += textBox_AppendNewLineDate;
            metroTextBox_NeedAndObjective.KeyDown += textBox_AppendNewLineDate;

            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;
            #endregion

            this.xInput_ShowCompletedTasks.ControlTypes = ControlTypes.CheckBox;
            this.xInput_ShowCompletedTasks.MappedField = "ShowCompletedTask";
            this.xInput_ShowCompletedTasks.LableText = "Show Completed notes";
            this.xInput_ShowCompletedTasks.Model = searchModel;
            this.xInput_ShowCompletedTasks.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInput_ShowCompletedTasks.chkBox.CheckedChanged += XInput_ShowCompletedTask_KeyPressed;
        }

        public frmMetroClientAdviceRecord(Retirement model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Retirement = model;
            InvestmentType = "Retirement";

            Initialise_SelectPanel(model.AdviceRecords);
            //selectedNote.PolicyNumber = model.ReferenceNo;
            
            
           

        }

        
        public frmMetroClientAdviceRecord(Investment model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");


            Investment = model;
            InvestmentType = "Investment";

            Initialise_SelectPanel(model.AdviceRecords);


        }

        public frmMetroClientAdviceRecord(Education model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Education = model;
            InvestmentType = "Education";
            Initialise_SelectPanel(model.AdviceRecords);

        }

        
        public frmMetroClientAdviceRecord(Medical model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Medical = model;
            InvestmentType = "Medical";

            Initialise_SelectPanel(model.AdviceRecords);

        }
        /*
        public frmMetroClientAdviceRecord(Life model, bool readOnly, PolicyAction action) : this($"{model.Description}[{model.ReferenceNo}]", readOnly, action)
        {

            if (model.Id == 0)
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Life = model;

            Initialise_SelectPanel(model.Notes);

        }*/

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

        #region Initialisation

        void Initialise_SelectPanel(IList<ClientAdviceRecord> notes = null)
        {
            if (notes != null)
                Notes = notes;

            if (Notes == null)
                filteredNotes = new List<ClientAdviceRecord>();
            else
                filteredNotes = Notes.Where(x => x.CreateDate != null).ToList();
                //filteredNotes = Notes.Where(x => x.IsCompleted == searchModel.ShowCompletedTask).ToList();

            #region Notes Grid
            this.dataGrid_Notes.Initialise1(filteredNotes, column =>
            {

                column.For(c => c.AdviceDate, "Note Date", new DateEditor(), MinWidth: 100);
                //column.For(x => x.PolicyNumber, "Policy no.", new StringEditor());
                //column.For(x => x.pol, "Policy status", new StringEditor());
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

            //selectedNote.InvestmentType = "Retirement";


            Initialise_PolicyNotePanel(selectedNote);
        }

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
                    InitializeStandardPortfolio();
                    populateRetirement(record);
 
                    break;
                case "investment":
                    this.xToolBarMenu1.tbCaption.Text = "Client Advice Record [CAR] - Investment Portfolio";
                    mostRecentRecord = Investment.AdviceRecords.LastOrDefault();

                    InitializeStandardPortfolio();
                    populateRetirement(record);
                    
                    break;
                case "medical":
                    this.xToolBarMenu1.tbCaption.Text = "Client Advice Record [CAR] - Medical Portfolio";
                    mostRecentRecord = Medical.AdviceRecords.LastOrDefault();

                    initialiseMedicalPortfolio();
                    populateMedical(record);

                    break;
                case "education":
                    this.xToolBarMenu1.tbCaption.Text = "Client Advice Record [CAR] - Education Portfolio";
                    mostRecentRecord = Education.AdviceRecords.LastOrDefault();

                    InitializeStandardPortfolio();
                    populateRetirement(record);
                    
                    break;
                case "incomeasset":
                    this.xToolBarMenu1.tbCaption.Text = "Client Advice Record [CAR] - Income Assets Portfolio";
                    mostRecentRecord = IncomeAsset.AdviceRecords.LastOrDefault();
                    
                    InitializeStandardPortfolio();
                    populateRetirement(record);
                    
                    break;
            }

            
                       /* this.metroPanel_Select.Initialise(selectedNote, cntr =>
            {
                //Put back after cntr.For(x => x.NoteDate, "Note Date ...", new MetroTextBoxEditor(160).ReadOnly(true));
                cntr.For(x => x.IsCompleted, "Completed", new MetroCheckBoxEditor().ReadOnly(ReadOnly));
            }, left: 100, top: 5, labelWidth: 100, controlsLayout: ControlsLayout.Horizontal, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();

            /* 
             this.metroPanel_needAndObj.Initialise<Note>(selectedNote, cntr =>
             {

                 //cntr.For(x => x.Text,"Needs and Objectives", new MetroMultiLineTextBoxEditor(Width: this.metroPanel_needAndObj.Width - 4, Height: this.metroPanel_needAndObj.Height - 30).ReadOnly(ReadOnly));


             }, left: 10, top: 0, labelWidth: 300, PropertyChangedHandler: Note_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Vertical, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged).Format();
             */
            
            selectedNote.IsLoading = false;

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
            Console.WriteLine(metroLabel_NeedsAndObj.Location);
        }

        #region Build Screens
        //Initialise the form screen for Retirement, Non-retirement, education, and income asset portfolios
        void InitializeStandardPortfolio()
        {
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
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_FinancialSolution);

            //Other Information
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherInformation);

            //Recommended Product
            this.metroPanel_AdviceRecord.Controls.Add(this.label_InitialAdvice);
            this.metroPanel_AdviceRecord.Controls.Add(this.label_InitialAdviceHint);
            
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_RecommendedFund);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_RecomendedFunds);
            //this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherInformation);

            //Motivation 
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Motivation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MotivationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Motivation);

            //this.metroPanel_PolicyNote.Controls.Add(new MetroScrollBar );

            this.metroPanel_AdviceRecord.PerformLayout();
        }

        void initialiseMedicalPortfolio()
        {
            Point startPoint = this.metroPanel_AdviceRecord.Location;

            //Summary (Title)
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Summary);

            //Product Knowledge and Experience
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_PKE);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_PKEHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_PKE);

            //Medical Conditions
            //this.metroLabel_MedicalConditions.Location = new System.Drawing.Point(10, 203);
            //this.metroLabel_MedicalConditionsHint.Location = new System.Drawing.Point(10, 223);
            //this.metroPanel_MedicalConditions.Location = new System.Drawing.Point(15, 255);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalConditions);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalConditionsHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_MedicalConditions);

            //Current Medical Cover
            //this.metroLabel_MedicalCover.Location = new System.Drawing.Point(10, 355);
            //this.metroLabel_MedicalCoverHint.Location = new System.Drawing.Point(10, 375);
            //this.metroPanel_MedicalCover.Location = new System.Drawing.Point(15, 407);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalCover);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_MedicalCoverHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_MedicalCover);


            //Other Information
            this.metroLabel_OtherInformation.Location = new Point(10-2, 507-101);
            this.metroLabel_OtherInformationHint.Location = new System.Drawing.Point(10-2, 527-101);
            this.metroPanel_OtherInformation.Location = new System.Drawing.Point(15-2, 559-101);

            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_OtherInformationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_OtherInformation);

            //Hospitalisation
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_Hospitalisation);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_HospitalisationHint);
            this.metroPanel_AdviceRecord.Controls.Add(this.metroPanel_Hospitalisation);

            //Needs and goals identified table
            this.metroPanel_AdviceRecord.Controls.Add(this.metroLabel_NeedsAndGoals);
            GenerateTable_NeedsAndGoals();
            initialiseTableListBoxes();

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



            this.metroPanel_AdviceRecord.Controls.Add(tblPanel_NeedsAndGoals);

            this.metroPanel_AdviceRecord.PerformLayout();
        }
        #endregion

        #region Lock State controls
        private void lockForm(bool state)
        {
            //Lock Product Knowledge
            lockButtons(metroPanel_PKE, state);

            //Lock investment Horizon
            lockButtons(metroPanel_InvestHorizen, state);

            //Lock Access to Capital
            lockButtons(metroPanel_AccessCapital, state);

            //Lock Needs and Objectives
            lockText(metroTextBox_NeedAndObjective, state);

            //Lock Financial Situation
            lockText(metroTextBox_FinancialSolution,state);
        }



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

        private void lockText(MetroTextBox txtBox, bool state)
        {
            txtBox.Enabled = !state;
        }
        #endregion

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
                        this.Medical.AdviceRecords.Add(selectedNote);

                        instructionType = InstructionType.MEDICAL_POLICY_NOTE;
                        referenceId = this.Medical.Id;
                        referenceNumber = this.Medical.ReferenceNo;
                        comment = this.Medical.Description;
                    }/*
                    if (this.Life != null)
                    {
                        this.Life.Notes.Add(selectedNote);

                        instructionType = InstructionType.LIFE_POLICY_NOTE;
                        referenceId = this.Life.Id;
                        referenceNumber = this.Life.ReferenceNo;
                        comment = this.Life.Description;

                    }*/
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
                xToolBarMenu1.SetCAREdit(false);
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

                selectedNote = new ClientAdviceRecord();

                clearSelection();

                if (mostRecentRecord != null)
                {
                    Initialise_PolicyNotePanel(mostRecentRecord);
                }
                else 
                {
                    Initialise_PolicyNotePanel(selectedNote);
                }

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
                        this.Medical.AdviceRecords.Remove(selectedNote);
                        Program.Repository.Update<Medical, int>(this.Medical);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Medical.AdviceRecords);
                    }/*
                    if (this.Life != null)
                    {
                        this.Life.Notes.Remove(selectedNote);
                        Program.Repository.Update<Life, int>(this.Life);

                        selectedNote = null;
                        Initialise_SelectPanel(this.Life.Notes);

                    }*/
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
                Initialise_SelectPanel(this.Medical.AdviceRecords);
            }/*
            if (this.Life != null)
            {
                Program.Repository.Update<Life, int>(this.Life);
                Initialise_SelectPanel(this.Life.Notes);
            }*/
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

        private void FinancialSolution_propertyChanged_EventHandler(object sender, EventArgs e)
        {

            try
            {
                this.xToolBarMenu1.SetCAREdit(true);

                selectedNote.FinancialSolution = metroTextBox_FinancialSolution.Text;
                selectedNote.UpdateBy = Program.User.Username;
                selectedNote.UpdateDate = DateTime.Now;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

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

        private void XInput_ShowCompletedTask_KeyPressed(object sender, EventArgs e)
        {
            //searchModel.ShowCompletedTask = !searchModel.ShowCompletedTask;
            this.xToolBarMenu1.SetCAREdit(false);
            Initialise_SelectPanel();
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

        #region Medical Table Elements

        #region Labels
        private void GenerateTable_NeedsAndGoals()
        {
            List<Label> vTableLbls = new List<Label>();
            List<Label> hTableLbls = new List<Label>();
            

            /*Horizontal Labels*/

            //Cover
            Label ng_Cover = new Label();
            ng_Cover.Text = "Cover";
            hTableLbls.Add(ng_Cover);

            //Cover Discussed
            Label ng_CoverDiscussed = new Label();
            ng_CoverDiscussed.Text = "Cover\n Discussed";
            hTableLbls.Add(ng_CoverDiscussed);

            //Cover Taken
            Label ng_CoverTaken = new Label();
            ng_CoverTaken.Text = "Cover\n Taken";
            hTableLbls.Add(ng_CoverTaken);

            //Comments
            Label ng_Comment = new Label();
            ng_Comment.Text = "Comments:";
            hTableLbls.Add(ng_Comment);


            /*Vertical Labels*/

            //Hospitalisation Cover
            Label ng_HospitalCover = new Label();
            ng_HospitalCover.Text = "Hospitalisation Cover";
            vTableLbls.Add(ng_HospitalCover);

            //Day To Day Benefit
            Label ng_DayToDay = new Label();
            ng_DayToDay.Text = "Day-to-Day Benefit";
            vTableLbls.Add(ng_DayToDay);

            //Threshold Benefit
            Label ng_Threshold = new Label();
            ng_Threshold.Text = "Threshold Benefit";
            vTableLbls.Add(ng_Threshold);
            
            //Chronic Benefit
            Label ng_ChronicBenefit = new Label();
            ng_ChronicBenefit.Text = "Chronic Benefit";
            vTableLbls.Add(ng_ChronicBenefit);

            //Savings Account
            Label ng_SavingsAccount = new Label();
            ng_SavingsAccount.Text = "Savings Account";
            vTableLbls.Add(ng_SavingsAccount );

            //Hospital Preference
            Label ng_Preference = new Label();
            ng_Preference.Text = "Hospital Preference";
            vTableLbls.Add(ng_Preference);

            //Gap Cover
            Label ng_GapCover = new Label();
            ng_GapCover.Text = "Gap Cover";
            vTableLbls.Add(ng_GapCover );

            //Other
            Label ng_Other = new Label();
            ng_Other.Text = "Other";
            vTableLbls.Add(ng_Other);

            
            //Format Horizontal headings
            foreach (Label lbl in hTableLbls)
            {
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
            }
            ng_Comment.TextAlign = ContentAlignment.MiddleLeft;

            //Format Vertical headings
            foreach (Label lbl in vTableLbls)
            {
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
            }

            //Add labels to table

            //Horizontal labels
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_Cover, 0, 0);
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_CoverDiscussed, 1, 0);
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_CoverTaken, 2, 0);
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_Comment, 3, 0);

            //Vertical labels
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_HospitalCover, 0, 1);
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_DayToDay, 0, 2);
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_Threshold, 0, 3);
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_ChronicBenefit, 0, 4);
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_SavingsAccount, 0, 5);
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_Preference, 0, 6);
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_GapCover, 0, 7);
            this.tblPanel_NeedsAndGoals.Controls.Add(ng_Other, 0, 8);
        }
        #endregion

        #region CheckBoxes
        private void initialiseTableListBoxes()
        {

            List<MetroComboBox> listBoxes = new List<MetroComboBox>();

            //Cover
            MetroComboBox Cover1 = new MetroComboBox();
            listBoxes.Add(Cover1);
            MetroComboBox Cover2 = new MetroComboBox();
            listBoxes.Add(Cover2);

            //DayToDay
            MetroComboBox DayToDay1 = new MetroComboBox();
            listBoxes.Add(DayToDay1);
            MetroComboBox DayToDay2 = new MetroComboBox();
            listBoxes.Add(DayToDay2);

            //Threshold Benefit
            MetroComboBox Threshold1 = new MetroComboBox();
            listBoxes.Add(Threshold1);
            MetroComboBox Threshold2 = new MetroComboBox();
            listBoxes.Add(Threshold2);


            //ChronicBenefit
            MetroComboBox ChronicBenefit1 = new MetroComboBox();
            listBoxes.Add(ChronicBenefit1);
            MetroComboBox ChronicBenefit2 = new MetroComboBox();
            listBoxes.Add(ChronicBenefit2);

            //Savings account
            MetroComboBox Savings1 = new MetroComboBox();
            listBoxes.Add(Savings1);
            MetroComboBox Savings2 = new MetroComboBox();
            listBoxes.Add(Savings2);

            //HospitalPreference
            MetroComboBox HospitalPreference1 = new MetroComboBox();
            listBoxes.Add(HospitalPreference1);
            MetroComboBox HospitalPreference2 = new MetroComboBox();
            listBoxes.Add(HospitalPreference2);

            //Gap Cover
            MetroComboBox GapCover1 = new MetroComboBox();
            listBoxes.Add(GapCover1);
            MetroComboBox GapCover2 = new MetroComboBox();
            listBoxes.Add(GapCover2);

            //Other
            MetroComboBox Other1 = new MetroComboBox();
            listBoxes.Add(Other1);
            MetroComboBox Other2 = new MetroComboBox();
            listBoxes.Add(Other2);

            foreach (MetroComboBox dropDown in listBoxes)
            {
                dropDown.DropDownStyle = ComboBoxStyle.DropDownList;
                dropDown.Dock = DockStyle.Fill;
                dropDown.Items.Add("");
                dropDown.Items.Add("Yes");
                dropDown.Items.Add("No");

            }

            
            
            //this.tblPanel_NeedsAndGoals.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tblPanel_NeedsAndGoals.Controls.Add(Cover1, 1, 1);
            this.tblPanel_NeedsAndGoals.Controls.Add(Cover2, 2, 1);

            this.tblPanel_NeedsAndGoals.Controls.Add(DayToDay1, 1, 2);
            this.tblPanel_NeedsAndGoals.Controls.Add(DayToDay2, 2, 2);

            this.tblPanel_NeedsAndGoals.Controls.Add(Threshold1, 1, 3);
            this.tblPanel_NeedsAndGoals.Controls.Add(Threshold2, 2, 3);

            this.tblPanel_NeedsAndGoals.Controls.Add(ChronicBenefit1, 1, 4);
            this.tblPanel_NeedsAndGoals.Controls.Add(ChronicBenefit2, 2, 4);

            this.tblPanel_NeedsAndGoals.Controls.Add(Savings1, 1, 5);
            this.tblPanel_NeedsAndGoals.Controls.Add(Savings2, 2, 5);

            this.tblPanel_NeedsAndGoals.Controls.Add(HospitalPreference1, 1, 6);
            this.tblPanel_NeedsAndGoals.Controls.Add(HospitalPreference2, 2, 6);

            this.tblPanel_NeedsAndGoals.Controls.Add(GapCover1, 1, 7);
            this.tblPanel_NeedsAndGoals.Controls.Add(GapCover2, 2, 7);

            this.tblPanel_NeedsAndGoals.Controls.Add(Other1, 1, 8);
            this.tblPanel_NeedsAndGoals.Controls.Add(Other2, 2, 8);




        }
        #endregion

        #endregion

        public class ClientSearchModel : BaseEntity<int>
        {
            public bool ShowCompletedTask { get; set; }
        }

        private void metroPanel_PolicyNote_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMetroClientAdviceRecord_Load(object sender, EventArgs e)
        {

        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel8_Click(object sender, EventArgs e)
        {

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
                this.IHchk1.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.IHchk2.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.IHchk3.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.IHchk4.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.PKEchk1.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.PKEchk2.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.PKEchk3.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.PKEchk4.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.PKEchk5.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.PKEchk6.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.PKEchk7.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.PKEchk8.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.PKEchk9.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.PKEchk10.Image = global::easiplan.app.Properties.Resources.checkmark_1;

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
                this.AtCchk1.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.AtCchk2.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.AtCchk3.Image = global::easiplan.app.Properties.Resources.checkmark_1;
                selectedNote.AccessToCapital = "Do not require access to capital for 5 years";
            }

            this.xToolBarMenu1.SetCAREdit(true);

            selectedNote.UpdateBy = Program.User.Username;
            selectedNote.UpdateDate = DateTime.Now;
        }

        #endregion

        #endregion

        #region Populate methods

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
                    this.PKEchk10.Image = global::easiplan.app.Properties.Resources.checkmark_1;
                }
                else
                {
                    PKEchk1.PerformClick();
                    this.PKEchk1.Image = global::easiplan.app.Properties.Resources.checkmark_1;
                }
            }
            else if (record.ProductKnowledge.Contains("2"))
            {
                PKEchk2.PerformClick();
                this.PKEchk2.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.ProductKnowledge.Contains("3"))
            {
                PKEchk3.PerformClick();
                this.PKEchk3.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.ProductKnowledge.Contains("4"))
            {
                
                PKEchk4.PerformClick();
                this.PKEchk4.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.ProductKnowledge.Contains("5"))
            {
                PKEchk5.PerformClick();
                this.PKEchk5.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.ProductKnowledge.Contains("6"))
            {
                PKEchk6.PerformClick();
                this.PKEchk6.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.ProductKnowledge.Contains("7"))
            {
                PKEchk7.PerformClick();
                this.PKEchk7.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.ProductKnowledge.Contains("8"))
            {
                PKEchk8.PerformClick();
                this.PKEchk8.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.ProductKnowledge.Contains("9"))
            {
                PKEchk9.PerformClick();
                this.PKEchk9.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
        }
        #endregion
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
                this.IHchk1.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.InvestmentHorizen.Contains("2-5"))
            {
                IHchk2.PerformClick();
                this.IHchk2.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.InvestmentHorizen.Contains("5-9"))
            {
                IHchk3.PerformClick();
                this.IHchk3.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.InvestmentHorizen.Contains("10"))
            {
                IHchk4.PerformClick();
                this.IHchk4.Image = global::easiplan.app.Properties.Resources.checkmark_1;
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
                this.AtCchk1.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.AccessToCapital.ToLower().Contains("always"))
            {
                AtCchk2.PerformClick();
                this.AtCchk2.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
            else if (record.AccessToCapital.ToLower().Contains("5"))
            {
                AtCchk3.PerformClick();
                this.AtCchk3.Image = global::easiplan.app.Properties.Resources.checkmark_1;
            }
        }

        
        private void populateNeedsAndObjectives(ClientAdviceRecord record)
        {
            if (record.NeedsAndObjectives != null)
            {
                this.metroTextBox_NeedAndObjective.Text = record.NeedsAndObjectives;
            }
        }

        private void populateFinancialSolution(ClientAdviceRecord record)
        {
            if (record.FinancialSolution != null)
            {
                this.metroTextBox_FinancialSolution.Text = record.FinancialSolution;
            }
        }

        private void populateRetirement(ClientAdviceRecord advRecord)
        {
            clearSelection();
            populateProductAndExperience(advRecord);
            populateInvestmentHorizon(advRecord);
            populateAccessToCapital(advRecord);
            populateNeedsAndObjectives(advRecord);
            populateFinancialSolution(advRecord);
        }

        private void populateMedical(ClientAdviceRecord advRecord)
        {
            clearSelection();
            populateProductAndExperience(advRecord);
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
                //control.Dispose(); // Optional, if needed
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

        #region Clear Text Boxes
        private void clearNeedsAndObj()
        {
            this.metroTextBox_NeedAndObjective.Text = selectedNote.NeedsAndObjectives;
        }

        private void clearFinancialSolution()
        {
            this.metroTextBox_FinancialSolution.Text = selectedNote.FinancialSolution;
        }

        #region Medical Text Boxes

        private void clearMedicalConditions()
        {
            this.metroTextBox_MedicalConditions.Text = selectedNote.FinancialSolution;
        }

        private void clearMedicalCover()
        {
            this.metroTextBox_CurrentMedicalCover.Text = selectedNote.FinancialSolution;
        }

        private void clearHospitalisation()
        {
            this.metroTextBox_Hospitalisation.Text = selectedNote.FinancialSolution;
        }

        private void clearChronicConditions()
        {
            this.metroTextBox_ChronicConditions.Text = selectedNote.FinancialSolution;
        }

        private void clearWaitingPeriods()
        {
            this.metroTextBox_WaitingPeriods.Text = selectedNote.FinancialSolution;
        }

        private void clearLateJoinerPenalty()
        {
            this.metroTextBox_LateJoiner.Text = selectedNote.FinancialSolution;
        }

        private void clearCoPayment()
        {
            this.metroTextBox_Copayment.Text = selectedNote.FinancialSolution;
        }

        #endregion

        #endregion
        private void clearSelection()
        {
            clearProductKnlgeAndExperience();
            clearInvestmentHorizon();
            clearAccessToCapital();
            clearNeedsAndObj();
            clearFinancialSolution();
        }


        #endregion

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }

}
