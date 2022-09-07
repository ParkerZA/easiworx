using QSS.Components.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace easiplan.app.Extensions
{
    public static class ControlExt
    {
        public static Control ShowTooltip(this Control control, string tooltip, string icon = "Error", string title = "")
        {
            BalloonToolTip btnToolTip = control.Tag as BalloonToolTip;
            if (null != btnToolTip)
            {
                btnToolTip.RemoveAll();
            }

            btnToolTip = new BalloonToolTip();

            //Add tooltips for the buttons
            btnToolTip.Icon = (BalloonIcon)Enum.Parse(typeof(BalloonIcon), icon);
            btnToolTip.ShowAlways = true;
            btnToolTip.Absolute = true;
            btnToolTip.Title = title;
            btnToolTip.SetToolTip(control, tooltip);
            btnToolTip.Alignment = (System.Drawing.ContentAlignment)Enum.Parse(typeof(System.Drawing.ContentAlignment), "BottomLeft");

            control.Tag = btnToolTip;

            return control;
        }
    }
}
