using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace Finx.App.MetroUserControls
{
    public partial class MetroPanelExt:MetroPanel 
    {
        public MetroPanelExt()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
           // e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), this.ClientRectangle);

           
        }
    }
}
