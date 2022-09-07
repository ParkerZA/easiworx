using Finx.App.Extensions;
using easiplan.domain.Entities;
using MetroFramework.Forms;
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
    public partial class frmManageUsers : MetroForm
    {
        IList<User> model;

        public frmManageUsers()
        {
            InitializeComponent();

            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Orange;

            this.Text = "Manage Users";

            if (Program.User.IsAdministrator)
                model = Program.Repository.List<User,int>(x => x.Id>0).ToList();//list all users
            else
                model = Program.Repository.List<User, int>(x => x.Username == Program.User.Username).ToList();//current user only

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "Application Users");
            this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.persons_1_32;

            xToolBarMenu1.tbSave.Visible = false;

            xToolBarMenu1.tbEdit.Visible = Program.User.IsAdministrator; xToolBarMenu1.tbEdit.Text = "New User";
            xToolBarMenu1.tbRefresh.Visible = true;xToolBarMenu1.tbRefresh.Enabled = true;

             xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;
             xToolBarMenu1.RefreshClicked += toolStripButton_Refresh_Click; ;
             xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;

            this.xInputFilterSurname.ControlTypes = UserControls.ControlTypes.TextBox;
            this.xInputFilterSurname.InitialiseControl();
            this.xInputFilterSurname.KeyPressed += XInputFilterSurname_Click;

        }

        private void XInputFilterSurname_Click(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Click(sender, e);
        }

        private void frmAdminTasks_Load(object sender, EventArgs e)
        {
            this.dataGrid_Users.Initialise();

            this.dataGrid_Users.Columns.Add("Title", "Title", new StringEditor()).Width = 50;
            this.dataGrid_Users.Columns.Add("Firstname", "Firstname", new StringEditor()).Width = 200;
            this.dataGrid_Users.Columns.Add("Surname", "Surname", new StringEditor()).Width = 200;
            this.dataGrid_Users.Columns.Add("Designation", "Designation", new StringEditor()).Width =200;
            this.dataGrid_Users.Columns.Add("Username", "Username", new StringEditor()).Width = 100;
            this.dataGrid_Users.Columns.Add("IsAdministrator", "IsAdministrator", new CheckBoxEditor()).Width = 50;
            this.dataGrid_Users.Columns.Add("IsActive", "Is Active", new CheckBoxEditor()).Width = 50;

            //  Edit click event
            SourceGrid.Cells.Controllers.CustomEvents clickEvent2 = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent2.Click += RowEdit_Click;

            var btnEdit = new SourceGrid.Cells.RowHeader("");
            btnEdit.Image = easiplan.app.Properties.Resources.save;
            btnEdit.ToolTipText = "Click to edit User";
            btnEdit.AddController(clickEvent2);

            var col = dataGrid_Users.Columns.Add("", "", btnEdit);
            col.Width = 22;
            col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
            col.MaximalWidth = 22;
            col.MinimalWidth = 22;

            this.dataGrid_Users.DataSource = new DevAge.ComponentModel.BoundList<User>(model);
            this.dataGrid_Users.DataSource.ListChanged += DataSource_ListChanged<User>;

            this.dataGrid_Users.Format(true);//ReadOnly
        }

        void DataSource_ListChanged<T>(object sender, ListChangedEventArgs e)
        {
           
        }

        void RowEdit_Click(object sender, EventArgs e)
        {

            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            try
            {
                if (context.Position.Row > 0)
                {
                    User user = model[context.Position.Row - 1];

                    frmManageUsersAdd childForm = new frmManageUsersAdd(user);
                    childForm.ShowDialog(this);
                }

            }
            catch (InvalidDataException x)
            {
                Program.Logger.Error(x);
                MessageBoxExt.ShowWarning(x.Message);
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }
            finally
            {
                toolStripButton_Refresh_Click(sender, e);
            }


        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.xInputFilterSurname.Text))
                {
                    this.dataGrid_Users.DataSource = new DevAge.ComponentModel.BoundList<User>(
                        model.Where(x => x.Id > 0 && x.Surname.ToLower().Contains(this.xInputFilterSurname.textBox.Text.ToLower())).ToList()
                        );
                }
                else
                {
                    if (Program.User.IsAdministrator)
                        model = Program.Repository.List<User, int>(x => x.Id > 0).ToList();
                    else
                        model = Program.Repository.List<User, int>(x => x.Username == Program.User.Username).ToList();

                    this.dataGrid_Users.DataSource = new DevAge.ComponentModel.BoundList<User>(model);
                }
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }
            finally
            {
                this.dataGrid_Users.Refresh();
                xToolBarMenu1.tbRefresh.Enabled = true;
            }
        }

        /// <summary>
        /// Adding a New User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                frmManageUsersAdd childForm = new frmManageUsersAdd(new User());
                childForm.ShowDialog(this);
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }
            finally
            {
                toolStripButton_Refresh_Click(sender, e);
                xToolBarMenu1.tbEdit.Enabled = true;
            }
        }

     

    }
}
