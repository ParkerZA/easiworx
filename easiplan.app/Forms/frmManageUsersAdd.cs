using Finx.App.Extensions;
using easiplan.domain.Entities;
using easiplan.domain.Services;
using my.domain.lib.core.Domain;
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
    public partial class frmManageUsersAdd : Form
    {
        User model;
        UserService userService = new UserService(Program.Repository);
        public frmManageUsersAdd(User user)
        {
            InitializeComponent();

            model = user;

            if (model.Status == "1") model.Status = "True";
            if (string.IsNullOrEmpty(model.Status) || model.Status == "0") model.Status = "False";

            this.xInput_Title.TabIndex = 1;
            this.xInput_Firstname.TabIndex = 2;
            this.xInput_Surname.TabIndex = 3;
            this.xInput_Username.TabIndex = 4;
            this.xInput_Password.TabIndex = 5;
            this.xInput_Designation.TabIndex = 6;
            this.xInput_Administrator.TabIndex = 7;
            this.xInput_IsActive.TabIndex = 8;

            this.xInput_Username.ReadOnly = user.Id != 0;

            this.xInput_Username.Model = model;
            this.xInput_Password.Model = model;
            this.xInput_Firstname.Model = model;
            this.xInput_Surname.Model = model;
            this.xInput_Administrator.Model = model;
            this.xInput_IsActive.Model = model;

            xInput_Title.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.Titles);
            this.xInput_Title.Model = model;

            this.xInput_Designation.DataSource = Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.UserDesignation);
            this.xInput_Designation.Model = model;

            this.xInput_Administrator.Visible = Program.User.IsAdministrator;
            this.xInput_IsActive.Visible = Program.User.IsAdministrator;
            this.xInput_Designation.Visible = Program.User.IsAdministrator;
            this.button_Delete.Visible = Program.User.IsAdministrator;
        }

        private void frmManageUsersAdd_Load(object sender, EventArgs e)
        {

        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            if (model.Id > 0)
                model = userService.Get(model.Id);// Program.Repository.Get<User, int>(model.Id);

            model.Refresh(); //Cancel all changes and reload from Database
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (model.Id == 0)
                {
                    userService.Add(model);
                }
                else
                {
                    userService.Update(model);
                }

                this.Close();
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
                userService.Refresh();
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxExt.ShowQuestion("Are you sure you wish to delete this user ?"))
                {
                    userService.Remove(model.Id);


                    this.Close();
                }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }
            finally
            {
                userService.Refresh();
            }

        }
    }
}
