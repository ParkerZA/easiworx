using QSS.Components.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.Extensions
{
    public static class ToolstripExt
    {
        public static ToolStrip SetDefaultStyle(this ToolStrip toolstrip, bool IncludeItems = false)
        {
            toolstrip.BackColor = Color.White;
            toolstrip.Dock = System.Windows.Forms.DockStyle.Fill;
            toolstrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            toolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolstrip.ImageScalingSize = new System.Drawing.Size(24, 24);

            if (IncludeItems)
            {
                foreach (ToolStripItem item in toolstrip.Items)
                {
                    if (item.GetType() == typeof(ToolStripButton))
                    {
                        ToolStripButton tb = item as ToolStripButton;
                        tb.SetDefaultStyle();
                    }
                }
            }
            return toolstrip;
        }

        public static ToolStripButton SetTooltip(this System.Windows.Forms.ToolStripButton button, string Icon, string Title, string Tooltip, bool ShowAlways = false)
        {
            BalloonToolTip btnToolTip = button.Tag as BalloonToolTip;
            if (null != btnToolTip)
            {
                btnToolTip.RemoveAll();
            }

            btnToolTip = new BalloonToolTip();

            //Add tooltips for the buttons
            btnToolTip.Icon = (BalloonIcon)Enum.Parse(typeof(BalloonIcon), Icon);
            btnToolTip.ShowAlways = ShowAlways;
            btnToolTip.Absolute = true;
            btnToolTip.Title = Title;
            // btnToolTip.SetToolTip(button,Tooltip);
            btnToolTip.Alignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), "BottomLeft");

            button.Tag = btnToolTip;
            button.ToolTipText = Tooltip;
            button.AutoToolTip = false;

            return button;
        }

        public static ToolStripButton SetDefaultStyle(this System.Windows.Forms.ToolStripButton button)
        {
            button.BackColor = Color.Transparent;
            button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            //button.Alignment = System.Windows.Forms.ToolStripItemAlignment.Left;
            button.AutoSize = false;
            button.Font = new System.Drawing.Font("Segoe UI", 9F);
            button.Size = new System.Drawing.Size(65, 50);
            button.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button.ImageTransparentColor = System.Drawing.Color.Magenta;
            button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;


            return button;
        }
    }
}
