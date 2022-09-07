using Finx.App.Extensions;
using Finx.Domain.Entities;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Finx.App.Forms
{
    public partial class frmAdminClientCommunication : MetroForm
    {
        IEnumerable<ClientDetails> model;
       
        public frmAdminClientCommunication()
        {
            InitializeComponent();

            this.Text = "Client Communication";

            model = Program.Repository.List<ClientDetails, int>(x => x.Id>0).ToList();

            this.xToolBarMenu1.tbCaption.Text = string.Format("{0}", "Client Communication");
            this.xToolBarMenu1.tbCaptionImage.Image = Properties.Resources.outlook_b_42;

            xToolBarMenu1.tbSave.Visible = false;
            xToolBarMenu1.tbEdit.Visible = false;
            xToolBarMenu1.tbRefresh.Visible = true;xToolBarMenu1.tbRefresh.Enabled = true;

            // xToolBarMenu1.EditClicked += toolStripButton_Edit_Click;
             xToolBarMenu1.RefreshClicked += toolStripButton_Refresh_Click; ;
            // xToolBarMenu1.SaveClicked += toolStripButton_AddToPortfolio_Click;
            xToolBarMenu1.CloseClicked += toolStripButton_Close_Click;

            xInput_BirthDate.ControlTypes = UserControls.ControlTypes.DatePicker;
            xInput_BirthDate.KeyPressed += XInput_BirthDate_KeyPressed;
            xInput_BirthDate.InitialiseControl();

            xInputFilterSurname.InitialiseControl();
            xInputFilterSurname.KeyPressed += XInputFilterSurname_KeyPressed;

            xCheckBoxList1.ListView.ItemChecked += ListView_ItemChecked;
            xCheckBoxList1.Button.Text = "Send";
            xCheckBoxList1.Button.Click += Button_Click;

           
        }

        private void XInput_BirthDate_KeyPressed(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Click(sender, e);
        }

        private void XInputFilterSurname_KeyPressed(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Click(sender, e);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (xCheckBoxList1.ListView.CheckedItems.Count == 0)
            {
                MessageBoxExt.ShowWarning("No clients were selected");
            }
            else
            {
                IList<ClientDetails> clientDetailsList = new List<ClientDetails>();
               
                foreach (ListViewItem item in xCheckBoxList1.ListView.CheckedItems)
                {
                    int clientId = int.Parse(item.Tag.ToString());
                    ClientDetails clientDetails = model.FirstOrDefault(x => x.ClientId == clientId);
                    clientDetails.Calculate();

                    clientDetails.RecipientAddress = item.ToolTipText.ToString();
                    
                    clientDetailsList.Add(clientDetails);
                 

                }

                frmAdminClientCommunicationAdd frm = new frmAdminClientCommunicationAdd(clientDetailsList);
                frm.ShowDialog(this);
            }
        }

        private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag == null)
                e.Item.Checked = false;
        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }

        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                xCheckBoxList1.ListView.Items.Clear();
                xCheckBoxList1.Caption = "";

                if (model != null)
                {
                    DateTime birthDate = (DateTime)xInput_BirthDate.Value;

                    var _userClients = model;

                    if (birthDate > DateTime.MinValue)
                    {
                        _userClients = model.Where(x => x.DateOfBirth.Day == birthDate.Day && x.DateOfBirth.Month == birthDate.Month ); // All clients including their spouses
                        xCheckBoxList1.Caption = string.Format("Clients and Spouses by Birth Date {0}",birthDate.ToString("dd MMM yyyy"));
                    }

                    if (!string.IsNullOrEmpty(xInputFilterSurname.Value.ToString()))
                    {
                        _userClients = model.Where(x => x.LastName.ToLower().StartsWith(xInputFilterSurname.Value.ToString().ToLower()) && x.ClientId >0); //only return clients and not their spouses
                        xCheckBoxList1.Caption = string.Format("Clients by Surname {0}", xInputFilterSurname.Value.ToString());
                    }

                    if (_userClients.Count()==0)
                    {
                           ListViewItem lvItem = xCheckBoxList1.ListView.Items.Add("0", string.Format(" no results found"), 0);
                    }
                    else
                    {
                        foreach (var client in _userClients.OrderBy(o => o.LastName).ThenBy(o => o.FirstName))
                        {
                            //Get Contact Details
                            ClientContacts _clientContacts = null;
                            if (client.ClientId > 0) //get client contact details
                                _clientContacts = Program.Repository.List<ClientContacts, int>(x => x.ClientId == client.ClientId).FirstOrDefault();

                            if (client.ClientId == 0) //get spouses contact details
                                _clientContacts = Program.Repository.List<ClientContacts, int>(x => x.ClientDetailsId == client.Id).FirstOrDefault();

                            if (_clientContacts != null)
                            {
                                if (!string.IsNullOrEmpty(_clientContacts.EMailAddr))
                                {
                                    ListViewItem lvItem = xCheckBoxList1.ListView.Items.Add(client.ClientId.ToString(), string.Format("{0},{1} {2} [{3}] ", client.LastName, client.FirstName, client.ClientTitle, _clientContacts.EMailAddr), 0);
                                    lvItem.Tag = client.ClientId;
                                    lvItem.ToolTipText = _clientContacts.EMailAddr;
                                }
                                else
                                {
                                    ListViewItem lvItem = xCheckBoxList1.ListView.Items.Add(client.ClientId.ToString(), string.Format("{0},{1} {2} [{3}] ", client.LastName, client.FirstName, client.ClientTitle, "no email"), 0);
                                    lvItem.Tag = null;
                                    lvItem.ForeColor = SystemColors.GrayText;
                                    lvItem.ToolTipText = "No email address found";
                                   
                                }
                            }
                            else
                            {
                                ListViewItem lvItem = xCheckBoxList1.ListView.Items.Add(client.ClientId.ToString(), string.Format("{0},{1} {2} [{3}] ", client.LastName, client.FirstName, client.ClientTitle, "no contact"), 0);
                                lvItem.Tag = null;
                                lvItem.ForeColor = SystemColors.GrayText;
                                lvItem.ToolTipText = "No contact details found";
                            }

                        }
                    }
                   
                }
            }
            catch (Exception x)
            {
            }
            finally
            {
                xToolBarMenu1.tbRefresh.Enabled = true;

            }
        }

        private void frmAdminClientCommunication_Load(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Click(sender, e);
        }
    }
}
