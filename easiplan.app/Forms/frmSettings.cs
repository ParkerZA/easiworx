using Finx.App.Extensions;
using easiplan.domain.Entities;
using my.domain.lib.core.Registry;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Finx.App.Forms
{
    public partial class frmSettings : MetroForm
    {
        string BackgroundImage_Filename = string.Empty;
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            try
            {
                this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
                this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
                this.Style = MetroFramework.MetroColorStyle.Orange;

                this.Text = "Settings";

                this.DisplayHeader = false;
                try
                {
                    BackgroundImage_Filename = RegistryWrapper.ReadRegistry(Global.RegistryKey, "BackgroundImage");
                    if (!string.IsNullOrEmpty(BackgroundImage_Filename))
                        htmlPanel_BackgroundImg.BackgroundImage = Image.FromFile(BackgroundImage_Filename, true);
                }
                catch (Exception) { }

                this.xToolBarMenu1.tbCaption.Text = "Customise Settings";
                this.xToolBarMenu1.tbCaptionImage.Image = easiplan.app.Properties.Resources.cog_32;

                this.xToolBarMenu1.tbEdit.Visible = false; 
                this.xToolBarMenu1.tbRefresh.Visible = false;
              

                xToolBarMenu1.CloseClicked += XToolBarMenu1_CloseClicked;
                xToolBarMenu1.EditClicked += XToolBarMenu1_EditClicked;
                xToolBarMenu1.RefreshClicked += XToolBarMenu1_CancelClicked;
                xToolBarMenu1.SaveClicked += XToolBarMenu1_SaveClicked;

                this.button_loadPicture.Enabled = true;
             
                var fontsize = RegistryWrapper.ReadRegistry(Global.RegistryKey, "FontSize");
                if (fontsize != null)
                {
                    radioButton_SmallFont.Checked = fontsize == "1" ? true : false;
                    radioButton_NormalFont.Checked = fontsize == "2" ? true : false;
                    radioButton_LargeFont.Checked = fontsize == "3" ? true : false;
                }
                radioButton_SmallFont.CheckedChanged += RadioButton_SmallFont_CheckedChanged;
                radioButton_NormalFont.CheckedChanged += RadioButton_NormalFont_CheckedChanged;
                radioButton_LargeFont.CheckedChanged += RadioButton_LargeFont_CheckedChanged;
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
        }

        private void XToolBarMenu1_SaveClicked(object sender, EventArgs e)
        {

            try
            {
                RegistryWrapper.WriteRegistry(Global.RegistryKey, "BackgroundImage", BackgroundImage_Filename);
            }
            catch (Exception x)
            {
                MessageBoxExt.ShowException(x);
            }
            finally
            {
                this.xToolBarMenu1.tbSave.Enabled = false;
            }


        }

        private void XToolBarMenu1_CancelClicked(object sender, EventArgs e)
        {
           
            BackgroundImage_Filename = RegistryWrapper.ReadRegistry(Global.RegistryKey, "BackgroundImage");
            if (!string.IsNullOrEmpty(BackgroundImage_Filename))
                htmlPanel_BackgroundImg.BackgroundImage = Image.FromFile(BackgroundImage_Filename, true);
        }

        private void XToolBarMenu1_EditClicked(object sender, EventArgs e)
        {
          
        }

        private void XToolBarMenu1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_loadPicture_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Image Files|*.bmp;*.png;*.jpg";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {

                BackgroundImage_Filename = openFileDialog.FileName;

                htmlPanel_BackgroundImg.BackgroundImage = Image.FromFile(BackgroundImage_Filename, true);

                this.xToolBarMenu1.tbSave.Enabled = true;
            }
        }

        private void RadioButton_LargeFont_CheckedChanged(object sender, EventArgs e)
        {
            Global.GridFont = Global.LargeFont;
            Global.LableFont = Global.LargeFontLable;
            Global.TextFont = Global.LargeFontText;

            // ControlFont = Global.LargeFont;

            RegistryWrapper.WriteRegistry(Global.RegistryKey, "FontSize", "3");
        }

        private void RadioButton_NormalFont_CheckedChanged(object sender, EventArgs e)
        {
            Global.GridFont = Global.NormalFont;
            Global.LableFont = Global.NormalFontLable;
            Global.TextFont = Global.NormalFontText;

            // ControlFont = Global.NormalFont;

            RegistryWrapper.WriteRegistry(Global.RegistryKey, "FontSize", "2");
        }

        private void RadioButton_SmallFont_CheckedChanged(object sender, EventArgs e)
        {
            Global.GridFont = Global.SmallFont;
            Global.LableFont = Global.SmallFontLable;
            Global.TextFont = Global.SmallFontText;

            // ControlFont = Global.SmallFont;

            RegistryWrapper.WriteRegistry(Global.RegistryKey, "FontSize", "1");
        }

    }
}
