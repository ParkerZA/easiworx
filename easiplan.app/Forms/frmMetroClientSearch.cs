using Finx.App.MetroControls;
using easiplan.domain.Entities;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transitions;
using easiplan.domain.Views;

namespace Finx.App.Forms
{
    public partial class frmMetroClientSearch : baseSlidingForm
    {
        List<ClientDetailsView> clientDetailsList = new List<ClientDetailsView>();

        public virtual event EventHandler<EventArgs> ItemSelectedEvent;
        public virtual void InvokeItemSelectedEvent(object sender, EventArgs e)
        {
            ItemSelectedEvent?.Invoke(sender, e);
        }

        public frmMetroClientSearch(Form owner, int left, int top) : base(owner, left, top)
        {

            InitializeComponent();

            this.Text = string.Format("      Client Search");
            this.BackImage = global::easiplan.app.Properties.Resources.UserFind;
            this.BackImagePadding = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.BackMaxSize = 40;

            //Search Textbox
            metroTextBox_Search.ButtonClick += MetroTextBox_Search_ButtonClick;
            metroTextBox_Search.KeyDown += MetroTextBox_Search_KeyDown;
            metroTextBox_Search.KeyPress += TextBox_KeyPress;
            metroTextBox_Search.ClearClicked += MetroTextBox_Search_ClearClicked;
            metroTextBox_Search.WaterMark = "Enter a value to search ...";
            metroTextBox_Search.FontSize = MetroFramework.MetroTextBoxSize.Large;
            metroTextBox_Search.Font = Global.LargeFontText;

            //Reults view
            metroListView_SearchResults.DoubleClick += MetroListView_SearchResults_DoubleClick;

            //Search Button
            mbSearch.Click += MetroButton_Search_ButtonClick;

            this.Left = -400;
            this.Top = top;

            this.ResizeRedraw = true;
        }

        private void MetroListView_SearchResults_DoubleClick(object sender, EventArgs e)
        {
            MetroListView listView = sender as MetroListView;
            if (listView.SelectedItems.Count > 0)
                InvokeItemSelectedEvent(listView.SelectedItems[0].Tag, e);
        }

        private void MetroTextBox_Search_ClearClicked()
        {
            this.metroListView_SearchResults.Clear();
        }

        private void MetroTextBox_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                using (new AppWaitCursor(sender))
                {
                    MetroTextBox textBox = sender as MetroTextBox;
                    PopulateClientList(textBox.Text);


                }
            }
        }
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //suppress the beep sound
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void MetroTextBox_Search_ButtonClick(object sender, EventArgs e)
        {
            using (new AppWaitCursor(sender))
            {
                MetroTextBox textBox = sender as MetroTextBox;
                PopulateClientList(textBox.Text);
            }
        }

        private void MetroButton_Search_ButtonClick(object sender, EventArgs e)
        {
            using (new AppWaitCursor(sender))
            {

                PopulateClientList(this.metroTextBox_Search.Text);
            }
        }

        private void frmMetroClientSearch_Load(object sender, EventArgs e)
        {
            this.metroListView_SearchResults.Columns[0].Width = this.metroListView_SearchResults.Width;
        }

        private void PopulateClientList(string filter = "*")
        {
            if (this.metroRadioButton_BySurname.Checked)
                PopulateClientListByName(this.metroListView_SearchResults, filter);

            if (this.metroRadioButton_ByIdNumber.Checked)
                PopulateClientListById(this.metroListView_SearchResults, filter);

        }
        private void PopulateClientListByName(ListView lv, string filter = "*")
        {

            lv.Items.Clear();
            this.clientDetailsList.Clear();

            if (!string.IsNullOrEmpty(filter))
            {
                try
                {
                    if (filter.Contains(","))
                    {
                        string[] _filters = filter.Split(",".ToCharArray()[0]);

                        clientDetailsList.AddRange(Program.ClientDetailsService.ListView(x => x.ClientId > 0 && x.LastName.StartsWith(_filters[0]))
                            .Where(y => y.FirstName.ToLower().StartsWith(_filters[1].ToLower())
                             || y.MidName.ToLower().StartsWith(_filters[1].ToLower())
                            ).OrderBy(o => o.LastName).ThenBy(o => o.FirstName));

                    }
                    else
                    {
                        clientDetailsList.AddRange(Program.ClientDetailsService.ListView(x => x.ClientId > 0 && x.LastName.StartsWith(filter)
                           ).OrderBy(o => o.LastName).ThenBy(o => o.FirstName));


                    }
                }
                catch (Exception x)
                {

                }


                if (clientDetailsList != null)
                    foreach (var client in clientDetailsList)
                    {
                        var item = lv.Items.Add(client.ClientId.ToString(), string.Format("{0}, {1} {2} [{3}]", client.LastName, client.FirstName, client.ClientTitle, string.IsNullOrEmpty(client.IdentificationNo) ? "" : client.IdentificationNo), 0);

                        item.Tag = client.ClientId;
                    }
            }

        }
        private void PopulateClientListById(ListView lv, string filter = "*")
        {

            lv.Items.Clear();
            this.clientDetailsList.Clear();

            if (!string.IsNullOrEmpty(filter))
            {

                try
                {
                    clientDetailsList.AddRange(Program.ClientDetailsService.ListView(x => x.ClientId > 0 && (x.IdentificationNo.StartsWith(filter)))
                       .OrderBy(o => o.IdentificationNo)
                    );


                }
                catch (Exception x)
                {

                }


                if (clientDetailsList != null)
                    foreach (var client in clientDetailsList)
                    {

                        //  var item= lv.Items.Add(client.ClientId.ToString(), string.Format("{0}", string.IsNullOrEmpty(client.IdentificationNo) ? client.PassportNo : client.IdentificationNo), 0);
                        var item = lv.Items.Add(client.ClientId.ToString(), string.Format("{0}, {1} {2} [{3}]", client.LastName, client.FirstName, client.ClientTitle, string.IsNullOrEmpty(client.IdentificationNo) ? "" : client.IdentificationNo), 0);//client.PassportNo
                        item.Tag = client.ClientId;
                    }
            }

        }

        public override void swipe(bool show = true)
        {

            base.swipe(show);

            this.metroTextBox_Search.Clear();
            this.metroListView_SearchResults.Clear();
        }

        public bool CloseOnSelect { get { return this.metroCheckBox_CloseOnSelect.Checked; } }


    }
}
