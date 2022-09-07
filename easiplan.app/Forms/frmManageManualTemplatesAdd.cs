using Finx.App.Extensions;
using easiplan.domain.Entities;
using easiplan.domain.Services;
using my.domain.lib.core.Domain;
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
    public partial class frmManageManualTemplatesAdd : Form
    {
        ManualsTemplate model;
        ManualsTemplateService modelService = new ManualsTemplateService(Program.Repository);
        
        public frmManageManualTemplatesAdd(ManualsTemplate template)
        {
            InitializeComponent();

            model = template;
        }

        private void frmManageTemplatesAdd_Load(object sender, EventArgs e)
        {

            this.Text = "Manuals";

            this.xInput_TemplateName.TabIndex = 1;
            this.xInput_TemplateDescription.TabIndex = 2;
            this.xInput_SourceFilename.TabIndex = 3;
            this.xInput_IsActive.TabIndex = 4;

            this.xInput_TemplateName.Model = model;
            this.xInput_TemplateDescription.Model = model;
            this.xInput_SourceFilename.Model = model;
            this.xInput_IsActive.Model = model;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            if (model.Id > 0)
                model = modelService.Get(model.Id);

            model.Refresh();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (model.Id == 0)
                {
                    modelService.Add(model);
                }                

                model.TemplateBytes = File.ReadAllBytes(model.SourceFilename);

                if (model.TemplateBytes.Length > 400000)
                    throw new MyValidationException("The file is too large . Maximum file size is 4MB.");

                modelService.Update(model);
               

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
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxExt.ShowQuestion("Are you sure you wish to delete this template ?"))
                {
                    modelService.Remove(model.Id);


                    this.Close();
                }
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);

            }

        }

        private void button_Preview_Click(object sender, EventArgs e)
        {
            try
            {
                model.Calculate();
                //Show template
                var outputFilename = string.Format("{0}", model.Filename);
                var outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), outputFilename);

                if (File.Exists(outputPath))
                    File.Delete(outputPath);

                //Save doc to local folder
                File.WriteAllBytes(outputPath, model.TemplateBytes);

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
