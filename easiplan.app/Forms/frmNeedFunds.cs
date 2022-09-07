using DevAge.Windows.Forms;
using Finx.App.Extensions;
using easiplan.domain.Entities;
using my.domain.lib.core.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using easiplan;
using easiplan.domain;
using easiplan.app.Extensions;

namespace Finx.App.Forms
{
    public partial class frmNeedFunds : Form
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler AddToCurrentPortfolio;

        bool ReadOnly = false;
        bool Changes = false;
        bool IsUpdate = false;

        Need Need = new Need();
        Client Client = new Client();
        Instruction Instruction = new Instruction();
        Retirement Retirement = new Retirement();

        IList<Need> _needList = new List<Need>();

        Provider ServiceProviders = new Provider();

        public frmNeedFunds(Client client, Need need, bool readOnly = true)
        {
            ReadOnly = readOnly;
            Client = client;
            Need = need;

            Need.Calculate();

            _needList.Add(Need);

            if (Need.InstructionId != 0)
                Instruction = Program.Repository.Get<Instruction, int>(Need.InstructionId);// Client.ClientInstructions.Instructions.Where(x => x.Id == Need.InstructionId).FirstOrDefault();

            InitializeComponent();

            InitialiseForm();



        }
        public frmNeedFunds(Client client, Instruction instruction, Need need, bool readOnly = true)
        {
            ReadOnly = readOnly;
            Client = client;
            Need = need;

            Need.Calculate();

            _needList.Add(Need);

            Instruction = instruction;

            InitializeComponent();

            InitialiseForm();
        }
        public frmNeedFunds(Client client, Retirement retirement, Need need, bool readOnly = true)
        {

            ReadOnly = readOnly;
            Client = client;
            Retirement = retirement;
            IsUpdate = true;

            if (need == null)
            {
                need = new Need();
                need.Type = retirement.Type;
                need.Description = retirement.Description;
                need.InitialAmount = 0;// need.InitialAmount;  //ignore Lump Sum Amounts
                need.MonthlyContribution = retirement.MonthlyContribution;

                need.CurrentAge = retirement.CurrentAge;
                need.InvestmentAge = retirement.InvestmentAge;

                need.InflationPercentage = retirement.InflationPercentage;
                need.EscalationPercentage = (double)retirement.EscalationPercentage;
                need.GrowthPercentage = retirement.GrowthPercentage;

                need.ReferenceNo = retirement.ReferenceNo;
                need.ReferenceId = retirement.Id;
                need.Status = "Updating";

                //need.InstructionId = retirement.InstructionId;

                need.CreateDate = DateTime.Now;
                need.UpdateDate = DateTime.Now;
                need.UpdateBy = Program.User.Username;

                need.Funds.Clear();
                foreach (Fund fund in retirement.Funds)
                    need.Funds.Add(new Fund()
                    {
                        CurrentAge = fund.CurrentAge,
                        Description = fund.Description,
                        EndDate = fund.EndDate,
                        EscalationPercentage = fund.EscalationPercentage,
                        GrowthPercentage = fund.GrowthPercentage,
                        InflationPercentage = fund.InflationPercentage,
                        CurrentAmount = fund.CurrentAmount,
                        InitialAmount = 0,// need.InitialAmount;  //ignore Lump Sum Amounts
                        InvestmentAge = fund.InvestmentAge,
                        InvestmentYears = fund.InvestmentYears,
                        MonthlyContribution = fund.MonthlyContribution,
                        Periods = fund.Periods,
                        ReferenceNo = fund.ReferenceNo,
                        SplitPerc = fund.SplitPerc,
                        StartDate = fund.StartDate,
                        Type = fund.Type,
                        Status = fund.Status,
                        Risk = fund.Risk,

                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        UpdateBy = Program.User.Username,
                    });


            }


            Need = need;
            Need.Calculate();

            _needList.Add(Need);

            if (Need.InstructionId != 0)
                Instruction = Program.Repository.Get<Instruction, int>(Need.InstructionId);// Client.ClientInstructions.Instructions.Where(x => x.Id == Need.InstructionId).FirstOrDefault();

            InitializeComponent();

            InitialiseForm();
        }

        void InitialiseForm()
        {

            if (Instruction == null)
                Instruction = new Instruction();

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0} [{1}] [{2}]", Need.Description, Instruction.Status == null ? Need.Status : Instruction.Status, Need.Id);
            this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.cog_32;

            xToolBarMenu1.tbSave.Visible = true; xToolBarMenu1.tbSave.Enabled = true;
            xToolBarMenu1.tbEdit.Visible = false;
            xToolBarMenu1.tbEdit.Text = "Cancel";
            xToolBarMenu1.tbRefresh.Visible = false;

            switch (Instruction.Status)
            {
                case "Pending":
                case "AddPending":
                case "UpdatePending":

                    xToolBarMenu1.tbEdit.Visible = true;
                    break;
                case "InProgress":
                case "AddInProgress":
                case "UpdateInProgress":
                    xToolBarMenu1.tbRefresh.Visible = true;
                    xToolBarMenu1.tbRefresh.Enabled = true;
                    xToolBarMenu1.tbRefresh.Text = "Complete";
                    xToolBarMenu1.tbEdit.Visible = true;
                    break;
                case "ReadyForAuth":
                case "AddReadyForAuth":
                case "UpdateReadyForAuth":
                    xToolBarMenu1.tbRefresh.Visible = true;
                    xToolBarMenu1.tbRefresh.Enabled = Program.User.Designation.Contains("Authoriser");
                    xToolBarMenu1.tbRefresh.Text = "Authorise";
                    xToolBarMenu1.tbEdit.Visible = true;
                    break;
                case "Authorised":
                case "AddAuthorised":
                case "UpdateAuthorised":
                    xToolBarMenu1.tbRefresh.Visible = true;
                    xToolBarMenu1.tbRefresh.Enabled = true;
                    xToolBarMenu1.tbRefresh.Text = "Update";
                    xToolBarMenu1.tbEdit.Visible = true;
                    break;
                case "Completed":
                case "AddCompleted":
                    xToolBarMenu1.tbRefresh.Visible = false;
                    xToolBarMenu1.tbEdit.Visible = false;
                    xToolBarMenu1.tbSave.Visible = false;
                    break;
                case "UpdateCompleted":
                    xToolBarMenu1.tbRefresh.Visible = false;
                    xToolBarMenu1.tbEdit.Visible = false;
                    xToolBarMenu1.tbSave.Visible = true;
                    xToolBarMenu1.tbSave.Text = "Update";
                    xToolBarMenu1.tbSave.Click += toolStripButton_Update_Click;
                    break;
                case "Cancelled":
                case "AddCancelled":
                case "UpdateCancelled":
                    xToolBarMenu1.tbRefresh.Visible = false;
                    xToolBarMenu1.tbEdit.Visible = false;
                    break;
                default:
                    xToolBarMenu1.tbRefresh.Visible = false;

                    break;

            }



        }

        private void frmNeedFunds_Load(object sender, EventArgs e)
        {
            //Retirement Portfolio
            this.dataGrid_RetirementPortfolio.Initialise();

            this.dataGrid_RetirementPortfolio.Columns.Add("Type", "Investment Type", new ComboBoxEditor(ListDataItemType.RetirementAssetClass, true)).Width = 200;

            ServiceProviders = (Provider)Program.Repository.List<Provider, int>(null).FirstOrDefault();

            var Lisps = new List<ListDataItem>();
            if (ServiceProviders != null)
                foreach (var lisp in ServiceProviders.LispProviders.Lisps)
                    Lisps.Add(new ListDataItem(ListDataItemType.Lisp, lisp.LispName, lisp.LispName));

            ComboBoxEditor cboLisps = new ComboBoxEditor(Lisps, true);
            cboLisps.Control.SelectedValueChanged += cboLisps_SelectedValueChanged;

            this.dataGrid_RetirementPortfolio.Columns.Add("Description", "LISP", cboLisps).Width = 170;
            this.dataGrid_RetirementPortfolio.Columns.Add("ReferenceNo", "Reference No", new StringEditor(false)).Width = 150;
            this.dataGrid_RetirementPortfolio.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor(false)).Width = 150;
            this.dataGrid_RetirementPortfolio.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor(false)).Width = 135;
            this.dataGrid_RetirementPortfolio.Columns.Add("EscalationPercentage", "Escalate (%p/a)", new DecimalEditor(false)).Width = 100;
            this.dataGrid_RetirementPortfolio.Columns.Add("GrowthPercentage", "Growth (%p/a)", new DecimalEditor(false)).Width = 100;
            this.dataGrid_RetirementPortfolio.Columns.Add("CurrentAmount", "Current Value", new CurrencyEditor(true)).Width = 190;
            this.dataGrid_RetirementPortfolio.Columns.Add("FutureAmount", "Retirement Value", new CurrencyEditor(true)).Width = 150;

            this.dataGrid_RetirementPortfolio.DataSource = new DevAge.ComponentModel.BoundList<Need>(_needList);
            this.dataGrid_RetirementPortfolio.DataSource.ListChanged += DataSource_ListChanged<Need>;

            //  row header click event
            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            //clickEvent.Click += RetirementPortfolio_Click;
            clickEvent.FocusEntered += RetirementPortfolio_Click;
            this.dataGrid_RetirementPortfolio.Controller.AddController(clickEvent); //Event fired when any column is clicked

            this.dataGrid_RetirementPortfolio.Format(ReadOnly, false, false);

            //Retirement Funds
            this.dataGrid_RetirementFunds.Initialise();

            this.dataGrid_RetirementFunds.Columns.Add("Description", "Fund Name", new StringEditor()).Width = 250;
            this.dataGrid_RetirementFunds.Columns.Add("StartDate", "Inception Date", new DateEditor()).Width = 200;
            this.dataGrid_RetirementFunds.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor()).Width = 135;
            this.dataGrid_RetirementFunds.Columns.Add("SplitPerc", "% Premium Split", new DecimalEditor()).Width = 135;
            this.dataGrid_RetirementFunds.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor(true)).Width = 135;

            this.dataGrid_RetirementFunds.Columns.Add("CurrentAmount", "Current Value", new CurrencyEditor()).Width = 190;
            this.dataGrid_RetirementFunds.Columns.Add("Type", "Fund Class", new StringEditor(true)).Width = 190;
            this.dataGrid_RetirementFunds.Columns.Add("Risk", "Risk Category", new StringEditor(true)).Width = 150;
            this.dataGrid_RetirementFunds.Columns.Add("IRR", "IRR", new DecimalEditor(true)).Width = 100;



            ShowFunds(0);

            this.dataGrid_RetirementFunds.Format(ReadOnly);

            this.dataGrid_RetirementPortfolio.Refresh();
            this.dataGrid_RetirementFunds.Refresh();


            xToolBarMenu1.EditClicked += toolStripButton_Cancel_Click;
            xToolBarMenu1.RefreshClicked += toolStripButton_Authorise_Click; ;
            xToolBarMenu1.SaveClicked += toolStripButton_Save_Click;
            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;



            Need.PropertyChanged += Need_PropertyChanged;

        }

        private void Need_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //try
            //{

            //    //Recalc the whole model
            //    Need.Calculate();

            //}
            //catch (Exception x)
            //{
            //    //xInput cntrl = _mappedControls.Where(y => y.MappedField == e.PropertyName).FirstOrDefault();
            //    //if(null!= cntrl)
            //    //    cntrl.SetTooltip("Warning", "warning", x.Message, true);

            //}
            //finally
            //{
            //    this.xToolBarMenu1.tbSave.Enabled = Changes;

            //    this.dataGrid_RetirementPortfolio.Refresh();
            //    this.dataGrid_RetirementFunds.Refresh();

            //};
        }

        private void cboLisps_SelectedValueChanged(object sender, EventArgs e)
        {
            DevAgeComboBox cbo = (DevAgeComboBox)sender;
            string selectedItem = (string)cbo.SelectedItem;

            int i = this.dataGrid_RetirementPortfolio.Selection.ActivePosition.Row;
            if (i > 0)
            {
                Need obj = (Need)this.dataGrid_RetirementPortfolio.DataSource[i - 1];
                obj.Description = selectedItem;
            }

            ShowFunds(i - 1);


        }

        void RetirementPortfolio_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;

            ShowFunds(context.Position.Row - 1);
        }

        /// <summary>
        /// Displays list of Funds for the selected LISP
        /// </summary>
        /// <param name="RowId">Row Id for the selected LISP</param>
        void ShowFunds(int RowId)
        {
            try
            {
                //the column containing the select box
                var selectCol = this.dataGrid_RetirementFunds.Columns[1].DataCell;
                selectCol.Editor = null;
                selectCol.Editor = new StringEditor(ReadOnly);//the default editor

                //the selected row model
                var m = _needList[RowId];

                try
                {
                    if (ServiceProviders != null)
                    {
                        IEnumerable<LispFund> lispFunds = (IEnumerable<LispFund>)ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == m.Description)
                            .FirstOrDefault().LispFunds; // the list of funds for the selected LISP

                        if (lispFunds.Where(x => x.FundName != null).Count() > 0)
                        {
                            ComboBoxEditor<LispFund, string> cboLispFunds = new ComboBoxEditor<LispFund, string>(lispFunds.ToListDataItem("FundName", "FundName"), true);

                            cboLispFunds.Control.SelectedValueChanged += LispFund_SelectedValueChanged;
                            selectCol.Editor = cboLispFunds;
                        }
                    }
                }
                catch (Exception y) { };



                //if (m.Funds == null)
                //    m.Funds = new List<Fund>();

                this.label_RetirementFund.Text = string.Format("Select Funds for: {0}", m.Description);

                this.dataGrid_RetirementFunds.DataSource = new DevAge.ComponentModel.BoundList<Fund>(m.Funds);
                this.dataGrid_RetirementFunds.DataSource.ListChanged += DataSource_ListChanged<Fund>;
            }
            catch (Exception x)
            {

            }
            finally
            {

            }
        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            if (Changes)
                if (!MessageBoxExt.ShowQuestion("Do you wish to close without saving changes"))
                    return;

            this.Close();
        }

        void DataSource_ListChanged<T>(object sender, ListChangedEventArgs e)
        {
            try
            {

                Need.Calculate();

                Changes = true;

            }
            catch (Exception x)
            {
            }
            finally
            {
                this.xToolBarMenu1.tbSave.Enabled = Changes;

                this.dataGrid_RetirementPortfolio.Refresh();
                this.dataGrid_RetirementFunds.Refresh();

            }


        }


        /// <summary>
        /// Cancel The Investment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Cancel this change ?"))
                    return;

                this.Enabled = false;

                string InstructionStatus = "";
                switch (Need.Status)
                {
                    case "Updated"://Cancelling an amendment to an existing processed need
                        Need.Status = "Cancelled";
                        Need.IsCancelled = true;
                        InstructionStatus = "UpdateCancelled";
                        break;
                    case "Accepted":
                        Need.Status = "Cancelled";
                        Need.IsCancelled = true;
                        InstructionStatus = "AddCancelled";
                        break;
                }

                Need.UpdateDate = DateTime.Now;
                Need.UpdateBy = Program.User.Username;

                if (Need.Id == 0)
                    Program.Repository.Add<Need, int>(Need);
                else
                    Program.Repository.Update<Need, int>(Need);

                if (Instruction.Id == 0)
                    Instruction = Program.Repository.Get<Instruction, int>(Need.InstructionId); //Client.ClientInstructions.Instructions.Where(x=>x.Id==Need.InstructionId).FirstOrDefault();

                Instruction.Status = InstructionStatus;
                Instruction.UpdateDate = DateTime.Now;
                Instruction.UpdateBy = Program.User.Username;

                //Program.Repository.Update<ClientInstructions, int>(Client.ClientInstructions);
                Program.Repository.Update<Instruction, int>(Instruction);

                //raise AddToCurrentPortfolio event
                Retirement.Status = Instruction.Status;
                EventHandler handlerA = AddToCurrentPortfolio;
                if (handlerA != null) handlerA(Retirement, new EventArgs());


                //raise changed event
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(Client, new PropertyChangedEventArgs(""));

                this.Close();


            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                this.xToolBarMenu1.tbSave.Enabled = Changes;

                this.Enabled = true;
            }
        }


        private void toolStripButton_Authorise_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate Need for Authorisation
                Need.Validate("referenceno");

                this.Enabled = false;
                string InstructionStatus = "";
                if (Instruction.Id != 0)
                    InstructionStatus = Instruction.Status;

                switch (Instruction.Status)
                {
                    case "AddInProgress":
                        if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Submit for Authorisation ?"))
                            return;

                        if (Instruction.Id != 0)
                        {
                            Instruction.Status = "AddReadyForAuth";
                            Instruction.ReferenceNo = Need.ReferenceNo;
                            Instruction.UpdateDate = DateTime.Now;
                            Instruction.UpdateBy = Program.User.Username;

                            Program.Repository.Update<Instruction, int>(Instruction);
                        }

                        break;
                    case "UpdateInProgress":
                        if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Submit for Authorisation ?"))
                            return;

                        if (Instruction.Id != 0)
                        {
                            Instruction.Status = "UpdateReadyForAuth";
                            Instruction.ReferenceNo = Need.ReferenceNo;
                            Instruction.UpdateDate = DateTime.Now;
                            Instruction.UpdateBy = Program.User.Username;

                            Program.Repository.Update<Instruction, int>(Instruction);
                        }

                        break;
                    case "ReadyForAuth":

                    case "AddReadyForAuth":
                    //if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Authorise this investment ?"))
                    //    return;

                    //if (Instruction.Id != 0)
                    //{
                    //    Instruction.Status = "AddAuthorised";
                    //    Instruction.ReferenceNo = Need.ReferenceNo;
                    //    Instruction.UpdateDate = DateTime.Now;
                    //    Instruction.UpdateBy = Program.User.Username;

                    //    Program.Repository.Update<Instruction, int>(Instruction);
                    //}
                    //break;

                    //if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Authorise this investment ?"))
                    //    return;

                    //if (Instruction.Id != 0)
                    //{
                    //    Instruction.Status = "UpdateAuthorised";
                    //    Instruction.ReferenceNo = Need.ReferenceNo;
                    //    Instruction.UpdateDate = DateTime.Now;
                    //    Instruction.UpdateBy = Program.User.Username;

                    //    Program.Repository.Update<Instruction, int>(Instruction);
                    //}
                    //break;
                    case "Authorised":
                    case "AddAuthorised":
                        //if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Update the clients portfolio with this investment ?"))
                        //    return;
                        if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Authorise this investment ?"))
                            return;
                        switch (Need.NeedType)
                        {
                            case NeedTypes.RetirementNeed:

                                Retirement retirement = new Retirement();

                                if (Need.ReferenceId != 0)
                                {
                                    retirement = Client.ClientPortfolio.Retirements.Where(x => x.Id == Need.ReferenceId).FirstOrDefault();
                                }


                                retirement.Type = Need.Type;
                                retirement.Description = Need.Description;
                                retirement.InitialAmount = Need.InitialAmount; // LumpSum amount
                                retirement.CurrentAmount += Need.InitialAmount; // Add LumpSum amount to existing current value
                                retirement.MonthlyContribution = Need.MonthlyContribution;

                                retirement.CurrentAge = Need.CurrentAge;
                                retirement.InvestmentAge = Need.InvestmentAge;

                                retirement.InflationPercentage = Need.InflationPercentage;
                                retirement.EscalationPercentage = (double)Need.EscalationPercentage;
                                retirement.GrowthPercentage = Need.GrowthPercentage;

                                retirement.ReferenceNo = Need.ReferenceNo;
                                // retirement.ReferenceId = Need.Id;
                                retirement.Status = "Implemented";

                                retirement.CreateDate = DateTime.Now;
                                retirement.UpdateDate = DateTime.Now;
                                retirement.UpdateBy = Program.User.Username;


                                retirement.Funds.Clear();
                                foreach (Fund fund in Need.Funds)
                                    retirement.Funds.Add(new Fund()
                                    {
                                        CurrentAge = fund.CurrentAge,
                                        Description = fund.Description,
                                        EndDate = fund.EndDate,
                                        EscalationPercentage = fund.EscalationPercentage,
                                        GrowthPercentage = fund.GrowthPercentage,
                                        InflationPercentage = fund.InflationPercentage,
                                        CurrentAmount = fund.CurrentAmount + fund.InitialAmount,
                                        InitialAmount = fund.InitialAmount,// Add LumpSum amount to existing current value
                                        InvestmentAge = fund.InvestmentAge,
                                        InvestmentYears = fund.InvestmentYears,
                                        MonthlyContribution = fund.MonthlyContribution,
                                        Periods = fund.Periods,
                                        ReferenceNo = fund.ReferenceNo,
                                        SplitPerc = fund.SplitPerc,
                                        StartDate = fund.StartDate,
                                        Type = fund.Type,
                                        Status = fund.Status,
                                        Risk = fund.Risk,

                                        CreateDate = DateTime.Now,
                                        UpdateDate = DateTime.Now,
                                        UpdateBy = Program.User.Username,
                                    });

                                //raise AddToCurrentPortfolio event
                                EventHandler handlerA = AddToCurrentPortfolio;
                                if (handlerA != null) handlerA(retirement, new EventArgs());

                                Need.ReferenceId = retirement.Id;

                                break;
                            default:
                                throw new Exception(string.Format("This type of Need has not been defined ...{0}", Need.NeedType));
                        }

                        if (Instruction.Id != 0)
                        {
                            Instruction.Status = "AddCompleted";
                            Instruction.ReferenceNo = Need.ReferenceNo;
                            Instruction.UpdateDate = DateTime.Now;
                            Instruction.UpdateBy = Program.User.Username;

                            Program.Repository.Update<Instruction, int>(Instruction);
                        }
                        Need.Status = "Completed";
                        Need.IsCancelled = false;
                        Need.IsImplemented = true;

                        Need.UpdateDate = DateTime.Now;
                        Need.UpdateBy = Program.User.Username;

                        if (Need.Id == 0)
                            Program.Repository.Add<Need, int>(Need);
                        else
                            Program.Repository.Update<Need, int>(Need);

                        break;
                    case "UpdateAuthorised":
                    case "UpdateReadyForAuth":
                        if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Update the clients portfolio with this investment ?"))
                            return;

                        switch (Need.NeedType)
                        {
                            case NeedTypes.RetirementNeed:



                                if (Retirement == null)
                                {
                                    Retirement = Client.ClientPortfolio.Retirements.Where(x => x.Id == Instruction.ReferenceId).FirstOrDefault();
                                }


                                Retirement.Type = Need.Type;
                                Retirement.Description = Need.Description;

                                Retirement.InitialAmount = Need.InitialAmount; // LumpSum amount
                                Retirement.CurrentAmount += Need.InitialAmount; // Add LumpSum amount to existing current value
                                Retirement.MonthlyContribution = Need.MonthlyContribution;

                                Retirement.CurrentAge = Need.CurrentAge;
                                Retirement.InvestmentAge = Need.InvestmentAge;

                                Retirement.InflationPercentage = Need.InflationPercentage;
                                Retirement.EscalationPercentage = (double)Need.EscalationPercentage;
                                Retirement.GrowthPercentage = Need.GrowthPercentage;

                                Retirement.ReferenceNo = Need.ReferenceNo;
                                // retirement.ReferenceId = Need.Id;
                                Retirement.Status = "Implemented";

                                Retirement.CreateDate = DateTime.Now;
                                Retirement.UpdateDate = DateTime.Now;
                                Retirement.UpdateBy = Program.User.Username;


                                Retirement.Funds.Clear();
                                foreach (Fund fund in Need.Funds)
                                    Retirement.Funds.Add(new Fund()
                                    {
                                        CurrentAge = fund.CurrentAge,
                                        Description = fund.Description,
                                        EndDate = fund.EndDate,
                                        EscalationPercentage = fund.EscalationPercentage,
                                        GrowthPercentage = fund.GrowthPercentage,
                                        InflationPercentage = fund.InflationPercentage,
                                        CurrentAmount = fund.CurrentAmount + fund.InitialAmount,
                                        InitialAmount = fund.InitialAmount,
                                        InvestmentAge = fund.InvestmentAge,
                                        InvestmentYears = fund.InvestmentYears,
                                        MonthlyContribution = fund.MonthlyContribution,
                                        Periods = fund.Periods,
                                        ReferenceNo = fund.ReferenceNo,
                                        SplitPerc = fund.SplitPerc,
                                        StartDate = fund.StartDate,
                                        Type = fund.Type,
                                        Status = fund.Status,
                                        Risk = fund.Risk,

                                        CreateDate = DateTime.Now,
                                        UpdateDate = DateTime.Now,
                                        UpdateBy = Program.User.Username,
                                    });

                                //raise AddToCurrentPortfolio event
                                EventHandler handlerA = AddToCurrentPortfolio;
                                if (handlerA != null) handlerA(Retirement, new EventArgs());

                                Need.ReferenceId = Retirement.Id;

                                break;
                            default:
                                throw new Exception(string.Format("This type of Need has not been defined ...{0}", Need.NeedType));

                        }


                        if (Instruction.Id != 0)
                        {
                            Instruction.Status = "UpdateCompleted";
                            Instruction.ReferenceNo = Need.ReferenceNo;
                            Instruction.UpdateDate = DateTime.Now;
                            Instruction.UpdateBy = Program.User.Username;

                            Program.Repository.Update<Instruction, int>(Instruction);
                        }

                        Need.Status = "Completed";
                        Need.IsCancelled = false;
                        Need.IsImplemented = true;

                        Need.UpdateDate = DateTime.Now;
                        Need.UpdateBy = Program.User.Username;

                        if (Need.Id == 0)
                            Program.Repository.Add<Need, int>(Need);
                        else
                            Program.Repository.Update<Need, int>(Need);
                        break;
                }

                //raise changed event
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(Client, new PropertyChangedEventArgs(""));

                //  this.Close();


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
                InitialiseForm();

                this.Enabled = true;
                this.xToolBarMenu1.tbSave.Enabled = Changes;
            }
        }

        //Update the Instruction/Need
        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {

            try
            {
                //Validate
                Need.Validate();

                this.Enabled = false;

                switch (Need.Status)
                {
                    case "Advised":
                    case "Completed":
                    case "Cancelled":
                    case "AddCancelled":
                        {
                            if (!MessageBoxExt.ShowQuestion("Are you sure you wish Accept this advice ?"))
                                return;

                            Need.Status = "Accepted";
                            Need.IsCancelled = false;
                            //Create an Admin Task/Instruction
                            Instruction = new Instruction()
                            {
                                Status = "AddPending",
                                AllocatedTo = "UnAllocated",
                                Type = Need.Type,
                                Description = string.Format("{0}, {1} {2} {3}", Client.ClientDetails.LastName, Client.ClientDetails.FirstName, Client.ClientDetails.MidName, Client.ClientDetails.ClientTitle),
                                Comment = Need.Description,
                                ReferenceId = Need.Id,
                                ReferenceNo = Need.ReferenceNo,
                                ClientId = Client.Id,
                                UpdateDate = DateTime.Now,
                                UpdateBy = Program.User.Username

                            };

                            Program.Repository.Add<Instruction, int>(Instruction);
                            Need.InstructionId = Instruction.Id;

                            //Update Admin tasks
                            //Client.ClientInstructions.Instructions.Add(Instruction);
                            //Program.Repository.Update<ClientInstructions, int>(Client.ClientInstructions);

                            //Update Instruction Id
                            Need.InstructionId = Instruction.Id;
                            Program.Repository.Update<ClientFna, int>(Client.ClientFna);

                            break;
                        }
                    case "Accepted":
                        if (Instruction.Status == "Pending"
                            || Instruction.Status == "AddPending")
                        {
                            //Need.Status = "InProgress";
                            Instruction.Status = "AddInProgress";
                        }
                        break;
                    case "Updating":
                    case "UpdateCancelled":

                        if (!MessageBoxExt.ShowQuestion("Are you sure you wish Update this Investment ?"))
                            return;

                        //if (MessageBoxExt.ShowQuestion("Do you wish to create an Admin Task for this Update ?"))
                        //{
                        Need.Status = "Updated";
                        Need.IsCancelled = false;

                        Instruction = new Instruction()
                        {
                            Status = "UpdatePending",
                            AllocatedTo = "UnAllocated",
                            Type = Need.Type,
                            Description = string.Format("{0}, {1} {2} {3}", Client.ClientDetails.LastName, Client.ClientDetails.FirstName, Client.ClientDetails.MidName, Client.ClientDetails.ClientTitle),
                            Comment = Need.Description,
                            ReferenceId = Need.Id,
                            ReferenceNo = Need.ReferenceNo,
                            ClientId = Client.Id,
                            UpdateDate = DateTime.Now,
                            UpdateBy = Program.User.Username

                        };

                        Program.Repository.Add<Instruction, int>(Instruction);
                        Need.InstructionId = Instruction.Id;

                        //Add Admin task 
                        //Client.ClientInstructions.Instructions.Add(Instruction);
                        //    Program.Repository.Update<ClientInstructions, int>(Client.ClientInstructions);

                        Need.InstructionId = Instruction.Id;

                        break;

                    case "Updated":
                        if (Instruction.Status == "Pending"
                            || Instruction.Status == "UpdatePending")
                        {
                            //Need.Status = "InProgress";
                            Instruction.Status = "UpdateInProgress";
                        }
                        break;

                    default:

                        break;
                }

                Need.UpdateDate = DateTime.Now;
                Need.UpdateBy = Program.User.Username;

                if (Need.Id == 0)
                    Program.Repository.Add<Need, int>(Need);
                else
                    Program.Repository.Update<Need, int>(Need);

                Instruction.ReferenceId = Need.Id;
                Instruction.ReferenceNo = Need.ReferenceNo;
                Instruction.UpdateDate = DateTime.Now;
                Instruction.UpdateBy = Program.User.Username;

                if (Instruction.Id == 0)
                    Program.Repository.Add<Instruction, int>(Instruction);
                else
                    Program.Repository.Update<Instruction, int>(Instruction);

                if (Retirement != null)
                {
                    Retirement.ReferenceId = Need.Id;
                    Retirement.Status = Instruction.Status;

                }

                Changes = false;

                //raise changed event
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(Client, new PropertyChangedEventArgs(""));

                //this.Close();


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
                InitialiseForm();

                this.Enabled = true;

                this.xToolBarMenu1.tbSave.Enabled = Changes;
            }
        }

        private void toolStripButton_Update_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                //Validate
                Need.Validate();

                switch (Need.Status)
                {

                    case "Completed":
                    case "Cancelled":

                        if (!MessageBoxExt.ShowQuestion("Are you sure you wish Update this investment ?"))
                            return;

                        //Clone this need
                        var need = new Need();
                        need.Type = Need.Type;
                        need.Description = Need.Description;
                        need.InitialAmount = 0;// need.InitialAmount;  //ignore Lump Sum Amounts
                        need.MonthlyContribution = Need.MonthlyContribution;

                        need.CurrentAge = Need.CurrentAge;
                        need.InvestmentAge = Need.InvestmentAge;

                        need.InflationPercentage = Need.InflationPercentage;
                        need.EscalationPercentage = (double)Need.EscalationPercentage;
                        need.GrowthPercentage = Need.GrowthPercentage;

                        need.ReferenceNo = Need.ReferenceNo;
                        need.ReferenceId = Need.Id;
                        need.Status = "Updating";

                        need.InstructionId = Need.InstructionId;

                        need.CreateDate = DateTime.Now;
                        need.UpdateDate = DateTime.Now;
                        need.UpdateBy = Program.User.Username;

                        need.Funds.Clear();
                        foreach (Fund fund in Need.Funds)
                            need.Funds.Add(new Fund()
                            {
                                CurrentAge = fund.CurrentAge,
                                Description = fund.Description,
                                EndDate = fund.EndDate,
                                EscalationPercentage = fund.EscalationPercentage,
                                GrowthPercentage = fund.GrowthPercentage,
                                InflationPercentage = fund.InflationPercentage,
                                InitialAmount = fund.InitialAmount,
                                InvestmentAge = fund.InvestmentAge,
                                InvestmentYears = fund.InvestmentYears,
                                MonthlyContribution = fund.MonthlyContribution,
                                Periods = fund.Periods,
                                ReferenceNo = fund.ReferenceNo,
                                SplitPerc = fund.SplitPerc,
                                StartDate = fund.StartDate,
                                Type = fund.Type,
                                Status = fund.Status,
                                Risk = fund.Risk,

                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                UpdateBy = Program.User.Username,
                            });

                        Need = need;


                        //Create an Admin Task/Instruction
                        Instruction = new Instruction()
                        {
                            Status = "AddPending",
                            AllocatedTo = "UnAllocated",
                            Type = Need.Type,
                            Description = string.Format("{0}, {1} {2} {3}", Client.ClientDetails.LastName, Client.ClientDetails.FirstName, Client.ClientDetails.MidName, Client.ClientDetails.ClientTitle),
                            Comment = Need.Description,
                            ReferenceId = Need.Id,
                            ReferenceNo = Need.ReferenceNo,
                            ClientId = Client.Id,
                            UpdateDate = DateTime.Now,
                            UpdateBy = Program.User.Username

                        };

                        Program.Repository.Add<Instruction, int>(Instruction);
                        Need.InstructionId = Instruction.Id;

                        //Update Admin tasks
                        //Client.ClientInstructions.Instructions.Add(Instruction);
                        //    Program.Repository.Update<ClientInstructions, int>(Client.ClientInstructions);

                        //Update Instruction Id
                        Need.InstructionId = Instruction.Id;
                        Program.Repository.Update<ClientFna, int>(Client.ClientFna);

                        break;

                    case "Accepted":
                        if (Instruction.Status == "Pending"
                            || Instruction.Status == "AddPending")
                        {
                            //Need.Status = "InProgress";
                            Instruction.Status = "AddInProgress";
                        }
                        break;


                    case "Updated":
                        if (Instruction.Status == "Pending"
                            || Instruction.Status == "UpdatePending")
                        {
                            //Need.Status = "InProgress";
                            Instruction.Status = "UpdateInProgress";
                        }
                        break;

                    default:

                        break;
                }

                Need.UpdateDate = DateTime.Now;
                Need.UpdateBy = Program.User.Username;

                if (Need.Id == 0)
                    Program.Repository.Add<Need, int>(Need);
                else
                    Program.Repository.Update<Need, int>(Need);

                Instruction.ReferenceId = Need.Id;
                Instruction.ReferenceNo = Need.ReferenceNo;
                Instruction.UpdateDate = DateTime.Now;
                Instruction.UpdateBy = Program.User.Username;

                if (Instruction.Id == 0)
                    Program.Repository.Add<Instruction, int>(Instruction);
                else
                    Program.Repository.Update<Instruction, int>(Instruction);

                if (Retirement != null)
                {
                    Retirement.ReferenceId = Need.Id;
                    // Program.Repository.Update<Retirement, int>(Retirement);
                }

                Changes = false;

                //raise changed event
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(Client, new PropertyChangedEventArgs(""));

                //this.Close();

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
                this.Enabled = true;
            }
        }
        private void LispFund_SelectedValueChanged(object sender, EventArgs e)
        {
            DevAge.Windows.Forms.DevAgeComboBox cbo = (DevAge.Windows.Forms.DevAgeComboBox)sender;
            LispFund lFund = cbo.SelectedItem as LispFund;

            if (lFund != null)
            {
                int i = this.dataGrid_RetirementFunds.Selection.ActivePosition.Row;
                if (i > 0)
                {
                    Fund objFund = (Fund)this.dataGrid_RetirementFunds.DataSource[i - 1];

                    objFund.Type = lFund.FundClass; //set the Fund Type
                    objFund.Risk = lFund.RiskCat;
                    objFund.IRR = lFund.IRR;

                    this.dataGrid_RetirementFunds.Refresh();
                }
            }
        }
    }
}
