using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transitions;

namespace Finx.App.Forms
{
    public partial class baseSlidingForm : MetroForm
    {
        Form _owner = null;
        bool _loaded = false;

        public bool IsClosing = false;
        public bool IsShowing = false;

        int _left = 0;
        int _top = 0;

        public new EventHandler Closed;
        public new EventHandler Shown;

        public virtual event EventHandler<EventArgs> SwipeEvent;
        public virtual void InvokeSwipeEvent(object sender, EventArgs e)
        {
            SwipeEvent?.Invoke(sender, e);
        }

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

        public baseSlidingForm()
        {
            InitializeComponent();

            this.MinimumSize = new Size(40, 60);
        }

        public baseSlidingForm(Form owner, int left, int top):this()
        {
            this.Visible = false;

            _left = left;
            _top = top;

            this.TopLevel = false;
            this.BorderStyle = MetroFormBorderStyle.FixedSingle;

            this.ShadowType = MetroFormShadowType.None;


            _owner = owner;
            owner.Controls.Add(this);

            this.BringToFront();

            owner.Resize += owner_Resize;

            _loaded = false;
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
            var y = _top;
            this.Location = new Point(x, y);
        }

        public virtual void swipe(bool show = true)
        {


            _loaded = false;
            IsShowing = false;

            //  ResizeForm();

            this.Visible = true;

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
            else
            {
                _loaded = true;
                IsShowing = true;
                ResizeForm();
                shown(new EventArgs());

                // this.BringToFront();
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            if (!IsClosing)
            {
                InvokeSwipeEvent(this, new EventArgs());

                e.Cancel = true;
            }

            base.OnClosing(e);
        }

        private void baseSlidingForm_Load(object sender, EventArgs e)
        {

        }
    }
}
