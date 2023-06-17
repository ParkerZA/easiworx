using DevAge.ComponentModel;
using Finx.App.Enums;
using Finx.App.Extensions;
using Finx.App.Models;
using easiplan.domain;
using easiplan.domain.Entities;

using MetroFramework;
using MetroFramework.Controls.Ext;
using MetroFramework.Controls;
using MetroFramework.Forms;
using my.domain.lib.core.Domain;
using my.domain.lib.core.Extensions;
using OpenXml;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Threading.Tasks;
using easiplan.app.ContextMenus;
using easiplan.domain.estate.Entities;
using easiplan.domain.estate.MetaEntities;
using System.Xml.Serialization;
using easiplan.app.Extensions;

namespace Finx.App.Forms
{
    public partial class frmMetroClient1 : MetroForm
    {
        #region Local variables
        bool _readOnly = true;
        bool _hasChanges = false;
        bool _readOnlyForClerk = true;
        bool _readOnlyForAdminAdvisor = true;
        bool _readOnlyForAdminAdvisorClerk = true;
        bool _readOnlyForAdmin = true;
        bool _readOnlyForAdminClerk = true;
        bool _isInitialising = false;
        public int clientId = 0;

        Client client;

        ClientLockStatus ClientLockStatus = new ClientLockStatus();

        //Collections for DropdownComboBoxes
        List<ListDataItem> Lisps = new List<ListDataItem>();
        List<ListDataItem> MedicalAids = new List<ListDataItem>();
        List<ListDataItem> MedicalAidPlans = new List<ListDataItem>();
        List<ListDataItem> LifeInsurers = new List<ListDataItem>();
        List<ListDataItem> LifeInsurersPlans = new List<ListDataItem>();

        //Popup DataGrid for PolicyFunds

        SourceGrid.DataGrid dataGrid_PolicyFunds = new SourceGrid.DataGrid() { Name = "dataGrid_PolicyFunds" };

        //Keep track of which tab is currently selected
        private TabControlEventArgs _currentTabControlEventArgs = new TabControlEventArgs(null, 0, TabControlAction.Selected);

        Lisp Lisp = new Lisp();

        IList<Instruction> instructions = new List<Instruction>();
        IList<ClientMeetings> clientMeetings = new List<ClientMeetings>();
        EstateAnalysis EstateAnalysis;
        EstateDefaults EstateDefaults;
        ClientFnaRisk ClientFnaRisk;
        #endregion

        #region  Constructor
        public frmMetroClient1(int clientId, int tabIndex = 0)
        {
            try
            {
                SuspendLayout();

                InitializeComponent();

                #region Form Format
                this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
                this.ShadowType = MetroFramework.Forms.MetroFormShadowType.Flat;

                this.Text = string.Empty;
                this.toolStripButton_Person.Font = MetroFonts.DefaultBold(20f);

                this.DisplayHeader = false;

                #endregion

                #region ToolbarMenuStrip Events

                this.toolStripButton_Edit.Click += toolStripButton_Edit_Click;
                this.toolStripButton_Refresh.Click += toolStripButton_Refresh_Click;
                this.toolStripButton_Documents.Click += toolStripButton_ClientDocs_Click;
                this.toolStripButton_Update.Click += toolStripButton_Update_Click;
                this.toolStripButton_Close.Click += toolStripButton_Close_Click;
                this.toolStripButton_Delete.Click += toolStripButton_Delete_Click;
                this.toolStripButton_Export.Click += toolStripDropDownButton_Export_Click;
                #endregion

                #region Form Events
                this.FormClosing += frmMetroClient1_FormClosing;

                this.metroButton_CopyAddress.Click += metroButton_CopyAddress_Click;

                this.metroCheckBox_Haves.Click += RetirementFna_propertyChanged_EventHandler;
                this.metroCheckBox_Needs.Click += RetirementFna_propertyChanged_EventHandler;
                this.metroCheckBox_Wants.Click += RetirementFna_propertyChanged_EventHandler;
                //Set defaults
                this.metroCheckBox_Haves.Checked = true;
                this.metroCheckBox_Needs.Checked = true;
                this.metroCheckBox_Wants.Checked = true;

                this.mbtnAddTask.Text = "Add Custom Task";
                this.mbtnAddTask.Click += MbtnAddTask_Click;

                #endregion

                #region Load Client model
                this.clientId = clientId;
                if (clientId != 0)
                { client = Program.ClientService.Get(clientId); }
                else
                { client = new Client(); _readOnly = false; }

                //initialise and setup events handler
                client.Initialise();
                client.PropertyChanged -= Client_PropertyChanged;
                client.PropertyChanged += Client_PropertyChanged;
                #endregion

                #region Load Service Providers

                Lisps.Clear();
                if (Program.ServiceProviders != null)
                    foreach (var lisp in Program.ServiceProviders.LispProviders.Lisps)
                        Lisps.Add(new ListDataItem(ListDataItemType.Lisp, lisp.LispName, lisp.LispName));

                MedicalAids.Clear();
                if (Program.ServiceProviders != null)
                {
                    foreach (var lisp in Program.ServiceProviders.MedicalProviders.MedicalAids)
                        MedicalAids.Add(new ListDataItem(ListDataItemType.Lisp, lisp.ProviderName, lisp.ProviderName));
                }

                LifeInsurers.Clear();
                if (Program.ServiceProviders != null)
                {
                    foreach (var lisp in Program.ServiceProviders.LifeProviders.LifeInsurers)
                        LifeInsurers.Add(new ListDataItem(ListDataItemType.Lisp, lisp.InsurerName, lisp.InsurerName));
                }
                #endregion

                #region ReportTemplates

                toolStripButton_Reports.DropDownItems.Clear();

                foreach (DocumentTemplate dT in Program.DocumentTemplatesList)
                {
                    ToolStripMenuItem tM = new ToolStripMenuItem()
                    {
                        Text = dT.TemplateName,
                        Tag = dT,
                    };
                    tM.Click += new EventHandler(DocumentTemplate_OnClick);
                    toolStripButton_Reports.DropDownItems.Add(tM);
                }

                #endregion

                #region TabControl_Main

                this.metroTabControl_Main.TabPages[0].Format(0, "Client Details |");
                this.metroTabControl_Main.TabPages[1].Format(1, "Assets && Liabilities |");
                this.metroTabControl_Main.TabPages[2].Format(2, "Income && Expenses |");
                this.metroTabControl_Main.TabPages[3].Format(3, "Current Portfolio |");
                this.metroTabControl_Main.TabPages[4].Format(4, "Retirement FNA |");
                this.metroTabControl_Main.TabPages[5].Format(5, "Non-Retirement FNA |");
                this.metroTabControl_Main.TabPages[6].Format(6, "Estate Planning |");
                this.metroTabControl_Main.TabPages[7].Format(7, "Checklist |");
                this.metroTabControl_Main.TabPages[8].Format(8, "Admin Tasks |");
                this.metroTabControl_Main.TabPages[9].Format(9, "Review Meetings");

                metroTabControl_Main.Selected += MetroTabControl_Main_Selected;
                this.metroTabControl_Main.SelectedIndex = tabIndex;

                //Exclusions
                if (!Program.Licensing.HasFeature("Estate & Risk Planning"))
                {
                    this.metroTabControl_Main.TabPages.Remove(this.metroTabControl_Main.TabPages[6]); //Estate Duty and Risk
                }
                #endregion

                #region TabControl_ClientDetails
                this.metroTabControl_ClientDetails.TabPages[0].Format("Personal Details |");
                this.metroTabControl_ClientDetails.TabPages[1].Format("Dependents |");
                this.metroTabControl_ClientDetails.TabPages[2].Format("Address Details |");
                this.metroTabControl_ClientDetails.TabPages[3].Format("Contact Details |");
                this.metroTabControl_ClientDetails.TabPages[4].Format("Bank Details |");
                this.metroTabControl_ClientDetails.TabPages[5].Format("Additional Info |");
                this.metroTabControl_ClientDetails.TabPages[6].Format("Client Fees");

                this.metroTabControl_ClientDetails.SelectedIndex = 0;
                #endregion

                #region TabControl_CurrentPortfolio

                this.metroTabControl_CurrentPortfolio.TabPages[0].Format("Retirement Portfolio |");
                this.metroTabControl_CurrentPortfolio.TabPages[1].Format("Non-Retirement Portfolio |");
                this.metroTabControl_CurrentPortfolio.TabPages[2].Format("Education Portfolio |");
                this.metroTabControl_CurrentPortfolio.TabPages[3].Format("Medical/GAP Portfolio |");
                this.metroTabControl_CurrentPortfolio.TabPages[4].Format("Risk Portfolio |");
                this.metroTabControl_CurrentPortfolio.TabPages[5].Format("Income Assets Portfolio |");

                this.metroTabControl_CurrentPortfolio.SelectedIndex = 0;
                #endregion

                #region TabControl_RetirementFNA

                this.metroTabControl_RetirementFNA.TabPages[0].Format("What I want at Retirement ... |");
                this.metroTabControl_RetirementFNA.TabPages[1].Format("What I have currently ... |");
                this.metroTabControl_RetirementFNA.TabPages[2].Format("What I need to make up shortfall... |");

                this.metroTabControl_RetirementFNA.SelectedIndex = 0;
                #endregion

                #region TabControl_NonRetirementFNA

                this.metroTabControl_InvestmentsFNA.TabPages[0].Format("Investment Needs ... |");
                this.metroTabControl_InvestmentsFNA.TabPages[1].Format("Education Needs ... |");
                this.metroTabControl_InvestmentsFNA.TabPages[2].Format("Risk Cover Needs ... |");

                this.metroTabControl_InvestmentsFNA.SelectedIndexChanged += MetroTabControl_InvestmentsFNA_SelectedIndexChanged;
                this.metroTabControl_InvestmentsFNA.SelectedIndex = 0;

                #endregion

                #region TabControl_EstateDuty

                this.tabPage_CGT.Format("Capital Gains Tax |");
                this.tabPage_ExecFees.Format("Executor Fees |");

                this.metroTabControl_EstateDuty.SelectedIndex = 0;

                #endregion

                #region Estate Analysis model
                EstateDefaults = new EstateDefaults();
                EstateAnalysis = Program.Repository.List<EstateAnalysis, int>(x => x.ClientId == this.client.Id).FirstOrDefault();
                if (EstateAnalysis == null)
                {
                    EstateAnalysis = new EstateAnalysis();
                }
                EstateAnalysis.Client = this.client;
                EstateAnalysis.EstateDefaults = this.EstateDefaults;
                //EstateAnalysis.PropertyChanged += EstateAnalysis_PropertyChanged;

                EstateAnalysis.Initialise(true);
                #endregion

                #region TabControl_Checklist
                #endregion

                #region ClientFnaRisk
                ClientFnaRisk = Program.Repository.List<ClientFnaRisk, int>(x => x.ClientId == this.client.Id).FirstOrDefault();
                //RiskCover
                if (ClientFnaRisk == null) ClientFnaRisk = new ClientFnaRisk() { ClientId = this.client.Id };
                ClientFnaRisk.Client = this.client;

                ClientFnaRisk.Initialise();
                #endregion

            }
            catch (Exception x)
            {
                //throw new Exception("There was an error loading this form.Please contact your vendor. \r\n\r\n " + x.Message);
                MessageBox.Show("There was an error loading this form.Please contact your vendor. \r\n\r\n " + x.Message);
            }
            finally
            {
                ResumeLayout();
            }
        }
        #endregion

        #region Form Events
        private void frmMetroClient1_Load(object sender, EventArgs e)
        {
            try
            {

                _readOnlyForClerk = _readOnly == true ? _readOnly : !Program.User.IsAdministrator && !Program.User.IsClerk;
                _readOnlyForAdminAdvisor = _readOnly == true ? _readOnly : !Program.User.IsAdministrator && !Program.User.IsAdvisor;
                _readOnlyForAdminAdvisorClerk = _readOnly == true ? _readOnly : !Program.User.IsAdministrator && !Program.User.IsAdvisor && !Program.User.IsClerk;
                _readOnlyForAdmin = _readOnly == true ? _readOnly : !Program.User.IsAdministrator;
                _readOnlyForAdminClerk = _readOnly == true ? _readOnly : !Program.User.IsAdministrator && !Program.User.IsClerk;

                //Select the current tab
                MetroTabControl_Main_Selected(this.metroTabControl_Main, _currentTabControlEventArgs);

                Size _size = new Size(width: 520, height:600);
                this.kgbMemberDetails.Size=_size;
                this.kgbSpouseDetails.Size = _size;


            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x, "There was an error loading this client. It may have been cancelled or deleted.");

                this.Close();
            }
            finally
            {

                _hasChanges = false;

                GetClientLock();

                RefreshStatusStrip();

            }
        }

        private void frmMetroClient1_Activated(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmMetroClient1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_hasChanges || client.IsModified)
            {
                DialogResult res = MessageBoxExt.ShowYesNoCancel("Do you wish to save changes before closing?");

                if (res == DialogResult.Cancel)
                { e.Cancel = true; return; }

                if (res == DialogResult.Yes)
                    toolStripButton_Update_Click(sender, new EventArgs());
            }
            //Release the lock
            ReleaseClientLock();
        }
        #endregion
       
        #region TabControl Events
        private void MetroTabControl_Main_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                _isInitialising = true;

                //set the current active tab event args
                _currentTabControlEventArgs = e;

                //get the current active tab page 
                TabPage t = this.metroTabControl_Main.TabPages[e.TabPageIndex];

                switch (t.Tag)
                {
                    case 0:


                        #region Personal Details
                        client.ClientDetails.Initialise(true);
                        this.kgbMemberDetails.Panel.Initialise<ClientDetails>(client.ClientDetails, cntr =>
                        {
                            cntr.For(x => x.ClientTitle, "Title", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.Titles)).ReadOnly(_readOnly));
                            cntr.For(x => x.FirstName, "First Name", new MetroNameEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.MidName, "Middle Names", new MetroNameEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.LastName, "Surname", new MetroNameEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.IdentificationNo, "SAId No", new MetroSAIDEditor().ReadOnly(!string.IsNullOrEmpty(client.ClientDetails.IdentificationNo) && client.Id > 0 && !Program.User.IsAdministrator ? true : _readOnly));
                            cntr.For(x => x.PassportNo, "Passport No", new MetroTextBoxEditor().ReadOnly(!string.IsNullOrEmpty(client.ClientDetails.PassportNo) && client.Id > 0 && !Program.User.IsAdministrator ? true : _readOnly));
                            cntr.For(x => x.Nationality, "Nationality", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.DateOfBirth, "Date Of Birth", new MetroDateEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Gender, "Gender", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.Gender)).ReadOnly(_readOnly));
                            cntr.For(x => x.BirthPlace, "Birth Place", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.MaritalStatus, "Marital Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.MaritalStatus)).ReadOnly(_readOnly));
                            cntr.For(x => x.TaxNumber, "Tax Number", new MetroTaxNoEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Occupation, "Occupation", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Language, "Language Preference", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.Language)).ReadOnly(_readOnly));
                        }, left: 25, top: 10, labelWidth: 200
                       , IsLoading: true
                        ).Format();

                        this.kgbMemberDetails.Panel.Initialise<ClientDetails>(client.ClientDetails, cntr =>
                        {
                            cntr.For(x => x.Age, "", new MetroNumberEditor(Width: 50).ReadOnly(true));
                        }, left: 310, top: 200
                        ).Format();

                        client.SpouseDetails.Initialise(true);
                        this.kgbSpouseDetails.Panel.Initialise<ClientDetails>(client.SpouseDetails, cntr =>
                        {
                            cntr.For(x => x.ClientTitle, "Title", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.Titles)).ReadOnly(_readOnly));
                            cntr.For(x => x.FirstName, "First Name", new MetroNameEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.MidName, "Middle Names", new MetroNameEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.LastName, "Surname", new MetroNameEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.IdentificationNo, "SAId No", new MetroSAIDEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.PassportNo, "Passport No", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Nationality, "Nationality", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.DateOfBirth, "Date Of Birth", new MetroDateEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Gender, "Gender", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.Gender)).ReadOnly(_readOnly));
                            cntr.For(x => x.BirthPlace, "Birth Place", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.MaritalStatus, "Marital Status", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.MaritalStatus)).ReadOnly(_readOnly));
                            cntr.For(x => x.TaxNumber, "Tax Number", new MetroTaxNoEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Occupation, "Occupation", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Language, "Language Preference", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.Language)).ReadOnly(_readOnly));

                        }, left: 25, top: 10, labelWidth: 200
                        , IsLoading: true
                        ).Format();

                        this.kgbSpouseDetails.Panel.Initialise<ClientDetails>(client.SpouseDetails, cntr =>
                        {
                            cntr.For(x => x.Age, "", new MetroNumberEditor(Width: 50).ReadOnly(true));
                        }, left: 310, top: 200).Format();
                        #endregion

                        #region Dependents
                        client.ClientDependents.Initialise();
                        this.dataGrid_Dependents.Initialise1<ClientDependent>(client.ClientDependents.DependentsBindingList, column =>
                        {
                            column.For(c => c.DependentName, "Name", new StringEditor(), MinWidth: 200);
                            column.For(c => c.DependentType, "Relation", new ComboBoxEditor(ListDataItemType.DependentType), MinWidth: 180);
                            column.For(c => c.BirthDate, "Birth Date", new DateEditor(), MinWidth: 100);
                            column.For(c => c.IdNumber, "Id Number", new SAIdEditor(), MinWidth: 100);
                            column.For(c => c.Percentage, "Will %", new DecimalEditor(), Width: 60, Tooltip: "Is the dependant include in your will ?");
                            column.For(c => c.MedAid, "Medical Aid", Width: 80, Tooltip: "Is the dependant a member on your Medical Aid?");
                            column.For(c => c._Age, "Age", new StringEditor(true), Width: 60);
                        }
                        , ReadOnly: _readOnly, AllowDelete: true
                        ).Format1(_readOnly);
                        #endregion

                        #region Address Details
                        client.PhysicalAddress.Initialise(true);
                        this.kgbResidentialAddress.Panel.Initialise<AddressDetail>(client.PhysicalAddress, cntr =>
                        {
                            cntr.For(x => x.Line1, "Address Number", new MetroTextBoxEditor(100).ReadOnly(_readOnly));
                            cntr.For(x => x.Line2, "Street", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Line3, "Suburb", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Line4, "City", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Code, "Code", new MetroNumberEditor(100).ReadOnly(_readOnly));
                        }, left: 25, top: 10, labelWidth: 200)
                        .Format();
                        client.PostalAddress.Initialise(true);
                        this.kgbPostalAddress.Panel.Initialise<AddressDetail>(client.PostalAddress, cntr =>
                        {
                            cntr.For(x => x.Line1, "Address Number", new MetroTextBoxEditor(100).ReadOnly(_readOnly));
                            cntr.For(x => x.Line2, "Street", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Line3, "Suburb", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Line4, "City", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.Code, "Code", new MetroNumberEditor(100).ReadOnly(_readOnly));
                        }, left: 25, top: 10, labelWidth: 200)
                        .Format();

                        metroButton_CopyAddress.Enabled = !_readOnly;
                        metroButton_CopyAddress.Click -= metroButton_CopyAddress_Click;
                        metroButton_CopyAddress.Click += metroButton_CopyAddress_Click;
                        #endregion

                        #region Contact Details
                        client.ClientContacts.Initialise(true);
                        this.kgbMemberContact.Panel.Initialise<ClientContacts>(client.ClientContacts, cntr =>
                        {
                            cntr.For(x => x.HomeTel, "Home Telephone", new MetroNumberEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.BussTel, "Work Telephone", new MetroNumberEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.CellNo, "Cellphone Number", new MetroNumberEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.EMailAddr, "Email", new MetroTextBoxEditor(325).ReadOnly(_readOnly));
                        }, left: 25, top: 10, labelWidth: 125
                        )
                        .Format();

                        client.SpouseContacts.Initialise(true);
                        this.kgbSpouseContact.Panel.Initialise<ClientContacts>(client.SpouseContacts, cntr =>
                        {
                            cntr.For(x => x.HomeTel, "Home Telephone", new MetroNumberEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.BussTel, "Work Telephone", new MetroNumberEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.CellNo, "Cellphone Number", new MetroNumberEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.EMailAddr, "Email", new MetroTextBoxEditor(325).ReadOnly(_readOnly));
                        }, left: 25, top: 10, labelWidth: 125
                            )
                        .Format();

                        client.ClientContacts.Initialise();
                        this.dataGrid_AdditionalContacts.Initialise1<ContactDetail>(client.ClientContacts.ContactDetailsBindingList, column =>
                        {
                            column.For(c => c.Type, "Contact Type", new ComboBoxEditor(ListDataItemType.ContactTypes), MinWidth: 100);
                            column.For(c => c.Description, "Description", MinWidth: 200);
                            column.For(c => c.Text, "Value", MinWidth: 100);
                        }
                        , ReadOnly: _readOnly, AllowDelete: true
                        ).Format1(_readOnly);

                        metroButton_MemberContactSync.Enabled = !_readOnly;
                        metroButton_MemberContactSync.Click -= metroButton_MemberContactSync_Click;
                        metroButton_MemberContactSync.Click += metroButton_MemberContactSync_Click;

                        metroButton_SpouseContactSync.Enabled = !_readOnly;
                        metroButton_SpouseContactSync.Click -= metroButton_SpouseContactSync_Click;
                        metroButton_SpouseContactSync.Click += metroButton_SpouseContactSync_Click;

                        this.chbMemberSynced.Checked = !String.IsNullOrEmpty(client.ClientContacts.Status);
                        this.chbMemberSynced.Checked = !String.IsNullOrEmpty(client.SpouseContacts.Status);
                        #endregion

                        #region Bank Details
                        client.BankDetails.Initialise(true);
                        this.kgbBankingDetails.Panel.Initialise<BankDetail>(client.BankDetails, cntr =>
                        {
                            cntr.For(x => x.BnkName, "Bank Name", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.Banks)).ReadOnly(_readOnly));
                            cntr.For(x => x.BrnchName, "Branch Name", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.BrnchCode, "Branch Code", new MetroTextBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.AcctName, "Account Name", new MetroTextBoxEditor(325).ReadOnly(_readOnly));
                            cntr.For(x => x.AcctType, "Account Type", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.BnkAcctTypes)).ReadOnly(_readOnly));
                            cntr.For(x => x.AcctNumber, "Account Number", new MetroTextBoxEditor().ReadOnly(_readOnly));
                        }, left: 25, top: 10, labelWidth: 125, IsLoading: true
                        )
                        .Format();

                        this.kgbAccountVerification.Panel.Initialise<BankDetail>(client.BankDetails, cntr =>
                        {
                            cntr.For(x => x.AcctVerified, "Account Verified", new MetroCheckBoxEditor().ReadOnly(_readOnly));
                            cntr.For(x => x.AcctReason, "Comments", new MetroMultiLineTextBoxEditor(300, 120).ReadOnly(_readOnly));

                        }, left: 25, top: 10, labelWidth: 125
                        )
                        .Format();
                        #endregion

                        #region Additional Info
                        client.ClientAdditionalInfo.Initialise();
                        this.dataGrid_AdditionalInfo.Initialise1<AdditionalInfo>(client.ClientAdditionalInfo.AdditionalInfoBindingList, column =>
                        {
                            column.For(c => c.InfoName, "Info Name", MinWidth: 100);
                            column.For(c => c.InfoValue, "Info Value", MinWidth: 200);
                        }
                        , ReadOnly: _readOnly
                        , AllowDelete: true
                        ).Format1(_readOnly);
                        #endregion

                        #region Client Fees
                        client.ClientFees.Initialise();
                        this.dataGrid_ClientFees.Initialise1<ClientFee>(client.ClientFees.FeesBindingList, column =>
                        {
                            column.For(c => c.ManagementFee, "Plan Fee (p/m)", new CurrencyEditor(), Width: 150);
                            column.For(c => c.InitialFee, "Initial Fee (% )", new DecimalEditor(), Width: 150);
                            column.For(c => c.OngoingFee, "Annual Fee (% p/a)", new DecimalEditor(), Width: 150);
                            column.For(c => c.EffectiveDate, "Effective From", new DateEditor(), Width: 150);
                        }
                    , ReadOnly: _readOnly, AllowDelete: true
                    ).Format1(_readOnly);
                        #endregion

                        break;
                    case 1:
                        #region Assets and Liabilities

                        //Initialise client portfolio so that values are present for graph display
                        client.ClientPortfolio.ServiceProvider = Program.ServiceProviders; // to calculate Fund risk values

                        client.ClientPortfolio.Initialise();
                        client.ClientPortfolio.Calculate();


                        client.ClientAssets.Initialise();
                        this.dataGrid_ClientAssets.Initialise1<Asset>(client.ClientAssets.AssetsBindingList, column =>
                        {
                            column.For(x => x.Type, "Asset Type", new ComboListEditor(ListDataItemType.AssetTypes));
                            column.For(x => x.Class, "Asset Class", new ComboListEditor(ListDataItemType.AssetClass));
                            column.For(x => x.Description, "Description");
                            column.For(c => c.Value, "Asset Value", new CurrencyEditor());

                        }, ReadOnly: _readOnly, AllowDelete: true,
                        PropertyChangedHandler: AssetsLiabilities_propertyChanged_EventHandler
                        ).Format1(_readOnly);

                        client.ClientLiabilities.Initialise();
                        this.dataGrid_ClientLiabilities.Initialise1<Liability>(client.ClientLiabilities.LiabilitiesBindingList, column =>
                        {
                            column.For(x => x.Type, "Liability Type", new ComboListEditor(ListDataItemType.LiabilityTypes));
                            column.For(x => x.Class, "Liability Class", new ComboListEditor(ListDataItemType.LiabilityClass));
                            column.For(x => x.Description, "Description");
                            column.For(c => c.Value, "Amount Outstanding", new CurrencyEditor());

                        }, ReadOnly: _readOnly, AllowDelete: true,
                        PropertyChangedHandler: AssetsLiabilities_propertyChanged_EventHandler
                        ).Format1(_readOnly);

                        #endregion

                        RefreshAssetLiabilitiesSummary();
                        break;
                    case 2:

                        //Initialise client portfolio so that values are present income and expense grid
                        client.ClientPortfolio.ServiceProvider = Program.ServiceProviders; // to calculate Fund risk values

                        client.ClientPortfolio.Initialise();
                        client.ClientPortfolio.Calculate();

                        client.ClientIncomes.Initialise();
                        #region Income

                        this.dataGrid_ClientIncomes.Initialise1<Income>(client.ClientIncomes.IncomesBindingList, column =>
                        {
                            column.For(x => x.Type, "Income Type", new ComboBoxEditor(ListDataItemType.IncomeTypes));
                            column.For(x => x.Class, "Frequency", new ComboListEditor(ListDataItemType.IncomeClass));
                            column.For(x => x.Description, "Description", new StringEditor());
                            column.For(c => c.Value, "Income", new CurrencyEditor());

                        }, ReadOnly: _readOnly, AllowDelete: true,
                        PropertyChangedHandler: IncomeExpenses_propertyChanged_EventHandler
                        ).Format1(_readOnly);
                        #endregion

                        client.ClientExpenses.Initialise();
                        #region Expenses
                        this.dataGrid_ClientExpenses.Initialise1<Expense>(client.ClientExpenses.ExpensesBindingList, column =>
                        {
                            column.For(x => x.Type, "Expense Type", new ComboBoxEditor(ListDataItemType.ExpenseTypes));
                            column.For(x => x.Class, "Frequency", new ComboListEditor(ListDataItemType.ExpenseClass));
                            column.For(x => x.Description, "Description", new StringEditor());
                            column.For(c => c.Value, "Expense", new CurrencyEditor());
                            column.For(c => c.RetirePerc, "Retire %", new ComboBoxEditor<double>(ListDataItemType.ExpensePerc), Tooltip: "The % of the Expense required at Retirement.");
                            column.For(c => c.DisabilityPerc, "Disability %", new ComboBoxEditor<double>(ListDataItemType.ExpensePerc), Tooltip: "The % of the Expense required on Disability.");
                            column.For(c => c.LifePerc, "Life %", new ComboBoxEditor<double>(ListDataItemType.ExpensePerc), Tooltip: "The % of the Expense required on Death.");
                        }, ReadOnly: _readOnly, AllowDelete: true,
                            PropertyChangedHandler: IncomeExpenses_propertyChanged_EventHandler
                            ).Format1(_readOnly, fixedCols: 1);

                        #endregion

                        RefreshIncomeExpensesSummary();
                        break;
                    case 3:
                        #region CurrentPortfolio

                        client.ClientPortfolio.ServiceProvider = Program.ServiceProviders; // to calculate Fund risk values

                        client.ClientPortfolio.Initialise();
                        client.ClientPortfolio.Calculate();

                        this.dataGrid_RetirementPortfolio.Initialise1<Retirement>(client.ClientPortfolio.RetirementsBindingList, column =>
                        {
                            column.For(x => x.Type, "Product Type", new ComboListEditor(ListDataItemType.RetirementAssetClass), MinWidth: 150);
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps), MinWidth: 150);
                            column.For(c => c.ReferenceNo, "Policy No.", new StringEditor());
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            //column.For(c => c.InitialAmount, "Lump Sum", new MetroCurrencyEditor());
                            //column.For(c => c.MonthlyContribution, "Premium p/m", new CurrencyEditor());
                            column.For(c => c.MonthlyContribution, "Premium p/m", new CurrencyEditor());
                            column.For(c => c.EscalationPercentage, "Escalation", new DecimalEditor());
                            column.For(c => c.GrowthPercentage, "Growth", new DecimalEditor());
                            column.For(c => c.CurrentAmount, "Current Value", new CurrencyEditor(true));
                            column.For(c => c.FutureAmount, "Retirement Value", new CurrencyEditor(true));
                            column.For(c => c.Status, "Policy Status", new StringEditor(true));
                        },
                        PropertyChangedHandler: ClientPortfolio_propertyChanged_EventHandler, //event handler when a property changes
                        RowHeaderSelectEventHandler: RetirementPortfolio_RowHeaderSelectEventHandler,//for the Policy Funds details view
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.RetirementPortfolio, _readOnly, client, OnCompleted: ClientPortfolio_PolicyChanged_EventHandler),
                        ReadOnly: _readOnlyForAdminAdvisor,
                        AllowDelete: false)
                        .Format1(_readOnlyForAdminAdvisor, fixedCols: 3);

                        this.dataGrid_InvestmentPortfolio.Initialise1<Investment>(client.ClientPortfolio.InvestmentsBindingList, column =>
                        {
                            column.For(x => x.Type, "Product Type", new ComboBoxEditor(ListDataItemType.InvestmentClass), MinWidth: 150);
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps));
                            column.For(c => c.ReferenceNo, "Policy No.", new StringEditor());
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            //column.For(c => c.InitialAmount, "Lump Sum", new CurrencyEditor());
                            
                            column.For(c => c.MonthlyContribution, "Premium p/m", new CurrencyEditor());
                            column.For(c => c.EscalationPercentage, "Escalation", new DecimalEditor());
                            column.For(c => c.GrowthPercentage, "Growth", new DecimalEditor());
                            column.For(c => c.InvestmentAge, "Invest Age", new NumericEditor(), Tooltip: "The age at which you will disinvest.");
                            column.For(c => c.CurrentAmount, "Current Value", new CurrencyEditor(true));
                            column.For(c => c.FutureAmount, "Future  Value", new CurrencyEditor(true));
                            column.For(c => c.Status, "Policy Status", new StringEditor(true));

                        },
                        PropertyChangedHandler: ClientPortfolio_propertyChanged_EventHandler,
                        RowHeaderSelectEventHandler: InvestmentPortfolio_RowHeaderSelectEventHandler,//for the Policy Funds details view
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.InvestmentPortfolio, _readOnly, client, OnCompleted: ClientPortfolio_PolicyChanged_EventHandler),
                        ReadOnly: _readOnlyForAdminAdvisor,
                        AllowDelete: false)
                        .Format1(_readOnlyForAdminAdvisor, fixedCols: 3);

                        this.dataGrid_EducationPortfolio.Initialise1<Education>(client.ClientPortfolio.EducationsBindingList, column =>
                        {
                            column.For(x => x.Type, "Product Type", new ComboBoxEditor(ListDataItemType.InvestmentClass));
                            column.For(x => x.DependentName, "Policy Owner", new ComboListEditor(client.ClientDependents.Dependents.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.CurrentAge, "Age", new NumericEditor(true));
                            column.For(c => c.InvestmentAge, "University Age", new NumericEditor());
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps));
                            column.For(c => c.ReferenceNo, "Policy No.", new StringEditor());
                            //column.For(c => c.InitialAmount, "Lump Sum", new CurrencyEditor());
                            column.For(c => c.MonthlyContribution, "Premium", new CurrencyEditor());
                            column.For(c => c.EscalationPercentage, "Escalation", new PercentageEditor());
                            column.For(c => c.GrowthPercentage, "Growth", new PercentageEditor());
                            column.For(c => c.CurrentAmount, "Current Value", new CurrencyEditor(true));
                            column.For(c => c.FutureAmount, "Future  Value", new CurrencyEditor(true));
                            column.For(c => c.Status, "Policy Status", new StringEditor(true));

                        },
                        PropertyChangedHandler: Education_propertyChanged_EventHandler,
                        RowHeaderSelectEventHandler: EducationPortfolio_RowHeaderSelectEventHandler,//for the Policy Funds details view
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.EducationPortfolio, _readOnly, client, OnCompleted: ClientPortfolio_PolicyChanged_EventHandler),
                        ReadOnly: _readOnlyForAdminAdvisor,
                        AllowDelete: false)
                        .Format1(_readOnlyForAdminAdvisor, fixedCols: 5);


                        this.dataGrid_MedicalPortfolio.Initialise1<Medical>(client.ClientPortfolio.MedicalsBindingList, column =>
                        {
                            column.For(x => x.Type, "Provider", new ComboListEditor(MedicalAids));
                            column.For(x => x.Insured, "Principal Member", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(x => x.Description, "Plan", new ComboBoxEditor(MedicalAidPlans));
                            column.For(c => c.ReferenceNo, "MedicalAid/GAP No.", new StringEditor());
                            column.For(c => c.MonthlyContribution, "Premium", new CurrencyEditor());
                            // column.For(c => c.InitialAmount, "Cover Amount", new MetroCurrencyEditor().ReadOnly(true));
                            column.For(c => c.Status, "Policy Status", new StringEditor(true));
                        },
                        PropertyChangedHandler: Medical_propertyChanged_EventHandler,
                        RowHeaderSelectEventHandler: MedicalPortfolio_RowHeaderSelectEventHandler,//for the Policy Funds details view
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.MedicalPortfolio, _readOnly, client, OnCompleted: ClientPortfolio_PolicyChanged_EventHandler),
                        ReadOnly: _readOnlyForAdminAdvisor,
                        AllowDelete: false)
                        .Format1(_readOnlyForAdminAdvisor);

                        this.dataGrid_LifePortfolio.Initialise1<Life>(client.ClientPortfolio.LifesBindingList, column =>
                        {
                            column.For(x => x.Type, "Cover Type", new ComboListEditor(ListDataItemType.RiskPolicyType)); //new MetroComboListEditor().DataSourceList(LifeInsurers));
                            column.For(x => x.Description, "Insurer", new ComboListEditor(LifeInsurers));// new MetroComboBoxEditor().DataSourceList(LifeInsurersPlans)); ;
                            column.For(c => c.ReferenceNo, "Policy No.", new StringEditor());
                            column.For(x => x.Insured, "Insured", new ComboBoxEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.MonthlyContribution, "Premium", new CurrencyEditor());
                            //column.For(c => c.InitialAmount, "Total Amount", new MetroCurrencyEditor().ReadOnly(true));
                            column.For(c => c.Status, "Policy Status", new StringEditor(true));
                        },
                        PropertyChangedHandler: Lifes_propertyChanged_EventHandler,
                        RowHeaderSelectEventHandler: LifePortfolio_RowHeaderSelectEventHandler,//for the Policy Funds details view
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.LifePortfolio, _readOnly, client, OnCompleted: ClientPortfolio_PolicyChanged_EventHandler),
                        ReadOnly: _readOnlyForAdminAdvisor,
                        AllowDelete: false)
                        .Formatt(_readOnlyForAdminAdvisor);

                        this.dataGrid_AssetsPortfolio.Initialise1<IncomeAsset>(client.ClientPortfolio.IncomeAssetsBindingList, column =>
                        {
                            column.For(x => x.Type, "Asset Type", new ComboListEditor(ListDataItemType.InvestmentClass));
                            column.For(x => x.Description, "Description", new StringEditor());
                            column.For(c => c.InitialAmount, "Current Value", new CurrencyEditor());
                            //column.For(c => c.MonthlyPayment, "Bond p/m", new MetroCurrencyEditor());
                            column.For(c => c.MonthlyContribution, "Income p/m", new CurrencyEditor());
                            column.For(c => c.EscalationPercentage, "Escalation %pa", new PercentageEditor(), Tooltip: "Annual % Escalation of the Income Amount");
                            column.For(c => c.GrowthPercentage, "Growth %pa", new PercentageEditor(), Tooltip: "Annual growth in the value of the Asset.");
                            column.For(c => c.RetirementIncome, "Retirement Income", Tooltip: "Mark if this Asset is to be used as Retirement Income");
                            column.For(c => c.InvestmentAge, "Invest Age", new NumericEditor(), Tooltip: "The age at which you plan to dispose of the Asset if not marked as Retirement Income.");
                            column.For(c => c.FutureAmount, "Future Value", new CurrencyEditor(true));
                            column.For(c => c.FutureIncome, "Future Income", new CurrencyEditor(true));
                            // column.For(c => c.Status, "Policy Status", new MetroStringEditor().ReadOnly(true));

                        },
                        PropertyChangedHandler: ClientPortfolio_propertyChanged_EventHandler,
                        //RowHeaderSelectEventHandler: AssetsPortfolio_RowHeaderSelectEventHandler, //no funds
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.AssetPortfolio, _readOnly, client),
                        ReadOnly: _readOnlyForAdminAdvisor,
                        AllowDelete: false)
                        .Format1(_readOnlyForAdminAdvisor, fixedCols: 3);

                        #endregion

                        RefreshPortfolioSummary();
                        break;
                    case 4:
                        #region Retirement FNA

                        //Initialise client portfolio so that values are present for graph display
                        client.ClientPortfolio.ServiceProvider = Program.ServiceProviders; // to calculate Fund risk values

                        client.ClientPortfolio.Initialise();
                        client.ClientPortfolio.Calculate();

                        //Initialise Retirement FNA
                        client.ClientFna.ServiceProvider = Program.ServiceProviders;

                        client.ClientFna.Initialise();

                        client.UpdateRetirementFNA();

                        this.metroPanel_RetireFnaSettings.Initialise<ClientFna>(client.ClientFna, cntr =>
                        {
                            cntr.For(x => x._CurrentAge, "Age", new MetroTextBoxEditor(Width: 50).ReadOnly(true));
                            cntr.For(x => x.RetirementAge, "Retire Age", new MetroNumberEditor(Width: 50).ReadOnly(_readOnly));
                            cntr.For(x => x.LifeExpectancy, "Life Age", new MetroNumberEditor(Width: 50).ReadOnly(_readOnly));

                        }, left: 5, top: 5, labelWidth: 160, PropertyChangedHandler: RetirementFna_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal)
                        .Format();

                        this.metroPanel_RetireFnaSettings.Initialise<ClientFna>(client.ClientFna, cntr =>
                        {
                            cntr.For(x => x.InflationPercentage, "Inflation %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));
                            cntr.For(x => x.GrowthPercentage, "Investment Growth %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));
                            cntr.For(x => x.EsclPercentage, "Investment Escalation %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));

                        }, left: 5, top: 100, labelWidth: 160, PropertyChangedHandler: RetirementFna_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal)
                        .Format();

                        this.metroPanel_RetireFnaSettings.Initialise<ClientFna>(client.ClientFna, cntr =>
                        {
                            cntr.For(x => x.InvestmentYears, "Invest yrs", new MetroNumberEditor(Width: 50).ReadOnly(true));
                            cntr.For(x => x.RetirementYears, "Retire yrs", new MetroNumberEditor(Width: 50).ReadOnly(true));
                        }, left: 5, top: 195, labelWidth: 160, PropertyChangedHandler: RetirementFna_propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal)
                        .Format();

                        this.metroPanel_RetireFnaSummary.Initialise<ClientFna>(client.ClientFna, cntr =>
                        {
                            cntr.For(x => x.Shortfall, "Shortfall +/-", new MetroCurrencyEditor(Width: 150).ReadOnly(true));
                            cntr.For(x => x.RequiredPremium, "Required p/m", new MetroCurrencyEditor(Width: 150).ReadOnly(true));
                        }, left: 5, top: 5, labelWidth: 100, PropertyChangedHandler: propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal)
                        .Format();

                        this.metroPanel_RetireFnaSummary.Initialise<ClientFna>(client.ClientFna, cntr =>
                        {
                            cntr.For(x => x.AvailableCash, "Available Cash", new MetroCurrencyEditor(Width: 150).ReadOnly(true));
                            cntr.For(x => x.AddContribs, "Additional p/m", new MetroCurrencyEditor(Width: 150).ReadOnly(true));
                            cntr.For(x => x.NettCashFlow, "Nett Cash Flow", new MetroCurrencyEditor(Width: 150).ReadOnly(true));
                        }, left: 5, top: 75, labelWidth: 100, PropertyChangedHandler: propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal)
                        .Format();

                        this.metroPanel_RetireFnaSummary.Initialise<ClientFna>(client.ClientFna, cntr =>
                        {
                            cntr.For(x => x.WantsTotal, "Total Wants", new MetroCurrencyEditor(Width: 150).ReadOnly(true));
                            cntr.For(x => x.HavesTotal, "Total Haves", new MetroCurrencyEditor(Width: 150).ReadOnly(true));
                            cntr.For(x => x.NeedsTotal, "Total Needs", new MetroCurrencyEditor(Width: 150).ReadOnly(true));

                        }, left: 5, top: 165, labelWidth: 100, PropertyChangedHandler: propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal)
                        .Format();

                        //---- Wants
                        this.dataGrid_RetireFNA_Wants.Initialise1<Want>(client.ClientFna.WantsBindingList, column =>
                        {
                            column.For(x => x.Description, "Description", new ComboBoxEditor(ListDataItemType.RetirementWants));
                            column.For(x => x.Type, "Frequency", new ComboBoxEditor(ListDataItemType.RetirementWantsClass));
                            column.For(c => c.CalculateInitialAmount, "Auto Calc", Tooltip: "Calculate Today's Value from the Expenses Tab ?");
                            column.For(c => c.InitialAmount, "Today's Value", new CurrencyEditor());
                            column.For(c => c.FutureAmount, "Future Value", new CurrencyEditor(true));
                            column.For(c => c.GrowthPercentage, "Annuity Growth %", new PercentageEditor(), Tooltip: "The expected growth rate of the annuity taken at retirement");
                            column.For(c => c.EscalationPercentage, "Annuity Escalation %", new PercentageEditor(), Tooltip: "The expected annual increase of the annuity at retirement");
                            column.For(c => c._RequiredAmount, "Required Investment", new CurrencyEditor(true));
                        },
                        PropertyChangedHandler: RetirementFna_propertyChanged_EventHandler,
                        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.RetirementPortfolio,ReadOnly),
                        ReadOnly: _readOnlyForAdminAdvisor,
                        AllowDelete: true)
                        .Format1(_readOnly);

                        //------Haves
                        this.dataGrid_RetireFNA_Have.Initialise1<Have>(client.ClientFna.HavesBindingList, column =>
                        {

                            column.For(x => x.Type, "Product Type", new StringEditor(true));
                            column.For(x => x.Description, "Lisp", new StringEditor(true));
                            column.For(c => c.CurrentAmount, "Current Value", new CurrencyEditor(true));
                            column.For(c => c.MonthlyContribution, "Premium", new CurrencyEditor(true));
                            column.For(c => c.GrowthPercentage, "Growth %", new StringEditor(true));
                            column.For(c => c.EscalationPercentage, "Escalation %", new StringEditor(true));
                            column.For(c => c.FutureAmount, "Future Value", new CurrencyEditor(true));
                            //column.For(c => c.FutureIncome, "Future Income", new CurrencyEditor(true));
                        },
                        PropertyChangedHandler: RetirementFna_propertyChanged_EventHandler,
                        ReadOnly: true,
                        AllowDelete: false,
                        AllowAddNew: false)
                        .Format1(_readOnly);

                        //-----Needs
                        this.dataGrid_RetireFNA_Need.Initialise1<Need>(client.ClientFna.NeedsBindingList, column =>
                        {

                            column.For(x => x.Type, "Product Type", new ComboListEditor(ListDataItemType.RetirementAssetClass));
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps));
                            //column.For(c => c.ReferenceNo, "Policy No.", new MetroStringEditor());
                            //YJ 2020-11-25
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName"), _readOnly));
                            column.For(c => c.InitialAmount, "Lump Sum", new CurrencyEditor(), Tooltip: "The initial deposit.");
                            column.For(c => c.NewMonthlyContribution, "Premium", new CurrencyEditor());
                            column.For(c => c.EscalationPercentage, "Escalation %", new PercentageEditor());
                            column.For(c => c.GrowthPercentage, "Growth %", new PercentageEditor());
                            // column.For(c => c.CurrentAmount, "Current Value", new MetroCurrencyEditor());//.ReadOnly(true)
                            column.For(c => c.FutureAmount, "Retirement Value", new CurrencyEditor(true));
                            column.For(c => c.Status, "Advice Status", new StringEditor(true));
                            //column.For(c => c.UpdateDate, "Date", new MetroDateEditor().ReadOnly(true));
                        },
                        PropertyChangedHandler: RetirementFna_propertyChanged_EventHandler,
                        RowHeaderSelectEventHandler: RetirementNeed_RowHeaderSelectEventHandler,
                        ItemDeleteEventHandler: FnaNeed_ItemDeleting_EventHandler,//YJ 2021-11-05
                        ItemDeletedEventHandler: FnaNeed_ItemDeleted_EventHandler,//YJ 2021-11-05
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.RetirementFna, _readOnly, client, OnCompleted: RetirementFna_needCompleted_EventHandler),
                        ReadOnly: _readOnlyForAdminAdvisor,
                        AllowDelete: !_readOnlyForAdminAdvisor)
                        .Format1(_readOnly, fixedCols: 3);

                        #endregion
                        RefreshClientFnaSummary();
                        break;
                    case 5:
                        #region Non-Retirement FNA

                        //Investment Needs
                        client.ClientFnaInvestment.ServiceProvider = Program.ServiceProviders; // to calculate Fund risk values

                        client.ClientFnaInvestment.Initialise();
                        client.ClientFnaInvestment.Calculate();

                        this.dataGrid_InvestmentFna.Initialise1<InvestmentNeed>(client.ClientFnaInvestment.InvestmentNeedsBindingList, column =>
                        {
                            column.For(x => x.Type, "Investment Type", new ComboListEditor(ListDataItemType.InvestmentClass));
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps));
                            //YJ 2020-11-25
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName"), _readOnly));
                            column.For(c => c.RequiredAmount, "Today's Value", new CurrencyEditor());
                            column.For(c => c.InitialAmount, "Deposit", new CurrencyEditor());
                            column.For(c => c.InvestmentYears, "Invest Yrs", new NumericEditor(), Tooltip: "The number of years you wish to invest for.");
                            column.For(c => c.GrowthPercentage, "Growth %", new PercentageEditor());
                            column.For(c => c.EscalationPercentage, "Escalation %", new PercentageEditor());
                            column.For(c => c.FutureAmount, "Future  Value", new CurrencyEditor(true));
                            column.For(c => c.NewMonthlyContribution, "Required p/m", new CurrencyEditor(true));
                            column.For(c => c.Status, "Status", new StringEditor(true));

                        },
                        PropertyChangedHandler: InvestmentNeed_propertyChanged_EventHandler,
                        RowHeaderSelectEventHandler: InvestmentNeed_RowHeaderSelectEventHandler,
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.InvestmentFna, _readOnly, client),
                        ItemDeleteEventHandler: FnaNeed_ItemDeleting_EventHandler,//YJ 2021-11-05
                        ItemDeletedEventHandler: FnaNeed_ItemDeleted_EventHandler,//YJ 2021-11-05
                        ReadOnly: _readOnlyForAdminAdvisor,
                        AllowDelete: true)
                        .Format1(_readOnly);


                        //Education Needs
                        client.ClientFnaEducation.ServiceProvider = Program.ServiceProviders; // to calculate Fund risk values

                        client.ClientFnaEducation.Initialise();
                        client.ClientFnaEducation.Calculate();

                        this.dataGrid_EducationFna.Initialise1<EducationNeed>(client.ClientFnaEducation.EducationNeedsBindingList, column =>
                        {
                            column.For(x => x.DependentName, "Dependent Name", new ComboListEditor(client.ClientDependents.Dependents.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            //column.For(c => c.DependentDOB, "Birth Date", new MetroDateEditor(100).ReadOnly(true));
                            column.For(c => c.CurrentAge, "Age", new NumberEditor(true));
                            column.For(c => c.InvestmentAge, "Univ Age", new NumberEditor(), Tooltip: "The Age at which the Dependent is expected to enter Tertiary education.");
                            column.For(x => x.Type, "Product Type", new ComboListEditor(ListDataItemType.InvestmentClass));
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps));
                            //YJ 2020-11-25
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.RequiredAmount, "Today's Cost", new CurrencyEditor());
                            column.For(c => c.InitialAmount, "Deposit", new CurrencyEditor());
                            //column.For(c => c.GrowthPercentage, "Growth %", new MetroPercentageEditor());
                            //column.For(c => c.EscalationPercentage, "Escalation %", new MetroPercentageEditor());
                            column.For(c => c.FutureAmount, "Future  Cost", new CurrencyEditor(true));
                            column.For(c => c.NewMonthlyContribution, "Required p/m", new CurrencyEditor(true));
                            column.For(c => c.Status, "Status", new StringEditor(true));

                        },
                            PropertyChangedHandler: EducationNeed_propertyChanged_EventHandler,
                            RowHeaderSelectEventHandler: EducationNeed_RowHeaderSelectEventHandler,
                            ContextMenu: new DataGridContextMenu(this, ContextMenuType.EducationFna, _readOnly, client),
                            ItemDeleteEventHandler: FnaNeed_ItemDeleting_EventHandler,//YJ 2021-11-05
                            ItemDeletedEventHandler: FnaNeed_ItemDeleted_EventHandler,//YJ 2021-11-05
                            ReadOnly: _readOnlyForAdminAdvisor,
                            AllowDelete: true)
                            .Format1(_readOnly);

                        // Risk Cover Needs
                        ClientFnaRisk.Initialise();
                        ClientFnaRisk.Calculate();

                        this.dataGrid_LifeRiskFna.Initialise1<RiskCoverNeed>(ClientFnaRisk.RiskCoverNeedsBindingList, column =>
                        {
                            column.For(x => x.CoverType, "Cover Type", new ComboListEditor(ListDataItemType.RiskCoverType));
                            // column.For(x => x.Type, "Product Type", new MetroComboBoxEditor(120).DataSourceList(Program.listData.List(ListDataItemType.InvestmentClass)));
                            column.For(x => x.Type, "Risk Insurer", new ComboListEditor(LifeInsurers));
                            column.For(x => x.Insured, "Insured ", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.InvestmentAge, "Risk Age", new NumberEditor(), Tooltip: "The Age at which the risk event is modelled to occur");
                            column.For(c => c.InitialAmount, "Lump Sum Required", new CurrencyEditor(), Tooltip: "Enter if lump sum amount is required");
                            column.For(c => c.CalculateCurrentAmount, "Auto Calc", Tooltip: "Calculate Income Required from the Expenses Tab ?");
                            column.For(c => c.CurrentAmount, "Income Required (p/m)", new CurrencyEditor(), Tooltip: "Enter if a monthly income is required.");
                            column.For(c => c.RequiredAmount, "Future Income (p/m)", new CurrencyEditor(true), Tooltip: "The future inflationary Income required at the risk event age.");
                            column.For(c => c.InvestmentYears, "Income Years", new NumberEditor(), Tooltip: "How many years do you need an income for?");
                            //column.For(c => c.GrowthPercentage, "Growth %", new MetroPercentageEditor());
                            //column.For(c => c.EscalationPercentage, "Escalation %", new MetroPercentageEditor());
                            column.For(c => c.FutureAmount, "Cover Required", new CurrencyEditor(true));
                            //column.For(c => c.NewMonthlyContribution, "Required p/m", new MetroCurrencyEditor().ReadOnly(true));
                            column.For(c => c.Status, "Status", new StringEditor(true));

                        },
                        PropertyChangedHandler: RiskCoverNeed_propertyChanged_EventHandler,
                        RowHeaderSelectEventHandler: RiskCoverNeed_RowHeaderSelectEventHandler,
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.RiskCoverFna, _readOnly, client),
                        ItemDeleteEventHandler: FnaNeed_ItemDeleting_EventHandler,//YJ 2021-11-05
                        ItemDeletedEventHandler: FnaNeed_ItemDeleted_EventHandler,//YJ 2021-11-05
                        ReadOnly: _readOnlyForAdminAdvisor,
                        AllowDelete: true)
                        .Format1(_readOnly, fixedCols: 3);

                        #endregion

                        //Fire event to show first tab :Investments
                        MetroTabControl_InvestmentsFNA_SelectedIndexChanged(metroTabControl_InvestmentsFNA, new EventArgs());
                        break;
                    case 6: //Estate Duty Analysis

                        EstateAnalysis.Initialise();

                        #region Estate Duty Parameters

                        this.pnl_EstDutyParams.Controls.Clear();

                        this.pnl_EstDutyParams.Initialise<EstateAnalysis>(EstateAnalysis, cntr =>
                        {
                            cntr.For(x => x.MaritalRegime, "Marital Regime", new MetroComboBoxEditor(150).DataSourceList(Program.listData.List(ListDataItemType.MaritalRegimes)).ReadOnly(_readOnly).ControlValueChanged(EstateDuty_MaritalRegimeChanged));
                            cntr.For(x => x.TaxRate, "Client's TaxRate (%)", new MetroPercentageEditor(50).ReadOnly(_readOnly));
                            cntr.For(x => x.ExecutorsPerc, "Executor's Fees excl. (%)", new MetroPercentageEditor(50).ReadOnly(_readOnly));
                            cntr.For(x => x.MastersFees, "Master's Fees (R)", new MetroNumberEditor(150).ReadOnly(_readOnly));
                            cntr.For(x => x.FuneralFees, "Funeral's Fees (R)", new MetroNumberEditor(150).ReadOnly(_readOnly));
                            //cntr.For(x => x.CGTInclRatePerc, "CGT Incl Rate (%)", new MetroPercentageEditor(50).ReadOnly(true));
                            //cntr.For(x => x.VAT, "VAT (%)", new MetroPercentageEditor(50).ReadOnly(true));

                        }, left: 10, top: 5, labelWidth: 150, controlsLayout: ControlsLayout.Horizontal, PropertyChangedHandler: EstateAnalysis_PropertyChangedEvent
                            ).Format();

                        this.pnl_EstDutyParams.Enabled = !_readOnlyForAdminAdvisor;

                        this.pnl_EstDutyParams.Initialise<EstateAnalysis>(EstateAnalysis, cntr =>
                        {
                            cntr.For(x => x.GrossTotal, "Gross Estate (R)", new MetroCurrencyEditor(150).ReadOnly(true));
                            cntr.For(x => x.NettTotal, "Nett Estate (R)", new MetroCurrencyEditor(150).ReadOnly(true));
                            cntr.For(x => x.EstateDuty, "less Estate Duty (R)", new MetroCurrencyEditor(150).ReadOnly(true));
                            cntr.For(x => x.Total, "Final Estate (R)", new MetroCurrencyEditor(150).ReadOnly(true));
                            cntr.For(x => x.LiquidityTotal, "Liquidity +/- (R)", new MetroCurrencyEditor(150).ReadOnly(true));
                        }, left: 330, top: 5, labelWidth: 150, controlsLayout: ControlsLayout.Horizontal
                            ).Format();

                        //this.pnl_EstDutyParams.Initialise<EstateAnalysis>(EstateAnalysis, cntr =>
                        //{
                        //    cntr.For(x => x.LiquidityTotal, "Liquidity +/- (R)", new MetroCurrencyEditor(150).ReadOnly(true));
                        //}, left: 330, top: 90, labelWidth: 150, controlsLayout: ControlsLayout.Horizontal
                        //).Format();

                        this.pnl_EstDutyParams.Initialise<EstateAnalysis>(EstateAnalysis, cntr =>
                        {
                            cntr.For(x => x.PrintSummary, "", new MetroButtonEditor(90, 28).ReadOnly(_readOnly).OnEditorClick(EstateDuty_printSummaryCalc));
                        }, left: 640, top: 5, labelWidth: 0, controlsLayout: ControlsLayout.Horizontal
                        ).Format();

                        #endregion

                        #region Assets and Liabilities
                        this.AL_tabPage1.Format("Fixed Property |");
                        this.dGrid_AL_FixedProperty.Initialise1<Asset>(EstateAnalysis.PropertyAssets, column =>
                        {
                            column.For(x => x.Description, "Description", new StringEditor(true));
                            column.For(x => x.Class, "Class", new ComboBoxEditor(ListDataItemType.AssetClass, true));
                            column.For(x => x.Type, "Type", new ComboBoxEditor(ListDataItemType.AssetTypes, true));
                            column.For(x => x.BequethTo, "Bequeath/Beneficiary", new ComboListEditor(client.EstateOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.BaseCost, "Base Cost", new CurrencyEditor());
                            column.For(c => c.Value, "Current Value", new CurrencyEditor());
                            column.For(c => c.ProfitLoss, "Profit/Loss", new CurrencyEditor(true));

                        }, ReadOnly: _readOnlyForAdminAdvisor, AllowDelete: false, AllowAddNew: false, PropertyChangedHandler: EstateDuty_propertyChanged_EventHandler
                        ).Format1(_readOnly, fixedCols: 1);

                        this.AL_tabPage5.Format("Deemed Property |");
                        this.dGrid_AL_DeemedProperty.Initialise1<Retirement>(EstateAnalysis.RetirementAssets, column =>
                        {
                            column.For(x => x.Description, "Description", new StringEditor(true));
                            column.For(x => x.Type, "Type", new StringEditor(true));
                            column.For(x => x.ReferenceNo, "Policy No", new StringEditor(true));
                            column.For(x => x.Insured, "Owner", new StringEditor(true));
                            column.For(x => x.BequethTo, "Bequeath/Beneficiary", new ComboListEditor(client.EstateOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.CurrentAmount, "Current Value", new CurrencyEditor(true));

                        }, ReadOnly: _readOnlyForAdminAdvisor, AllowDelete: false, AllowAddNew: false, PropertyChangedHandler: EstateDuty_propertyChanged_EventHandler
                        ).Format1(_readOnly, fixedCols: 3);

                        this.AL_tabPage7.Format("Risk Policies |");
                        this.dGrid_AL_RiskPolicies.Initialise1<Life>(EstateAnalysis.LifePolicies, column =>
                        {
                            column.For(x => x.Type, "Cover Type", new StringEditor(true));
                            column.For(x => x.Description, "Insurer", new StringEditor(true));
                            column.For(x => x.ReferenceNo, "Policy No", new StringEditor(true));
                            column.For(x => x.BequethTo, "Bequeath/Beneficiary", new StringEditor(true)); //new ComboListEditor(client.EstateOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName"), true));
                            column.For(c => c.InitialAmount, "Insured Amount", new CurrencyEditor(true));
                        }, ReadOnly: _readOnlyForAdminAdvisor, AllowDelete: false, AllowAddNew: false, PropertyChangedHandler: EstateDuty_propertyChanged_EventHandler
                        ).Format1(_readOnly, fixedCols: 3);

                        this.AL_tabPage3.Format("Other Assets |");
                        this.dGrid_AL_OtherAssets.Initialise1<Asset>(EstateAnalysis.OtherAssets, column =>
                        {
                            column.For(x => x.Description, "Description", new StringEditor(true));
                            column.For(x => x.Class, "Class", new ComboBoxEditor(ListDataItemType.AssetClass, true));
                            //column.For(x => x.Type, "Type", new MetroComboBoxEditor().DataSourceList(Program.listData.List(ListDataItemType.AssetTypes)).ReadOnly(true));
                            column.For(x => x.BequethTo, "Bequeath/Beneficiary", new ComboListEditor(client.EstateOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.BaseCost, "Base Cost", new CurrencyEditor());
                            column.For(c => c.Value, "Current Value", new CurrencyEditor());
                            column.For(c => c.ProfitLoss, "Profit/Loss", new CurrencyEditor(true));

                        }, ReadOnly: _readOnlyForAdminAdvisor, AllowDelete: false, AllowAddNew: false, PropertyChangedHandler: EstateDuty_propertyChanged_EventHandler
                        ).Format1(_readOnly);

                        this.AL_tabPage2.Format("Investments |");
                        this.dGrid_AL_Investments.Initialise1<Investment>(EstateAnalysis.InvestmentAssets, column =>
                        {
                            column.For(x => x.Description, "Description", new StringEditor(true));
                            column.For(x => x.Type, "Type", new StringEditor(true));
                            column.For(x => x.ReferenceNo, "Policy No", new StringEditor(true));
                            column.For(x => x.Insured, "Owner", new StringEditor(true));
                            column.For(x => x.BequethTo, "Bequeath/Beneficiary", new ComboListEditor(client.EstateOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.CurrentAmount, "Current Value", new CurrencyEditor(true));

                        }, ReadOnly: _readOnlyForAdminAdvisor, AllowDelete: false, AllowAddNew: false, PropertyChangedHandler: EstateDuty_propertyChanged_EventHandler
                        ).Format1(_readOnly, fixedCols: 1);


                        this.AL_tabPage4.Format("Liabilities |");
                        this.dGrid_AL_Liabilities.Initialise1<Liability>(EstateAnalysis.Liabilities, column =>
                        {
                            column.For(x => x.Description, "Description", new StringEditor(true));
                            column.For(x => x.Class, "Liability Class", new ComboBoxEditor(ListDataItemType.LiabilityClass, true));
                            column.For(x => x.Owner, "Liable to", new ComboListEditor(client.EstateOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.Value, "Amount Outstanding", new CurrencyEditor());

                        }, ReadOnly: _readOnlyForAdminAdvisor, AllowDelete: false, AllowAddNew: false, PropertyChangedHandler: EstateDuty_propertyChanged_EventHandler
                        ).Format1(_readOnly);

                        this.AL_tabPage6.Format("Other Deductions |");
                        this.dGrid_OtherDeductions.Initialise1<OtherDeduction>(EstateAnalysis.OtherDeductions, column =>
                        {
                            column.For(x => x.Type, "Deduction Type", new ComboBoxEditor(ListDataItemType.OtherDeductionTypes));
                            column.For(x => x.Description, "Bequeath/Beneficiary", new ComboBoxEditor(client.EstateOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            //column.For(x => x.BequethTo, "Bequeth to", new MetroComboBoxEditor().DataSourceList(client.EstateOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            //column.For(c => c.BaseCost, "Base Cost", new MetroCurrencyEditor());
                            column.For(c => c.Value, "Amount", new CurrencyEditor());
                        }, ReadOnly: _readOnlyForAdminAdvisor, AllowDelete: true, AllowAddNew: true, PropertyChangedHandler: EstateDuty_propertyChanged_EventHandler
                        ).Format1(_readOnly);



                        #endregion

                        #region Calculation Analysis SUmmary

                        this.dataGrid_CalculationAnalysis.Initialise1<EstateSummaryItem>(EstateAnalysis.CalculationCGTSummary, column =>
                        {
                            column.For(x => x.Description, "Description", new StringEditor(true));
                            column.For(x => x.Asset, "Asset Value", new CurrencyEditor(true));
                            column.For(x => x.Expense, "Profit/Loss", new CurrencyEditor(true));
                            column.For(x => x.Total, "Total", new CurrencyEditor(true));

                        }, ReadOnly: true, AllowDelete: false, AllowAddNew: false
                        ).Format1(true, format: MetroControlExt.GridFormats.Default);


                        this.dataGrid_CalcExecFeesAnalysis.Initialise1<EstateSummaryItem>(EstateAnalysis.CalculationExecutorSummary, column =>
                        {
                            column.For(x => x.Description, "Description", new StringEditor(true));
                            column.For(x => x.Asset, "Asset Value", new CurrencyEditor(true));
                            column.For(x => x.Expense, "Exclusions", new CurrencyEditor(true));
                            column.For(x => x.Total, "Total", new CurrencyEditor(true));

                        }, ReadOnly: true, AllowDelete: false, AllowAddNew: false
                        ).Format1(true, format: MetroControlExt.GridFormats.Default);
                        #endregion

                        #region Liquidity Analysis

                        this.dataGrid_LiquidityAnalysis.Initialise1<EstateSummaryItem>(EstateAnalysis.LiquiditySummary, column =>
                        {
                            column.For(x => x.Description, "Description", new StringEditor(true));
                            column.For(x => x.Asset, "Accruals", new CurrencyEditor(true));
                            column.For(x => x.Expense, "Deductions", new CurrencyEditor(true));
                            column.For(x => x.Total, "Total", new CurrencyEditor(true));

                        }, ReadOnly: true, AllowDelete: false, AllowAddNew: false
                        ).Format1(true, format: MetroControlExt.GridFormats.Default);
                        #endregion

                        #region Risk Cover Analysis

                        this.dataGrid_RiskCoverAnalysis.Initialise1<EstateSummaryItem>(EstateAnalysis.RiskAnalysisSummary, column =>
                        {
                            column.For(x => x.Description, "Description", new StringEditor(true));
                            column.For(x => x.Asset, "Existing Cover", new CurrencyEditor(true));
                            column.For(x => x.Expense, "Liabilities", new CurrencyEditor(true));
                            column.For(x => x.Total, "Totals/Shortfall", new CurrencyEditor(true));

                        }, ReadOnly: true, AllowDelete: false, AllowAddNew: false
                        ).Format1(true, format: MetroControlExt.GridFormats.Default);


                        #endregion

                        #region EstateDuty Summary
                        this.dataGrid_EstateDutySummary.Initialise1<EstateSummaryItem>(EstateAnalysis.EstateDutySummary, column =>
                        {
                            column.For(x => x.Description, "Description", new StringEditor(true));
                            column.For(x => x.Asset, "Assets", new CurrencyEditor(true));
                            column.For(x => x.Expense, "Deductions", new CurrencyEditor(true));
                            column.For(x => x.Total, "Total", new CurrencyEditor(true));

                        }, ReadOnly: true, AllowDelete: false, AllowAddNew: false
                        ).Format1(true, format: MetroControlExt.GridFormats.Format1, fixedCols: 1);
                        #endregion

                        break;
                    case 7:
                        #region CheckList
                        this.metroPanel_CheckListProfile.Initialise<ClientChecklist>(client.ClientChecklist, cntr =>
                        {
                            cntr.For(x => x.StatutoryNotice, "Was a Statutory Notice issued?", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.RiskCompleted, "Was a Risk Profile completed?", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                        }, left: 5, top: 30, labelWidth: 200, PropertyChangedHandler: propertyChanged_EventHandler).Format();

                        this.metroPanel_CheckListProfile.Initialise<ClientChecklist>(client.ClientChecklist, cntr =>
                        {
                            cntr.For(x => x.RiskStatus, "Risk Status", new MetroComboBoxEditor(Width: 150).DataSourceList(Program.listData.List(ListDataItemType.RiskProfileStatus)).ReadOnly(_readOnly));
                        }, left: 5, top: 120, labelWidth: 150, PropertyChangedHandler: propertyChanged_EventHandler).Format();

                        this.metroPanel_CheckListFica.Initialise<ClientChecklist>(client.ClientChecklist, cntr =>
                        {
                            cntr.For(x => x.FicaId, "Certified Copy of Id Document on file?", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.FicaTaxNumber, "Certified Copy of Income TaxNumber?", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.FicaUtility, "Certified Copy of Utility Bill on file?", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.FicaVAT, "Certified Copy of VAT certificate on file?", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));

                        }, left: 5, top: 30, labelWidth: 250, PropertyChangedHandler: propertyChanged_EventHandler).Format();

                        this.metroPanel_CheckListWill.Initialise<ClientChecklist>(client.ClientChecklist, cntr =>
                        {
                            cntr.For(x => x.HasWill, "Client has a will and requires no changes.", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.NewWill, "Client has requested a new will.", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.HasNoWill, "Client does not have a will.", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));

                        }, left: 5, top: 30, labelWidth: 200, PropertyChangedHandler: propertyChanged_EventHandler).Format();

                        this.metroPanel_CheckListWill.Initialise<ClientChecklist>(client.ClientChecklist, cntr =>
                        {
                            cntr.For(x => x.WillExecutor, "Will Executor", new MetroTextBoxEditor(Width: 200).ReadOnly(_readOnly));

                        }, left: 5, top: 130, labelWidth: 100, PropertyChangedHandler: propertyChanged_EventHandler).Format();

                        this.metroPanel_CheckListNotes.Initialise<ClientChecklist>(client.ClientChecklist, cntr =>
                        {
                            cntr.For(x => x.NotesLA, "Living Annuity", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.NotesLife, "Life Annuity", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.NotesPF, "Retirement Preservation Fund", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.NotesRA, "Retirement Annuities", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.NotesUT, "Unit Trusts", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.NotesMed, "Medical Aid", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));


                        }, left: 5, top: 30, labelWidth: 250, PropertyChangedHandler: propertyChanged_EventHandler).Format();

                        this.metroPanel_CheckListNotesInvestments.Initialise<ClientChecklist>(client.ClientChecklist, cntr =>
                        {
                            cntr.For(x => x.FullAnalysis, "Full Analysis", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.InvUT, "Unit Trusts", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.InvOffshore, "Offshore", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.InvLocal, "Local", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.InvGold, "Gold", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));
                            cntr.For(x => x.InvETF, "ETF's", new MetroCheckBoxEditor(Width: 300).ReadOnly(_readOnly));


                        }, left: 5, top: 30, labelWidth: 250, PropertyChangedHandler: propertyChanged_EventHandler).Format();

                        this.metroPanel_CheckListClientStatus.Initialise<Client>(client, cntr =>
                        {
                            cntr.For(x => x.AgentId, "Advisor ", new MetroComboBoxEditor().DataSourceList(Program.UserServices.ListAdvisors().ToListDataItem<User>("Fullname", "Id")).ReadOnly(_readOnlyForAdminClerk).ControlValueChanged(Checklist_AdvisorChanged));
                            cntr.For(x => x.Status, "Client Status", new MetroComboBoxEditor(Width: 200).DataSourceList(Program.listData.List(ListDataItemType.ClientStatus)).ReadOnly(_readOnly));

                        }, left: 5, top: 30, labelWidth: 100, PropertyChangedHandler: propertyChanged_EventHandler, controlsLayout: ControlsLayout.Horizontal).Format();

                        this.metroPanel_CheckListClientStatus.Initialise<Client>(client, cntr =>
                        {
                            cntr.For(x => x.StatusComments, "Comments", new MetroMultiLineTextBoxEditor(320, 150).ReadOnly(_readOnly));
                        }, left: 5, top: 85, labelWidth: 150, PropertyChangedHandler: propertyChanged_EventHandler, controlsLayout: ControlsLayout.Vertical).Format();

                        #endregion

                        #region Client Advise Records

                        client.ClientPortfolio.Initialise();
                        client.ClientPortfolio.Calculate();

                        //Retirement Portfolio CAR data grid
                        this.dataGrid_CARRetirement.Initialise1<Retirement>(client.ClientPortfolio.RetirementsBindingList, column =>
                        {
                            column.For(x => x.Type, "Product Type", new ComboListEditor(ListDataItemType.RetirementAssetClass), MinWidth: 150);
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps), MinWidth: 150);
                            column.For(c => c.CurrentAdviceRecord.IsCompleted, "Completed");
                            column.For(c => c.CurrentAdviceRecord.AdviceDate, "Note Date", new DateEditor());
                            column.For(c => c.ReferenceNo, "Policy No.", new StringEditor());                            
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.Status, "Policy Status", new StringEditor());                            
                            column.For(c => c.CurrentAdviceRecord.UpdateDate, "Last Date", new DateEditor());
                            column.For(c => c.CurrentAdviceRecord.UpdateBy, "Updated By", new StringEditor());
                            
                        },
                        //RowHeaderSelectEventHandler: RetirementPortfolio_RowHeaderSelectEventHandler,//for the Policy Funds details view
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.RetirementPortfolio, _readOnly, client, OnCompleted: ClientPortfolio_PolicyChanged_EventHandler),
                        ReadOnly: true,
                        AllowDelete: false,
                        AllowAddNew: false)
                        .Format1(true, fixedCols: 3);

                        //Non Retirement Portfolio CAR data grid
                        this.dataGrid_CARNonRetirement.Initialise1<Investment>(client.ClientPortfolio.InvestmentsBindingList, column =>
                        {
                            column.For(x => x.Type, "Product Type", new ComboBoxEditor(ListDataItemType.InvestmentClass), MinWidth: 150);
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps));
                            column.For(c => c.CurrentAdviceRecord.IsCompleted, "Completed");
                            column.For(c => c.CurrentAdviceRecord.AdviceDate, "Note Date", new DateEditor());
                            column.For(c => c.ReferenceNo, "Policy No.", new StringEditor());
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.Status, "Policy Status", new StringEditor());
                            column.For(c => c.UpdateDate, "Last Date", new DateEditor());
                            column.For(c => c.UpdateBy, "Updated By", new StringEditor());
                            
                        },
                        //PropertyChangedHandler: ClientPortfolio_propertyChanged_EventHandler, //event handler when a property changes
                        //RowHeaderSelectEventHandler: RetirementPortfolio_RowHeaderSelectEventHandler,//for the Policy Funds details view
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.InvestmentPortfolio, _readOnly, client, OnCompleted: ClientPortfolio_PolicyChanged_EventHandler),
                        ReadOnly: true,
                        AllowDelete: false,
                        AllowAddNew: false)
                        .Format1(true, fixedCols: 3);


                        //Education Portfolio CAR data grid
                        this.dataGrid_CAREducation.Initialise1<Education>(client.ClientPortfolio.EducationsBindingList, column =>
                        {
                            column.For(x => x.Type, "Product Type", new ComboListEditor(ListDataItemType.RetirementAssetClass), MinWidth: 150);
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps), MinWidth: 150);
                            column.For(c => c.CurrentAdviceRecord.IsCompleted, "Completed");
                            column.For(c => c.CurrentAdviceRecord.AdviceDate, "Note Date", new DateEditor());
                            column.For(c => c.ReferenceNo, "Policy No.", new StringEditor());
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.Status, "Policy Status", new StringEditor());
                            column.For(c => c.CurrentAdviceRecord.UpdateDate, "Last Date", new DateEditor());
                            column.For(c => c.CurrentAdviceRecord.UpdateBy, "Updated By", new StringEditor());
                        },
                       // PropertyChangedHandler: ClientPortfolio_propertyChanged_EventHandler, //event handler when a property changes
                        //RowHeaderSelectEventHandler: RetirementPortfolio_RowHeaderSelectEventHandler,//for the Policy Funds details view
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.EducationPortfolio, _readOnly, client, OnCompleted: ClientPortfolio_PolicyChanged_EventHandler),
                        ReadOnly: true,
                        AllowDelete: false,
                        AllowAddNew: false)
                        .Format1(true, fixedCols: 3);


                        //Medical Aid Portfolio CAR data grid
                        this.dataGrid_CARMedicalAid.Initialise1<Medical>(client.ClientPortfolio.MedicalsBindingList, column =>
                        {
                            column.For(x => x.Type, "Product Type", new ComboListEditor(ListDataItemType.RetirementAssetClass), MinWidth: 150);
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps), MinWidth: 150);
                            column.For(c => c.CurrentAdviceRecord.IsCompleted, "Completed");
                            column.For(c => c.CurrentAdviceRecord.AdviceDate, "Note Date", new DateEditor());
                            column.For(c => c.ReferenceNo, "Policy No.", new StringEditor());
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.Status, "Policy Status", new StringEditor());
                            column.For(c => c.CurrentAdviceRecord.UpdateDate, "Last Date", new DateEditor());
                            column.For(c => c.CurrentAdviceRecord.UpdateBy, "Updated By", new StringEditor());
                        },
                       // PropertyChangedHandler: ClientPortfolio_propertyChanged_EventHandler, //event handler when a property changes
                        //RowHeaderSelectEventHandler: RetirementPortfolio_RowHeaderSelectEventHandler,//for the Policy Funds details view
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.MedicalPortfolio, _readOnly, client, OnCompleted: ClientPortfolio_PolicyChanged_EventHandler),
                        ReadOnly: true,
                        AllowDelete: false,
                        AllowAddNew: false)
                        .Format1(true, fixedCols: 3);

                        

                        //Risk Portfolio CAR data grid
                        this.dataGrid_CARRisk.Initialise1<Life>(client.ClientPortfolio.LifesBindingList, column =>
                        {
                            column.For(x => x.Type, "Product Type", new ComboListEditor(ListDataItemType.RetirementAssetClass), MinWidth: 150);
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps), MinWidth: 150);
                            column.For(c => c.CurrentAdviceRecord.IsCompleted, "Completed");
                            column.For(c => c.CurrentAdviceRecord.AdviceDate, "Note Date", new DateEditor());
                            column.For(c => c.ReferenceNo, "Policy No.", new StringEditor());
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.Status, "Policy Status", new StringEditor());
                            column.For(c => c.CurrentAdviceRecord.UpdateDate, "Last Date", new DateEditor());
                            column.For(c => c.CurrentAdviceRecord.UpdateBy, "Updated By", new StringEditor());
                        },
                        //PropertyChangedHandler: ClientPortfolio_propertyChanged_EventHandler, //event handler when a property changes
                        //RowHeaderSelectEventHandler: RetirementPortfolio_RowHeaderSelectEventHandler,//for the Policy Funds details view
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.LifePortfolio, _readOnly, client, OnCompleted: ClientPortfolio_PolicyChanged_EventHandler),
                        ReadOnly: true,
                        AllowDelete: false, 
                        AllowAddNew: false)
                        .Format1(true, fixedCols: 3);



                        //Income Assets Portfolio CAR data grid
                        this.dataGrid_CARIncomeAsset.Initialise1<IncomeAsset>(client.ClientPortfolio.IncomeAssetsBindingList, column =>
                        {
                            column.For(x => x.Type, "Product Type", new ComboListEditor(ListDataItemType.RetirementAssetClass), MinWidth: 150);
                            column.For(x => x.Description, "LISP", new ComboListEditor(Lisps), MinWidth: 150);
                            column.For(c => c.CurrentAdviceRecord.IsCompleted, "Completed");
                            column.For(c => c.CurrentAdviceRecord.AdviceDate, "Note Date", new DateEditor());
                            column.For(c => c.ReferenceNo, "Policy No.", new StringEditor());
                            column.For(x => x.Insured, "Policy Owner", new ComboListEditor(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")));
                            column.For(c => c.Status, "Policy Status", new StringEditor());
                            column.For(c => c.CurrentAdviceRecord.UpdateDate, "Last Date", new DateEditor());
                            column.For(c => c.CurrentAdviceRecord.UpdateBy, "Updated By", new StringEditor());

                        },
                        //PropertyChangedHandler: ClientPortfolio_propertyChanged_EventHandler,
                        //RowHeaderSelectEventHandler: AssetsPortfolio_RowHeaderSelectEventHandler, //no funds
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.AssetPortfolio, _readOnly, client),
                        ReadOnly: true,
                        AllowDelete: false)
                        .Format1(true, fixedCols: 3);

                        #endregion

                        break;
                    case 8:
                        #region Admin Tasks
                        this.dataGrid_ClientAdminTasks.Initialise1<Instruction>(instructions, column =>
                        {
                            column.For(x => x.Type, "Task Type", new StringEditor(true));
                            column.For(c => c.TaskName, "Task Name", new StringEditor(true));
                            column.For(c => c.ReferenceNo, "Reference", new StringEditor(true));
                            //column.For(x => x.ReferenceOwner, "Policy Owner", new MetroComboBoxEditor().DataSourceList(client.PolicyOwners.ToListDataItem<ClientDependent>("DependentName", "DependentName")).ReadOnly(ReadOnly));
                            column.For(c => c.ReferenceOwner, "Policy Owner", new StringEditor(true));
                            column.For(x => x.AllocatedTo, "Allocate To", new ComboListEditor(Program.UserServices.ListActiveUsers().ToListDataItem<User>("Firstname", "Firstname"), _readOnly));
                            column.For(x => x.Status, "Status", new ComboListEditor(InstructionStatusExt.ToListDataItem(), true));
                            column.For(c => c.CreateDate, "Created", new DateEditor(true));
                            column.For(c => c.UpdateDate, "Updated", new DateEditor(true));
                            column.For(c => c.UpdateBy, "By", new StringEditor(true));
                        },
                            ListChangedEventHandler: AdminTask_propertyChanged_EventHandler,
                            // PropertyChangedHandler: AdminTask_propertyChanged_EventHandler,
                            ItemDeletedEventHandler: AdminTaskDeleted_EventHandler,
                            //RowHeaderSelectEventHandler: ClientAdminTasks_RowHeaderSelectEventHandler,
                            ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, _readOnly, client),
                            ReadOnly: _readOnly,
                            AllowDelete: false,//Program.User.IsAdministrator,
                            AllowAddNew: false
                            )
                            .Format1(_readOnly);

                        //Format Status Cells
                        this.dataGrid_ClientAdminTasks.Columns[6].DataCell.View = new InstructionsStatusView();

                        #endregion
                        ShowAdminTasks1(this.metroCheckBox1.Checked);
                        break;
                    case 9:
                        #region Meetings

                        this.dataGrid_ClientMeetings.Initialise1<ClientMeetings>(clientMeetings, column =>
                        {
                            column.For(x => x.MeetingType, "Meeting", new ComboBoxEditor(ListDataItemType.MeetingType));
                            column.For(x => x.ScheduledDate, "Scheduled Date", new DateEditor());
                            column.For(c => c.TimeFrom, "Time From", new TimeEditor());
                            column.For(c => c.TimeTo, "Time To", new TimeEditor());
                            column.For(c => c.Venue, "Venue", new StringEditor());
                            column.For(c => c.MeetingStatus, "Status", new ComboListEditor(ListDataItemType.MeetingStatus));
                            column.For(c => c.Sync, "Synced ?", Editable: false, Tooltip: "Sync this meeting to your Outlook Calendar");
                        },
                        ListChangedEventHandler: Meetings_propertyChanged_EventHandler,
                        ItemDeletedEventHandler: Meetings_itemDeleted_EventHandler,
                        ContextMenu: new DataGridContextMenu(this, ContextMenuType.MeetingSync, _readOnly, client, !_readOnly, OnCompleted: MeetingSync_Completed),
                        ReadOnly: _readOnly,
                        AllowDelete: true)
                        .Format1(_readOnly);

                        #endregion
                        ShowReviewMeetings();
                        break;
                }
            }
            catch (Exception x)
            {

            }
            finally { _isInitialising = false; }
        }

        private void Checklist_AdvisorChanged(object sender, EventArgs e)
        {
            //MetroComboBoxEditor cntrl = sender as MetroComboBoxEditor;

            client.AgentDetails = Program.UserServices.GetAdvisor(client.AgentId);

            propertyChanged_EventHandler(sender, e);
        }
        #endregion;

        #region Review Meetings
        private void ShowReviewMeetings(bool ShowCompleted = false)
        {
            clientMeetings = Program.Repository.List<ClientMeetings, int>(x => x.ClientId == client.Id).OrderBy(x => x.ScheduledDate).ToList();

            foreach (ClientMeetings meeting in clientMeetings)
                meeting.Calculate();

            if (!ShowCompleted)
                clientMeetings = clientMeetings.Where<ClientMeetings>(x => x.MeetingStatus == MeetingStatusEnum.Scheduled.ToString()
                                                        || x.MeetingStatus == MeetingStatusEnum.Postponed.ToString()
                                                        || x.MeetingStatus == MeetingStatusEnum.Accepted.ToString()
                                                        ).ToList();

            this.dataGrid_ClientMeetings.Rebind(clientMeetings, Meetings_propertyChanged_EventHandler, Meetings_itemDeleted_EventHandler);
        }

        /// <summary>
        /// Show Review Meetings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            MetroCheckBox chk = sender as MetroCheckBox;
            ShowReviewMeetings(chk.Checked);
        }
        private void MeetingSync_Completed(object sender, EventArgs e)
        {
            ClientMeetings meeting = sender as ClientMeetings;
            Program.Repository.Update<ClientMeetings, int>(meeting);

            ShowReviewMeetings(false);
        }
        #endregion

        #region Admin Tasks

        private void ShowAdminTasks1(bool ShowCompleted = false)
        {
            instructions = Program.Repository.List<Instruction, int>(x => x.ClientId == client.ClientDetails.ClientId && x.ClientId != 0).ToList();

            if (!ShowCompleted)
                instructions = instructions.Where(x => x.Status != InstructionStatus.Completed.ToString()
                                                        && x.Status != InstructionStatus.AddCompleted.ToString()
                                                        && x.Status != InstructionStatus.UpdateCompleted.ToString()
                                                        && x.Status != InstructionStatus.Cancelled.ToString()
                                                        && x.Status != InstructionStatus.AddCancelled.ToString()
                                                        && x.Status != InstructionStatus.UpdateCancelled.ToString()).ToList();


            this.dataGrid_ClientAdminTasks.Rebind(instructions, AdminTask_propertyChanged_EventHandler);
        }
        /// <summary>
        /// Show Admin Tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            MetroCheckBox chk = sender as MetroCheckBox;
            ShowAdminTasks1(chk.Checked);
        }

        //private void ClientAdminTasks_RowHeaderSelectEventHandler(object sender, EventArgs e)
        //{
        //    CellContext context = (CellContext)sender;



        //    try
        //    {
        //        SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
        //        BoundList<Instruction> bList = dGrid.DataSource as BoundList<Instruction>;

        //        Instruction instruction = bList[context.Position.Row - 1] as Instruction;

        //        frmMetroAdminTaskAdd frm = new frmMetroAdminTaskAdd(instruction);
        //        frm.ShowDialog(this);


        //    }
        //    catch (Exception x)
        //    {

        //    }
        //}
        #endregion

        #region Refresh Methods
        private void RefreshStatusStrip()
        {

            if (client.ClientDetails.Id == 0)
                this.toolStripButton_Person.Text = string.Format("New Client", client.ClientDetails.LastName, client.ClientDetails.Initials, client.ClientDetails.ClientTitle);
            else
                this.toolStripButton_Person.Text = string.Format("{0},{1} {2}", client.ClientDetails.LastName, client.ClientDetails.Initials, client.ClientDetails.ClientTitle);

            this.Text = string.Format("Client No: {0}", client.Id, client.Status);

            this.toolStripStatusLabel_ClientStatus.Text = client.Status;
            this.toolStripStatusLabel_LastUpdated.Text = string.Format("{0} by {1}", client.UpdateDate, client.UpdateBy);

            this.toolStripStatusLabel_LockedBy.Text = ClientLockStatus.IsLocked ? ClientLockStatus.FirstName : "none";

            this.toolStripButton_Update.Enabled = _hasChanges;
            this.toolStripButton_Edit.Enabled = Program.User.IsGuest ? false : _readOnly;
            this.toolStripButton_Refresh.Enabled = true;// !_readOnly;
                                                        //this.button_CopyAddress.Enabled = !ReadOnly;

            this.toolStripButton_Delete.Enabled = !_readOnly;
            this.toolStripButton_Delete.Visible = Program.User.IsAdministrator && Program.User.IsActive;

            this.toolStripButton_Export.Enabled = !_readOnly;
            this.toolStripButton_Export.Visible = Program.User.IsAdministrator && Program.User.IsActive;

            this.unlockToolStripMenuItem.Enabled = Program.User.IsAdministrator && Program.User.IsActive;

            if (client.Id == 0)
                this.toolStripButton_Refresh.Enabled = false;
        }

        private void RefreshAssetLiabilitiesSummary()
        {
            #region Assets Liabilities Summary

            IList<PortfolioSummary> _ALSummaryList = new List<PortfolioSummary>();
            _ALSummaryList.Add(new PortfolioSummary()
            {
                Description = "Totals",
                TotalIncomeAsset = client.ClientAssets.Total,
                TotalInv = client.ClientPortfolio.TotalAssets,
                TotalLife = client.ClientLiabilities.Total,
                TotalMedical = client.ClientAssets.Total + client.ClientPortfolio.TotalAssets - client.ClientLiabilities.Total,
            });
            this.metroGrid_AssetLiabilitiesSummary.Initialise<PortfolioSummary>(_ALSummaryList, column =>
            {
                column.For(x => x.Description, "Assets and Liabilities Summary:", new MetroTextBoxEditor().ReadOnly(true));
                column.For(x => x.TotalIncomeAsset, "Non-Investment Assets", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalInv, " + Investment Assets", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalLife, " - Liabilities", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalMedical, " = Nett Asset Value (NAV)", new MetroCurrencyEditor().ReadOnly(true));


            }).Format(true, MetroControlExt.GridFormats.Format1);
            #endregion
        }
        private void RefreshIncomeExpensesSummary()
        {
            #region Income Expenses Summary

            IList<PortfolioSummary> _IESummaryList = new List<PortfolioSummary>();
            _IESummaryList.Add(new PortfolioSummary()
            {
                Description = "Totals",
                TotalIncomeAsset = client.ClientIncomes.Total,
                TotalInv = client.ClientExpenses.Total,
                TotalLife = client.ClientPortfolio.TotalPremium,
                TotalMedical = client.ClientIncomes.Total - client.ClientExpenses.Total - client.ClientPortfolio.TotalPremium,
            });
            this.metroGrid_IncomeExpenseSummary.Initialise<PortfolioSummary>(_IESummaryList, column =>
            {
                column.For(x => x.Description, "Income and Expenses Summary:", new MetroTextBoxEditor().ReadOnly(true));
                column.For(x => x.TotalIncomeAsset, "Income", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalInv, " - Non-Investment Expenses", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalLife, " - Investment Premiums", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalMedical, " = Nett Cash Flow (NCF)", new MetroCurrencyEditor().ReadOnly(true));


            }).Format(true, MetroControlExt.GridFormats.Format1);
            #endregion
        }
        private void RefreshPortfolioSummary()
        {
            //Calculate Portfolio Summary
            #region Portfolio Summary
            IList<PortfolioSummary> _summaryList = new List<PortfolioSummary>();
            _summaryList.Add(new PortfolioSummary()
            {
                Description = "Total Current Value",
                TotalIncomeAsset = client.ClientPortfolio._TotalIncomeAsset,
                TotalInv = client.ClientPortfolio._TotalInv,
                TotalLife = client.ClientPortfolio._TotalLife,
                TotalMedical = client.ClientPortfolio._TotalMedical,
                TotalRetirement = client.ClientPortfolio._TotalRetirement,
                TotalEducation = client.ClientPortfolio._TotalEdu
            });
            _summaryList.Add(new PortfolioSummary()
            {
                Description = "Total Future Value",
                TotalIncomeAsset = client.ClientPortfolio._TotalIncomeAssetFuture,
                TotalInv = client.ClientPortfolio._TotalInvFuture,
                TotalLife = client.ClientPortfolio._TotalLifeFuture,
                TotalMedical = 0f,
                TotalRetirement = client.ClientPortfolio._TotalRetirementFuture,
                TotalEducation = client.ClientPortfolio._TotalEduFuture
            });
            _summaryList.Add(new PortfolioSummary()
            {
                Description = "Total Premium (p/m)",
                TotalIncomeAsset = client.ClientPortfolio._TotalIncomeAssetPremium,
                TotalInv = client.ClientPortfolio._TotalInvPremium,
                TotalLife = client.ClientPortfolio._TotalLifePremium,
                TotalMedical = client.ClientPortfolio._TotalMedicalPremium,
                TotalRetirement = client.ClientPortfolio._TotalRetirementPremium,
                TotalEducation = client.ClientPortfolio._TotalEduPremium
            });

            this.metroGrid_PortfolioSummary.Initialise<PortfolioSummary>(_summaryList, column =>
            {
                column.For(x => x.Description, "Portfolio Summary :", new MetroTextBoxEditor().ReadOnly(true));
                column.For(x => x.TotalRetirement, "Retirement", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalInv, "Non-Retirement", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalEducation, "Education", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalMedical, "Medical Cover", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalLife, "Risk Cover", new MetroCurrencyEditor().ReadOnly(true));
                column.For(x => x.TotalIncomeAsset, "Income Assets", new MetroCurrencyEditor().ReadOnly(true));
            }).Format(true, MetroControlExt.GridFormats.Format1);
            #endregion
        }
        private void RefreshClientFnaSummary()
        {

            //recalculate Fna graphs
            chart_Fna.Series.Clear();
            chart_Fna.ChartAreas.Clear();

            ChartArea chartArea1 = new ChartArea("FnaArea");

            chartArea1.Position.Auto = false;
            chartArea1.Position.X = 5;
            chartArea1.Position.Y = 5;
            chartArea1.Position.Width = 90;
            chartArea1.Position.Height = 100;

            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.X = 0;
            chartArea1.InnerPlotPosition.Y = 0;
            chartArea1.InnerPlotPosition.Width = 100;
            chartArea1.InnerPlotPosition.Height = 90;

            chartArea1.AxisX.Title = "Investment Years";
            //  chartArea1.AxisY.Title = "Amount";
            chartArea1.AxisX.IsLabelAutoFit = true;
            chartArea1.AxisX.MajorGrid.LineColor = Color.White;
            chartArea1.AxisX.MinorGrid.LineColor = Color.White;

            chartArea1.AxisY.MajorGrid.LineColor = Color.White;
            chartArea1.AxisY.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY.LabelStyle.Format = "c";
            chartArea1.AxisY.Enabled = AxisEnabled.True;

            //Y Axis on right hand side
            chartArea1.AxisY2.MajorGrid.LineColor = Color.White;
            chartArea1.AxisY2.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY2.LabelStyle.Format = "c";
            chartArea1.AxisY2.Enabled = AxisEnabled.True;


            chart_Fna.ChartAreas.Add(chartArea1);

            chart_Fna.Legends[0].Docking = Docking.Top;
            chart_Fna.Legends[0].IsDockedInsideChartArea = true;
            chart_Fna.Legends[0].DockedToChartArea = "FnaArea";



            if (this.metroCheckBox_Wants.Checked)
            {
                Series seriesWants = client.ClientFna.GetWantsSeries();

                seriesWants.ChartType = SeriesChartType.Column;
                seriesWants.ChartArea = "FnaArea";
                seriesWants.Color = Color.Red;


                chart_Fna.Series.Add(seriesWants);
            }

            if (this.metroCheckBox_Haves.Checked)
            {
                Series seriesHaves = client.ClientFna.GetHavesSeries();

                seriesHaves.ChartType = SeriesChartType.StackedColumn;
                seriesHaves.ChartArea = "FnaArea";
                seriesHaves.Color = Color.Green;


                chart_Fna.Series.Add(seriesHaves);
            }


            //if (chkBox_HavesAdj.Checked)
            //{
            //    Series seriesHaves = model.ClientFna.GetHavesSeriesInflationAdj();

            //    seriesHaves.ChartType = SeriesChartType.Column;
            //    seriesHaves.ChartArea = "FnaArea";
            //    seriesHaves.Color = Color.Yellow;

            //    chart_Fna.Series.Add(seriesHaves);
            //}

            if (this.metroCheckBox_Needs.Checked)
            {
                Series seriesNeeds = client.ClientFna.GetNeedsSeries();

                seriesNeeds.ChartType = SeriesChartType.StackedColumn;
                seriesNeeds.ChartArea = "FnaArea";
                seriesNeeds.Color = Color.Yellow;

                chart_Fna.Series.Add(seriesNeeds);

            }

            client.ClientFna.ShowNeeds = this.metroCheckBox_Needs.Checked;



            this.dataGrid_RetireFNA_Wants.Refresh();
            this.dataGrid_RetireFNA_Have.Refresh();
            this.dataGrid_RetireFNA_Need.Refresh();

            //Refresh the Fna Grids
            //dataGrid_Haves.Refresh();
            //dataGrid_Wants.Refresh();
            //dataGrid_Needs.Refresh();

            ////dataGrid_EstatePortfolio.Refresh();

            //if (model.ClientFna.Shortfall <= 0)
            //{
            //    xInput_ShortfallTotal.textBox.BackColor = Color.Yellow;
            //    xInput_ShortfallTotal.textBox.ForeColor = Color.Red;
            //    xInput_RequiredPremium.textBox.BackColor = Color.Yellow;
            //    xInput_RequiredPremium.textBox.ForeColor = Color.Red;
            //}
            //else
            //{
            //    xInput_ShortfallTotal.textBox.BackColor = Color.GhostWhite;
            //    xInput_ShortfallTotal.textBox.ForeColor = Color.Black;
            //    xInput_RequiredPremium.textBox.BackColor = Color.GhostWhite;
            //    xInput_RequiredPremium.textBox.ForeColor = Color.Black;
            //}

            //if (model.ClientFna.NeedsTotal <= 0)
            //{
            //    xInput_NeedsTotal.textBox.BackColor = Color.Yellow;
            //    xInput_NeedsTotal.textBox.ForeColor = Color.Red;

            //}
            //else
            //{
            //    xInput_NeedsTotal.textBox.BackColor = Color.GhostWhite;
            //    xInput_NeedsTotal.textBox.ForeColor = Color.Black;

            //}
            //if (model.CashFlow <= 0)
            //{
            //    xInput_CashFlow.textBox.BackColor = Color.Yellow;
            //    xInput_CashFlow.textBox.ForeColor = Color.Red;

            //}
            //else
            //{
            //    xInput_CashFlow.textBox.BackColor = Color.GhostWhite;
            //    xInput_CashFlow.textBox.ForeColor = Color.Black;

            //}

            #region ReportViewer

            //ReportViewer_RetirementFNA_ReportRefresh(this.reportViewer_RetirementFNA, new CancelEventArgs());

            ////Refresh the Fna Grids
            //dataGrid_Haves.Refresh();
            //dataGrid_Wants.Refresh();
            //dataGrid_Needs.Refresh();



            #endregion

        }

        private void RefreshInvestmentFnaSummary()
        {
            chart_NonRetirementFna.Series.Clear();
            chart_NonRetirementFna.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("FnaArea");
            chartArea1.Position.Auto = false;
            chartArea1.Position.X = 5f;
            chartArea1.Position.Y = 5f;
            chartArea1.Position.Width = 90f;
            chartArea1.Position.Height = 100f;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.X = 0f;
            chartArea1.InnerPlotPosition.Y = 0f;
            chartArea1.InnerPlotPosition.Width = 100f;
            chartArea1.InnerPlotPosition.Height = 90f;
            chartArea1.AxisX.Title = "Investment Years";
            chartArea1.AxisX.IsLabelAutoFit = true;
            chartArea1.AxisX.MajorGrid.LineColor = Color.White;
            chartArea1.AxisX.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY.MajorGrid.LineColor = Color.White;
            chartArea1.AxisY.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY.LabelStyle.Format = "c";
            chartArea1.AxisY2.MajorGrid.LineColor = Color.White;
            chartArea1.AxisY2.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY2.LabelStyle.Format = "c";
            chartArea1.AxisY2.Enabled = AxisEnabled.True;
            chart_NonRetirementFna.ChartAreas.Add(chartArea1);
            chart_NonRetirementFna.Legends[0].Docking = Docking.Top;
            chart_NonRetirementFna.Legends[0].IsDockedInsideChartArea = true;
            chart_NonRetirementFna.Legends[0].DockedToChartArea = "FnaArea";
            //Series seriesWants = client.ClientFnaInvestment.GetNeedsSeries();
            //seriesWants.ChartType = SeriesChartType.Column;
            //seriesWants.ChartArea = "FnaArea";
            //seriesWants.Color = Color.Blue;
            //chart_NonRetirementFna.Series.Add(seriesWants);
            int iColor = 0;
            foreach (InvestmentNeed need in client.ClientFnaInvestment.Needs)
            {
                try
                {
                    Series seriesHaves = need.GetNeedsSeries();
                    seriesHaves.ChartType = SeriesChartType.Column;
                    seriesHaves.ChartArea = "FnaArea";
                    seriesHaves.Color = Global.GetChartSeriesColor(iColor);
                    chart_NonRetirementFna.Series.Add(seriesHaves);
                    iColor++;
                }
                catch (Exception) { }
            }
        }
        private void RefreshEducationFnaSummary()
        {
            chart_NonRetirementFna.Series.Clear();
            chart_NonRetirementFna.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("FnaArea");
            chartArea1.Position.Auto = false;
            chartArea1.Position.X = 5f;
            chartArea1.Position.Y = 5f;
            chartArea1.Position.Width = 90f;
            chartArea1.Position.Height = 100f;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.X = 0f;
            chartArea1.InnerPlotPosition.Y = 0f;
            chartArea1.InnerPlotPosition.Width = 100f;
            chartArea1.InnerPlotPosition.Height = 90f;
            chartArea1.AxisX.Title = "Investment Years";
            chartArea1.AxisX.IsLabelAutoFit = true;
            chartArea1.AxisX.MajorGrid.LineColor = Color.White;
            chartArea1.AxisX.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY.MajorGrid.LineColor = Color.White;
            chartArea1.AxisY.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY.LabelStyle.Format = "c";
            chartArea1.AxisY2.MajorGrid.LineColor = Color.White;
            chartArea1.AxisY2.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY2.LabelStyle.Format = "c";
            chartArea1.AxisY2.Enabled = AxisEnabled.True;
            chart_NonRetirementFna.ChartAreas.Add(chartArea1);
            chart_NonRetirementFna.Legends[0].Docking = Docking.Top;
            chart_NonRetirementFna.Legends[0].IsDockedInsideChartArea = true;
            chart_NonRetirementFna.Legends[0].DockedToChartArea = "FnaArea";
            int iColor = 0;
            foreach (EducationNeed eduNeed in client.ClientFnaEducation.EducationNeeds)
            {
                try
                {
                    Series seriesHaves = eduNeed.GetNeedsSeries();
                    seriesHaves.ChartType = SeriesChartType.Column;
                    seriesHaves.ChartArea = "FnaArea";
                    seriesHaves.Color = Global.GetChartSeriesColor(iColor);
                    chart_NonRetirementFna.Series.Add(seriesHaves);
                    iColor++;
                }
                catch (Exception) { }
            }
        }
        private void RefreshRiskFnaSummary()
        {
            chart_NonRetirementFna.Series.Clear();
            chart_NonRetirementFna.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("FnaArea");
            chartArea1.Position.Auto = false;
            chartArea1.Position.X = 5f;
            chartArea1.Position.Y = 5f;
            chartArea1.Position.Width = 90f;
            chartArea1.Position.Height = 100f;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.X = 0f;
            chartArea1.InnerPlotPosition.Y = 0f;
            chartArea1.InnerPlotPosition.Width = 100f;
            chartArea1.InnerPlotPosition.Height = 90f;
            chartArea1.AxisX.Title = "Investment Years";
            chartArea1.AxisX.IsLabelAutoFit = true;
            chartArea1.AxisX.MajorGrid.LineColor = Color.White;
            chartArea1.AxisX.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY.MajorGrid.LineColor = Color.White;
            chartArea1.AxisY.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY.LabelStyle.Format = "c";
            chartArea1.AxisY2.MajorGrid.LineColor = Color.White;
            chartArea1.AxisY2.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY2.LabelStyle.Format = "c";
            chartArea1.AxisY2.Enabled = AxisEnabled.True;
            chart_NonRetirementFna.ChartAreas.Add(chartArea1);
            chart_NonRetirementFna.Legends[0].Docking = Docking.Top;
            chart_NonRetirementFna.Legends[0].IsDockedInsideChartArea = true;
            chart_NonRetirementFna.Legends[0].DockedToChartArea = "FnaArea";
            int iColor = 0;
            foreach (RiskCoverNeed need in ClientFnaRisk.RiskCoverNeeds)
            {
                try
                {
                    Series seriesHaves = need.GetNeedsSeries();
                    seriesHaves.ChartType = SeriesChartType.Line;
                    seriesHaves.ChartArea = "FnaArea";
                    seriesHaves.Color = Global.GetChartSeriesColor(iColor);
                    chart_NonRetirementFna.Series.Add(seriesHaves);
                    iColor++;
                }
                catch (Exception) { }
            }
        }

        private void RefreshGrids()
        {
            this.dataGrid_RetirementPortfolio.Refresh();
            this.dataGrid_InvestmentPortfolio.Refresh();
            this.dataGrid_EducationPortfolio.Refresh();
            this.dataGrid_LifePortfolio.Refresh();
            this.dataGrid_MedicalPortfolio.Refresh();

            this.dataGrid_RetireFNA_Have.Refresh();

            this.dataGrid_InvestmentFna.Refresh();
            this.dataGrid_EducationFna.Refresh();
            this.dataGrid_LifeRiskFna.Refresh();
        }

       

        #endregion


        #region DataGrid propertyChanged EventHandlers
        private void propertyChanged_EventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (!_isInitialising && !_readOnly)
            {
                _hasChanges = true;
                RefreshStatusStrip();
            }
        }

        private void propertyChanged_EventHandler(object sender, EventArgs e)
        {
            if (!_isInitialising && !_readOnly)
            {
                _hasChanges = true;
                RefreshStatusStrip();
            }
        }

        private void AssetsLiabilities_propertyChanged_EventHandler(object sender, EventArgs e)
        {
            if (!_isInitialising)
            {
                propertyChanged_EventHandler(sender, e);

                RefreshAssetLiabilitiesSummary();
            }
        }

        private void IncomeExpenses_propertyChanged_EventHandler(object sender, EventArgs e)
        {
            if (!_isInitialising)
            {
                propertyChanged_EventHandler(sender, e);
                RefreshIncomeExpensesSummary();
            }
        }

        private void ClientPortfolio_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {

            if (!_isInitialising)
            {
                propertyChanged_EventHandler(sender, e);
                RefreshPortfolioSummary();
                RefreshGrids();
            }

        }
        private void ClientPortfolio_PolicyChanged_EventHandler(object sender, EventArgs e)
        {
           // return;

            if (!_isInitialising)
            {
                Retirement retirement = sender as Retirement;
                if (retirement != null)
                {
                    //client.ClientPortfolio.Initialise();

                    this.dataGrid_RetirementPortfolio.Rebind<Retirement>(client.ClientPortfolio.RetirementsBindingList);
                    
                }

                Investment investment = sender as Investment;
                if (investment != null)
                {
                    //client.ClientPortfolio.Initialise();

                    this.dataGrid_InvestmentPortfolio.Rebind<Investment>(client.ClientPortfolio.InvestmentsBindingList);
                }

                Education education = sender as Education;
                if (education != null)
                {
                    //client.ClientPortfolio.Initialise();

                    this.dataGrid_EducationPortfolio.Rebind<Education>(client.ClientPortfolio.EducationsBindingList);
                }

                Medical medical = sender as Medical;
                if (medical != null)
                {
                   // client.ClientPortfolio.Initialise();

                    this.dataGrid_MedicalPortfolio.Rebind<Medical>(client.ClientPortfolio.MedicalsBindingList);
                }

                Life life = sender as Life;
                if (life != null)
                {
                    //client.ClientPortfolio.Initialise();

                    this.dataGrid_LifePortfolio.Rebind<Life>(client.ClientPortfolio.LifesBindingList);
                }

                IncomeAsset incomeAsset = sender as IncomeAsset;
                if (incomeAsset != null)
                {
                    //client.ClientPortfolio.Initialise();

                    this.dataGrid_AssetsPortfolio.Rebind<IncomeAsset>(client.ClientPortfolio.IncomeAssetsBindingList);
                }

                //TO DO: Rebind the other grids

                RefreshPortfolioSummary();
                RefreshGrids();

                propertyChanged_EventHandler(sender, e);
            }

        }

        private void RetirementFna_propertyChanged_EventHandler(object sender, EventArgs e)
        {
            if (!_isInitialising)
            {
                propertyChanged_EventHandler(sender, e);

                RefreshClientFnaSummary();

            }
        }

        private void RetirementFna_needCompleted_EventHandler(object sender, EventArgs e)
        {
            this.dataGrid_RetireFNA_Need.Rebind<Need>(client.ClientFna.NeedsBindingList);
        }
        private void FnaNeed_ItemDeleting_EventHandler(object sender, EventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }
            _hasChanges = true;
            CellContext context = (CellContext)sender;
            try
            {
                SourceGrid.DataGrid dg = (SourceGrid.DataGrid)context.Grid;

                if (context.Position.Row > 0)
                {
                    if (context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow || dg.DataSource.AllowNew == false)
                    {
                        context.Grid.Selection.FocusRow(context.Position.Row);

                        Need obj = dg.DataSource.EditedObject as Need;

                        if (obj != null)
                        {
                            if (obj.InstructionId != 0)
                                throw new Exception("This item may not be deleted as it is linked to a Task. Please remove the task if you wish to delete this item.");
                        }

                        if (MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this row?"))
                        {

                            int dataIndex = dg.Rows.IndexToDataSourceIndex(context.Position.Row);

                            if (dataIndex < dg.DataSource.Count)
                            {
                                dg.DataSource.RemoveAt(dataIndex);
                            }
                        }
                    }
                }
            }
            catch (Exception x1)
            {
                MessageBoxExt.ShowWarning(x1.Message);
            }
            try
            {


                //    Need need = e.Item as Need;

                //    if (need != null)
                //    {
                //        //check for linked instruction
                //        var instruction = client.ClientInstructions.Instructions.Where(x => x.ReferenceId == need.Id).FirstOrDefault();

                //        if (instruction != null)
                //            throw new Exception("This item is linked to a Task and cannot be deleted. Please delete the task to remove the item");

                //        //        client.ClientInstructions.Instructions.Remove(instruction);

                //    }
            }
            catch (Exception x)
            {
            }

        }
        private void FnaNeed_ItemDeleted_EventHandler(object sender, ItemDeletedEventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }
            _hasChanges = true;

        }

        private void Fund_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }
            try
            {
                //HasChanges = true;

                 DevAge.ComponentModel.BoundList<Fund> _bList = sender as DevAge.ComponentModel.BoundList<Fund>;
                Fund mObj = _bList.EditedObject as Fund;

                mObj.UpdateBy = Program.User.Username;
                mObj.UpdateDate = DateTime.Now;
                mObj.FundValueDate = DateTime.Now;
               
                _hasChanges = true;


                //Calculate the fund policy
                if (dataGrid_PolicyFunds.Tag != null)
                {
                    BaseEntity<int> entity = dataGrid_PolicyFunds.Tag as BaseEntity<int>;
                    if (entity != null)
                    {
                        entity.Calculate();//.InvokePropertyChanged(e.PropertyDescriptor.Name);
                    }

                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                RefreshPortfolioSummary();

                RefreshStatusStrip();

                RefreshGrids();

            }
        }

        private void Benefit_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }
            try
            {

                DevAge.ComponentModel.BoundList<Benefit> _bList = sender as DevAge.ComponentModel.BoundList<Benefit>;
                Benefit mObj = _bList.EditedObject as Benefit;

                mObj.UpdateBy = Program.User.Username;
                mObj.UpdateDate = DateTime.Now;

                _hasChanges = true;

                //Calculate the fund policy
                if (dataGrid_PolicyFunds.Tag != null)
                {
                    BaseEntity<int> entity = dataGrid_PolicyFunds.Tag as BaseEntity<int>;
                    if (entity != null)
                    {
                        entity.Calculate();
                    }

                }
            }
            catch (Exception x)
            {

            }
            finally
            {
                RefreshPortfolioSummary();

                RefreshStatusStrip();

                RefreshGrids();

            }
        }

        private void Education_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }

            _hasChanges = true;
            try
            {
                if (!(e.PropertyDescriptor == null))
                { 
                if (e.PropertyDescriptor.Name == "DependentName")
                {
                    DevAge.ComponentModel.BoundList<Education> _bList = sender as DevAge.ComponentModel.BoundList<Education>;
                    Education mObj = _bList.EditedObject as Education;

                    var _Obj = client.Beneficiaries.Where(x => x.DependentName == mObj.DependentName).FirstOrDefault();
                    if (_Obj != null)
                    {
                        mObj.DependentDOB = _Obj.BirthDate;
                        mObj.CurrentAge = _Obj._Age;
                    }
                    //Calculate the fund policy
                    if (dataGrid_PolicyFunds.Tag != null)
                    {
                        BaseEntity<int> entity = dataGrid_PolicyFunds.Tag as BaseEntity<int>;
                        if (entity != null)
                        {
                            entity.Calculate();
                        }

                    }
                }
                }
            }
            catch (Exception x)
            {
            }
            finally
            {
                RefreshPortfolioSummary();

                RefreshStatusStrip();

                RefreshGrids();

            }
            ClientPortfolio_propertyChanged_EventHandler(sender, e);
        }

        private void Medical_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }
            _hasChanges = true;


            try
            {
                if (e.PropertyDescriptor.Name == "Type")
                {
                    DevAge.ComponentModel.BoundList<Medical> _bList = sender as DevAge.ComponentModel.BoundList<Medical>;
                    Medical mObj = _bList.EditedObject as Medical;

                    IEnumerable<MedicalPlan> medicalPlans = (IEnumerable<MedicalPlan>)Program.ServiceProviders.MedicalProviders.MedicalAids.Where(x => x.ProviderName == mObj.Type).FirstOrDefault().MedicalPlans;
                    if (medicalPlans.Where(x => x.PlanName != null).Count() > 0)
                    {   //Update the editor
                        dataGrid_MedicalPortfolio.Columns[3].DataCell.Editor = new ComboListEditor(medicalPlans.ToListDataItem("PlanName", "PlanName")); ;
                        mObj.Description = "";//reset the Medical Plans
                    }
                }
            }
            catch (Exception x)
            {
            }

            ClientPortfolio_propertyChanged_EventHandler(sender, e);
        }
        private void Lifes_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }
            _hasChanges = true;
            try
            {
                if (e.PropertyDescriptor!=null)
                {
                    if (e.PropertyDescriptor.Name == "Type")
                    {
                        //    DevAge.ComponentModel.BoundList<Life> _bList = sender as DevAge.ComponentModel.BoundList<Life>;
                        //    Life mObj = _bList.EditedObject as Life;

                        //    dataGrid_LifePortfolio.Columns[2].DataCell.Editor.StandardValues = new List<LifeProduct>();

                        //    IEnumerable<LifeProduct> lifeProducts = (IEnumerable<LifeProduct>)Program.ServiceProviders.LifeProviders.LifeInsurers.Where(x => x.InsurerName == mObj.Type).FirstOrDefault().LifeProducts;

                        //    if (lifeProducts.Where(x => x.ProductName != null).Count() > 0)
                        //    {
                        //        dataGrid_LifePortfolio.Columns[2].DataCell.Editor.StandardValues = lifeProducts.Select(x => x.ProductName).ToList();

                        //        // m.Description = "";//reset the policy
                        //    }
                    }
                }
            }
            catch (NullReferenceException)
            { 
                
            }
            catch (Exception x)
            {
            }
            ClientPortfolio_propertyChanged_EventHandler(sender, e);
        }

        private void Meetings_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }
            try
            {

                DevAge.ComponentModel.BoundList<ClientMeetings> _bList = sender as DevAge.ComponentModel.BoundList<ClientMeetings>;
                ClientMeetings mObj = _bList.EditedObject as ClientMeetings;

                mObj.ClientId = client.Id;
                mObj.AgentId = client.AgentDetails.Id;

                mObj.UpdateBy = Program.User.Username;
                mObj.UpdateDate = DateTime.Now;

                //_hasChanges = true;

                if (e.ListChangedType == ListChangedType.ItemAdded)
                    Program.Repository.Add<ClientMeetings, int>(mObj);
                else
                    Program.Repository.Update<ClientMeetings, int>(mObj);

            }
            catch (Exception x)
            {

            }
            finally
            {
                RefreshStatusStrip();
            }
        }
        private void Meetings_itemDeleted_EventHandler(object sender, EventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }

            try
            {

                DevAge.ComponentModel.BoundList<ClientMeetings> _bList = sender as DevAge.ComponentModel.BoundList<ClientMeetings>;
                ClientMeetings mObj = _bList.EditedObject as ClientMeetings;

                Program.Repository.Remove<ClientMeetings, int>(mObj.Id);


            }
            catch (Exception x)
            {

            }
            finally
            {
                RefreshStatusStrip();
            }
        }

        private void AdminTask_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }
            try
            {

                DevAge.ComponentModel.BoundList<Instruction> _bList = sender as DevAge.ComponentModel.BoundList<Instruction>;
                Instruction instruction = _bList.EditedObject as Instruction;

                if (instruction != null)
                {

                    instruction.ClientId = client.Id;
                    //instruction.ReferenceOwner = mObj.ReferenceOwner;
                    instruction.UpdateDate = DateTime.Now;
                    instruction.UpdateBy = Program.User.Username;

                    if (e.ListChangedType == ListChangedType.ItemAdded)
                        Program.Repository.Add<Instruction, int>(instruction);
                    else
                        Program.Repository.Update<Instruction, int>(instruction);
                }

            }
            catch (Exception x)
            {

            }
            finally
            {
                RefreshStatusStrip();
            }
        }
        private void AdminTaskDeleted_EventHandler(object sender, ItemDeletedEventArgs e)
        {
            if (_isInitialising)
            {
                return;
            }
            _hasChanges = true;
            try
            {

                Instruction instruction = e.Item as Instruction;
                //TO DO: Update Task Status to Deleted
            }
            catch (Exception x)
            {
            }

        }

        #endregion

        #region Client Events
        /// <summary>
        /// Captures the property change event for Client and all its children
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_isInitialising && !_readOnly)
            {
                _hasChanges = true;
                RefreshStatusStrip();
            }
        }
        #endregion

        #region Client Portfolio RowHeader Select Events

        private void RetirementPortfolio_RowHeaderSelectEventHandler(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;

                dataGrid_PolicyFunds.InitialiseRectangle(context);

                try
                {
                    SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                    BoundList<Retirement> bList = dGrid.DataSource as BoundList<Retirement>;

                    Retirement retirement = bList[context.Position.Row - 1] as Retirement;
                    if (retirement != null)
                    {
                        //Lisp Lisp = new Lisp();
                        //List<ListDataItem> LispFunds = new List<ListDataItem>();
                        //if (Program.ServiceProviders != null)
                        //    Lisp = Program.ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == retirement.Description).FirstOrDefault();
                        //if (Lisp != null)
                        //    LispFunds = Lisp.LispFunds.ToListDataItem("FundName", "FundName");

                        retirement.Initialise();

                        this.dataGrid_PolicyFunds.Initialise1<Fund>(retirement.FundsBindingList, columns =>
                        {
                            columns.For(x => x.FundCode, "Fund Code", new StringEditor(true));
                            columns.For(x => x.Description, "Fund Name", new StringEditor(true));
                            columns.For(x => x.StartDate, "Inception Dt", new DateEditor(true));
                            columns.For(x => x.InitialAmount, "Deposit", new CurrencyEditor(true));
                            columns.For(x => x.WithdrawalAmount, "Withdrawal", new CurrencyEditor(true));
                            columns.For(x => x.SplitPerc, "Split %", new DecimalEditor(_readOnly));
                            columns.For(x => x.MonthlyContribution, "Premium", new CurrencyEditor(true));
                            columns.For(x => x.CurrentAmount, "Current Value", new CurrencyEditor(_readOnly));
                            // columns.For(x => x.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
                            columns.For(x => x.UpdateDate, "Last Update", new DateEditor(true));

                            //remove this
                            //columns.For(x => x.UpdateDate, "Update", new DateEditor(true));

                            //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
                            columns.For(x => x.Risk, "Risk", new StringEditor(true));
                            //columns.For(x => x.IRR, "IRR", new StringEditor(true));
                        },
                        PropertyChangedHandler: Fund_propertyChanged_EventHandler,

                         //OnCompleted: Fund_propertyChanged_EventHandler),
                        

                        ReadOnly: _readOnlyForAdminAdvisorClerk,
                        AllowAddNew: false,
                        AllowDelete: false
                        )
                        .Format1(_readOnlyForAdminAdvisorClerk, format: MetroControlExt.GridFormats.Default);

                        dataGrid_PolicyFunds.Tag = retirement;

                        dataGrid_PolicyFunds.DisplayRectangle(context);
                    }

                }
                catch (Exception x)
                {

                }
            }
            catch (Exception x)
            {
            }
        }
        private void InvestmentPortfolio_RowHeaderSelectEventHandler(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;

                dataGrid_PolicyFunds.InitialiseRectangle(context);

                try
                {
                    SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                    BoundList<Investment> bList = dGrid.DataSource as BoundList<Investment>;

                    Investment Investment = bList[context.Position.Row - 1] as Investment;
                    if (Investment != null)
                    {
                        //Lisp Lisp = new Lisp();
                        //List<ListDataItem> LispFunds = new List<ListDataItem>();
                        //if (Program.ServiceProviders != null)
                        //    Lisp = Program.ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == Investment.Description).FirstOrDefault();
                        //if (Lisp != null)
                        //    LispFunds = Lisp.LispFunds.ToListDataItem("FundName", "FundName");

                        Investment.Initialise();

                        this.dataGrid_PolicyFunds.Initialise1<Fund>(Investment.FundsBindingList, columns =>
                        {
                            columns.For(x => x.Description, "Fund Name", new StringEditor(true));
                            columns.For(x => x.StartDate, "Inception Dt", new DateEditor(true));
                            columns.For(x => x.InitialAmount, "Deposit", new CurrencyEditor(true));
                            columns.For(x => x.WithdrawalAmount, "Withdrawal", new CurrencyEditor(true));
                            columns.For(x => x.SplitPerc, "Split %", new DecimalEditor(true));
                            columns.For(x => x.MonthlyContribution, "Premium", new CurrencyEditor(true));
                            columns.For(x => x.CurrentAmount, "Current Value", new CurrencyEditor(_readOnly));
                            // columns.For(x => x.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
                            columns.For(x => x.UpdateDate, "Last Update", new DateEditor(true));
                            //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
                            columns.For(x => x.Risk, "Risk", new StringEditor(true));
                            columns.For(x => x.IRR, "IRR", new StringEditor(true));
                        },
                        PropertyChangedHandler: Fund_propertyChanged_EventHandler,
                        ReadOnly: _readOnlyForAdminAdvisorClerk,
                        AllowAddNew: false,
                        AllowDelete: false
                        )
                        .Format1(_readOnlyForAdminAdvisorClerk, format: MetroControlExt.GridFormats.Default);

                        dataGrid_PolicyFunds.Tag = client.ClientPortfolio;// Investment;
                    }

                    dataGrid_PolicyFunds.DisplayRectangle(context);

                }
                catch (Exception x)
                {

                }
            }
            catch (Exception x)
            {
            }
        }
        private void EducationPortfolio_RowHeaderSelectEventHandler(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;

                dataGrid_PolicyFunds.InitialiseRectangle(context);

                try
                {
                    SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                    BoundList<Education> bList = dGrid.DataSource as BoundList<Education>;

                    Education education = bList[context.Position.Row - 1] as Education;
                    if (education != null)
                    {
                        //Lisp Lisp = new Lisp();
                        //List<ListDataItem> LispFunds = new List<ListDataItem>();
                        //if (Program.ServiceProviders != null)
                        //    Lisp = Program.ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == education.Description).FirstOrDefault();
                        //if (Lisp != null)
                        //    LispFunds = Lisp.LispFunds.ToListDataItem("FundName", "FundName");


                        education.Initialise();

                        this.dataGrid_PolicyFunds.Initialise1<Fund>(education.FundsBindingList, columns =>
                        {
                            columns.For(x => x.Description, "Fund Name", new StringEditor(true));
                            columns.For(x => x.StartDate, "Inception Dt", new DateEditor(true));
                            columns.For(x => x.InitialAmount, "Deposit", new CurrencyEditor(true));
                            columns.For(x => x.WithdrawalAmount, "Withdrawal", new CurrencyEditor(true));
                            columns.For(x => x.SplitPerc, "Split %", new DecimalEditor(true));
                            columns.For(x => x.MonthlyContribution, "Premium", new CurrencyEditor(true));
                            columns.For(x => x.CurrentAmount, "Current Value", new CurrencyEditor(_readOnly));
                            // columns.For(x => x.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
                            columns.For(x => x.UpdateDate, "Last Update", new DateEditor(true));
                            //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
                            columns.For(x => x.Risk, "Risk", new StringEditor(true));
                            columns.For(x => x.IRR, "IRR", new StringEditor(true));
                        },
                        PropertyChangedHandler: Fund_propertyChanged_EventHandler,
                        ReadOnly: _readOnlyForAdminAdvisorClerk,
                        AllowAddNew: false,
                        AllowDelete: false
                        )
                        .Format1(_readOnlyForAdminAdvisorClerk, format: MetroControlExt.GridFormats.Default);

                        dataGrid_PolicyFunds.Tag = client.ClientPortfolio;// Education;
                    }

                    dataGrid_PolicyFunds.DisplayRectangle(context);

                }
                catch (Exception x)
                {

                }
            }
            catch (Exception x)
            {
            }
        }
        private void MedicalPortfolio_RowHeaderSelectEventHandler(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;

                dataGrid_PolicyFunds.InitialiseRectangle(context);

                try
                {
                    SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                    BoundList<Medical> bList = dGrid.DataSource as BoundList<Medical>;
                    Medical Medical = bList[context.Position.Row - 1] as Medical;
                    if (Medical != null)
                    {
                        //MedicalAid Lisp = new MedicalAid();
                        //List<ListDataItem> LispFunds = new List<ListDataItem>();
                        //if (Program.ServiceProviders != null)
                        //    Lisp = Program.ServiceProviders.MedicalProviders.MedicalAids.Where(x => x.ProviderName == Medical.Description).FirstOrDefault();
                        //if (Lisp != null)
                        //    LispFunds = Lisp.MedicalPlans.ToListDataItem("PlanName", "PlanName");

                        Medical.Initialise();

                        this.dataGrid_PolicyFunds.Initialise1<Benefit>(Medical.BenefitsBindingList, columns =>
                        {
                            columns.For(x => x.Description, "Benefit Type", new StringEditor(true));
                            columns.For(x => x.StartDate, "Inception Dt", new DateEditor(true));
                            //columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                            //columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(Action != PolicyAction.AmendPolicy ? true : ReadOnly));
                            //columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
                            columns.For(x => x.CoverAmount, "Cover Amount", new CurrencyEditor(true));
                            columns.For(x => x.UpdateDate, "Last Update", new DateEditor(true));
                        },
                        PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
                        //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                        ReadOnly: _readOnlyForAdminAdvisorClerk,
                        AllowAddNew: false,
                        AllowDelete: false
                        )
                        .Formatt(_readOnlyForAdminAdvisorClerk);

                        dataGrid_PolicyFunds.Tag = client.ClientPortfolio;// Medical;
                    }

                    dataGrid_PolicyFunds.DisplayRectangle(context);

                }
                catch (Exception x)
                {

                }
            }
            catch (Exception x)
            {
            }
        }
        private void LifePortfolio_RowHeaderSelectEventHandler(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;

                dataGrid_PolicyFunds.InitialiseRectangle(context);

                try
                {
                    SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                    BoundList<Life> bList = dGrid.DataSource as BoundList<Life>;
                    Life Life = bList[context.Position.Row - 1] as Life;

                    Life.Initialise();

                    if (Life != null)
                    {
                        this.dataGrid_PolicyFunds.Initialise1<Benefit>(Life.BenefitsBindingList, columns =>
                        {
                            columns.For(x => x.Type, "Benefit", new ComboBoxEditor(ListDataItemType.LifeBenefit, true));
                            columns.For(x => x.Description, "Description", new StringEditor(true));
                            columns.For(x => x.CoverAmount, "Insured Amount", new CurrencyEditor(true));
                            columns.For(x => x.UpdateDate, "Last Update", new DateEditor(true));
                        },
                        PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
                        ReadOnly: _readOnlyForAdminAdvisorClerk,
                        AllowAddNew: false,
                        AllowDelete: false
                        )
                        .Formatt(_readOnlyForAdminAdvisorClerk);

                        dataGrid_PolicyFunds.Tag = client.ClientPortfolio;// Life;
                    }

                    dataGrid_PolicyFunds.DisplayRectangle(context);

                }
                catch (Exception x)
                {

                }
            }
            catch (Exception x)
            {
            }
        }
        private void AssetsPortfolio_RowHeaderSelectEventHandler(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;

                dataGrid_PolicyFunds.InitialiseRectangle(context);

                try
                {
                    SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                    BoundList<IncomeAsset> bList = dGrid.DataSource as BoundList<IncomeAsset>;
                    IncomeAsset asset = bList[context.Position.Row - 1] as IncomeAsset;
                    if (asset != null)
                    {
                        //Lisp Lisp = new Lisp();
                        //List<ListDataItem> LispFunds = new List<ListDataItem>();
                        //if (Program.ServiceProviders != null)
                        //    Lisp = Program.ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == asset.Description).FirstOrDefault();
                        //if (Lisp != null)
                        //    LispFunds = Lisp.LispFunds.ToListDataItem("FundName", "FundName");

                        asset.Initialise();

                        this.dataGrid_PolicyFunds.Initialise1<Fund>(asset.FundsBindingList, columns =>
                        {
                            columns.For(x => x.Description, "Fund Name", new StringEditor(true));
                            columns.For(x => x.StartDate, "Inception Dt", new DateEditor(true));
                            columns.For(x => x.InitialAmount, "Value", new CurrencyEditor(true));
                            columns.For(x => x.SplitPerc, "Split %", new DecimalEditor(true));
                            columns.For(x => x.MonthlyContribution, "Income", new CurrencyEditor(true));
                            columns.For(x => x.UpdateDate, "Last Update", new DateEditor(true));
                            columns.For(x => x.Risk, "Risk", new StringEditor(true));
                            columns.For(x => x.IRR, "IRR", new StringEditor(true));
                        },
                        PropertyChangedHandler: Fund_propertyChanged_EventHandler,
                        ReadOnly: _readOnlyForAdminAdvisorClerk,
                        AllowAddNew: false,
                        AllowDelete: false
                        )
                        .Formatt(_readOnlyForAdminAdvisorClerk);

                        dataGrid_PolicyFunds.Tag = client.ClientPortfolio;
                    }

                    dataGrid_PolicyFunds.DisplayRectangle(context);

                }
                catch (Exception x)
                {

                }
            }
            catch (Exception x)
            {
            }
        }

        #endregion

        #region Client Fna Events
        private void MetroTabControl_InvestmentsFNA_SelectedIndexChanged(object sender, EventArgs e)
        {
            MetroTabControl cntrl = sender as MetroTabControl;
            this.metroPanel_NonRetirementFnaParams.Controls.Clear();

            switch (cntrl.SelectedIndex)
            {
                case 0://Investment
                    client.ClientFnaInvestment.IsLoading = true;
                    if (client.ClientFnaInvestment.InflationPercentage == 0)
                        client.ClientFnaInvestment.InflationPercentage = client.ClientFna.InflationPercentage;
                    //if (client.ClientFnaInvestment.GrowthPercentage == 0)
                    //    client.ClientFnaInvestment.GrowthPercentage = client.ClientFna.GrowthPercentage;
                    //if (client.ClientFnaInvestment.EsclPercentage == 0)
                    //    client.ClientFnaInvestment.EsclPercentage = client.ClientFna.EsclPercentage;
                    this.metroPanel_NonRetirementFnaParams.Initialise(client.ClientFnaInvestment, cntr =>
                    {
                        cntr.For(x => x.InflationPercentage, "Inflation %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));
                        //cntr.For(x => x.GrowthPercentage, "Growth %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));
                        //cntr.For(x => x.EsclPercentage, "Escalation %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));

                    }, left: 5, top: 45, labelWidth: 85, controlsLayout: ControlsLayout.Horizontal, PropertyChangedHandler: InvestmentNeed_propertyChanged_EventHandler)
                            .Format();//

                    RefreshInvestmentFnaSummary();
                    client.ClientFnaInvestment.IsLoading = false;
                    break;
                case 1://Education Needs
                    client.ClientFnaEducation.IsLoading = true;
                    if (client.ClientFnaEducation.InflationPercentage == 0)
                        client.ClientFnaEducation.InflationPercentage = client.ClientFna.InflationPercentage;
                    if (client.ClientFnaEducation.GrowthPercentage == 0)
                        client.ClientFnaEducation.GrowthPercentage = client.ClientFna.GrowthPercentage;
                    if (client.ClientFnaEducation.EsclPercentage == 0)
                        client.ClientFnaEducation.EsclPercentage = client.ClientFna.EsclPercentage;

                    this.metroPanel_NonRetirementFnaParams.Initialise(client.ClientFnaEducation, cntr =>
                    {
                        cntr.For(x => x.InflationPercentage, "Inflation %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));
                        cntr.For(x => x.GrowthPercentage, "Growth %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));
                        cntr.For(x => x.EsclPercentage, "Escalation %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));

                    }, left: 5, top: 45, labelWidth: 85, controlsLayout: ControlsLayout.Horizontal, PropertyChangedHandler: EducationNeed_propertyChanged_EventHandler)
                            .Format();//

                    RefreshEducationFnaSummary();
                    client.ClientFnaEducation.IsLoading = false;
                    break;
                case 2://Risk Cover Needs
                    ClientFnaRisk.IsLoading = true;
                    if (ClientFnaRisk.InflationPercentage == 0)
                        ClientFnaRisk.InflationPercentage = client.ClientFna.InflationPercentage;
                    if (ClientFnaRisk.GrowthPercentage == 0)
                        ClientFnaRisk.GrowthPercentage = client.ClientFna.GrowthPercentage;
                    if (ClientFnaRisk.EsclPercentage == 0)
                        ClientFnaRisk.EsclPercentage = client.ClientFna.EsclPercentage;

                    this.metroPanel_NonRetirementFnaParams.Initialise(ClientFnaRisk, cntr =>
                    {
                        cntr.For(x => x.CurrentAge, "Age", new MetroTextBoxEditor(Width: 50).ReadOnly(true));
                        cntr.For(x => x.InflationPercentage, "Inflation %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));
                        cntr.For(x => x.GrowthPercentage, "Growth %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));
                        cntr.For(x => x.EsclPercentage, "Escalation %", new MetroPercentageEditor(Width: 50).ReadOnly(_readOnly));
                        //cntr.For(x => x.RiskAge, "Risk Event Age", new MetroTextBoxEditor(Width: 50).ReadOnly(_readOnly));

                    }, left: 5, top: 25, labelWidth: 95, controlsLayout: ControlsLayout.Horizontal, PropertyChangedHandler: RiskCoverNeed_propertyChanged_EventHandler)
                            .Format(); //

                    RefreshRiskFnaSummary();

                    ClientFnaRisk.IsLoading = false;
                    break;
            }
        }

        private void RetirementNeed_RowHeaderSelectEventHandler(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;

                dataGrid_PolicyFunds.InitialiseRectangle(context);

                try
                {
                    SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                    BoundList<Need> bList = dGrid.DataSource as BoundList<Need>;
                    Need need = bList[context.Position.Row - 1] as Need;
                    if (need != null)
                    {
                        Lisp = new Lisp();
                        List<ListDataItem> LispFunds = new List<ListDataItem>();
                        if (Program.ServiceProviders != null)
                            Lisp = Program.ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == need.Description).FirstOrDefault();
                        if (Lisp != null)
                            LispFunds = Lisp.LispFunds.ToListDataItem("FundName", "FundName");

                        this.dataGrid_PolicyFunds.Initialise<Fund>(need.Funds, columns =>
                        {
                            columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(_readOnly));
                            columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(_readOnly));
                            columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(_readOnly));
                            columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(_readOnly));
                            columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
                            //columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                            //columns.For(x => x.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
                            columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                            //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
                            columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
                            columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
                        },
                            PropertyChangedHandler: Fund_propertyChanged_EventHandler,
                            //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                            ReadOnly: _readOnlyForAdminAdvisor,
                            AllowAddNew: !_readOnly,
                            AllowDelete: !_readOnly
                            )
                            .Formatt(_readOnly);

                        dataGrid_PolicyFunds.Tag = client.ClientFna; //Fna
                    }
                    dataGrid_PolicyFunds.DisplayRectangle(context);

                }
                catch (Exception x)
                {

                }
            }
            catch (Exception x)
            {
            }
        }
        private void EducationNeed_RowHeaderSelectEventHandler(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;

                dataGrid_PolicyFunds.InitialiseRectangle(context);

                try
                {
                    SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                    BoundList<EducationNeed> bList = dGrid.DataSource as BoundList<EducationNeed>;
                    EducationNeed need = bList[context.Position.Row - 1] as EducationNeed;
                    if (need != null)
                    {
                        Lisp = new Lisp();
                        List<ListDataItem> LispFunds = new List<ListDataItem>();
                        if (Program.ServiceProviders != null)
                            Lisp = Program.ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == need.Description).FirstOrDefault();
                        if (Lisp != null)
                            LispFunds = Lisp.LispFunds.ToListDataItem("FundName", "FundName");

                        this.dataGrid_PolicyFunds.Initialise<Fund>(need.Funds, columns =>
                        {
                            columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(_readOnly));
                            columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(_readOnly));
                            columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(_readOnly));
                            columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(_readOnly));
                            columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
                            //columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                            //columns.For(x => x.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
                            columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                            //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
                            columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
                            columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
                        },
                            PropertyChangedHandler: Fund_propertyChanged_EventHandler,
                            //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                            ReadOnly: _readOnlyForAdminAdvisor,
                            AllowAddNew: !_readOnly,
                            AllowDelete: !_readOnly
                            )
                            .Formatt(_readOnly);

                        dataGrid_PolicyFunds.Tag = client.ClientFnaEducation; //Fna
                    }
                    dataGrid_PolicyFunds.DisplayRectangle(context);

                }
                catch (Exception x)
                {

                }
            }
            catch (Exception x)
            {
            }
        }
        private void InvestmentNeed_RowHeaderSelectEventHandler(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;

                dataGrid_PolicyFunds.InitialiseRectangle(context);

                try
                {
                    SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                    BoundList<InvestmentNeed> bList = dGrid.DataSource as BoundList<InvestmentNeed>;
                    InvestmentNeed need = bList[context.Position.Row - 1] as InvestmentNeed;
                    if (need != null)
                    {
                        Lisp = new Lisp();
                        List<ListDataItem> LispFunds = new List<ListDataItem>();
                        if (Program.ServiceProviders != null)
                            Lisp = Program.ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == need.Description).FirstOrDefault();
                        if (Lisp != null)
                            LispFunds = Lisp.LispFunds.ToListDataItem("FundName", "FundName");

                        this.dataGrid_PolicyFunds.Initialise<Fund>(need.Funds, columns =>
                        {
                            columns.For(x => x.Description, "Fund Name", new MetroComboListEditor().DataSourceList(LispFunds).ReadOnly(_readOnly));
                            columns.For(x => x.StartDate, "Inception Dt", new MetroDateEditor().ReadOnly(_readOnly));
                            columns.For(x => x.InitialAmount, "Lump Sum", new MetroCurrencyEditor().ReadOnly(_readOnly));
                            columns.For(x => x.SplitPerc, "Split %", new MetroPercentageEditor().ReadOnly(_readOnly));
                            columns.For(x => x.MonthlyContribution, "Premium", new MetroCurrencyEditor().ReadOnly(true));
                            //columns.For(x => x.CurrentAmount, "Current Value", new MetroCurrencyEditor().ReadOnly(ReadOnly));
                            //columns.For(x => x.GrowthPercentage, "Growth", new MetroPercentageEditor().ReadOnly(ReadOnly));
                            columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                            //columns.For(x => x.Type, "Type", new MetroTextBoxEditor().ReadOnly(true));
                            columns.For(x => x.Risk, "Risk", new MetroTextBoxEditor().ReadOnly(true));
                            columns.For(x => x.IRR, "IRR", new MetroDecimalEditor().ReadOnly(true));
                        },
                            PropertyChangedHandler: Fund_propertyChanged_EventHandler,
                            //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                            ReadOnly: _readOnlyForAdminAdvisor,
                            AllowAddNew: !_readOnly,
                            AllowDelete: !_readOnly
                            )
                            .Formatt(_readOnly);

                        dataGrid_PolicyFunds.Tag = client.ClientFnaInvestment; //Fna
                    }
                    dataGrid_PolicyFunds.DisplayRectangle(context);

                }
                catch (Exception x)
                {

                }
            }
            catch (Exception x)
            {
            }
        }
        private void RiskCoverNeed_RowHeaderSelectEventHandler(object sender, EventArgs e)
        {
            try
            {
                CellContext context = (CellContext)sender;

                dataGrid_PolicyFunds.InitialiseRectangle(context);

                try
                {
                    SourceGrid.DataGrid dGrid = context.Grid as SourceGrid.DataGrid;
                    BoundList<RiskCoverNeed> bList = dGrid.DataSource as BoundList<RiskCoverNeed>;
                    RiskCoverNeed need = bList[context.Position.Row - 1] as RiskCoverNeed;
                    if (need != null)
                    {


                        this.dataGrid_PolicyFunds.Initialise<Benefit>(need.Benefits, columns =>
                        {
                            columns.For(x => x.Type, "Benefit", new MetroComboListEditor().DataSourceList(Program.listData.List(ListDataItemType.LifeBenefit)).ReadOnly(true));
                            columns.For(x => x.Description, "Description", new MetroTextBoxEditor().ReadOnly(true));
                            columns.For(x => x.CoverAmount, "Insured Amount", new MetroCurrencyEditor().ReadOnly(true));
                            columns.For(x => x.UpdateDate, "Last Update", new MetroDateEditor().ReadOnly(true));
                        },
                            PropertyChangedHandler: Benefit_propertyChanged_EventHandler,
                            //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly),
                            ReadOnly: _readOnlyForAdminAdvisorClerk,
                            AllowAddNew: false,
                            AllowDelete: false
                            )
                            .Formatt(_readOnlyForAdminAdvisorClerk);

                        dataGrid_PolicyFunds.Tag = client.ClientPortfolio;// Life;
                    }
                    dataGrid_PolicyFunds.DisplayRectangle(context);

                }
                catch (Exception x)
                {

                }
            }
            catch (Exception x)
            {
            }
        }

        


        private void RetirementNeed_propertyChanged_EventHandler(object sender, EventArgs e)
        {
            this.dataGrid_RetireFNA_Need.Refresh();
            RefreshClientFnaSummary();
        }
        private void EducationNeed_propertyChanged_EventHandler(object sender, EventArgs e)
        {
            this.dataGrid_EducationFna.Refresh();
            RefreshEducationFnaSummary();
        }
        private void InvestmentNeed_propertyChanged_EventHandler(object sender, EventArgs e)
        {
            this.dataGrid_InvestmentFna.Refresh();
            RefreshInvestmentFnaSummary();
        }
        private void RiskCoverNeed_propertyChanged_EventHandler(object sender, EventArgs e)
        {
            this.dataGrid_LifeRiskFna.Refresh();
            RefreshRiskFnaSummary();
        }

        private void RetirementNeed_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (!_isInitialising)
            {

                //_hasChanges = true;
                //try
                //{
                //    client.ClientFna.Calculate();

                //    RefreshStatusStrip();
                //}
                //catch (Exception x)
                //{
                //}
                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    propertyChanged_EventHandler(sender, e);
                    RefreshClientFnaSummary();
                }
            }
        }
        private void EducationNeed_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (!_isInitialising)
            {
                if (e.PropertyDescriptor != null)
                {
                    if (e.PropertyDescriptor.Name == "DependentName")
                    {
                        DevAge.ComponentModel.BoundList<EducationNeed> _bList = sender as DevAge.ComponentModel.BoundList<EducationNeed>;
                        EducationNeed mObj = _bList.EditedObject as EducationNeed;

                        var _Obj = client.ClientDependents.Dependents.Where(x => x.DependentName == mObj.DependentName).FirstOrDefault();
                        if (_Obj != null)
                        {
                            //mObj.IsLoading = true;
                            mObj.DependentDOB = _Obj.BirthDate;
                            //mObj.IsLoading = false;
                        }

                    }
                }

                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    propertyChanged_EventHandler(sender, e);
                    RefreshEducationFnaSummary();
                }
            }
        }
        private void InvestmentNeed_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (!_isInitialising)
            {
                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    propertyChanged_EventHandler(sender, e);
                    RefreshInvestmentFnaSummary();
                }
            }
        }
        private void RiskCoverNeed_propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            if (!_isInitialising)
            {
                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    propertyChanged_EventHandler(sender, e);
                    RefreshRiskFnaSummary();
                }
            }

        }

        #endregion

        #region Toolstrip Button Event Handlers
        private void toolStripDropDownButton_Export_Click(object sender, EventArgs e)
        {
            //if (!Program.Licensing.IsValid())
            //{
            //    MessageBoxExt.ShowInvalidLicense("Export Failed.");
            //    return;
            //}

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog.FileName = string.Format("{0} {1} {2}.xml", client.ClientDetails.FirstName, client.ClientDetails.LastName, client.ClientDetails.IdentificationNo);

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string FileName = saveFileDialog.FileName;

                    var doc = client.Serialize<Client>();
                    doc.Save(FileName, "some key");

                    MessageBoxExt.ShowInformation("Export succeeded");
                }
                catch (Exception x)
                {
                    MessageBoxExt.ShowException(x);
                }
            }
        }

        //private void toolStripButton_Documents_ButtonClick(object sender, EventArgs e)
        //{
        //    //Generates a word document from the selected template
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    openFileDialog.Filter = "Word Files (*.docx)|*.docx|Excel Files (*.xlsx)|*.xlsx";
        //    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        //    {
        //        try
        //        {
        //            string FileName = openFileDialog.FileName;
        //            FileInfo fInfo = new FileInfo(FileName);

        //            GenerateDocument(File.ReadAllBytes(fInfo.FullName), fInfo.Name.Substring(0, fInfo.Name.IndexOf(".")));
        //        }
        //        catch (Exception x)
        //        {
        //            MessageBoxExt.ShowException(x);
        //        }
        //    }

        //}

        private void DocumentTemplate_OnClick(object sender, EventArgs e)
        {

            //Ensure that all tabs are initialised so that all information pulls to reports
            initialiseTabs();


            try
            {
                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
                DocumentTemplate template = clickedItem.Tag as DocumentTemplate;

                if (template.TemplateBytes == null)
                    throw new Exception("Invalid or Incomplete document template. No document was loaded for " + template.TemplateName);

                GenerateDocument(template.TemplateBytes, template.TemplateName);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x, "Error loading document Template.");
            }

        }

        private void GenerateDocument(byte[] fileData, string fileName)
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                if (string.IsNullOrEmpty(client.DocumentsFolder))
                {
                    if (MessageBoxExt.ShowQuestion("A documents folder has not been created for this client. Do you wish to create now?"))
                    {
                        frmClientDocuments frm = new frmClientDocuments(client);
                        frm.ShowDialog(this);

                        if (!string.IsNullOrEmpty(client.DocumentsFolder))
                            documentsPath = client.DocumentsFolder;
                    }
                }
                else
                {
                    if (Directory.Exists(client.DocumentsFolder))
                        documentsPath = client.DocumentsFolder;
                }

                XmlDocument xmlDoc = new XmlDocument();

                if (fileName == "Estate & Risk Plan")
                {
                    xmlDoc = EstateAnalysis.ToXml<EstateAnalysis>(); //Serialise to Xml
                }
                else
                {
                    //Add Client data
                    xmlDoc = client.ToXml<Client>(); //Serialise Client object to Xml

                    //Add Advisor data
                    using (XmlWriter writer = xmlDoc.DocumentElement.CreateNavigator().AppendChild())
                    {
                        writer.WriteStartElement("Advisor");
                        writer.WriteElementString("FirstName", Program.User.Firstname);
                        writer.WriteElementString("Surname", Program.User.Surname);
                        writer.WriteEndElement();

                        //writer.WriteStartElement("Company");
                        //writer.WriteElementString("TradeName", Program.Company.TradeName);
                        //writer.WriteElementString("RegistrationNumber", Program.Company.RegistrationNumber);
                        //writer.WriteElementString("FSBNumber", Program.Company.FSBNumber);
                        //writer.WriteElementString("Caption", Program.Company.Caption);
                        //writer.WriteStartElement("Image");
                        //if (Program.Company.BgImage != null)
                        //    writer.WriteBase64(Program.Company.BgImage, 0, Program.Company.BgImage.Length);
                        //writer.WriteEndElement();
                        //writer.WriteEndElement();

                        //Other data you may wish to add


                    }

                    //Add Chart data
                    using (XmlWriter writer = xmlDoc.DocumentElement.CreateNavigator().AppendChild())
                    {
                        writer.WriteStartElement("Charts");

                        //Fna Chart
                        byte[] fnaChart = this.chart_Fna.GetChartAsBytes();
                        //writer.WriteElementString("Chart_Fna", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "chart_fna.png"));
                        writer.WriteStartElement("Chart_Fna");
                        writer.WriteBase64(fnaChart, 0, fnaChart.Length);
                        writer.WriteEndElement();

                        //Other Charts you may wish to add ...

                        writer.WriteEndElement();
                    }
                }
                //Generate the document
                byte[] output = xml2docx.Generate(fileData, xmlDoc);

                if (null != output)
                {
                    var outputFilename = string.Format("{0} {1} {2}- {3} {4}.docx", client.ClientDetails.LastName, client.ClientDetails.FirstName, client.ClientDetails.IdentificationNo, fileName, DateTime.Now.ToString("yyyyMMdd hhmmss"));
                    var outputPath = Path.Combine(documentsPath, outputFilename);

                    //Save doc to documents path
                    File.WriteAllBytes(outputPath, output);

                    //Launch doc in associated app
                    ProcessStartInfo startInfo = new ProcessStartInfo();

                    startInfo.CreateNoWindow = true;
                    startInfo.UseShellExecute = true;
                    startInfo.FileName = outputPath;
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;

                    Process.Start(startInfo);


                }
                else
                    throw new Exception("Could not generate docx");
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
        }

        private void toolStripButton_Update_Click(object sender, EventArgs e)
        {
            using (new AppWaitCursor(sender))
            {
                try
                {
                    //Check InActive Status
                    if (client.Status == ClientStatus.InActive.ToString())
                        if (!Program.User.IsAdministrator)
                            MessageBoxExt.ShowWarning(string.Format("This client is no longer Active and can only be updated by an Administrator"));
                        else
                            if (MessageBoxExt.ShowQuestion("This client is no longer Active. \r\n\r\nAre you sure you wish to Update this Client ? "))
                            client.Status = ClientStatus.Active.ToString();
                        else
                            return;

                    if (client.Id == 0)//New Client
                    {
                        //set agent details to current logged on advisor
                        if (Program.User.IsAdvisor)
                            client.AgentDetails = Program.User;

                        if (client.AgentDetails == null)
                            throw new MyValidationException("Please allocate this client to an Advisor in the Checklist Tab.");

                        Program.ClientService.Add(client);
                    }
                    client.Calculate();
                    Program.ClientService.Update(client);
                    if (Program.Licensing.HasFeature("Estate & Risk Planning"))
                    {
                        if (EstateAnalysis.Id == 0)
                            Program.EstateAnalysisService.Add(EstateAnalysis);
                        else
                            Program.EstateAnalysisService.Update(EstateAnalysis);
                    }

                    if (ClientFnaRisk.Id == 0)
                        Program.ClientFnaRiskService.Add(ClientFnaRisk);
                    else
                        Program.ClientFnaRiskService.Update(ClientFnaRisk);
                    _hasChanges = false;
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
                    RefreshStatusStrip();
                }
            }
        }
        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton_ClientDocs_Click(object sender, EventArgs e)
        {
            frmClientDocuments frm = new frmClientDocuments(client);
            frm.ShowDialog(this);
        }
        private void toolStripButton_Notes_Click(object sender, EventArgs e)
        {
            //frmClientNotes frmNotes = new frmClientNotes(_model.Id,ReadOnly);
            //frmNotes.ClientLable = string.Format("Notes for {0} {1}", _model.ClientDetails.FirstName, _model.ClientDetails.LastName);
            //frmNotes.Show();
            //frmNotes.BringToFront();
        }
        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            if (this._hasChanges)
                if (!MessageBoxExt.ShowQuestion(string.Format("This will refresh the data and all your changes will be lost.\r\n\r\nAre you sure you wish to Refresh?")))
                    return;

            using (new AppWaitCursor(sender))
            {
                this._hasChanges = false;

                #region Load model

                if (clientId != 0)
                { client = Program.ClientService.Get(clientId); }
                else
                { client = new Client(); _readOnly = false; }

                //setup events
                client.Initialise();

                client.PropertyChanged -= Client_PropertyChanged;
                client.PropertyChanged += Client_PropertyChanged;

                client.Refresh();


                #endregion

                MetroTabControl_Main_Selected(sender, _currentTabControlEventArgs);
                //frmMetroClient1_Load(sender, e);

                //Refresh Status
                RefreshStatusStrip();
            }
        }
        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {
            using (new AppWaitCursor(sender))
            {
                GetClientLock();

                //Check if client is locked and show Message
                if (ClientLockStatus.IsLocked == true)
                {
                    MessageBoxExt.ShowWarning(string.Format("This client has been locked for editing by {0} {1} on machine {2} at {3}", ClientLockStatus.FirstName, ClientLockStatus.Surname, ClientLockStatus.MachineName, ClientLockStatus.LockDate));
                }
                else
                {
                    //Check Inactive Status
                    if (client.Status == ClientStatus.InActive.ToString() && !Program.User.IsAdministrator)
                    {
                        MessageBoxExt.ShowWarning(string.Format("This client is no longer Active and can only be Updated by an Administrator"));
                        return;
                    }

                    //Add Lock
                    ClientLockStatus = new ClientLockStatus()
                    {
                        ClientId = client.Id,
                        UserId = Program.User.Id,
                        FirstName = string.IsNullOrEmpty(Program.User.Firstname) ? Program.User.Username : Program.User.Firstname,
                        Surname = Program.User.Surname,
                        MachineName = Environment.MachineName,
                        LockDate = DateTime.Now,
                        IsLocked = true,
                        Status = "0"
                    };

                    Program.Repository.Add<ClientLockStatus, int>(ClientLockStatus);

                    _readOnly = !_readOnly;

                    frmMetroClient1_Load(sender, e);
                }

            }
        }
        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            if (Program.User.IsAdministrator && Program.User.IsActive && client.Status == ClientStatus.InActive.ToString())
            {
                if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Permanently Delete this Client ? \r\n\r\n NB. The client will be permanently deleted by this action."))
                    return;
                try
                {
                    using (new AppWaitCursor(sender))
                    {
                        Program.ClientService.Remove(client.Id);

                        _hasChanges = false;
                    }

                    this.Close();
                }
                catch (Exception x)
                {
                    MessageBoxExt.ShowException(x);
                }
            }
            else
            {
                if (!MessageBoxExt.ShowQuestion("Are you sure you wish to Delete this Client ? \r\n\r\n NB. The client will be marked as InActive."))
                    return;
                try
                {
                    client.Status = ClientStatus.InActive.ToString();
                    Program.ClientService.Update(client);

                    _hasChanges = false;

                    this.Close();
                }
                catch (Exception x)
                {
                    MessageBoxExt.ShowException(x);
                }
            }
        }
        private void unlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (new AppWaitCursor(sender))
            {
                if (Program.User.IsAdministrator && Program.User.IsActive)
                {
                    //Force unlock - Administrator Only
                    if (ClientLockStatus.Id != 0)
                    {
                        ClientLockStatus.IsLocked = false;
                        Program.Repository.Update<ClientLockStatus, int>(ClientLockStatus);
                    }
                }

                RefreshStatusStrip();
            }
        }
        #endregion

        #region Form Button Events
        private void metroButton_CopyAddress_Click(object sender, EventArgs e)
        {
            //Copy Physical Address to Postal Address
            client.PostalAddress.Line1 = client.PhysicalAddress.Line1;
            client.PostalAddress.Line2 = client.PhysicalAddress.Line2;
            client.PostalAddress.Line3 = client.PhysicalAddress.Line3;
            client.PostalAddress.Line4 = client.PhysicalAddress.Line4;
            client.PostalAddress.Code = client.PhysicalAddress.Code;
        }
        private void metroButton_MemberContactSync_Click(object sender, EventArgs e)
        {
            using (new AppWaitCursor(sender))
            {
                if (string.IsNullOrEmpty(client.ClientDetails.IdentificationNo))
                {
                    MessageBoxExt.ShowWarning("Client Identification Number is required for synchronisation");
                    return;
                }
                try
                {
                    var contactDetails = new za.co.easiworx.office365.net.models.ContactDetails()
                    {
                        CustomerID = client.ClientContacts.Status,

                        Title = client.ClientDetails.ClientTitle,
                        Initials = client.ClientDetails.Initials,
                        FirstName = client.ClientDetails.FirstName.Trim(),
                        MiddleName = client.ClientDetails.MidName,
                        LastName = client.ClientDetails.LastName.Trim(),

                        Birthday = client.ClientDetails.DateOfBirth,
                        Language = client.ClientDetails.Language,

                        FullName = string.Format("{0} {1} {2}", client.ClientDetails.FirstName, client.ClientDetails.MidName, client.ClientDetails.LastName),
                        GovernmentIDNumber = client.ClientDetails.IdentificationNo,

                        HomeAddress = string.Format("{0} {1}, {2}\r\n{3}\r\n{4}\r\n{5}", client.PhysicalAddress.Line1, client.PhysicalAddress.Line2, client.PhysicalAddress.Line3, client.PhysicalAddress.Line4, client.PhysicalAddress.Code.ToString(), "South Africa"),
                        HomeAddressStreet = string.Format("{0} {1}, {2}", client.PhysicalAddress.Line1, client.PhysicalAddress.Line2, client.PhysicalAddress.Line3),
                        HomeAddressCity = client.PhysicalAddress.Line4,
                        HomeAddressCountry = "South Africa",
                        HomeAddressPostalCode = client.PhysicalAddress.Code.ToString(),

                        MailingAddress = string.Format("{0} {1}, {2}\r\n{3}\r\n{4}\r\n{5}", client.PostalAddress.Line1, client.PostalAddress.Line2, client.PostalAddress.Line3, client.PostalAddress.Line4, client.PostalAddress.Code.ToString(), "South Africa"),
                        MailingAddressStreet = string.Format("{0} {1}, {2}", client.PostalAddress.Line1, client.PostalAddress.Line2, client.PostalAddress.Line3),
                        MailingAddressCity = client.PostalAddress.Line4,
                        MailingAddressCountry = "South Africa",
                        MailingAddressPostalCode = client.PostalAddress.Code.ToString(),

                        HomeTelephoneNumber = client.ClientContacts.HomeTel,
                        MobileTelephoneNumber = client.ClientContacts.CellNo,
                        Email1Address = client.ClientContacts.EMailAddr,
                        BusinessTelephoneNumber = client.ClientContacts.BussTel,

                        Spouse = string.Format("{0}, {1} {2}", client.SpouseDetails.LastName, client.SpouseDetails.Initials, client.SpouseDetails.ClientTitle),

                        Profession = client.ClientDetails.Occupation,
                        JobTitle = client.ClientDetails.Occupation,

                    };

                    Task.Run(async () => {
                        await Program.OutlookProxy.AddContactDetailsAsync(contactDetails);
                        client.ClientContacts.Status = contactDetails.CustomerID;
                        this.chbMemberSynced.Checked = !String.IsNullOrEmpty(client.ClientContacts.Status);
                        this.Refresh();
                    }
                    );
                }
                catch (Exception x)
                {
                    MessageBoxExt.ShowException(x);
                }
            }

        }
        private void metroButton_SpouseContactSync_Click(object sender, EventArgs e)
        {
            using (new AppWaitCursor(sender))
            {
                if (string.IsNullOrEmpty(client.SpouseDetails.IdentificationNo))
                {
                    MessageBoxExt.ShowWarning("Spouse Identification Number is required for synchronisation");
                    return;
                }
                try
                {
                    var contactDetails = new za.co.easiworx.office365.net.models.ContactDetails()
                    {
                        CustomerID = client.SpouseContacts.Status,

                        Title = client.SpouseDetails.ClientTitle,
                        Initials = client.SpouseDetails.Initials,
                        FirstName = client.SpouseDetails.FirstName.Trim(),
                        MiddleName = client.SpouseDetails.MidName,
                        LastName = client.SpouseDetails.LastName.Trim(),

                        Birthday = client.SpouseDetails.DateOfBirth,
                        Language = client.SpouseDetails.Language,

                        FullName = string.Format("{0} {1} {2}", client.SpouseDetails.FirstName, client.SpouseDetails.MidName, client.SpouseDetails.LastName),
                        GovernmentIDNumber = client.SpouseDetails.IdentificationNo,

                        HomeAddress = string.Format("{0} {1}, {2}\r\n{3}\r\n{4}\r\n{5}", client.PhysicalAddress.Line1, client.PhysicalAddress.Line2, client.PhysicalAddress.Line3, client.PhysicalAddress.Line4, client.PhysicalAddress.Code.ToString(), "South Africa"),
                        HomeAddressStreet = string.Format("{0} {1}, {2}", client.PhysicalAddress.Line1, client.PhysicalAddress.Line2, client.PhysicalAddress.Line3),
                        HomeAddressCity = client.PhysicalAddress.Line4,
                        HomeAddressCountry = "South Africa",
                        HomeAddressPostalCode = client.PhysicalAddress.Code.ToString(),

                        MailingAddress = string.Format("{0} {1}, {2}\r\n{3}\r\n{4}\r\n{5}", client.PostalAddress.Line1, client.PostalAddress.Line2, client.PostalAddress.Line3, client.PostalAddress.Line4, client.PostalAddress.Code.ToString(), "South Africa"),
                        MailingAddressStreet = string.Format("{0} {1}, {2}", client.PostalAddress.Line1, client.PostalAddress.Line2, client.PostalAddress.Line3),
                        MailingAddressCity = client.PostalAddress.Line4,
                        MailingAddressCountry = "South Africa",
                        MailingAddressPostalCode = client.PostalAddress.Code.ToString(),

                        HomeTelephoneNumber = client.SpouseContacts.HomeTel,
                        MobileTelephoneNumber = client.SpouseContacts.CellNo,
                        Email1Address = client.SpouseContacts.EMailAddr,
                        BusinessTelephoneNumber = client.SpouseContacts.BussTel,

                        Spouse = string.Format("{0}, {1} {2}", client.ClientDetails.LastName, client.ClientDetails.Initials, client.ClientDetails.ClientTitle),

                        Profession = client.SpouseDetails.Occupation,
                        JobTitle = client.SpouseDetails.Occupation,

                    };

                    Task.Run(async () =>
                    {
                        await Program.OutlookProxy.AddContactDetailsAsync(contactDetails);
                        client.SpouseContacts.Status = contactDetails.CustomerID;
                        this.chbMemberSynced.Checked = !String.IsNullOrEmpty(client.SpouseContacts.Status);
                    }
                    );



                }
                catch (Exception x)
                {
                    MessageBoxExt.ShowException(x);
                }
            }
        }
        private void MbtnAddTask_Click(object sender, EventArgs e)
        {
            Instruction instruction = new Instruction();
            instruction.ClientId = client.ClientDetails.ClientId;
            instruction.Description = client.ClientDetails.Fullname;
            instruction.InstructionType = InstructionType.CUSTOMTASK;


            frmMetroAdminTaskAdd frm = new frmMetroAdminTaskAdd(instruction);
            frm.ShowDialog(this);

            ShowAdminTasks1(this.metroCheckBox1.Checked);
        }
        #endregion

        #region Form Methods

        private void GetClientLock()
        {
            ClientLockStatus = Program.Repository.List<ClientLockStatus, int>(x => x.ClientId == client.Id && x.IsLocked == true).FirstOrDefault();
            if (ClientLockStatus == null)
                ClientLockStatus = new ClientLockStatus();
        }
        private void ReleaseClientLock()
        {
            if (ClientLockStatus != null)
            {
                if (ClientLockStatus.UserId == Program.User.Id)
                {
                    if (ClientLockStatus.Id != 0)
                    {
                        ClientLockStatus.IsLocked = false;
                        Program.Repository.Update<ClientLockStatus, int>(ClientLockStatus);
                    }
                }
            }
        }

        //This is a method to initialise all tabs in the client form in order to ensure that no information goes missing when a report is generated. 
        private void initialiseTabs()
        {
            //Initialise Client portfolio
            client.ClientPortfolio.ServiceProvider = Program.ServiceProviders; // to calculate Fund risk values
            client.ClientPortfolio.Initialise();
            client.ClientPortfolio.Calculate();
            RefreshPortfolioSummary();

            //Initialise Assets and Liabilities
            client.ClientAssets.Initialise();
            client.ClientLiabilities.Initialise();
            RefreshAssetLiabilitiesSummary();

            //Initialise income and expenses
            client.ClientIncomes.Initialise();
            client.ClientExpenses.Initialise();
            RefreshIncomeExpensesSummary();

            //Initialise Retirement FNA
            client.ClientFna.ServiceProvider = Program.ServiceProviders;
            client.ClientFna.Initialise();
            client.UpdateRetirementFNA();
            RefreshClientFnaSummary();

            //Initialise Non Retirement FNA
            client.ClientFnaInvestment.ServiceProvider = Program.ServiceProviders; // to calculate Fund risk values
            client.ClientFnaInvestment.Initialise();
            client.ClientFnaInvestment.Calculate();

            //Initialise Education needs
            client.ClientFnaEducation.ServiceProvider = Program.ServiceProviders; // to calculate Fund risk values

            client.ClientFnaEducation.Initialise();
            client.ClientFnaEducation.Calculate();

            //Initialise Risk cover
            ClientFnaRisk.Initialise();
            ClientFnaRisk.Calculate();

            //Initialise Estate Planning
            EstateAnalysis.Initialise();
        }
        #endregion

        #region EstateDuty Events
        private void EstateAnalysis_PropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (!_hasChanges && !_readOnly)
            {
                _hasChanges = true;
                RefreshStatusStrip();
            }

            if (!_readOnly)
            {
                if (EstateAnalysis.CalculationCGTSummary.Count > 0)
                    this.dataGrid_CalculationAnalysis.Rebind(EstateAnalysis.CalculationCGTSummary);
                if (EstateAnalysis.CalculationExecutorSummary.Count > 0)
                    this.dataGrid_CalcExecFeesAnalysis.Rebind(EstateAnalysis.CalculationExecutorSummary);
                if (EstateAnalysis.LiquiditySummary.Count > 0)
                    this.dataGrid_LiquidityAnalysis.Rebind(EstateAnalysis.LiquiditySummary);
                if (EstateAnalysis.EstateDutySummary.Count > 0)
                    this.dataGrid_EstateDutySummary.Rebind(EstateAnalysis.EstateDutySummary);
                if (EstateAnalysis.RiskAnalysisSummary.Count > 0)
                    this.dataGrid_RiskCoverAnalysis.Rebind(EstateAnalysis.RiskAnalysisSummary);
            }
        }
        private void EstateAnalysis_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            EstateAnalysis_PropertyChangedEvent(sender, e);
        }
        private void EstateDuty_propertyChanged_EventHandler(object sender, EventArgs e)
        {
            EstateAnalysis.Calculate();

            EstateAnalysis_PropertyChanged(sender, null);
        }
        private void EstateDuty_MaritalRegimeChanged(object sender, EventArgs e)
        {
            MetroComboBoxEditor cntrl = sender as MetroComboBoxEditor;

            EstateAnalysis.MaritalRegimeDesc = cntrl.Control.Text;
        }

        //TO DO
        private void EstateDuty_printSummaryCalc(object sender, EventArgs e)
        {
            try
            {
                var docTemplate = Program.DocumentTemplatesList.Where(x => x.TemplateName == "Estate & Risk Plan").FirstOrDefault();

                if (docTemplate == null)
                    throw new Exception("The required template 'Estate & Risk Plan' could not be found. Please request your Administrator to import this document template");

                GenerateDocument(docTemplate.TemplateBytes, docTemplate.TemplateName);

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }

        }


        #endregion

        private void toolStripButton_Edit_Click_1(object sender, EventArgs e)
        {

        }

        private void kgbSpouseDetails_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGrid_CARRetirement_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
