using QSS.Components.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.Extensions
{
    public static class TreeViewExt
    {
        public static void SetTooltip(this System.Windows.Forms.Control button,string Icon,string Title,string Tooltip,bool ShowAlways = false)
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
            btnToolTip.Title =Title;
            btnToolTip.SetToolTip(button,Tooltip);
            btnToolTip.Alignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), "BottomLeft");

            button.Tag = btnToolTip;
        }

        public static void SetTooltip(this System.Windows.Forms.Control button, BalloonIcon Icon, string Title, string Tooltip, bool ShowAlways = false)
        {
            BalloonToolTip btnToolTip = button.Tag as BalloonToolTip;
            if (null != btnToolTip)
            {
                btnToolTip.RemoveAll();
            }

            btnToolTip = new BalloonToolTip();

            //Add tooltips for the buttons
            btnToolTip.Icon = Icon;
            btnToolTip.ShowAlways = ShowAlways;
            btnToolTip.Absolute = true;
            btnToolTip.Title = Title;
            btnToolTip.SetToolTip(button, Tooltip);
            btnToolTip.Alignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), "BottomLeft");

            button.Tag = btnToolTip;
        }

        public static void HideTooltip(this System.Windows.Forms.Control button)
        {
            try
            {
                BalloonToolTip btnToolTip = button.Tag as BalloonToolTip;
                btnToolTip.ShowAlways = false;
                btnToolTip.RemoveAll();
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }

        }

        public static void Initialise<T>(this TreeView treeView,IList<T> dataList)
        {
           
        }

        static void BuildTreeNode(TreeNode parent,string caption, [Optional]int callLevel)
        {

        }
    }
}
