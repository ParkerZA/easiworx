
using my.domain.lib.core;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Extensions;

using Finx.App.Enums;
using Finx.App.Extensions;
using easiplan.domain.Entities;
using easiplan.domain;

using MetroFramework;
using MetroFramework.Controls.Ext;
using MetroFramework.Forms;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using SourceGrid;
using easiplan.app.ContextMenus;
using easiplan.domain.Views;
using easiplan.app.Extensions;

namespace Finx.App.Forms
{
    public partial class frmMetroPolicyFunds : MetroForm
    {
        #region Delegates and Events
        //Need
        // public delegate Need GetNeedEventHandler(object sender, EventArgs args);
        // public event GetNeedEventHandler GetNeedEvent;

        #endregion

        #region Private Variables
        bool HasChanges = false;
        bool ReadOnly = true;

        PolicyAction Action;

        Need TempNeed = null;
        Need FnaNeed = null;
        EducationNeed EducationNeed = null;
        InvestmentNeed InvestmentNeed = null;
        RiskCoverNeed RiskCoverNeed = null;

        Retirement Retirement = null;
        Investment Investment = null;
        Education Education = null;
        Medical Medical = null;
        Life Life = null;
        IncomeAsset IncomeAsset = null;

        Instruction Instruction = null;

        Lisp Lisp;
        List<ListDataItem> LispFunds = new List<ListDataItem>();

        Client Client = null;

        #endregion

        #region Constructors
        /// <summary>
        /// Base Constructor
        /// </summary>
        /// <param name="action"></param>
        /// <param name="lispName"></param>
        /// <param name="readOnly"></param>
        internal frmMetroPolicyFunds(PolicyAction action, string lispName = "", bool readOnly = true, Client client = null)
        {
            InitializeComponent();

            ReadOnly = readOnly;
            Action = action;

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;

            this.Style = MetroColorStyle.White;

            this.Text = string.Empty;
            this.SubTitle = string.Format("{0} :", action.ToDescription());
            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);

            this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.cog_32;

            this.DisplayHeader = false;

            this.Width = 1150;
            #endregion

            #region Collections for DropdownComboxes
            if (Program.ServiceProviders != null && !string.IsNullOrEmpty(lispName))
            {
                //Lisp Funds
                Lisp = Program.ServiceProviders.LispProviders.Lisps.Where(x => x.LispName.ToUpper() == lispName.ToUpper())
                            .FirstOrDefault();
                if (Lisp != null)
                    LispFunds = Lisp.LispFunds.ToListDataItem("FundName", "FundName"); // the list of funds for the selected LISP
            }
            #endregion

            #region Initialise TabControl
            this.metroTabControl_PolicyMain.TabPages[0].Format("Funds");
            this.metroTabControl_PolicyMain.TabPages[1].Format("Beneficiaries");
            this.metroTabControl_PolicyMain.TabPages[2].Format("Comments");


            this.metroTabControl_PolicyMain.SelectedIndex = 0;
            #endregion

            Client = client;
        }

        public frmMetroPolicyFunds(Retirement model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Description, readOnly, client)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            if (string.IsNullOrEmpty(model.Description))
                throw new MyValidationException("You have to select a LISP provider before you can allocate funds");

            if (string.IsNullOrEmpty(model.Insured))
                throw new MyValidationException("Policy owner is required.");

            Retirement = model;

            InitialiseForm();
        }

        public frmMetroPolicyFunds(Investment model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Description, readOnly, client)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            if (string.IsNullOrEmpty(model.Description))
                throw new MyValidationException("You have to select a LISP provider before you can allocate funds");

            if (string.IsNullOrEmpty(model.Insured))
                throw new MyValidationException("Policy owner is required.");

            Investment = model;

            InitialiseForm();
        }

        public frmMetroPolicyFunds(Education model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Description, readOnly, client)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            if (string.IsNullOrEmpty(model.DependentName))
                throw new MyValidationException("Policy owner is required.");

            Education = model;

            InitialiseForm();
        }

        public frmMetroPolicyFunds(Medical model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Type, readOnly, client)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            if (string.IsNullOrEmpty(model.Insured))
                throw new MyValidationException("Policy owner is required.");

            Medical = model;

            this.metroTabControl_PolicyMain.TabPages[0].Format("Benefits");

            InitialiseForm();


        }

        public frmMetroPolicyFunds(Life model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Type, readOnly, client)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            if (string.IsNullOrEmpty(model.Insured))
                throw new MyValidationException("Policy owner is required.");

            Life = model;

            this.metroTabControl_PolicyMain.TabPages[0].Format("Benefits");

            InitialiseForm();

        }

        public frmMetroPolicyFunds(IncomeAsset model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Type, readOnly, client)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            IncomeAsset = model;

            InitialiseForm();

        }

        /// <summary>
        /// Retirement Task
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <param name="readOnly"></param>
        /// <param name="client"></param>
        public frmMetroPolicyFunds(Need model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Description, readOnly, client)
        {

            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            if (string.IsNullOrEmpty(model.Description))
                throw new MyValidationException("You have to select a LISP provider before you can allocate funds");

            if (string.IsNullOrEmpty(model.Insured))
                throw new MyValidationException("Policy owner is required.");

            FnaNeed = model;

            InitialiseForm();
        }

        /// <summary>
        /// Education Task
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <param name="readOnly"></param>
        /// <param name="client"></param>
        public frmMetroPolicyFunds(EducationNeed model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Description, readOnly, client)
        {

            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            if (string.IsNullOrEmpty(model.Description))
                throw new MyValidationException("You have to select a LISP provider before you can allocate funds");

            if (string.IsNullOrEmpty(model.Insured))
                throw new MyValidationException("Policy owner is required.");

            EducationNeed = model;

            InitialiseForm();

        }

        /// <summary>
        /// Investment Task
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <param name="readOnly"></param>
        /// <param name="client"></param>
        public frmMetroPolicyFunds(InvestmentNeed model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Description, readOnly, client)
        {

            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            //Investment may not be LISP related
            //if (string.IsNullOrEmpty(model.Description))
            //    throw new MyValidationException("You have to select a LISP provider before you can allocate funds");

            if (string.IsNullOrEmpty(model.Insured))
                throw new MyValidationException("Policy owner is required.");

            InvestmentNeed = model;

            InitialiseForm();

        }

        /// <summary>
        /// RiskCover Task
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <param name="readOnly"></param>
        /// <param name="client"></param>
        public frmMetroPolicyFunds(RiskCoverNeed model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Description, readOnly, client)
        {

            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            //Investment is not be LISP related
            //if (string.IsNullOrEmpty(model.Description))
            //    throw new MyValidationException("You have to select a LISP provider before you can allocate funds");

            if (string.IsNullOrEmpty(model.Insured))
                throw new MyValidationException("Insured is required.");

            RiskCoverNeed = model;

            InitialiseForm();

        }
        /// <summary>
        /// Instruction Task
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <param name="readOnly"></param>
        /// <param name="client"></param>
        public frmMetroPolicyFunds(Instruction model, PolicyAction action, bool readOnly = true, Client client = null) : this(action, model.Type, readOnly, client)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            if (model.ReferenceId == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Cannot open this Instruction. Missing Reference Id");

            Instruction = model;

            if (client == null)
                Client = Program.Repository.Get<Client, int>(Instruction.ClientId);

            InitialiseForm();

        }
        #endregion

        #region InitialiseGrid
        void InitialiseGrid(Retirement model)
        {
            if (Action == PolicyAction.PolicyHistory)
            {
                TempNeed = new Need() { ReferenceNo = model.ReferenceNo, CurrentAge = model.CurrentAge, InvestmentAge = model.InvestmentAge, EscalationPercentage = model.EscalationPercentage, GrowthPercentage = model.GrowthPercentage };

                InitialiseGrid(Program.Repository.List<Need, int>(x => x.NeedType == NeedTypes.RetirementNeed && x.ReferenceId == model.Id).ToList());
            }
            else
            {
                TempNeed = Program.Repository.Get<Need, int>(model.ReferenceId);

                if (TempNeed != null)
                    if (TempNeed.Status != "Implemented")
                        TempNeed.IsUpdate = true;
                    else TempNeed = null;

                if (TempNeed == null)
                    TempNeed = model.CloneAsNeed(Action);

                InitialiseGrid(TempNeed);

            }

        }
        void InitialiseGrid(Investment model)
        {
            if (Action == PolicyAction.PolicyHistory)
            {
                TempNeed = new Need() { ReferenceNo = model.ReferenceNo, CurrentAge = model.CurrentAge, InvestmentAge = model.InvestmentAge, EscalationPercentage = model.EscalationPercentage, GrowthPercentage = model.GrowthPercentage };

                InitialiseGrid(Program.Repository.List<Need, int>(x => x.NeedType == NeedTypes.InvestmentNeed && x.ReferenceId == model.Id).ToList());
            }
            else
            {
                TempNeed = Program.Repository.Get<InvestmentNeed, int>(model.ReferenceId);

                if (TempNeed == null)
                    TempNeed = Program.Repository.Get<Need, int>(model.ReferenceId);

                if (TempNeed != null)
                    if (TempNeed.Status != "Implemented")
                        TempNeed.IsUpdate = true;
                    else TempNeed = null;

                if (TempNeed == null)
                    TempNeed = model.CloneAsNeed(Action);

                InitialiseGrid(TempNeed);

            }

        }
        void InitialiseGrid(Education model)
        {
            if (Action == PolicyAction.PolicyHistory)
            {
                TempNeed = new Need() { ReferenceNo = model.ReferenceNo, CurrentAge = model.CurrentAge, InvestmentAge = model.InvestmentAge, EscalationPercentage = model.EscalationPercentage, GrowthPercentage = model.GrowthPercentage };

                InitialiseGrid(Program.Repository.List<Need, int>(x => x.NeedType == NeedTypes.EducationNeed && x.ReferenceId == model.Id).ToList());
            }
            else
            {

                TempNeed = Program.Repository.Get<EducationNeed, int>(model.ReferenceId);

                if (TempNeed == null)
                    TempNeed = Program.Repository.Get<Need, int>(model.ReferenceId);

                if (TempNeed != null)
                    if (TempNeed.Status != "Implemented")
                        TempNeed.IsUpdate = true;
                    else TempNeed = null;

                if (TempNeed == null)
                    TempNeed = model.CloneAsNeed(Action);

                InitialiseGrid(TempNeed);

            }
        }
        void InitialiseGrid(Medical model)
        {
            if (Action == PolicyAction.PolicyHistory)
            {
                TempNeed = new Need() { ReferenceNo = model.ReferenceNo, CurrentAge = model.CurrentAge, InvestmentAge = model.InvestmentAge, EscalationPercentage = model.EscalationPercentage, GrowthPercentage = model.GrowthPercentage };

                InitialiseGrid(Program.Repository.List<Need, int>(x => x.NeedType == NeedTypes.MedicalNeed && x.ReferenceId == model.Id).ToList());
            }
            else
            {
                TempNeed = Program.Repository.Get<Need, int>(model.ReferenceId);

                if (TempNeed != null)
                    if (TempNeed.Status != "Implemented")
                        TempNeed.IsUpdate = true;
                    else TempNeed = null;

                if (TempNeed == null)
                    TempNeed = model.CloneAsNeed(Action);

                TempNeed.Initialise();
                TempNeed.PropertyChanged -= Need_PropertyChanged;

                #region Temp Need
                this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
                {
                    column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
                    //column.For(x => x.Type, "Investment Type", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.RetirementAssetClass)).ReadOnly(ReadOnly));
                    // column.For(x => x.Description, "LISP", new MetroComboBoxEditor().DataSourceList(Lisps).ReadOnly(true));
                    column.For(x => x.Description, "Plan", new MetroTextBoxEditor().ReadOnly(true));
                    column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    //column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    //column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    //column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    //column.For(c => c.CurrentAmount, "Cover Amount", new MetroCurrencyEditor().ReadOnly(true));
                    //column.For(c => c.FutureAmount, "Retirement Value", new MetroCurrencyEditor().ReadOnly(true));
                    column.For(c => c.Status, "Policy Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(true));

                }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
                    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                    ReadOnly: ReadOnly,
                  AllowDelete: false,
                    AllowAddNew: false)
                    .Formatt(ReadOnly);
                #endregion

                #region Benefits
                this.dataGrid_PolicyFunds.Initialise(TempNeed.Benefits, columns =>
                {
                    columns.For(x => x.Description, "Benefit Type", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
                    //columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    //columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    //columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
                    columns.For(x => x.CoverAmount, "Cover Amount", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                    columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                }, PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
                    ItemDeleteEventHandler: Benefit_propertyDelete_EventHandler,
                    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                    ReadOnly: ReadOnly,
                     AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
                    AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
                    .Formatt(ReadOnly);
                #endregion

                #region Comments
                this.dataGrid_Comments.Initialise(TempNeed.Notes, columns =>
                {
                    columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
                    //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
                    columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
                    columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
                }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
                //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                ReadOnly: ReadOnly,
                AllowDelete: true)
                .Formatt(ReadOnly);
                #endregion

                #region Beneficiaries
                this.dataGrid_Beneficiaries.Initialise<ClientDependent>(TempNeed.Beneficiaries, column =>
                {
                    column.For(x => x.DependentName, "Name", new MetroComboBoxEditor().DataSourceList(GetClientDependents().ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                    column.For(c => c.DependentType, "Relation", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.DependentType)));
                    column.For(c => c.BirthDate, "Birth Date", new MetroDateEditor());
                    column.For(c => c.IdNumber, "Id Number", new MetroSAIDEditor());
                    column.For(c => c.Percentage, "Percentage %", new MetroPercentageEditor());
                    column.For(c => c._Age, "Age", new MetroTextBoxEditor().ReadOnly(true));
                }
                , ReadOnly: ReadOnly, AllowDelete: true, PropertyChangedHandler: Beneficiary_propertyChanged_EventHandler
                ).Formatt(ReadOnly);
                #endregion

                TempNeed.PropertyChanged += Need_PropertyChanged;
            }
        }
        void InitialiseGrid(Life model)
        {
            if (Action == PolicyAction.PolicyHistory)
            {
                TempNeed = new Need() { ReferenceNo = model.ReferenceNo, CurrentAge = model.CurrentAge, InvestmentAge = model.InvestmentAge, EscalationPercentage = model.EscalationPercentage, GrowthPercentage = model.GrowthPercentage };

                InitialiseGrid(Program.Repository.List<Need, int>(x => x.NeedType == NeedTypes.LifeDisabilityNeed && x.ReferenceId == model.Id).ToList());
            }
            else
            {
                TempNeed = Program.Repository.Get<Need, int>(model.ReferenceId);

                if (TempNeed != null)
                    if (TempNeed.Status != "Implemented")
                        TempNeed.IsUpdate = true;
                    else TempNeed = null;

                if (TempNeed == null)
                    TempNeed = model.CloneAsNeed(Action);

                TempNeed.Initialise();
                TempNeed.PropertyChanged -= Need_PropertyChanged;

                #region Temp Need
                this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
                {
                    column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
                    column.For(x => x.Type, "Policy Type", new MetroTextBoxEditor().ReadOnly(true));
                    column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    //column.For(c => c.InitialAmount, "Insured Amount", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    column.For(c => c.Status, "Policy Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(true));

                }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
                    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                    ReadOnly: ReadOnly,
                    AllowDelete: false,
                    AllowAddNew: false)
                    .Formatt(ReadOnly);
                #endregion

                #region Benefits
                this.dataGrid_PolicyFunds.Initialise(TempNeed.Benefits, columns =>
                {
                    columns.For(x => x.Type, "Benefit", new MetroComboListEditor().DataSourceList(Program.listData.List(ListDataItemType.LifeBenefit)).ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    columns.For(x => x.Description, "Description", new MetroTextBoxEditor().ReadOnly(ReadOnly));
                    columns.For(x => x.CoverAmount, "Insured Amount", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                    columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                }, PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
                    ItemDeleteEventHandler: Benefit_propertyDelete_EventHandler,
                    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                    ReadOnly: ReadOnly,
                    AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
                    AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
                    .Formatt(ReadOnly);
                #endregion

                #region Comments
                this.dataGrid_Comments.Initialise(TempNeed.Notes, columns =>
                {
                    columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
                    //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
                    columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
                    columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
                }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
                //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                ReadOnly: ReadOnly,
                AllowDelete: true)
                .Formatt(ReadOnly);
                #endregion

                #region Beneficiaries
                this.dataGrid_Beneficiaries.Initialise<ClientDependent>(TempNeed.Beneficiaries, column =>
                {
                    column.For(x => x.DependentName, "Name", new MetroComboBoxEditor().DataSourceList(GetClientDependents().ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                    column.For(c => c.DependentType, "Relation", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.DependentType)));
                    column.For(c => c.BirthDate, "Birth Date", new MetroDateEditor());
                    column.For(c => c.IdNumber, "Id Number", new MetroSAIDEditor());
                    column.For(c => c.Percentage, "Percentage %", new MetroPercentageEditor());
                    column.For(c => c._Age, "Age", new MetroTextBoxEditor().ReadOnly(true));
                }
                , ReadOnly: ReadOnly, AllowDelete: true, PropertyChangedHandler: Beneficiary_propertyChanged_EventHandler
                ).Formatt(ReadOnly);
                #endregion

                TempNeed.PropertyChanged += Need_PropertyChanged;
            }
        }
        void InitialiseGrid(IncomeAsset model)
        {
            if (Action == PolicyAction.PolicyHistory)
            {
                TempNeed = new Need() { ReferenceNo = model.ReferenceNo, CurrentAge = model.CurrentAge, InvestmentAge = model.InvestmentAge, EscalationPercentage = model.EscalationPercentage, GrowthPercentage = model.GrowthPercentage };

                InitialiseGrid(Program.Repository.List<Need, int>(x => x.NeedType == NeedTypes.IncomeAssetNeed && x.ReferenceId == model.Id).ToList());
            }
            else
            {

                TempNeed = Program.Repository.Get<Need, int>(model.ReferenceId);

                if (TempNeed != null)
                    if (TempNeed.Status != "Implemented")
                        TempNeed.IsUpdate = true;
                    else TempNeed = null;

                if (TempNeed == null)
                    TempNeed = model.CloneAsNeed(Action);

                TempNeed.Initialise();
                TempNeed.PropertyChanged -= Need_PropertyChanged;

                #region Temp Need
                this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
                {
                    column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
                    column.For(x => x.Description, "Description", new MetroTextBoxEditor().ReadOnly(true));
                    column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    column.For(c => c.MonthlyContribution, "Income p/m", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    column.For(c => c.InitialAmount, "Asset Value", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    column.For(c => c.Status, "Policy Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(true));

                }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
                    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                    ReadOnly: ReadOnly,
                    AllowDelete: false,
                    AllowAddNew: false)
                    .Formatt(ReadOnly);
                #endregion

                #region Funds
                this.dataGrid_PolicyFunds.Initialise(TempNeed.Funds, columns =>
                {
                    columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
                    columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    columns.For(x => x.MonthlyContribution, "Income", new MetroCurrencyEditor().ReadOnly(true));
                    columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(Action == PolicyAction.AmendPolicy ? true : ReadOnly));
                    columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                    //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
                    // columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
                    // columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
                }, PropertyChangedHandler: Fund_propertyChanged_EventHandler,
               ItemDeleteEventHandler: Fund_propertyDelete_EventHandler,
               //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
               ReadOnly: ReadOnly,
               AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
               AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
               .Formatt(ReadOnly);
                #endregion

                #region Comments
                this.dataGrid_Comments.Initialise(TempNeed.Notes, columns =>
                {
                    columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
                    //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
                    columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
                    columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
                }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
                //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                ReadOnly: ReadOnly,
                AllowDelete: true)
                .Formatt(ReadOnly);
                #endregion

                #region Beneficiaries
                this.dataGrid_Beneficiaries.Initialise<ClientDependent>(TempNeed.Beneficiaries, column =>
                {
                    column.For(x => x.DependentName, "Name", new MetroComboBoxEditor().DataSourceList(GetClientDependents().ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                    column.For(c => c.DependentType, "Relation", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.DependentType)));
                    column.For(c => c.BirthDate, "Birth Date", new MetroDateEditor());
                    column.For(c => c.IdNumber, "Id Number", new MetroSAIDEditor());
                    column.For(c => c.Percentage, "Percentage %", new MetroPercentageEditor());
                    column.For(c => c._Age, "Age", new MetroTextBoxEditor().ReadOnly(true));
                }
                , ReadOnly: ReadOnly, AllowDelete: true, PropertyChangedHandler: Beneficiary_propertyChanged_EventHandler
                ).Formatt(ReadOnly);
                #endregion

                TempNeed.PropertyChanged += Need_PropertyChanged;
            }
        }

        void InitialiseGrid(Need model)
        {

            if (TempNeed == null)
                TempNeed = model;

            InitialiseGridBase(model);


        }
        void InitialiseGrid(EducationNeed model)
        {
            //Add Defualt Beneficiary if not exists
            if (model.Beneficiaries.Count == 0)
            {
                ClientDependent _beneficiary = new ClientDependent()
                {
                    BirthDate = model.DependentDOB,
                    DependentName = model.DependentName,
                    Percentage = 100
                };
                model.Beneficiaries.Add(_beneficiary);
            }

            if (TempNeed == null)
                TempNeed = model;

            InitialiseGridBase(model);


        }
        void InitialiseGrid(InvestmentNeed model)
        {
            if (TempNeed == null)
                TempNeed = model;

            InitialiseGridBase(model);


        }
        void InitialiseGrid(RiskCoverNeed model)
        {

            if (TempNeed == null)
                TempNeed = model;

            InitialiseGridBase(model);


        }
        void InitialiseGrid(Instruction model)
        {
            if (TempNeed == null)
            {
                TempNeed = Program.Repository.Get<InvestmentNeed, int>(model.ReferenceId);
                if (TempNeed == null)
                    TempNeed = Program.Repository.Get<EducationNeed, int>(model.ReferenceId);
                if (TempNeed == null)
                    TempNeed = Program.Repository.Get<RiskCoverNeed, int>(model.ReferenceId);
                if (TempNeed == null)
                    TempNeed = Program.Repository.Get<Need, int>(model.ReferenceId);

                if (TempNeed == null)
                {
                    throw new MyValidationException("Policy amendment no longer exists");
                }
            }

            InitialiseGrid(TempNeed);
        }

        private void InitialiseGridBase(Need model)
        {

            if (model.Status == "Completed" || model.Status == "Implemented" || model.Status == "Cancelled")
                ReadOnly = true;

            model.Initialise();

            model.PropertyChanged -= Need_PropertyChanged;

            model.Calculate();

            switch (model.NeedType)
            {
                case NeedTypes.LifeDisabilityNeed:
                    this.metroTabControl_PolicyMain.TabPages[0].Format("Benefits");

                    #region Temp Need
                    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
                    {

                        column.For(x => x.Type, "LISP", new MetroTextBoxEditor().ReadOnly(true));
                        column.For(x => x.Description, "Policy Type", new MetroTextBoxEditor().ReadOnly(true));
                        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(ReadOnly));
                        //column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(true));
                        column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
                        column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(ReadOnly));
                        //column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
                        // column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
                        //column.For(c => c.FutureAmount, "Retirement Value", new MetroCurrencyEditor().ReadOnly(true));
                        column.For(c => c.Status, "Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.InstructionStatus)).ReadOnly(Action == PolicyAction.UpdateInstruction ? false : ReadOnly));

                    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
                     //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                     ReadOnly: TempNeed.Status == NeedStatus.Cancelled.ToText() ? true : ReadOnly,
                     AllowDelete: false,
                     AllowAddNew: false)
                     .Formatt(ReadOnly);
                    #endregion

                    #region Benefits
                    this.dataGrid_PolicyFunds.Initialise(TempNeed.Benefits, columns =>
                    {
                        columns.For(x => x.Type, "Benefit", new MetroComboListEditor().DataSourceList(Program.listData.List(ListDataItemType.LifeBenefit)).ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                        columns.For(x => x.Description, "Description", new MetroTextBoxEditor().ReadOnly(ReadOnly));
                        columns.For(x => x.CoverAmount, "Insured Amount", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                        columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                    }, PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
                      ItemDeleteEventHandler: Benefit_propertyDelete_EventHandler,
                      //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                      ReadOnly: ReadOnly,
                      AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
                      AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
                      .Formatt(ReadOnly);

                    this.dataGrid_Comments.Initialise(TempNeed.Notes, columns =>
                    {
                        columns.For(x => x.CreateDate, "Date", new MetroDateEditor(175).ReadOnly(true));
                        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
                        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
                        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
                    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
                   //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                   ReadOnly: ReadOnly,
                   AllowDelete: true)
                   .Formatt(ReadOnly);
                    #endregion

                    #region Beneficiaries
                    this.dataGrid_Beneficiaries.Initialise<ClientDependent>(TempNeed.Beneficiaries, column =>
                    {
                        column.For(x => x.DependentName, "Name", new MetroComboBoxEditor().DataSourceList(GetClientDependents().ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                        column.For(c => c.DependentType, "Relation", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.DependentType)));
                        column.For(c => c.BirthDate, "Birth Date", new MetroDateEditor());
                        column.For(c => c.IdNumber, "Id Number", new MetroSAIDEditor());
                        column.For(c => c.Percentage, "Percentage %", new MetroPercentageEditor());
                        column.For(c => c._Age, "Age", new MetroTextBoxEditor().ReadOnly(true));
                    }
                    , ReadOnly: ReadOnly, AllowDelete: true, PropertyChangedHandler: Beneficiary_propertyChanged_EventHandler
                    ).Formatt(ReadOnly);
                    #endregion

                    #region Notes
                    this.dataGrid_Comments.Initialise(TempNeed.Notes, columns =>
                    {
                        columns.For(x => x.CreateDate, "Date", new MetroDateEditor(175).ReadOnly(true));
                        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
                        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor().ReadOnly(ReadOnly));
                        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(130).ReadOnly(true));
                    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
                    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                    ReadOnly: ReadOnly,
                    AllowDelete: true)
                    .Formatt(ReadOnly);
                    #endregion

                    break;
                case NeedTypes.RiskNeed:
                    this.metroTabControl_PolicyMain.TabPages[0].Format("Benefits");

                    #region Temp Need
                    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
                    {

                        column.For(x => x.Type, "Risk Insurer", new MetroTextBoxEditor().ReadOnly(true));
                        column.For(x => x.Insured, "Insured", new MetroTextBoxEditor().ReadOnly(true));
                        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(ReadOnly));
                        column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                        column.For(c => c.FutureAmount, "Cover Amount", new MetroCurrencyEditor().ReadOnly(ReadOnly));

                        column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(ReadOnly));
                        //column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
                        // column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
                        //column.For(c => c.FutureAmount, "Retirement Value", new MetroCurrencyEditor().ReadOnly(true));
                        column.For(c => c.Status, "Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.InstructionStatus)).ReadOnly(Action == PolicyAction.UpdateInstruction ? false : ReadOnly));

                    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
                     //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                     ReadOnly: TempNeed.Status == NeedStatus.Cancelled.ToText() ? true : ReadOnly,
                     AllowDelete: false,
                     AllowAddNew: false)
                     .Formatt(ReadOnly);
                    #endregion

                    #region Benefits
                    this.dataGrid_PolicyFunds.Initialise(TempNeed.Benefits, columns =>
                    {
                        columns.For(x => x.Type, "Benefit", new MetroComboListEditor().DataSourceList(Program.listData.List(ListDataItemType.LifeBenefit)).ReadOnly(Action != PolicyAction.UpdateRiskFna ? true : ReadOnly));
                        columns.For(x => x.Description, "Description", new MetroTextBoxEditor().ReadOnly(ReadOnly));
                        columns.For(x => x.CoverAmount, "Insured Amount", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                        columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                    }, PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
                      ItemDeleteEventHandler: Benefit_propertyDelete_EventHandler,
                      //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                      ReadOnly: ReadOnly,
                      AllowAddNew: Action == PolicyAction.UpdateRiskFna ? true : false,
                      AllowDelete: Action == PolicyAction.UpdateRiskFna ? true : false)
                      .Formatt(ReadOnly);

                    // this.dataGrid_Comments.Initialise(RiskCoverNeed.Notes, columns =>
                    // {
                    //     columns.For(x => x.CreateDate, "Date", new MetroDateEditor(175).ReadOnly(true));
                    //     //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
                    //     columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
                    //     columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
                    // }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
                    ////ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                    //ReadOnly: ReadOnly,
                    //AllowDelete: true)
                    //.Formatt(ReadOnly);
                    #endregion

                    #region Beneficiaries
                    this.dataGrid_Beneficiaries.Initialise<ClientDependent>(TempNeed.Beneficiaries, column =>
                    {
                        column.For(x => x.DependentName, "Name", new MetroComboBoxEditor().DataSourceList(GetClientDependents().ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                        column.For(c => c.DependentType, "Relation", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.DependentType)));
                        column.For(c => c.BirthDate, "Birth Date", new MetroDateEditor());
                        column.For(c => c.IdNumber, "Id Number", new MetroSAIDEditor());
                        column.For(c => c.Percentage, "Percentage %", new MetroPercentageEditor());
                        column.For(c => c._Age, "Age", new MetroTextBoxEditor().ReadOnly(true));
                    }
                    , ReadOnly: ReadOnly, AllowDelete: true, PropertyChangedHandler: Beneficiary_propertyChanged_EventHandler
                    ).Formatt(ReadOnly);
                    #endregion

                    #region Notes
                    this.dataGrid_Comments.Initialise(TempNeed.Notes, columns =>
                    {
                        columns.For(x => x.CreateDate, "Date", new MetroDateEditor(175).ReadOnly(true));
                        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
                        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor().ReadOnly(ReadOnly));
                        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(130).ReadOnly(true));
                    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
                    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                    ReadOnly: ReadOnly,
                    AllowDelete: true)
                    .Formatt(ReadOnly);
                    #endregion
                    break;
                case NeedTypes.MedicalNeed:

                    TempNeed.Initialise();
                    TempNeed.PropertyChanged -= Need_PropertyChanged;

                    //TempNeed.Calculate();

                    this.metroTabControl_PolicyMain.TabPages[0].Format("Benefits");

                    #region Temp Need
                    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
                    {
                        column.For(x => x.Type, "LISP", new MetroTextBoxEditor().ReadOnly(true));
                        column.For(x => x.Description, "Plan", new MetroTextBoxEditor().ReadOnly(true));
                        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(ReadOnly));
                        // column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(true));
                        column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
                        column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(ReadOnly));
                        //column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
                        // column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
                        //column.For(c => c.FutureAmount, "Retirement Value", new MetroCurrencyEditor().ReadOnly(true));
                        column.For(c => c.Status, "Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.InstructionStatus)).ReadOnly(Action == PolicyAction.UpdateInstruction ? false : ReadOnly));

                    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
                     //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                     ReadOnly: TempNeed.Status == NeedStatus.Cancelled.ToText() ? true : ReadOnly,
                     AllowDelete: false,
                     AllowAddNew: false)
                     .Formatt(ReadOnly);

                    #endregion

                    #region Benefits
                    this.dataGrid_PolicyFunds.Initialise(TempNeed.Benefits, columns =>
                    {
                        columns.For(x => x.Description, "Benefit Type", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                        columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
                        //columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                        //columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                        //columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
                        columns.For(x => x.CoverAmount, "Cover Amount", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                        columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                    }, PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
                       ItemDeleteEventHandler: Benefit_propertyDelete_EventHandler,
                       //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                       ReadOnly: ReadOnly,
                        AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
                       AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
                       .Formatt(ReadOnly);
                    #endregion

                    #region Beneficiaries
                    this.dataGrid_Beneficiaries.Initialise<ClientDependent>(TempNeed.Beneficiaries, column =>
                    {
                        column.For(x => x.DependentName, "Name", new MetroComboBoxEditor().DataSourceList(GetClientDependents().ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                        column.For(c => c.DependentType, "Relation", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.DependentType)));
                        column.For(c => c.BirthDate, "Birth Date", new MetroDateEditor());
                        column.For(c => c.IdNumber, "Id Number", new MetroSAIDEditor());
                        column.For(c => c.Percentage, "Percentage %", new MetroPercentageEditor());
                        column.For(c => c._Age, "Age", new MetroTextBoxEditor().ReadOnly(true));
                    }
                    , ReadOnly: ReadOnly, AllowDelete: true, PropertyChangedHandler: Beneficiary_propertyChanged_EventHandler
                    ).Formatt(ReadOnly);
                    #endregion

                    #region Notes
                    this.dataGrid_Comments.Initialise(TempNeed.Notes, columns =>
                    {
                        columns.For(x => x.CreateDate, "Date", new MetroDateEditor(175).ReadOnly(true));
                        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
                        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
                        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
                    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
                    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                    ReadOnly: ReadOnly,
                    AllowDelete: true)
                    .Formatt(ReadOnly);
                    #endregion

                    TempNeed.PropertyChanged += Need_PropertyChanged;

                    break;
                default:
                    #region model
                    this.dataGrid_PolicyDetails.Initialise(model, column =>
                    {

                        column.For(x => x.CreateDate, "Amend Dt", new MetroDateEditor(100).ReadOnly(true));
                        column.For(c => c.Type, "Product Type", new MetroTextBoxEditor(200).ReadOnly(true));
                        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor(201).ReadOnly(ReadOnly));
                        column.For(x => x.Insured, "Policy Owner", new MetroComboBoxEditor(120).DataSourceList(GetPolicyOwners().ToListDataItem<ClientDependent>("DependentName", "DependentName")).ReadOnly(true));//Action == PolicyAction.UpdateInstruction ? true : ReadOnly
                        column.For(c => c.InitialAmount, "Deposit/Withdrawls", new MetroCurrencyEditor(150).ReadOnly(true));//Action == PolicyAction.UpdateInstruction ? true : ReadOnly)//Action != PolicyAction.AmendPolicy ? true : ReadOnly
                        column.For(c => c.MonthlyContribution, "Current Premium", new MetroCurrencyEditor(130).ReadOnly(true));//Action != PolicyAction.UpdateFna ? true : ReadOnly  //.ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                        column.For(c => c.NewMonthlyContribution, "New Premium", new MetroCurrencyEditor(130).ReadOnly(ReadOnly));//Action == PolicyAction.UpdateInstruction ? true : ReadOnly
                        column.For(c => c.EscalationPercentage, "Esc %", new MetroPercentageEditor(55).ReadOnly(ReadOnly));//Action != PolicyAction.AmendPolicy && Action != PolicyAction.UpdateFna ? true :
                        //column.For(c => c.GrowthPercentage, "Growth %", new MetroPercentageEditor().ReadOnly(Action== PolicyAction.UpdateInstruction ? true : ReadOnly));// Action != PolicyAction.AmendPolicy && Action != PolicyAction.UpdateFna ? true :
                        column.For(c => c.CurrentAmount, "Policy Value", new MetroCurrencyEditor(130).ReadOnly(true));
                        column.For(c => c.NewPolicyValue, "New Policy Value", new MetroCurrencyEditor().ReadOnly(true));
                        //column.For(c => c.FutureAmount, "Future Value", new MetroCurrencyEditor().ReadOnly(true));
                        column.For(c => c.Status, "Task Status", new MetroComboBoxEditor(100).DataSourceList(Program.listData.List(ListDataItemType.InstructionStatus)).ReadOnly(ReadOnly));//
                        column.For(x => x.UpdateDate, "Update Dt", new MetroDateEditor(100).ReadOnly(true));
                    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
                    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),                   
                    ReadOnly: ReadOnly,
                    AllowDelete: false,
                    AllowAddNew: false)
                    .Formatt(ReadOnly);
                    #endregion

                    #region Funds
                    this.dataGrid_PolicyFunds.Initialise(model.FundsBindingList, columns =>
                    {
                        columns.For(x => x.Description, "Fund Name", new MetroComboListEditor(201).DataSourceList(LispFunds).ReadOnly(Action == PolicyAction.UpdateInstruction ? true : ReadOnly));
                        columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor(100).ReadOnly(ReadOnly));
                        columns.For(x => x.InitialAmount, "Deposit", new MetroCurrencyEditor(130).ReadOnly(ReadOnly));//Action == PolicyAction.UpdateInstruction ? true :
                        columns.For(x => x.WithdrawalAmount, "Withdrawal", new MetroCurrencyEditor(130).ReadOnly(ReadOnly));//Action == PolicyAction.UpdateInstruction ? true : 
                        columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor(60).ReadOnly(ReadOnly));//Action == PolicyAction.UpdateInstruction ? true : 
                        columns.For(x => x.MonthlyContribution, "Split Premium", new MetroCurrencyEditor(120).ReadOnly(true));
                        columns.For(x => x.CurrentAmount, "Fund Value", new MetroCurrencyEditor(130).ReadOnly(ReadOnly));
                        columns.For(x => x.NewFundValue, "New Fund Value", new MetroCurrencyEditor(130).ReadOnly(true));
                        columns.For(x => x.FundValueDate, "Last Update", new MetroDateEditor(100).ReadOnly(true));
                    }, PropertyChangedHandler: Fund_propertyChanged_EventHandler,
                        ItemDeleteEventHandler: Fund_propertyDelete_EventHandler,
                        ReadOnly: ReadOnly,
                        AllowDelete: Action == PolicyAction.UpdateInstruction ? false : true,
                        AllowAddNew: Action == PolicyAction.UpdateInstruction ? false : true)
                        .Formatt(ReadOnly);
                    #endregion

                    #region Comments
                    this.dataGrid_Comments.Initialise(model.Notes, columns =>
                    {
                        columns.For(x => x.CreateDate, "Date Logged", new MetroDateTimeEditor(175).ReadOnly(true));
                        columns.For(x => x.Text, "Comments", new MetroMultiLineTextBoxEditor(255).ReadOnly(false));
                        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(130).ReadOnly(true));
                    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
                     //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientComms, false, Client),
                     ReadOnly: false,//ReadOnly,
                     AllowDelete: true)
                     .Formatt(false);
                    #endregion

                    #region Beneficiaries
                    this.dataGrid_Beneficiaries.Initialise<ClientDependent>(model.BeneficiariesBindingList, column =>
                    {
                        column.For(x => x.DependentName, "Name", new MetroComboBoxEditor().DataSourceList(GetClientDependents().ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                        column.For(c => c.DependentType, "Relation", new MetroComboBoxEditor(130).DataSourceList(Program.listData.List(ListDataItemType.DependentType)));
                        column.For(c => c.BirthDate, "Birth Date", new MetroDateEditor(100));
                        column.For(c => c.IdNumber, "Id Number", new MetroSAIDEditor(130));
                        column.For(c => c.Percentage, "Allocated %", new MetroPercentageEditor(100));
                        column.For(c => c._Age, "Age", new MetroTextBoxEditor(55).ReadOnly(true));
                    }
                    ,
                    ReadOnly: ReadOnly,
                    AllowDelete: true,//Action == PolicyAction.UpdateInstruction ? false : true,
                    AllowAddNew: true,//Action == PolicyAction.UpdateInstruction ? false : true,
                    PropertyChangedHandler: Beneficiary_propertyChanged_EventHandler
                    ).Formatt(ReadOnly);
                    #endregion

                    break;
            }

            model.PropertyChanged += Need_PropertyChanged;
        }

        #endregion

        #region toolStripButton ClickEvents
        //private void toolStripButton_SaveClicked(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        TempNeed.IsLoading = true;
        //        //Force any updates to the grids on lost focus
        //        //  this.dataGrid_PolicyDetails;
        //        //  this.dataGrid_PolicyFunds
        //        this.dataGrid_Comments.EndEditingRow(false);

        //        #region Policy
        //        if (Retirement != null)
        //        {
        //            TempNeed.UpdateNeed(ref Retirement, Action);
        //            UpdatePolicyEvent?.Invoke(Retirement, new EventArgs());
        //        }

        //        if (Investment != null)
        //        {
        //            TempNeed.UpdateNeed(ref Investment, Action);

        //           // Investment.Status= 
        //            Program.Repository.Update<Investment, int>(Investment);
        //            UpdatePolicyEvent?.Invoke(Investment, new EventArgs());

        //        }

        //        if (Education != null)
        //        {
        //            TempNeed.UpdateNeed(ref Education, Action);
        //            UpdatePolicyEvent?.Invoke(Education, new EventArgs());

        //        }

        //        if (Medical != null)
        //        {
        //            TempNeed.UpdateNeed(ref Medical, Action);
        //            UpdatePolicyEvent?.Invoke(Medical, new EventArgs());
        //        }

        //        if (Life != null)
        //        {
        //            TempNeed.UpdateNeed(ref Life, Action);
        //            UpdatePolicyEvent?.Invoke(Life, new EventArgs());
        //        }

        //        if (IncomeAsset != null)
        //        {
        //            TempNeed.UpdateNeed(ref IncomeAsset, Action);
        //            UpdatePolicyEvent?.Invoke(IncomeAsset, new EventArgs());
        //        }
        //        #endregion

        //        #region Needs
        //        if (FnaNeed != null)
        //        {
        //            //TempNeed.UpdateNeed(ref FnaNeed, Action);
        //            UpdateFnaNeedEvent?.Invoke(FnaNeed, new EventArgs());

        //        }

        //        if (InvestmentNeed != null)
        //        {
        //            TempNeed.UpdateNeed(ref InvestmentNeed, Action);
        //            UpdateFnaNeedEvent?.Invoke(InvestmentNeed, new EventArgs());

        //        }

        //        if (EducationNeed != null)
        //        {
        //            TempNeed.UpdateNeed(ref EducationNeed, Action);
        //            UpdateFnaNeedEvent?.Invoke(EducationNeed, new EventArgs());

        //        }
        //        #endregion

        //        #region Instruction
        //        if (Instruction != null && TempNeed != null)
        //        {
        //            Instruction.ReferenceNo = TempNeed.ReferenceNo;
        //            Instruction.ReferenceOwner = TempNeed.Insured;

        //            //set the instruction status
        //            switch (TempNeed.Status.ToEnum<NeedStatus>())
        //            {
        //                case NeedStatus.Pending:
        //                case NeedStatus.Advised:
        //                case NeedStatus.Accepted:
        //                    break;
        //                case NeedStatus.InProgress:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateInProgress.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddInProgress.ToText();
        //                    break;
        //                case NeedStatus.Submitted:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateSubmitted.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddSubmitted.ToText();
        //                    break;
        //                case NeedStatus.ReadyForAuth:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateReadyForAuth.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddReadyForAuth.ToText();
        //                    break;
        //                case NeedStatus.Authorised:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateAuthorised.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddAuthorised.ToText();
        //                    break;
        //                case NeedStatus.Completed:
        //                case NeedStatus.Implemented:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateCompleted.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddCompleted.ToText();

        //                    TempNeed.IsImplemented = true;
        //                    TempNeed.IsCancelled = false;

        //                    break;
        //                case NeedStatus.Cancelled:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateCancelled.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddCancelled.ToText();

        //                    TempNeed.IsImplemented = false;
        //                    TempNeed.IsCancelled = true;

        //                    break;
        //            }

        //            UpdateNeedEvent?.Invoke(TempNeed, new EventArgs());

        //            UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());

        //        }
        //        #endregion

        //        //Successfully Saved
        //        HasChanges = false;
        //    }
        //    catch (Exception x)
        //    {
        //        MessageBoxExt.ShowException(x);
        //    }
        //    finally
        //    {
        //        this.dataGrid_PolicyDetails.Refresh();
        //        this.dataGrid_PolicyFunds.Refresh();
        //        //this.dataGrid_PolicyNotes.Refresh();

        //        UpdateToolBar();

        //        TempNeed.IsLoading = false;

        //        this.xToolBarMenu1.SetEditMode(false);
        //    }
        //}
        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            try
            {
                TempNeed.IsLoading = true;

                if (TempNeed.Id == 0)
                    Program.Repository.Add<Need, int>(TempNeed);

                //Update the TempNeed status
                switch (TempNeed.Status.ToEnum<NeedStatus>())
                {
                    case NeedStatus.Completed:
                    case NeedStatus.Implemented:
                        TempNeed.IsImplemented = true;
                        TempNeed.IsCancelled = false;
                        break;
                    case NeedStatus.Cancelled:
                        TempNeed.IsImplemented = false;
                        TempNeed.IsCancelled = true;
                        break;
                    default:
                        TempNeed.IsImplemented = false;
                        TempNeed.IsCancelled = false;

                        if (Retirement != null)
                        {
                            Retirement.ReferenceId = TempNeed.Id;
                            Retirement.Status = TempNeed.Status;

                            Program.Repository.Update<Retirement, int>(Retirement);

                        }

                        if (Investment != null)
                        {
                            Investment.ReferenceId = TempNeed.Id;
                            Investment.Status = TempNeed.Status;

                            Program.Repository.Update<Investment, int>(Investment);

                        }

                        if (Education != null)
                        {
                            Education.ReferenceId = TempNeed.Id;
                            Education.Status = TempNeed.Status;

                            Program.Repository.Update<Education, int>(Education);

                        }

                        if (Medical != null)
                        {
                            Medical.ReferenceId = TempNeed.Id;
                            Medical.Status = TempNeed.Status;

                            Program.Repository.Update<Medical, int>(Medical);

                        }

                        if (Life != null)
                        {
                            Life.ReferenceId = TempNeed.Id;
                            Life.Status = TempNeed.Status;

                            Program.Repository.Update<Life, int>(Life);

                        }

                        if (IncomeAsset != null)
                        {
                            IncomeAsset.ReferenceId = TempNeed.Id;
                            IncomeAsset.Status = TempNeed.Status;

                            Program.Repository.Update<IncomeAsset, int>(IncomeAsset);

                        }
                        break;
                };

                //Update the instruction
                if (Instruction != null)
                {
                    //set the instruction status
                    switch (TempNeed.Status.ToEnum<NeedStatus>())
                    {
                        case NeedStatus.Pending:
                        case NeedStatus.Advised:
                        case NeedStatus.Accepted:
                            break;
                        case NeedStatus.InProgress:
                            if (TempNeed.IsUpdate)
                                Instruction.Status = InstructionStatus.UpdateInProgress.ToText();
                            else
                                Instruction.Status = InstructionStatus.AddInProgress.ToText();
                            break;
                        case NeedStatus.Submitted:
                            if (TempNeed.IsUpdate)
                                Instruction.Status = InstructionStatus.UpdateSubmitted.ToText();
                            else
                                Instruction.Status = InstructionStatus.AddSubmitted.ToText();
                            break;
                        case NeedStatus.ReadyForAuth:
                            if (TempNeed.IsUpdate)
                                Instruction.Status = InstructionStatus.UpdateReadyForAuth.ToText();
                            else
                                Instruction.Status = InstructionStatus.AddReadyForAuth.ToText();
                            break;
                        case NeedStatus.Authorised:
                            if (TempNeed.IsUpdate)
                                Instruction.Status = InstructionStatus.UpdateAuthorised.ToText();
                            else
                                Instruction.Status = InstructionStatus.AddAuthorised.ToText();
                            break;
                        case NeedStatus.Completed:
                        case NeedStatus.Implemented:
                            if (TempNeed.IsUpdate)
                                Instruction.Status = InstructionStatus.UpdateCompleted.ToText();
                            else
                                Instruction.Status = InstructionStatus.AddCompleted.ToText();
                            break;
                        case NeedStatus.Cancelled:
                            if (TempNeed.IsUpdate)
                                Instruction.Status = InstructionStatus.UpdateCancelled.ToText();
                            else
                                Instruction.Status = InstructionStatus.AddCancelled.ToText();
                            break;
                    }

                    Instruction.ReferenceId = TempNeed.Id;
                    Instruction.ReferenceNo = TempNeed.ReferenceNo;
                    Instruction.ReferenceOwner = TempNeed.Insured;
                    Instruction.InstructionType = Action.ToInstructionType();// InstructionTypeExt.ToType(Instruction.Type);

                    Instruction.UpdateDate = DateTime.Now;
                    Instruction.UpdateBy = Program.User.Username;

                    if (Client != null)
                    {
                        Instruction.Description = Client.ClientDetails.Fullname;
                        Instruction.ClientId = Client.Id;
                    }

                    if (Instruction.Id == 0)
                    {
                        Program.Repository.Add<Instruction, int>(Instruction);
                    }
                    else
                    {
                        Program.Repository.Update<Instruction, int>(Instruction);
                    }

                    //link the need to the instruction
                    TempNeed.InstructionId = Instruction.Id;

                }
                //Successfully Saved
                HasChanges = false;
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                //Update the neeed
                Program.Repository.Update<Need, int>(TempNeed);

                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();
                //this.dataGrid_PolicyNotes.Refresh();

                UpdateToolBar();

                TempNeed.IsLoading = false;

                this.xToolBarMenu1.SetEditMode(false);
            }

        }
        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {
            try
            {

                TempNeed.IsLoading = true;

                TempNeed.Validate();

                switch (xToolBarMenu1.tbEdit.Text)
                {
                    case "Accept":
                        if (MessageBoxExt.ShowQuestion(string.Format("Are you sure you wish to Accept this policy ?")))
                        {
                            TempNeed.Status = NeedStatus.Accepted.ToText();
                            TempNeed.IsCancelled = false;
                        }
                        else return;

                        break;
                    case "Re-open":
                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Re-open this policy ?"))
                        {
                            TempNeed.Status = NeedStatus.Pending.ToText();

                            ReadOnly = false;
                            InitialiseGrid(TempNeed);
                        }
                        else return;
                        break;
                    case "Submit for Authorisation":
                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Submit this policy for Authorisation ?"))
                        {
                            TempNeed.Validate("ReferenceNo");
                            TempNeed.Validate("startdate");

                            TempNeed.Status = NeedStatus.ReadyForAuth.ToText();
                        }
                        else return;
                        break;
                    case "Authorise":
                        if (!Program.User.Designation.Contains("Authoriser") && !Program.User.IsAdministrator)
                            throw new MyValidationException("UnAuthorised . This action can only be performed by a valid Authoriser");

                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Authorise this policy ?"))
                        {
                            TempNeed.Validate("ReferenceNo");
                            TempNeed.Validate("startdate");

                            TempNeed.Status = NeedStatus.Authorised.ToText();
                        }
                        else return;
                        break;
                    case "Implement":
                        //if (!Program.User.Designation.Contains("Authoriser"))
                        //    throw new MyValidationException("UnAuthorised . This action can only be performed by a valid Authoriser");

                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Add this policy to the Client's Portfolio?"))
                        {
                            TempNeed.Validate("ReferenceNo");
                            TempNeed.Validate("startdate");
                           
                            TempNeed.Status = NeedStatus.Implemented.ToText();
                            TempNeed.IsImplemented = true;
                            TempNeed.IsCancelled = false;

                            var args = new EventArgs();

                            if (Client == null)
                                Client = Program.Repository.Get<Client, int>(Instruction.ClientId);

                            if (Client == null)
                                throw new Exception(string.Format("Client with id '{0}' could not be found  ", Instruction.ClientId));

                            switch (TempNeed.NeedType)
                            {
                                case NeedTypes.RetirementNeed:

                                    if (Retirement == null)
                                        Retirement = Client.ClientPortfolio.Retirements.Where(x => x.Id == TempNeed.ReferenceId).FirstOrDefault();

                                    TempNeed.UpdateNeed(ref Retirement, Action);

                                    Retirement.ReferenceId = 0;

                                    if (Retirement.Id == 0)
                                    {
                                        Client.ClientPortfolio.RetirementsBindingList.Add(Retirement);
                                        Program.Repository.Update<Client, int>(Client);
                                    }
                                    else
                                    {
                                        Program.Repository.Update<Retirement, int>(Retirement);
                                    }

                                    break;
                                case NeedTypes.InvestmentNeed:

                                    if (Investment == null)
                                        Investment = Client.ClientPortfolio.Investments.Where(x => x.Id == TempNeed.ReferenceId).FirstOrDefault();

                                    if (InvestmentNeed == null)
                                        InvestmentNeed = TempNeed as InvestmentNeed;

                                    InvestmentNeed.UpdateNeed(ref Investment, Action);

                                    Investment.ReferenceId = 0;

                                    if (Investment.Id == 0)
                                    {
                                        Client.ClientPortfolio.InvestmentsBindingList.Add(Investment);
                                        Program.Repository.Update<Client, int>(Client);
                                    }
                                    else
                                    {
                                        Program.Repository.Update<Investment, int>(Investment);
                                    }

                                    break;
                                case NeedTypes.EducationNeed:

                                    if (Education == null)
                                        Education = Client.ClientPortfolio.Educations.Where(x => x.Id == TempNeed.ReferenceId).FirstOrDefault();

                                    if (EducationNeed == null)
                                        EducationNeed = TempNeed as EducationNeed;

                                    EducationNeed.UpdateNeed(ref Education, Action);

                                    Education.ReferenceId = 0;

                                    if (Education.Id == 0)
                                    {
                                        Client.ClientPortfolio.EducationsBindingList.Add(Education);
                                        Program.Repository.Update<Client, int>(Client);
                                    }
                                    else
                                    {
                                        Program.Repository.Update<Education, int>(Education);
                                    }

                                    break;
                                case NeedTypes.MedicalNeed:

                                    if (Medical == null)
                                        Medical = Client.ClientPortfolio.Medicals.Where(x => x.Id == TempNeed.ReferenceId).FirstOrDefault();

                                    TempNeed.UpdateNeed(ref Medical, Action);

                                    Medical.ReferenceId = 0;

                                    if (Medical.Id == 0)
                                    {
                                        Client.ClientPortfolio.MedicalsBindingList.Add(Medical);
                                        Program.Repository.Update<Client, int>(Client);
                                    }
                                    else
                                    {
                                        Program.Repository.Update<Medical, int>(Medical);
                                    }

                                    break;
                                case NeedTypes.LifeDisabilityNeed:

                                    if (Life == null)
                                        Life = Client.ClientPortfolio.Lifes.Where(x => x.Id == TempNeed.ReferenceId).FirstOrDefault();

                                    TempNeed.UpdateNeed(ref Life, Action);

                                    Life.ReferenceId = 0;

                                    if (Life.Id == 0)
                                    {
                                        Client.ClientPortfolio.LifesBindingList.Add(Life);
                                        Program.Repository.Update<Client, int>(Client);
                                    }
                                    else
                                    {
                                        Program.Repository.Update<Life, int>(Life);
                                    }

                                    break;
                                case NeedTypes.IncomeAssetNeed:

                                    if (IncomeAsset == null)
                                        IncomeAsset = Client.ClientPortfolio.IncomeAssets.Where(x => x.Id == TempNeed.ReferenceId).FirstOrDefault();

                                    TempNeed.UpdateNeed(ref IncomeAsset, Action);

                                    IncomeAsset.ReferenceId = 0;

                                    if (IncomeAsset.Id == 0)
                                    {
                                        Client.ClientPortfolio.IncomeAssetsBindingList.Add(IncomeAsset);
                                        Program.Repository.Update<Client, int>(Client);
                                    }
                                    else
                                    {
                                        Program.Repository.Update<IncomeAsset, int>(IncomeAsset);
                                    }

                                    break;
                                case NeedTypes.RiskNeed:

                                    if (Life == null)
                                        Life = Client.ClientPortfolio.Lifes.Where(x => x.Id == RiskCoverNeed.ReferenceId).FirstOrDefault();

                                    RiskCoverNeed.UpdateNeed(ref Life, Action);

                                    Life.ReferenceId = 0;

                                    if (Life.Id == 0)
                                    {
                                        Client.ClientPortfolio.LifesBindingList.Add(Life);
                                        Program.Repository.Update<Client, int>(Client);
                                    }
                                    else
                                    {
                                        Program.Repository.Update<Life, int>(Life);
                                    }

                                    break;
                                case NeedTypes.NonRetirementNeed:
                                    break;
                            }

                            ReadOnly = true;
                            InitialiseGrid(TempNeed);
                        }
                        else
                            return;

                        break;

                    default:
                        throw new Exception("Unknown Edit Action : " + xToolBarMenu1.tbEdit.Text);
                }

                //Add the Temp Need
                //if (TempNeed.Id == 0)
                //    Program.Repository.Add<Need, int>(TempNeed);

                //Add the Instruction
                if (Instruction == null && TempNeed.InstructionId == 0)
                {
                    Instruction = new Instruction()
                    {
                        Status = InstructionStatus.AddPending.ToText(),
                        Type = Action.ToInstructionType().ToText(), // TO DO: determine the type of policy instruction
                        InstructionType = Action.ToInstructionType(),// InstructionTypeExt.ToType(InstructionType.POLICY_CREATE.ToText()),
                        TaskName = TempNeed.Description,
                        ReferenceId = TempNeed.Id,
                        ReferenceNo = TempNeed.ReferenceNo,
                        ReferenceOwner = TempNeed.Insured,
                        ClientId = Client == null ? 0 : Client.Id,
                        UpdateDate = DateTime.Now,
                        UpdateBy = Program.User.Username
                    };

                    //Add the Admin Task
                    Program.Repository.Add<Instruction, int>(Instruction);
                }


                HasChanges = false;

            }
            catch (MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                //Save
                toolStripButton_Save_Click(sender, e);

            }


        }
        private void toolStripButton_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                TempNeed.IsLoading = true;

                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Cancel this task ?"))
                {
                    TempNeed.Status = NeedStatus.Cancelled.ToText();
                    TempNeed.IsImplemented = false;
                    TempNeed.IsCancelled = true;

                    //Save the Changes
                    toolStripButton_Save_Click(sender, e);

                    ReadOnly = true;

                    InitialiseGrid(TempNeed);

                }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }
            finally
            {

                TempNeed.IsLoading = false;

                this.xToolBarMenu1.SetEditMode(false);
            }

        }
        private void toolStripButton_EmailSMS_Click(object sender, EventArgs e)
        {
            try
            {
                List<ClientDetailsView> clientDetailsView = Program.ClientDetailsService.ListView(x => x.ClientId == Client.Id).ToList();

                if (clientDetailsView != null)
                {
                    var frm = new frmClientCommunication(clientDetailsView, CommunicationAction.SendAll);
                    frm.ShowDialog(this);
                }
                else
                {
                    throw new Exception("Invalid client...please select a valid client");
                }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }
            finally
            {

            }

        }

        //The toolbar Edit button
        //private void toolStripButton_Amend_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        TempNeed.Validate();
        //        TempNeed.IsLoading = true;
        //        switch (xToolBarMenu1.tbEdit.Text)
        //        {
        //            case "Accept":
        //                if (MessageBoxExt.ShowQuestion(string.Format("Are you sure you wish to Accept changes to this policy ?")))
        //                {
        //                    #region Accept
        //                    TempNeed.Status = NeedStatus.Accepted.ToText();
        //                    TempNeed.IsCancelled = false;

        //                    string instructionComment;

        //                    switch (TempNeed.NeedType)
        //                    {
        //                        case NeedTypes.RetirementNeed:
        //                            UpdatePolicyNeedEvent?.Invoke(Retirement, TempNeed, new EventArgs());
        //                            instructionComment = TempNeed.Description;
        //                            break;
        //                        case NeedTypes.InvestmentNeed:
        //                            UpdatePolicyNeedEvent?.Invoke(Investment, TempNeed, new EventArgs());
        //                            instructionComment = TempNeed.Description;
        //                            break;
        //                        case NeedTypes.EducationNeed:
        //                            UpdatePolicyNeedEvent?.Invoke(Education, TempNeed, new EventArgs());
        //                            instructionComment = TempNeed.Description;
        //                            break;
        //                        case NeedTypes.MedicalNeed:
        //                            UpdatePolicyNeedEvent?.Invoke(Medical, TempNeed, new EventArgs());
        //                            instructionComment = TempNeed.Type;
        //                            break;
        //                        case NeedTypes.LifeDisabilityNeed:
        //                            UpdatePolicyNeedEvent?.Invoke(Life, TempNeed, new EventArgs());
        //                            instructionComment = TempNeed.Type;
        //                            break;
        //                        case NeedTypes.IncomeAssetNeed:
        //                            UpdatePolicyNeedEvent?.Invoke(IncomeAsset, TempNeed, new EventArgs());
        //                            instructionComment = TempNeed.Type;
        //                            break;
        //                        default:
        //                            throw new Exception("Unknown or missing Need Type");
        //                    }
        //                    #endregion

        //                    #region Instruction
        //                    if (Instruction == null)
        //                    {
        //                        Instruction = new Instruction()
        //                        {
        //                            Status = InstructionStatus.UpdatePending.ToText(),
        //                            Type = InstructionType.POLICY_UPDATE.ToText(),
        //                            InstructionType = InstructionType.POLICY_UPDATE,
        //                            Comment = instructionComment,
        //                            ReferenceId = TempNeed.Id,
        //                            ReferenceNo = TempNeed.ReferenceNo,
        //                            ReferenceOwner = TempNeed.Insured,
        //                        UpdateDate = DateTime.Now,
        //                            UpdateBy = Program.User.Username
        //                        };

        //                        //Add the Admin Task
        //                        AddClientInstructionEvent?.Invoke(Instruction, new EventArgs());
        //                    }
        //                    else
        //                    {
        //                        Instruction.Status = InstructionStatus.UpdatePending.ToText();
        //                        Instruction.Comment = instructionComment;
        //                        Instruction.ReferenceId = TempNeed.Id;
        //                        Instruction.ReferenceNo = TempNeed.ReferenceNo;

        //                        Instruction.UpdateDate = DateTime.Now;
        //                        Instruction.UpdateBy = Program.User.Username;

        //                        //Update the Admin Task
        //                        UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());
        //                    }

        //                    TempNeed.InstructionId = Instruction.Id;
        //                    #endregion
        //                }
        //                break;
        //            case "Re-open":
        //                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Re-open this policy ?"))
        //                {
        //                    TempNeed.Status = NeedStatus.Pending.ToText();

        //                    //Disable ReadOnly
        //                    ReadOnly = false;
        //                    InitialiseGrid(TempNeed);
        //                }
        //                break;
        //            case "Submit for Authorisation":
        //                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Submit this policy for Authorisation ?"))
        //                {
        //                    TempNeed.Validate("ReferenceNo");
        //                    TempNeed.Validate("startdate");

        //                    TempNeed.Status = NeedStatus.ReadyForAuth.ToText();
        //                }
        //                break;
        //            case "Authorise":
        //                if (!Program.User.Designation.Contains("Authoriser"))
        //                    throw new MyValidationException("UnAuthorised . This action can only be performed by a valid Authoriser");

        //                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Authorise this policy ?"))
        //                {
        //                    TempNeed.Validate("ReferenceNo");
        //                    TempNeed.Validate("startdate");

        //                    TempNeed.Status = NeedStatus.Authorised.ToText();
        //                }
        //                break;
        //            case "Implement":
        //                //if (!Program.User.Designation.Contains("Authoriser"))
        //                //    throw new MyValidationException("UnAuthorised . This action can only be performed by a valid Authoriser");

        //                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Implement changes to this policy in the Client's Portfolio ?"))
        //                {
        //                    TempNeed.Validate("ReferenceNo");
        //                    TempNeed.Validate("startdate");

        //                    #region Implement
        //                    TempNeed.Status = NeedStatus.Implemented.ToText();
        //                    TempNeed.IsImplemented = true;
        //                    TempNeed.IsCancelled = false;

        //                    switch (TempNeed.NeedType)
        //                    {
        //                        case NeedTypes.RetirementNeed:
        //                            TempNeed.UpdateNeed(ref Retirement, Action);

        //                            //if(Retirement.Id==0)
        //                            //    Retirement.Amendments.Add(TempNeed);

        //                            Retirement.ReferenceId = 0;
        //                            UpdateClientPortfoilioEvent?.Invoke(Retirement,Instruction, new EventArgs());
        //                            break;
        //                        case NeedTypes.InvestmentNeed:
        //                            TempNeed.UpdateNeed(ref Investment, Action);

        //                            //if (Retirement.Id == 0)
        //                            //    Investment.Amendments.Add(TempNeed);

        //                            Investment.ReferenceId = 0;
        //                            UpdateClientPortfoilioEvent?.Invoke(Investment, Instruction, new EventArgs());
        //                            break;
        //                        case NeedTypes.EducationNeed:
        //                            TempNeed.UpdateNeed(ref Education, Action);

        //                            //if (Retirement.Id == 0)
        //                            //    Education.Amendments.Add(TempNeed);

        //                            Education.ReferenceId = 0;
        //                            UpdateClientPortfoilioEvent?.Invoke(Education, Instruction, new EventArgs());
        //                            break;
        //                        case NeedTypes.MedicalNeed:
        //                            TempNeed.UpdateNeed(ref Medical, Action);
        //                            //if (Retirement.Id == 0)
        //                            //    Medical.Amendments.Add(TempNeed);

        //                            Medical.ReferenceId = 0;
        //                            UpdateClientPortfoilioEvent?.Invoke(Medical, Instruction, new EventArgs());
        //                            break;
        //                        case NeedTypes.LifeDisabilityNeed:
        //                            TempNeed.UpdateNeed(ref Life, Action);
        //                            //if (Retirement.Id == 0)
        //                            //    Life.Amendments.Add(TempNeed);

        //                            Life.ReferenceId = 0;
        //                            UpdateClientPortfoilioEvent?.Invoke(Life, Instruction, new EventArgs());
        //                            break;
        //                        case NeedTypes.IncomeAssetNeed:
        //                            TempNeed.UpdateNeed(ref IncomeAsset, Action);
        //                            //if (Retirement.Id == 0)
        //                            //    IncomeAsset.Amendments.Add(TempNeed);

        //                            IncomeAsset.ReferenceId = 0;
        //                            UpdateClientPortfoilioEvent?.Invoke(IncomeAsset, Instruction, new EventArgs());
        //                            break;
        //                        case NeedTypes.NonRetirementNeed:
        //                            break;
        //                    }
        //                    #endregion

        //                    ReadOnly = true;
        //                    InitialiseGrid(TempNeed);
        //                }
        //                break;
        //            default:
        //                throw new Exception("Unknown Amend Action : " + xToolBarMenu1.tbEdit.Text);
        //        }

        //        #region Instruction
        //        if (Instruction != null && TempNeed != null)
        //        {
        //            //set the instruction status
        //            switch (TempNeed.Status.ToEnum<NeedStatus>())
        //            {
        //                case NeedStatus.Pending:
        //                case NeedStatus.Advised:
        //                case NeedStatus.Accepted:
        //                    break;
        //                case NeedStatus.InProgress:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateInProgress.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddInProgress.ToText();
        //                    break;
        //                case NeedStatus.Submitted:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateSubmitted.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddSubmitted.ToText();
        //                    break;
        //                case NeedStatus.ReadyForAuth:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateReadyForAuth.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddReadyForAuth.ToText();
        //                    break;
        //                case NeedStatus.Authorised:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateAuthorised.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddAuthorised.ToText();
        //                    break;
        //                case NeedStatus.Completed:
        //                case NeedStatus.Implemented:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateCompleted.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddCompleted.ToText();

        //                    TempNeed.IsImplemented = true;
        //                    TempNeed.IsCancelled = false;

        //                    break;
        //                case NeedStatus.Cancelled:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateCancelled.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddCancelled.ToText();

        //                    TempNeed.IsImplemented = false;
        //                    TempNeed.IsCancelled = true;

        //                    break;
        //            }

        //             UpdateNeedEvent?.Invoke(TempNeed, new EventArgs());

        //            UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());
        //        }
        //        #endregion

        //        HasChanges = false;
        //    }
        //    catch (MyValidationException vx)
        //    {
        //        MessageBoxExt.ShowWarning(vx.Message);
        //    }
        //    catch (Exception x)
        //    {
        //        MessageBoxExt.ShowException(x);
        //    }
        //    finally
        //    {
        //        this.dataGrid_PolicyDetails.Refresh();
        //        this.dataGrid_PolicyFunds.Refresh();
        //        //this.dataGrid_PolicyNotes.Refresh();

        //        UpdateToolBar();

        //        TempNeed.IsLoading = false;
        //    }


        //}
        //private void toolStripButton_AmendCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        TempNeed.IsLoading = true;
        //        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Cancel this Amendment ?"))
        //        {
        //            TempNeed.Status = NeedStatus.Cancelled.ToText();
        //            TempNeed.IsImplemented = false;
        //            TempNeed.IsCancelled = true;


        //            //Save the Changes
        //            xToolBarMenu1.tbSave_Click(sender, e);

        //            //Make the grid readonly
        //            ReadOnly = true;
        //            InitialiseGrid(TempNeed);
        //        }
        //    }
        //    catch (Exception x)
        //    {
        //        MessageBoxExt.ShowException(x);
        //    }
        //    finally { TempNeed.IsLoading = false; }


        //}
        //private void toolStripButton_AmendSave_Click(object sender, EventArgs e) {

        //    try {

        //        TempNeed.IsLoading = true;

        //        //Update the TempNeed status
        //        switch (TempNeed.Status.ToEnum<NeedStatus>())
        //        {
        //            case NeedStatus.Pending:
        //                //TempNeed.Status = NeedStatus.Accepted.ToText();

        //                TempNeed.IsImplemented = false;
        //                TempNeed.IsCancelled = false;
        //                break;
        //            case NeedStatus.Completed:
        //            case NeedStatus.Implemented:
        //                //TempNeed.Status = NeedStatus.Implemented.ToText();

        //                TempNeed.IsImplemented = true;
        //                TempNeed.IsCancelled = false;
        //                break;
        //            case NeedStatus.Cancelled:

        //                //TempNeed.Status = NeedStatus.Cancelled.ToText();

        //                TempNeed.IsImplemented = false;
        //                TempNeed.IsCancelled = true;
        //                break;
        //        };

        //        if (TempNeed.Id == 0)
        //        {
        //            Program.Repository.Add<Need, int>(TempNeed);
        //        }
        //        else
        //        {
        //            Program.Repository.Update<Need, int>(TempNeed);
        //        }

        //        if (Retirement != null)
        //        {
        //            Retirement.ReferenceId = TempNeed.Id;
        //            Retirement.Status = TempNeed.Status;

        //            Program.Repository.Update<Retirement, int>(Retirement);

        //            //UpdatePolicyNeedEvent?.Invoke(Retirement, TempNeed, new EventArgs());
        //        }

        //        if (Investment != null)
        //        {
        //            Investment.ReferenceId = TempNeed.Id;
        //            Investment.Status = TempNeed.Status;

        //            Program.Repository.Update<Investment, int>(Investment);

        //            // UpdatePolicyNeedEvent?.Invoke(Investment, TempNeed, new EventArgs());
        //        }

        //        if (Education != null)
        //        {
        //            Education.ReferenceId = TempNeed.Id;
        //            Education.Status = TempNeed.Status;

        //            Program.Repository.Update<Education, int>(Education);

        //            //UpdatePolicyNeedEvent?.Invoke(Education, TempNeed, new EventArgs());
        //        }

        //        if (Medical != null)
        //        {
        //            Medical.ReferenceId = TempNeed.Id;
        //            Medical.Status = TempNeed.Status;

        //            Program.Repository.Update<Medical, int>(Medical);

        //            //UpdatePolicyNeedEvent?.Invoke(Medical, TempNeed, new EventArgs());
        //        }

        //        if (Life != null)
        //        {
        //            Life.ReferenceId = TempNeed.Id;
        //            Life.Status = TempNeed.Status;

        //            Program.Repository.Update<Life, int>(Life);

        //           // UpdatePolicyNeedEvent?.Invoke(Life, TempNeed, new EventArgs());
        //        }

        //        if (IncomeAsset != null)
        //        {
        //            IncomeAsset.ReferenceId = TempNeed.Id;
        //            IncomeAsset.Status = TempNeed.Status;

        //            Program.Repository.Update<IncomeAsset, int>(IncomeAsset);

        //            //UpdatePolicyNeedEvent?.Invoke(IncomeAsset, TempNeed, new EventArgs());
        //        }

        //        if (Instruction != null && TempNeed != null)
        //        {
        //            //set the instruction status
        //            switch (TempNeed.Status.ToEnum<NeedStatus>())
        //            {
        //                case NeedStatus.Pending:
        //                case NeedStatus.Advised:
        //                case NeedStatus.Accepted:
        //                    break;
        //                case NeedStatus.InProgress:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateInProgress.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddInProgress.ToText();
        //                    break;
        //                case NeedStatus.Submitted:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateSubmitted.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddSubmitted.ToText();
        //                    break;
        //                case NeedStatus.ReadyForAuth:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateReadyForAuth.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddReadyForAuth.ToText();
        //                    break;
        //                case NeedStatus.Authorised:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateAuthorised.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddAuthorised.ToText();
        //                    break;
        //                case NeedStatus.Completed:
        //                case NeedStatus.Implemented:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateCompleted.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddCompleted.ToText();
        //                    break;
        //                case NeedStatus.Cancelled:
        //                    if (TempNeed.IsUpdate)
        //                        Instruction.Status = InstructionStatus.UpdateCancelled.ToText();
        //                    else
        //                        Instruction.Status = InstructionStatus.AddCancelled.ToText();
        //                    break;
        //            }

        //            Instruction.ReferenceNo = TempNeed.ReferenceNo;
        //            Instruction.ReferenceOwner = TempNeed.Insured;
        //            Instruction.Description = Client.ClientDetails.Fullname;
        //            Instruction.ClientId = Client.Id;
        //            Instruction.UpdateDate = DateTime.Now;
        //            Instruction.UpdateBy = Program.User.Username;

        //            if (Instruction.Id == 0)
        //            {
        //                Program.Repository.Add<Instruction, int>(Instruction);
        //            }
        //            else
        //            {
        //                Program.Repository.Update<Instruction, int>(Instruction);
        //            }
        //        }

        //        //Successfully Saved
        //        HasChanges = false;
        //    }
        //    catch (Exception x)
        //    {
        //        MessageBoxExt.ShowException(x);
        //    }
        //    finally
        //    {
        //        this.dataGrid_PolicyDetails.Refresh();
        //        this.dataGrid_PolicyFunds.Refresh();
        //        //this.dataGrid_PolicyNotes.Refresh();

        //        UpdateToolBar();

        //        TempNeed.IsLoading = false;
        //    }

        //}

        private void XToolBarMenu1_Close_Click(object sender, EventArgs e)
        {
            if (HasChanges)
                if (MessageBoxExt.ShowQuestion("Do you wish to Save changes?"))
                    toolStripButton_Save_Click(sender, e);

            this.Close();
        }

        #endregion

        #region Model PropertyChanged Event Handlers
        private void Need_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                HasChanges = true;
            }
            catch (Exception x) { }
            finally
            {
                UpdateToolBar();
            };
        }
        #endregion

        #region DataGrid EventHandlers
        private void Need_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            try
            {
                HasChanges = true;
            }
            catch (Exception x)
            {

            }
            finally
            {
                dataGrid_PolicyFunds.Refresh();

                UpdateToolBar();

            }
        }

        private void Note_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            try
            {
                //DevAge.ComponentModel.BoundList<Note> _bList = sender as DevAge.ComponentModel.BoundList<Note>;
                //Note mObj = _bList.EditedObject as Note;

                HasChanges = true;
            }
            catch (Exception x)
            {

            }
            finally
            {

                dataGrid_Comments.Refresh();
                UpdateToolBar();
            }

        }

        private void Fund_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {

            try
            {
                //DevAge.ComponentModel.BoundList<Fund> _bList = sender as DevAge.ComponentModel.BoundList<Fund>;
                //Fund mObj = _bList.EditedObject as Fund;
                Console.WriteLine("High am me");
                HasChanges = true;
            }
            catch (Exception x)
            {

            }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();

                UpdateToolBar();


            }


        }

        private void Fund_propertyDelete_EventHandler(object sender, EventArgs e)
        {

            try
            {
                CellContext context = (CellContext)sender;

                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this Fund?"))
                {

                    DataGrid dg = context.Grid as DataGrid;
                    DevAge.ComponentModel.BoundList<Fund> _bList = dg.DataSource as DevAge.ComponentModel.BoundList<Fund>;
                    Fund fund = _bList[context.Position.Row - 1] as Fund;

                    TempNeed.Funds.Remove(fund);

                    HasChanges = true;
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                this.dataGrid_PolicyFunds.Refresh();
                UpdateToolBar();
            }

        }

        private void Benefit_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {

            try
            {

                //DevAge.ComponentModel.BoundList<Benefit> _bList = sender as DevAge.ComponentModel.BoundList<Benefit>;
                //Benefit mObj = _bList.EditedObject as Benefit;

                HasChanges = true;

            }
            catch (Exception x)
            {

            }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();

                UpdateToolBar();
            }


        }

        private void Benefit_propertyDelete_EventHandler(object sender, EventArgs e)
        {

            try
            {
                CellContext context = (CellContext)sender;

                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this Benefit?"))
                {

                    DataGrid dg = context.Grid as DataGrid;
                    DevAge.ComponentModel.BoundList<Benefit> _bList = dg.DataSource as DevAge.ComponentModel.BoundList<Benefit>;
                    Benefit benefit = _bList[context.Position.Row - 1] as Benefit;

                    TempNeed.Benefits.Remove(benefit);

                    HasChanges = true;
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                this.dataGrid_PolicyFunds.Refresh();
                UpdateToolBar();
            }

        }

        private void Beneficiary_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            HasChanges = true;
            try
            {
                if (e.PropertyDescriptor.Name == "DependentName")
                {
                    //Automatically set the BirthDate
                    DevAge.ComponentModel.BoundList<ClientDependent> _bList = sender as DevAge.ComponentModel.BoundList<ClientDependent>;
                    ClientDependent mObj = _bList.EditedObject as ClientDependent;

                    var _Obj = GetClientDependents().Where(x => x.DependentName == mObj.DependentName).FirstOrDefault();
                    if (_Obj != null)
                    {
                        mObj.BirthDate = _Obj.BirthDate;
                        mObj.IdNumber = _Obj.IdNumber;
                        mObj.DependentType = _Obj.DependentType;
                    }


                }

            }
            catch (Exception x)
            {
            }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();
                this.dataGrid_Beneficiaries.Refresh();
                UpdateToolBar();


            }



        }
        #endregion

        #region Delegate Event Handlers

        IList<ClientDependent> GetClientDependents()
        {
            if (Client != null)
                return Client.Beneficiaries;

            return new List<ClientDependent>();
        }

        IList<ClientDependent> GetPolicyOwners()
        {
            if (Client != null)
                return Client.PolicyOwners;

            return new List<ClientDependent>();

        }
        #endregion

        #region History Amendemnts
        void InitialiseGrid(IList<Need> model)
        {
            if (TempNeed == null)
                TempNeed = new Need();

            TempNeed.Status = NeedStatus.Unknown.ToText();
            //Calculate the need
            foreach (var need in model)
            {
                need.CurrentAge = TempNeed.CurrentAge;
                need.InvestmentAge = TempNeed.InvestmentAge;

                need.Calculate();
            }

            #region model
            this.dataGrid_PolicyDetails.Initialise<Need>(model.OrderBy(x => x.CreateDate).ToList(), column =>
            {
                column.For(x => x.CreateDate, "Amend Dt", new MetroDateEditor().ReadOnly(ReadOnly));
                column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(ReadOnly));
                column.For(c => c.InitialAmount, "Deposit/Withdrawls", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                column.For(c => c.MonthlyContribution, "Current Premium", new MetroCurrencyEditor().ReadOnly(ReadOnly));//.ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                column.For(c => c.NewMonthlyContribution, "New Premium", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                column.For(c => c.EscalationPercentage, "Esc %", new MetroPercentageEditor().ReadOnly(ReadOnly));
                column.For(c => c.CurrentAmount, "Policy Value", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                column.For(c => c.NewPolicyValue, "New Policy Value", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                column.For(c => c.FutureAmount, "Future Value", new MetroCurrencyEditor().ReadOnly(true));
                column.For(c => c.Status, "Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(true));//Action != PolicyAction.AmendPolicy ? true : ReadOnly

            },
            ListChangedEventHandler: Need_ListChanged_Event,
            ItemDeleteEventHandler: Need_PropertyDelete_Event,
            PropertyChangedHandler: Need_PropertyChanged_Event,
            RowSelectEventHandler: Need_RowSelectChanged_Event,//for the Policy Funds details view
            //ContextMenu: new DataGridContextMenu(this, ContextMenuType.RetirementPortfolio, ReadOnly, client),
            ReadOnly: ReadOnly,
            AllowDelete: !ReadOnly,
            AllowAddNew: !ReadOnly
            ).Formatt(ReadOnly);
            #endregion


            this.splitContainer1.SplitterDistance = 200;

        }

        private void Need_RowSelectChanged_Event(object sender, RowEventArgs e)
        {
            try
            {
                //CellContext cellContext = (CellContext)sender;
                DevAge.ComponentModel.BoundList<Need> _bList = dataGrid_PolicyDetails.DataSource as DevAge.ComponentModel.BoundList<Need>;
                var need = _bList[e.Row - 1] as Need;

                bool _ReadOnly = ReadOnly;

                #region _ReadOnly
                if (Retirement != null)
                {
                    if (Retirement.ReferenceId == need.Id)
                        _ReadOnly = true;
                }

                if (Investment != null)
                {

                    if (Investment.ReferenceId == need.Id)
                        _ReadOnly = true;
                }

                if (Education != null)
                {

                    if (Education.ReferenceId == need.Id)
                        _ReadOnly = true;
                }

                if (Medical != null)
                {

                    if (Medical.ReferenceId == need.Id)
                        _ReadOnly = true;
                }

                if (Life != null)
                {

                    if (Life.ReferenceId == need.Id)
                        _ReadOnly = true;
                }

                if (IncomeAsset != null)
                {

                    if (IncomeAsset.ReferenceId == need.Id)
                        _ReadOnly = true;
                }
                #endregion

                #region Funds
                this.dataGrid_PolicyFunds.Initialise(need.Funds, columns =>
                {
                    columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(_ReadOnly));
                    columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(_ReadOnly));
                    columns.For(x => x.InitialAmount, "Deposit", new MetroCurrencyEditor().ReadOnly(_ReadOnly));
                    columns.For(x => x.WithdrawalAmount, "Withdrawal", new MetroCurrencyEditor().ReadOnly(_ReadOnly));
                    columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(_ReadOnly));
                    columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
                    columns.For(x => x.CurrentAmount, "Fund Value", new MetroCurrencyEditor().ReadOnly(_ReadOnly));
                    columns.For(x => x.NewFundValue, "New Fund Value", new MetroCurrencyEditor().ReadOnly(_ReadOnly));
                    columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));

                }, ReadOnly: _ReadOnly,
                ItemDeleteEventHandler: Need_Fund_PropertyDelete_Event,
                  AllowDelete: !_ReadOnly,
                  AllowAddNew: !_ReadOnly
                   ).Formatt(_ReadOnly);
                #endregion

                #region Notes
                this.dataGrid_Comments.Initialise(need.Notes, columns =>
                {
                    columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(_ReadOnly));
                    //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
                    columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(_ReadOnly));
                    columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
                },// PropertyChangedHandler: Note_propertyChanged_EventHandler,
                  //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                  ReadOnly: _ReadOnly,
                  AllowDelete: false,
                  AllowAddNew: !_ReadOnly
                   ).Formatt(_ReadOnly);
                #endregion

                #region Beneficiaries
                this.dataGrid_Beneficiaries.Initialise<ClientDependent>(need.Beneficiaries, column =>
                {
                    column.For(x => x.DependentName, "Name", new MetroComboBoxEditor().DataSourceList(GetClientDependents().ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                    column.For(c => c.DependentType, "Relation", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.DependentType)));
                    column.For(c => c.BirthDate, "Birth Date", new MetroDateEditor());
                    column.For(c => c.IdNumber, "Id Number", new MetroSAIDEditor());
                    column.For(c => c.Percentage, "Percentage %", new MetroPercentageEditor());
                    column.For(c => c._Age, "Age", new MetroTextBoxEditor().ReadOnly(true));
                }, ReadOnly: _ReadOnly,
                ItemDeleteEventHandler: Need_Beneficiary_PropertyDelete_Event,
                  AllowDelete: !_ReadOnly,
                  AllowAddNew: !_ReadOnly
                   ).Formatt(_ReadOnly);
                #endregion

                TempNeed = need;
            }
            catch { };
        }
        private void Need_ListChanged_Event(object sender, ListChangedEventArgs e)
        {
            try
            {
                //    CellContext context = (CellContext)sender;
                //    DataGrid dg = context.Grid as DataGrid;
                DevAge.ComponentModel.BoundList<Need> _bList = sender as DevAge.ComponentModel.BoundList<Need>;

                Need need = _bList.EditedObject as Need;

                if (e.ListChangedType == ListChangedType.ItemAdded)
                {
                    need.ReferenceNo = TempNeed.ReferenceNo;
                    need.EscalationPercentage = TempNeed.EscalationPercentage;
                    need.GrowthPercentage = TempNeed.GrowthPercentage;
                    need.Status = "Added";
                    need.InvestmentAge = TempNeed.InvestmentAge;
                    need.CurrentAge = TempNeed.CurrentAge;

                    need.Funds = TempNeed.Funds.ToListObject<Fund, Fund>();
                    foreach (var f in need.Funds)
                    { f.Id = 0; f.InitialAmount = 0; f.WithdrawalAmount = 0; f.StartDate = TempNeed.CreateDate; f.MonthlyContribution = 0; f.CurrentAmount = 0; f.NewFundValue = 0; }

                    need.Beneficiaries = TempNeed.Beneficiaries.ToListObject<ClientDependent, ClientDependent>();

                    HasChanges = true;
                }

                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    need.Calculate();

                    HasChanges = true;
                }

            }
            catch { }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();
                UpdateToolBar();
            };


        }
        private void Need_PropertyChanged_Event(object sender, ListChangedEventArgs e)
        {
            try
            {


                //CellContext context = (CellContext)sender;
                //DataGrid dg = context.Grid as DataGrid;
                //DevAge.ComponentModel.BoundList<Need> _bList = dg.DataSource as DevAge.ComponentModel.BoundList<Need>;

                //Need need = _bList[e.Row - 1] as Need;


            }
            catch { };
        }
        private void Need_PropertyDelete_Event(object sender, EventArgs e)
        {

            try
            {
                CellContext context = (CellContext)sender;

                DataGrid dg = context.Grid as DataGrid;
                dg.Selection.FocusRow(context.Position.Row);

                DevAge.ComponentModel.BoundList<Need> _bList = dg.DataSource as DevAge.ComponentModel.BoundList<Need>;
                Need need = _bList[context.Position.Row - 1] as Need;

                bool _ReadOnly = false;
                if (need != null)
                {
                    #region _ReadOnly
                    if (Retirement != null)
                    {
                        if (Retirement.ReferenceId == need.Id)
                            _ReadOnly = true;
                    }

                    if (Investment != null)
                    {

                        if (Investment.ReferenceId == need.Id)
                            _ReadOnly = true;
                    }

                    if (Education != null)
                    {

                        if (Education.ReferenceId == need.Id)
                            _ReadOnly = true;
                    }

                    if (Medical != null)
                    {

                        if (Medical.ReferenceId == need.Id)
                            _ReadOnly = true;
                    }

                    if (Life != null)
                    {

                        if (Life.ReferenceId == need.Id)
                            _ReadOnly = true;
                    }

                    if (IncomeAsset != null)
                    {

                        if (IncomeAsset.ReferenceId == need.Id)
                            _ReadOnly = true;
                    }
                    #endregion
                }

                if (_ReadOnly)
                    throw new Exception("This Amendment cannot be deleted at this time.");

                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this Amendment?"))
                {
                    _bList.RemoveAt(context.Position.Row - 1);

                    Need_RowSelectChanged_Event(sender, new RowEventArgs(context.Position.Row));

                    HasChanges = true;
                }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();
                UpdateToolBar();
            }

        }
        private void Need_Fund_PropertyDelete_Event(object sender, EventArgs e)
        {

            try
            {
                CellContext context = (CellContext)sender;

                DataGrid dg = context.Grid as DataGrid;
                DevAge.ComponentModel.BoundList<Fund> _bList = dg.DataSource as DevAge.ComponentModel.BoundList<Fund>;
                Fund fund = _bList[context.Position.Row - 1] as Fund;

                bool _ReadOnly = false;
                if (TempNeed != null)
                {
                    #region _ReadOnly
                    if (Retirement != null)
                    {
                        if (Retirement.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }

                    if (Investment != null)
                    {

                        if (Investment.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }

                    if (Education != null)
                    {

                        if (Education.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }

                    if (Medical != null)
                    {

                        if (Medical.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }

                    if (Life != null)
                    {

                        if (Life.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }

                    if (IncomeAsset != null)
                    {

                        if (IncomeAsset.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }
                    #endregion


                }

                if (_ReadOnly)
                    throw new Exception("This Fund cannot be deleted at this time.");

                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this Fund?"))
                {
                    TempNeed.Funds.Remove(fund);

                    DevAge.ComponentModel.BoundList<Need> _bPList = dataGrid_PolicyDetails.DataSource as DevAge.ComponentModel.BoundList<Need>;
                    _bPList.EditedItems.Add(TempNeed);


                    HasChanges = true;
                }


            }
            catch (Exception x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();
                UpdateToolBar();
            }

        }
        private void Need_Beneficiary_PropertyDelete_Event(object sender, EventArgs e)
        {

            try
            {
                CellContext context = (CellContext)sender;

                DataGrid dg = context.Grid as DataGrid;
                DevAge.ComponentModel.BoundList<ClientDependent> _bList = dg.DataSource as DevAge.ComponentModel.BoundList<ClientDependent>;
                ClientDependent fund = _bList[context.Position.Row - 1] as ClientDependent;

                bool _ReadOnly = false;
                if (TempNeed != null)
                {
                    #region _ReadOnly
                    if (Retirement != null)
                    {
                        if (Retirement.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }

                    if (Investment != null)
                    {

                        if (Investment.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }

                    if (Education != null)
                    {

                        if (Education.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }

                    if (Medical != null)
                    {

                        if (Medical.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }

                    if (Life != null)
                    {

                        if (Life.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }

                    if (IncomeAsset != null)
                    {

                        if (IncomeAsset.ReferenceId == TempNeed.Id)
                            _ReadOnly = true;
                    }
                    #endregion


                }

                if (_ReadOnly)
                    throw new Exception("This beneficiary cannot be deleted at this time.");

                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this beneficiary?"))
                {
                    TempNeed.Beneficiaries.Remove(fund);

                    DevAge.ComponentModel.BoundList<Need> _bPList = dataGrid_PolicyDetails.DataSource as DevAge.ComponentModel.BoundList<Need>;
                    _bPList.EditedItems.Add(TempNeed);


                    HasChanges = true;
                }


            }
            catch (Exception x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();
                this.dataGrid_Beneficiaries.Refresh();

                UpdateToolBar();
            }

        }

        private void toolStripButton_SaveHistoryClicked(object sender, EventArgs e)
        {
            //try
            //{
            //    DevAge.ComponentModel.BoundList<Need> _bList = dataGrid_PolicyDetails.DataSource as DevAge.ComponentModel.BoundList<Need>;

            //    foreach(var b in _bList.EditedItems)
            //    {

            //        bool _ReadOnly = false;

            //        #region Update Policy
            //        if (Retirement != null)
            //        {
            //            if (Retirement.ReferenceId == b.Id)
            //                _ReadOnly = true;

            //            b.ReferenceId = Retirement.Id;
            //        }

            //        if (Investment != null)
            //        {
            //            if (Investment.ReferenceId == b.Id)
            //                _ReadOnly = true;

            //            b.ReferenceId = Investment.Id;
            //        }

            //        if (Education != null)
            //        {
            //            if (Education.ReferenceId == b.Id)
            //                _ReadOnly = true;

            //            b.ReferenceId = Education.Id;
            //        }

            //        if (Medical != null)
            //        {
            //            if (Medical.ReferenceId == b.Id)
            //                _ReadOnly = true;

            //            b.ReferenceId = Medical.Id;
            //        }

            //        if (Life != null)
            //        {
            //            if (Life.ReferenceId == b.Id)
            //                _ReadOnly = true;

            //            b.ReferenceId = Life.Id;
            //        }

            //        if (IncomeAsset != null)
            //        {
            //            if (IncomeAsset.ReferenceId == b.Id)
            //                _ReadOnly = true;

            //            b.ReferenceId = IncomeAsset.Id;
            //        }
            //        #endregion

            //        if (!_ReadOnly)
            //        {
            //            UpdateNeedEvent?.Invoke(b, new EventArgs());
            //        }

            //    }

            //    foreach (var b in _bList.AddedItems)
            //    {
            //        #region Update Policy
            //        if (Retirement != null)
            //        {
            //            b.ReferenceId = Retirement.Id;
            //        }

            //        if (Investment != null)
            //        {
            //            b.ReferenceId = Investment.Id;
            //        }

            //        if (Education != null)
            //        {
            //            b.ReferenceId = Education.Id;
            //        }

            //        if (Medical != null)
            //        {
            //            b.ReferenceId = Medical.Id;
            //        }

            //        if (Life != null)
            //        {
            //            b.ReferenceId = Life.Id;
            //        }

            //        if (IncomeAsset != null)
            //        {
            //            b.ReferenceId = IncomeAsset.Id;
            //        }
            //        #endregion

            //        DateTime _cdate = b.CreateDate;

            //        UpdateNeedEvent?.Invoke(b, new EventArgs());

            //        b.CreateDate = _cdate;
            //        UpdateNeedEvent?.Invoke(b, new EventArgs());

            //    }

            //    foreach (var b in _bList.RemovedItems)
            //    {
            //        DeleteNeedEvent?.Invoke(b, new EventArgs());
            //    }

            //    ReadOnly = true;

            //    #region Initialise Grids
            //    if (Retirement != null)
            //    {
            //        InitialiseGrid(Retirement);
            //    }

            //    if (Investment != null)
            //    {

            //        InitialiseGrid(Investment);
            //    }

            //    if (Education != null)
            //    {

            //        InitialiseGrid(Education);
            //    }

            //    if (Medical != null)
            //    {

            //        InitialiseGrid(Medical);
            //    }

            //    if (Life != null)
            //    {

            //        InitialiseGrid(Life);
            //    }

            //    if (IncomeAsset != null)
            //    {

            //        InitialiseGrid(IncomeAsset);
            //    }
            //    #endregion              

            //    //Successfully Saved
            //    HasChanges = false;
            //}
            //catch (Exception x)
            //{
            //    MessageBoxExt.ShowException(x);
            //}
            //finally
            //{

            //    this.dataGrid_PolicyDetails.Refresh();
            //    this.dataGrid_PolicyFunds.Refresh();
            //    //this.dataGrid_PolicyNotes.Refresh();

            //    UpdateToolBar();
            //}


        }
        private void toolStripButton_EditHistory_Click(object sender, EventArgs e)
        {
            try
            {
                ReadOnly = false;

                #region Initialise Grids
                if (Retirement != null)
                {
                    InitialiseGrid(Retirement);
                }

                if (Investment != null)
                {

                    InitialiseGrid(Investment);
                }

                if (Education != null)
                {

                    InitialiseGrid(Education);
                }

                if (Medical != null)
                {

                    InitialiseGrid(Medical);
                }

                if (Life != null)
                {

                    InitialiseGrid(Life);
                }

                if (IncomeAsset != null)
                {

                    InitialiseGrid(IncomeAsset);
                }
                #endregion              
            }
            catch (MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }


        }

        #endregion

        #region  Form Events
        private void frmMetroPolicyFunds_Load(object sender, EventArgs e)
        {
            bool IsError = false;

            try
            {
                if (Retirement != null)
                {
                    this.SubTitle += string.Format(" {0}", Retirement.Type);
                    this.xToolBarMenu1.tbCaption.Text = Retirement.Description;
                    InitialiseGrid(Retirement);

                }
                if (Investment != null)
                {
                    this.SubTitle += string.Format(" {0}", Investment.Type);
                    this.xToolBarMenu1.tbCaption.Text = Investment.Description;
                    InitialiseGrid(Investment);

                }

                if (Education != null)
                {
                    this.SubTitle += string.Format(" {0}", Education.DependentName);
                    this.xToolBarMenu1.tbCaption.Text = Education.Description;
                    InitialiseGrid(Education);

                }

                if (Medical != null)
                {

                    this.xToolBarMenu1.tbCaption.Text = Medical.Type;
                    InitialiseGrid(Medical);

                }

                if (Life != null)
                {
                    this.xToolBarMenu1.tbCaption.Text = Life.Description;
                    InitialiseGrid(Life);

                }

                if (IncomeAsset != null)
                {
                    this.xToolBarMenu1.tbCaption.Text = IncomeAsset.Type;
                    InitialiseGrid(IncomeAsset);

                }


                if (FnaNeed != null)
                {
                    this.SubTitle += string.Format(" FNA {0}", FnaNeed.NeedType);
                    this.xToolBarMenu1.tbCaption.Text = FnaNeed.Description;
                    InitialiseGrid(FnaNeed);

                }

                if (EducationNeed != null)
                {
                    this.SubTitle += string.Format(" FNA {0}", EducationNeed.NeedType);
                    this.xToolBarMenu1.tbCaption.Text = EducationNeed.Description;
                    InitialiseGrid(EducationNeed);

                }

                if (InvestmentNeed != null)
                {
                    this.SubTitle += string.Format(" FNA {0}", InvestmentNeed.NeedType);
                    this.xToolBarMenu1.tbCaption.Text = InvestmentNeed.Description;
                    InitialiseGrid(InvestmentNeed);

                }

                if (RiskCoverNeed != null)
                {
                    this.SubTitle += string.Format(" FNA {0}", RiskCoverNeed.NeedType);
                    this.xToolBarMenu1.tbCaption.Text = RiskCoverNeed.CoverType;
                    InitialiseGrid(RiskCoverNeed);

                }

                if (Instruction != null)
                {
                    this.SubTitle = string.Format("{1} [Task Status : {0}]", Instruction.Status, Instruction.Type);
                    this.xToolBarMenu1.tbCaption.Text = Instruction.TaskName;
                    InitialiseGrid(Instruction);

                }

                if (Instruction == null && TempNeed != null)
                {
                    if (TempNeed.InstructionId > 0)
                        Instruction = Program.Repository.Get<Instruction, int>(TempNeed.InstructionId);
                    if (Instruction != null)
                        this.SubTitle += string.Format(" [Task Status : {0}]", Instruction.Status);
                }

            }
            catch (MyValidationException vx)
            {
                IsError = true;
                MessageBoxExt.ShowWarning(vx.Message);
            }
            catch (Exception x)
            {
                IsError = true;

                MessageBoxExt.ShowException(x);


            }
            finally
            {
                UpdateToolBar();

                if (IsError)
                    this.Close();

            }
        }
        void InitialiseForm()
        {
            //Amend Button
            xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Enabled = false;

            //Cancel Button
            xToolBarMenu1.tbRefresh.Visible = true; xToolBarMenu1.tbRefresh.Enabled = true;
            xToolBarMenu1.tbNotes.Image = easiplan.app.Properties.Resources.Delete_42;
            xToolBarMenu1.tbRefresh.Text = "Cancel";

            xToolBarMenu1.tbSave.Visible = true; xToolBarMenu1.tbSave.Enabled = false;


            //Display Email/SMS Button
            xToolBarMenu1.tbNotes.Visible = true;
            xToolBarMenu1.tbNotes.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            xToolBarMenu1.tbNotes.Image = easiplan.app.Properties.Resources.mail_b_42;
            xToolBarMenu1.tbNotes.Text = "Email/SMS";
            xToolBarMenu1.NotesClicked -= toolStripButton_EmailSMS_Click;
            xToolBarMenu1.NotesClicked += toolStripButton_EmailSMS_Click;

            switch (Action)
            {
                case PolicyAction.PolicyHistory:

                    xToolBarMenu1.EditClicked -= toolStripButton_EditHistory_Click;
                    xToolBarMenu1.EditClicked += toolStripButton_EditHistory_Click;

                    //xToolBarMenu1.RefreshClicked -= toolStripButton_Cancel_Click;
                    //xToolBarMenu1.RefreshClicked += toolStripButton_Cancel_Click;

                    xToolBarMenu1.SaveClicked -= toolStripButton_SaveHistoryClicked;
                    xToolBarMenu1.SaveClicked += toolStripButton_SaveHistoryClicked;


                    break;

                default:

                    xToolBarMenu1.EditClicked -= toolStripButton_Edit_Click;
                    xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;

                    xToolBarMenu1.RefreshClicked -= toolStripButton_Cancel_Click;
                    xToolBarMenu1.RefreshClicked += toolStripButton_Cancel_Click;

                    xToolBarMenu1.SaveClicked -= toolStripButton_Save_Click;
                    xToolBarMenu1.SaveClicked += toolStripButton_Save_Click;

                    break;


            }

            xToolBarMenu1.CloseClicked -= XToolBarMenu1_Close_Click;
            xToolBarMenu1.CloseClicked += XToolBarMenu1_Close_Click;

        }
        void UpdateToolBar()
        {
            xToolBarMenu1.tbRefresh.Visible = false;
            xToolBarMenu1.tbEdit.Visible = false;
            xToolBarMenu1.tbSave.Visible = false;

            xToolBarMenu1.tbEdit.Enabled = !ReadOnly && HasChanges;
            xToolBarMenu1.tbSave.Enabled = !ReadOnly && HasChanges;

            if (Action != PolicyAction.AmendPolicy) xToolBarMenu1.tbSave.Visible = true;

            if (Action == PolicyAction.ViewFunds) xToolBarMenu1.tbSave.Visible = false;

            if (Action == PolicyAction.PolicyHistory)
            {
                xToolBarMenu1.tbRefresh.Visible = false;
                xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "Edit";
                xToolBarMenu1.tbSave.Visible = true;

                xToolBarMenu1.tbEdit.Enabled = ReadOnly;

            }

            if (TempNeed == null)
                return;

            switch (TempNeed.Status.ToEnum<NeedStatus>())
            {
                case NeedStatus.Advised:
                    xToolBarMenu1.tbEdit.Visible = true;
                    xToolBarMenu1.tbEdit.Text = "Accept";
                    xToolBarMenu1.tbEdit.Enabled = !ReadOnly;//&& HasChanges;

                    //xToolBarMenu1.tbRefresh.Visible = true; xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;
                    break;

                case NeedStatus.Pending:
                    if (Action != PolicyAction.ViewPolicy && Action != PolicyAction.ViewFunds)
                    {
                        xToolBarMenu1.tbEdit.Visible = true;
                        xToolBarMenu1.tbEdit.Text = "Accept";
                        xToolBarMenu1.tbEdit.Enabled = !ReadOnly && HasChanges;
                    }
                    break;
                case NeedStatus.Accepted:
                    xToolBarMenu1.tbRefresh.Visible = true; xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;
                    xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "Submit for Authorisation"; xToolBarMenu1.tbEdit.Enabled = !ReadOnly;//Instruction != null
                    xToolBarMenu1.tbSave.Visible = true;
                    break;
                case NeedStatus.InProgress:
                    xToolBarMenu1.tbRefresh.Visible = true; xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;
                    xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "Submit for Authorisation"; xToolBarMenu1.tbEdit.Enabled = !ReadOnly;//Instruction != null
                    xToolBarMenu1.tbSave.Visible = true;
                    break;
                case NeedStatus.Submitted:
                    xToolBarMenu1.tbRefresh.Visible = true; xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;
                    xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "Submit for Authorisation"; xToolBarMenu1.tbEdit.Enabled = !ReadOnly;//Instruction != null
                    xToolBarMenu1.tbSave.Visible = true;
                    break;
                case NeedStatus.ReadyForAuth: //for Authorisation
                    xToolBarMenu1.tbRefresh.Visible = true; xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;
                    xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "Authorise"; xToolBarMenu1.tbEdit.Enabled = !ReadOnly;//&& Program.User.Designation.Contains("Authoriser");//Instruction != null
                    xToolBarMenu1.tbSave.Visible = true;
                    break;
                case NeedStatus.Authorised:
                    xToolBarMenu1.tbRefresh.Visible = true; xToolBarMenu1.tbRefresh.Enabled = !ReadOnly;
                    xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "Implement"; xToolBarMenu1.tbEdit.Enabled = !ReadOnly;//&& Program.User.Designation.Contains("Authoriser");//Instruction != null
                    xToolBarMenu1.tbSave.Visible = true;
                    break;
                case NeedStatus.Cancelled:
                    xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "Re-open"; xToolBarMenu1.tbEdit.Enabled = ReadOnly;
                    xToolBarMenu1.tbSave.Visible = false;
                    break;
                case NeedStatus.Unknown:
                    xToolBarMenu1.tbSave.Visible = true;// !ReadOnly;
                    break;

            }


            //Display Save Button
            if (xToolBarMenu1.tbSave.Visible)
                xToolBarMenu1.tbSave.Enabled = HasChanges;

        }
        #endregion
    }

    internal static class ModelUtils
    {

        internal static Need CloneAsNeed(this Retirement obj, PolicyAction action)
        {
            Need need = new Need()
            {
                NeedType = NeedTypes.RetirementNeed,
                IsUpdate = true,
                Status = NeedStatus.Pending.ToText()
            };

            need.IsLoading = true;
            try
            {
                // need.Id = obj.Id;
                need.Type = obj.Type;
                need.Description = obj.Description;

                need.MonthlyContribution = obj.MonthlyContribution;
                need.NewMonthlyContribution = obj.MonthlyContribution;//
                need.CurrentAmount = obj.CurrentAmount;
                //YJ 2021-11-05
                if (action != PolicyAction.AmendPolicy)
                    need.InitialAmount = obj.InitialAmount;

                need.CurrentAge = obj.CurrentAge;
                need.InvestmentAge = obj.InvestmentAge;

                need.InflationPercentage = obj.InflationPercentage;
                need.EscalationPercentage = (double)obj.EscalationPercentage;
                need.GrowthPercentage = obj.GrowthPercentage;

                need.ReferenceNo = obj.ReferenceNo;
                need.ReferenceId = obj.Id;

                need.Insured = obj.Insured; //YJ 10/02/2020

                need.Funds.Clear();
                foreach (var fund in obj.Funds)
                {
                    Fund f = fund.ToObject<Fund>();
                    f.Id = 0;
                    f.StartDate = DateTime.Now;
                    f.EndDate = f.MinDateTime;
                    f.InitialAmount = 0;
                    f.WithdrawalAmount = 0;
                    need.Funds.Add(f);
                }

                need.Beneficiaries.Clear();
                foreach (var beneficiary in obj.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    need.Beneficiaries.Add(f);
                }

            }
            catch (Exception x)
            {

            }
            finally
            {
                need.Calculate();

                need.IsLoading = false;
            }
            return need;

        }
        internal static InvestmentNeed CloneAsNeed(this Investment obj, PolicyAction action)
        {
            InvestmentNeed need = new InvestmentNeed()
            {
                NeedType = NeedTypes.InvestmentNeed,
                IsUpdate = true,
                Status = NeedStatus.Pending.ToText()
            };

            need.IsLoading = true;
            try
            {
                // need.Id = obj.Id;
                need.Type = obj.Type;
                need.Description = obj.Description;

                need.MonthlyContribution = obj.MonthlyContribution;
                need.NewMonthlyContribution = obj.MonthlyContribution;//
                need.CurrentAmount = obj.CurrentAmount;
                //YJ 2021-11-05
                if (action != PolicyAction.AmendPolicy)
                    need.InitialAmount = obj.InitialAmount;

                need.CurrentAge = obj.CurrentAge;
                need.InvestmentAge = obj.InvestmentAge;

                need.InflationPercentage = obj.InflationPercentage;
                need.EscalationPercentage = (double)obj.EscalationPercentage;
                need.GrowthPercentage = obj.GrowthPercentage;

                need.ReferenceNo = obj.ReferenceNo;
                need.ReferenceId = obj.Id;

                need.Insured = obj.Insured; //YJ 10/02/2020

                need.Funds.Clear();
                foreach (var fund in obj.Funds)
                {
                    Fund f = fund.ToObject<Fund>();
                    f.Id = 0;
                    f.StartDate = DateTime.Now;
                    f.EndDate = f.MinDateTime;
                    f.InitialAmount = 0;
                    f.WithdrawalAmount = 0;
                    need.Funds.Add(f);
                }

                need.Beneficiaries.Clear();
                foreach (var beneficiary in obj.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    need.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                need.Calculate();
                need.IsLoading = false;
            }
            return need;

        }
        internal static EducationNeed CloneAsNeed(this Education obj, PolicyAction action)
        {
            EducationNeed need = new EducationNeed()
            {
                NeedType = NeedTypes.EducationNeed,
                IsUpdate = true,
                Status = NeedStatus.Pending.ToText()
            };

            need.IsLoading = true;
            try
            {
                // need.Id = obj.Id; 
                need.Type = obj.Type;
                need.Description = obj.Description;

                need.MonthlyContribution = obj.MonthlyContribution;
                need.NewMonthlyContribution = obj.MonthlyContribution;//
                need.CurrentAmount = obj.CurrentAmount;
                //YJ 2021-11-05
                if (action != PolicyAction.AmendPolicy)
                    need.InitialAmount = obj.InitialAmount;

                need.DependentDOB = obj.DependentDOB;
                need.CurrentAge = obj.CurrentAge;
                need.InvestmentAge = obj.InvestmentAge;

                need.InflationPercentage = obj.InflationPercentage;
                need.EscalationPercentage = (double)obj.EscalationPercentage;
                need.GrowthPercentage = obj.GrowthPercentage;

                need.ReferenceNo = obj.ReferenceNo;
                need.ReferenceId = obj.Id;

                need.Insured = obj.DependentName; //YJ 10/02/2020

                need.Funds.Clear();
                foreach (var fund in obj.Funds)
                {
                    Fund f = fund.ToObject<Fund>();
                    f.Id = 0;
                    f.StartDate = DateTime.Now;
                    f.EndDate = f.MinDateTime;
                    f.InitialAmount = 0;
                    f.WithdrawalAmount = 0;
                    need.Funds.Add(f);
                }

                need.Beneficiaries.Clear();
                foreach (var beneficiary in obj.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    need.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                need.Calculate();
                need.IsLoading = false;
            }
            return need;

        }
        internal static Need CloneAsNeed(this Medical obj, PolicyAction action)
        {
            Need need = new Need()
            {
                NeedType = NeedTypes.MedicalNeed,
                IsUpdate = true,
                Status = NeedStatus.Pending.ToText()
            };

            need.IsLoading = true;
            try
            {
                // need.Id = obj.Id;
                need.Type = obj.Type;
                need.Description = obj.Description;

                need.MonthlyContribution = obj.MonthlyContribution;
                need.NewMonthlyContribution = obj.MonthlyContribution;//
                need.CurrentAmount = obj.CurrentAmount;

                need.CurrentAge = obj.CurrentAge;
                need.InvestmentAge = obj.InvestmentAge;

                need.InflationPercentage = obj.InflationPercentage;
                need.EscalationPercentage = (double)obj.EscalationPercentage;
                need.GrowthPercentage = obj.GrowthPercentage;

                need.ReferenceNo = obj.ReferenceNo;
                need.ReferenceId = obj.Id;

                need.Insured = obj.Insured; //YJ 10/02/2020

                need.Benefits.Clear();
                foreach (var benefit in obj.Benefits)
                {
                    Benefit f = benefit.ToObject<Benefit>();
                    f.Id = 0;
                    f.StartDate = DateTime.Now;
                    f.EndDate = f.MinDateTime;

                    need.Benefits.Add(f);
                }

                need.Beneficiaries.Clear();
                foreach (var beneficiary in obj.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    need.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                need.Calculate();
                need.IsLoading = false;
            }
            return need;

        }
        internal static Need CloneAsNeed(this Life obj, PolicyAction action)
        {
            Need need = new Need()
            {
                NeedType = NeedTypes.LifeDisabilityNeed,
                IsUpdate = true,
                Status = NeedStatus.Pending.ToText()
            };

            need.IsLoading = true;
            try
            {
                // need.Id = obj.Id;
                need.Type = obj.Type;
                need.Description = obj.Description;

                need.MonthlyContribution = obj.MonthlyContribution;
                need.NewMonthlyContribution = obj.MonthlyContribution;//
                need.CurrentAmount = obj.CurrentAmount;

                need.CurrentAge = obj.CurrentAge;
                need.InvestmentAge = obj.InvestmentAge;

                need.InflationPercentage = obj.InflationPercentage;
                need.EscalationPercentage = (double)obj.EscalationPercentage;
                need.GrowthPercentage = obj.GrowthPercentage;

                need.ReferenceNo = obj.ReferenceNo;
                need.ReferenceId = obj.Id;

                need.Insured = obj.Insured; //YJ 10/02/2020

                need.Benefits.Clear();
                foreach (var benefit in obj.Benefits)
                {
                    Benefit f = benefit.ToObject<Benefit>();
                    f.Id = 0;
                    f.StartDate = DateTime.Now;
                    f.EndDate = f.MinDateTime;

                    need.Benefits.Add(f);
                }

                need.Beneficiaries.Clear();
                foreach (var beneficiary in obj.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    need.Beneficiaries.Add(f);
                }

            }
            catch (Exception x)
            {

            }
            finally
            {
                need.Calculate();
                need.IsLoading = false;
            }
            return need;

        }
        internal static Need CloneAsNeed(this IncomeAsset obj, PolicyAction action)
        {
            Need need = new Need()
            {
                NeedType = NeedTypes.IncomeAssetNeed,
                IsUpdate = true,
                Status = NeedStatus.Pending.ToText()
            };

            need.IsLoading = true;
            try
            {
                // need.Id = obj.Id;
                need.Type = obj.Type;
                need.Description = obj.Description;

                need.MonthlyContribution = obj.MonthlyContribution;
                need.NewMonthlyContribution = obj.MonthlyContribution;//
                need.CurrentAmount = obj.CurrentAmount;

                need.CurrentAge = obj.CurrentAge;
                need.InvestmentAge = obj.InvestmentAge;

                need.InflationPercentage = obj.InflationPercentage;
                need.EscalationPercentage = (double)obj.EscalationPercentage;
                need.GrowthPercentage = obj.GrowthPercentage;

                need.ReferenceNo = obj.ReferenceNo;
                need.ReferenceId = obj.Id;

                need.Funds.Clear();
                foreach (var fund in obj.Funds)
                {
                    Fund f = fund.ToObject<Fund>();
                    f.Id = 0;
                    f.StartDate = DateTime.Now;
                    f.EndDate = f.MinDateTime;
                    f.InitialAmount = 0;
                    f.WithdrawalAmount = 0;
                    need.Funds.Add(f);
                }

                need.Beneficiaries.Clear();
                foreach (var beneficiary in obj.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    need.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                need.Calculate();
                need.IsLoading = false;
            }
            return need;

        }
        //internal static Need CloneAsNeed(this Need obj, PolicyAction action)
        //{
        //    Need need = new Need()
        //    {
        //        NeedType = NeedTypes.EducationNeed,
        //        IsUpdate = false,
        //        Status = obj.Status
        //    };

        //    need.IsLoading = true;
        //    try
        //    {
        //       // need.Id = obj.Id;
        //        need.Type = obj.Type;
        //        need.Description = obj.Description;

        //        need.MonthlyContribution = obj.MonthlyContribution;
        //        need.NewMonthlyContribution = obj.MonthlyContribution;//
        //        need.CurrentAmount = obj.CurrentAmount;

        //        need.CurrentAge = obj.CurrentAge;
        //        need.InvestmentAge = obj.InvestmentAge;

        //        need.InflationPercentage = obj.InflationPercentage;
        //        need.EscalationPercentage = (double)obj.EscalationPercentage;
        //        need.GrowthPercentage = obj.GrowthPercentage;

        //        need.ReferenceNo = obj.ReferenceNo;
        //        need.ReferenceId = obj.Id;

        //        need.InstructionId = obj.InstructionId;
        //        //need.InitialAmount = obj.InitialAmount;

        //        need.Insured = obj.Insured; //YJ 10/02/2020

        //        need.Funds.Clear();
        //        foreach (var fund in obj.Funds)
        //        {
        //            Fund f = fund.ToObject<Fund>();
        //            f.Id = 0;
        //           // f.StartDate = DateTime.Now;
        //           // f.EndDate = f.MinDateTime;
        //           // f.InitialAmount = 0;
        //           // f.WithdrawalAmount = 0;
        //            need.Funds.Add(f);
        //        }

        //        need.Beneficiaries.Clear();
        //        foreach (var beneficiary in obj.Beneficiaries)
        //        {
        //            ClientDependent f = beneficiary.ToObject<ClientDependent>();
        //            f.Id = 0;
        //            need.Beneficiaries.Add(f);
        //        }

        //    }
        //    catch (Exception x)
        //    {

        //    }
        //    finally
        //    {
        //        need.Calculate();
        //        need.IsLoading = false;
        //    }
        //    return need;

        //}
        //internal static Need CloneAsNeed(this EducationNeed obj, PolicyAction action)
        //{
        //    Need need = new Need()
        //    {
        //        NeedType = NeedTypes.EducationNeed,
        //        IsUpdate = false, // this is a need
        //        Status = obj.Status
        //    };

        //    need.IsLoading = true;
        //    try
        //    {
        //       // need.Id = obj.Id;
        //        need.Type = obj.Type;
        //        need.Description = obj.Description;

        //        need.MonthlyContribution = obj.MonthlyContribution;
        //        need.NewMonthlyContribution = obj.NewMonthlyContribution;
        //        need.CurrentAmount = obj.CurrentAmount;
        //        //YJ 2021-11-05
        //        need.InitialAmount = obj.InitialAmount;

        //        need.CurrentAge = obj.CurrentAge;
        //        need.InvestmentAge = obj.InvestmentAge;

        //        need.InflationPercentage = obj.InflationPercentage;
        //        need.EscalationPercentage = (double)obj.EscalationPercentage;
        //        need.GrowthPercentage = obj.GrowthPercentage;

        //        need.ReferenceNo = obj.ReferenceNo;
        //        need.ReferenceId = obj.Id;

        //        need.InstructionId = obj.InstructionId;

        //        need.Insured = obj.Insured; //YJ 10/02/2020

        //        need.Funds.Clear();

        //        foreach (var fund in obj.Funds)
        //        {
        //            Fund f = fund.ToObject<Fund>();
        //            f.Id = 0;
        //            f.StartDate = DateTime.Now;
        //            f.EndDate = f.MinDateTime;
        //            f.InitialAmount = 0;
        //            f.WithdrawalAmount = 0;
        //            need.Funds.Add(f);
        //        }

        //        need.Beneficiaries.Clear();
        //        foreach (var beneficiary in obj.Beneficiaries)
        //        {
        //            ClientDependent f = beneficiary.ToObject<ClientDependent>();
        //            f.Id = 0;
        //            need.Beneficiaries.Add(f);
        //        }

        //    }
        //    catch (Exception x)
        //    {

        //    }
        //    finally
        //    {
        //        need.Calculate();
        //        need.IsLoading = false;
        //    }
        //    return need;

        //}
        //internal static Need CloneAsNeed(this InvestmentNeed obj, PolicyAction action)
        //{
        //    Need need = new Need()
        //    {
        //        NeedType = NeedTypes.InvestmentNeed,
        //        IsUpdate = false,
        //        Status = obj.Status
        //    };

        //    need.IsLoading = true;
        //    try
        //    {
        //       // need.Id = obj.Id;
        //        need.Type = obj.Type;
        //        need.Description = obj.Description;

        //        need.MonthlyContribution = obj.MonthlyContribution;
        //        need.NewMonthlyContribution = obj.NewMonthlyContribution;//
        //        need.CurrentAmount = obj.CurrentAmount;
        //        //YJ 2021-11-05
        //        need.InitialAmount = obj.InitialAmount;

        //        need.CurrentAge = obj.CurrentAge;
        //        need.InvestmentAge = obj.InvestmentAge;

        //        need.InflationPercentage = obj.InflationPercentage;
        //        need.EscalationPercentage = (double)obj.EscalationPercentage;
        //        need.GrowthPercentage = obj.GrowthPercentage;

        //        need.ReferenceNo = obj.ReferenceNo;
        //        need.ReferenceId = obj.Id;

        //        need.InstructionId = obj.InstructionId;

        //        need.Insured = obj.Insured; //YJ 10/02/2020

        //        need.Funds.Clear();

        //        foreach (var fund in obj.Funds)
        //        {
        //            Fund f = fund.ToObject<Fund>();
        //            f.Id = 0;
        //            f.StartDate = DateTime.Now;
        //            f.EndDate = f.MinDateTime;
        //            f.InitialAmount = 0;
        //            f.WithdrawalAmount = 0;
        //            need.Funds.Add(f);
        //        }

        //        need.Beneficiaries.Clear();

        //        foreach (var beneficiary in obj.Beneficiaries)
        //        {
        //            ClientDependent f = beneficiary.ToObject<ClientDependent>();
        //            f.Id = 0;
        //            need.Beneficiaries.Add(f);
        //        }
        //    }
        //    catch (Exception x)
        //    {

        //    }
        //    finally
        //    {
        //        need.Calculate();
        //        need.IsLoading = false;
        //    }
        //    return need;

        //}

        internal static void UpdateNeed(this Need need, ref Retirement obj, PolicyAction action)
        {
            //if (need.Equals(obj))
            //    return;

            if (obj == null)
                obj = new Retirement();

            obj.IsLoading = true;
            try
            {
                obj.Type = need.Type;

                obj.Description = need.Description;
                obj.InitialAmount = need.InitialAmount;
                obj.MonthlyContribution = need.NewMonthlyContribution;

                obj.CurrentAmount = need.CurrentAmount;
                obj.CurrentAge = need.CurrentAge;
                obj.InvestmentAge = need.InvestmentAge;

                obj.InflationPercentage = need.InflationPercentage;
                obj.EscalationPercentage = (double)need.EscalationPercentage;
                obj.GrowthPercentage = need.GrowthPercentage;

                obj.ReferenceNo = need.ReferenceNo;
                obj.ReferenceId = need.Id;
                obj.Status = need.Status;

                obj.UpdateDate = DateTime.Now;
                obj.UpdateBy = Program.User.Username;

                obj.Insured = need.Insured; //YJ 10/02/2020

                obj.Funds.Clear();
                foreach (var fund in need.Funds)
                {
                    Fund f = fund.ToObject<Fund>();
                    f.Id = 0;
                    f.CurrentAmount = fund.NewFundValue;

                    obj.Funds.Add(f);
                }

                obj.Beneficiaries.Clear();
                foreach (var beneficiary in need.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    obj.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                obj.Calculate();
                obj.IsLoading = false;
            }
        }
        //internal static void UpdateNeed(this Need need, ref Investment obj, PolicyAction action)
        //{
        //    if (need.Equals(obj))
        //        return;

        //    if (obj == null)
        //        obj = new Investment();

        //    obj.IsLoading = true;
        //    try
        //    {
        //        obj.Type = need.Type;
        //        obj.Description = need.Description;
        //        obj.InitialAmount = need.InitialAmount;
        //        obj.MonthlyContribution = need.NewMonthlyContribution;

        //        obj.CurrentAmount = need.CurrentAmount;
        //        obj.CurrentAge = need.CurrentAge;
        //        obj.InvestmentAge = need.InvestmentAge;

        //        obj.InflationPercentage = need.InflationPercentage;
        //        obj.EscalationPercentage = (double)need.EscalationPercentage;
        //        obj.GrowthPercentage = need.GrowthPercentage;

        //        obj.ReferenceNo = need.ReferenceNo;
        //        obj.ReferenceId = need.Id;
        //        obj.Status = need.Status;

        //        obj.Insured = need.Insured; //YJ 10/02/2020

        //        obj.UpdateDate = DateTime.Now;
        //        obj.UpdateBy = Program.User.Username;

        //        obj.Funds.Clear();
        //        foreach (var fund in need.Funds)
        //        {
        //            Fund f = fund.ToObject<Fund>();
        //            f.Id = 0;
        //            f.CurrentAmount = fund.NewFundValue;

        //            obj.Funds.Add(f);
        //        }

        //        obj.Beneficiaries.Clear();
        //        foreach (var beneficiary in need.Beneficiaries)
        //        {
        //            ClientDependent f = beneficiary.ToObject<ClientDependent>();
        //            f.Id = 0;
        //            obj.Beneficiaries.Add(f);
        //        }

        //    }
        //    catch (Exception x)
        //    {

        //    }
        //    finally
        //    {
        //        obj.Calculate();
        //        obj.IsLoading = false;
        //    }


        //}
        //internal static void UpdateNeed(this Need need, ref Education obj, PolicyAction action)
        //{
        //    if (need.Equals(obj))
        //        return;

        //    if (obj == null)
        //        obj = new Education();

        //    obj.IsLoading = true;
        //    try
        //    {
        //        obj.Type = need.Type;
        //        obj.Description = need.Description;
        //        obj.InitialAmount = need.InitialAmount;
        //        obj.MonthlyContribution = need.NewMonthlyContribution;

        //        obj.CurrentAmount = need.CurrentAmount;
        //        obj.CurrentAge = need.CurrentAge;
        //        obj.InvestmentAge = need.InvestmentAge;

        //        obj.InflationPercentage = need.InflationPercentage;
        //        obj.EscalationPercentage = (double)need.EscalationPercentage;
        //        obj.GrowthPercentage = need.GrowthPercentage;

        //        obj.ReferenceNo = need.ReferenceNo;
        //        obj.ReferenceId = need.Id;
        //        obj.Status = need.Status;               

        //        obj.UpdateDate = DateTime.Now;
        //        obj.UpdateBy = Program.User.Username;

        //        obj.DependentName = need.Insured; //YJ 10/02/2020

        //        obj.Funds.Clear();
        //        foreach (var fund in need.Funds)
        //        {
        //            Fund f = fund.ToObject<Fund>();
        //            f.Id = 0;
        //            f.CurrentAmount = fund.NewFundValue;

        //            obj.Funds.Add(f);
        //        }

        //        obj.Beneficiaries.Clear();
        //        foreach (var beneficiary in need.Beneficiaries)
        //        {
        //            ClientDependent f = beneficiary.ToObject<ClientDependent>();
        //            f.Id = 0;
        //            obj.Beneficiaries.Add(f);
        //        }
        //    }
        //    catch (Exception x)
        //    {

        //    }
        //    finally
        //    {
        //        obj.Calculate();
        //        obj.IsLoading = false;
        //    }


        //}
        internal static void UpdateNeed(this Need need, ref Medical obj, PolicyAction action)
        {
            //if (need.Equals(obj))
            //    return;

            if (obj == null)
                obj = new Medical();

            obj.IsLoading = true;
            try
            {
                obj.Type = need.Type;
                obj.Description = need.Description;
                obj.InitialAmount = need.InitialAmount;
                obj.MonthlyContribution = need.NewMonthlyContribution;

                obj.CurrentAmount = need.CurrentAmount;
                obj.CurrentAge = need.CurrentAge;
                obj.InvestmentAge = need.InvestmentAge;

                obj.InflationPercentage = need.InflationPercentage;
                obj.EscalationPercentage = (double)need.EscalationPercentage;
                obj.GrowthPercentage = need.GrowthPercentage;

                obj.ReferenceNo = need.ReferenceNo;
                obj.ReferenceId = need.Id;
                obj.Status = need.Status;


                obj.Insured = need.Insured; //YJ 10/02/2020

                obj.UpdateDate = DateTime.Now;
                obj.UpdateBy = Program.User.Username;

                obj.Benefits.Clear();
                foreach (var fund in need.Benefits)
                {
                    Benefit f = fund.ToObject<Benefit>();
                    f.Id = 0;

                    obj.Benefits.Add(f);
                }

                obj.Beneficiaries.Clear();
                foreach (var beneficiary in need.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    obj.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                obj.Calculate();
                obj.IsLoading = false;
            }

        }
        internal static void UpdateNeed(this Need need, ref Life obj, PolicyAction action)
        {
            //if (need.Equals(obj))
            //    return;

            if (obj == null)
                obj = new Life();

            obj.IsLoading = true;
            try
            {
                obj.Type = need.Type;
                obj.Description = need.Description;
                obj.InitialAmount = need.InitialAmount;
                obj.MonthlyContribution = need.NewMonthlyContribution;

                obj.CurrentAmount = need.CurrentAmount;
                obj.CurrentAge = need.CurrentAge;
                obj.InvestmentAge = need.InvestmentAge;

                obj.InflationPercentage = need.InflationPercentage;
                obj.EscalationPercentage = (double)need.EscalationPercentage;
                obj.GrowthPercentage = need.GrowthPercentage;

                obj.ReferenceNo = need.ReferenceNo;
                obj.ReferenceId = need.Id;
                obj.Status = need.Status;

                obj.Insured = need.Insured; //YJ 10/02/2020

                obj.UpdateDate = DateTime.Now;
                obj.UpdateBy = Program.User.Username;

                obj.Benefits.Clear();
                foreach (var fund in need.Benefits)
                {
                    Benefit f = fund.ToObject<Benefit>();
                    f.Id = 0;

                    obj.Benefits.Add(f);
                }

                obj.Beneficiaries.Clear();
                foreach (var beneficiary in need.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    obj.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                obj.Calculate();
                obj.IsLoading = false;
            }

        }
        internal static void UpdateNeed(this Need need, ref IncomeAsset obj, PolicyAction action)
        {
            //if (need.Equals(obj))
            //    return;

            if (obj == null)
                obj = new IncomeAsset();

            obj.IsLoading = true;

            try
            {
                obj.Type = need.Type;
                obj.Description = need.Description;
                obj.InitialAmount = need.InitialAmount;
                obj.MonthlyContribution = need.NewMonthlyContribution;

                obj.CurrentAmount = need.CurrentAmount;
                obj.CurrentAge = need.CurrentAge;
                obj.InvestmentAge = need.InvestmentAge;

                obj.InflationPercentage = need.InflationPercentage;
                obj.EscalationPercentage = (double)need.EscalationPercentage;
                obj.GrowthPercentage = need.GrowthPercentage;

                obj.ReferenceNo = need.ReferenceNo;
                obj.ReferenceId = need.Id;
                obj.Status = need.Status;

                obj.UpdateDate = DateTime.Now;
                obj.UpdateBy = Program.User.Username;

                obj.Funds.Clear();
                foreach (var fund in need.Funds)
                {
                    Fund f = fund.ToObject<Fund>();
                    f.Id = 0;
                    f.CurrentAmount = fund.NewFundValue;

                    obj.Funds.Add(f);
                }

                obj.Beneficiaries.Clear();
                foreach (var beneficiary in need.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    obj.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                obj.Calculate();
                obj.IsLoading = false;
            }
        }
        //internal static void UpdateNeed(this Need need, ref Need obj, PolicyAction action)
        //{
        //    if (need.Equals(obj))
        //        return;

        //    if (obj == null)
        //        obj = new Need();

        //    obj.IsLoading = true;


        //    try
        //    {
        //        obj.Type = need.Type;
        //        obj.Description = need.Description;
        //        obj.InitialAmount = need.InitialAmount;
        //        obj.MonthlyContribution = need.NewMonthlyContribution;

        //        obj.CurrentAmount = need.CurrentAmount;
        //        obj.CurrentAge = need.CurrentAge;
        //        obj.InvestmentAge = need.InvestmentAge;

        //        obj.InflationPercentage = need.InflationPercentage;
        //        obj.EscalationPercentage = (double)need.EscalationPercentage;
        //        obj.GrowthPercentage = need.GrowthPercentage;

        //        obj.ReferenceNo = need.ReferenceNo;
        //        obj.ReferenceId = need.Id;
        //        obj.Status = need.Status;


        //        obj.Insured = need.Insured; //YJ 10/02/2020

        //        obj.UpdateDate = DateTime.Now;
        //        obj.UpdateBy = Program.User.Username;

        //        obj.Funds.Clear();
        //        foreach (var fund in need.Funds)
        //        {
        //            Fund f = fund.ToObject<Fund>();
        //            f.Id = 0;
        //            f.CurrentAmount = fund.NewFundValue;

        //            obj.Funds.Add(f);
        //        }

        //        obj.Notes.Clear();
        //        foreach (var fund in need.Notes)
        //        {
        //            Note f = fund.ToObject<Note>();
        //            f.Id = 0;

        //            obj.Notes.Add(f);
        //        }

        //        obj.Beneficiaries.Clear();
        //        foreach (var beneficiary in need.Beneficiaries)
        //        {
        //            ClientDependent f = beneficiary.ToObject<ClientDependent>();
        //            f.Id = 0;
        //            obj.Beneficiaries.Add(f);
        //        }
        //    }
        //    catch (Exception x)
        //    {

        //    }
        //    finally
        //    {
        //        obj.Calculate();
        //        obj.IsLoading = false;
        //    }

        //}
        //internal static void UpdateNeed(this Need need, ref EducationNeed obj, PolicyAction action)
        //{
        //    if (need.Equals(obj))
        //        return;

        //    if (obj == null)
        //        obj = new EducationNeed();

        //    obj.IsLoading = true;

        //    try
        //    {
        //        obj.Type = need.Type;
        //        obj.Description = need.Description;
        //        obj.InitialAmount = need.InitialAmount;
        //        obj.MonthlyContribution = need.MonthlyContribution;
        //        obj.NewMonthlyContribution = need.NewMonthlyContribution;

        //        obj.CurrentAmount = need.CurrentAmount;
        //        obj.CurrentAge = need.CurrentAge;
        //        obj.InvestmentAge = need.InvestmentAge;

        //        obj.InflationPercentage = need.InflationPercentage;
        //        obj.EscalationPercentage = (double)need.EscalationPercentage;
        //        obj.GrowthPercentage = need.GrowthPercentage;

        //        obj.ReferenceNo = need.ReferenceNo;
        //        obj.ReferenceId = need.Id;
        //        obj.Status = need.Status;

        //        obj.Insured = need.Insured; //YJ 10/02/2020


        //        obj.UpdateDate = DateTime.Now;
        //        obj.UpdateBy = Program.User.Username;

        //        obj.Funds.Clear();
        //        foreach (var fund in need.Funds)
        //        {
        //            Fund f = fund.ToObject<Fund>();
        //            f.Id = 0;
        //            f.CurrentAmount = fund.NewFundValue;

        //            obj.Funds.Add(f);
        //        }

        //        obj.Notes.Clear();
        //        foreach (var fund in need.Notes)
        //        {
        //            Note f = fund.ToObject<Note>();
        //            f.Id = 0;

        //            obj.Notes.Add(f);
        //        }

        //        obj.Beneficiaries.Clear();
        //        foreach (var beneficiary in need.Beneficiaries)
        //        {
        //            ClientDependent f = beneficiary.ToObject<ClientDependent>();
        //            f.Id = 0;
        //            obj.Beneficiaries.Add(f);
        //        }
        //    }
        //    catch (Exception x)
        //    {

        //    }
        //    finally
        //    {
        //        obj.Calculate();
        //        obj.IsLoading = false;
        //    }
        //}
        //internal static void UpdateNeed(this Need need, ref InvestmentNeed obj, PolicyAction action)
        //{
        //    if (need.Equals(obj))
        //        return;

        //    if (obj == null)
        //        obj = new InvestmentNeed();

        //    obj.IsLoading = true;


        //    try
        //    {
        //        obj.Type = need.Type;
        //        obj.Description = need.Description;
        //        obj.InitialAmount = need.InitialAmount;
        //        obj.MonthlyContribution = need.MonthlyContribution;
        //        obj.NewMonthlyContribution = need.NewMonthlyContribution;

        //        obj.CurrentAmount = need.CurrentAmount;
        //        obj.CurrentAge = need.CurrentAge;
        //        obj.InvestmentAge = need.InvestmentAge;

        //        obj.InflationPercentage = need.InflationPercentage;
        //        obj.EscalationPercentage = (double)need.EscalationPercentage;
        //        obj.GrowthPercentage = need.GrowthPercentage;

        //        obj.ReferenceNo = need.ReferenceNo;
        //        obj.ReferenceId = need.Id;
        //        obj.Status = need.Status;
        //        obj.Insured = need.Insured; //YJ 10/02/2020
        //        obj.UpdateDate = DateTime.Now;
        //        obj.UpdateBy = Program.User.Username;

        //        obj.Funds.Clear();
        //        foreach (var fund in need.Funds)
        //        {
        //            Fund f = fund.ToObject<Fund>();
        //            f.Id = 0;
        //            f.CurrentAmount = fund.NewFundValue;

        //            obj.Funds.Add(f);
        //        }

        //        obj.Notes.Clear();
        //        foreach (var fund in need.Notes)
        //        {
        //            Note f = fund.ToObject<Note>();
        //            f.Id = 0;

        //            obj.Notes.Add(f);
        //        }

        //        obj.Beneficiaries.Clear();
        //        foreach (var beneficiary in need.Beneficiaries)
        //        {
        //            ClientDependent f = beneficiary.ToObject<ClientDependent>();
        //            f.Id = 0;
        //            obj.Beneficiaries.Add(f);
        //        }
        //    }
        //    catch (Exception x)
        //    {

        //    }
        //    finally
        //    {
        //        obj.Calculate();
        //        obj.IsLoading = false;
        //    }
        //}
        internal static void UpdateNeed(this EducationNeed need, ref Education obj, PolicyAction action)
        {
            //if (need.Equals(obj))
            //    return;

            if (obj == null)
                obj = new Education();

            obj.IsLoading = true;
            try
            {
                obj.Type = need.Type;
                obj.Description = need.Description;
                obj.InitialAmount = need.InitialAmount;
                obj.MonthlyContribution = need.NewMonthlyContribution;

                obj.CurrentAmount = need.CurrentAmount;
                obj.CurrentAge = need.CurrentAge;
                obj.InvestmentAge = need.InvestmentAge;

                obj.InflationPercentage = need.InflationPercentage;
                obj.EscalationPercentage = (double)need.EscalationPercentage;
                obj.GrowthPercentage = need.GrowthPercentage;

                obj.ReferenceNo = need.ReferenceNo;
                obj.ReferenceId = need.Id;
                obj.Status = need.Status;

                obj.DependentName = need.DependentName;
                obj.DependentDOB = need.DependentDOB;

                obj.DependentName = need.Insured; //YJ 10/02/2020

                obj.UpdateDate = DateTime.Now;
                obj.UpdateBy = Program.User.Username;

                obj.Funds.Clear();
                foreach (var fund in need.Funds)
                {
                    Fund f = fund.ToObject<Fund>();
                    f.Id = 0;
                    f.CurrentAmount = fund.NewFundValue;

                    obj.Funds.Add(f);
                }

                obj.Beneficiaries.Clear();
                foreach (var beneficiary in need.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    obj.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                obj.Calculate();
                obj.IsLoading = false;
            }


        }
        internal static void UpdateNeed(this InvestmentNeed need, ref Investment obj, PolicyAction action)
        {
            //if (need.Equals(obj))
            //    return;

            if (obj == null)
                obj = new Investment();

            obj.IsLoading = true;
            try
            {
                obj.Type = need.Type;
                obj.Description = need.Description;
                obj.InitialAmount = need.InitialAmount;
                obj.MonthlyContribution = need.NewMonthlyContribution;

                obj.CurrentAmount = need.CurrentAmount;
                obj.CurrentAge = need.CurrentAge;
                obj.InvestmentAge = need.InvestmentAge;

                obj.InflationPercentage = need.InflationPercentage;
                obj.EscalationPercentage = (double)need.EscalationPercentage;
                obj.GrowthPercentage = need.GrowthPercentage;

                obj.ReferenceNo = need.ReferenceNo;
                obj.ReferenceId = need.Id;
                obj.Status = need.Status;

                obj.Insured = need.Insured; //YJ 10/02/2020


                obj.UpdateDate = DateTime.Now;
                obj.UpdateBy = Program.User.Username;

                obj.Funds.Clear();
                foreach (var fund in need.Funds)
                {
                    Fund f = fund.ToObject<Fund>();
                    f.Id = 0;
                    f.CurrentAmount = fund.NewFundValue;

                    obj.Funds.Add(f);
                }

                obj.Beneficiaries.Clear();
                foreach (var beneficiary in need.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    obj.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                obj.Calculate();
                obj.IsLoading = false;
            }


        }

        internal static void UpdateNeed(this RiskCoverNeed need, ref Life obj, PolicyAction action)
        {
            //if (need.Equals(obj))
            //    return;

            if (obj == null)
                obj = new Life();

            obj.IsLoading = true;
            try
            {
                obj.Type = need.CoverType;
                obj.Description = need.Type;
                obj.InitialAmount = need.InitialAmount;
                obj.MonthlyContribution = need.MonthlyContribution;

                obj.CurrentAmount = need.CurrentAmount;
                obj.CurrentAge = need.CurrentAge;
                obj.InvestmentAge = need.InvestmentAge;

                obj.InflationPercentage = need.InflationPercentage;
                obj.EscalationPercentage = (double)need.EscalationPercentage;
                obj.GrowthPercentage = need.GrowthPercentage;

                obj.ReferenceNo = need.ReferenceNo;
                obj.ReferenceId = need.Id;
                obj.Status = need.Status;

                obj.Insured = need.Insured; //YJ 10/02/2020


                obj.UpdateDate = DateTime.Now;
                obj.UpdateBy = Program.User.Username;

                obj.Benefits.Clear();
                foreach (var fund in need.Benefits)
                {
                    Benefit f = fund.ToObject<Benefit>();
                    f.Id = 0;
                    f.CoverAmount = fund.CoverAmount;

                    obj.Benefits.Add(f);
                }

                obj.Beneficiaries.Clear();
                foreach (var beneficiary in need.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    obj.Beneficiaries.Add(f);
                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                obj.Calculate();
                obj.IsLoading = false;
            }


        }
    }
}
