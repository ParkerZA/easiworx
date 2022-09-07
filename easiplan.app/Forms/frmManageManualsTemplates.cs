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
    public partial class frmManageManualsTemplates : MetroForm
    {

        IList<ManualsTemplate> model;
        ManualsTemplateService modelService = new ManualsTemplateService(Program.Repository);

        public frmManageManualsTemplates()
        {
            InitializeComponent();

            model = modelService.List(x => x.Id > 0).ToList();

            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Orange;

            this.Text = "Manage Application Manuals";


            // this.Text = "Manuals";
            // this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "Manuals");
            // this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.documents_b_42;

            this.xToolBarMenu1.tbCaption.Visible = false;
            this.xToolBarMenu1.tbCaptionImage.Visible = false;


            xToolBarMenu1.tbSave.Visible = false;
            xToolBarMenu1.tbEdit.Visible = true; xToolBarMenu1.tbEdit.Text = "New Manual"; xToolBarMenu1.tbEdit.Enabled = Program.User.IsAdministrator;
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
            this.dataGrid_Templates.Initialise(AllowDelete:false);

            this.dataGrid_Templates.Columns.Add("TemplateName", "Manual Name", new StringEditor()).Width = 150;
            this.dataGrid_Templates.Columns.Add("TemplateDescription", "Description", new StringEditor()).Width = 250;
            this.dataGrid_Templates.Columns.Add("Status", "Is Active", new StringEditor()).Width = 50;
            //this.dataGrid_Templates.Columns.Add("Filename", "Filename", new StringEditor()).Width =200;
           
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

            dataGrid_Templates.Format(AllowDelete: false);

            this.dataGrid_Templates.DataSource = new DevAge.ComponentModel.BoundList<ManualsTemplate>(model);
            this.dataGrid_Templates.DataSource.ListChanged += DataSource_ListChanged<ManualsTemplate>;

            this.dataGrid_Templates.Format( true);
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
                    ManualsTemplate template = model[context.Position.Row - 1];

                    frmManageManualTemplatesAdd childForm = new frmManageManualTemplatesAdd(template);
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
                    this.dataGrid_Templates.DataSource = new DevAge.ComponentModel.BoundList<ManualsTemplate>(
                        model.Where(x => x.Id > 0 && x.TemplateName.ToLower().Contains(this.xInputFilterSurname.textBox.Text.ToLower())).ToList()
                        );
                }
                else
                {
                    model = modelService.List(x => x.Id > 0).ToList();
                    this.dataGrid_Templates.DataSource = new DevAge.ComponentModel.BoundList<ManualsTemplate>(model);
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
                frmManageManualTemplatesAdd childForm = new frmManageManualTemplatesAdd(new ManualsTemplate());
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
