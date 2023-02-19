using Finx.App.Extensions;
using Finx.App.MetroControls;
using Finx.App.UserControls;
using easiplan.domain.Entities;
using easiplan.domain.Services;
using MetroFramework;
using MetroFramework.Forms;
using my.domain.lib.core.Registry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.Forms
{
    public partial class metroMdiMain : MetroForm
    {
        frmMetroClientSearch frmclientSearch;
        frmMetroAdminTasks frmAdminTasks;       
        frmClientManagement frmMetroClientManagement;
       

        #region Constructor
        public metroMdiMain()
        {
            InitializeComponent();

            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;

            this.Text = string.Format("{0}", Program.ApplicationVersion()); 
            //this.SubTitle = string.Format(" v{0} ", Program.ApplicationVersion());
            this.SubTitle += string.Format(" - {0} | logged on as : {1} [{2}]", Program.User.CompanyName, Program.User.Firstname, Program.User.Designation);

            this.Padding = new Padding(2, 8, 8, 8);
            this.BackImage = global::easiplan.app.Properties.Resources.finworks_b_42;

            this.DisplayHeader = false;
            this.BackImagePadding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.BackMaxSize = 25;

            this.ResizeRedraw = true;
            
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //Initialise Client Search form
            frmclientSearch = new frmMetroClientSearch(this, this.metroPanel1.Location.X + this.metroPanel1.Width + 2, this.metroPanel1.Top);
            frmclientSearch.SwipeEvent += clientSearchForm_SwipeEvent;
            frmclientSearch.ItemSelectedEvent += clientSearchForm_ItemSelectedEvent;

            this.metroPanel1.BringToFront();        

            this.toolStripSplitButton_Clients.Click += new System.EventHandler(this.toolStripButton_Clients_Click);

            this.toolStrip1.SetDefaultStyle(true);           

            //Load long running data queries here
            MetroProgressWindow progress = new MetroProgressWindow();
            progress.SetCaption("LOADING");
            progress.SetText("Loading data... Please wait.");
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(InitialiseData), progress);
            progress.ShowDialog();
            progress.Close();

            ShowManualsMenuItems(this, new EventArgs());

        }
        #endregion

        #region Page Events

        private void metroMdiMain_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    string BackgroundImage_Filename = RegistryWrapper.ReadRegistry(Global.RegistryKey, "BackgroundImage");
            //    // Show Background Image
            //    if (!string.IsNullOrEmpty(BackgroundImage_Filename))
            //    {
            //        //this.BackgroundImage = Image.FromFile(RegistryWrapper.ReadRegistry(Global.RegistryKey, "BackgroundImage"), true);
            //        //this.BackgroundImageLayout = ImageLayout.Stretch;

            //       htmlPanel_BackgroundImg.BackgroundImage = Image.FromFile(BackgroundImage_Filename, true);

            //    }
            //}
            //catch (Exception) { };

            FormatMenu();
        }

        private void metroMdiMain_Activated(object sender, EventArgs e)
        {

            try
            {
                this.SuspendLayout();
                
                string BackgroundImage_Filename = RegistryWrapper.ReadRegistry(Global.RegistryKey, "BackgroundImage");
                // Show Background Image
                if (!string.IsNullOrEmpty(BackgroundImage_Filename))
                {
                    this.BackgroundImage = Image.FromFile(BackgroundImage_Filename, true);
                }
                else
                {
                    this.BackgroundImage = global::easiplan.app.Properties.Resources.background1;
                }

                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception) { };

            this.ResumeLayout();

        }

        private void metroMdiMain_Resize(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Refresh();

        }

        private void metroMdiMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            frmclientSearch.IsClosing = true;
            frmclientSearch.Close();

            IList<Form> frms = new List<Form>();
            foreach (Form form in Application.OpenForms)
                if (!form.IsMdiContainer && form.OwnedForms.Count() == 0 && form.Name != this.Name)
                    frms.Add(form);

            foreach (Form form in frms)
                form.Close();

            Application.Exit();
        }


        #endregion

        #region SearchForm Events
        private void clientSearchForm_ItemSelectedEvent(object sender, EventArgs e)
        {
            try
            {
                // Set cursor as hourglass
                Cursor.Current = Cursors.WaitCursor;

                int clientId = (int)sender;

                string FormText = string.Format("Client {0}", clientId);
                Form cForm = MdiChildren.Where(x => x.Text == FormText).FirstOrDefault();

                if (cForm == null)
                {
                    try
                    {
                        var childForm = new frmMetroClient1(clientId);

                        childForm.MdiParent = this;
                        childForm.Text = FormText;
                        childForm.Show();
                    }
                    catch (NullReferenceException ex)
                    { 
                    
                    }
                    catch (Exception x)
                    {
                        MessageBoxExt.ShowWarning(x.Message);
                    }

                }
                else
                {
                    cForm.BringToFront();

                }

            }
            catch (Exception x)
            {

            }
            finally
            {
                //Close the form automatically
                if (this.frmclientSearch.CloseOnSelect)
                    clientSearchForm_SwipeEvent(sender, e);

                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;
            }

        }
        private void clientSearchForm_SwipeEvent(object sender, EventArgs e)
        {
            frmclientSearch.swipe(!frmclientSearch.IsShowing);
        }

        #endregion

        #region Menu  Events

        void FormatMenu()
        {
            this.documentsToolStripMenuItem.Enabled = Program.User.IsAdministrator;
            this.templatesToolStripMenuItem.Enabled = Program.User.IsAdministrator;

            usersToolStripMenuItem.Enabled = Program.User.IsAdministrator;
            //usersToolStripMenuItem.Text = Program.User.IsAdministrator == false ? "My Password" : "Application Users";
            
        }

       
        private void tasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAdminTasksForm(sender, e);
        }
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowManageUsersForm(sender, e);
        }
        private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowManageTemplatesForm(sender, e);
        }
        private void manualsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowManageManualsTemplatesForm(sender, e);
        }
        private void emailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowManageEmailTemplatesForm(sender, e);
        }
        private void smsTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowManageSmsTemplatesForm(sender, e);
        }
        private void serviceProvidersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowServiceProvidersForm(sender, e);
        }

        private void clientToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowNewClient(sender, e);
        }

        private void customiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsForm(sender, e);

        }

        private void setupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowConfigureForm(sender, e);
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowManualsMenuItems(sender, e);
        }

        private void communicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowClientCommunicationForm(sender, e);
        }

        private void ManualsMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            if (item != null)
            {
                ManualsTemplate template = item.Tag as ManualsTemplate;
                if (template != null)
                {
                    try
                    {
                        //Show template
                        var outputFilename = string.Format("{0}", template.Filename);
                        var outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), outputFilename);

                        if (File.Exists(outputPath))
                            File.Delete(outputPath);

                        //Save doc to local folder
                        File.WriteAllBytes(outputPath, template.TemplateBytes);

                        //Launch doc in associated app
                        ProcessStartInfo startInfo = new ProcessStartInfo();

                        startInfo.CreateNoWindow = true;
                        startInfo.UseShellExecute = true;
                        startInfo.FileName = outputPath;
                        startInfo.WindowStyle = ProcessWindowStyle.Normal;

                        Process.Start(startInfo);
                    }
                    catch (Exception x)
                    {
                        MessageBoxExt.ShowException(x);
                    }
                }
            }
        }
        #endregion

        #region SideToolbar Events
        private void toolStripButton_NewClient_Click(object sender, EventArgs e)
        {
            ShowNewClient(sender,e);
        }

        private void toolStripButton_ClientSearch_Click(object sender, EventArgs e)
        {
            clientSearchForm_SwipeEvent(sender, e);
        }

        private void toolStripButton_AdminTasks_Click(object sender, EventArgs e)
        {
            ShowAdminTasksForm(sender, e);
        }

        private void toolStripButton_Clients_Click(object sender, EventArgs e)
        {
            ShowClientManagementForm(sender, e);
        }
        #endregion

        #region Toolstrip Events
      
        private void appointmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowClientManagementForm(sender, e);
        }

        private void importClientInvestmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mdiForm = Application.OpenForms["metroMdiMain"];
            var cForm = mdiForm.MdiChildren.Where(x => x.Name == "frmMetroClientImportInvestments").FirstOrDefault();

            if (cForm == null)
            {
                var childForm = new frmMetroClientImportInvestments();
                mdiForm.WindowState = FormWindowState.Maximized;
                childForm.MdiParent = mdiForm;
                childForm.StartPosition = FormStartPosition.CenterParent;
                childForm.WindowState = FormWindowState.Maximized;
                //if(ImportedCsvFiles == null)
                //    ImportedCsvFiles = new Dictionary<string, byte[]>(1);
                childForm.Show();
            }
            else
                cForm.BringToFront();
        }

        #endregion

        #region Private Methods
        private void InitialiseData(object status)
        {
            IProgressCallback callback = status as IProgressCallback;

            //TO DO : Other queries
            //Create ServiceProviders
            Program.SetServiceProviders();

            //Create DocumentTemplates
            Program.SetDocumentTemplatesList();

            if (callback != null)
                callback.End();

        }
        #endregion

        #region ShowForm Methods
        void ShowNewClient(object sender, EventArgs e)
        {
           
            try
            {
                // Set cursor as hourglass
                Cursor.Current = Cursors.WaitCursor;

                string FormText = string.Format("Client 0");
                Form cForm = MdiChildren.Where(x => x.Text == FormText).FirstOrDefault();

                if (cForm == null)
                {
                    frmMetroClient1 childForm = new frmMetroClient1(0);
                   
                    childForm.MdiParent = this;                   
                    childForm.Show();

                }
                else
                {
                    cForm.BringToFront();

                }

                
            }
            catch (Exception x)
            {

            }
            finally
            {
                // Set cursor as Default
                Cursor.Current = Cursors.Default;
            }
        }
        void ShowConfigureForm(object sender, EventArgs e)
        {
            frmConfigure frm = new frmConfigure();
            frm.ShowDialog(this);
        }
        void ShowSettingsForm(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.ShowDialog(this);
        }
        void ShowManageUsersForm(object sender, EventArgs e)
        {
            frmManageUsers frm = new frmManageUsers();
            frm.ShowDialog(this);            
        }
        void ShowManageTemplatesForm(object sender, EventArgs e)
        {
            frmManageTemplates frm = new frmManageTemplates();
            frm.ShowDialog(this);
        }
        void ShowManageEmailTemplatesForm(object sender, EventArgs e)
        {
            //frmMetroClientCommunicationEmail frm = new frmMetroClientCommunicationEmail();
            //frm.ShowDialog(this);
        }
        void ShowManageSmsTemplatesForm(object sender, EventArgs e)
        {
            //frmMetroClientCommunicationSms frm = new frmMetroClientCommunicationSms();
            //frm.ShowDialog(this);
        }
        void ShowServiceProvidersForm(object sender, EventArgs e)
        {
            frmServiceProviders frm = new frmServiceProviders();
            frm.ShowDialog(this);
        }
        void ShowAdminTasksForm(object sender, EventArgs e)
        {
            try
            {
                // Set cursor as hourglass
                Cursor.Current = Cursors.WaitCursor;

                frmAdminTasks.Show();
                
            }
            catch (Exception)
            {
                frmAdminTasks = new frmMetroAdminTasks();
                frmAdminTasks.Show();
            }
            finally
            {
                frmAdminTasks.BringToFront();

                // Set cursor as Default
                Cursor.Current = Cursors.Default;
            }
        }
       
        void ShowClientCommunicationForm(object sender, EventArgs e)
        {

            //try
            //{
            //    frmMetroClientCommunication.Show();
            //}
            //catch (Exception x)
            //{
            //    frmMetroClientCommunication = new frmClientCommunication();
            //    frmMetroClientCommunication.Show();
            //}
        }
        void ShowClientManagementForm(object sender, EventArgs e)
        {

            try
            {
                // Set cursor as hourglass
                Cursor.Current = Cursors.WaitCursor;
                
                if(frmMetroClientManagement == null)
                    frmMetroClientManagement = new frmClientManagement();

                frmMetroClientManagement.Show();
            }
            catch (Exception)
            {
                frmMetroClientManagement = new frmClientManagement();
                frmMetroClientManagement.Show();
            }
            finally
            {
                frmMetroClientManagement.BringToFront();

                // Set cursor as Default
                Cursor.Current = Cursors.Default;
            }
        }
        void ShowManageManualsTemplatesForm(object sender, EventArgs e)
        {
            frmManageManualsTemplates frm = new frmManageManualsTemplates();
            frm.ShowDialog(this);
        }
        void ShowManualsMenuItems(object sender, EventArgs e)
        {
            this.contentsToolStripMenuItem.DropDownItems.Clear();
            var list = Program.ManualsTemplateService.List(x => x.Status == "true");
            foreach (var l in list)
            {
                l.Calculate();

                ToolStripItem item = this.contentsToolStripMenuItem.DropDownItems.Add(l.TemplateName);
                item.ToolTipText = l.TemplateDescription;
                item.Tag = l;

                item.Click += ManualsMenuItem_Click;
            }

        }

        #endregion

        
    }
}
