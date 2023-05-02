using BrightIdeasSoftware;
using DevAge.ComponentModel;
using DevAge.Windows.Forms;
using DocumentFormat.OpenXml.EMMA;
using Finx.App.Enums;
using Finx.App.Extensions;

using Finx.App.Sms;
using Finx.App.UserControls;
using easiplan.domain;
using easiplan.domain.Entities;
using easiplan.domain.Services;
using MetroFramework.Controls.Ext;
using MetroFramework.Forms;
using my.domain.lib.core.Extensions;
using NHibernate.Util;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using easiplan.app.ContextMenus;
using easiplan.domain.Views;
using easiplan.app.Extensions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace Finx.App.Forms
{
    public partial class frmClientManagement : MetroForm
    {
        List<ClientDetailsView> clientDetailsViewList = new List<ClientDetailsView>();
        ClientDetailsView clientDetailsView = null;
        ClientSearchModel searchModel = new ClientSearchModel();
        IList<Instruction> clientInstructions = new List<Instruction>();

        public frmClientManagement()
        {
            InitializeComponent();

            this.Text = "     Client Management";

            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;

            this.BackImage = global::easiplan.app.Properties.Resources.Clients;
            this.BackImagePadding = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.BackMaxSize = 40;

            #region Search Fields
            this.grbFilter.Text = "Search Clients by ...";

            this.xInput_Surname.MappedField = "Clientname";
            this.xInput_Surname.Model = searchModel;
            this.xInput_Surname.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInput_Surname.EnterKeyPressed += XInput_Surname_KeyPressed;

            this.xInput1_Identification.MappedField = "ClientIdentification";
            this.xInput1_Identification.Model = searchModel;
            this.xInput1_Identification.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInput1_Identification.EnterKeyPressed += XInput_Identification_KeyPressed;

            this.xInput_BirthdayMonth.ControlTypes = ControlTypes.ComboList;
            this.xInput_BirthdayMonth.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.MonthsOfYear);
            this.xInput_BirthdayMonth.MappedField = "BirthMonth";
            this.xInput_BirthdayMonth.Model = searchModel;
            this.xInput_BirthdayMonth.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInput_BirthdayMonth.comboBox.SelectedIndexChanged += XInput_BirthdayMonth_Changed;

            this.xInput_BirthdayDay.ControlTypes = ControlTypes.ComboList;
            this.xInput_BirthdayDay.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.DaysOfMonth);
            this.xInput_BirthdayDay.MappedField = "BirthDay";
            this.xInput_BirthdayDay.Model = searchModel;
            this.xInput_BirthdayDay.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInput_BirthdayDay.comboBox.SelectedIndexChanged += XInput_BirthdayDay_Changed;

            //this.xInput_AppointmentDate.ControlTypes = ControlTypes.DatePicker;
            //this.xInput_AppointmentDate.MappedField = "AppointmentDate";
            //this.xInput_AppointmentDate.Model = searchModel;
            //this.xInput_AppointmentDate.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            //this.xInput_AppointmentDate.KeyPressed += XInput_AppointmentDate_KeyPressed;

            this.xInput_ClientSegment.ControlTypes = ControlTypes.ComboList;
            this.xInput_ClientSegment.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.ClientSegment); ;
            this.xInput_ClientSegment.MappedField = "ClientSegment";
            this.xInput_ClientSegment.Label.Text = "Client Rating";
            this.xInput_ClientSegment.Model = searchModel;
            this.xInput_ClientSegment.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInput_ClientSegment.comboBox.SelectedIndexChanged += XInput_Segment_Changed;

            this.xInput_ShowCompletedTasks.ControlTypes = ControlTypes.CheckBox;
            this.xInput_ShowCompletedTasks.MappedField = "ShowCompletedTask";
            this.xInput_ShowCompletedTasks.LableText = "Include Completed tasks";
            this.xInput_ShowCompletedTasks.Model = searchModel;
            this.xInput_ShowCompletedTasks.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInput_ShowCompletedTasks.chkBox.CheckedChanged += XInput_ShowCompletedTask_KeyPressed;

            this.xInput_Advisor.ControlTypes = ControlTypes.ComboList;
            this.xInput_Advisor.DataSource = Program.UserServices.ListAdvisors().ToListDataItem("Fullname", "Id").OrderBy(x => x.Id);
            this.xInput_Advisor.MappedField = "AgentId";
            this.xInput_Advisor.Label.Text = "Advisor";
            this.xInput_Advisor.Model = searchModel;
            this.xInput_Advisor.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInput_Advisor.comboBox.SelectedIndexChanged += XInput_AgentId_Changed;
            this.xInput_Advisor.Enabled = !Program.User.IsAdvisorOnly;

            this.xInputSelectAll.ControlTypes = ControlTypes.CheckBox;
            this.xInputSelectAll.MappedField = "SelectAll";
            this.xInputSelectAll.LableText = "Select All";
            this.xInputSelectAll.Model = searchModel;
            this.xInputSelectAll.Label.Font = new Font(FontFamily.GenericSansSerif, 10F);
            this.xInputSelectAll.chkBox.CheckedChanged += XInput_SelectAll_KeyPressed;

            #endregion

            #region Toolstrip Buttons

            this.tsb_Download.Text = "Extract";
            //this.tsb_Download.Image = Finx.App.easiplan.app.Properties.Resources.save2;
            this.tsb_Download.ToolTipText = "Download an extract of the clients into a .csv file that can be opened in Excell";
            this.tsb_Download.Click += Tsb_Download_Click;

            this.tsb_Email.Text = "Send Email";
            //this.tsb_Email.Image = Finx.App.easiplan.app.Properties.Resources.outlook_b_42;
            this.tsb_Email.ToolTipText = "Send email to the client list.";
            this.tsb_Email.Click += Tsb_Email_Click;

            this.tsb_SMS.Text = "Send SMS";
            // this.tsb_SMS.Image = Finx.App.easiplan.app.Properties.Resources.xml_b_42;
            this.tsb_SMS.ToolTipText = "Send sms to the client list.";
            this.tsb_SMS.Click += Tsb_Sms_Click;

            tsbNew.Text = "Add Client Task";
            this.tsbNew.ToolTipText = "Add a custom task linked to this client.";
            tsbNew.Click += TsbNew_Click;

            this.tsbDeleteClient.Visible = Program.User.IsAdministrator;
            #endregion

            #region ClientDetailsViewList Grid
            //this.dataGrid_ClientDetails.Initialise(clientDetailsViewList, column =>
            //{
            //    column.For(x => x.IsSelected, "Select", new MetroCheckBoxEditor());
            //    column.For(x => x.Fullname, "Full Name", new MetroStringEditor(201).ReadOnly(true));
            //    column.For(x => x.DateOfBirth, "Birth Date", new MetroDateEditor(100));
            //    column.For(x => x.IdentificationNo, "Identification No", new MetroDateEditor(140));
            //    column.For(c => c.RecipientAddress, "Email", new MetroStringEditor(201));
            //    column.For(c => c.RecipientCell, "Cellphone", new MetroStringEditor(201));
            //    column.For(x => x.AgentName, "Advisor Name", new MetroStringEditor(201));
            //    column.For(c => c.Rating, "Rating", new MetroStringEditor(70));
            //},
            //RowSelectEventHandler: ClientDetails_RowSelect_EventHandler,
            //ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientTask, false, null),
            //ReadOnly: false,
            //AllowDelete: false)
            //.Formatt();

            this.dataGrid_ClientDetails.Initialise1<ClientDetailsView>(clientDetailsViewList, column =>
            {
                column.For(x => x.IsSelected,"=");
                column.For(x => x.Fullname, "Full Name", new StringEditor(true), MinWidth: 200);
                column.For(x => x.DateOfBirth, "Birth Date", new DateEditor(true),MinWidth:100);
                column.For(x => x.IdentificationNo, "Identification No", new StringEditor(true), MinWidth: 200);
                column.For(c => c.RecipientAddress, "Email", new StringEditor(), MinWidth: 200);
                column.For(c => c.RecipientCell, "Cellphone", new StringEditor(), MinWidth: 200);
                column.For(x => x.AgentName, "Advisor Name", new StringEditor(true), MinWidth: 200);
                column.For(c => c.Rating, "Rating", new StringEditor(true), MinWidth: 70);

            },
            RowSelectEventHandler: ClientDetails_RowSelect_EventHandler,
            
            ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientTask, false, null),
            ReadOnly: true, AllowDelete: false
            ).Format1(AllowAddNew:false, fixedCols:2);

            this.dataGrid_ClientDetails.EnableSort = true;
            
            #endregion

            #region ClientInstructions Grid

            this.dataGrid_ClientInstructions.Initialise<Instruction>(clientInstructions, column =>
            {
                column.For(x => x.Type, "Task Type", new MetroComboBoxEditor().DataSourceList(InstructionTypeExt.ToListDataItem()).ReadOnly(true));
                column.For(c => c.TaskName, "Task Name", new MetroStringEditor());
                column.For(c => c.UpdateDate, "Updated", new MetroDateEditor());
                column.For(c => c.Status, "Status", new MetroStringEditor());
            },
            ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, false, null),
            ReadOnly: true,
            AllowDelete: false,
            AllowAddNew: false)
            .Formatt(true);



            #endregion

            lblClientCaption.Text = string.Format("{0} rows", 0);

            EnableDisableToolStrip();
        }

        #region Toolstrip Button Events

        private void EnableDisableToolStrip()
        {
            tsb_Download.Enabled = false;
            tsb_Email.Enabled = false;
            tsb_SMS.Enabled = false;
            tsbNew.Enabled = false;
            tsbClientRating.Enabled = false;

            if (clientDetailsViewList.Count > 0)
            {
                tsb_Download.Enabled = true;
                tsb_Email.Enabled = true;
                tsb_SMS.Enabled = true;
            }
            if (clientDetailsView != null)
            {
                tsbNew.Enabled = true;
                tsbClientRating.Enabled = true;
            }

        }

        private void Tsb_Download_Click(object sender, EventArgs e)
        {
            if (this.clientDetailsViewList.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                //saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";

                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string FileName = saveFileDialog.FileName;

                        Program.CreateCSVFromGenericList(this.clientDetailsViewList, FileName);

                        Process.Start(FileName);

                        //MessageBoxExt.ShowInformation("Export succeeded");
                    }
                    catch (Exception x)
                    {
                        MessageBoxExt.ShowException(x);
                    }
                }

            }
        }

        private void Tsb_Email_Click(object sender, EventArgs e)
        {

            var frm = new frmClientCommunication(this.clientDetailsViewList.Where(x => !string.IsNullOrEmpty(x.RecipientAddress)).ToList(), CommunicationAction.SendEmail);
            frm.ShowDialog(this);

        }

        private void Tsb_Sms_Click(object sender, EventArgs e)
        {
            var frm = new frmClientCommunication(this.clientDetailsViewList.Where(x => !string.IsNullOrEmpty(x.RecipientCell)).ToList(), CommunicationAction.SendSMS);
            frm.ShowDialog(this);
        }

        /// <summary>
        /// Add a new Custom task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbNew_Click(object sender, EventArgs e)
        {
            Instruction instruction = new Instruction();
            instruction.ClientId = clientDetailsView.ClientId;
            instruction.Description = clientDetailsView.Fullname;
            instruction.InstructionType = InstructionType.CUSTOMTASK;

            frmMetroAdminTaskAdd frm = new frmMetroAdminTaskAdd(instruction);
            frm.ShowDialog(this);

            FillClientInstructionsList();

        }

        /// <summary>
        /// Add Client Rating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbClientRating_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientDetailsView.ClientId > 0)
                {
                    frmMetroClientSegmentation frm = new frmMetroClientSegmentation(clientDetailsView.ClientId);
                    frm.ShowDialog(this);
                }
                else
                {
                    throw new Exception("Invalid client...please select a valid client");
                }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x, "Not a valid client");
            };
        }


        private async void tsbDeleteClient_Clicked(object sender, EventArgs e)
        {
            var count= this.clientDetailsViewList.Where(x => x.IsSelected==true).Count();
            if(MessageBoxExt.ShowQuestion($"Are you sure you wish to permanently delete these {count} client/s")){
                using (new AppWaitCursor(sender))
                {
                    foreach (var client in this.clientDetailsViewList.Where(x => x.IsSelected == true))
                    {
                       
                        await Task.Run(() =>
                        {
                           Program.ClientService.Remove(client.ClientId);

                        });
                    }

                    metroButton_Refresh_Click_1(sender, e);
                }

                //Remove this as soon as girls are done cleaning
                if (xInputSelectAll.chkBox.Checked == true)
                {
                    
                     List<Client> cl = null;
                     cl = Program.ClientService.List(null).ToList();
                     foreach (Client rec in cl)
                     {
                         Program.ClientService.Remove(rec.Id);
                     }


                    List<ClientDetails> clDetails = Program.ClientDetailsService.List(null).ToList();
                    foreach (ClientDetails rec in clDetails)
                     {
                         Program.ClientDetailsService.Remove(rec.Id);
                     }
                     
                }


            };
        }

        #endregion

        #region SearchControl KeyPress events
        private void XInput_Surname_KeyPressed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchModel.Clientname))
            {
                if (searchModel.Clientname.Length >= 1)
                {
                    using (new AppWaitCursor(sender))
                    {
                        this.clientDetailsViewList.Clear();

                        string name = searchModel.Clientname.ToLower();

                        if (name == "*")
                            clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.ClientId > 0));
                        else
                            clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.LastName.StartsWith(name)
                           && x.ClientId > 0));

                        FillClientDetailsList();
                    }
                }
            }
        }
        private void XInput_Identification_KeyPressed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchModel.ClientIdentification))
            {
                if (searchModel.ClientIdentification.Length >= 2)
                {
                    using (new AppWaitCursor(sender))
                    {
                        this.clientDetailsViewList.Clear();

                        string name = searchModel.ClientIdentification.ToLower();

                        if (name == "*")
                            clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.ClientId > 0));
                        else
                            clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.IdentificationNo.StartsWith(name)
                           && x.ClientId > 0));

                        FillClientDetailsList();
                    }
                }
            }

        }
        //private void XInput_DOB_KeyPressed(object sender, EventArgs e)
        //{
        //    DateTimePicker dt = sender as DateTimePicker;

        //    this.filteredList.Clear();
        //    //this.objectListView1.Items.Clear();

        //    //if (!string.IsNullOrEmpty(searchModel.BirthDate))
        //    //{
        //            filteredList.AddRange(clientList.Where(x => x.ClientId > 0 && x.DateOfBirth.Day == searchModel.BirthDate.Day
        //                                 && x.DateOfBirth.Month == searchModel.BirthDate.Month));//return clients only and not their spouses

        //        GetClientContactDetails();


        //    //}
        //    FillClientDetailsList();
        //}
        private void XInput_BirthdayMonth_Changed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchModel.BirthMonth))
            {
                if (searchModel.BirthMonth.Length >= 1)
                {
                    using (new AppWaitCursor(sender))
                    {
                        this.clientDetailsViewList.Clear();

                        string name = searchModel.BirthMonth.ToLower();

                        if (name == "*")
                            clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.ClientId > 0));
                        else
                            clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.DateOfBirth.Month == int.Parse(searchModel.BirthMonth)
                           ));

                        FillClientDetailsList();

                        searchModel.BirthDay = string.Empty;
                    }
                }
            }

        }
        private void XInput_BirthdayDay_Changed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchModel.BirthMonth))
            {
                if (searchModel.BirthMonth.Length >= 1 && searchModel.BirthDay.Length >= 1)
                {
                    using (new AppWaitCursor(sender))
                    {
                        this.clientDetailsViewList.Clear();

                        string name = searchModel.BirthMonth.ToLower();

                        if (name == "*")
                            clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.ClientId > 0));
                        else
                            clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.DateOfBirth.Month == int.Parse(searchModel.BirthMonth)
                            && x.DateOfBirth.Day == int.Parse(searchModel.BirthDay)
                           ));

                        FillClientDetailsList();

                    }
                }
                else
                {
                    XInput_BirthdayMonth_Changed(sender, e);
                }
            }

        }
        private void XInput_Segment_Changed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchModel.ClientSegment))
            {
                if (searchModel.ClientSegment.Length >= 1)
                {
                    using (new AppWaitCursor(sender))
                    {
                        this.clientDetailsViewList.Clear();

                        string name = searchModel.ClientSegment.ToLower();

                        if (name == "*")
                            clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.ClientId > 0));
                        else
                            clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.Rating == searchModel.ClientSegment
                           && x.ClientId > 0));

                        FillClientDetailsList();
                    }
                }
            }

        }
        private void XInput_ShowCompletedTask_KeyPressed(object sender, EventArgs e)
        {
            searchModel.ShowCompletedTask = this.xInput_ShowCompletedTasks.chkBox.Checked;

            FillClientInstructionsList();
        }

        private void XInput_SelectAll_KeyPressed(object sender, EventArgs e)
        {
            searchModel.SelectAll = this.xInputSelectAll.chkBox.Checked;
            clientDetailsViewList.Select(c => { c.IsSelected = searchModel.SelectAll; return c; }).ToList();

            FillClientDetailsList();
        }
        private void XInput_AgentId_Changed(object sender, EventArgs e)
        {
            //if (!(searchModel.AgentId==0))
            //{

            using (new AppWaitCursor(sender))
            {
                this.clientDetailsViewList.Clear();

                clientDetailsViewList.AddRange(Program.ClientDetailsService.ListView(x => x.AgentId == searchModel.AgentId
               && x.ClientId > 0));

                FillClientDetailsList();
            }

            // }

        }
        #endregion

        #region Form Button Events
        private void metroButton_Refresh_Click_1(object sender, EventArgs e)
        {
            if (searchModel.Clientname!=null)
                XInput_Surname_KeyPressed(sender, e);
            else if (searchModel.ClientIdentification!=null)
                XInput_Identification_KeyPressed(sender, e);


        }
        #endregion

        #region Form Methods
        private void FillClientDetailsList()
        {

            this.dataGrid_ClientDetails.SuspendLayout();
            clientDetailsView = null;

            if (clientDetailsViewList.Count > 0)
                clientDetailsView = clientDetailsViewList.ElementAtOrDefault(0);

            lblClientCaption.Text = string.Format("{0} rows", clientDetailsViewList.Count);

            FillClientInstructionsList();

            EnableDisableToolStrip();

            this.dataGrid_ClientDetails.ResumeLayout();

            this.dataGrid_ClientDetails.RecalcCustomScrollBars();

            this.dataGrid_ClientDetails.Refresh();

        }

        private void FillClientInstructionsList()
        {

            clientInstructions.Clear();
            toolStripLabel_Client.Font = new Font(FontFamily.GenericSansSerif, 10F);
            toolStripLabel_Client.Text = "";

            this.dataGrid_ClientInstructions.SuspendLayout();

            if (clientDetailsView != null)
            {
                if (searchModel.ShowCompletedTask)
                    clientInstructions = Program.Repository.List<Instruction, int>(x => (x.ClientId == clientDetailsView.ClientId && clientDetailsView.ClientId > 0)
                    //|| ( x.ClientId==0 && x.Description.Contains(clientDetails.FirstName.Trim()) && x.Description.Contains(clientDetails.LastName.Trim()) )
                    ).OrderByDescending(c => c.CreateDate).ToList();
                else
                    clientInstructions = Program.Repository.List<Instruction, int>(x => (x.ClientId == clientDetailsView.ClientId && clientDetailsView.ClientId > 0)
                    //|| (x.ClientId == 0 && x.Description.Contains(clientDetails.FirstName.Trim()) && x.Description.Contains(clientDetails.LastName.Trim()))
                    ).Where(x => x.Status != InstructionStatus.Completed.ToString()
                    && x.Status != InstructionStatus.AddCompleted.ToString()
                    && x.Status != InstructionStatus.UpdateCompleted.ToString()
                    && x.Status != InstructionStatus.Cancelled.ToString()
                    && x.Status != InstructionStatus.AddCancelled.ToString()
                    && x.Status != InstructionStatus.UpdateCancelled.ToString()).OrderByDescending(c => c.CreateDate).ToList();

                toolStripLabel_Client.Text = clientDetailsView.Fullname;

                tsbNew.Enabled = clientDetailsView.ClientId != 0;
                tsbClientRating.Enabled = clientDetailsView.ClientId != 0;
            }

            this.dataGrid_ClientInstructions.Formatt(clientDetailsView == null);

            this.dataGrid_ClientInstructions.Rebind(clientInstructions);

            this.dataGrid_ClientInstructions.ResumeLayout();

            this.dataGrid_ClientInstructions.RecalcCustomScrollBars();

            this.dataGrid_ClientInstructions.Columns[4].DataCell.View = new InstructionsStatusView();

            this.dataGrid_ClientInstructions.Refresh();

            EnableDisableToolStrip();


        }

        #endregion

        #region Grid Events
        private void ClientDetails_RowSelect_EventHandler(object sender, SourceGrid.RowEventArgs e)
        {
            clientDetailsView = null;
            if (clientDetailsViewList.Count > 0)
                clientDetailsView = clientDetailsViewList.ElementAtOrDefault(e.Row - 1);

            FillClientInstructionsList();
        }


        #endregion

        
    }


    public class ClientSearchModel : BaseEntity<int>
    {

        string _Clientname;
        public string Clientname { get { return _Clientname; } set { _Clientname = value; InvokePropertyChanged("Clientname"); } }

        string _ClientIdentification;
        public string ClientIdentification { get { return _ClientIdentification; } set { _ClientIdentification = value; InvokePropertyChanged("ClientIdentification"); } }

        string _BirthDay;
        public string BirthDay { get { return _BirthDay; } set { _BirthDay = value; InvokePropertyChanged("BirthDay"); } }

        string _BirthMonth;
        public string BirthMonth { get { return _BirthMonth; } set { _BirthMonth = value; InvokePropertyChanged("BirthMonth"); } }

        string _ClientSegment;
        public string ClientSegment { get { return _ClientSegment; } set { _ClientSegment = value; InvokePropertyChanged("ClientSegment"); } }

        DateTime _BirthDate;
        public DateTime BirthDate { get { return _BirthDate; } set { _BirthDate = value; InvokePropertyChanged("BirthDate"); } }

        DateTime _AppointmentDate;
        public DateTime AppointmentDate { get { return _AppointmentDate; } set { _AppointmentDate = value; InvokePropertyChanged("AppointmentDate"); } }

        public bool ShowCompletedTask { get; set; }

        public bool SelectAll { get; set; }

        int _agentId;
        public int AgentId { get { return _agentId; } set { _agentId = value; InvokePropertyChanged("AgentId"); } }


        public ClientSearchModel()
        {
            _BirthDay = "";
            _BirthMonth = "";
            _BirthDate = DateTime.Now;
            _AppointmentDate = DateTime.Now;

            AgentId = Program.User.Id;
        }

        public void Clear()
        {
            _Clientname = "";
            _ClientIdentification = "";
            _BirthDay = "";
            _BirthMonth = "";
            _ClientSegment = "";
            _BirthDate = DateTime.Now;
            _AppointmentDate = DateTime.Now;

        }
    }
}
