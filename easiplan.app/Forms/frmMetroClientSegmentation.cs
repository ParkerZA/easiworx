using Finx.App.Enums;
using Finx.App.Extensions;
using Finx.App.UserControls;
using easiplan.domain;
using easiplan.domain.Entities;
using MetroFramework;
using MetroFramework.Controls.Ext;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.Forms
{
    public partial class frmMetroClientSegmentation : MetroForm
    {
        ClientDetails model;
        
        public frmMetroClientSegmentation(int clientId)
        {
            InitializeComponent();

            model = Program.Repository.List<ClientDetails,int>(x=>x.ClientId == clientId).FirstOrDefault();

            if (model == null)
                throw new Exception("Invalid Client Id");

            #region Form Format
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ResizeRedraw = true;
            #endregion

            this.Text = model.Fullname;

            this.metroPanel_Rating.Initialise<ClientDetails>(model, cntr =>
                {
                    cntr.For(x => x.Rating, "Rating", new MetroText(50).ReadOnly(true).FontSize(Global.XLargeFontLable));
                }, left: 250, top: 10, labelWidth: 50, controlsLayout: ControlsLayout.Horizontal
            ).Format();

            this.metroPanel_Main.Initialise<ClientDetails>(model, cntr =>
            {               
                cntr.For(x => x.FValue, "Financial Value", new MetroNumberUpDownEditor(0,5));
                cntr.For(x => x.PValue, "Potential Value", new MetroNumberUpDownEditor(0, 5));
                cntr.For(x => x.Delegator, "Delegator", new MetroNumberUpDownEditor(0, 5));
                cntr.For(x => x.LikeMinded, "Like Minded", new MetroNumberUpDownEditor(0, 5));
                cntr.For(x => x.Influence, "Influence", new MetroNumberUpDownEditor(0, 5));
            }, left: 25, top: 10, labelWidth: 200,controlsLayout:ControlsLayout.Vertical
            , PropertyChangedHandler: propertyChanged_EventHandler
            ).Format();

            this.metroPanel_Main.Initialise<ClientDetails>(model, cntr =>
            {
                cntr.For(x => x.FValue, " ", new MetroStringEditor(50,32).ReadOnly(true));
                cntr.For(x => x.PValue, " ", new MetroStringEditor(50, 32).ReadOnly(true));
                cntr.For(x => x.Delegator, " ", new MetroStringEditor(50, 32).ReadOnly(true));
                cntr.For(x => x.LikeMinded, " ", new MetroStringEditor(50, 32).ReadOnly(true));
                cntr.For(x => x.Influence, " ", new MetroStringEditor(50, 32).ReadOnly(true));
            }, left: 250, top: 10, labelWidth: 50, controlsLayout: ControlsLayout.Vertical
          ).Format();
        }

        private void propertyChanged_EventHandler(object sender, PropertyChangedEventArgs e)
        {
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Repository.Update<ClientDetails, int>(model);

                this.Close();
            }
            catch(Exception x)
            {
                MessageBoxExt.ShowException(x);
            }

            
        }
    }
  
}
