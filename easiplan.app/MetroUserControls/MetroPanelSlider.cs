using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transitions;
using MetroFramework.Forms;

namespace Finx.App.MetroControls
{
    public partial class MetroPanelSlider : MetroFramework.Controls.MetroUserControl
    {
        Form _owner = null;
        bool _loaded = false;

        int _left = 0;
        int _top = 0;

        public EventHandler Closed;
        public EventHandler Shown;

        public virtual void closed(EventArgs e)
        {
            EventHandler handler = Closed;
            if (handler != null) handler(this, e);
        }
        public virtual void shown(EventArgs e)
        {
            EventHandler handler = Shown;
            if (handler != null) handler(this, e);
        }

        public MetroPanelSlider()
        {
            InitializeComponent();
            
        }
        public MetroPanelSlider(Form owner,int left,int top):this()
        {
            this.Visible = false;

            _left = left;
            _top = top;

            _owner = owner;
            owner.Controls.Add(this);
            this.BringToFront();

            owner.Resize += owner_Resize;

            this.Click += MetroPanelSlider_Click;

            ResizeForm();
        }

        private void owner_Resize(object sender, EventArgs e)
        {
            _loaded = false;
            ResizeForm();
        }

        private void ResizeForm()
        {
            //this.Width = _owner.Width/2;
            //this.Height = _owner.Height/2;
            //var x = _loaded ? _owner.Width - this.Width:_owner.Width                 ;
            var x = _loaded ? _left : -this.Width;
            var y =  _top;
            this.Location = new Point(x,y);
        }

        public void swipe(bool show = true)
        {
            this.Visible = true;
            _loaded = false;

            // var leftDestination = show ? _owner.Width - this.Width : _owner.Width;
            var leftDestination = show ? _left : -this.Width;

            Transition _transition = new Transitions.Transition(new TransitionType_EaseInEaseOut(600));
            _transition.add(this, "Left", leftDestination);
            _transition.run();
            while (this.Left != leftDestination)
            {
                Application.DoEvents();
            }

            if (!show)
            {
                closed(new EventArgs());
                _owner.Resize -= owner_Resize;

               // this.SendToBack();
               // _owner.Controls.Remove(this);
               // this.Dispose();
            }
            else {
                _loaded = true;
                ResizeForm();
                shown(new EventArgs());

               // this.BringToFront();
            }
        }

        private void MetroPanelSlider_Click(object sender, EventArgs e)
        {
            swipe(false);
        }
    }
}
