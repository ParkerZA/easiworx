using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml;
using System.Drawing.Imaging;
using System.Reflection;
using QSS.Components.Windows.Forms;
using SourceGrid;
using DevAge.Windows.Forms;

using Finx.Domain.Entities;
using Finx.App.Extensions;
using Finx.App.UserControls;
using my.domain.lib.core.Extensions;
using OpenXml;
using Finx.Domain;
using my.domain.lib.core.Domain;
using Finx.App.Enums;

namespace Finx.App.Forms
{
    public partial class frmClient : Form
    {

        #region----------------Initialise parameters----------------------------------------------
        Client model = new Client(); //the model for this form
       
        ClientLockStatus ClientLockStatus = new ClientLockStatus();
        IList<ClientMeetings> clientMeetings = new List<ClientMeetings>();

        Provider ServiceProviders = new Provider();

        bool IsCalculating = false;
        bool HasChanges = false;
        bool ReadOnly = true;
        #endregion-------------------------------------------------------------------------------------

        #region Constructors
        public frmClient()
        {
            InitializeComponent();
           
        }
        public frmClient(Client Model)
        {
            InitializeComponent();
            model = Model;
        }
        #endregion

        #region frmClient Events
        private void frmClient_Load(object sender, EventArgs e)
        {
            try
            {

                this.SuspendLayout();

                ServiceProviders = (Provider)Program.Repository.List<Provider, int>(null).FirstOrDefault();

                int tabIndex = 1;
                HtmlUtils.LoadHtmlPanel(this.htmlPanel_AssetsLiabilities, "Finx.App.Html.Help_AssetsLiabilities.html");
                HtmlUtils.LoadHtmlPanel(this.htmlPanel_IncomeExpenses, "Finx.App.Html.Help_IncomeExpenses.html");

                #region ComboBox Editors

                var EmptyList = new List<ListDataItem>();

                Member_ClientTitle.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.Titles);
                Spouse_ClientTitle.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.Titles);
                Member_Gender.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.Gender);
                Spouse_Gender.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.Gender);
                Member_MaritalStatus.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.MaritalStatus);
                Spouse_MaritalStatus.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.MaritalStatus);
                Member_Language.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.Language);
                Spouse_Language.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.Language);

                #endregion

                #region Model PropertyChanged Events

                if (model.Id == 0) // A new client
                {
                    model.Status = "New";
                    model.Calculate();
                    ReadOnly = false;
                }

               // model.Calculate();

                model.PropertyChanged += Model_PropertyChanged;
                model.ModelCalculated += Model_ModelCalculated;
                model.EntityValidated += Model_ModelValidated;
                model.EntitySaved += Model_ModelSaved;

                model.ClientDetails.PropertyChanged += Model_PropertyChanged;
                model.SpouseDetails.PropertyChanged += Model_PropertyChanged;
                model.PhysicalAddress.PropertyChanged += Model_PropertyChanged;
                model.PostalAddress.PropertyChanged += Model_PropertyChanged;
                model.BankDetails.PropertyChanged += Model_PropertyChanged;
                model.ClientChecklist.PropertyChanged += Model_PropertyChanged;
                model.ClientContacts.PropertyChanged += Model_PropertyChanged;
                model.SpouseContacts.PropertyChanged += Model_PropertyChanged;
                model.ClientPortfolio.PropertyChanged += Model_PropertyChanged;
                model.ClientFna.PropertyChanged += Model_PropertyChanged;
                model.ClientFnaEducation.PropertyChanged += Model_PropertyChanged;



                #endregion

                #region MemberDetails

                Member_ClientTitle.ReadOnly = ReadOnly; Member_ClientTitle.MappedField = "ClientTitle"; Member_ClientTitle.Model = model.ClientDetails; Member_ClientTitle.TabIndex = tabIndex++;
                Member_FirstName.ReadOnly = ReadOnly; Member_FirstName.MappedField = "FirstName"; Member_FirstName.Model = model.ClientDetails; Member_FirstName.TabIndex = tabIndex++;
                Member_MidName.ReadOnly = ReadOnly; Member_MidName.MappedField = "MidName"; Member_MidName.Model = model.ClientDetails; Member_MidName.TabIndex = tabIndex++;
                Member_LastName.ReadOnly = ReadOnly; Member_LastName.MappedField = "LastName"; Member_LastName.Model = model.ClientDetails; Member_LastName.TabIndex = tabIndex++;
                Member_IdentificationNo.ReadOnly = ReadOnly; Member_IdentificationNo.MappedField = "IdentificationNo"; Member_IdentificationNo.Model = model.ClientDetails; Member_IdentificationNo.TabIndex = tabIndex++;
                Member_PassportNo.ReadOnly = ReadOnly; Member_PassportNo.MappedField = "PassportNo"; Member_PassportNo.Model = model.ClientDetails; Member_PassportNo.TabIndex = tabIndex++;
                Member_Nationality.ReadOnly = ReadOnly; Member_Nationality.MappedField = "Nationality"; Member_Nationality.Model = model.ClientDetails; Member_Nationality.TabIndex = tabIndex++;
                Member_DateOfBirth.ReadOnly = ReadOnly; Member_DateOfBirth.MappedField = "DateOfBirth"; Member_DateOfBirth.Model = model.ClientDetails; Member_DateOfBirth.TabIndex = tabIndex++;
                Member_Gender.ReadOnly = ReadOnly; Member_Gender.MappedField = "Gender"; Member_Gender.Model = model.ClientDetails; Member_Gender.TabIndex = tabIndex++;
                Member_BirthPlace.ReadOnly = ReadOnly; Member_BirthPlace.MappedField = "BirthPlace"; Member_BirthPlace.Model = model.ClientDetails; Member_BirthPlace.TabIndex = tabIndex++;
                Member_MaritalStatus.ReadOnly = ReadOnly; Member_MaritalStatus.MappedField = "MaritalStatus"; Member_MaritalStatus.Model = model.ClientDetails; Member_MaritalStatus.TabIndex = tabIndex++;
                Member_TaxNumber.ReadOnly = ReadOnly; Member_TaxNumber.MappedField = "TaxNumber"; Member_TaxNumber.Model = model.ClientDetails; Member_TaxNumber.TabIndex = tabIndex++;
                Member_Occupation.ReadOnly = ReadOnly; Member_Occupation.MappedField = "Occupation"; Member_Occupation.Model = model.ClientDetails; Member_Occupation.TabIndex = tabIndex++;
                Member_Language.ReadOnly = ReadOnly; Member_Language.MappedField = "Language"; Member_Language.Model = model.ClientDetails; Member_Language.TabIndex = tabIndex++;

                Member_Age.ReadOnly = true; Member_Age.MappedField = "Age"; Member_Age.Model = model.ClientDetails;
                #endregion

                #region SpouseDetails

                Spouse_ClientTitle.ReadOnly = ReadOnly; Spouse_ClientTitle.MappedField = "ClientTitle"; Spouse_ClientTitle.Model = model.SpouseDetails; Spouse_ClientTitle.TabIndex = tabIndex++;
                Spouse_FirstName.ReadOnly = ReadOnly; Spouse_FirstName.MappedField = "FirstName"; Spouse_FirstName.Model = model.SpouseDetails; Spouse_FirstName.TabIndex = tabIndex++;
                Spouse_MidName.ReadOnly = ReadOnly; Spouse_MidName.MappedField = "MidName"; Spouse_MidName.Model = model.SpouseDetails; Spouse_MidName.TabIndex = tabIndex++;
                Spouse_LastName.ReadOnly = ReadOnly; Spouse_LastName.MappedField = "LastName"; Spouse_LastName.Model = model.SpouseDetails; Spouse_LastName.TabIndex = tabIndex++;
                Spouse_IdentificationNo.ReadOnly = ReadOnly; Spouse_IdentificationNo.MappedField = "IdentificationNo"; Spouse_IdentificationNo.Model = model.SpouseDetails; Spouse_IdentificationNo.TabIndex = tabIndex++;
                Spouse_PassportNo.ReadOnly = ReadOnly; Spouse_PassportNo.MappedField = "PassportNo"; Spouse_PassportNo.Model = model.SpouseDetails; Spouse_PassportNo.TabIndex = tabIndex++;
                Spouse_Nationality.ReadOnly = ReadOnly; Spouse_Nationality.MappedField = "Nationality"; Spouse_Nationality.Model = model.SpouseDetails; Spouse_Nationality.TabIndex = tabIndex++;
                Spouse_DateOfBirth.ReadOnly = ReadOnly; Spouse_DateOfBirth.MappedField = "DateOfBirth"; Spouse_DateOfBirth.Model = model.SpouseDetails; Spouse_DateOfBirth.TabIndex = tabIndex++;
                Spouse_Gender.ReadOnly = ReadOnly; Spouse_Gender.MappedField = "Gender"; Spouse_Gender.Model = model.SpouseDetails; Spouse_Gender.TabIndex = tabIndex++;
                Spouse_BirthPlace.ReadOnly = ReadOnly; Spouse_BirthPlace.MappedField = "BirthPlace"; Spouse_BirthPlace.Model = model.SpouseDetails; Spouse_BirthPlace.TabIndex = tabIndex++;
                Spouse_MaritalStatus.ReadOnly = ReadOnly; Spouse_MaritalStatus.MappedField = "MaritalStatus"; Spouse_MaritalStatus.Model = model.SpouseDetails; Spouse_MaritalStatus.TabIndex = tabIndex++;
                Spouse_TaxNumber.ReadOnly = ReadOnly; Spouse_TaxNumber.MappedField = "TaxNumber"; Spouse_TaxNumber.Model = model.SpouseDetails; Spouse_TaxNumber.TabIndex = tabIndex++;
                Spouse_Occupation.ReadOnly = ReadOnly; Spouse_Occupation.MappedField = "Occupation"; Spouse_Occupation.Model = model.SpouseDetails; Spouse_Occupation.TabIndex = tabIndex++;
                Spouse_Language.ReadOnly = ReadOnly; Spouse_Language.MappedField = "Language"; Spouse_Language.Model = model.SpouseDetails; Spouse_Language.TabIndex = tabIndex++;

                Spouse_Age.ReadOnly = true; Spouse_Age.MappedField = "Age"; Spouse_Age.Model = model.SpouseDetails;
                #endregion

                #region Client Dependents

                this.dataGrid_Dependents
                .Initialise().Columns<ClientDependent>(column => {
                    column.For(c => c.DependentName, "Name",new StringEditor());
                    column.For(c => c.DependentType, "Relation", new ComboBoxEditor(ListDataItemType.DependentType));
                    column.For(c => c.BirthDate, "Birth Date", new DateEditor());
                    column.For(c => c.IdNumber, "Id Number", new StringEditor());
                    column.For(c => c.Percentage, "Will %", new DecimalEditor());
                    column.For(c => c.MedAid, "Med Aid", new CheckBoxEditor());
                    column.For(c => c._Age, "Age");
                })
                .DataSource(model.ClientDependents.Dependents, DataSource_ListChanged<ClientDependent>)
                .Format(ReadOnly);

                // this.dataGrid_Dependents.Initialise();
                //this.dataGrid_Dependents.Columns.Add("DependentName", "Name", new StringEditor()).Width = 150;
                //this.dataGrid_Dependents.Columns.Add("DependentType", "Relation", new ComboBoxEditor(ListDataItemType.DependentType)).Width = 150;
                //this.dataGrid_Dependents.Columns.Add("BirthDate", "Birth Date", new DateEditor()).Width = 150;
                //this.dataGrid_Dependents.Columns.Add("IdNumber", "Id Number", new StringEditor()).Width = 150;
                //this.dataGrid_Dependents.Columns.Add("Percentage", "Will %", new DecimalEditor()).Width = 75;
                //this.dataGrid_Dependents.Columns.Add("MedAid", "Medical Aid", new CheckBoxEditor()).Width = 75;
                //this.dataGrid_Dependents.Columns.Add("_Age", "Age", new NumericEditor(true)).Width = 75;

                //this.dataGrid_Dependents.DataSource = new DevAge.ComponentModel.BoundList<ClientDependent>(model.ClientDependents.Dependents);
                //this.dataGrid_Dependents.DataSource.ListChanged += DataSource_ListChanged<ClientDependent>;

                // this.dataGrid_Dependents.Format(ReadOnly);

                #endregion

                #region Physical Address

                xInput_ResAddrLine1.ReadOnly = ReadOnly; xInput_ResAddrLine1.Model = model.PhysicalAddress;
                xInput_ResAddrLine2.ReadOnly = ReadOnly; xInput_ResAddrLine2.Model = model.PhysicalAddress;
                xInput_ResAddrLine3.ReadOnly = ReadOnly; xInput_ResAddrLine3.Model = model.PhysicalAddress;
                xInput_ResAddrLine4.ReadOnly = ReadOnly; xInput_ResAddrLine4.Model = model.PhysicalAddress;
                xInput__ResAddrCode.ReadOnly = ReadOnly; xInput__ResAddrCode.Model = model.PhysicalAddress;

                xInput_PostAddrLine1.ReadOnly = ReadOnly; xInput_PostAddrLine1.Model = model.PostalAddress;
                xInput_PostAddrLine2.ReadOnly = ReadOnly; xInput_PostAddrLine2.Model = model.PostalAddress;
                xInput_PostAddrLine3.ReadOnly = ReadOnly; xInput_PostAddrLine3.Model = model.PostalAddress;
                xInput_PostAddrLine4.ReadOnly = ReadOnly; xInput_PostAddrLine4.Model = model.PostalAddress;
                xInput_PostAddrCode.ReadOnly = ReadOnly; xInput_PostAddrCode.Model = model.PostalAddress;
                #endregion

                #region Bank Details

                xInput_BnkName.ReadOnly = ReadOnly; xInput_BnkName.MappedField = "BnkName";
                xInput_BnkName.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.Banks);
                xInput_BrnchName.ReadOnly = ReadOnly; xInput_BrnchName.MappedField = "BrnchName";
                xInput_BrnchCode.ReadOnly = ReadOnly; xInput_BrnchCode.MappedField = "BrnchCode";
                xInput_AcctName.ReadOnly = ReadOnly; xInput_AcctName.MappedField = "AcctName";
                xInput_AcctType.ReadOnly = ReadOnly; xInput_AcctType.MappedField = "AcctType";
                xInput_AcctType.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.BnkAcctTypes);
                xInput_AcctNumber.ReadOnly = ReadOnly; xInput_AcctNumber.MappedField = "AcctNumber";

                xInput_AcctReason.ReadOnly = ReadOnly; xInput_AcctReason.MappedField = "AcctReason";
                xInput_AcctVerified.ReadOnly = ReadOnly; xInput_AcctVerified.MappedField = "AcctVerified";


                xInput_BnkName.Model = model.BankDetails;
                xInput_BrnchName.Model = model.BankDetails;
                xInput_BrnchCode.Model = model.BankDetails;
                xInput_AcctName.Model = model.BankDetails;
                xInput_AcctType.Model = model.BankDetails;
                xInput_AcctNumber.Model = model.BankDetails;
                xInput_AcctReason.Model = model.BankDetails;
                xInput_AcctVerified.Model = model.BankDetails;

                #endregion

                #region Checklist

                xInput_StatutoryNotice.ReadOnly = ReadOnly; xInput_StatutoryNotice.MappedField = "StatutoryNotice";
                xInput_RiskProfileCompleted.ReadOnly = ReadOnly; xInput_RiskProfileCompleted.MappedField = "RiskCompleted";
                xInput_RiskStatus.ReadOnly = ReadOnly; xInput_RiskStatus.MappedField = "RiskStatus";
                xInput_RiskStatus.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.RiskProfileStatus); xInput_RiskStatus.ControlTypes = UserControls.ControlTypes.ComboList;

                xInput_FicaId.ReadOnly = ReadOnly; xInput_FicaId.MappedField = "FicaId";
                xInput_FicaIncomeTax.ReadOnly = ReadOnly; xInput_FicaIncomeTax.MappedField = "FicaTaxNumber";
                xInput_FicaUtilityBill.ReadOnly = ReadOnly; xInput_FicaUtilityBill.MappedField = "FicaUtility";
                xInput_FicaVAT.ReadOnly = ReadOnly; xInput_FicaVAT.MappedField = "FicaVAT";

                xInput_WillCompleted.ReadOnly = ReadOnly; xInput_WillCompleted.MappedField = "HasWill";
                xInput_WillCreate.ReadOnly = ReadOnly; xInput_WillCreate.MappedField = "NewWill";
                xInput_WillOutstanding.ReadOnly = ReadOnly; xInput_WillOutstanding.MappedField = "HasNoWill";
                xInput_WillExecutor.ReadOnly = ReadOnly; xInput_WillExecutor.MappedField = "WillExecutor";

                xInput_NotesLA.ReadOnly = ReadOnly; xInput_NotesLA.MappedField = "NotesLA";
                xInput_NotesLife.ReadOnly = ReadOnly; xInput_NotesLife.MappedField = "NotesLife";
                xInput_NotesPreserver.ReadOnly = ReadOnly; xInput_NotesPreserver.MappedField = "NotesPF";
                xInput_NotesRA.ReadOnly = ReadOnly; xInput_NotesRA.MappedField = "NotesRA";
                xInput_NotesUnitTrusts.ReadOnly = ReadOnly; xInput_NotesUnitTrusts.MappedField = "NotesUT";
                xInput_NotesMedicalAid.ReadOnly = ReadOnly; xInput_NotesMedicalAid.MappedField = "NotesMed";
                xInput_FullAnalysis.ReadOnly = ReadOnly; xInput_FullAnalysis.MappedField = "FullAnalysis";


                xInput_StatutoryNotice.Model = model.ClientChecklist;
                xInput_RiskProfileCompleted.Model = model.ClientChecklist;
                xInput_RiskStatus.Model = model.ClientChecklist;

                xInput_FicaId.Model = model.ClientChecklist;
                xInput_FicaIncomeTax.Model = model.ClientChecklist;
                xInput_FicaUtilityBill.Model = model.ClientChecklist;
                xInput_FicaVAT.Model = model.ClientChecklist;

                xInput_WillCompleted.Model = model.ClientChecklist;
                xInput_WillCreate.Model = model.ClientChecklist;
                xInput_WillOutstanding.Model = model.ClientChecklist;
                xInput_WillExecutor.Model = model.ClientChecklist;

                xInput_NotesLA.Model = model.ClientChecklist;
                xInput_NotesLife.Model = model.ClientChecklist;
                xInput_NotesPreserver.Model = model.ClientChecklist;
                xInput_NotesRA.Model = model.ClientChecklist;
                xInput_NotesUnitTrusts.Model = model.ClientChecklist;
                xInput_NotesMedicalAid.Model = model.ClientChecklist;
                xInput_FullAnalysis.Model = model.ClientChecklist;

                xInput_InvUT.ReadOnly = ReadOnly; xInput_InvUT.MappedField = "InvUT";
                xInput_InvOffshore.ReadOnly = ReadOnly; xInput_InvOffshore.MappedField = "InvOffshore";
                xInput_InvLocal.ReadOnly = ReadOnly; xInput_InvLocal.MappedField = "InvLocal";
                xInput_InvGold.ReadOnly = ReadOnly; xInput_InvGold.MappedField = "InvGold";
                xInput_InvETF.ReadOnly = ReadOnly; xInput_InvETF.MappedField = "InvETF";

                xInput_InvUT.Model = model.ClientChecklist;
                xInput_InvOffshore.Model = model.ClientChecklist;
                xInput_InvLocal.Model = model.ClientChecklist;
                xInput_InvGold.Model = model.ClientChecklist;
                xInput_InvETF.Model = model.ClientChecklist;


                xInput_ClientStatus.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.ClientStatus); xInput_ClientStatus.ControlTypes = UserControls.ControlTypes.ComboList;
                xInput_ClientStatus.ReadOnly = ReadOnly; xInput_ClientStatus.MappedField = "Status"; xInput_ClientStatus.Model = model; xInput_ClientStatus.TabIndex = 12;
                xInput_StatusComments.ReadOnly = ReadOnly; xInput_StatusComments.ControlTypes = ControlTypes.MultiLineTextBox; xInput_StatusComments.MappedField = "StatusComments"; xInput_StatusComments.Model = model; xInput_StatusComments.TabIndex = 13;


                #endregion

                #region Client Contact Details

                ClientContacts_HomeTel.ReadOnly = ReadOnly; ClientContacts_HomeTel.Model = model.ClientContacts;
                ClientContacts_CellNo.ReadOnly = ReadOnly; ClientContacts_CellNo.Model = model.ClientContacts;
                ClientContacts_BussTel.ReadOnly = ReadOnly; ClientContacts_BussTel.Model = model.ClientContacts;
                ClientContacts_EMailAddr.ReadOnly = ReadOnly; ClientContacts_EMailAddr.Model = model.ClientContacts;
                ClientContacts_FaxNo.ReadOnly = ReadOnly; ClientContacts_FaxNo.Model = model.ClientContacts;

                btn_SyncContactDetails.Enabled = !ReadOnly;

                this.dataGrid_ClientContactDetails
                .Initialise().Columns<ContactDetail>(column => {
                    column.For(c => c.Type, "Contact Type", new ComboBoxEditor(ListDataItemType.ContactTypes));
                    column.For(c => c.Description, "Description", new StringEditor());
                    column.For(c => c.Text, "Number", new StringEditor());
                })
                .DataSource(model.ClientContacts.ContactDetails, DataSource_ListChanged<ContactDetail>)
                .Format(ReadOnly);

                //this.dataGrid_ClientContactDetails.Initialise();

                //this.dataGrid_ClientContactDetails.Columns.Add("Type", "Type", new ComboBoxEditor(ListDataItemType.ContactTypes)).Width = 200;
                //this.dataGrid_ClientContactDetails.Columns.Add("Description", "Description", new StringEditor()).Width = 275;
                //this.dataGrid_ClientContactDetails.Columns.Add("Text", "Number", new StringEditor()).Width = 250;

                //this.dataGrid_ClientContactDetails.DataSource = new DevAge.ComponentModel.BoundList<ContactDetail>(model.ClientContacts.ContactDetails);
                //this.dataGrid_ClientContactDetails.DataSource.ListChanged += DataSource_ListChanged<ContactDetail>;

                //this.dataGrid_ClientContactDetails.Format(ReadOnly);

                #endregion

                #region Spouse Contact Details

                SpouseContacts_HomeTel.ReadOnly = ReadOnly; SpouseContacts_HomeTel.Model = model.SpouseContacts;
                SpouseContacts_CellNo.ReadOnly = ReadOnly; SpouseContacts_CellNo.Model = model.SpouseContacts;
                SpouseContacts_BussTel.ReadOnly = ReadOnly; SpouseContacts_BussTel.Model = model.SpouseContacts;
                SpouseContacts_EMailAddr.ReadOnly = ReadOnly; SpouseContacts_EMailAddr.Model = model.SpouseContacts;

                btn_SyncSpouseContactDetails.Enabled = !ReadOnly;

                #endregion

                #region Client Additional Info

                this.dataGrid_AdditionalInfo
               .Initialise().Columns<AdditionalInfo>(column => {
                   column.For(c => c.InfoName, "Name", new StringEditor());
                   column.For(c => c.InfoValue, "Description", new StringEditor());
               })
               .DataSource(model.ClientAdditionalInfo.AdditionalInfos, DataSource_ListChanged<AdditionalInfo>)
               .Format(ReadOnly);

                //this.dataGrid_AdditionalInfo.Initialise();

                //this.dataGrid_AdditionalInfo.Columns.Add("InfoName", "Name", new StringEditor()).Width = 300;
                //this.dataGrid_AdditionalInfo.Columns.Add("InfoValue", "Description", new StringEditor()).Width = 300;


                //this.dataGrid_AdditionalInfo.DataSource = new DevAge.ComponentModel.BoundList<AdditionalInfo>(model.ClientAdditionalInfo.AdditionalInfos);
                //this.dataGrid_AdditionalInfo.DataSource.ListChanged += DataSource_ListChanged<AdditionalInfo>;

                //this.dataGrid_AdditionalInfo.Format(ReadOnly);

                #endregion

                #region Client Fees

                this.dataGrid_ClientFees
              .Initialise().Columns<ClientFee>(column => {
                  column.For(c => c.ManagementFee, "Plan Fee (p/m)", new CurrencyEditor());
                  column.For(c => c.InitialFee, "Initial Fee (% )", new DecimalEditor());
                  column.For(c => c.OngoingFee, "Annual Fee (% p/a)", new DecimalEditor());
                  column.For(c => c.EffectiveDate, "Effective From", new DateEditor());
              })
              .DataSource(model.ClientFees.Fees, DataSource_ListChanged<ClientFee>)
              .Format(ReadOnly);

                //this.dataGrid_ClientFees.Initialise();

                //this.dataGrid_ClientFees.Columns.Add("ManagementFee", "Plan Fee (p/m)", new CurrencyEditor()).Width = 175;
                //this.dataGrid_ClientFees.Columns.Add("InitialFee", "Initial Fee (% p/a)", new DecimalEditor()).Width = 175;
                //this.dataGrid_ClientFees.Columns.Add("OngoingFee", "Ongoing Fee (% p/a)", new DecimalEditor()).Width = 175;
                //this.dataGrid_ClientFees.Columns.Add("EffectiveDate", "Effective From", new DateEditor()).Width = 150;
                ////this.dataGrid_ClientFees.Columns.Add("ExpiryDate", "Effective To", new DateEditor()).Width = 150;

                //this.dataGrid_ClientFees.DataSource = new DevAge.ComponentModel.BoundList<ClientFee>(model.ClientFees.Fees);
                //this.dataGrid_ClientFees.DataSource.ListChanged += DataSource_ListChanged<ClientFee>;

                //this.dataGrid_ClientFees.Format(ReadOnly);//,AllowDelete:Program.User.IsAdministrator

                #endregion

                #region Client Assets and Liabilities Controls

                this.splitContainer7.FixedPanel = FixedPanel.Panel1;
                this.splitContainer7.SplitterDistance = 150;
                this.splitContainer7.Panel1MinSize = 150;

                //Assets
                this.dataGrid_Assets
             .Initialise().Columns<Asset>(column => {
                 column.For(c => c.Type, "Type", new ComboBoxEditor(ListDataItemType.AssetTypes));
                 column.For(c => c.Class, "Class", new ComboBoxEditor(ListDataItemType.AssetClass));
                 column.For(c => c.Description, "Description", new StringEditor());
                 column.For(c => c.Value, "Amount", new CurrencyEditor());
             })
             .DataSource(model.ClientAssets.Assets, DataSource_ListChanged<Asset>)
             .Format(ReadOnly);

                //this.dataGrid_Assets.Initialise();

                //this.dataGrid_Assets.Columns.Add("Type", "Type", new ComboBoxEditor(ListDataItemType.AssetTypes)).Width = 100;
                //this.dataGrid_Assets.Columns.Add("Class", "Class", new ComboBoxEditor(ListDataItemType.AssetClass)).Width = 200;
                //this.dataGrid_Assets.Columns.Add("Description", "Description", new StringEditor()).Width = 225;
                //this.dataGrid_Assets.Columns.Add("Value", "Value", new CurrencyEditor()).Width = 118;

                //this.dataGrid_Assets.DataSource = new DevAge.ComponentModel.BoundList<Asset>(model.ClientAssets.Assets);
                //this.dataGrid_Assets.DataSource.ListChanged += DataSource_ListChanged<Asset>;

                //this.dataGrid_Assets.Format(ReadOnly);

                // Liabilities

                this.dataGrid_Liabilities
            .Initialise().Columns<Liability>(column => {
                column.For(c => c.Type, "Type", new ComboBoxEditor(ListDataItemType.LiabilityTypes));
                column.For(c => c.Class, "Class", new ComboBoxEditor(ListDataItemType.LiabilityClass));
                column.For(c => c.Description, "Description", new StringEditor());
                column.For(c => c.Value, "Amount", new CurrencyEditor());
            })
            .DataSource(model.ClientLiabilities.Liabilities, DataSource_ListChanged<Liability>)
            .Format(ReadOnly);

                //this.dataGrid_Liabilities.Initialise();

                //this.dataGrid_Liabilities.Columns.Add("Type", "Type", new ComboBoxEditor(ListDataItemType.LiabilityTypes)).Width = 100;
                //this.dataGrid_Liabilities.Columns.Add("Class", "Class", new ComboBoxEditor(ListDataItemType.LiabilityClass)).Width = 200;
                //this.dataGrid_Liabilities.Columns.Add("Description", "Description", new StringEditor()).Width = 225;
                //this.dataGrid_Liabilities.Columns.Add("Value", "Value", new CurrencyEditor()).Width = 118;

                //this.dataGrid_Liabilities.DataSource = new DevAge.ComponentModel.BoundList<Liability>(model.ClientLiabilities.Liabilities);
                //this.dataGrid_Liabilities.DataSource.ListChanged += DataSource_ListChanged<Liability>;

                //this.dataGrid_Liabilities.Format(ReadOnly);

                //Totals
                xInput_TotalAssets.MappedField = "Total";
                xInput_TotalAssets.Model = model.ClientAssets;
                xInput_CurrentPortfolioAssets.MappedField = "TotalAssets";
                xInput_CurrentPortfolioAssets.Model = model.ClientPortfolio;
                xInput_TotalLiabilities.MappedField = "Total";
                xInput_TotalLiabilities.Model = model.ClientLiabilities;
                xInput_ClientNettWorth.MappedField = "ClientNettWorth";
                xInput_ClientNettWorth.Model = model;

                #endregion

                #region Client Income and Expenses Controls

                this.splitContainer8.FixedPanel = FixedPanel.Panel1;
                this.splitContainer8.SplitterDistance = 150;
                this.splitContainer8.Panel1MinSize = 150;

                //Incomes
                this.dataGrid_Income.Initialise();

                this.dataGrid_Income.Columns.Add("Type", "Type", new ComboBoxEditor(ListDataItemType.IncomeTypes)).Width = 150;
                this.dataGrid_Income.Columns.Add("Class", "Frequency", new ComboBoxEditor(ListDataItemType.IncomeClass)).Width = 100;
                this.dataGrid_Income.Columns.Add("Description", "Description", new StringEditor()).Width = 225;
                this.dataGrid_Income.Columns.Add("Value", "Value", new CurrencyEditor()).Width = 118;

                this.dataGrid_Income.DataSource = new DevAge.ComponentModel.BoundList<Income>(model.ClientIncomes.Incomes);
                this.dataGrid_Income.DataSource.ListChanged += DataSource_ListChanged<Income>;

                this.dataGrid_Income.Format(ReadOnly);

                // Expenses

                this.dataGrid_Expense.Initialise();

                this.dataGrid_Expense.Columns.Add("Type", "Type", new ComboBoxEditor(ListDataItemType.ExpenseTypes)).Width = 150;
                this.dataGrid_Expense.Columns.Add("Class", "Frequency", new ComboBoxEditor(ListDataItemType.ExpenseClass)).Width = 100;
                this.dataGrid_Expense.Columns.Add("Description", "Description", new StringEditor()).Width = 225;
                this.dataGrid_Expense.Columns.Add("Value", "Value", new CurrencyEditor()).Width = 118;

                this.dataGrid_Expense.DataSource = new DevAge.ComponentModel.BoundList<Expense>(model.ClientExpenses.Expenses);
                this.dataGrid_Expense.DataSource.ListChanged += DataSource_ListChanged<Expense>;

                this.dataGrid_Expense.Format(ReadOnly);

                //Totals
                xInput_TotalIncome.MappedField = "Total";
                xInput_TotalIncome.Model = model.ClientIncomes;
                xInput_TotalExpenses.MappedField = "Total";
                xInput_TotalExpenses.Model = model.ClientExpenses;
                xInput_PortfolioExpenses.MappedField = "TotalPremium";
                xInput_PortfolioExpenses.Model = model.ClientPortfolio;
                xInput_AvailableCash.MappedField = "AvailableCash";
                xInput_AvailableCash.Model = model;

                #endregion

                #region Client Portfolio

                #region Graph Series

                //this.reportViewer_CurrentPortfolio.ReportRefresh += ReportViewer_CurrentPortfolio_ReportRefresh;
                //this.reportViewer_CurrentPortfolio.Resize += ReportViewer_Resize;

                RefreshClientPortfolio();

                #endregion

                #region Mapped Fields
                xInput_ClientAge_Portfolio.ReadOnly = true;
                xInput_ClientAge_Portfolio.MappedField = "Age";
                xInput_ClientAge_Portfolio.ControlTypes = UserControls.ControlTypes.IntBox;
                xInput_ClientAge_Portfolio.Model = model.ClientDetails;

                xInput_RetirementAge_Portfolio.ReadOnly = ReadOnly;
                xInput_RetirementAge_Portfolio.MappedField = "RetirementAge";
                xInput_RetirementAge_Portfolio.ControlTypes = UserControls.ControlTypes.IntBox;
                xInput_RetirementAge_Portfolio.Model = model.ClientDetails;

                xInput_InflationRate_Portfolio.ReadOnly = ReadOnly;
                xInput_InflationRate_Portfolio.MappedField = "InflationPercentage";
                //xInput_InflationRate.ControlTypes = UserControls.ControlTypes.PercentBox;
                xInput_InflationRate_Portfolio.Model = model.ClientFna;

                hScrollBar_RetirementAge_Portfolio.Enabled = !ReadOnly;
                hScrollBar_RetirementAge_Portfolio.Minimum = 55;
                hScrollBar_RetirementAge_Portfolio.Maximum = 110;
                hScrollBar_RetirementAge_Portfolio.Value = model.ClientDetails.RetirementAge == 0 ? 55 : model.ClientDetails.RetirementAge;
                hScrollBar_RetirementAge_Portfolio.Scroll += hScrollBar_RetirementAge_Scroll;

                hScrollBar_InflationRate_Portfolio.Enabled = !ReadOnly;
                hScrollBar_InflationRate_Portfolio.Minimum = 0;
                hScrollBar_InflationRate_Portfolio.Maximum = 100;
                hScrollBar_InflationRate_Portfolio.Value = (int)model.ClientFna.InflationPercentage;
                hScrollBar_InflationRate_Portfolio.Scroll += hScrollBar_InflationRate_Scroll;

                //Retirement Investments
                portf_TotRet.ReadOnly = true;
                portf_TotRet.MappedField = "_TotalRetirement";
                portf_TotRet.ControlTypes = UserControls.ControlTypes.AmountBox;
                portf_TotRet.Model = model.ClientPortfolio;

                portf_TotRetPrem.ReadOnly = true;
                portf_TotRetPrem.MappedField = "_TotalRetirementPremium";
                portf_TotRetPrem.ControlTypes = UserControls.ControlTypes.AmountBox;
                portf_TotRetPrem.Model = model.ClientPortfolio;

                //Non Retirement Investments
                portf_TotInv.ReadOnly = true;
                portf_TotInv.MappedField = "_TotalInv";
                portf_TotInv.ControlTypes = UserControls.ControlTypes.AmountBox;
                portf_TotInv.Model = model.ClientPortfolio;

                portf_TotInvPrem.ReadOnly = true;
                portf_TotInvPrem.MappedField = "_TotalInvPremium";
                portf_TotInvPrem.ControlTypes = UserControls.ControlTypes.AmountBox;
                portf_TotInvPrem.Model = model.ClientPortfolio;

                //Non Medical Insurance
                portf_TotMed.ReadOnly = true;
                portf_TotMed.MappedField = "_TotalMedical";
                portf_TotMed.ControlTypes = UserControls.ControlTypes.AmountBox;
                portf_TotMed.Model = model.ClientPortfolio;

                portf_TotMedPrem.ReadOnly = true;
                portf_TotMedPrem.MappedField = "_TotalMedicalPremium";
                portf_TotMedPrem.ControlTypes = UserControls.ControlTypes.AmountBox;
                portf_TotMedPrem.Model = model.ClientPortfolio;

                //Non Life & Disability Insurance
                portf_TotLife.ReadOnly = true;
                portf_TotLife.MappedField = "_TotalLife";
                portf_TotLife.ControlTypes = UserControls.ControlTypes.AmountBox;
                portf_TotLife.Model = model.ClientPortfolio;

                portf_TotLifePrem.ReadOnly = true;
                portf_TotLifePrem.MappedField = "_TotalLifePremium";
                portf_TotLifePrem.ControlTypes = UserControls.ControlTypes.AmountBox;
                portf_TotLifePrem.Model = model.ClientPortfolio;

                //Total Education Investments
                portf_TotEdu.ReadOnly = true;
                portf_TotEdu.MappedField = "_TotalEdu";
                portf_TotEdu.ControlTypes = UserControls.ControlTypes.AmountBox;
                portf_TotEdu.Model = model.ClientPortfolio;

                portf_TotEduPrem.ReadOnly = true;
                portf_TotEduPrem.MappedField = "_TotalEduPremium";
                portf_TotEduPrem.ControlTypes = UserControls.ControlTypes.AmountBox;
                portf_TotEduPrem.Model = model.ClientPortfolio;

                //TotalIncome Generating Assets
                //portf_TotInv.ReadOnly = true;
                //portf_TotInv.MappedField = "_TotalInv";
                //portf_TotInv.ControlTypes = UserControls.ControlTypes.AmountBox;
                //portf_TotInv.Model = model.ClientPortfolio;

                //portf_TotInvPrem.ReadOnly = true;
                //portf_TotInvPrem.MappedField = "_TotalInvPrem";
                //portf_TotInvPrem.ControlTypes = UserControls.ControlTypes.AmountBox;
                //portf_TotInvPrem.Model = model.ClientPortfolio;

                #endregion

                #region Retirement Portfolio

                //Retirement Portfolio
                this.dataGrid_RetirementPortfolio.Initialise();

                this.dataGrid_RetirementPortfolio.Columns.Add("Type", "Investment Type", new ComboBoxEditor(ListDataItemType.RetirementAssetClass)).Width = 200;

                var Lisps = new List<ListDataItem>();
                if (ServiceProviders != null)
                    foreach (var lisp in ServiceProviders.LispProviders.Lisps)
                        Lisps.Add(new ListDataItem(ListDataItemType.Lisp, lisp.LispName, lisp.LispName));


                ComboBoxEditor cboRetirementLisps = new ComboBoxEditor(Lisps);
                cboRetirementLisps.Control.SelectedValueChanged += cboRetirementLisps_SelectedValueChanged;

                this.dataGrid_RetirementPortfolio.Columns.Add("Description", "LISP", cboRetirementLisps).Width = 250;
                this.dataGrid_RetirementPortfolio.Columns.Add("ReferenceNo", "Policy No.", new StringEditor()).Width = 150;
                this.dataGrid_RetirementPortfolio.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor()).Width = 165;
                this.dataGrid_RetirementPortfolio.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor()).Width = 135;
                this.dataGrid_RetirementPortfolio.Columns.Add("EscalationPercentage", "Escalate (%p/a)", new DecimalEditor()).Width = 65;
                this.dataGrid_RetirementPortfolio.Columns.Add("GrowthPercentage", "Growth (%p/a)", new DecimalEditor()).Width = 65;
                this.dataGrid_RetirementPortfolio.Columns.Add("CurrentAmount", "Total Current Value", new CurrencyEditor(true)).Width = 165;
                this.dataGrid_RetirementPortfolio.Columns.Add("FutureAmount", "Retirement Value", new CurrencyEditor(true)).Width = 165;
                //this.dataGrid_RetirementPortfolio.Columns.Add("Status", "Status", new StringEditor(true)).Width = 200;
                if (!ReadOnly)
                {
                    //  Funds click event
                    SourceGrid.Cells.Controllers.CustomEvents clickEvent2 = new SourceGrid.Cells.Controllers.CustomEvents();
                    clickEvent2.Click += CurrentPortfolio_Retirement_Click;
                   
                    var btnFunds = new SourceGrid.Cells.RowHeader("");
                    btnFunds.Image = Properties.Resources.save;
                    btnFunds.ToolTipText = "Click to add Funds";
                    btnFunds.AddController(clickEvent2);

                    var col = dataGrid_RetirementPortfolio.Columns.Add("", "", btnFunds);
                    col.Width = 22;
                    col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
                    col.MaximalWidth = 22;
                    col.MinimalWidth = 22;

                }


                this.dataGrid_RetirementPortfolio.DataSource = new DevAge.ComponentModel.BoundList<Retirement>(model.ClientPortfolio.Retirements);
                this.dataGrid_RetirementPortfolio.DataSource.ListChanged += DataSource_ListChanged<Retirement>;

                //  row header click event
                SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
                //clickEvent.Click += RetirementPortfolio_Click;
                clickEvent.FocusEntered += RetirementPortfolio_Click;
                this.dataGrid_RetirementPortfolio.Controller.AddController(clickEvent); //Event fired when any column is clicked

                this.dataGrid_RetirementPortfolio.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator

                //Retirement Funds
                this.dataGrid_RetirementFunds.Initialise();

                this.dataGrid_RetirementFunds.Columns.Add("Description", "Fund Name", new StringEditor()).Width = 250;
                this.dataGrid_RetirementFunds.Columns.Add("StartDate", "Inception Date", new DateEditor()).Width = 200;
                this.dataGrid_RetirementFunds.Columns.Add("SplitPerc", " % Premium Split", new DecimalEditor()).Width = 145;
                this.dataGrid_RetirementFunds.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor()).Width = 165;
                this.dataGrid_RetirementFunds.Columns.Add("CurrentAmount", "Current Value", new CurrencyEditor()).Width = 200;
                this.dataGrid_RetirementFunds.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor(true)).Width = 120;
                this.dataGrid_RetirementFunds.Columns.Add("Type", "Fund Class", new StringEditor(true)).Width = 195;
                this.dataGrid_RetirementFunds.Columns.Add("Risk", "Risk Category", new StringEditor(true)).Width = 150;
                this.dataGrid_RetirementFunds.Columns.Add("IRR", "IRR", new DecimalEditor(true)).Width = 100;

                ShowRetirementFunds(0);

                this.dataGrid_RetirementFunds.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator

                #endregion

                #region Investment Portfolio

                this.dataGrid_InvestmentsPortfolio.Initialise();

                ComboBoxEditor cboInvestmentLisps = new ComboBoxEditor(Lisps);
                cboInvestmentLisps.Control.SelectedValueChanged += cboInvestmentLisps_SelectedValueChanged;

                this.dataGrid_InvestmentsPortfolio.Columns.Add("Type", "Investment Type", new ComboBoxEditor(ListDataItemType.InvestmentClass)).Width = 150;
                this.dataGrid_InvestmentsPortfolio.Columns.Add("Description", "LISP", cboInvestmentLisps).Width = 175;
                this.dataGrid_InvestmentsPortfolio.Columns.Add("ReferenceNo", "Policy No.", new StringEditor()).Width = 150;
                this.dataGrid_InvestmentsPortfolio.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor()).Width = 165;
                this.dataGrid_InvestmentsPortfolio.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor()).Width = 150;
                this.dataGrid_InvestmentsPortfolio.Columns.Add("EscalationPercentage", "Escalate (%p/a)", new DecimalEditor()).Width = 65;
                this.dataGrid_InvestmentsPortfolio.Columns.Add("GrowthPercentage", "Growth (%p/a)", new DecimalEditor()).Width = 150;
                this.dataGrid_InvestmentsPortfolio.Columns.Add("CurrentAmount", "Current Value", new CurrencyEditor()).Width = 150;
                this.dataGrid_InvestmentsPortfolio.Columns.Add("InvestmentAge", "Invest Age", new NumericEditor()).Width = 100;
                this.dataGrid_InvestmentsPortfolio.Columns.Add("FutureAmount", "Projected Value", new CurrencyEditor(true)).Width = 150;
                // this.dataGrid_InvestmentsPortfolio.Columns.Add("FutureAmountInflationAdj", "Inflation Adj Value", new CurrencyEditor(true)).Width = 150;
                //this.dataGrid_InvestmentsPortfolio.AddColumnButton(CurrentPortfolio_Investments_Click, ReadOnly);
                this.dataGrid_InvestmentsPortfolio.AddRowClickEvent(InvestmentPortfolio_Click);
                this.dataGrid_InvestmentsPortfolio.DataSource = new DevAge.ComponentModel.BoundList<Investment>(model.ClientPortfolio.Investments);
                this.dataGrid_InvestmentsPortfolio.DataSource.ListChanged += DataSource_ListChanged<Investment>;
                this.dataGrid_InvestmentsPortfolio.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator

                //Investment Funds
                this.dataGrid_InvestmentsPortfolioFunds.Initialise();

                this.dataGrid_InvestmentsPortfolioFunds.Columns.Add("Description", "Fund Name", new StringEditor()).Width = 250;
                this.dataGrid_InvestmentsPortfolioFunds.Columns.Add("StartDate", "Inception Date", new DateEditor()).Width = 200;
                this.dataGrid_InvestmentsPortfolioFunds.Columns.Add("SplitPerc", " % Premium Split", new DecimalEditor()).Width = 145;
                this.dataGrid_InvestmentsPortfolioFunds.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor()).Width = 165;
                this.dataGrid_InvestmentsPortfolioFunds.Columns.Add("CurrentAmount", "Current Value", new CurrencyEditor()).Width = 200;
                this.dataGrid_InvestmentsPortfolioFunds.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor(true)).Width = 120;
                this.dataGrid_InvestmentsPortfolioFunds.Columns.Add("Type", "Fund Class", new StringEditor(true)).Width = 195;
                this.dataGrid_InvestmentsPortfolioFunds.Columns.Add("Risk", "Risk Category", new StringEditor(true)).Width = 150;
                this.dataGrid_InvestmentsPortfolioFunds.Columns.Add("IRR", "IRR", new DecimalEditor(true)).Width = 100;
                this.dataGrid_InvestmentsPortfolioFunds.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator

                ShowInvestmentFunds(0);

                #endregion

                #region Education Portfolio

                this.dataGrid_EducationPortfolio.Initialise();

                ComboBoxEditor cboEducationLisps = new ComboBoxEditor(Lisps);
                cboEducationLisps.Control.SelectedValueChanged += cboEducationLisps_SelectedValueChanged;

                this.dataGrid_EducationPortfolio.Columns.Add("DependentName", "Name", new StringEditor()).Width = 150;
                this.dataGrid_EducationPortfolio.Columns.Add("DependentDOB", "Birth Date", new DateEditor()).Width = 150;
                this.dataGrid_EducationPortfolio.Columns.Add("Description", "LISP", cboEducationLisps).Width = 175;
                this.dataGrid_EducationPortfolio.Columns.Add("ReferenceNo", "Policy No.", new StringEditor()).Width = 150;
                this.dataGrid_EducationPortfolio.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor()).Width = 150;
                this.dataGrid_EducationPortfolio.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor()).Width = 150;
                this.dataGrid_EducationPortfolio.Columns.Add("EscalationPercentage", "Escalate (%p/a)", new DecimalEditor()).Width = 65;
                this.dataGrid_EducationPortfolio.Columns.Add("GrowthPercentage", "Growth (%p/a)", new DecimalEditor()).Width = 150;

                this.dataGrid_EducationPortfolio.Columns.Add("InvestmentAge", "University Age", new NumericEditor()).Width = 100;
                this.dataGrid_EducationPortfolio.Columns.Add("CurrentAmount", "Current Value", new CurrencyEditor()).Width = 150;
                this.dataGrid_EducationPortfolio.Columns.Add("FutureAmount", "Projected Value", new CurrencyEditor(true)).Width = 150;

                this.dataGrid_EducationPortfolio.DataSource = new DevAge.ComponentModel.BoundList<Education>(model.ClientPortfolio.Educations);
                this.dataGrid_EducationPortfolio.DataSource.ListChanged += DataSource_ListChanged<Education>;

                //  row header click event
                SourceGrid.Cells.Controllers.CustomEvents EducationPortfolioClickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
                EducationPortfolioClickEvent.FocusEntered += EducationPortfolio_Click;
                this.dataGrid_EducationPortfolio.Controller.AddController(EducationPortfolioClickEvent); //Event fired when any column is clicked

                this.dataGrid_EducationPortfolio.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator

                //Retirement Funds
                this.dataGrid_EducationPortfolioFunds.Initialise();

                this.dataGrid_EducationPortfolioFunds.Columns.Add("Description", "Fund Name", new StringEditor()).Width = 250;
                this.dataGrid_EducationPortfolioFunds.Columns.Add("SplitPerc", " % Premium Split", new DecimalEditor()).Width = 145;
                this.dataGrid_EducationPortfolioFunds.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor()).Width = 200;
                this.dataGrid_EducationPortfolioFunds.Columns.Add("CurrentAmount", "Current Value", new CurrencyEditor()).Width = 200;
                this.dataGrid_EducationPortfolioFunds.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor(true)).Width = 120;
                this.dataGrid_EducationPortfolioFunds.Columns.Add("Type", "Fund Class", new StringEditor(true)).Width = 195;
                this.dataGrid_EducationPortfolioFunds.Columns.Add("Risk", "Risk Category", new StringEditor(true)).Width = 150;
                this.dataGrid_EducationPortfolioFunds.Columns.Add("IRR", "IRR", new DecimalEditor(true)).Width = 100;

                ShowEducationFunds(0);

                this.dataGrid_EducationPortfolioFunds.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator
                #endregion

                #region Medical Portfolio
                this.dataGrid_MedicalPortfolio.Initialise();

                var MedicalAids = new List<ListDataItem>();
                if (ServiceProviders != null)
                {
                    foreach (var lisp in ServiceProviders.MedicalProviders.MedicalAids)
                        MedicalAids.Add(new ListDataItem(ListDataItemType.Lisp, lisp.ProviderName, lisp.ProviderName));
                }

                ComboBoxEditor cboMedicalAids = new ComboBoxEditor(MedicalAids);
                cboMedicalAids.Control.SelectedValueChanged += cboMedicalAids_SelectedValueChanged;

                this.dataGrid_MedicalPortfolio.Columns.Add("Type", "Medical Aid", cboMedicalAids).Width = 250;
                this.dataGrid_MedicalPortfolio.Columns.Add("Description", "Plan", new ComboBoxEditor(EmptyList)).Width = 175;
                this.dataGrid_MedicalPortfolio.Columns.Add("ReferenceNo", "Medical Aid No.", new StringEditor()).Width = 175;
                this.dataGrid_MedicalPortfolio.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor()).Width = 120;
                this.dataGrid_MedicalPortfolio.Columns.Add("InitialAmount", "Total Cover Amount", new CurrencyEditor()).Width = 150;

                this.dataGrid_MedicalPortfolio.DataSource = new DevAge.ComponentModel.BoundList<Medical>(model.ClientPortfolio.Medicals);
                this.dataGrid_MedicalPortfolio.DataSource.ListChanged += DataSource_ListChanged<Medical>;

                //  row header click event
                SourceGrid.Cells.Controllers.CustomEvents clickEventMedicalPortfolio = new SourceGrid.Cells.Controllers.CustomEvents();
                clickEventMedicalPortfolio.FocusEntered += MedicalPortfolio_Click;
                this.dataGrid_MedicalPortfolio.Controller.AddController(clickEventMedicalPortfolio); //Event fired when any column is clicked

                this.dataGrid_MedicalPortfolio.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator

                //Medical Benefits
                this.dataGrid_MedicalBenefits.Initialise();

                this.dataGrid_MedicalBenefits.Columns.Add("Type", "Benefit Type", new ComboBoxEditor(ListDataItemType.MedicalBenefit)).Width = 250;
                this.dataGrid_MedicalBenefits.Columns.Add("Description", "Description", new StringEditor()).Width = 250;
                this.dataGrid_MedicalBenefits.Columns.Add("CoverAmount", "Cover Amount", new CurrencyEditor()).Width = 200;
                // this.dataGrid_MedicalBenefits.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor(true)).Width = 120;

                ShowMedicalBenefits(0);

                this.dataGrid_MedicalBenefits.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator

                #endregion

                #region Life Portfolio
                this.dataGrid_LifePortfolio.Initialise();

                var LifeInsurers = new List<ListDataItem>();
                if (ServiceProviders != null)
                {
                    foreach (var lisp in ServiceProviders.LifeProviders.LifeInsurers)
                        LifeInsurers.Add(new ListDataItem(ListDataItemType.Lisp, lisp.InsurerName, lisp.InsurerName));
                }

                ComboBoxEditor cboLifeInsurers = new ComboBoxEditor(LifeInsurers);
                cboLifeInsurers.Control.SelectedValueChanged += cboLifeInsurers_SelectedValueChanged;

                this.dataGrid_LifePortfolio.Columns.Add("Type", " Insurer", cboLifeInsurers).Width = 250;
                this.dataGrid_LifePortfolio.Columns.Add("Description", "Policy", new ComboBoxEditor(EmptyList)).Width = 175;
                this.dataGrid_LifePortfolio.Columns.Add("ReferenceNo", "Policy No.", new StringEditor()).Width = 175;
                this.dataGrid_LifePortfolio.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor()).Width = 120;
                this.dataGrid_LifePortfolio.Columns.Add("InitialAmount", "Insured Amount", new CurrencyEditor()).Width = 150;

                this.dataGrid_LifePortfolio.DataSource = new DevAge.ComponentModel.BoundList<Life>(model.ClientPortfolio.Lifes);
                this.dataGrid_LifePortfolio.DataSource.ListChanged += DataSource_ListChanged<Life>;

                //  row header click event
                SourceGrid.Cells.Controllers.CustomEvents clickEventLifePortfolio = new SourceGrid.Cells.Controllers.CustomEvents();
                //clickEventMedicalPortfolio.Click += MedicalPortfolio_Click;
                clickEventLifePortfolio.FocusEntered += LifePortfolio_Click;
                this.dataGrid_LifePortfolio.Controller.AddController(clickEventLifePortfolio); //Event fired when any column is clicked

                this.dataGrid_LifePortfolio.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator

                //Life Benefits
                this.dataGrid_LifeBenefits.Initialise();

                this.dataGrid_LifeBenefits.Columns.Add("Type", "Benefit Type", new ComboBoxEditor(ListDataItemType.LifeBenefit)).Width = 250;
                this.dataGrid_LifeBenefits.Columns.Add("Description", "Description", new StringEditor()).Width = 250;
                this.dataGrid_LifeBenefits.Columns.Add("CoverAmount", "Insured Amount", new CurrencyEditor()).Width = 200;
                // this.dataGrid_LifeBenefits.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor(true)).Width = 120;

                ShowLifeBenefits(0);

                this.dataGrid_LifeBenefits.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator

                #endregion

                #region Estate Portfolio

                //this.dataGrid_EstatePortfolio.Initialise();

                //this.dataGrid_EstatePortfolio.Columns.Add("Type", "Type",new ComboBoxEditor( ListDataItemType.RetirementAssetClass)).Width = 150;
                //this.dataGrid_EstatePortfolio.Columns.Add("Description", "Description",new StringEditor()).Width = 175;
                //this.dataGrid_EstatePortfolio.Columns.Add("InitialAmount", "Value",new CurrencyEditor()).Width = 150;
                //this.dataGrid_EstatePortfolio.Columns.Add("MonthlyContribution", "Monthly",new CurrencyEditor()).Width = 150;
                //this.dataGrid_EstatePortfolio.Columns.Add("GrowthPercentage", "% Growth (p/a)",new DecimalEditor()).Width = 150;
                //this.dataGrid_EstatePortfolio.Columns.Add("FutureAmount", "Projected Value",new CurrencyEditor(true)).Width = 200;

                //this.dataGrid_EstatePortfolio.DataSource = new DevAge.ComponentModel.BoundList<Estate>(model.ClientPortfolio.Estates);
                //this.dataGrid_EstatePortfolio.DataSource.ListChanged += DataSource_ListChanged<Estate>;

                //this.dataGrid_EstatePortfolio.Format(ReadOnly);

                #endregion

                #region Income Generation Assets Portfolio

                this.dataGrid_IncomeAssets.Initialise();

                this.dataGrid_IncomeAssets.Columns.Add("Type", "Type", new ComboBoxEditor(ListDataItemType.InvestmentClass)).Width = 100;
                this.dataGrid_IncomeAssets.Columns.Add("Description", "Description", new StringEditor()).Width = 175;
                //this.dataGrid_IncomeAssets.Columns.Add("ReferenceNo", "Policy No.", new StringEditor()).Width = 150;
                this.dataGrid_IncomeAssets.Columns.Add("InitialAmount", "Asset Value", new CurrencyEditor()).Width = 150;
                this.dataGrid_IncomeAssets.Columns.Add("GrowthPercentage", "Growth (%p/a)", new DecimalEditor()).Width = 100;
                this.dataGrid_IncomeAssets.Columns.Add("MonthlyContribution", "Income (p/m)", new CurrencyEditor()).Width = 150;
                this.dataGrid_IncomeAssets.Columns.Add("EscalationPercentage", "Increase (%p/a)", new DecimalEditor()).Width = 65;


                this.dataGrid_IncomeAssets.Columns.Add("InvestmentAge", "Invest Age", new NumericEditor()).Width = 100;

                this.dataGrid_IncomeAssets.Columns.Add("FutureAmount", "Future Value", new CurrencyEditor(true)).Width = 150;
                this.dataGrid_IncomeAssets.Columns.Add("FutureIncome", "Projected Income", new CurrencyEditor(true)).Width = 150;

                this.dataGrid_IncomeAssets.DataSource = new DevAge.ComponentModel.BoundList<IncomeAsset>(model.ClientPortfolio.IncomeAssets);
                this.dataGrid_IncomeAssets.DataSource.ListChanged += DataSource_ListChanged<IncomeAsset>;

                //  row header click event
                SourceGrid.Cells.Controllers.CustomEvents dataGrid_IncomeAssetsClickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
                // dataGrid_IncomeAssetsClickEvent.FocusEntered += InvestmentPortfolio_Click;
                this.dataGrid_IncomeAssets.Controller.AddController(dataGrid_IncomeAssetsClickEvent); //Event fired when any column is clicked

                this.dataGrid_IncomeAssets.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator

                //Income Generation Assets Funds
                //this.dataGrid_IncomeAssetsFunds.Initialise();

                //this.dataGrid_IncomeAssetsFunds.Columns.Add("Description", "Fund Name", new StringEditor()).Width = 250;
                //this.dataGrid_IncomeAssetsFunds.Columns.Add("SplitPerc", " % Premium Split", new DecimalEditor()).Width = 145;
                //this.dataGrid_IncomeAssetsFunds.Columns.Add("InitialAmount", "Current Value", new CurrencyEditor()).Width = 200;
                //this.dataGrid_IncomeAssetsFunds.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor(true)).Width = 120;
                //this.dataGrid_IncomeAssetsFunds.Columns.Add("Type", "Fund Class", new StringEditor(true)).Width = 195;
                //this.dataGrid_IncomeAssetsFunds.Columns.Add("GrowthPercentage", "% IRR (p/a)", new DecimalEditor(true)).Width = 100;
                //this.dataGrid_IncomeAssetsFunds.Columns.Add("_Risk", "Risk Category", new StringEditor(true)).Width = 150;

                //ShowIncomeAssetsFunds(0);

                //this.dataGrid_IncomeAssetsFunds.Format(ReadOnly);
                #endregion

                #endregion

                #region Retirement FNA

                #region Wants
                this.dataGrid_Wants.Initialise(ReadOnly);


                this.dataGrid_Wants.Columns.Add("Description", "Description", new StringEditor()).Width = 175;
                this.dataGrid_Wants.Columns.Add("Type", "Frequency", new ComboBoxEditor(ListDataItemType.RetirementWantsClass)).Width = 100;
                this.dataGrid_Wants.Columns.Add("InitialAmount", "Present Value", new CurrencyEditor()).Width = 120;
                //this.dataGrid_Wants.Columns.Add("InvestmentAge", "Age",new NumericEditor(true)).Width = 50;
                this.dataGrid_Wants.Columns.Add("FutureAmount", "Future Value", new CurrencyEditor(true)).Width = 120;
                this.dataGrid_Wants.Columns.Add("GrowthPercentage", "Future Grwth (%p/a)", new DecimalEditor()).Width = 150;
                this.dataGrid_Wants.Columns.Add("EscalationPercentage", "Future Incr. (%p/a)", new DecimalEditor()).Width = 150;
                this.dataGrid_Wants.Columns.Add("_RequiredAmount", "Required Investment", new CurrencyEditor(true)).Width = 200;

                this.dataGrid_Wants.DataSource = new DevAge.ComponentModel.BoundList<Want>(model.ClientFna.Wants);
                this.dataGrid_Wants.DataSource.ListChanged += DataSource_ListChanged<Want>;

                this.dataGrid_Wants.Format(ReadOnly);

                #endregion

                #region Haves
                this.dataGrid_Haves.Initialise(ReadOnly);

                //this.dataGrid_Haves.Columns.Add("Type", "Type", new ComboBoxEditor(ListDataItemType.RetirementAssetClass)).Width = 150;
                this.dataGrid_Haves.Columns.Add("Type", "Type", new StringEditor(true)).Width = 200;
                this.dataGrid_Haves.Columns.Add("Description", "LISP", new StringEditor(true)).Width = 175;
                this.dataGrid_Haves.Columns.Add("CurrentAmount", "Current Value", new CurrencyEditor(true)).Width = 150;
                this.dataGrid_Haves.Columns.Add("MonthlyContribution", "Monthly", new CurrencyEditor(true)).Width = 100;
                this.dataGrid_Haves.Columns.Add("GrowthPercentage", "% Growth (p/a)", new DecimalEditor(true)).Width = 100;
                this.dataGrid_Haves.Columns.Add("EscalationPercentage", "% Escalation (p/a)", new DecimalEditor(true)).Width = 100;
                this.dataGrid_Haves.Columns.Add("FutureAmount", "Retirement Value", new CurrencyEditor(true)).Width = 200;

                this.dataGrid_Haves.DataSource = new DevAge.ComponentModel.BoundList<Have>(model.ClientFna.Haves);
                this.dataGrid_Haves.DataSource.ListChanged += DataSource_ListChanged<Have>;

                this.dataGrid_Haves.Format(ReadOnly);

                #endregion

                #region Needs
                this.dataGrid_Needs.Initialise(ReadOnly);

                this.dataGrid_Needs.Columns.Add("Type", "Type", new ComboBoxEditor(ListDataItemType.InvestmentType)).Width = 150;

                ComboBoxEditor cboLisps2 = new ComboBoxEditor(Lisps);
                //cboLisps2.Control.SelectedValueChanged += cboLisps_SelectedValueChanged;

                this.dataGrid_Needs.Columns.Add("Description", "LISP", cboLisps2).Width = 250;
                this.dataGrid_Needs.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor()).Width = 150;
                this.dataGrid_Needs.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor()).Width = 135;
                this.dataGrid_Needs.Columns.Add("EscalationPercentage", "Escalate (%p/a)", new DecimalEditor()).Width = 100;
                this.dataGrid_Needs.Columns.Add("GrowthPercentage", "Growth (%p/a)", new DecimalEditor()).Width = 100;
                //this.dataGrid_Needs.Columns.Add("InvestmentYears", "Term (yr)", new DecimalEditor(true)).Width = 75;
                this.dataGrid_Needs.Columns.Add("FutureAmount", "Future Amount", new CurrencyEditor(true)).Width = 150;
                //this.dataGrid_Needs.Columns.Add("ReferenceNo", "Ref", new StringEditor(true)).Width = 150;
                this.dataGrid_Needs.Columns.Add("Status", "Status", new StringEditor(true)).Width = 150;
                this.dataGrid_Needs.Columns.Add("UpdateDate", "Date Updated", new DateEditor(true)).Width = 150;
                if (!ReadOnly)
                {
                    //  Funds click event
                    SourceGrid.Cells.Controllers.CustomEvents clickEvent2 = new SourceGrid.Cells.Controllers.CustomEvents();
                    clickEvent2.Click += FNANeeds_Click;

                    var btnFunds = new SourceGrid.Cells.RowHeader("");
                    btnFunds.Image = Properties.Resources.save;
                    btnFunds.ToolTipText = "Click to add Funds";
                    btnFunds.AddController(clickEvent2);

                    var col = dataGrid_Needs.Columns.Add("", "", btnFunds);
                    col.Width = 22;
                    col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
                    col.MaximalWidth = 22;
                    col.MinimalWidth = 22;

                }

                this.dataGrid_Needs.DataSource = new DevAge.ComponentModel.BoundList<Need>(model.ClientFna.Needs);//.Where(a => a.IsImplemented == false).ToList()
                this.dataGrid_Needs.DataSource.ListChanged += DataSource_ListChanged<Need>;

                this.dataGrid_Needs.Format(ReadOnly);//, AllowDelete: Program.User.IsAdministrator
                #endregion

                #region FNA Notes
                this.dataGrid_RetirementNotes.Initialise(ReadOnly);

                this.dataGrid_RetirementNotes.Columns.Add("DateStart", "Date", new DateEditor(true)).Width = 100;
                this.dataGrid_RetirementNotes.Columns.Add("Text", "Note", new MultiLineEditor()).Width = dataGrid_RetirementNotes.Width - 240;
                this.dataGrid_RetirementNotes.Columns.Add("IsCompleted", "Completed", new CheckBoxEditor()).Width = 100;

                this.dataGrid_RetirementNotes.DataSource = new DevAge.ComponentModel.BoundList<Note>(model.ClientFna.FnaNotes);
                this.dataGrid_RetirementNotes.DataSource.ListChanged += DataSource_ListChanged<Note>;

                this.dataGrid_RetirementNotes.Format(ReadOnly, AllowDelete: Program.User.IsAdministrator);
                this.dataGrid_RetirementNotes.Rows.RowHeight = 40;
                #endregion

                #region Mapped Fields
                xInput_ClientAge.MappedField = "Age";
                xInput_ClientAge.ControlTypes = UserControls.ControlTypes.IntBox;
                xInput_ClientAge.Model = model.ClientDetails;

                xInput_RetirementAge.ReadOnly = ReadOnly;
                xInput_RetirementAge.MappedField = "RetirementAge";
                xInput_RetirementAge.ControlTypes = UserControls.ControlTypes.IntBox;
                xInput_RetirementAge.Model = model.ClientDetails;

                xInput_LifeExpectancy.ReadOnly = ReadOnly;
                xInput_LifeExpectancy.MappedField = "LifeExpectancy";
                xInput_LifeExpectancy.ControlTypes = UserControls.ControlTypes.IntBox;
                xInput_LifeExpectancy.Model = model.ClientDetails;

                xInput_InflationRate.ReadOnly = ReadOnly;
                xInput_InflationRate.MappedField = "InflationPercentage";
                //xInput_InflationRate.ControlTypes = UserControls.ControlTypes.PercentBox;
                xInput_InflationRate.Model = model.ClientFna;

                xInput_GrowthRate.ReadOnly = ReadOnly;
                xInput_GrowthRate.MappedField = "GrowthPercentage";
                //xInput_InflationRate.ControlTypes = UserControls.ControlTypes.PercentBox;
                xInput_GrowthRate.Model = model.ClientFna;

                xInput_Escalation.ReadOnly = ReadOnly;
                xInput_Escalation.MappedField = "EsclPercentage";
                //xInput_InflationRate.ControlTypes = UserControls.ControlTypes.PercentBox;
                xInput_Escalation.Model = model.ClientFna;

                xInput_InvestmentYears.MappedField = "InvestmentYears";
                xInput_InvestmentYears.ControlTypes = UserControls.ControlTypes.IntBox;
                xInput_InvestmentYears.Model = model.ClientFna;

                xInput_RetireYears.MappedField = "RetirementYears";
                xInput_RetireYears.ControlTypes = UserControls.ControlTypes.IntBox;
                xInput_RetireYears.Model = model.ClientFna;

                xInput_CurrentContributions.MappedField = "_HavesTotalPremium";
                xInput_CurrentContributions.Model = model.ClientFna;

                xInput_AvailCash.MappedField = "AvailableCash";
                xInput_AvailCash.Model = model;

                xInput_CashFlow.MappedField = "CashFlow";
                xInput_CashFlow.Model = model;

                xInput_AddContribs.MappedField = "AddContribs";
                xInput_AddContribs.Model = model.ClientFna;

                xInput_HavesTotal.MappedField = "HavesTotal";
                xInput_HavesTotal.Model = model.ClientFna;
                xInput_NeedsTotal.MappedField = "NeedsTotal";
                xInput_NeedsTotal.Model = model.ClientFna;
                xInput_RequiredTotal.MappedField = "WantsTotal";
                xInput_RequiredTotal.Model = model.ClientFna;
                xInput_ShortfallTotal.MappedField = "Shortfall";
                xInput_ShortfallTotal.Model = model.ClientFna;
                xInput_RequiredPremium.MappedField = "RequiredPremium";
                xInput_RequiredPremium.Model = model.ClientFna;

                hScrollBar_RetirementAge.Enabled = !ReadOnly;
                hScrollBar_RetirementAge.Minimum = 55;
                hScrollBar_RetirementAge.Maximum = 110;
                hScrollBar_RetirementAge.Value = model.ClientDetails.RetirementAge == 0 ? 55 : model.ClientDetails.RetirementAge;
                hScrollBar_RetirementAge.Scroll += hScrollBar_RetirementAge_Scroll;

                hScrollBar_LifeExpectancy.Enabled = !ReadOnly;
                hScrollBar_LifeExpectancy.Minimum = 55;
                hScrollBar_LifeExpectancy.Maximum = 120;
                hScrollBar_LifeExpectancy.Value = model.ClientDetails.LifeExpectancy == 0 ? 55 : model.ClientDetails.LifeExpectancy;
                hScrollBar_LifeExpectancy.Scroll += hScrollBar_LifeExpectancy_Scroll;

                hScrollBar_InflationRate.Enabled = !ReadOnly;
                hScrollBar_InflationRate.Minimum = 0;
                hScrollBar_InflationRate.Maximum = 100;
                hScrollBar_InflationRate.Value = (int)model.ClientFna.InflationPercentage;
                hScrollBar_InflationRate.Scroll += hScrollBar_InflationRate_Scroll;

                hScrollBar_GrowthRate.Enabled = !ReadOnly;
                hScrollBar_GrowthRate.Minimum = 0;
                hScrollBar_GrowthRate.Maximum = 100;
                hScrollBar_GrowthRate.Value = (int)model.ClientFna.GrowthPercentage;
                hScrollBar_GrowthRate.Scroll += hScrollBar_GrowthRate_Scroll;
                #endregion

                #region Graph Series

                //this.reportViewer_RetirementFNA.ReportRefresh += ReportViewer_RetirementFNA_ReportRefresh;
                //this.reportViewer_RetirementFNA.Resize += ReportViewer_Resize;

                RefreshClientFna();

                #endregion

                #endregion

                #region Education Fna

                this.dataGrid_EducationFna.Initialise();

                ComboBoxEditor cboEducationFnaLisps = new ComboBoxEditor(Lisps);
                cboEducationFnaLisps.Control.SelectedValueChanged += cboEducationFnaLisps_SelectedValueChanged;

                this.dataGrid_EducationFna.Columns.Add("DependentName", "Name", new StringEditor()).Width = 150;
                this.dataGrid_EducationFna.Columns.Add("DependentDOB", "Birth Date", new DateEditor()).Width = 150;

                //this.dataGrid_EducationFna.Columns.Add("ReferenceNo", "Policy No.", new StringEditor()).Width = 150;
                //this.dataGrid_EducationFna.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor()).Width = 150;

                this.dataGrid_EducationFna.Columns.Add("InvestmentAge", "University Age", new NumericEditor()).Width = 100;
                this.dataGrid_EducationFna.Columns.Add("Description", "Qualification", new StringEditor()).Width = 175;
                this.dataGrid_EducationFna.Columns.Add("CurrentAmount", "Current Value", new CurrencyEditor()).Width = 150;
                this.dataGrid_EducationFna.Columns.Add("EscalationPercentage", "Escalate (%p/a)", new DecimalEditor()).Width = 65;
                this.dataGrid_EducationFna.Columns.Add("GrowthPercentage", "Growth (%p/a)", new DecimalEditor()).Width = 150;

                this.dataGrid_EducationFna.Columns.Add("FutureAmount", "Future Value", new CurrencyEditor(true)).Width = 150;
                this.dataGrid_EducationFna.Columns.Add("MonthlyContribution", "Required (p/m)", new CurrencyEditor(true)).Width = 150;

                this.dataGrid_EducationFna.DataSource = new DevAge.ComponentModel.BoundList<EducationNeed>(model.ClientFnaEducation.EducationNeeds);
                this.dataGrid_EducationFna.DataSource.ListChanged += DataSource_ListChanged<EducationNeed>;

                //  row header click event
                SourceGrid.Cells.Controllers.CustomEvents EducationFnaClickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
                EducationFnaClickEvent.FocusEntered += EducationFna_Click;
                this.dataGrid_EducationFna.Controller.AddController(EducationFnaClickEvent); //Event fired when any column is clicked

                this.dataGrid_EducationFna.Format(ReadOnly);

                //Retirement Funds
                //this.dataGrid_EducationFnaFunds.Initialise();

                //this.dataGrid_EducationFnaFunds.Columns.Add("Description", "Fund Name", new StringEditor()).Width = 250;
                //this.dataGrid_EducationFnaFunds.Columns.Add("SplitPerc", " % Premium Split", new DecimalEditor()).Width = 145;
                //this.dataGrid_EducationFnaFunds.Columns.Add("InitialAmount", "Lump Sum", new CurrencyEditor()).Width = 200;
                //this.dataGrid_EducationFnaFunds.Columns.Add("CurrentAmount", "Current Value", new CurrencyEditor()).Width = 200;
                //this.dataGrid_EducationFnaFunds.Columns.Add("MonthlyContribution", "Premium (p/m)", new CurrencyEditor(true)).Width = 120;
                //this.dataGrid_EducationFnaFunds.Columns.Add("Type", "Fund Class", new StringEditor(true)).Width = 195;
                //this.dataGrid_EducationFnaFunds.Columns.Add("Risk", "Risk Category", new StringEditor(true)).Width = 150;
                //this.dataGrid_EducationFnaFunds.Columns.Add("IRR", "IRR", new DecimalEditor(true)).Width = 100;

                //ShowEducationFnaFunds(0);

                //this.dataGrid_EducationFnaFunds.Format(ReadOnly);
                #endregion

                #region Client Instructions/ Admin Tasks
                this.dataGrid_ClientInstructions.Initialise();

                var Users = new List<ListDataItem>();
                foreach (var user in Program.Repository.List<User, int>(null))
                    Users.Add(new ListDataItem(ListDataItemType.User, user.Firstname, user.Firstname));
                ComboBoxEditor cboUsers = new ComboBoxEditor(Users);
                cboUsers.Control.SelectedValueChanged += cboUsers_SelectedValueChanged;

                this.dataGrid_ClientInstructions.Columns.Add("Type", "Type", new ComboBoxEditor(ListDataItemType.ClientInstructionType)).Width = 150;
                this.dataGrid_ClientInstructions.Columns.Add("Comment", "Comment", new MultiLineEditor()).Width = 350;
                this.dataGrid_ClientInstructions.Columns.Add("ReferenceNo", "Reference No", new StringEditor()).Width = 150;
                this.dataGrid_ClientInstructions.Columns.Add("AllocatedTo", "Allocated To", cboUsers).Width = 350;
                this.dataGrid_ClientInstructions.Columns.Add("Status", "Status", new ComboBoxEditor(ListDataItemType.ClientInstructionStatus, true)).Width = 100;
                this.dataGrid_ClientInstructions.Columns.Add("CreateDate", "Date Start", new DateEditor(true)).Width = 75;
                this.dataGrid_ClientInstructions.Columns.Add("UpdateDate", "Date Updated", new DateEditor(true)).Width = 75;
                this.dataGrid_ClientInstructions.Columns.Add("UpdateBy", "By", new StringEditor(true)).Width = 75;

                this.dataGrid_ClientInstructions.DataSource = new DevAge.ComponentModel.BoundList<Instruction>(model.ClientInstructions.Instructions);
                this.dataGrid_ClientInstructions.DataSource.ListChanged += DataSource_ListChanged<Instruction>;


                if (!ReadOnly)
                {
                    //  Funds click event
                    SourceGrid.Cells.Controllers.CustomEvents clickEvent2 = new SourceGrid.Cells.Controllers.CustomEvents();
                    clickEvent2.Click += Instructions_Click;

                    var btnFunds = new SourceGrid.Cells.RowHeader("");
                    btnFunds.Image = Properties.Resources.save;
                    btnFunds.ToolTipText = "Click to add Funds";
                    btnFunds.AddController(clickEvent2);

                    var col = dataGrid_ClientInstructions.Columns.Add("", "", btnFunds);
                    col.Width = 22;
                    col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
                    col.MaximalWidth = 22;
                    col.MinimalWidth = 22;

                }

                this.dataGrid_ClientInstructions.Format(ReadOnly, AllowDelete: Program.User.IsAdministrator);

                #endregion

                #region DocumentTemplates

                var DocumentTemplatesList = Program.Repository.List<DocumentTemplate, int>(x => x.Status == "True").ToList();
                toolStripButton_Documents.DropDownItems.Clear();

                foreach (DocumentTemplate dT in DocumentTemplatesList)
                {
                    ToolStripMenuItem tM = new ToolStripMenuItem()
                    {
                        Text = dT.TemplateName,
                        Tag = dT,
                    };
                    tM.Click += new EventHandler(DocumentTemplate_OnClick);
                    toolStripButton_Documents.DropDownItems.Add(tM);
                }

                #endregion

                #region Client Review Meetings

                if (Program.Repository != null)
                    clientMeetings = Program.Repository.List<ClientMeetings, int>(x => x.ClientId == model.Id && x.AgentId == Program.User.Id).OrderBy(x => x.ScheduledDate).ToList();

                foreach (ClientMeetings meeting in clientMeetings)
                    meeting.Calculate();

                this.dataGrid_Appointments.Initialise();

                this.dataGrid_Appointments.Columns.Add("ScheduledDate", "Date", new DateEditor()).Width = 100;
                this.dataGrid_Appointments.Columns.Add("TimeFrom", "From", new TimeEditor()).Width = 100;
                this.dataGrid_Appointments.Columns.Add("TimeTo", "To", new TimeEditor()).Width = 100;
                this.dataGrid_Appointments.Columns.Add("_Duration", "Hours", new DecimalEditor(true)).Width = 55;
                this.dataGrid_Appointments.Columns.Add("Venue", "Venue", new StringEditor()).Width = 100;
                this.dataGrid_Appointments.Columns.Add("Notes", "Review Notes", new MultiLineEditor()).Width = 250;

                this.dataGrid_Appointments.Columns.Add("SetReminder", "Reminder", new CheckBoxEditor()).Width = 75;
                this.dataGrid_Appointments.Columns.Add("Reminder", "Minutes ...", new DecimalEditor()).Width = 75;

                //  this.dataGrid_Appointments.Columns.Add("Sync", "Sync?", new CheckBoxEditor()).Width = 75;

                this.dataGrid_Appointments.Columns.Add("MeetingStatus", "Status", new ComboBoxEditor(ListDataItemType.MeetingStatus)).Width = 100;

                this.dataGrid_Appointments.DataSource = new DevAge.ComponentModel.BoundList<ClientMeetings>(clientMeetings);
                this.dataGrid_Appointments.DataSource.ListChanged += DataSource_ListChanged<ClientMeetings>;

                this.dataGrid_Appointments.Format(ReadOnly, AllowDelete: Program.User.IsAdministrator);

                #endregion

                #region LockStatus
                SetClientLock();
                #endregion

                //TO DO: Update agent detail to current logged on user or keep existing agent details?
                if (model.AgentDetails == null)
                    model.AgentDetails = Program.User;

            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
                this.Close();
            }
            finally
            {
                this.ResumeLayout(true);


                RefreshStatusStrip();
                HasChanges = false;


            }
            
        }
        private void frmClient_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void frmClient_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HasChanges)
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

        #region Client portfolio
        #region SelectValueChanged Events
        private void cboRetirementLisps_SelectedValueChanged(object sender, EventArgs e)
        {
            DevAgeComboBox cbo = (DevAgeComboBox)sender;
            string selectedItem = (string)cbo.SelectedItem;

            int i = this.dataGrid_RetirementPortfolio.Selection.ActivePosition.Row;
            if (i > 0)
            {
                Retirement obj = (Retirement)this.dataGrid_RetirementPortfolio.DataSource[i - 1];
                obj.Description = selectedItem;
            }

            ShowRetirementFunds(i - 1);
        }
        private void LispRetirementFund_SelectedValueChanged(object sender, EventArgs e)
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
                   // objFund.GrowthPercentage = lFund.IRR;

                    this.dataGrid_RetirementFunds.Refresh();

                }
            }
        }
        private void cboInvestmentLisps_SelectedValueChanged(object sender, EventArgs e)
        {
            DevAgeComboBox cbo = (DevAgeComboBox)sender;
            string selectedItem = (string)cbo.SelectedItem;

            int i = this.dataGrid_InvestmentsPortfolio.Selection.ActivePosition.Row;
            if (i > 0)
            {
                Investment obj = (Investment)this.dataGrid_InvestmentsPortfolio.DataSource[i - 1];
                obj.Description = selectedItem;
            }

            ShowInvestmentFunds(i - 1);
        }
        private void LispInvestmentFund_SelectedValueChanged(object sender, EventArgs e)
        {
            DevAge.Windows.Forms.DevAgeComboBox cbo = (DevAge.Windows.Forms.DevAgeComboBox)sender;
            LispFund lFund = cbo.SelectedItem as LispFund;

            if (lFund != null)
            {
                int i = this.dataGrid_InvestmentsPortfolioFunds.Selection.ActivePosition.Row;
                if (i > 0)
                {
                    Fund objFund = (Fund)this.dataGrid_InvestmentsPortfolioFunds.DataSource[i - 1];

                    objFund.Type = lFund.FundClass; //set the Fund Type
                    objFund.Risk = lFund.RiskCat;
                    objFund.GrowthPercentage = lFund.IRR;

                    this.dataGrid_InvestmentsPortfolioFunds.Refresh();

                }
            }
        }

        private void cboEducationLisps_SelectedValueChanged(object sender, EventArgs e)
        {
            DevAgeComboBox cbo = (DevAgeComboBox)sender;
            string selectedItem = (string)cbo.SelectedItem;

            int i = this.dataGrid_EducationPortfolio.Selection.ActivePosition.Row;
            if (i > 0)
            {
                Education obj = (Education)this.dataGrid_EducationPortfolio.DataSource[i - 1];
                obj.Description = selectedItem;
            }

            ShowEducationFunds(i - 1);


        }
        private void cboEducationFnaLisps_SelectedValueChanged(object sender, EventArgs e)
        {
            DevAgeComboBox cbo = (DevAgeComboBox)sender;
            string selectedItem = (string)cbo.SelectedItem;

            int i = this.dataGrid_EducationFna.Selection.ActivePosition.Row;
            if (i > 0)
            {
                EducationNeed obj = (EducationNeed)this.dataGrid_EducationFna.DataSource[i - 1];
                obj.Description = selectedItem;
            }

            ShowEducationFnaFunds(i - 1);


        }
        private void LispEducationFund_SelectedValueChanged(object sender, EventArgs e)
        {
            DevAge.Windows.Forms.DevAgeComboBox cbo = (DevAge.Windows.Forms.DevAgeComboBox)sender;
            LispFund lFund = cbo.SelectedItem as LispFund;

            if (lFund != null)
            {
                int i = this.dataGrid_EducationPortfolioFunds.Selection.ActivePosition.Row;
                if (i > 0)
                {
                    Fund objFund = (Fund)this.dataGrid_EducationPortfolioFunds.DataSource[i - 1];

                    objFund.Type = lFund.FundClass; //set the Fund Type
                    objFund.Risk = lFund.RiskCat;
                    objFund.GrowthPercentage = lFund.IRR;

                    this.dataGrid_EducationPortfolioFunds.Refresh();

                }
            }
        }
        private void cboMedicalAids_SelectedValueChanged(object sender, EventArgs e)
        {
           
            DevAgeComboBox cbo = (DevAgeComboBox)sender;
            if (cbo.SelectedItem == null)
                return;

            string selectedItem = (string)cbo.SelectedItem;

            int i = this.dataGrid_MedicalPortfolio.Selection.ActivePosition.Row;
            if (i > 0)
            {
                var m = model.ClientPortfolio.Medicals[i - 1];
                if (selectedItem != m.Type)
                {
                    var dataCell = this.dataGrid_MedicalPortfolio.Columns[2].DataCell;
                    try
                    {
                        if (ServiceProviders != null)
                        {
                            IEnumerable<MedicalPlan> medicalPlans = (IEnumerable<MedicalPlan>)ServiceProviders.MedicalProviders.MedicalAids.Where(x => x.ProviderName == selectedItem).FirstOrDefault().MedicalPlans;
                            if (medicalPlans.Where(x => x.PlanName != null).Count() > 0)
                            {
                                dataCell.Editor.StandardValues = medicalPlans.Select(x => x.PlanName).ToList();

                                m.Description = "";//reset the policy
                            }
                        }
                    }
                    catch (Exception y) { };

                }
            }
        }
        private void LispMedicalAidBenefits_SelectedValueChanged(object sender, EventArgs e)

        {
            //DevAge.Windows.Forms.DevAgeComboBox cbo = (DevAge.Windows.Forms.DevAgeComboBox)sender;
            //LispFund lFund = cbo.SelectedItem as LispFund;

            //if (lFund != null)
            //{
            //    int i = this.dataGrid_EducationPortfolioFunds.Selection.ActivePosition.Row;
            //    if (i > 0)
            //    {
            //        Fund objFund = (Fund)this.dataGrid_EducationPortfolioFunds.DataSource[i - 1];

            //        objFund.Type = lFund.FundClass; //set the Fund Type
            //        objFund._Risk = lFund.RiskCat;
            //        objFund.GrowthPercentage = lFund.IRR;

            //        this.dataGrid_EducationPortfolioFunds.Refresh();

            //    }
            //}
        }
        private void cboLifeInsurers_SelectedValueChanged(object sender, EventArgs e)
        {
            DevAgeComboBox cbo = (DevAgeComboBox)sender;
            if (cbo.SelectedItem == null)
                return;

            string selectedItem = (string)cbo.SelectedItem;

            int i = this.dataGrid_LifePortfolio.Selection.ActivePosition.Row;
            if (i > 0)
            {
                var m = model.ClientPortfolio.Lifes[i - 1];
                if (selectedItem != m.Type)
                {
                    var dataCell = this.dataGrid_LifePortfolio.Columns[2].DataCell;
                    try
                    {
                        if (ServiceProviders != null)
                        {
                            IEnumerable<LifeProduct> lifeProducts = (IEnumerable<LifeProduct>)ServiceProviders.LifeProviders.LifeInsurers.Where(x => x.InsurerName == selectedItem).FirstOrDefault().LifeProducts;
                            if (lifeProducts.Where(x => x.ProductName != null).Count() > 0)
                            {
                                dataCell.Editor.StandardValues = lifeProducts.Select(x => x.ProductName).ToList();

                                m.Description = "";//reset the policy
                            }
                        }
                    }
                    catch (Exception y) { };

                }
            }

        }
        private void LispLifeInsurersBenefits_SelectedValueChanged(object sender, EventArgs e)

        {
            //DevAge.Windows.Forms.DevAgeComboBox cbo = (DevAge.Windows.Forms.DevAgeComboBox)sender;
            //LispFund lFund = cbo.SelectedItem as LispFund;

            //if (lFund != null)
            //{
            //    int i = this.dataGrid_EducationPortfolioFunds.Selection.ActivePosition.Row;
            //    if (i > 0)
            //    {
            //        Fund objFund = (Fund)this.dataGrid_EducationPortfolioFunds.DataSource[i - 1];

            //        objFund.Type = lFund.FundClass; //set the Fund Type
            //        objFund._Risk = lFund.RiskCat;
            //        objFund.GrowthPercentage = lFund.IRR;

            //        this.dataGrid_EducationPortfolioFunds.Refresh();

            //    }
            //}
        }
        #endregion

        void RetirementPortfolio_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            ShowRetirementFunds(context.Position.Row - 1);

        }
        void InvestmentPortfolio_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            ShowInvestmentFunds(context.Position.Row - 1);

        }
        void EducationPortfolio_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            ShowEducationFunds(context.Position.Row - 1);

        }
        void EducationFna_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            ShowEducationFnaFunds(context.Position.Row - 1);

        }
        void MedicalPortfolio_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int i = context.Position.Row;

            ShowMedicalBenefits(i - 1);

            var dataCell = this.dataGrid_MedicalPortfolio.Columns[2].DataCell;
            if (i > 0)
            {
                try
                {
                    var m = model.ClientPortfolio.Medicals[i - 1];

                    if (ServiceProviders != null)
                    {
                        IEnumerable<MedicalPlan> medicalPlans = (IEnumerable<MedicalPlan>)ServiceProviders.MedicalProviders.MedicalAids.Where(x => x.ProviderName == m.Type).FirstOrDefault().MedicalPlans;
                        if (medicalPlans.Where(x => x.PlanName != null).Count() > 0)
                        {
                            dataCell.Editor.StandardValues = medicalPlans.Select(x => x.PlanName).ToList();

                        }
                    }
                }
                catch (Exception y) { };
            }

        }
        void LifePortfolio_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int i = context.Position.Row;

            ShowLifeBenefits(i - 1);

            var dataCell = this.dataGrid_LifePortfolio.Columns[2].DataCell;
            if (i > 0)
            {
                try
                {
                    var m = model.ClientPortfolio.Lifes[i - 1];
                    if (ServiceProviders != null)
                    {
                        IEnumerable<LifeProduct> lifeProducts = (IEnumerable<LifeProduct>)ServiceProviders.LifeProviders.LifeInsurers.Where(x => x.InsurerName == m.Type).FirstOrDefault().LifeProducts;
                        if (lifeProducts.Where(x => x.ProductName != null).Count() > 0)
                        {
                            dataCell.Editor.StandardValues = lifeProducts.Select(x => x.ProductName).ToList();
                        }
                    }
                }
                catch (Exception y) { };
            }


        }

        void FNANeeds_Click(object sender, EventArgs e)
        {

            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            try
            {
                if (context.Position.Row > 0 && context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow)
                {
                    context.Grid.Selection.FocusRow(context.Position.Row);

                    Need need =  model.ClientFna.Needs[context.Position.Row - 1];

                    frmNeedFunds frm = new frmNeedFunds(model, need, ReadOnly);
                    frm.PropertyChanged += Fna_PropertyChanged;
                    frm.AddToCurrentPortfolio += AddToClientPortfolio_EventHandler;
                    frm.ShowDialog(this);

                    this.dataGrid_Needs.Refresh();

                }

            }
            catch (InvalidDataException x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            catch (Exception x1)
            {

            }
            finally
            {

            }


        }
        void CurrentPortfolio_Retirement_Click(object sender, EventArgs e)
        {

            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            try
            {
                if (context.Position.Row > 0 && context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow)
                {
                    context.Grid.Selection.FocusRow(context.Position.Row);


                    Retirement retirement = model.ClientPortfolio.Retirements[context.Position.Row - 1];

                    if (retirement.ReferenceId != 0 && retirement.Status != "Implemented")
                    {
                        Need need = model.ClientFna.Needs.Where(x => x.Id == retirement.ReferenceId).FirstOrDefault();

                        if (need == null)
                            need = Program.Repository.Get<Need, int>(retirement.ReferenceId);
                       
                        frmNeedFunds frm = new frmNeedFunds(model, retirement, need, ReadOnly);
                        frm.PropertyChanged += Fna_PropertyChanged;
                        frm.ShowDialog(this);
                    }
                    else
                    {
                      
                        frmNeedFunds frm = new frmNeedFunds(model, retirement, null, ReadOnly);
                        frm.PropertyChanged += Model_PropertyChanged;
                        frm.AddToCurrentPortfolio += AddToClientPortfolio_EventHandler;
                        frm.ShowDialog(this);

                    }
                    this.dataGrid_Needs.Refresh();

                }

            }
            catch (InvalidDataException x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            catch (Exception x1)
            {

            }
            finally
            {

            }


        }
        void CurrentPortfolio_Investments_Click(object sender, EventArgs e)
        {

            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            try
            {
                if (context.Position.Row > 0 && context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow)
                {
                    context.Grid.Selection.FocusRow(context.Position.Row);

                    Investment retirement = model.ClientPortfolio.Investments[context.Position.Row - 1];

                    if (retirement.ReferenceId != 0 && retirement.Status != "Implemented")
                    {
                        Need need = model.ClientFna.Needs.Where(x => x.Id == retirement.ReferenceId).FirstOrDefault();

                        if (need == null)
                            need = Program.Repository.Get<Need, int>(retirement.ReferenceId);

                        //frmNeedFunds frm = new frmNeedFunds(model, retirement, need, ReadOnly);
                        //frm.PropertyChanged += Fna_PropertyChanged;
                        //frm.ShowDialog(this);
                    }
                    else
                    {

                        //frmNeedFunds frm = new frmNeedFunds(model, retirement, null, ReadOnly);
                        //frm.PropertyChanged += Model_PropertyChanged;
                        //frm.AddToCurrentPortfolio += AddToClientPortfolio_EventHandler;
                        //frm.ShowDialog(this);

                    }
                    this.dataGrid_InvestmentsPortfolio.Refresh();

                }

            }
            catch (InvalidDataException x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            catch (Exception x1)
            {

            }
            finally
            {

            }


        }
        void Instructions_Click(object sender, EventArgs e)
        {

            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            try
            {
                if (context.Position.Row > 0 && context.Position.Row != context.Grid.Rows.LastVisibleScrollableRow)
                {

                    context.Grid.Selection.FocusRow(context.Position.Row);

                    Instruction instruction = model.ClientInstructions.Instructions[context.Position.Row - 1];

                    if (instruction.ReferenceId != 0)
                    {
                        Need need = model.ClientFna.Needs.Where(x=>x.Id==instruction.ReferenceId).FirstOrDefault();

                        if(need==null)
                         need = Program.Repository.Get<Need, int>(instruction.ReferenceId);

                        if (need == null)
                            throw new Exception(string.Format("This item could not be found {0}", instruction.ReferenceId));

                            frmNeedFunds frm = new frmNeedFunds(model, instruction, need, ReadOnly);
                        frm.PropertyChanged += Fna_PropertyChanged;
                        frm.AddToCurrentPortfolio += AddToClientPortfolio_EventHandler;

                        frm.ShowDialog(this);
                    }

                }

            }
            catch (InvalidDataException x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            catch (Exception x1)
            {
                MessageBoxExt.ShowWarning(x1.Message);
            }
            finally
            {
                this.dataGrid_Needs.Refresh();
            }


        }
        void ShowRetirementFunds(int RowId)
        {
            try
            {
                var dataCell = this.dataGrid_RetirementFunds.Columns[1].DataCell;
                dataCell.Editor = null;
                dataCell.Editor = new StringEditor(ReadOnly);// SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey };

                var m = model.ClientPortfolio.Retirements[RowId];

                try
                {
                    if (ServiceProviders != null)
                    {
                        IEnumerable<LispFund> lispFunds = (IEnumerable<LispFund>)ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == m.Description).FirstOrDefault().LispFunds;
                        if (lispFunds.Where(x => x.FundName != null).Count() > 0)
                        {
                            // ComboBoxEditor cboLispFunds = new ComboBoxEditor(lispFunds.Select(x => x.FundName).ToList(), ReadOnly);
                            ComboBoxEditor<LispFund> cboLispFunds = new ComboBoxEditor<LispFund>(lispFunds, ReadOnly);

                            cboLispFunds.Control.SelectedValueChanged += LispRetirementFund_SelectedValueChanged;
                            dataCell.Editor = cboLispFunds;
                        }
                    }
                }
                catch (Exception y) { };



                if (m.Funds == null)
                    m.Funds = new List<Fund>();

                this.label_RetirementFund.Text = string.Format("Funds : {0}", m.Description);

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
        void ShowInvestmentFunds(int RowId)
        {
            try
            {
                var dataCell = this.dataGrid_InvestmentsPortfolioFunds.Columns[1].DataCell;
                dataCell.Editor = null;
                dataCell.Editor = new StringEditor(ReadOnly);// SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey };

                var m = model.ClientPortfolio.Investments[RowId];

                try
                {
                    if (ServiceProviders != null)
                    {
                        IEnumerable<LispFund> lispFunds = (IEnumerable<LispFund>)ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == m.Description).FirstOrDefault().LispFunds;
                        if (lispFunds.Where(x => x.FundName != null).Count() > 0)
                        {
                            //ComboBoxEditor cboLispFunds = new ComboBoxEditor(lispFunds.Select(x => x.FundName).ToList(), ReadOnly);
                            ComboBoxEditor<LispFund> cboLispFunds = new ComboBoxEditor<LispFund>(lispFunds, ReadOnly);
                            cboLispFunds.Control.SelectedValueChanged += LispInvestmentFund_SelectedValueChanged;
                            dataCell.Editor = cboLispFunds;
                        }
                    }
                }
                catch (Exception y) { };



                if (m.Funds == null)
                    m.Funds = new List<Fund>();

                this.labelInvestmentFunds.Text = string.Format("Funds : {0}", m.Description);

                this.dataGrid_InvestmentsPortfolioFunds.DataSource = new DevAge.ComponentModel.BoundList<Fund>(m.Funds);
                this.dataGrid_InvestmentsPortfolioFunds.DataSource.ListChanged += DataSource_ListChanged<Fund>;

            }
            catch (Exception x)
            {

            }
            finally
            {
                
            }

        }
        void ShowEducationFunds(int RowId)
        {
            try
            {
                var dataCell = this.dataGrid_EducationPortfolioFunds.Columns[1].DataCell;
                dataCell.Editor = null;
                dataCell.Editor = new StringEditor(ReadOnly);// SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey };

                var m = model.ClientPortfolio.Educations[RowId];

                try
                {
                    if (ServiceProviders != null)
                    {
                        IEnumerable<LispFund> lispFunds = (IEnumerable<LispFund>)ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == m.Description).FirstOrDefault().LispFunds;
                        if (lispFunds.Where(x => x.FundName != null).Count() > 0)
                        {
                            // ComboBoxEditor cboLispFunds = new ComboBoxEditor(lispFunds.Select(x => x.FundName).ToList(), ReadOnly);
                            ComboBoxEditor<LispFund> cboLispFunds = new ComboBoxEditor<LispFund>(lispFunds, ReadOnly);

                            cboLispFunds.Control.SelectedValueChanged += LispEducationFund_SelectedValueChanged;
                            dataCell.Editor = cboLispFunds;
                        }
                    }
                }
                catch (Exception y) { };


                if (m.Funds == null)
                    m.Funds = new List<Fund>();

                this.labelEducationFunds.Text = string.Format("Funds : {0}", m.Description);

                this.dataGrid_EducationPortfolioFunds.DataSource = new DevAge.ComponentModel.BoundList<Fund>(m.Funds);
                this.dataGrid_EducationPortfolioFunds.DataSource.ListChanged += DataSource_ListChanged<Fund>;

            }
            catch (Exception x)
            {

            }
            finally
            {
                
            }

        }

        void ShowEducationFnaFunds(int RowId)
        {
            try
            {
                var dataCell = this.dataGrid_EducationFnaFunds.Columns[1].DataCell;
                dataCell.Editor = null;
                dataCell.Editor = new StringEditor(ReadOnly);// SourceGrid.Cells.Editors.TextBox(typeof(string)) { EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey };

                var m = model.ClientFnaEducation.EducationNeeds[RowId];

                try
                {
                    if (ServiceProviders != null)
                    {
                        IEnumerable<LispFund> lispFunds = (IEnumerable<LispFund>)ServiceProviders.LispProviders.Lisps.Where(x => x.LispName == m.Description).FirstOrDefault().LispFunds;
                        if (lispFunds.Where(x => x.FundName != null).Count() > 0)
                        {
                            // ComboBoxEditor cboLispFunds = new ComboBoxEditor(lispFunds.Select(x => x.FundName).ToList(), ReadOnly);
                            ComboBoxEditor<LispFund> cboLispFunds = new ComboBoxEditor<LispFund>(lispFunds, ReadOnly);

                            cboLispFunds.Control.SelectedValueChanged += LispEducationFund_SelectedValueChanged;
                            dataCell.Editor = cboLispFunds;
                        }
                    }
                }
                catch (Exception y) { };


                if (m.Funds == null)
                    m.Funds = new List<Fund>();

                this.labelEducationFunds.Text = string.Format("Funds : {0}", m.Description);

                this.dataGrid_EducationFnaFunds.DataSource = new DevAge.ComponentModel.BoundList<Fund>(m.Funds);
                this.dataGrid_EducationFnaFunds.DataSource.ListChanged += DataSource_ListChanged<Fund>;

            }
            catch (Exception x)
            {

            }
            finally
            {
                
            }

        }
        void ShowMedicalBenefits(int RowId)
        {
            try
            {
                var m = model.ClientPortfolio.Medicals[RowId];

                if (m.Benefits == null)
                    m.Benefits = new List<Benefit>();

                this.label_MedicalBenefits.Text = string.Format("Benefits : {0} [{1}]", m.Type,m.Description);

                this.dataGrid_MedicalBenefits.DataSource = new DevAge.ComponentModel.BoundList<Benefit>(m.Benefits);
                this.dataGrid_MedicalBenefits.DataSource.ListChanged += DataSource_ListChanged<Benefit>;

            }
            catch (Exception x)
            {

            }
            finally
            {
                
            }

        }
        void ShowLifeBenefits(int RowId)
        {
            try
            {
             
                var m = model.ClientPortfolio.Lifes[RowId];

                if (m.Benefits == null)
                    m.Benefits = new List<Benefit>();

                this.label_LifeBenefits.Text = string.Format("Benefits : {0} [{1}]", m.Type, m.Description);

                this.dataGrid_LifeBenefits.DataSource = new DevAge.ComponentModel.BoundList<Benefit>(m.Benefits);
                this.dataGrid_LifeBenefits.DataSource.ListChanged += DataSource_ListChanged<Benefit>;

            }
            catch (Exception x)
            {

            }
            finally
            {
                
            }

        }

        #endregion

        #region Client Fna Control events
        void hScrollBar_InflationRate_Scroll(object sender, ScrollEventArgs e)
        {
            model.ClientFna.InflationPercentage = e.NewValue;
        }
        private void hScrollBar_GrowthRate_Scroll(object sender, ScrollEventArgs e)
        {
            model.ClientFna.GrowthPercentage = e.NewValue;
        }
        void hScrollBar_LifeExpectancy_Scroll(object sender, ScrollEventArgs e)
        {
            model.ClientDetails.LifeExpectancy = e.NewValue;
        }

        void hScrollBar_RetirementAge_Scroll(object sender, ScrollEventArgs e)
        {
            model.ClientDetails.RetirementAge = e.NewValue;
        }

        private void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshClientFna();
        }
        private void cboUsers_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void Fna_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            model.ClientFna.Refresh();

            this.dataGrid_Needs.Refresh();

        }
        #endregion

        #region Model and DataSource Property Changed Event handlers
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            HasChanges = true;

            if (!IsCalculating)
            {
                try
                {
                    IsCalculating = true;

                    baseEntity<int> obj = sender as baseEntity<int>;

                    //Recalc the whole model
                    model.Calculate();

                    //Refresh graphs
                    RefreshClientFna();
                    RefreshClientPortfolio();
                }
                catch (Exception x)
                {
                  

                }
                finally
                {
                    IsCalculating = false;

                };
            }
            this.toolStripButton_Update.Enabled = HasChanges;
        }
        private void Model_ModelSaved(object sender, EntityEventArgs e)
        {
            MessageBoxExt.ShowInformation("Updated successfully");
        }
        private void Model_ModelValidated(object sender, EntityEventArgs e)
        {
          //  MessageBoxExt.ShowInformation("Validated successfully");
        }

        private void Model_ModelCalculated(object sender, EntityEventArgs e)
        {
           // MessageBoxExt.ShowInformation("Calculated successfully");
        }
        void DataSource_ListChanged<T>(object sender, ListChangedEventArgs e)
        {
            HasChanges = true;

            if (!IsCalculating)
            {
                try
                {
                  
                    IsCalculating = true;
                    model.Calculate();
                    RefreshClientFna();
                    RefreshClientPortfolio();

                    if (typeof(T) == typeof(Instruction))
                    {
                        DevAge.ComponentModel.BoundList<Instruction> o = (DevAge.ComponentModel.BoundList<Instruction>)sender;
                        Instruction m = (Instruction)o.EditedObject;

                        m.Description = string.Format("{0}, {1} {2} {3}", model.ClientDetails.LastName, model.ClientDetails.FirstName, model.ClientDetails.MidName, model.ClientDetails.ClientTitle);

                        if (m.Id == 0)
                        {
                            m.Status = InstructionStatus.Pending.ToString();
                            m.AllocatedTo = "UnAllocated";
                        }

                        m.ClientId = model.Id;
                        m.UpdateDate = DateTime.Now;
                        m.UpdateBy = Program.User.Username;
                    }
                    if (typeof(T) == typeof(Need))
                    {
                        DevAge.ComponentModel.BoundList<Need> o = (DevAge.ComponentModel.BoundList<Need>)sender;
                        Need m = (Need)o.EditedObject;

                        if (m.Status == null)
                            m.Status = "Advised";

                        m.UpdateDate = DateTime.Now;
                        m.UpdateBy = Program.User.Username;
                    }

                    }
                catch (Exception x) { }
                finally { IsCalculating = false; };
            }

            this.toolStripButton_Update.Enabled = HasChanges;

        }

        private void AddToClientPortfolio_EventHandler(object sender, EventArgs e)
        {
            Type type = sender.GetType();

            if (type == typeof(Retirement))
            {
                Retirement retirement = sender as Retirement;
                if (retirement.Id == 0)
                {
                    model.ClientPortfolio.Retirements.Add(retirement);

                    Program.Repository.Update<ClientPortfolio, int>(model.ClientPortfolio);
                }
               
                Program.Repository.Update<ClientPortfolio, int>(model.ClientPortfolio);

                model.Calculate();
            }
        }

      
        #endregion

        #region Toolstrip Button Event Handlers
        private void toolStripDropDownButton_Export_Click(object sender, EventArgs e)
        {
            if (!Program.Licensing.IsValid())
            {
                MessageBoxExt.ShowInvalidLicense("Export Failed.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog.FileName = string.Format("{0} {1} {2}.xml", model.ClientDetails.FirstName, model.ClientDetails.LastName, model.ClientDetails.IdentificationNo);

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string FileName = saveFileDialog.FileName;

                    var doc = model.Serialize<Client>();
                    doc.Save(FileName, "some key");

                    MessageBoxExt.ShowInformation("Export succeeded");
                }
                catch (Exception x)
                {
                    MessageBoxExt.ShowException(x);
                }
            }
        }

        private void toolStripButton_Documents_ButtonClick(object sender, EventArgs e)
        {
            //Generates a word document from the selected template
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Word Files (*.docx)|*.docx|Excel Files (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string FileName = openFileDialog.FileName;
                    FileInfo fInfo = new FileInfo(FileName);

                    GenerateDocument(File.ReadAllBytes(fInfo.FullName), fInfo.Name.Substring(0, fInfo.Name.IndexOf(".")));
                }
                catch (Exception x)
                {
                    MessageBoxExt.ShowException(x);
                }
            }

        }

        private void DocumentTemplate_OnClick(object sender, EventArgs e)
        {
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

                if (string.IsNullOrEmpty(model.DocumentsFolder))
                {
                    if (MessageBoxExt.ShowQuestion("A documents folder has not been created for this client. Do you wish to create now?"))
                    {
                        frmClientDocuments frm = new frmClientDocuments(model);
                        frm.ShowDialog(this);

                        if(!string.IsNullOrEmpty(model.DocumentsFolder))
                            documentsPath = model.DocumentsFolder;
                    }
                }
                else {
                    documentsPath = model.DocumentsFolder;
                }
                //Add Client data
               var xmlDoc = model.ToXml<Client>(); //Serialise Client object to Xml

                //Add Advisor data
                using (XmlWriter writer = xmlDoc.DocumentElement.CreateNavigator().AppendChild())
                {
                    writer.WriteStartElement("Advisor");
                    writer.WriteElementString("FirstName", Program.User.Firstname);
                    writer.WriteElementString("Surname", Program.User.Surname);
                    writer.WriteEndElement();

                    writer.WriteStartElement("Company");
                    writer.WriteElementString("TradeName", Program.Company.TradeName);
                    writer.WriteElementString("RegistrationNumber", Program.Company.RegistrationNumber);
                    writer.WriteElementString("FSBNumber", Program.Company.FSBNumber);
                    writer.WriteElementString("Caption", Program.Company.Caption);
                    writer.WriteStartElement("Image");
                    if (Program.Company.BgImage != null)
                        writer.WriteBase64(Program.Company.BgImage, 0, Program.Company.BgImage.Length);
                    writer.WriteEndElement();
                    writer.WriteEndElement();

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

                //Generate the document
                byte[] output = xml2docx.Generate(fileData, xmlDoc);

                if (null != output)
                {
                    var outputFilename = string.Format("{0} {1} {2}- {3} {4}.docx", model.ClientDetails.LastName, model.ClientDetails.FirstName, model.ClientDetails.IdentificationNo, fileName, DateTime.Now.ToString("yyyyMMdd"));
                    var outputPath = Path.Combine(documentsPath , outputFilename);

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
            try
            {
                if (!Program.Licensing.IsValid())
                {
                    MessageBoxExt.ShowInvalidLicense("Update Failed.");
                    return;
                }
                //Update the model
                model.Calculate();

                //Validate the model
                model.Validate();

                if (model.ClientDetails.Id == 0)
                {
                    if (Program.Licensing.IsValid())
                    {
                        //set agent details to current logged on user
                        model.AgentDetails = Program.User;

                        Program.Repository.Add<Client, int>(model);

                        model.Calculate();

                        Program.Repository.Update<Client, int>(model);

                        HasChanges = false;
                    }
                    else
                    {
                        MessageBoxExt.ShowInvalidLicense("Add New Failed.");
                    }
                }
                else
                {
                    if (Program.Licensing.IsValid())
                    {
                        Program.Repository.Update<Client, int>(model);

                        HasChanges = false;
                    }
                    else
                    {
                        MessageBoxExt.ShowInvalidLicense("Update Failed.");
                    }
                }

                if (Program.Licensing.IsValid())
                {
                    foreach (ClientMeetings meeting in clientMeetings.Where(x => x.ScheduledDate >= DateTime.Now))
                    {
                        meeting.ClientId = model.Id;
                        meeting.Name = string.Format("{0},{1}", model.ClientDetails.LastName, model.ClientDetails.Initials);

                        if (meeting.AgentId == 0)
                            meeting.AgentId = Program.User.Id;

                        meeting.Calculate();

                        if (meeting.Id == 0)
                            Program.Repository.Add<ClientMeetings, int>(meeting);
                        else
                            Program.Repository.Update<ClientMeetings, int>(meeting);
                    }
                    this.dataGrid_Appointments.Refresh();
                }
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

        private void toolStripButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Save fna graph to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveFna_Click(object sender, EventArgs e)
        {
            this.chart_Fna.GetChartAsBitMap().Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "chart_fna.png"));
        }
        private void toolStripButton_ClientDocs_Click(object sender, EventArgs e)
        {
            frmClientDocuments frm = new frmClientDocuments(model);
            frm.ShowDialog(this);
        }
        private void toolStripButton_Notes_Click(object sender, EventArgs e)
        {
            //frmClientNotes frmNotes = new frmClientNotes(model.Id,ReadOnly);
            //frmNotes.ClientLable = string.Format("Notes for {0} {1}", model.ClientDetails.FirstName, model.ClientDetails.LastName);
            //frmNotes.Show();
            //frmNotes.BringToFront();
        }
        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            if (HasChanges)
                if (!MessageBoxExt.ShowQuestion(string.Format("This will refresh the data and all your changes will be lost.\r\n\r\nAre you sure you wish to Refresh?")))
                    return;

            model = Program.Repository.Get<Client,int>(model.Id);

            ReadOnly = true;
            frmClient_Load(sender, e);

            //Release the lock
            ReleaseClientLock();

            this.Refresh();
        }
        //edit
        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {

            SetClientLock();

            //Check if client is locked and show Message
            if (ClientLockStatus.IsLocked == true)
            {
                MessageBoxExt.ShowWarning(string.Format("This client has been locked for editing by {0} {1} on machine {2} at {3}", ClientLockStatus.FirstName, ClientLockStatus.Surname, ClientLockStatus.MachineName, ClientLockStatus.LockDate));
            }
            else {
                //Add Lock
                ClientLockStatus = new ClientLockStatus()
                {
                    ClientId = model.Id,
                    UserId = Program.User.Id,
                    FirstName = Program.User.Firstname,
                    Surname = Program.User.Surname,
                    MachineName = Environment.MachineName,
                    LockDate = DateTime.Now,
                    IsLocked = true,
                    Status="0"
                };

                Program.Repository.Add<ClientLockStatus,int>(ClientLockStatus);

                model = Program.Repository.Get<Client,int>(model.Id);

                ReadOnly = false;
                frmClient_Load(sender, e);

                this.Refresh();
            }
        }
        #endregion

        #region Client Locks
        private void SetClientLock()
        {
            ClientLockStatus = Program.Repository.List<ClientLockStatus,int>(x => x.ClientId == model.Id && x.IsLocked == true).FirstOrDefault();
            if (ClientLockStatus == null)
                ClientLockStatus = new ClientLockStatus();
        }
        private void ReleaseClientLock()
        {
            if (ClientLockStatus != null)
            {
                if (ClientLockStatus.UserId == Program.User.Id)
                {
                    ClientLockStatus.IsLocked = false;
                    Program.Repository.Update<ClientLockStatus,int>(ClientLockStatus);
                }
            }
        }
        #endregion

        #region Client Contacts
        private void button_CopyAddress_Click(object sender, EventArgs e)
        {
            //Copy Physical Address to Postal Address
            model.PostalAddress.Line1 = model.PhysicalAddress.Line1;
            model.PostalAddress.Line2 = model.PhysicalAddress.Line2;
            model.PostalAddress.Line3 = model.PhysicalAddress.Line3;
            model.PostalAddress.Line4 = model.PhysicalAddress.Line4;
            model.PostalAddress.Code = model.PhysicalAddress.Code;
        }
        private void btn_SyncContactDetails_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(model.ClientDetails.IdentificationNo))
            {
                MessageBoxExt.ShowWarning("Client Identification Number is required for synchronisation");
                return;
            }

            Office.OutlookProxy proxy = new Office.OutlookProxy();
            proxy.AddContactDetails(new Office.ContactDetails()
            {
                CustomerID = model.ClientDetails.IdentificationNo,

                Title = model.ClientDetails.ClientTitle,
                Initials = model.ClientDetails.Initials,
                FirstName = model.ClientDetails.FirstName.Trim(),
                MiddleName = model.ClientDetails.MidName,
                LastName = model.ClientDetails.LastName.Trim(),

                Birthday = model.ClientDetails.DateOfBirth,
                Language = model.ClientDetails.Language,

                FullName = string.Format("{0} {1} {2}", model.ClientDetails.FirstName, model.ClientDetails.MidName, model.ClientDetails.LastName),
                GovernmentIDNumber = model.ClientDetails.IdentificationNo,

                HomeAddress = string.Format("{0} {1}, {2}\r\n{3}\r\n{4}\r\n{5}", model.PhysicalAddress.Line1, model.PhysicalAddress.Line2, model.PhysicalAddress.Line3, model.PhysicalAddress.Line4, model.PhysicalAddress.Code.ToString(), "South Africa"),
                HomeAddressStreet = string.Format("{0} {1}, {2}", model.PhysicalAddress.Line1, model.PhysicalAddress.Line2, model.PhysicalAddress.Line3),
                HomeAddressCity = model.PhysicalAddress.Line4,
                HomeAddressCountry = "South Africa",
                HomeAddressPostalCode = model.PhysicalAddress.Code.ToString(),

                MailingAddress = string.Format("{0} {1}, {2}\r\n{3}\r\n{4}\r\n{5}", model.PostalAddress.Line1, model.PostalAddress.Line2, model.PostalAddress.Line3, model.PostalAddress.Line4, model.PostalAddress.Code.ToString(), "South Africa"),
                MailingAddressStreet = string.Format("{0} {1}, {2}", model.PostalAddress.Line1, model.PostalAddress.Line2, model.PostalAddress.Line3),
                MailingAddressCity = model.PostalAddress.Line4,
                MailingAddressCountry = "South Africa",
                MailingAddressPostalCode = model.PostalAddress.Code.ToString(),

                HomeTelephoneNumber = model.ClientContacts.HomeTel,
                MobileTelephoneNumber = model.ClientContacts.CellNo,
                Email1Address = model.ClientContacts.EMailAddr,
                BusinessTelephoneNumber = model.ClientContacts.BussTel,

                Spouse = string.Format("{0}, {1} {2}", model.SpouseDetails.LastName, model.SpouseDetails.Initials, model.SpouseDetails.ClientTitle),

                Profession = model.ClientDetails.Occupation,
                JobTitle = model.ClientDetails.Occupation,

                
            });

        }

        private void btn_SyncSpouseContactDetails_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(model.SpouseDetails.IdentificationNo))
            {
                MessageBoxExt.ShowWarning("Spouse Identification Number is required for synchronisation");
                return;
            }

            Office.OutlookProxy proxy = new Office.OutlookProxy();
            proxy.AddContactDetails(new Office.ContactDetails()
            {
                CustomerID = model.SpouseDetails.IdentificationNo,

                Title = model.SpouseDetails.ClientTitle,
                Initials = model.SpouseDetails.Initials,
                FirstName = model.SpouseDetails.FirstName.Trim(),
                MiddleName = model.SpouseDetails.MidName,
                LastName = model.SpouseDetails.LastName.Trim(),

                Birthday = model.SpouseDetails.DateOfBirth,
                Language = model.SpouseDetails.Language,

                FullName = string.Format("{0} {1} {2}", model.SpouseDetails.FirstName, model.SpouseDetails.MidName, model.SpouseDetails.LastName),
                GovernmentIDNumber = model.SpouseDetails.IdentificationNo,

                HomeAddress = string.Format("{0} {1}, {2}\r\n{3}\r\n{4}\r\n{5}", model.PhysicalAddress.Line1, model.PhysicalAddress.Line2, model.PhysicalAddress.Line3, model.PhysicalAddress.Line4, model.PhysicalAddress.Code.ToString(), "South Africa"),
                HomeAddressStreet = string.Format("{0} {1}, {2}", model.PhysicalAddress.Line1, model.PhysicalAddress.Line2, model.PhysicalAddress.Line3),
                HomeAddressCity = model.PhysicalAddress.Line4,
                HomeAddressCountry = "South Africa",
                HomeAddressPostalCode = model.PhysicalAddress.Code.ToString(),

                MailingAddress = string.Format("{0} {1}, {2}\r\n{3}\r\n{4}\r\n{5}", model.PostalAddress.Line1, model.PostalAddress.Line2, model.PostalAddress.Line3, model.PostalAddress.Line4, model.PostalAddress.Code.ToString(), "South Africa"),
                MailingAddressStreet = string.Format("{0} {1}, {2}", model.PostalAddress.Line1, model.PostalAddress.Line2, model.PostalAddress.Line3),
                MailingAddressCity = model.PostalAddress.Line4,
                MailingAddressCountry = "South Africa",
                MailingAddressPostalCode = model.PostalAddress.Code.ToString(),

                HomeTelephoneNumber = model.SpouseContacts.HomeTel,
                MobileTelephoneNumber = model.SpouseContacts.CellNo,
                Email1Address = model.SpouseContacts.EMailAddr,
                BusinessTelephoneNumber = model.SpouseContacts.BussTel,

                Spouse = string.Format("{0}, {1} {2}", model.ClientDetails.LastName, model.ClientDetails.Initials, model.ClientDetails.ClientTitle),

                Profession = model.SpouseDetails.Occupation,
                JobTitle = model.SpouseDetails.Occupation,

            });

        }

        #endregion

        #region Refresh Charts
        private void RefreshClientFna()
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

            chartArea1.AxisY.LabelStyle.Format = "c";

            chart_Fna.ChartAreas.Add(chartArea1);

            chart_Fna.Legends[0].Docking = Docking.Top;
            chart_Fna.Legends[0].IsDockedInsideChartArea = true;
            chart_Fna.Legends[0].DockedToChartArea = "FnaArea";



            if (chkBox_Wants.Checked)
            {
                Series seriesWants = model.ClientFna.GetWantsSeries();

                seriesWants.ChartType = SeriesChartType.Column;
                seriesWants.ChartArea = "FnaArea";
                seriesWants.Color = Color.Red;


                chart_Fna.Series.Add(seriesWants);
            }

            if (chkBox_Haves.Checked)
            {
                Series seriesHaves = model.ClientFna.GetHavesSeries();

                seriesHaves.ChartType = SeriesChartType.StackedColumn;
                seriesHaves.ChartArea = "FnaArea";
                seriesHaves.Color = Color.Green;


                chart_Fna.Series.Add(seriesHaves);
            }


            if (chkBox_HavesAdj.Checked)
            {
                Series seriesHaves = model.ClientFna.GetHavesSeriesInflationAdj();

                seriesHaves.ChartType = SeriesChartType.Column;
                seriesHaves.ChartArea = "FnaArea";
                seriesHaves.Color = Color.Yellow;

                chart_Fna.Series.Add(seriesHaves);
            }

            if (chkBox_Needs.Checked)
            {
                Series seriesNeeds = model.ClientFna.GetNeedsSeries();

                seriesNeeds.ChartType = SeriesChartType.StackedColumn;
                seriesNeeds.ChartArea = "FnaArea";
                seriesNeeds.Color = Color.Yellow;

                chart_Fna.Series.Add(seriesNeeds);

            }

            //Refresh the Fna Grids
            dataGrid_Haves.Refresh();
            dataGrid_Wants.Refresh();
            dataGrid_Needs.Refresh();

            //dataGrid_EstatePortfolio.Refresh();

            if (model.ClientFna.Shortfall <= 0)
            {
                xInput_ShortfallTotal.textBox.BackColor = Color.Yellow;
                xInput_ShortfallTotal.textBox.ForeColor = Color.Red;
                xInput_RequiredPremium.textBox.BackColor = Color.Yellow;
                xInput_RequiredPremium.textBox.ForeColor = Color.Red;
            }
            else {
                xInput_ShortfallTotal.textBox.BackColor = Color.GhostWhite;
                xInput_ShortfallTotal.textBox.ForeColor = Color.Black;
                xInput_RequiredPremium.textBox.BackColor = Color.GhostWhite;
                xInput_RequiredPremium.textBox.ForeColor = Color.Black;
            }

            if (model.ClientFna.NeedsTotal <= 0)
            {
                xInput_NeedsTotal.textBox.BackColor = Color.Yellow;
                xInput_NeedsTotal.textBox.ForeColor = Color.Red;

            }
            else {
                xInput_NeedsTotal.textBox.BackColor = Color.GhostWhite;
                xInput_NeedsTotal.textBox.ForeColor = Color.Black;

            }
            if (model.CashFlow <= 0)
            {
                xInput_CashFlow.textBox.BackColor = Color.Yellow;
                xInput_CashFlow.textBox.ForeColor = Color.Red;

            }
            else {
                xInput_CashFlow.textBox.BackColor = Color.GhostWhite;
                xInput_CashFlow.textBox.ForeColor = Color.Black;

            }

            #region ReportViewer

            //ReportViewer_RetirementFNA_ReportRefresh(this.reportViewer_RetirementFNA, new CancelEventArgs());

            ////Refresh the Fna Grids
            //dataGrid_Haves.Refresh();
            //dataGrid_Wants.Refresh();
            //dataGrid_Needs.Refresh();



            #endregion

        }
        private void RefreshClientPortfolio()
        {


           // ReportViewer_CurrentPortfolio_ReportRefresh(this.reportViewer_CurrentPortfolio, new CancelEventArgs());

            this.dataGrid_RetirementPortfolio.Refresh();
            this.dataGrid_InvestmentsPortfolio.Refresh();
            this.dataGrid_EducationPortfolio.Refresh();
            this.dataGrid_LifePortfolio.Refresh();
            this.dataGrid_MedicalPortfolio.Refresh();



        }
        private void RefreshStatusStrip()
        {
            if (model.ClientDetails.Id == 0)
                this.lblCaption.Text = string.Format("{0} {1}", "New", "Client");
            else
                this.lblCaption.Text = string.Format("{0}, {1} {2} {3}", model.ClientDetails.LastName, model.ClientDetails.FirstName, model.ClientDetails.MidName, model.ClientDetails.ClientTitle);

            this.Text = string.Format("Client {0}", model.Id);


            this.toolStripButton_Update.Enabled = HasChanges;
            this.toolStripButton1.Enabled = ReadOnly;
            this.toolStripButton_Refresh.Enabled = !ReadOnly;
            this.button_CopyAddress.Enabled = !ReadOnly;

            if (model.Id == 0)
                this.toolStripButton_Refresh.Enabled = false;

            #region  Toolstrip items
            statusStrip_Client.Items.Clear();

            var tsStatus = new ToolStripLabel() { Text = string.Format("Client Status : ") }; tsStatus.Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold);
            var tsStatusValue = new ToolStripLabel() { Text = string.Format("{0}", model.Status) };

            statusStrip_Client.Items.Add(tsStatus); statusStrip_Client.Items.Add(tsStatusValue);
            statusStrip_Client.Items.Add(new ToolStripSeparator());          


            var lastUpdate = new ToolStripLabel() { Text = string.Format("Date Amended :") }; lastUpdate.Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold);
            var lastUpdateDate = new ToolStripLabel() { Text = string.Format("{0} by {1}", model.ClientDetails.UpdateDate.ToString("dd MMM yyyy"),model.ClientDetails.UpdateBy) };

            statusStrip_Client.Items.Add(lastUpdate); statusStrip_Client.Items.Add(lastUpdateDate);
            statusStrip_Client.Items.Add(new ToolStripSeparator());

            var lockStatus = new ToolStripLabel() { Text = string.Format("Edit Mode") }; lockStatus.Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold);
            var lockStatusValue = new ToolStripLabel() { Text = string.Format("", ClientLockStatus.IsLocked) };

            lockStatusValue.Image = Properties.Resources.lock_close_32;
            if (ClientLockStatus.IsLocked)
                if (ClientLockStatus.UserId == Program.User.Id)
                    lockStatusValue.Image = Properties.Resources.lock_open_32;



            lockStatusValue.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.SizeToFit;
            lockStatusValue.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;

            statusStrip_Client.Items.Add(lockStatus); statusStrip_Client.Items.Add(lockStatusValue);
            statusStrip_Client.Items.Add(new ToolStripSeparator());
            #endregion

            if (Program.User.IsAdministrator)
            {
                this.toolStripDropDownButton_Export.Visible = true;
            }
        }
        #endregion

        #region ReportViewer
        //private void ReportViewer_Resize(object sender, EventArgs e)
        //{
        //    ReportViewer ReportViewer = (ReportViewer)sender;

        //    // y  (inches) = x (Pixels) / 96 dpi(screen resolution) 

        //    using (Graphics g = ReportViewer.CreateGraphics())
        //    {
        //        ReportViewer.SuspendLayout();

        //        float HeightInch = (ReportViewer.Height - 15) / g.DpiY;
        //        float WidthInch = (ReportViewer.Width - 15) / g.DpiX;

        //        IList<ReportParameter> _Params = new List<ReportParameter>();
        //        _Params.Add(new ReportParameter("ChartWidth", string.Format("{0} in", WidthInch)));
        //        _Params.Add(new ReportParameter("ChartHeight", string.Format("{0} in", HeightInch)));

        //        ReportViewer.LocalReport.SetParameters(_Params);

        //        ReportViewer.RefreshReport();

        //        ReportViewer.ResumeLayout(true);


        //    }


        //}
        
        private void ReportViewer_CurrentPortfolio_ReportRefresh(object sender, CancelEventArgs e)
        {
            //this.reportViewer_CurrentPortfolio.LocalReport.DataSources.Clear();
            //List<ClientPortfolio> ClientPortfolio_DS = new List<ClientPortfolio>();
            //ClientPortfolio_DS.Add(model.ClientPortfolio);

            //this.reportViewer_CurrentPortfolio.LocalReport.DataSources.Add(
            //    new Microsoft.Reporting.WinForms.ReportDataSource("ClientPortfolio_DS", ClientPortfolio_DS));

            //this.reportViewer_CurrentPortfolio.RefreshReport();
        }
        private void ReportViewer_RetirementFNA_ReportRefresh(object sender, CancelEventArgs e)
        {
            //ReportViewer ReportViewer = (ReportViewer)sender;

            //ReportViewer.LocalReport.DataSources.Clear();

            //ReportViewer.LocalReport.DataSources.Add(
            //    new Microsoft.Reporting.WinForms.ReportDataSource("Haves_DS", model.ClientFna.GetHavesDataPoints()));
            //ReportViewer.LocalReport.DataSources.Add(
            //    new Microsoft.Reporting.WinForms.ReportDataSource("Wants_DS", model.ClientFna.GetWantsDataPoints()));
            //ReportViewer.LocalReport.DataSources.Add(
            //    new Microsoft.Reporting.WinForms.ReportDataSource("Needs_DS", model.ClientFna.GetNeedsDataPoints()));

            //ReportViewer.RefreshReport();
        }

        #endregion

        private void FrmNotes_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HasChanges = true;
            this.toolStripButton_Update.Enabled = HasChanges;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void button_CollapseFunds_Click_1(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2Collapsed = !this.splitContainer2.Panel2Collapsed;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
