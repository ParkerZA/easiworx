using Finx.App.Extensions;
using easiplan.domain.Entities;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Finx.App.Forms
{
    public partial class frmServiceProviders : MetroForm
    {

        Provider ServiceProviders = new Provider();

        public bool HasChanges { get; private set; }

        public frmServiceProviders()
        {
            InitializeComponent();

            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Orange;

            this.Text = "Manage Service Providers";

            xToolBarMenu1.tbEdit.Visible = false;
            xToolBarMenu1.tbRefresh.Visible = false;
            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;
            //xToolBarMenu1.EditClicked += XToolBarMenu1_EditClicked;
            //xToolBarMenu1.RefreshClicked += XToolBarMenu1_RefreshClicked;
            xToolBarMenu1.SaveClicked += toolStripButton1_Update_Click;

            xToolBarMenu1.tbCaption.Text = "Manage Service Providers";

           // this.Text = "Manage Service Providers:";
        }

        private void frmListData_Load(object sender, EventArgs e)
        {
           

            ServiceProviders = (Provider)Program.Repository.List<Provider,int>(null).FirstOrDefault();

            if (ServiceProviders == null)
                ServiceProviders = new Provider();


            #region Lisp

            this.dataGrid_Lisp.Initialise();

            this.dataGrid_Lisp.Columns.Add("LispName", "Lisp",
                                  new SourceGrid.Cells.Editors.TextBox(typeof(string))
                                  { EditableMode = SourceGrid.EditableMode.Focus| SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 250;
            this.dataGrid_Lisp.Columns.Add("LispFSB", "FSCA Number",
                                new SourceGrid.Cells.Editors.TextBox(typeof(string))
                                { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;
            this.dataGrid_Lisp.Columns.Add("LispContact", "Contact Name",
                               new SourceGrid.Cells.Editors.TextBox(typeof(string))
                               { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 175;
            this.dataGrid_Lisp.Columns.Add("LispTel", "Contact Number",
                              new SourceGrid.Cells.Editors.TextBox(typeof(string))
                              { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 175;
            this.dataGrid_Lisp.Columns.Add("InitialFees", "Initial Fee %",
                                new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(double))
                                { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;
            this.dataGrid_Lisp.Columns.Add("AdminFees", "Admin Fee %",
                               new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(double))
                               { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;

            if (ServiceProviders.LispProviders == null)
                ServiceProviders.LispProviders = new LispProviders();

            this.dataGrid_Lisp.DataSource = new DevAge.ComponentModel.BoundList<Lisp>(ServiceProviders.LispProviders.Lisps);
            this.dataGrid_Lisp.DataSource.ListChanged += DataSource_ListChanged<Lisp>;

            this.dataGrid_Lisp.Format(ReadOnly:!Program.User.IsAdministrator);

            //  row header click event
            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent.Click += clickEvent_Click; //for the row header click
            clickEvent.FocusEntered += clickEvent_Click; //for the cell gotfocus
            this.dataGrid_Lisp.Controller.AddController(clickEvent); //Event fired when any column is clicked
            #endregion

            #region Lisp Funds
            
            this.dataGrid_LispFunds.Initialise();

            this.dataGrid_LispFunds.Columns.Add("FundName", "Fund Name",
                                new SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 300;
            this.dataGrid_LispFunds.Columns.Add("FundCode", "Fund Code",
                                  new SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 100;
            this.dataGrid_LispFunds.Columns.Add("FundClass", "Fund Class",
                                new SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 150;
            this.dataGrid_LispFunds.Columns.Add("UnitClass", "Unit Class",
                                new SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 100;
            //this.dataGrid_LispFunds.Columns.Add("RiskCat", "Risk",
            //                    new SourceGrid.Cells.Editors.ComboBox(typeof(string), Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.FundRiskCat), false, "Value", "Text")
            //                    { EditableMode = SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 100;

            this.dataGrid_LispFunds.Columns.Add("RiskCat", "Risk", new ComboBoxEditor(Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.FundRiskCat))).Width = 100;


            this.dataGrid_LispFunds.Columns.Add("TER", "Expense Ratio",
                            new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(double)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;
            this.dataGrid_LispFunds.Columns.Add("IRR", "IRR",
                           new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(double)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 100;


            //Show the funds for the first Lisp

            if (ServiceProviders.LispProviders.Lisps.Count() > 0)
            {
                var m = ServiceProviders.LispProviders.Lisps[0];
                this.label_RetirementFund.Text = string.Format("Funds : {0}", m.LispName);

                this.dataGrid_LispFunds.DataSource = new DevAge.ComponentModel.BoundList<LispFund>(m.LispFunds);
                this.dataGrid_LispFunds.DataSource.ListChanged += DataSource_ListChanged<LispFund>;
            }

            this.dataGrid_LispFunds.Format(ReadOnly: !Program.User.IsAdministrator);


            #endregion

            #region Medical Providers

            this.dataGrid_MedicalAid.Initialise();

            this.dataGrid_MedicalAid.Columns.Add("ProviderName", "Medical Aid ",
                                  new SourceGrid.Cells.Editors.TextBox(typeof(string))
                                  { EditableMode = SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 250;
            this.dataGrid_MedicalAid.Columns.Add("RegNo", "Reg. Number",
                                new SourceGrid.Cells.Editors.TextBox(typeof(string))
                                { EditableMode = SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;
            this.dataGrid_MedicalAid.Columns.Add("Contact", "Contact Name",
                               new SourceGrid.Cells.Editors.TextBox(typeof(string))
                               { EditableMode = SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 150;
            this.dataGrid_MedicalAid.Columns.Add("Tel", "Contact Number",
                              new SourceGrid.Cells.Editors.TextBox(typeof(string))
                              { EditableMode = SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 135;
            //this.dataGrid_MedicalAid.Columns.Add("InitialFees", "Initial Fee %",
            //                    new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(double))
            //                    { EditableMode = SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;
            //this.dataGrid_MedicalAid.Columns.Add("AdminFees", "Admin Fee %",
            //                   new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(double))
            //                   { EditableMode = SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;

            if (ServiceProviders.MedicalProviders == null)
                ServiceProviders.MedicalProviders = new MedicalProviders();

            this.dataGrid_MedicalAid.DataSource = new DevAge.ComponentModel.BoundList<MedicalAid>(ServiceProviders.MedicalProviders.MedicalAids);
            this.dataGrid_MedicalAid.DataSource.ListChanged += DataSource_ListChanged<MedicalAid>;

            this.dataGrid_MedicalAid.Format(ReadOnly: !Program.User.IsAdministrator);

            //  row header click event
            SourceGrid.Cells.Controllers.CustomEvents dataGrid_MedicalAid_Event = new SourceGrid.Cells.Controllers.CustomEvents();
            dataGrid_MedicalAid_Event.FocusEntered += dataGrid_MedicalAid_Event_Click;
            this.dataGrid_MedicalAid.Controller.AddController(dataGrid_MedicalAid_Event); //Event fired when any column is clicked

            //dataGrid_MedicalAidPlans

            this.dataGrid_MedicalAidPlans.Initialise();

            this.dataGrid_MedicalAidPlans.Columns.Add("PlanCode", "Plan Code",
                                  new SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 100;
            this.dataGrid_MedicalAidPlans.Columns.Add("PlanName", "Plan Name",
                                new SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 300;
            this.dataGrid_MedicalAidPlans.Columns.Add("PlanType", "Plan Type",
                                new SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 150;
            //this.dataGrid_MedicalAidPlans.Columns.Add("RiskCat", "Risk",
            //                    new SourceGrid.Cells.Editors.ComboBox(typeof(string), Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.FundRiskCat), false, "Value", "Text")
            //                    { EditableMode = SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 100;
            this.dataGrid_MedicalAidPlans.Columns.Add("CoverLimit", "Cover Limit",
                            new SourceGrid.Cells.Editors.TextBoxCurrency(typeof(double)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;

           

            //Show the funds for the first Lisp

            if (ServiceProviders.MedicalProviders.MedicalAids.Count() > 0)
            {
                var m = ServiceProviders.MedicalProviders.MedicalAids[0];
                this.label_MedicalProviderFunds.Text = string.Format("Plans for : {0}", m.ProviderName);

                this.dataGrid_MedicalAidPlans.DataSource = new DevAge.ComponentModel.BoundList<MedicalPlan>(m.MedicalPlans);
                this.dataGrid_MedicalAidPlans.DataSource.ListChanged += DataSource_ListChanged<MedicalPlan>;
            }

            this.dataGrid_MedicalAidPlans.Format(ReadOnly: !Program.User.IsAdministrator);
            #endregion

            #region Life Insurers

            this.dataGrid_LifeInsurers.Initialise();

            this.dataGrid_LifeInsurers.Columns.Add("InsurerName", "Insurer Name",
                                  new SourceGrid.Cells.Editors.TextBox(typeof(string))
                                  { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 250;
            this.dataGrid_LifeInsurers.Columns.Add("FSB", "FSB Number",
                                new SourceGrid.Cells.Editors.TextBox(typeof(string))
                                { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;
            this.dataGrid_LifeInsurers.Columns.Add("Contact", "Contact Name",
                               new SourceGrid.Cells.Editors.TextBox(typeof(string))
                               { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 175;
            this.dataGrid_LifeInsurers.Columns.Add("Tel", "Contact Number",
                              new SourceGrid.Cells.Editors.TextBox(typeof(string))
                              { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 175;
            this.dataGrid_LifeInsurers.Columns.Add("InitialFees", "Initial Fee %",
                                new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(double))
                                { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;
            this.dataGrid_LifeInsurers.Columns.Add("AdminFees", "Admin Fee %",
                               new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(double))
                               { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;

            if (ServiceProviders.LifeProviders == null)
                ServiceProviders.LifeProviders = new LifeProviders();

            this.dataGrid_LifeInsurers.DataSource = new DevAge.ComponentModel.BoundList<LifeInsurer>(ServiceProviders.LifeProviders.LifeInsurers);
            this.dataGrid_LifeInsurers.DataSource.ListChanged += DataSource_ListChanged<LifeInsurer>;

            //  row header click event
            SourceGrid.Cells.Controllers.CustomEvents dataGrid_LifeInsurer_Event = new SourceGrid.Cells.Controllers.CustomEvents();
            dataGrid_LifeInsurer_Event.FocusEntered += dataGrid_LifeInsurer_Event_Click;
            this.dataGrid_LifeInsurers.Controller.AddController(dataGrid_LifeInsurer_Event); //Event fired when any column is clicked

            this.dataGrid_LifeInsurers.Format(ReadOnly: !Program.User.IsAdministrator);

            //Life Products
            this.dataGrid_LifeProducts.Initialise();

            this.dataGrid_LifeProducts.Columns.Add("ProductCode", "Plan Code",
                                            new SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 100;
            this.dataGrid_LifeProducts.Columns.Add("ProductName", "Plan Name",
                                new SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 300;
            this.dataGrid_LifeProducts.Columns.Add("ProductType", "Plan Type",
                                new SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 150;
            //this.dataGrid_LifeProducts.Columns.Add("RiskCat", "Risk",
            //                    new SourceGrid.Cells.Editors.ComboBox(typeof(string), Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.FundRiskCat), false, "Value", "Text")
            //                    { EditableMode = SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 100;
            this.dataGrid_LifeProducts.Columns.Add("CoverLimit", "Cover Limit",
                            new SourceGrid.Cells.Editors.TextBoxCurrency(typeof(double)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey }).Width = 120;


            if (ServiceProviders.MedicalProviders.MedicalAids.Count() > 0)
            {
                var m = ServiceProviders.LifeProviders.LifeInsurers[0];
                
                this.label_LifeProducts.Text = string.Format("Products for : {0}", m.InsurerName);

                this.dataGrid_LifeProducts.DataSource = new DevAge.ComponentModel.BoundList<LifeProduct>(m.LifeProducts);
                this.dataGrid_LifeProducts.DataSource.ListChanged += DataSource_ListChanged<LifeProduct>;
            }



            this.dataGrid_LifeProducts.Format(ReadOnly: !Program.User.IsAdministrator);

            #endregion
        }

        void user_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HasChanges = true;
            this.xToolBarMenu1.tbSave.Enabled = HasChanges;
        }
        void DataSource_ListChanged<T>(object sender, ListChangedEventArgs e)
        {
            HasChanges = true;
            this.xToolBarMenu1.tbSave.Enabled = HasChanges;
        }

        //Click event handler for Retirement Funds
        void clickEvent_Click(object sender, EventArgs e)
        {
            try
            {
                SourceGrid.CellContext context = (SourceGrid.CellContext)sender;

                var m = ServiceProviders.LispProviders.Lisps[context.Position.Row - 1];
                this.label_RetirementFund.Text = string.Format("Funds for : {0}", m.LispName);

                this.dataGrid_LispFunds.DataSource = new DevAge.ComponentModel.BoundList<LispFund>(m.LispFunds);
                this.dataGrid_LispFunds.DataSource.ListChanged += DataSource_ListChanged<LispFund>;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }


        }

        void dataGrid_MedicalAid_Event_Click(object sender, EventArgs e)
        {
            try
            {
                SourceGrid.CellContext context = (SourceGrid.CellContext)sender;

                var m = ServiceProviders.MedicalProviders.MedicalAids[context.Position.Row - 1];
                this.label_MedicalProviderFunds.Text = string.Format("Plans for : {0}", m.ProviderName);

               this.dataGrid_MedicalAidPlans.DataSource = new DevAge.ComponentModel.BoundList<MedicalPlan>(m.MedicalPlans);
               this.dataGrid_MedicalAidPlans.DataSource.ListChanged += DataSource_ListChanged<MedicalPlan>;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }


        }

        void dataGrid_LifeInsurer_Event_Click(object sender, EventArgs e)
        {
            try
            {
                SourceGrid.CellContext context = (SourceGrid.CellContext)sender;

                var m = ServiceProviders.LifeProviders.LifeInsurers[context.Position.Row - 1];
                this.label_LifeProducts.Text = string.Format("Products for : {0}", m.InsurerName);

                this.dataGrid_LifeProducts.DataSource = new DevAge.ComponentModel.BoundList<LifeProduct>(m.LifeProducts);
                this.dataGrid_LifeProducts.DataSource.ListChanged += DataSource_ListChanged<LifeProduct>;

            }
            catch (Exception x)
            {

            }


        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {

            if (HasChanges)
                if (!MessageBoxExt.ShowQuestion("Do you wish to close without saving changes"))
                    return;

            this.Close();
        }

        private void toolStripButton1_Update_Click(object sender, EventArgs e)
        {
            try
            {
            
                if(ServiceProviders.Id==0)
                    Program.Repository.Add<Provider,int>(ServiceProviders);
                else
                    Program.Repository.Update<Provider,int>(ServiceProviders);

                HasChanges = false;

                Program.SetServiceProviders();

                //this.toolStripButton1_Update.Enabled = false;
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                //xListView_Users.DataGridList.Refresh();
            }
        }

      

        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            //if (this.dataGrid_Lisp.Focus())
            //    this.dataGrid_Lisp.DeleteSelectedRows();

            //if (this.dataGrid_LispFunds.Focus())
            //    this.dataGrid_LispFunds.DeleteSelectedRows();

        }

        private void dataGrid1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGrid2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

