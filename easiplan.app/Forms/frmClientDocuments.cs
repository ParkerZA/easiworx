
using easiplan.domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Finx.App.Forms
{
    public partial class frmClientDocuments : Form
    {
        Client model = new Client();
 
        public event PropertyChangedEventHandler PropertyChanged;
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(e.PropertyName));

            if(e.PropertyName=="DocumentsFolder")
                ShowFileView();
        }

        public frmClientDocuments(Client Client)
        {
            InitializeComponent();

            model = Client;
            model.PropertyChanged += Model_PropertyChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.splitContainer2.Panel1Collapsed = true;
            this.splitContainer2.IsSplitterFixed = true;

            this.xFileView1.ItemActivated += XFileView1_ItemActivated;
            
           
        }

        private void XInput_Filename_EnterKeyPressed(object sender, EventArgs e)
        {
            ShowFileView();
        }

     

        /// <summary>
        /// Open the selected file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XFileView1_ItemActivated(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;

            foreach (ListViewItem lvi in lv.SelectedItems)
            {
                try
                {
                    FileInfo fi = lvi.Tag as FileInfo;
                    if (fi != null)
                        Process.Start(fi.FullName);
                }
                catch
                {
                    continue;
                }
            }
        }

        private void frmClientDocuments_Load(object sender, EventArgs e)
        {

            xInput_ClientDocFolder.MappedField = "DocumentsFolder";
            xInput_ClientDocFolder.Model = model;

            ShowFileView();

        }

        private void ShowFileView()
        {
            if (!string.IsNullOrEmpty(model.DocumentsFolder))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(model.DocumentsFolder);
                if (dirInfo.Exists)
                {
                    var filter = tbFilter.Text;
                    xFileView1.ShowFiles(dirInfo, filter);
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xFileView1_Load(object sender, EventArgs e)
        {

        }

        private void xInput_Filename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowFileView();
            }

           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
