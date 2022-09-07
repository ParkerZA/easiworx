
using my.domain.lib.core.Domain;
using Finx.App.Enums;
using Finx.App.Extensions;
using Finx.Domain.Entities;

using MetroFramework;
using MetroFramework.Controls.Ext;
using MetroFramework.Forms;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using SourceGrid;

namespace Finx.App.Forms
{
    public partial class frmMetroPolicyHistory : MetroForm
    {
        #region Delegates and Events
        //Fna Need
        public delegate Need GetFnaNeedEventHandler(object sender, EventArgs args);
        public event GetFnaNeedEventHandler GetFnaNeedEvent;
        public delegate void UpdateFnaNeedEventHandler(object sender, EventArgs args);
        public event UpdateFnaNeedEventHandler UpdateFnaNeedEvent;

        //Policy Need/Amendment
        public delegate Need GetPolicyNeedEventHandler(object sender, EventArgs args);
        public event GetPolicyNeedEventHandler GetPolicyNeedEvent;
        public delegate void  UpdatePolicyNeedEventHandler(object sender, Need need, EventArgs args);
        public event UpdatePolicyNeedEventHandler UpdatePolicyNeedEvent;

        //Admin Task/Instruction
        public delegate Instruction GetInstructionEventHandler(object sender, EventArgs args);
        public event GetInstructionEventHandler GetInstructionEvent;
        public event EventHandler AddClientInstructionEvent;
        public event EventHandler UpdateClientInstructionEvent;

        //Policy
        public delegate void UpdatePolicyEventHandler(object sender, EventArgs args);
        public event UpdatePolicyEventHandler UpdatePolicyEvent;
        public event EventHandler UpdateClientPortfoilioEvent;

        //Updates a Need directly
        public event EventHandler UpdateNeedEvent;

        #endregion

        #region Private Variables
        bool HasChanges = false;
        bool ReadOnly = true;

        PolicyAction Action;

        Need TempNeed = null; //temp Need Object

        Retirement Retirement=null;
        Investment Investment = null;
        Education Education = null;
        Medical Medical = null;
        Life Life = null;
        IncomeAsset IncomeAsset = null;

        Need FnaNeed = null;
        EducationNeed EducationNeed = null;
        InvestmentNeed InvestmentNeed = null;
        
        Instruction Instruction = null;

        Provider ServiceProviders = (Provider)Program.Repository.List<Provider, int>(null).FirstOrDefault();
        Lisp Lisp;
        List<ListDataItem> LispFunds = new List<ListDataItem>();


        frmMetroPolicyNotes frmPolicyNotes = null;

        #endregion

        #region Constructors
        /// <summary>
        /// Base Constructor
        /// </summary>
        /// <param name="action"></param>
        /// <param name="lispName"></param>
        /// <param name="readOnly"></param>
        internal frmMetroPolicyHistory(PolicyAction action,string lispName="", bool readOnly = true)
        {
            InitializeComponent();

            ReadOnly = readOnly;
            Action = action;

            this.Format("Policy Amendments History");

            #region xToolbar

            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);
            this.xToolBarMenu1.tbCaptionImage.Image = Properties.Resources.cog_32;
            #endregion

            #region Collections for DropdownComboxes
            if (ServiceProviders != null && !string.IsNullOrEmpty(lispName))
            {

                //Lisp Funds
                Lisp = ServiceProviders.LispProviders.Lisps.Where(x => x.LispName.ToUpper() == lispName.ToUpper())
                            .FirstOrDefault();
                if (Lisp != null)
                    LispFunds = Lisp.LispFunds.ToListDataItem("FundName", "FundName"); // the list of funds for the selected LISP
            }
            #endregion

            #region Initialise TabControl
            this.metroTabControl_PolicyMain.TabPages[0].Format("Funds");
            this.metroTabControl_PolicyMain.TabPages[1].Format("Notes");

            this.metroTabControl_PolicyMain.SelectedIndex = 0;
            #endregion

        }

        public frmMetroPolicyHistory(Retirement model, PolicyAction action, bool readOnly = true) : this(action, model.Description, readOnly)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Retirement = model;

            InitialiseForm();
        }

        public frmMetroPolicyHistory(Investment model, PolicyAction action, bool readOnly = true) : this(action, model.Description, readOnly)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Investment = model;

            InitialiseForm();
        }

        public frmMetroPolicyHistory(Education model, PolicyAction action, bool readOnly = true) : this(action, model.Description, readOnly)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Education = model;

            InitialiseForm();
        }

        public frmMetroPolicyHistory(Medical model, PolicyAction action, bool readOnly = true) : this(action, model.Type, readOnly)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Medical = model;

            this.metroTabControl_PolicyMain.TabPages[0].Format("Benefits");

            InitialiseForm();

            
        }

        public frmMetroPolicyHistory(Life model, PolicyAction action, bool readOnly = true) : this(action, model.Type, readOnly)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            Life = model;

            this.metroTabControl_PolicyMain.TabPages[0].Format("Benefits");

            InitialiseForm();

        }

        public frmMetroPolicyHistory(IncomeAsset model, PolicyAction action, bool readOnly = true) : this(action, model.Type, readOnly)
        {
            if (model.Id == 0) //TO DO : This may be a custom instruction not linked to a need
                throw new MyValidationException("Policy has not yet been saved. Please Update policy");

            IncomeAsset = model;

            this.metroTabControl_PolicyMain.TabPages[0].Format("Funds");

            InitialiseForm();

        }

        #endregion

        #region Initialisation
        void InitialiseGrid(IList<Need> model)
        {
            TempNeed = new Need();
            TempNeed.PropertyChanged += Need_PropertyChanged;

            this.dataGrid_PolicyDetails.Initialise<Need>(model, column =>
            {
                column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
                column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                column.For(c => c.InitialAmount, "Lump Sum / Withdrawals", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(true));
                column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
                column.For(c => c.FutureAmount, "Retirement Value", new MetroCurrencyEditor().ReadOnly(true));
                column.For(c => c.Status, "Policy Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(true));//Action != PolicyAction.AmendPolicy ? true : ReadOnly

           },
            // PropertyChangedHandler: ClientPortfolio_propertyChanged_EventHandler, //event handler when a property changes
            RowSelectEventHandler: Need_PropertyChanged_Event,//for the Policy Funds details view
            //ContextMenu: new DataGridContextMenu(this, ContextMenuType.RetirementPortfolio, ReadOnly, client),
            ReadOnly: false,
            AllowDelete: false,
            AllowAddNew:true
           
            ).Formatt(AlternateBackground:true);

        }

        //void InitialiseGrid(Investment model)
        //{
        //    //if (TempNeed == null)
        //    //{
        //    //    if (Action == PolicyAction.AmendPolicy)
        //    //    {
        //    //        TempNeed = GetPolicyNeedEvent?.Invoke(model, new EventArgs());

        //    //        if (TempNeed != null)
        //    //            TempNeed.IsUpdate = true;
        //    //    }

        //    //    if (TempNeed == null)
        //    //        TempNeed = model.CloneAsNeed(Action);

        //    //}
        //    TempNeed = GetPolicyNeedEvent?.Invoke(model, new EventArgs());

        //    if (TempNeed != null)
        //        TempNeed.IsUpdate = true;
        //    else
        //        TempNeed = model.CloneAsNeed(Action);

        //    TempNeed.PropertyChanged -= Need_PropertyChanged;

        //    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //    {
        //        column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
        //        //column.For(x => x.Type, "Investment Type", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.RetirementAssetClass)).ReadOnly(ReadOnly));
        //        // column.For(x => x.Description, "LISP", new MetroComboBoxEditor().DataSourceList(Lisps).ReadOnly(true));
        //        //column.For(x => x.Description, "LISP", new MetroTextBoxEditor().ReadOnly(true));
        //        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
        //        column.For(c => c.FutureAmount, "Retirement Value", new MetroCurrencyEditor().ReadOnly(true));
        //        column.For(c => c.Status, "Policy Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(true));

        //    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //  //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //  ReadOnly: ReadOnly,
        //  AllowDelete: false,
        //  AllowAddNew: false)
        //  .Formatt(ReadOnly);

        //    this.dataGrid_PolicyFunds.Initialise(TempNeed.Funds, columns =>
        //    {
        //        columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //        columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(Action == PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
        //        columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
        //        columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
        //    }, PropertyChangedHandler: Fund_propertyChanged_EventHandler,
        //        ItemDeleteEventHandler: Fund_propertyDelete_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //        AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
        //        AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyNotes.Initialise(TempNeed.Notes, columns =>
        //    {
        //        columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
        //    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
        //   //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //   ReadOnly: ReadOnly,
        //   AllowDelete: true)
        //   .Formatt(ReadOnly);

        //    TempNeed.PropertyChanged += Need_PropertyChanged;
        //}
        //void InitialiseGrid(Education model)
        //{
        //    //if (TempNeed == null)
        //    //{
        //    //    if (Action == PolicyAction.AmendPolicy)
        //    //    {
        //    //        TempNeed = GetPolicyNeedEvent?.Invoke(model, new EventArgs());

        //    //        if (TempNeed != null)
        //    //            TempNeed.IsUpdate = true;
        //    //    }

        //    //    if (TempNeed == null)
        //    //        TempNeed = model.CloneAsNeed(Action);



        //    //}

        //    TempNeed = GetPolicyNeedEvent?.Invoke(model, new EventArgs());

        //    if (TempNeed != null)
        //        TempNeed.IsUpdate = true;
        //    else
        //        TempNeed = model.CloneAsNeed(Action);

        //    TempNeed.PropertyChanged -= Need_PropertyChanged;
        //    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //    {
        //        column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
        //        //column.For(x => x.Type, "Investment Type", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.RetirementAssetClass)).ReadOnly(ReadOnly));
        //        // column.For(x => x.Description, "LISP", new MetroComboBoxEditor().DataSourceList(Lisps).ReadOnly(true));
        //        //column.For(x => x.Description, "LISP", new MetroTextBoxEditor().ReadOnly(true));
        //        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
        //        column.For(c => c.FutureAmount, "Future Value", new MetroCurrencyEditor().ReadOnly(true));
        //        column.For(c => c.Status, "Policy Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(true));

        //    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //   //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //   ReadOnly: ReadOnly,
        //   AllowDelete: false,
        //   AllowAddNew: false)
        //   .Formatt(ReadOnly);

        //    this.dataGrid_PolicyFunds.Initialise(TempNeed.Funds, columns =>
        //    {
        //        columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //        columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
        //        columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
        //        columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
        //    }, PropertyChangedHandler: Fund_propertyChanged_EventHandler,
        //        ItemDeleteEventHandler: Fund_propertyDelete_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //         AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
        //        AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyNotes.Initialise(TempNeed.Notes, columns =>
        //    {
        //        columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
        //    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
        //    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //    ReadOnly: ReadOnly,
        //    AllowDelete: true)
        //    .Formatt(ReadOnly);

        //    TempNeed.PropertyChanged += Need_PropertyChanged;
        //}
        //void InitialiseGrid(Medical model)
        //{
        //    //if (TempNeed == null)
        //    //{
        //    //    if (Action == PolicyAction.AmendPolicy)
        //    //    {
        //    //        TempNeed = GetPolicyNeedEvent?.Invoke(model, new EventArgs());

        //    //        if (TempNeed != null)
        //    //            TempNeed.IsUpdate = true;
        //    //    }

        //    //    if (TempNeed == null)
        //    //        TempNeed = model.CloneAsNeed(Action);



        //    //}

        //    TempNeed = GetPolicyNeedEvent?.Invoke(model, new EventArgs());

        //    if (TempNeed != null)
        //        TempNeed.IsUpdate = true;
        //    else
        //        TempNeed = model.CloneAsNeed(Action);

        //    TempNeed.PropertyChanged -= Need_PropertyChanged;


        //    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //    {
        //        column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
        //        //column.For(x => x.Type, "Investment Type", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.RetirementAssetClass)).ReadOnly(ReadOnly));
        //        // column.For(x => x.Description, "LISP", new MetroComboBoxEditor().DataSourceList(Lisps).ReadOnly(true));
        //        column.For(x => x.Description, "Plan", new MetroTextBoxEditor().ReadOnly(true));
        //        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        //column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        //column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        //column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        //column.For(c => c.CurrentAmount, "Cover Amount", new MetroCurrencyEditor().ReadOnly(true));
        //        //column.For(c => c.FutureAmount, "Retirement Value", new MetroCurrencyEditor().ReadOnly(true));
        //        column.For(c => c.Status, "Policy Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(true));

        //    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //      AllowDelete: false,
        //        AllowAddNew: false)
        //        .Formatt(ReadOnly);


        //    this.dataGrid_PolicyFunds.Initialise(TempNeed.Benefits, columns =>
        //    {
        //        columns.For(x => x.Description, "Benefit Type", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
        //        //columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        //columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        //columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //        columns.For(x => x.CoverAmount, "Cover Amount", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
        //    }, PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
        //        ItemDeleteEventHandler: Benefit_propertyDelete_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //         AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
        //        AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyNotes.Initialise(TempNeed.Notes, columns =>
        //    {
        //        columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
        //    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
        //    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //    ReadOnly: ReadOnly,
        //    AllowDelete: true)
        //    .Formatt(ReadOnly);

        //    TempNeed.PropertyChanged += Need_PropertyChanged;
        //}
        //void InitialiseGrid(Life model)
        //{
        //    //if (TempNeed == null)
        //    //{
        //    //    if (Action == PolicyAction.AmendPolicy)
        //    //    {
        //    //        TempNeed = GetPolicyNeedEvent?.Invoke(model, new EventArgs());

        //    //        if (TempNeed != null)
        //    //            TempNeed.IsUpdate = true;
        //    //    }

        //    //    if (TempNeed == null)
        //    //        TempNeed = model.CloneAsNeed(Action);



        //    //}

        //    TempNeed = GetPolicyNeedEvent?.Invoke(model, new EventArgs());

        //    if (TempNeed != null)
        //        TempNeed.IsUpdate = true;
        //    else
        //        TempNeed = model.CloneAsNeed(Action);

        //    TempNeed.PropertyChanged -= Need_PropertyChanged;

        //    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //    {
        //        column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
        //        column.For(x => x.Description, "Policy Type", new MetroTextBoxEditor().ReadOnly(true));
        //        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        //column.For(c => c.InitialAmount, "Insured Amount", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.Status, "Policy Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(true));

        //    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //        AllowDelete: false,
        //        AllowAddNew: false)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyFunds.Initialise(TempNeed.Benefits, columns =>
        //    {
        //        columns.For(x => x.Type, "Benefit", new MetroComboListEditor().DataSourceList(Program.listData.List(ListDataItemType.LifeBenefit)).ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.Description, "Description", new MetroTextBoxEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.CoverAmount, "Insured Amount", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
        //    }, PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
        //        ItemDeleteEventHandler: Benefit_propertyDelete_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //        AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
        //        AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyNotes.Initialise(TempNeed.Notes, columns =>
        //    {
        //        columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
        //    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
        //    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //    ReadOnly: ReadOnly,
        //    AllowDelete: true)
        //    .Formatt(ReadOnly);

        //    TempNeed.PropertyChanged += Need_PropertyChanged;
        //}
        //void InitialiseGrid(IncomeAsset model)
        //{
        //    //if (TempNeed == null)
        //    //{
        //    //    if (Action == PolicyAction.AmendPolicy)
        //    //    {
        //    //        TempNeed = GetPolicyNeedEvent?.Invoke(model, new EventArgs());

        //    //        if (TempNeed != null)
        //    //            TempNeed.IsUpdate = true;
        //    //    }

        //    //    if (TempNeed == null)
        //    //        TempNeed = model.CloneAsNeed(Action);



        //    //}

        //    TempNeed = GetPolicyNeedEvent?.Invoke(model, new EventArgs());

        //    if (TempNeed != null)
        //        TempNeed.IsUpdate = true;
        //    else
        //        TempNeed = model.CloneAsNeed(Action);

        //    TempNeed.PropertyChanged -= Need_PropertyChanged;

        //    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //    {
        //        column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
        //        column.For(x => x.Description, "Description", new MetroTextBoxEditor().ReadOnly(true));
        //        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.MonthlyContribution, "Income p/m", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.InitialAmount, "Asset Value", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.Status, "Policy Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(true));

        //    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //        AllowDelete: false,
        //        AllowAddNew: false)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyFunds.Initialise(TempNeed.Funds, columns =>
        //    {
        //        columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.MonthlyContribution, "Income", new MetroCurrencyEditor().ReadOnly(true));
        //        columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(Action == PolicyAction.AmendPolicy ? true : ReadOnly));
        //        columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
        //       // columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
        //       // columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
        //    }, PropertyChangedHandler: Fund_propertyChanged_EventHandler,
        //   ItemDeleteEventHandler: Fund_propertyDelete_EventHandler,
        //   //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //   ReadOnly: ReadOnly,
        //   AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
        //   AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
        //   .Formatt(ReadOnly);

        //    this.dataGrid_PolicyNotes.Initialise(TempNeed.Notes, columns =>
        //    {
        //        columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
        //    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
        //    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //    ReadOnly: ReadOnly,
        //    AllowDelete: true)
        //    .Formatt(ReadOnly);

        //    TempNeed.PropertyChanged += Need_PropertyChanged;
        //}

        //void InitialiseGrid(Need model)
        //{
        //    if (TempNeed == null)
        //        TempNeed = model;//.CloneAsNeed(Action);

        //    if (TempNeed.Status == "Completed" || TempNeed.Status == "Implemented")
        //        ReadOnly = true;

        //    TempNeed.PropertyChanged -= Need_PropertyChanged;

        //    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //    {
        //        column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
        //       // column.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));// new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.RetirementAssetClass)).ReadOnly(ReadOnly));
        //       // column.For(x => x.Description, "LISP", new MetroComboBoxEditor().DataSourceList(Lisps).ReadOnly(true));
        //        //column.For(x => x.Description, "LISP", new MetroTextBoxEditor().ReadOnly(true));
        //        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //        // column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
        //        column.For(c => c.FutureAmount, "Future Value", new MetroCurrencyEditor().ReadOnly(true));
        //        column.For(c => c.Status, "Advice Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.NeedStatus)).ReadOnly(ReadOnly));
        //        column.For(c => c.UpdateDate, "Updated On", new MetroDateEditor().ReadOnly(true));
        //    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //       AllowDelete: false,
        //        AllowAddNew: false)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyFunds.Initialise(TempNeed.Funds, columns =>
        //    {
        //        columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //        //columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
        //        columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
        //        columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
        //        columns.For(c => c.UpdateDate, "Updated On", new MetroDateEditor().ReadOnly(true));
        //    }, PropertyChangedHandler: Fund_propertyChanged_EventHandler,
        //        ItemDeleteEventHandler: Fund_propertyDelete_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //       AllowDelete: true,
        //        AllowAddNew: true)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyNotes.Initialise(TempNeed.Notes, columns =>
        //    {
        //        columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
        //    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
        //     //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //     ReadOnly: ReadOnly,
        //     AllowDelete: true)
        //     .Formatt(ReadOnly);

        //    TempNeed.PropertyChanged += Need_PropertyChanged;
        //}
        //void InitialiseGrid(EducationNeed model)
        //{
        //    if (TempNeed == null)
        //        TempNeed = model.CloneAsNeed(Action);

        //    if (TempNeed.Status == "Completed" || TempNeed.Status == "Implemented")
        //        ReadOnly = true;

        //    TempNeed.PropertyChanged -= Need_PropertyChanged;

        //    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //    {
        //        column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
        //        //column.For(x => x.Type, "Investment Type", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.RetirementAssetClass)).ReadOnly(ReadOnly));
        //        // column.For(x => x.Description, "LISP", new MetroComboBoxEditor().DataSourceList(Lisps).ReadOnly(true));
        //        //column.For(x => x.Description, "LISP", new MetroTextBoxEditor().ReadOnly(true));
        //        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
        //        column.For(c => c.FutureAmount, "Future Value", new MetroCurrencyEditor().ReadOnly(true));
        //        //column.For(c => c.Status, "Advise Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.PolicyStatus)).ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //        column.For(c => c.Status, "Advise Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.NeedStatus)).ReadOnly(ReadOnly));
        //    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //        AllowDelete: false,
        //        AllowAddNew: false)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyFunds.Initialise(TempNeed.Funds, columns =>
        //    {
        //        columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //        columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //       // columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
        //        columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
        //        columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
        //    }, PropertyChangedHandler: Fund_propertyChanged_EventHandler,
        //        ItemDeleteEventHandler: Fund_propertyDelete_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //        AllowDelete: true)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyNotes.Initialise(TempNeed.Notes, columns =>
        //    {
        //        columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
        //    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
        //    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //    ReadOnly: ReadOnly,
        //    AllowDelete: true)
        //    .Formatt(ReadOnly);

        //    TempNeed.PropertyChanged += Need_PropertyChanged;
        //}
        //void InitialiseGrid(InvestmentNeed model)
        //{
        //    if (TempNeed == null)
        //        TempNeed = model.CloneAsNeed(Action);

        //    if (TempNeed.Status == "Completed" || TempNeed.Status == "Implemented")
        //        ReadOnly = true;

        //    TempNeed.PropertyChanged -= Need_PropertyChanged;

        //    this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //    {
        //        column.For(x => x.CreateDate, "Create Dt", new MetroDateEditor().ReadOnly(true));
        //        //column.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));// new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.RetirementAssetClass)).ReadOnly(ReadOnly));
        //                                                                                 // column.For(x => x.Description, "LISP", new MetroComboBoxEditor().DataSourceList(Lisps).ReadOnly(true));
        //                                                                                 //column.For(x => x.Description, "LISP", new MetroTextBoxEditor().ReadOnly(true));
        //        column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //        column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //        // column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
        //        column.For(c => c.FutureAmount, "Future Value", new MetroCurrencyEditor().ReadOnly(true));
        //        //column.For(c => c.Status, "Advise Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.NeedStatus)).ReadOnly(true));
        //        column.For(c => c.Status, "Advise Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.NeedStatus)).ReadOnly(ReadOnly));
        //        column.For(c => c.UpdateDate, "Updated On", new MetroDateEditor().ReadOnly(true));
        //    }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //       AllowDelete: false,
        //        AllowAddNew: false)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyFunds.Initialise(TempNeed.Funds, columns =>
        //    {
        //        columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //        columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //        //columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //        //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
        //        columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
        //        columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
        //        columns.For(c => c.UpdateDate, "Updated On", new MetroDateEditor().ReadOnly(true));
        //    }, PropertyChangedHandler: Fund_propertyChanged_EventHandler,
        //        ItemDeleteEventHandler: Fund_propertyDelete_EventHandler,
        //        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //        ReadOnly: ReadOnly,
        //       AllowDelete: true,
        //        AllowAddNew: true)
        //        .Formatt(ReadOnly);

        //    this.dataGrid_PolicyNotes.Initialise(TempNeed.Notes, columns =>
        //    {
        //        columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
        //    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
        //     //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //     ReadOnly: ReadOnly,
        //     AllowDelete: true)
        //     .Formatt(ReadOnly);

        //    TempNeed.PropertyChanged += Need_PropertyChanged;
        //}

        //void InitialiseGrid(Instruction model)
        //{

        //    if (TempNeed.Status == "Completed" || TempNeed.Status == "Implemented")
        //        ReadOnly = true;

        //    TempNeed.PropertyChanged -= Need_PropertyChanged;

        //    switch (TempNeed.NeedType)
        //    {
        //        case Domain.NeedTypes.LifeDisabilityNeed:

        //            this.metroTabControl_PolicyMain.TabPages[0].Format("Benefits");

        //            this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //            {

        //                column.For(x => x.Type, "LISP", new MetroTextBoxEditor().ReadOnly(true));
        //                column.For(x => x.Description, "Policy Type", new MetroTextBoxEditor().ReadOnly(true));
        //                column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(ReadOnly));
        //                //column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(true));
        //                column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //                column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //                //column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //               // column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
        //                //column.For(c => c.FutureAmount, "Retirement Value", new MetroCurrencyEditor().ReadOnly(true));
        //                column.For(c => c.Status, "Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.InstructionStatus)).ReadOnly(Action == PolicyAction.UpdateInstruction ? false : ReadOnly));

        //             }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //             //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //             ReadOnly: TempNeed.Status == NeedStatus.Cancelled.ToText() ? true : ReadOnly,
        //             AllowDelete: false,
        //             AllowAddNew: false)
        //             .Formatt(ReadOnly);

        //            this.dataGrid_PolicyFunds.Initialise(TempNeed.Benefits, columns =>
        //            {
        //                columns.For(x => x.Type, "Benefit", new MetroComboListEditor().DataSourceList(Program.listData.List(ListDataItemType.LifeBenefit)).ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //                columns.For(x => x.Description, "Description", new MetroTextBoxEditor().ReadOnly(ReadOnly));
        //                columns.For(x => x.CoverAmount, "Insured Amount", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //                columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
        //            }, PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
        //              ItemDeleteEventHandler: Benefit_propertyDelete_EventHandler,
        //              //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //              ReadOnly: ReadOnly,
        //              AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
        //              AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
        //              .Formatt(ReadOnly);
        //            break;

        //        case Domain.NeedTypes.MedicalNeed:

        //            this.metroTabControl_PolicyMain.TabPages[0].Format("Benefits");

        //            this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //            {
        //                column.For(x => x.Type, "LISP", new MetroTextBoxEditor().ReadOnly(true));
        //                column.For(x => x.Description, "Plan", new MetroTextBoxEditor().ReadOnly(true));
        //                column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(ReadOnly));
        //               // column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(true));
        //                column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //                column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //                //column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //               // column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
        //                //column.For(c => c.FutureAmount, "Retirement Value", new MetroCurrencyEditor().ReadOnly(true));
        //                column.For(c => c.Status, "Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.InstructionStatus)).ReadOnly(Action == PolicyAction.UpdateInstruction ? false : ReadOnly));

        //            }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //             //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //             ReadOnly: TempNeed.Status == NeedStatus.Cancelled.ToText() ? true : ReadOnly,
        //             AllowDelete: false,
        //             AllowAddNew: false)
        //             .Formatt(ReadOnly);


        //            this.dataGrid_PolicyFunds.Initialise(TempNeed.Benefits, columns =>
        //            {
        //                columns.For(x => x.Description, "Benefit Type", new MetroTextBoxEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //                columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
        //                //columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //                //columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
        //                //columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //                columns.For(x => x.CoverAmount, "Cover Amount", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //                columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
        //            }, PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
        //               ItemDeleteEventHandler: Benefit_propertyDelete_EventHandler,
        //               //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //               ReadOnly: ReadOnly,
        //                AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
        //               AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
        //               .Formatt(ReadOnly);
        //            break;
        //        default:

        //            this.dataGrid_PolicyDetails.Initialise(TempNeed, column =>
        //            {
        //                column.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
        //                column.For(x => x.Description, "LISP", new MetroTextBoxEditor().ReadOnly(true));
        //                column.For(c => c.ReferenceNo, "Policy No.", new MetroTextBoxEditor().ReadOnly(ReadOnly));
        //                column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(true));
        //                column.For(c => c.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //                column.For(c => c.EscalationPercentage, "Escalation", new MetroPercentageEditor().ReadOnly(true));
        //                //column.For(c => c.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
        //                column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(true));
        //                //column.For(c => c.FutureAmount, "Retirement Value", new MetroCurrencyEditor().ReadOnly(true));
        //                column.For(c => c.Status, "Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.InstructionStatus)).ReadOnly(Action == PolicyAction.UpdateInstruction ? false : ReadOnly));

        //            }, PropertyChangedHandler: Need_propertyChanged_EventHandler,
        //             //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //             ReadOnly: TempNeed.Status == NeedStatus.Cancelled.ToText() ? true : ReadOnly,
        //             AllowDelete: false,
        //             AllowAddNew: false)
        //             .Formatt(ReadOnly);

        //            this.dataGrid_PolicyFunds.Initialise(TempNeed.Funds, columns =>
        //            {
        //                columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(true));
        //                columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
        //                columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(true));
        //                columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(true));
        //                columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
        //                columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(ReadOnly));
        //                //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
        //                columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
        //                columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
        //            }, PropertyChangedHandler: Fund_propertyChanged_EventHandler,
        //           ItemDeleteEventHandler: Fund_propertyDelete_EventHandler,
        //           //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //           ReadOnly: TempNeed.Status == NeedStatus.Cancelled.ToText() ? true : ReadOnly,
        //           AllowDelete: false,
        //           AllowAddNew: false)
        //           .Formatt(ReadOnly);
        //            break;
        //    }


        //    this.dataGrid_PolicyNotes.Initialise(TempNeed.Notes, columns =>
        //    {
        //        columns.For(x => x.CreateDate, "Date", new MetroDateEditor(175).ReadOnly(true));
        //        //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
        //        columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
        //        columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
        //    }, PropertyChangedHandler: Note_propertyChanged_EventHandler,
        //    //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
        //    ReadOnly: ReadOnly,
        //    AllowDelete: true)
        //    .Formatt(ReadOnly);

        //    TempNeed.PropertyChanged += Need_PropertyChanged;
        //}
        #endregion

        #region  Form Events
        private void frmMetroPolicyFunds_Load(object sender, EventArgs e)
        {

            if (Retirement != null)
            {
                this.xToolBarMenu1.tbCaption.Text = Retirement.Description;
                InitialiseGrid(Retirement.Amendments);

            }
            if (Investment != null)
            {
                this.xToolBarMenu1.tbCaption.Text = Investment.Description;
                InitialiseGrid(Investment.Amendments);

            }

            if (Education != null)
            {
                this.xToolBarMenu1.tbCaption.Text = Education.Description;
                InitialiseGrid(Education.Amendments);

            }

            if (Medical != null)
            {
                this.xToolBarMenu1.tbCaption.Text = Medical.Type;
                InitialiseGrid(Medical.Amendments);

            }

            if (Life != null)
            {
                this.xToolBarMenu1.tbCaption.Text = Life.Type;
                InitialiseGrid(Life.Amendments);

            }

            if (IncomeAsset != null)
            {
                this.xToolBarMenu1.tbCaption.Text = IncomeAsset.Type;
                InitialiseGrid(IncomeAsset.Amendments);

            }

            if (TempNeed != null)
                TempNeed.LispProvider = ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == TempNeed.Description).FirstOrDefault();

            UpdateToolBar();
        }
        void InitialiseForm()
        {
            xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Enabled = false;
            xToolBarMenu1.tbRefresh.Visible = false; xToolBarMenu1.tbRefresh.Enabled = false; xToolBarMenu1.tbRefresh.Text = "Cancel";
            xToolBarMenu1.tbSave.Visible = true; xToolBarMenu1.tbSave.Enabled = false;

            xToolBarMenu1.EditClicked -= toolStripButton_Edit_Click;
            xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;

            xToolBarMenu1.RefreshClicked -= toolStripButton_Cancel_Click;
            xToolBarMenu1.RefreshClicked += toolStripButton_Cancel_Click;

            xToolBarMenu1.SaveClicked -= toolStripButton_SaveClicked;
            xToolBarMenu1.SaveClicked += toolStripButton_SaveClicked;

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

            xToolBarMenu1.tbEdit.Visible = true;
            xToolBarMenu1.tbEdit.Text = "Edit";
            xToolBarMenu1.tbEdit.Enabled = !ReadOnly;//&& HasChanges;

          
        }
        #endregion

        #region toolStripButton ClickEvents
        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                TempNeed.Validate();

                switch (xToolBarMenu1.tbEdit.Text)
                {
                    case "Accept":
                        if (MessageBoxExt.ShowQuestion(string.Format("Are you sure you wish to Accept this policy ?")))
                        {
                            TempNeed.Status = NeedStatus.Accepted.ToText();
                            TempNeed.IsCancelled = false;

                            //Save the changes
                            if (FnaNeed != null)
                            {
                                TempNeed.UpdateNeed(ref FnaNeed, Action);
                                UpdateFnaNeedEvent?.Invoke(FnaNeed, new EventArgs());
                                TempNeed.Id = FnaNeed.Id;
                            }
                            if (EducationNeed != null)
                            {
                                TempNeed.UpdateNeed(ref EducationNeed, Action);
                                UpdateNeedEvent?.Invoke(EducationNeed, new EventArgs());
                                TempNeed.Id = EducationNeed.Id;
                            }
                            if (InvestmentNeed != null)
                            {
                                TempNeed.UpdateNeed(ref InvestmentNeed, Action);
                                UpdateNeedEvent?.Invoke(InvestmentNeed, new EventArgs());
                                TempNeed.Id = InvestmentNeed.Id;
                            }


                            if (Instruction == null)
                            {
                                Instruction = new Instruction()
                                {
                                    Status = InstructionStatus.AddPending.ToText(),
                                    Type = InstructionType.POLICY_CREATE.ToText(),
                                    Comment = TempNeed.Description,
                                    ReferenceId = TempNeed.Id,
                                    ReferenceNo = TempNeed.ReferenceNo,

                                    UpdateDate = DateTime.Now,
                                    UpdateBy = Program.User.Username
                                };

                                //Add the Admin Task
                                AddClientInstructionEvent?.Invoke(Instruction, new EventArgs());
                            }
                            else
                            {
                                Instruction.Status = InstructionStatus.AddPending.ToText();
                                Instruction.Comment = TempNeed.Description;
                                Instruction.ReferenceId = TempNeed.Id;
                                Instruction.ReferenceNo = TempNeed.ReferenceNo;

                                Instruction.UpdateDate = DateTime.Now;
                                Instruction.UpdateBy = Program.User.Username;

                                //Update the Admin Task
                                UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());
                            }

                            if (FnaNeed != null)
                            {
                                FnaNeed.InstructionId = Instruction.Id;
                            }
                            if (EducationNeed != null)
                            {
                                EducationNeed.InstructionId = Instruction.Id;
                            }
                            if (InvestmentNeed != null)
                            {
                                InvestmentNeed.InstructionId = Instruction.Id;
                            }
                           

                        }
                        break;
                    case "Re-open":
                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Re-open this policy ?"))
                        {
                            TempNeed.Status = NeedStatus.InProgress.ToText();
                        }
                        break;
                    case "Submit for Authorisation":
                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Submit this policy for Authorisation ?"))
                        {
                            TempNeed.Validate("ReferenceNo");
                            TempNeed.Validate("startdate");

                            TempNeed.Status = NeedStatus.ReadyForAuth.ToText();
                        }
                        break;
                    case "Authorise":
                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Authorise this policy ?"))
                        {
                            TempNeed.Validate("ReferenceNo");
                            TempNeed.Validate("startdate");

                            TempNeed.Status = NeedStatus.Authorised.ToText();
                        }
                        break;
                    case "Implement":
                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Add this policy to the Client's Portfolio ?"))
                        {
                            TempNeed.Validate("ReferenceNo");
                            TempNeed.Validate("startdate");

                            TempNeed.Status = NeedStatus.Implemented.ToText();

                            switch (TempNeed.NeedType)
                            {
                                case Domain.NeedTypes.RetirementNeed:
                                    TempNeed.UpdateNeed(ref Retirement, Action);
                                    UpdateClientPortfoilioEvent?.Invoke(Retirement, new EventArgs());
                                    break;
                                case Domain.NeedTypes.InvestmentNeed:
                                    TempNeed.UpdateNeed(ref Investment, Action);
                                    UpdateClientPortfoilioEvent?.Invoke(Investment, new EventArgs());
                                    break;
                                case Domain.NeedTypes.EducationNeed:
                                    TempNeed.UpdateNeed(ref Education, Action);
                                    Education.DependentDOB = EducationNeed.DependentDOB;
                                    Education.DependentName = EducationNeed.DependentName;
                                    UpdateClientPortfoilioEvent?.Invoke(Education, new EventArgs());
                                    break;
                                case Domain.NeedTypes.MedicalNeed:
                                    TempNeed.UpdateNeed(ref Medical, Action);
                                    UpdateClientPortfoilioEvent?.Invoke(Medical, new EventArgs());
                                    break;
                                case Domain.NeedTypes.LifeDisabilityNeed:
                                    TempNeed.UpdateNeed(ref Life, Action);
                                    UpdateClientPortfoilioEvent?.Invoke(Life, new EventArgs());
                                    break;
                                case Domain.NeedTypes.IncomeAssetNeed:
                                    TempNeed.UpdateNeed(ref IncomeAsset, Action);
                                    UpdateClientPortfoilioEvent?.Invoke(IncomeAsset, new EventArgs());
                                    break;
                                case Domain.NeedTypes.NonRetirementNeed:
                                    break;
                            }

                        }
                        break;
                }
                //Save the Changes
                xToolBarMenu1.tbSave_Click(sender, e);

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
        private void toolStripButton_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Cancel this task ?"))
                {
                TempNeed.Status = NeedStatus.Cancelled.ToText();
                TempNeed.IsCancelled = true;

                //Save the Changes
                xToolBarMenu1.tbSave_Click(sender, e);

               }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
         
        }
        private void toolStripButton_SaveClicked(object sender, EventArgs e)
        {
            try
            {
                //Force any updates to the grids on lost focus
              //  this.dataGrid_PolicyDetails;
              //  this.dataGrid_PolicyFunds
              //  this.dataGrid_PolicyNotes.EndEditingRow(false);
           
          
                #region Policy
                if (Retirement != null)
                {
                    TempNeed.UpdateNeed(ref Retirement, Action);
                    UpdatePolicyEvent?.Invoke(Retirement, new EventArgs());
                }

                if (Investment != null)
                {
                    TempNeed.UpdateNeed(ref Investment, Action);
                    UpdatePolicyEvent?.Invoke(Investment, new EventArgs());

                }

                if (Education != null)
                {
                    TempNeed.UpdateNeed(ref Education, Action);
                    UpdatePolicyEvent?.Invoke(Education, new EventArgs());

                }

                if (Medical != null)
                {
                    TempNeed.UpdateNeed(ref Medical, Action);
                    UpdatePolicyEvent?.Invoke(Medical, new EventArgs());
                }

                if (Life != null)
                {
                    TempNeed.UpdateNeed(ref Life, Action);
                    UpdatePolicyEvent?.Invoke(Life, new EventArgs());
                }
                if (IncomeAsset != null)
                {
                    TempNeed.UpdateNeed(ref IncomeAsset, Action);
                    UpdatePolicyEvent?.Invoke(IncomeAsset, new EventArgs());
                }
                #endregion

                #region Needs
                if (FnaNeed != null)
                {
                    TempNeed.UpdateNeed(ref FnaNeed, Action);
                    UpdateFnaNeedEvent?.Invoke(FnaNeed, new EventArgs());
                }

                if (InvestmentNeed != null)
                {
                    TempNeed.UpdateNeed(ref InvestmentNeed, Action);
                    UpdateNeedEvent?.Invoke(InvestmentNeed, new EventArgs());

                }
                if (EducationNeed != null)
                {
                    TempNeed.UpdateNeed(ref EducationNeed, Action);
                    UpdateNeedEvent?.Invoke(EducationNeed, new EventArgs());

                }
                #endregion

                #region Instruction
                if (Instruction != null && TempNeed != null)
                {
                    //set the instruction status
                    switch (TempNeed.Status.ToEnum<NeedStatus>())
                    {
                        // case NeedStatus.Pending:
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

                            TempNeed.Status = NeedStatus.Implemented.ToText();

                            TempNeed.IsImplemented = true;
                            TempNeed.IsCancelled = false;

                            break;
                        case NeedStatus.Cancelled:
                            if (TempNeed.IsUpdate)
                                Instruction.Status = InstructionStatus.UpdateCancelled.ToText();
                            else
                                Instruction.Status = InstructionStatus.AddCancelled.ToText();

                            TempNeed.IsImplemented = false;
                            TempNeed.IsCancelled = true;

                            break;
                    }
                   
                    UpdateNeedEvent?.Invoke(TempNeed, new EventArgs());

                    UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());

                 

                    // InitialiseGrid(Instruction);
                }
                #endregion

                //Successfully Saved
                HasChanges = false;
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();
                this.dataGrid_PolicyNotes.Refresh();

                UpdateToolBar();
            }


        }

        private void toolStripButton_Amend_Click(object sender, EventArgs e)
        {
            try
            {
                TempNeed.Validate();

                switch (xToolBarMenu1.tbEdit.Text)
                {
                    case "Accept":
                        if (MessageBoxExt.ShowQuestion(string.Format("Are you sure you wish to Accept changes to this policy ?")))
                        {
                            #region Accept
                            TempNeed.Status = NeedStatus.Accepted.ToText();
                            TempNeed.IsCancelled = false;

                            TempNeed.Id = 0;
                            string instructionComment;

                            switch (TempNeed.NeedType)
                            {
                                case Domain.NeedTypes.RetirementNeed:
                                    UpdatePolicyNeedEvent?.Invoke(Retirement, TempNeed, new EventArgs());
                                    instructionComment = TempNeed.Description;
                                    break;
                                case Domain.NeedTypes.InvestmentNeed:
                                    UpdatePolicyNeedEvent?.Invoke(Investment, TempNeed, new EventArgs());
                                    instructionComment = TempNeed.Description;
                                    break;
                                case Domain.NeedTypes.EducationNeed:
                                    UpdatePolicyNeedEvent?.Invoke(Education, TempNeed, new EventArgs());
                                    instructionComment = TempNeed.Description;
                                    break;
                                case Domain.NeedTypes.MedicalNeed:
                                    UpdatePolicyNeedEvent?.Invoke(Medical, TempNeed, new EventArgs());
                                    instructionComment = TempNeed.Type;
                                    break;
                                case Domain.NeedTypes.LifeDisabilityNeed:
                                    UpdatePolicyNeedEvent?.Invoke(Life, TempNeed, new EventArgs());
                                    instructionComment = TempNeed.Type;
                                    break;
                                case Domain.NeedTypes.IncomeAssetNeed:
                                    UpdatePolicyNeedEvent?.Invoke(IncomeAsset, TempNeed, new EventArgs());
                                    instructionComment = TempNeed.Type;
                                    break;
                                default:
                                    throw new Exception("Unknown or missing Need Type");
                            }
                            #endregion

                            #region Instruction
                            if (Instruction == null)
                            {
                                Instruction = new Instruction()
                                {
                                    Status = InstructionStatus.UpdatePending.ToText(),
                                    Type = InstructionType.POLICY_UPDATE.ToText(),
                                    InstructionType = InstructionType.POLICY_UPDATE,
                                    Comment = instructionComment,
                                    ReferenceId = TempNeed.Id,
                                    ReferenceNo = TempNeed.ReferenceNo,

                                    UpdateDate = DateTime.Now,
                                    UpdateBy = Program.User.Username
                                };

                                //Add the Admin Task
                                AddClientInstructionEvent?.Invoke(Instruction, new EventArgs());
                            }
                            else
                            {
                                Instruction.Status = InstructionStatus.UpdatePending.ToText();
                                Instruction.Comment = instructionComment;
                                Instruction.ReferenceId = TempNeed.Id;
                                Instruction.ReferenceNo = TempNeed.ReferenceNo;

                                Instruction.UpdateDate = DateTime.Now;
                                Instruction.UpdateBy = Program.User.Username;

                                //Update the Admin Task
                                UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());
                            }

                            TempNeed.InstructionId = Instruction.Id;
                            #endregion
                        }
                        break;
                    case "Re-open":
                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Re-open this policy ?"))
                        {
                            TempNeed.Status = NeedStatus.InProgress.ToText();
                        }
                        break;
                    case "Submit for Authorisation":
                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Submit this policy for Authorisation ?"))
                        {
                            TempNeed.Validate("ReferenceNo");
                            TempNeed.Validate("startdate");

                            TempNeed.Status = NeedStatus.ReadyForAuth.ToText();
                        }
                        break;
                    case "Authorise":
                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Authorise this policy ?"))
                        {
                            TempNeed.Validate("ReferenceNo");
                            TempNeed.Validate("startdate");

                            TempNeed.Status = NeedStatus.Authorised.ToText();
                        }
                        break;
                    case "Implement":
                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Implement changes to this policy in the Client's Portfolio ?"))
                        {
                            TempNeed.Validate("ReferenceNo");
                            TempNeed.Validate("startdate");

                            #region Implement
                            TempNeed.Status = NeedStatus.Implemented.ToText();

                            switch (TempNeed.NeedType)
                            {
                                case Domain.NeedTypes.RetirementNeed:
                                    TempNeed.UpdateNeed(ref Retirement, Action);
                                    UpdateClientPortfoilioEvent?.Invoke(Retirement, new EventArgs());
                                    break;
                                case Domain.NeedTypes.InvestmentNeed:
                                    TempNeed.UpdateNeed(ref Investment, Action);
                                    UpdateClientPortfoilioEvent?.Invoke(Investment, new EventArgs());
                                    break;
                                case Domain.NeedTypes.EducationNeed:
                                    TempNeed.UpdateNeed(ref Education, Action);
                                    Education.DependentDOB = EducationNeed.DependentDOB;
                                    Education.DependentName = EducationNeed.DependentName;
                                    UpdateClientPortfoilioEvent?.Invoke(Education, new EventArgs());
                                    break;
                                case Domain.NeedTypes.MedicalNeed:
                                    TempNeed.UpdateNeed(ref Medical, Action);
                                    UpdateClientPortfoilioEvent?.Invoke(Medical, new EventArgs());
                                    break;
                                case Domain.NeedTypes.LifeDisabilityNeed:
                                    TempNeed.UpdateNeed(ref Life, Action);
                                    UpdateClientPortfoilioEvent?.Invoke(Life, new EventArgs());
                                    break;
                                case Domain.NeedTypes.IncomeAssetNeed:
                                    TempNeed.UpdateNeed(ref IncomeAsset, Action);
                                    UpdateClientPortfoilioEvent?.Invoke(IncomeAsset, new EventArgs());
                                    break;
                                case Domain.NeedTypes.NonRetirementNeed:
                                    break;
                            }
                            #endregion
                        }
                        break;
                }
                //Save the Changes
               xToolBarMenu1.tbSave_Click(sender, e);

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
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();
                this.dataGrid_PolicyNotes.Refresh();

                UpdateToolBar();
            }


        }
        private void toolStripButton_AmendCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxExt.ShowQuestion("Are you sure you wish to Cancel this Amendment ?"))
                {
                    TempNeed.Status = NeedStatus.Cancelled.ToText();
                    TempNeed.IsCancelled = true;

                    if(Retirement!=null)
                        UpdatePolicyNeedEvent?.Invoke(Retirement, TempNeed, new EventArgs());

                    if (Investment != null)
                        UpdatePolicyNeedEvent?.Invoke(Investment, TempNeed, new EventArgs());

                    if (Education != null)
                        UpdatePolicyNeedEvent?.Invoke(Education, TempNeed, new EventArgs());

                    if (Medical != null)
                        UpdatePolicyNeedEvent?.Invoke(Medical, TempNeed, new EventArgs());

                    if (Life != null)
                        UpdatePolicyNeedEvent?.Invoke(Life, TempNeed, new EventArgs());

                    if (IncomeAsset != null)
                        UpdatePolicyNeedEvent?.Invoke(IncomeAsset, TempNeed, new EventArgs());

                    //Save the Changes
                    //xToolBarMenu1.tbSave_Click(sender, e);

                }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();
                this.dataGrid_PolicyNotes.Refresh();

                UpdateToolBar();
            }

        }
        private void toolStripButton_AmendSave_Click(object sender, EventArgs e) {

            try {

                //Force any updates to the grids on lost focus
                //  this.dataGrid_PolicyDetails;
                //  this.dataGrid_PolicyFunds
                


                switch (TempNeed.Status.ToEnum<NeedStatus>())
                {
                    case NeedStatus.Completed:
                        TempNeed.Status = NeedStatus.Implemented.ToText();

                        TempNeed.IsImplemented = true;
                        TempNeed.IsCancelled = false;
                        break;
                    case NeedStatus.Cancelled:

                        TempNeed.IsImplemented = false;
                        TempNeed.IsCancelled = true;
                        break;
                };

                if (Retirement != null)
                {
                    UpdatePolicyNeedEvent?.Invoke(Retirement, TempNeed, new EventArgs());
                }

                if (Investment != null)
                {

                    UpdatePolicyNeedEvent?.Invoke(Investment, TempNeed, new EventArgs());
                }

                if (Education != null)
                {

                    UpdatePolicyNeedEvent?.Invoke(Education, TempNeed, new EventArgs());
                }

                if (Medical != null)
                {

                    UpdatePolicyNeedEvent?.Invoke(Medical, TempNeed, new EventArgs());
                }

                if (Life != null)
                {

                    UpdatePolicyNeedEvent?.Invoke(Life, TempNeed, new EventArgs());
                }

                if (IncomeAsset != null)
                {

                    UpdatePolicyNeedEvent?.Invoke(IncomeAsset, TempNeed, new EventArgs());
                }

                if (Instruction != null && TempNeed != null)
                {
                    //set the instruction status
                    switch (TempNeed.Status.ToEnum<NeedStatus>())
                    {
                        // case NeedStatus.Pending:
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

                            TempNeed.Status = NeedStatus.Implemented.ToText();

                            TempNeed.IsImplemented = true;
                            TempNeed.IsCancelled = false;

                            break;
                        case NeedStatus.Cancelled:
                            if (TempNeed.IsUpdate)
                                Instruction.Status = InstructionStatus.UpdateCancelled.ToText();
                            else
                                Instruction.Status = InstructionStatus.AddCancelled.ToText();

                            TempNeed.IsImplemented = false;
                            TempNeed.IsCancelled = true;

                            break;
                    }

                    UpdateNeedEvent?.Invoke(TempNeed, new EventArgs());

                    //if (FnaNeed != null)
                    //{
                    //    TempNeed.UpdateNeed(ref FnaNeed, Action);
                    //    UpdateFnaNeedEvent?.Invoke(FnaNeed, new EventArgs());
                    //}

                    UpdateClientInstructionEvent?.Invoke(Instruction, new EventArgs());

                    //InitialiseGrid(Instruction);
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
                this.dataGrid_PolicyDetails.Refresh();
                this.dataGrid_PolicyFunds.Refresh();
                this.dataGrid_PolicyNotes.Refresh();

                UpdateToolBar();
            }

        }

        private void XToolBarMenu1_Close_Click(object sender, EventArgs e)
        {
            if (HasChanges && xToolBarMenu1.IsInEditMode)
                if (!MessageBoxExt.ShowQuestion("Do you wish to close without saving changes"))
                    return;

            this.Close();
        }

        private void toolStripButton_Notes_Click(object sender, EventArgs e)
        {
            try
            {


                #region Policy
                if (Retirement != null)
                {
                    frmPolicyNotes = new frmMetroPolicyNotes(Retirement, ReadOnly,PolicyAction.ViewFunds);
                }

                if (Investment != null)
                {
                    frmPolicyNotes = new frmMetroPolicyNotes(Investment, ReadOnly, PolicyAction.ViewFunds);
                }

                if (Education != null)
                {
                    frmPolicyNotes = new frmMetroPolicyNotes(Education, ReadOnly, PolicyAction.ViewFunds);
                }

                if (Medical != null)
                {
                    frmPolicyNotes = new frmMetroPolicyNotes(Medical, ReadOnly, PolicyAction.ViewFunds);
                }

                if (Life != null)
                {
                    frmPolicyNotes = new frmMetroPolicyNotes(Life, ReadOnly, PolicyAction.ViewFunds);
                }
                if (IncomeAsset != null)
                {
                    frmPolicyNotes = new frmMetroPolicyNotes(IncomeAsset, ReadOnly, PolicyAction.ViewFunds);
                }
                #endregion

                #region Needs
                if (FnaNeed != null)
                {
                    frmPolicyNotes = new frmMetroPolicyNotes(FnaNeed, ReadOnly, PolicyAction.ViewFunds);
                }

                if (InvestmentNeed != null)
                {
                    frmPolicyNotes = new frmMetroPolicyNotes(InvestmentNeed, ReadOnly, PolicyAction.ViewFunds);

                }
                if (EducationNeed != null)
                {
                    frmPolicyNotes = new frmMetroPolicyNotes(EducationNeed, ReadOnly, PolicyAction.ViewFunds);

                }
                #endregion

                //if (Instruction != null)
                //{
                //    frmPolicyNotes = new frmMetroPolicyNotes(Instruction, ReadOnly);
                //}

                frmPolicyNotes.AddClientInstructionEvent += AddClientInstruction_EventHandler;
                frmPolicyNotes.UpdateClientInstructionEvent += UpdateClientInstruction_EventHandler;
                frmPolicyNotes.GetInstructionEvent += GetInstruction_EventHandler;

                frmPolicyNotes.ShowDialog(this);

                //frmPolicyNotes.Show(this);

            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                frmPolicyNotes = null;
            }
        }

        private Instruction GetInstruction_EventHandler(object sender, EventArgs args)
        {

            return GetInstructionEvent(TempNeed.InstructionId, new EventArgs());
        }
        private void AddClientInstruction_EventHandler(object sender, EventArgs e)
        {
            Instruction instruction = sender as Instruction;

            AddClientInstructionEvent?.Invoke(instruction, new EventArgs());

        }
        private void UpdateClientInstruction_EventHandler(object sender, EventArgs e)
        {
           
            Instruction instruction = sender as Instruction;

            UpdateClientInstructionEvent?.Invoke(instruction, new EventArgs());
        }
        #endregion

        #region PropertyChanged Event Handlers
        private void Need_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.dataGrid_PolicyDetails.Refresh();

            HasChanges = true;

            UpdateToolBar();
        }

        private void Need_PropertyChanged_Event(object sender, RowEventArgs e)
        {
            try
            {
                //CellContext cellContext = (CellContext)sender;
                DevAge.ComponentModel.BoundList<Need> _bList = dataGrid_PolicyDetails.DataSource as DevAge.ComponentModel.BoundList<Need>;
                TempNeed = _bList[e.Row - 1] as Need;

                this.dataGrid_PolicyFunds.Initialise(TempNeed.Funds, columns =>
                {
                    columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(ReadOnly));
                    columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                    columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
                    columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(Action == PolicyAction.AmendPolicy ? true : ReadOnly));
                    columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                    //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
                    columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
                    columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
                }//PropertyChangedHandler: Fund_propertyChanged_EventHandler,
                 //   ItemDeleteEventHandler: Fund_propertyDelete_EventHandler,
                 //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                 //    ReadOnly: ReadOnly,
                 //    AllowAddNew: Action == PolicyAction.AmendPolicy ? true : false,
                 //    AllowDelete: Action == PolicyAction.AmendPolicy ? true : false)
                        ).Formatt(ReadOnly);

                this.dataGrid_PolicyNotes.Initialise(TempNeed.Notes, columns =>
                {
                    columns.For(x => x.CreateDate, "Date", new MetroDateTimeEditor(175).ReadOnly(true));
                    //columns.For(x => x.Type, "Type", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(ReadOnly));
                    columns.For(x => x.Text, "Comment", new MetroMultiLineTextBoxEditor(600).ReadOnly(ReadOnly));
                    columns.For(x => x.UpdateBy, "By", new MetroDateEditor(195).ReadOnly(true));
                }// PropertyChangedHandler: Note_propertyChanged_EventHandler,
                 //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                 // ReadOnly: ReadOnly,
                 //AllowDelete: true)
                   ).Formatt(ReadOnly);
            }
            catch { };
        }
        private void Need_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
           
            try
            {
                DevAge.ComponentModel.BoundList<Need> _bList = sender as DevAge.ComponentModel.BoundList<Need>;
                Need mObj = _bList.EditedObject as Need;

                mObj.UpdateBy = Program.User.Username;
                mObj.UpdateDate = DateTime.Now;

                HasChanges = true;

                UpdateToolBar();
            }
            catch (Exception x)
            {
              
            }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();

                //Display Save Button
                if (xToolBarMenu1.tbSave.Visible)
                    xToolBarMenu1.tbSave.Enabled = HasChanges;
            }

            if (TempNeed != null && Instruction!=null)
                if (TempNeed.Status == NeedStatus.Accepted.ToText())
                    TempNeed.Status = NeedStatus.InProgress.ToText();

            if (!TempNeed.IsLoading)
                if (!TempNeed.IsCalculating)
                    TempNeed.Calculate();
        }

        private void Note_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {

            try
            {
                DevAge.ComponentModel.BoundList<Note> _bList = sender as DevAge.ComponentModel.BoundList<Note>;
                Note mObj = _bList.EditedObject as Note;

                mObj.UpdateBy = Program.User.Username;
                mObj.UpdateDate = DateTime.Now;

                HasChanges = true;

                UpdateToolBar();
            }
            catch (Exception x)
            {

            }
            finally
            {
                //Display Save Button
                if (xToolBarMenu1.tbSave.Visible)
                    xToolBarMenu1.tbSave.Enabled = HasChanges;

                dataGrid_PolicyNotes.Refresh();
            }

        }

        private void Fund_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {

            try
            {
                DevAge.ComponentModel.BoundList<Fund> _bList = sender as DevAge.ComponentModel.BoundList<Fund>;
                Fund mObj = _bList.EditedObject as Fund;

                mObj.UpdateBy = Program.User.Username;
                mObj.UpdateDate = DateTime.Now;

                mObj.Calculate();

                HasChanges = true;

                if (TempNeed != null && Instruction != null)
                    if (TempNeed.Status == NeedStatus.Accepted.ToText())
                        TempNeed.Status = NeedStatus.InProgress.ToText();

                if (!TempNeed.IsLoading)
                    if (!TempNeed.IsCalculating)
                        TempNeed.Calculate();

               
                UpdateToolBar();
            }
            catch (Exception x)
            {

            }
            finally
            {
                this.dataGrid_PolicyDetails.Refresh();

                //Display Save Button
                if (xToolBarMenu1.tbSave.Visible)
                    xToolBarMenu1.tbSave.Enabled = HasChanges;
            }

          
        }

        #endregion

    }

}
