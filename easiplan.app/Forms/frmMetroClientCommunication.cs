using Finx.App.Enums;
using Finx.App.Extensions;
using Finx.App.UserControls;
using Finx.Domain;
using Finx.Domain.Entities;
using MetroFramework;
using MetroFramework.Controls.Ext;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.Forms
{
    /// <summary>
    /// Form to manage client communications
    /// </summary>
    public partial class frmMetroClientCommunication : MetroForm
    {
        IList<ClientDetails> clientList;
        IList<ClientDetails> filteredList = new List<ClientDetails>();

        ClientCommunicationSearchModel searchModel = new ClientCommunicationSearchModel();

        public frmMetroClientCommunication()
        {
            InitializeComponent();

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ResizeRedraw = true;

            this.Text = string.Empty;

            this.xToolBarMenu1.tbCaption.Font = MetroFonts.DefaultBold(20f);

            this.DisplayHeader = false;
            #endregion

            #region xToolBarMenu
            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "Client Communications");
            this.xToolBarMenu1.tbCaptionImage.Image = Properties.Resources.mail_b_42;

           
            xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Image = Properties.Resources.calculator_b_42; xToolBarMenu1.tbEdit.Text = "Sms";
            xToolBarMenu1.tbRefresh.Visible = true; xToolBarMenu1.tbRefresh.Image = Properties.Resources.mail_b_42; xToolBarMenu1.tbRefresh.Text = "Email";
            xToolBarMenu1.tbSave.Visible = true; xToolBarMenu1.tbSave.Image = Properties.Resources.refresh_b_42; xToolBarMenu1.tbSave.Text = "Refresh";

            xToolBarMenu1.tbEdit.Enabled = true;
            xToolBarMenu1.tbRefresh.Enabled = true;
            xToolBarMenu1.tbSave.Enabled = true;

            xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;
            xToolBarMenu1.RefreshClicked += toolStripButton_Refresh_Click; ;
            xToolBarMenu1.SaveClicked += toolStripButton_Save_Click;
            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;
            #endregion

            #region Search Bar
            this.metroPanel_Search.Initialise(searchModel, cntr =>
            {
                cntr.For(x => x.Clientname, "Client Name", new MetroTextBoxEditor(Width: 200));
            }, left: 5, labelWidth: 100, PropertyChangedHandler: clientSearch_EventHandler,  dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged);

            this.metroPanel_Search.Initialise(searchModel, cntr =>
            {
                cntr.For(x => x.ClientIdentification, "Id Number", new MetroTextBoxEditor(Width: 150));
            }, left: 305 + 30, labelWidth: 100, PropertyChangedHandler: clientSearch_EventHandler, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged);

            this.metroPanel_Search.Initialise(searchModel, cntr =>
            {
                cntr.For(x => x.DateOfBirth, "Birth Date", new MetroDateEditor(Width: 150));
            }, left: 305 + 30 + 285, labelWidth: 100, PropertyChangedHandler: clientSearch_EventHandler, dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged);
            #endregion
        }

        private void frmMetroClientCommunication_Load(object sender, EventArgs e)
        {
            try
            {
                //Load long running data queries here
                MetroProgressWindow progress = new MetroProgressWindow();
                progress.SetCaption("LOADING");
                progress.SetText("Loading data... Please wait.");
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(InitialiseData), progress);
                progress.ShowDialog();
                progress.Close();


                this.dataGrid_ClientSegmentation.Initialise<ClientDetails>(clientList.Where<ClientDetails>(s=>s.Id==0).ToList(), column =>
                {
                    column.For(x => x.Fullname, "Client Name", new MetroTextBoxEditor().ReadOnly(true));
                    column.For(c => c.IdentificationNo, "Id Number", new MetroTextBoxEditor().ReadOnly(true));
                    column.For(c => c.DateOfBirth, "Date Of Birth", new MetroDateEditor().ReadOnly(true));
                    //column.For(c => c.Age, "Age", new MetroTextBoxEditor().ReadOnly(true));
                    //column.For(c => c.Rating, "Rating", new MetroTextBoxEditor().ReadOnly(true));
                    column.For(c => c.RecipientAddress, "Email", new MetroTextBoxEditor().ReadOnly(false));
                    column.For(c => c.RecipientCell, "Cell No", new MetroTextBoxEditor().ReadOnly(false));
                    column.For(c => c.IsSelected, "Select", new MetroCheckBoxEditor().ReadOnly(false));
                },
                  PropertyChangedHandler: propertyChanged_EventHandler,
                  // ContextMenu: new DataGridContextMenu(this, ContextMenuType.ClientInstruction, ReadOnly, _Client, !ReadOnly),
                  ReadOnly: false,
                  AllowDelete: false,
                  AllowAddNew: false)
                  .Formatt(ReadOnly:false,AllowAddNew:false,AllowDelete:false);

            }
            catch (Exception x)
            {

            }
        }

        private void InitialiseData(object status)
        {
            IProgressCallback callback = status as IProgressCallback;

            clientList = Program.Repository.List<ClientDetails, int>(x => x.Id > 0).ToList();

            if (callback != null)
                callback.End();

        }

        //Send Email
        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            frmMetroClientCommunicationEmail frm = new frmMetroClientCommunicationEmail(filteredList);
            frm.ShowDialog();

        }

        //Send SMS
        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {
        }

        //Refresh
        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            searchModel.Clientname = "";
            searchModel.ClientIdentification = "";
            searchModel.DateOfBirth = searchModel.MinDateTime;

            frmMetroClientCommunication_Load(sender, e);

            xToolBarMenu1.tbRefresh.Enabled = true;
        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void propertyChanged_EventHandler(object sender, ListChangedEventArgs e)
        {
            try
            {

                DevAge.ComponentModel.BoundList<ClientDetails> _bList = sender as DevAge.ComponentModel.BoundList<ClientDetails>;
                ClientDetails mObj = _bList.EditedObject as ClientDetails;

               // mObj.Calculate();

              //  Program.Repository.Update<ClientDetails,int>(mObj);
            }
            catch (Exception x)
            {

            }
        }

        private void clientSearch_EventHandler(object sender, EventArgs e)
        {
            filteredList.Clear();
            try
            {

                if (!string.IsNullOrEmpty(searchModel.Clientname))
                {
                    if (searchModel.Clientname == "*")
                        filteredList = clientList;
                    else
                        filteredList = clientList.Where(x => x.Fullname.ToLower().StartsWith(searchModel.Clientname.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(searchModel.ClientIdentification))
                {
                    filteredList = clientList.Where(x => x.IdentificationNo.ToLower().StartsWith(searchModel.ClientIdentification.ToLower())).ToList();
                }

                if (searchModel.DateOfBirth > searchModel.MinDateTime)
                {
                    filteredList = clientList.Where(x => x.DateOfBirth.Day == searchModel.DateOfBirth.Day && x.DateOfBirth.Month == searchModel.DateOfBirth.Month).ToList();
                }

                foreach (var client in filteredList)
                {
                    //Get Contact Details
                    ClientContacts _clientContacts = null;
                    if (client.ClientId > 0) //get client contact details
                        _clientContacts = Program.Repository.List<ClientContacts, int>(x => x.ClientId == client.ClientId).FirstOrDefault();

                    if (_clientContacts != null)
                    {
                        client.RecipientAddress = _clientContacts.EMailAddr;
                        client.RecipientCell = _clientContacts.CellNo;
                    }
                }
                }
            catch (Exception x)
            {

            }
            finally
            {
                this.dataGrid_ClientSegmentation.Rebind(filteredList, propertyChanged_EventHandler);
            }
        }

        private void showClientForm_EventHandler(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            try
            {
                if (context.Position.Row > 0)
                {
                    context.Grid.Selection.FocusRow(context.Position.Row);
                    SourceGrid.DataGrid grid = context.Grid as SourceGrid.DataGrid;

                    ClientDetails clientDetails = grid.SelectedDataRows[0] as ClientDetails;

                    if (clientDetails.ClientId != 0)
                    {
                        string FormText = string.Format("Client {0}", clientDetails.ClientId);
                        Form mdiForm = Application.OpenForms["metroMdiMain"];

                        Form cForm = mdiForm.MdiChildren.Where(x => x.Text == FormText).FirstOrDefault();

                        if (cForm == null)
                        {

                            frmMetroClient1 childForm = new frmMetroClient1(clientDetails.ClientId);

                            childForm.MdiParent = mdiForm;
                            childForm.Text = FormText;
                            childForm.Show();
                        }
                        else
                        {
                            cForm.BringToFront();
                            //cForm.WindowState = FormWindowState.Maximized;
                        }

                    }
                }

            }
            catch (InvalidDataException x)
            {
                MessageBoxExt.ShowWarning(x.Message);
            }
            catch (Exception x1)
            {
                MessageBoxExt.ShowWarning("Could not show Client Form. " + x1.Message);
            }
            finally
            {
                this.dataGrid_ClientSegmentation.Refresh();
            }

        }
    }
    public class ClientCommunicationSearchModel : BaseEntity<int>
    {

        string _Clientname;
        public string Clientname { get { return _Clientname; } set { _Clientname = value; InvokePropertyChanged("Clientname"); } }

        string _ClientIdentification;
        public string ClientIdentification { get { return _ClientIdentification; } set { _ClientIdentification = value; InvokePropertyChanged("ClientIdentification"); } }

        DateTime _DateOfBirth;
        public DateTime DateOfBirth { get { return _DateOfBirth; } set { _DateOfBirth = value; InvokePropertyChanged("DateOfBirth"); } }

        public ClientCommunicationSearchModel()
        {
            IsLoading = false;
        }
    }
}
