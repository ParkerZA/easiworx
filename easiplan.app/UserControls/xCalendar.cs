using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using System.Windows.Forms.Calendar;
using WindowsFormsCalendar;
using System.Drawing.Drawing2D;

namespace Finx.App.UserControls
{
    public partial class xCalendar : UserControl
    {
        Calendar calendar = new Calendar();
        public delegate void CalendarItemEventHandler(object sender, CalendarItemEventArgs e);
        public event CalendarItemEventHandler ItemClicked;

        public xCalendar()
        {
            InitializeComponent();

            
            calendar.Dock = DockStyle.Fill;
            calendar.AllowItemEdit = false;
            calendar.AllowItemResize = false;
            calendar.AllowNew = false;
            calendar.AllowDrop = false;
          
            calendar.Font = new Font("Verdana", 8);
            calendar.ItemsFont = new Font("verdana", 8);
            calendar.BackColor = Color.GhostWhite;
            calendar.AutoScroll = true;
            calendar.Scrollbars = CalendarScrollBars.Vertical;

            calendar.DayHeaderClick += Calendar_DayHeaderClick;
            calendar.ItemCreated += Calendar_ItemCreated;
            
            calendar.TimeScale = CalendarTimeScale.ThirtyMinutes;
            
            calendar.ItemDoubleClick += Calendar_ItemDoubleClick;

            this.BorderStyle = BorderStyle.FixedSingle;
            
            this.Controls.Add(calendar);
        }

        private void Calendar_ItemCreated(object sender, CalendarItemCancelEventArgs e)
        {
          
        }

        private void Calendar_DayHeaderClick(object sender, CalendarDayEventArgs e)
        {
           
        }

        private void Calendar_ItemDoubleClick(object sender, CalendarItemEventArgs e)
        {
            if (ItemClicked != null)
                ItemClicked(sender, e);
        }

        public DateTime CurrentDate
        {
            get { return calendar.SelectionStart; }
            set {
                calendar.SetViewRange(value, value);

               // calendar.SelectionStart = value;
            }
        }

        public void AddItem(ItemInfo item)
        {
            CalendarItem cal = new CalendarItem(calendar, item.StartTime, item.EndTime, item.Text);
            cal.Tag = item.Tag;

            if(calendar.ViewIntersects(cal))
                calendar.Items.Add(cal);

           
        }

        CalendarItem _CurrentItem = null;
        public CalendarItem CurrentItem
        {
            get { return _CurrentItem; }
            set { _CurrentItem = value; }
        }

        public void ScrollToTime(int offset)
        {
            calendar.TimeUnitsOffset = offset * (60 / 30) * -1; //-16 starts at 8AM //basic formula is (# hours after 12AM) * (60 / timeScaleMinutes) * -1

        }

    }

    public class ItemInfo
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public string Text;
        public int A;
        public int R;
        public int G;
        public int B;
        //HatchStyle pattern;
        //Color patternColor;
        public object Tag;
        public ItemInfo()
        { }

        public ItemInfo(DateTime startTime, DateTime endTime, string text, Color color,object tag)
        {
            StartTime = startTime;
            EndTime = endTime;
            Text = text;
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
            Tag = tag;
        }
    }
}
