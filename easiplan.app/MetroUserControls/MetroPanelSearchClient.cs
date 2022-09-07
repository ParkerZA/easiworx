using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.MetroControls
{
    public partial class MetroPanelSearchClient : MetroPanelSlider
    {
        public virtual event EventHandler<EventArgs> SwipeEvent;
        public virtual event EventHandler<EventArgs> ItemSelectedEvent;

        public virtual void InvokeSwipeEvent(object sender, EventArgs e)
        {
            SwipeEvent?.Invoke(sender, e);
        }
        public virtual void InvokeItemSelectedEvent(object sender, EventArgs e)
        {
            ItemSelectedEvent?.Invoke(sender, e);
        }
        public MetroPanelSearchClient(Form form,int left,int top):base(form,left,top)
        {
            InitializeComponent();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            InvokeSwipeEvent(sender, e);
        }
    }
}
