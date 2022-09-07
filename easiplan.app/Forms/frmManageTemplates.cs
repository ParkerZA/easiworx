using Finx.App.Extensions;
using easiplan.domain.Entities;
using easiplan.domain.Services;
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
    public partial class frmManageTemplates : MetroForm
    {

        DocumentTemplateService DocumentTemplateService = new DocumentTemplateService(Program.Repository);
      
        IList<DocumentTemplate> model;

        public frmManageTemplates()
        {
            InitializeComponent();

            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Orange;

            this.Text = "Manage Report Templates";

            model = DocumentTemplateService.List(x => x.Id > 0).ToList();

            //this.Text = "Document Templates";
            //this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "Manage Report Templates");
            //this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.documents_b_42;
           

            xToolBarMenu1.tbSave.Visible = false;
            xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "New Template"; xToolBarMenu1.tbEdit.Enabled = Program.User.IsAdministrator;
            xToolBarMenu1.tbRefresh.Visible = true;xToolBarMenu1.tbRefresh.Enabled = true;

             xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;
             xToolBarMenu1.RefreshClicked += toolStripButton_Refresh_Click; ;
             
            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;

            this.xInputFilterSurname.ControlTypes = UserControls.ControlTypes.TextBox;
            this.xInputFilterSurname.InitialiseControl();
            this.xInputFilterSurname.KeyPressed += XInputFilterSurname_KeyPressed;
        }

        private void XInputFilterSurname_KeyPressed(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Click(sender, e);
        }

        private void frmManageTemplates_Load(object sender, EventArgs e)
        {
            this.dataGrid_Templates.Initialise();

            this.dataGrid_Templates.Columns.Add("TemplateName", "Template Name", new StringEditor()).Width = 150;
            this.dataGrid_Templates.Columns.Add("TemplateDescription", "Description", new StringEditor()).Width = 250;
            this.dataGrid_Templates.Columns.Add("Status", "Is Active", new CheckBoxEditor()).Width = 50;
            this.dataGrid_Templates.Columns.Add("Filename", "Filename", new StringEditor()).Width =200;
           
            //  Funds click event
            SourceGrid.Cells.Controllers.CustomEvents clickEvent2 = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent2.Click += RowEdit_Click;

            var btnFunds = new SourceGrid.Cells.RowHeader("");
            btnFunds.Image = easiplan.app.Properties.Resources.save;
            btnFunds.ToolTipText = "Click to show client";
            btnFunds.AddController(clickEvent2);

            if (Program.User.IsAdministrator)
            {
                var col = dataGrid_Templates.Columns.Add("", "", btnFunds);
                col.Width = 22;
                col.AutoSizeMode = SourceGrid.AutoSizeMode.None;
                col.MaximalWidth = 22;
                col.MinimalWidth = 22;
            }

            this.dataGrid_Templates.DataSource = new DevAge.ComponentModel.BoundList<DocumentTemplate>(model);
            this.dataGrid_Templates.DataSource.ListChanged += DataSource_ListChanged<DocumentTemplate>;

            this.dataGrid_Templates.Format(true);//ReadOnly:!Program.User.IsAdministrator
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
                    DocumentTemplate template = model[context.Position.Row - 1];

                    frmManageTemplatesAdd childForm = new frmManageTemplatesAdd(template);
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
                    this.dataGrid_Templates.DataSource = new DevAge.ComponentModel.BoundList<DocumentTemplate>(
                        model.Where(x => x.Id > 0 && x.TemplateName.ToLower().Contains(this.xInputFilterSurname.textBox.Text.ToLower())).ToList()
                        );
                }
                else
                {
                    model = Program.Repository.List<DocumentTemplate, int>(x => x.Id > 0).ToList();
                    this.dataGrid_Templates.DataSource = new DevAge.ComponentModel.BoundList<DocumentTemplate>(model);
                }
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
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
                frmManageTemplatesAdd childForm = new frmManageTemplatesAdd(new DocumentTemplate());
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
