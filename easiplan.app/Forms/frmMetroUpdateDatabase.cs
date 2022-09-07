using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetroFramework.Controls.Ext;
using Finx.App.Models;
using easiplan.domain;
using easiplan.domain.Entities;
using Finx.App.Extensions;
using easiplan.domain.Services;
using QSS.Components.Windows.Forms;
using my.domain.lib.core.Registry;
using my.domain.lib.core.Extensions;

namespace Finx.App.Forms
{
    public partial class frmMetroUpdateDatabase : MetroFramework.Forms.MetroForm
    {
     
        public frmMetroUpdateDatabase()
        {
           InitializeComponent();

            this.Text = string.Format("     {0} v{1}", Application.ProductName, Application.ProductVersion);
            this.BackImage = global::easiplan.app.Properties.Resources.finworks_b_42;
            this.BackImagePadding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.BackMaxSize = 50;

            this.ForeColor = Color.Navy;

            ImageList imageList = new ImageList();
            imageList.Images.Add(global::easiplan.app.Properties.Resources.computer_32);
            imageList.Images.Add(global::easiplan.app.Properties.Resources.config_32);


            HtmlUtils.LoadHtmlPanel(this.htmlPanel, "Finx.App.Html.DBOutOfDate.html");

            Program.ConnectionInfo.UserPwd = "";
            this.metroPanel_Configuration.Initialise<MySqlConnectionInfo>(Program.ConnectionInfo, cntr =>
            {
                cntr.For(x => x.ConnectionType, "Db Type", new MetroComboBoxEditor().DataSourceList(Program.listData.Items.Where<ListDataItem>(x => x.ListType == ListDataItemType.DBTypes).ToList()));
                cntr.For(x => x.ServerName, "Server IP", new MetroTextBoxEditor());
                cntr.For(x => x.PortNumber, "Port No", new MetroTextBoxEditor());
                cntr.For(x => x.DbName, "Database Name", new MetroTextBoxEditor());
                cntr.For(x => x.UserName, "Login Name", new MetroTextBoxEditor());
                cntr.For(x => x.UserPwd, "Password", new MetroPasswordEditor());

            }, top: 5);

            //Hook up the button click events
            this.metroButton_Configure.Text = "Update";
            this.metroButton_Configure.Click += MetroButton_Configure_Click;
         

        }

        private void MetroButton_Configure_Click(object sender, EventArgs e)
        {
            try
            {
                Program.ConnectionInfo.Validate();

                HtmlUtils.LoadHtmlPanel(this.htmlPanel, "Finx.App.Html.UpdatingDatabase.html");
                
                this.metroButton_Configure.Enabled = false;

                Program.ConfigureDatabase(false, true);

                ////Configure Database
                //MetroProgressWindow progress2 = new MetroProgressWindow();
                //progress2.SetCaption("Configuring Database");
                //progress2.SetText("Configuring Database... Please wait.");
                //System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Program.ConfigureDatabase(), progress2);
                //progress2.ShowDialog();
                //progress2.Close();


                //Update the registry
                if (Program.Repository.IsConfigured && !Program.Repository.IsInError)
                {
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbType", Program.ConnectionInfo.ConnectionType);
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbServer", Program.ConnectionInfo.ServerName);
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbCatalog", Program.ConnectionInfo.DbName);
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbSchema", Program.ConnectionInfo.DbSchema);
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbPort", Program.ConnectionInfo.PortNumber);
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbAdminUser", Program.ConnectionInfo.UserName.Encrypt(Global.RegistryKey));
                    RegistryWrapper.WriteRegistry(Global.RegistryKey, "DbAdminPwd", Program.ConnectionInfo.UserPwd.Encrypt(Global.RegistryKey));

                    MessageBoxExt.ShowInformation("Your database has successfully been configured.");
                }

            }
            catch (my.domain.lib.core.Domain.MyValidationException vx)
            {
                MessageBoxExt.ShowWarning(vx.Message);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally { this.metroButton_Configure.Enabled = true; }
        }

        private void baseForm_Load(object sender, EventArgs e)
        {

        }
    }
}
