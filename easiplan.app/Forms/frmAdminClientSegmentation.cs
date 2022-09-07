using Finx.App.Extensions;
using Finx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Finx.App.Forms
{
    public partial class frmAdminClientSegmentation : Form
    {
        IList<ClientDetails> model;
        Timer MyTimer = new Timer();

        public frmAdminClientSegmentation()
        {
            InitializeComponent();

            model = Program.Repository.List<ClientDetails, int>(x => x.Id>0).ToList();

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "Client Segmentation");
            this.xToolBarMenu1.tbCaptionImage.Image = Properties.Resources.cog_32;

            xToolBarMenu1.tbSave.Visible = false;
            xToolBarMenu1.tbEdit.Visible = false;
            xToolBarMenu1.tbRefresh.Visible = true;xToolBarMenu1.tbRefresh.Enabled = true;

            // xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;
             xToolBarMenu1.RefreshClicked += toolStripButton_Refresh_Click; ;
            // xToolBarMenu1.SaveClicked += toolStripButton_AddToPortfolio_Click;
            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;

            this.labelTime.Text = string.Format("{0}", DateTime.Now);

            var Users = new List<ListDataItem>();
            Users.Add(new ListDataItem(ListDataItemType.User, "", ""));
            Users.Add(new ListDataItem(ListDataItemType.User,"UnAllocated", "UnAllocated"));
            foreach (var user in Program.Repository.List<User, int>(null))
                Users.Add(new ListDataItem(ListDataItemType.User, user.Firstname, user.Firstname));
            this.xInputFilterAllocation.ControlTypes = UserControls.ControlTypes.ComboBox;
            this.xInputFilterAllocation.DataSource = Users;
            this.xInputFilterAllocation.InitialiseControl();

            IEnumerable<ListDataItem> statuses = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.ClientInstructionStatus);
            this.xInputFilterStatus.ControlTypes = UserControls.ControlTypes.ComboList;
            this.xInputFilterStatus.DataSource = statuses; 
            this.xInputFilterStatus.InitialiseControl();

            //Refresh time
            //MyTimer.Interval = (30 * 1000);//30 secs // (45 * 60 * 1000); // 45 mins
            //MyTimer.Tick += new EventHandler(MyTimer_Tick);
            //MyTimer.Start();
        }

        private void frmAdminTasks_Load(object sender, EventArgs e)
        {
            this.dataGrid_AdminTasks.Initialise();

            this.dataGrid_AdminTasks.Columns.Add("ClientTitle", "Title", new StringEditor()).Width = 350;
            this.dataGrid_AdminTasks.Columns.Add("LastName", "LastName", new StringEditor()).Width = 150;
            this.dataGrid_AdminTasks.Columns.Add("MidName", "MidName", new StringEditor()).Width = 350;
            this.dataGrid_AdminTasks.Columns.Add("FirstName", "FirstName", new StringEditor()).Width = 350;
            this.dataGrid_AdminTasks.Columns.Add("IdentificationNo", "IdentificationNo", new StringEditor()).Width = 100;
            this.dataGrid_AdminTasks.Columns.Add("PassportNo", "PassportNo", new StringEditor(true)).Width = 75;
           
            if (model == null)
                model = new List<ClientDetails>();

            //  Funds click event
            SourceGrid.Cells.Controllers.CustomEvents clickEvent2 = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent2.Click += FNANeeds_Click;

            var btnFunds = new SourceGrid.Cells.RowHeader("");
            btnFunds.Image = Properties.Resources.save;
            btnFunds.ToolTipText = "Click to show client";
            btnFunds.AddController(clickEvent2);

            var col = dataGrid_AdminTasks.Columns.Add("", "", btnFunds);
            col.Width = 22;
            col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
            col.MaximalWidth = 22;
            col.MinimalWidth = 22;

            this.dataGrid_AdminTasks.DataSource = new DevAge.ComponentModel.BoundList<ClientDetails>(model);
            this.dataGrid_AdminTasks.DataSource.ListChanged += DataSource_ListChanged<ClientDetails>;

            this.dataGrid_AdminTasks.Format(true);//ReadOnly
        }

        void DataSource_ListChanged<T>(object sender, ListChangedEventArgs e)
        {
           
        }

        void FNANeeds_Click(object sender, EventArgs e)
        {

            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            try
            {
                if (context.Position.Row > 0)
                {
                    ClientDetails instruction = model[context.Position.Row - 1];

                   // Need need = Program.Repository.Get<Need,int>(instruction.ReferenceId);
                   // frmNeedFunds frm = new frmNeedFunds(instruction, need);
                   //// frm.PropertyChanged += Model_PropertyChanged;
                   // frm.ShowDialog(this);

                    frmClient childForm = new frmClient(Program.Repository.Get<Client, int>(instruction.ClientId));
                    childForm.WindowState = FormWindowState.Normal;

                    childForm.ShowDialog(this);

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
                this.dataGrid_AdminTasks.Refresh();
            }


        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            //if (Changes)
            //    if (!MessageBoxExt.ShowQuestion("Do you wish to close without saving changes"))
            //        return;

            this.Close();
        }

        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!xInputFilterStatus.comboBox.SelectedValue.Equals(string.Empty))
                //{
                //    model = Program.Repository.List<Instruction, int>(x => x.Status != "Completed" && x.Status == xInputFilterStatus.comboBox.SelectedValue.ToString()).ToList();
                //    if (!xInputFilterAllocation.comboBox.Text.Equals(string.Empty))
                //        model = Program.Repository.List<Instruction, int>(x => x.Status != "Completed"
                //            && x.Status == xInputFilterStatus.comboBox.SelectedValue.ToString()
                //            && x.AllocatedTo == xInputFilterAllocation.comboBox.Text.ToString()).ToList();

                //}
                //else
                //{
                //    model = Program.Repository.List<Instruction, int>(x => x.Status != "Completed").ToList();
                //    if (!xInputFilterAllocation.comboBox.Text.Equals(string.Empty))
                //        model = Program.Repository.List<Instruction, int>(x => x.Status != "Completed"
                //            && x.AllocatedTo == xInputFilterAllocation.comboBox.Text.ToString()).ToList();

                //}
            }
            catch (Exception x)
            {
            }
            finally
            {

                this.dataGrid_AdminTasks.DataSource = new DevAge.ComponentModel.BoundList<ClientDetails>(model);

                this.dataGrid_AdminTasks.Refresh();

                xToolBarMenu1.tbRefresh.Enabled = true;

                this.labelTime.Text = string.Format("{0}", DateTime.Now);
            }
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Click(sender, e);
        }
    }
}
