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
    public partial class frmManageSmsTemplates : Form
    {
        IList<EmailTemplate> model;

        public frmManageSmsTemplates()
        {
            InitializeComponent();

            model = Program.Repository.List<EmailTemplate, int>(x => x.Id>0).ToList();

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "SMS Templates");
            this.xToolBarMenu1.tbCaptionImage.Image = Properties.Resources.documents_b_42;

            xToolBarMenu1.tbSave.Visible = false;
            xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "New Template";
            xToolBarMenu1.tbRefresh.Visible = true;xToolBarMenu1.tbRefresh.Enabled = true;

             xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;
             xToolBarMenu1.RefreshClicked += toolStripButton_Refresh_Click; ;
             xToolBarMenu1.SaveClicked += toolStripButton_Save_Click;
            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;

            this.xInputFilterSurname.ControlTypes = UserControls.ControlTypes.TextBox;
            this.xInputFilterSurname.InitialiseControl();
            this.xInputFilterSurname.KeyPressed += XInputFilterSurname_KeyPressed;

        }

        private void XInputFilterSurname_KeyPressed(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Click(sender, e);
        }

        private void frmAdminTasks_Load(object sender, EventArgs e)
        {
            this.dataGrid_Templates.Initialise();

            this.dataGrid_Templates.Columns.Add("TemplateName", "Template Name", new StringEditor()).Width = 150;
            this.dataGrid_Templates.Columns.Add("TemplateSubject", "Subject", new StringEditor()).Width = 250;
            this.dataGrid_Templates.Columns.Add("Status", "Status", new StringEditor()).Width = 50;
           
            //  Funds click event
            SourceGrid.Cells.Controllers.CustomEvents clickEvent2 = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent2.Click += RowEdit_Click;

            var btnFunds = new SourceGrid.Cells.RowHeader("");
            btnFunds.Image = Properties.Resources.save;
            btnFunds.ToolTipText = "Click to show client";
            btnFunds.AddController(clickEvent2);

            var col = dataGrid_Templates.Columns.Add("", "", btnFunds);
            col.Width = 22;
            col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
            col.MaximalWidth = 22;
            col.MinimalWidth = 22;

            this.dataGrid_Templates.DataSource = new DevAge.ComponentModel.BoundList<EmailTemplate>(model);
            this.dataGrid_Templates.DataSource.ListChanged += DataSource_ListChanged<EmailTemplate>;

            this.dataGrid_Templates.Format(true);//ReadOnly
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
                    EmailTemplate template = model[context.Position.Row - 1];

                    frmManageEmailTemplatesAdd childForm = new frmManageEmailTemplatesAdd(template);
                    childForm.ShowDialog(this);
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
                    this.dataGrid_Templates.DataSource = new DevAge.ComponentModel.BoundList<EmailTemplate>(
                        model.Where(x => x.Id > 0 && x.TemplateName.ToLower().Contains(this.xInputFilterSurname.textBox.Text.ToLower())).ToList()
                        );
                }
                else
                {
                    model = Program.Repository.List<EmailTemplate, int>(x => x.Id > 0).ToList();
                    this.dataGrid_Templates.DataSource = new DevAge.ComponentModel.BoundList<EmailTemplate>(model);
                }
            }
            catch (Exception x)
            {
            }
            finally
            {
                this.dataGrid_Templates.Refresh();
                xToolBarMenu1.tbRefresh.Enabled = true;
            }
        }

        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                frmManageEmailTemplatesAdd childForm = new frmManageEmailTemplatesAdd(new EmailTemplate());
                childForm.ShowDialog(this);
            }
            catch (Exception x)
            {
            }
            finally
            {
                toolStripButton_Refresh_Click(sender, e);
                xToolBarMenu1.tbEdit.Enabled = true;
            }
        }

        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            try
            {
              
            }
            catch (Exception x)
            {
            }
            finally
            {


            }
        }

    }
}
