using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Finx.App.Extensions;
using easiplan.domain.Views;
using Finx.App;
using Finx.App.Enums;
using Finx.App.Forms;
using easiplan.domain.Entities;
using easiplan.app.Extensions;
using easiplan.app.Services;
using System.Diagnostics;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace easiplan.app.Forms
{
    public partial class frmClientDashboard : MetroForm
    {
        private int _clientId;
        private ClientDetailsView _clientDetails;
        private bool _isLoadingEmails = false;
        private bool _autoRefreshEnabled = true;
        private int _refreshIntervalMinutes = 3;
        private int currentEmailCount = 0;
        private string _currentEasiWorxUser => Program.User?.Username ?? "DefaultUser";

        private Timer emailRefreshTimer;
        private DateTime lastRefreshTime = DateTime.MinValue;
        private bool autoRefreshEnabled = true;
        private int refreshIntervalMinutes = 1;
        private int lastEmailCount = 0;
        private DataGridView inboxGrid;
        private DataGridView outboxGrid;
        private int inboxCurrentPage = 1;
        private int outboxCurrentPage = 1;
        private int pageSize = 20;
        private List<ClientEmailMessage> allInboxEmails = new List<ClientEmailMessage>();
        private List<ClientEmailMessage> allOutboxEmails = new List<ClientEmailMessage>();

        // Modern UI Colors
        public static class ModernColors
        {
            public static Color Primary = ColorTranslator.FromHtml("#3b82f6");
            public static Color PrimaryHover = ColorTranslator.FromHtml("#2563eb");
            public static Color Background = ColorTranslator.FromHtml("#f8fafc");
            public static Color Surface = Color.White;
            public static Color TextPrimary = ColorTranslator.FromHtml("#1e293b");
            public static Color TextSecondary = ColorTranslator.FromHtml("#64748b");
            public static Color Border = ColorTranslator.FromHtml("#e2e8f0");
            public static Color BorderLight = ColorTranslator.FromHtml("#f1f5f9");
            public static Color Success = ColorTranslator.FromHtml("#10b981");
            public static Color SuccessLight = ColorTranslator.FromHtml("#dcfce7");
            public static Color Warning = ColorTranslator.FromHtml("#f59e0b");
            public static Color WarningLight = ColorTranslator.FromHtml("#fef3c7");
            public static Color Error = ColorTranslator.FromHtml("#ef4444");
        }

        public class ClientEmailMessage
        {
            public string Id { get; set; }
            public string Subject { get; set; }
            public string FromName { get; set; }
            public string FromAddress { get; set; }
            public string BodyPreview { get; set; }
            public DateTime ReceivedDate { get; set; }
            public bool IsRead { get; set; }
            public string Direction { get; set; }
            public string EntryID { get; set; }
            public string StoreID { get; set; }
            public string WebLink { get; set; }
            public string OutlookId { get; set; }
        }

        // Modern UI components
        private Panel clientSidebar;
        private Panel mainContent;
        private TabControl tabControl;
        //private DataGridView emailListView;
        private Panel statsPanel;

        public frmClientDashboard(int clientId = 0)
        {
            InitializeComponent();
            _clientId = clientId;

            SetupModernUI();
            LoadClientData();
        }

        private void SetupModernUI()
        {
            // Form setup with modern styling - FIXED MetroFramework properties  
            this.Text = "Client Dashboard";
            this.Size = new Size(1400, 900);
            this.MinimumSize = new Size(1200, 700);
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = ModernColors.Background;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Use correct MetroFramework properties  
            this.BorderStyle = MetroFormBorderStyle.FixedSingle; // FIXED CS0176 by qualifying with type name  
            this.ShadowType = MetroFormShadowType.None;

            CreateModernLayout();
        }

        private void CreateModernLayout()
        {
            this.SuspendLayout();
            this.Controls.Clear();

            // Sidebar - fixed position and size
            clientSidebar = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(280, this.ClientSize.Height),
                BackColor = ModernColors.Surface,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left  // Only anchor to resize vertically
            };

            // Sidebar border
            clientSidebar.Paint += (s, e) =>
            {
                using (var pen = new Pen(ModernColors.Border, 1))
                {
                    e.Graphics.DrawLine(pen, clientSidebar.Width - 1, 0, clientSidebar.Width - 1, clientSidebar.Height);
                }
            };

            this.Controls.Add(clientSidebar);

            // Main content area - positioned to the right of sidebar
            mainContent = new Panel
            {
                Location = new Point(280, 0),  // Start exactly where sidebar ends
                Size = new Size(this.ClientSize.Width - 280, this.ClientSize.Height),
                BackColor = ModernColors.Background,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right  // Resize with form
            };

            this.Controls.Add(mainContent);

            // Header panel - positioned at top of main content
            var headerPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(mainContent.Width, 60),
                BackColor = ModernColors.Surface,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Header border
            headerPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(ModernColors.Border, 1))
                {
                    e.Graphics.DrawLine(pen, 0, headerPanel.Height - 1, headerPanel.Width, headerPanel.Height - 1);
                }
            };

            // Title
            var titleLabel = new Label
            {
                Text = "Client Dashboard",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(20, 18),
                Size = new Size(300, 25),
                BackColor = Color.Transparent
            };
            headerPanel.Controls.Add(titleLabel);

            // Refresh button - positioned from right edge
            var refreshButton = new ModernButton("🔄 Refresh", false)
            {
                Size = new Size(80, 32),
                Location = new Point(headerPanel.Width - 175, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            refreshButton.Click += async (s, e) => await RefreshEmailsManually();
            headerPanel.Controls.Add(refreshButton);

            var closeButton = new ModernButton("✕ Close", false)
            {
                Size = new Size(80, 32),
                Location = new Point(headerPanel.Width - 90, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            closeButton.Click += (s, e) => this.Close();
            headerPanel.Controls.Add(closeButton);

            mainContent.Controls.Add(headerPanel);

            // Tab control - positioned below header
            tabControl = new TabControl
            {
                Location = new Point(0, 60),  // Start below header
                Size = new Size(mainContent.Width, mainContent.Height - 60),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            SetupTabs();
            mainContent.Controls.Add(tabControl);

            // Handle form resize to update sizes
            this.Resize += (s, e) =>
            {
                if (clientSidebar != null && mainContent != null)
                {
                    clientSidebar.Height = this.ClientSize.Height;
                    mainContent.Size = new Size(this.ClientSize.Width - 280, this.ClientSize.Height);
                }
            };

            this.ResumeLayout();
        }

        private void SetupTabs()
        {
            // Overview tab
            var overviewTab = new TabPage("Overview");
            SetupOverviewTab(overviewTab);
            tabControl.TabPages.Add(overviewTab);

            // Emails tab
            var emailsTab = new TabPage("Emails");
            SetupEmailsTab(emailsTab);
            tabControl.TabPages.Add(emailsTab);

            // Add other tabs with basic setup
            var tasksTab = new TabPage("Tasks");
            SetupTasksTab(tasksTab);
            tabControl.TabPages.Add(tasksTab);

            tabControl.TabPages.Add(new TabPage("Calls"));
            tabControl.TabPages.Add(new TabPage("WhatsApp"));
            tabControl.TabPages.Add(new TabPage("Documents"));
            tabControl.TabPages.Add(new TabPage("Notes"));
        }

        private Panel CreateClientSidebar()
        {
            var sidebar = new ModernPanel
            {
                Width = 300,
                Dock = DockStyle.Left,
                BackColor = ModernColors.Surface,
                Padding = new Padding(16)
            };

            sidebar.Paint += (s, e) =>
            {
                using (var pen = new Pen(ModernColors.Border, 1))
                {
                    e.Graphics.DrawLine(pen, sidebar.Width - 1, 0, sidebar.Width - 1, sidebar.Height);
                }
            };

            return sidebar;
        }

        private void UpdateClientSidebar()
        {
            if (_clientDetails == null || clientSidebar == null) return;

            clientSidebar.Controls.Clear();

            // Client name - fixed position
            var nameLabel = new Label
            {
                Text = _clientDetails.Fullname,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(16, 20),
                Size = new Size(240, 30),
                BackColor = Color.Transparent
            };
            clientSidebar.Controls.Add(nameLabel);

            // Client ID - fixed position
            var idLabel = new Label
            {
                Text = $"ID: {_clientDetails.IdentificationNo}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = ModernColors.TextSecondary,
                Location = new Point(16, 55),
                Size = new Size(240, 20),
                BackColor = Color.Transparent
            };
            clientSidebar.Controls.Add(idLabel);

            // Contact header - fixed position
            var contactHeader = new Label
            {
                Text = "CONTACT INFORMATION",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = ModernColors.TextSecondary,
                Location = new Point(16, 90),
                Size = new Size(240, 18),
                BackColor = Color.Transparent
            };
            clientSidebar.Controls.Add(contactHeader);

            // Email - fixed position
            var emailLabel = new Label
            {
                Text = $"Email: {_clientDetails.RecipientAddress ?? "N/A"}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(16, 115),
                Size = new Size(240, 20),
                BackColor = Color.Transparent
            };
            clientSidebar.Controls.Add(emailLabel);

            // Cell - fixed position
            var cellLabel = new Label
            {
                Text = $"Cell: {_clientDetails.RecipientCell ?? "N/A"}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(16, 140),
                Size = new Size(240, 20),
                BackColor = Color.Transparent
            };
            clientSidebar.Controls.Add(cellLabel);

            // Rating - add this new label
            var ratingLabel = new Label
            {
                Text = $"Rating: {_clientDetails.Rating ?? "N/A"}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(16, 165),
                Size = new Size(240, 20),
                BackColor = Color.Transparent
            };
            clientSidebar.Controls.Add(ratingLabel);

            // Buttons - update positions to accommodate rating
            var emailBtn = new ModernButton("📧 Email", true)
            {
                Size = new Size(110, 32),
                Location = new Point(16, 200) // moved down from 180
            };
            emailBtn.Click += BtnEmail_Click;
            clientSidebar.Controls.Add(emailBtn);

            var callBtn = new ModernButton("📞 Call", false)
            {
                Size = new Size(110, 32),
                Location = new Point(136, 200)
            };
            callBtn.Click += BtnCall_Click;
            clientSidebar.Controls.Add(callBtn);

            var smsBtn = new ModernButton("💬 SMS", false)
            {
                Size = new Size(110, 32),
                Location = new Point(16, 240)
            };
            smsBtn.Click += BtnSMS_Click;
            clientSidebar.Controls.Add(smsBtn);

            var testBtn = new ModernButton("🔍 Test", false)
            {
                Size = new Size(110, 32),
                Location = new Point(136, 240)
            };
            testBtn.Click += BtnTestEmails_Click;
            clientSidebar.Controls.Add(testBtn);

            var outlookBtn = new ModernButton("📮 Connect Outlook", false)
            {
                Size = new Size(240, 32),
                Location = new Point(16, 280)
            };
            outlookBtn.Click += async (s, e) => await ConnectToOutlook();
            clientSidebar.Controls.Add(outlookBtn);
        }

        private Panel CreateClientHeader()
        {
            var header = new Panel
            {
                Size = new Size(272, 160),
                BackColor = Color.Transparent
            };

            if (_clientDetails == null) return header;

            // Get initials for avatar
            var initials = GetClientInitials(_clientDetails.Fullname);

            // Client avatar
            var avatar = new ModernAvatar(initials)
            {
                Size = new Size(80, 80),
                Location = new Point((header.Width - 80) / 2, 0)
            };
            header.Controls.Add(avatar);

            // Client name - FIXED FontStyle.Bold reference
            var nameLabel = new Label
            {
                Text = _clientDetails.Fullname,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = ModernColors.TextPrimary,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(272, 25),
                Location = new Point(0, avatar.Bottom + 16)
            };
            header.Controls.Add(nameLabel);

            // Client ID
            var idLabel = new Label
            {
                Text = $"ID: {_clientDetails.IdentificationNo}",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = ModernColors.TextSecondary,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(272, 20),
                Location = new Point(0, nameLabel.Bottom + 4)
            };
            header.Controls.Add(idLabel);

            // Add separator line
            header.Paint += (s, e) =>
            {
                using (var pen = new Pen(ModernColors.Border, 1))
                {
                    e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
                }
            };

            return header;
        }

        private string GetClientInitials(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return "??";

            var parts = fullName.Split(' ');
            if (parts.Length >= 2)
            {
                return $"{parts[0][0]}{parts[1][0]}".ToUpper();
            }
            else if (parts.Length == 1 && parts[0].Length >= 2)
            {
                return parts[0].Substring(0, 2).ToUpper();
            }
            return fullName.Substring(0, Math.Min(2, fullName.Length)).ToUpper();
        }

        private Panel CreateInfoSection(string title, (string label, string value)[] items)
        {
            var section = new Panel
            {
                Size = new Size(272, 32 + items.Length * 32),
                BackColor = Color.Transparent
            };

            // Section title - FIXED FontStyle.Bold reference
            var titleLabel = new Label
            {
                Text = title.ToUpper(),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = ModernColors.TextSecondary,
                Size = new Size(272, 20),
                Location = new Point(0, 0)
            };
            section.Controls.Add(titleLabel);

            // Info items
            for (int i = 0; i < items.Length; i++)
            {
                var itemPanel = new Panel
                {
                    Size = new Size(272, 32),
                    Location = new Point(0, 24 + i * 32),
                    BackColor = Color.Transparent
                };

                var labelControl = new Label
                {
                    Text = items[i].label,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    ForeColor = ModernColors.TextSecondary,
                    Size = new Size(120, 20),
                    Location = new Point(0, 6),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                itemPanel.Controls.Add(labelControl);

                // FIXED: Changed FontStyle.Medium to FontStyle.Bold (Medium doesn't exist)
                var valueControl = new Label
                {
                    Text = items[i].value,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = ModernColors.TextPrimary,
                    Size = new Size(152, 20),
                    Location = new Point(120, 6),
                    TextAlign = ContentAlignment.MiddleRight
                };
                itemPanel.Controls.Add(valueControl);

                // Add subtle border
                if (i < items.Length - 1)
                {
                    itemPanel.Paint += (s, e) =>
                    {
                        using (var pen = new Pen(ModernColors.BorderLight, 1))
                        {
                            e.Graphics.DrawLine(pen, 0, itemPanel.Height - 1, itemPanel.Width, itemPanel.Height - 1);
                        }
                    };
                }

                section.Controls.Add(itemPanel);
            }

            return section;
        }

        private Panel CreateQuickActions()
        {
            var panel = new Panel
            {
                Size = new Size(272, 120),
                BackColor = Color.Transparent
            };

            var emailBtn = new ModernButton("📧 Email", true)
            {
                Size = new Size(130, 36),
                Location = new Point(0, 0)
            };
            emailBtn.Click += BtnEmail_Click;
            panel.Controls.Add(emailBtn);

            var callBtn = new ModernButton("📞 Call", false)
            {
                Size = new Size(130, 36),
                Location = new Point(138, 0)
            };
            callBtn.Click += BtnCall_Click;
            panel.Controls.Add(callBtn);

            var smsBtn = new ModernButton("💬 SMS", false)
            {
                Size = new Size(130, 36),
                Location = new Point(0, 42)
            };
            smsBtn.Click += BtnSMS_Click;
            panel.Controls.Add(smsBtn);

            var testEmailsBtn = new ModernButton("🔍 Test Emails", false)
            {
                Size = new Size(130, 36),
                Location = new Point(138, 42)
            };
            testEmailsBtn.Click += BtnTestEmails_Click;
            panel.Controls.Add(testEmailsBtn);

            var outlookBtn = new ModernButton("📮 Connect Outlook", false)
            {
                Size = new Size(272, 36),
                Location = new Point(0, 84)
            };
            outlookBtn.Click += async (s, e) => await ConnectToOutlook();
            panel.Controls.Add(outlookBtn);

            return panel;
        }

        private Panel CreateMainContent()
        {
            var content = new Panel
            {
                Dock = DockStyle.Fill, // This will fill remaining space after sidebar
                BackColor = ModernColors.Background,
                Padding = new Padding(0)
            };

            // Create header
            var header = CreateContentHeader();
            content.Controls.Add(header);

            // Create tab control that fills remaining space
            tabControl = CreateModernTabControl();
            tabControl.Dock = DockStyle.Fill;
            content.Controls.Add(tabControl);

            return content;
        }

        private Panel CreateContentHeader()
        {
            var header = new ModernPanel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = ModernColors.Surface,
                Padding = new Padding(16)
            };

            header.Paint += (s, e) =>
            {
                using (var pen = new Pen(ModernColors.Border, 1))
                {
                    e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
                }
            };

            // Title
            var title = new Label
            {
                Text = "Client Dashboard",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(16, 18),
                Size = new Size(250, 24),
                AutoSize = false
            };
            header.Controls.Add(title);

            // Button container to ensure proper positioning
            var buttonContainer = new Panel
            {
                Height = 32,
                Width = 170,  // Width for Refresh (85) + Close (75) + gap (10)
                Location = new Point(header.Width - 186, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent
            };

            var refreshBtn = new ModernButton("🔄 Refresh", false)
            {
                Size = new Size(80, 32),
                Location = new Point(0, 0)
            };
            refreshBtn.Click += async (s, e) => await RefreshEmailsManually();
            buttonContainer.Controls.Add(refreshBtn);

            var closeBtn = new ModernButton("✕ Close", false)
            {
                Size = new Size(80, 32),
                Location = new Point(90, 0)  // 80 (refresh width) + 10 (gap)
            };
            closeBtn.Click += (s, e) => this.Close();
            buttonContainer.Controls.Add(closeBtn);

            header.Controls.Add(buttonContainer);

            return header;
        }


        private TabControl CreateModernTabControl()
        {
            var tabCtrl = new TabControl();
            tabCtrl.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            tabCtrl.ForeColor = Color.Black;  // Force black text
            tabCtrl.BackColor = Color.White;

            // Create tabs
            var overviewTab = new TabPage("Overview");
            var emailsTab = new TabPage("Emails");
            var tasksTab = new TabPage("Tasks");
            var callsTab = new TabPage("Calls");
            var whatsappTab = new TabPage("WhatsApp");
            var documentsTab = new TabPage("Documents");
            var notesTab = new TabPage("Notes");

            // Setup tab content
            SetupOverviewTab(overviewTab);
            SetupEmailsTab(emailsTab);
            SetupTasksTab(tasksTab);
            SetupCallsTab(callsTab);
            SetupWhatsAppTab(whatsappTab);
            SetupDocumentsTab(documentsTab);
            SetupNotesTab(notesTab);

            tabCtrl.TabPages.AddRange(new[] {
        overviewTab, emailsTab, tasksTab, callsTab, whatsappTab, documentsTab, notesTab
    });

            tabCtrl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            return tabCtrl;
        }

        private void SetupOverviewTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Stats panel - fixed position
            statsPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(600, 100),
                BackColor = Color.Transparent
            };
            tab.Controls.Add(statsPanel);

            // Recent Emails header
            var emailHeader = new Label
            {
                Text = "Recent Emails",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 140),
                Size = new Size(200, 25),
                ForeColor = ModernColors.TextPrimary,
                BackColor = Color.Transparent
            };
            tab.Controls.Add(emailHeader);

            // Recent Emails list - fixed position and size
            var emailList = new ListView
            {
                Location = new Point(20, 170),
                Size = new Size(700, 140),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                BackColor = ModernColors.Surface,
                ForeColor = ModernColors.TextPrimary,
                Font = new Font("Segoe UI", 9F),
                //BorderStyle = BorderStyle.FixedSingle
            };

            emailList.Columns.Add("Subject", 300);
            emailList.Columns.Add("From", 200);
            emailList.Columns.Add("Date", 120);
            tab.Controls.Add(emailList);

            // Recent Tasks header
            var tasksHeader = new Label
            {
                Text = "Recent Tasks",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 330),
                Size = new Size(200, 25),
                ForeColor = ModernColors.TextPrimary,
                BackColor = Color.Transparent
            };
            tab.Controls.Add(tasksHeader);

            // Recent Tasks list - made wider to show all columns
            var tasksList = new ListView
            {
                Location = new Point(20, 360),
                Size = new Size(900, 180),  // Increased width from 700 to 900
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                BackColor = ModernColors.Surface,
                ForeColor = ModernColors.TextPrimary,
                Font = new Font("Segoe UI", 9F),
                //BorderStyle = BorderStyle.FixedSingle
            };

            tasksList.Columns.Add("Type", 100);
            tasksList.Columns.Add("Name", 250);
            tasksList.Columns.Add("Status", 120);
            tasksList.Columns.Add("Created", 110);  // Slightly wider
            tasksList.Columns.Add("Updated", 110);  // Slightly wider
            tasksList.Columns.Add("By", 150);       // Wider to show full email addresses
            tab.Controls.Add(tasksList);

            // Load data
            LoadEmailsIntoOverview(emailList);
            LoadTasksIntoOverview(tasksList);
            UpdateStatsPanel();
        }



        //private async 
        //Task
        private async void LoadEmailsIntoOverview(ListView emailList)
        {
            try
            {
                string clientEmail = _clientDetails?.RecipientAddress;

                if (!string.IsNullOrEmpty(clientEmail) && EmailService.IsAvailable())
                {
                    var emailService = new EmailService();
                    var allEmails = await emailService.GetEmailsForClient(clientEmail, 5); // Get recent 5 emails

                    emailList.Items.Clear();
                    foreach (var email in allEmails)
                    {
                        var item = new ListViewItem(email.Subject ?? "No Subject");
                        item.SubItems.Add(email.FromName ?? "Unknown");
                        item.SubItems.Add(email.ReceivedDate.ToString("dd-MMM"));
                        item.Tag = email;

                        if (!email.IsRead)
                        {
                            item.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                            item.BackColor = ColorTranslator.FromHtml("#eff6ff");
                        }

                        emailList.Items.Add(item);
                    }
                }
                else
                {
                    emailList.Items.Clear();
                    var item = new ListViewItem(string.IsNullOrEmpty(clientEmail) ? "No client email address" : "Outlook not connected");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    emailList.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                emailList.Items.Clear();
                var item = new ListViewItem($"Error: {ex.Message}");
                item.SubItems.Add("");
                item.SubItems.Add("");
                emailList.Items.Add(item);
            }
        }

        private List<ClientEmailMessage> ConvertToDisplayEmails(List<easiplan.app.Services.EmailMessage> serviceEmails)
        {
            var displayEmails = new List<ClientEmailMessage>();

            foreach (var email in serviceEmails)
            {
                var clientEmail = new ClientEmailMessage
                {
                    Id = email.Id,
                    Subject = email.Subject,
                    FromName = email.FromName,
                    FromAddress = email.ToAddresses,
                    BodyPreview = email.BodyPreview,
                    ReceivedDate = email.ReceivedDate,
                    IsRead = email.IsRead,
                    Direction = email.Direction,
                    WebLink = email.WebLink  // Make sure this is not null
                };

                // Debug each email
                if (string.IsNullOrEmpty(clientEmail.WebLink))
                {
                    Program.Logger?.Warn($"Email '{email.Subject}' has no WebLink");
                }

                displayEmails.Add(clientEmail);
            }

            return displayEmails;
        }

        private void UpdateStatsPanel()
        {
            if (statsPanel == null) return;

            try
            {
                Program.Logger?.Info($"UpdateStatsPanel called with currentEmailCount: {currentEmailCount}");

                // Clear and rebuild the entire stats panel
                statsPanel.SuspendLayout();
                statsPanel.Controls.Clear();

                int openTasks = GetOpenTasksCount();

                var stats = new[]
                {
            (currentEmailCount.ToString(), "Total Emails", "📧"),
            (openTasks.ToString(), "Open Tasks", "📋")
        };

                int cardWidth = Math.Max(150, (statsPanel.Width - 30) / 2);

                for (int i = 0; i < stats.Length; i++)
                {
                    var card = CreateStatCard(stats[i].Item1, stats[i].Item2, stats[i].Item3);
                    card.Location = new Point(i * (cardWidth + 15), 0);
                    card.Size = new Size(cardWidth, 100);
                    statsPanel.Controls.Add(card);
                }

                statsPanel.ResumeLayout(true);
                statsPanel.Invalidate();
                statsPanel.Refresh();

                Program.Logger?.Info($"Stats panel updated - showing {currentEmailCount} emails");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error updating stats panel", ex);
            }
        }

        private int GetTotalEmailCount()
        {
            try
            {
                int inboxCount = 0;
                int outboxCount = 0;

                if (inboxGrid != null && inboxGrid.Rows != null)
                {
                    inboxCount = inboxGrid.Rows.Count;
                    Program.Logger?.Info($"Inbox grid has {inboxCount} rows");
                }
                else
                {
                    Program.Logger?.Info("Inbox grid is null or has no rows collection");
                }

                if (outboxGrid != null && outboxGrid.Rows != null)
                {
                    outboxCount = outboxGrid.Rows.Count;
                    Program.Logger?.Info($"Outbox grid has {outboxCount} rows");
                }
                else
                {
                    Program.Logger?.Info("Outbox grid is null or has no rows collection");
                }

                int totalCount = inboxCount + outboxCount;
                Program.Logger?.Info($"Total email count: {totalCount}");

                return totalCount;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error getting total email count", ex);
                return 0;
            }
        }

        private int GetOpenTasksCount()
        {
            try
            {
                if (_clientId <= 0) return 0;

                // Get actual open tasks for this client
                var openTasks = Program.Repository.List<Instruction, int>(x =>
                    x.ClientId == _clientId &&
                    x.Status != InstructionStatus.Completed.ToString() &&
                    x.Status != InstructionStatus.AddCompleted.ToString() &&
                    x.Status != InstructionStatus.UpdateCompleted.ToString() &&
                    x.Status != InstructionStatus.Cancelled.ToString() &&
                    x.Status != InstructionStatus.AddCancelled.ToString() &&
                    x.Status != InstructionStatus.UpdateCancelled.ToString()
                );

                return openTasks.Count();
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error getting open tasks count", ex);
                return 0;
            }
        }

        private ModernPanel CreateStatCard(string number, string label, string icon)
        {
            var card = new ModernPanel
            {
                BackColor = ModernColors.Surface,
                Padding = new Padding(20)
            };

            var iconLabel = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 16F, FontStyle.Regular),
                ForeColor = ModernColors.Primary,
                Size = new Size(30, 25),
                Location = new Point(20, 15)
            };
            card.Controls.Add(iconLabel);

            // FIXED FontStyle.Bold reference
            var numberLabel = new Label
            {
                Text = number,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = ModernColors.TextPrimary,
                Size = new Size(card.Width - 40, 30),
                Location = new Point(20, 40)
            };
            card.Controls.Add(numberLabel);

            var labelText = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = ModernColors.TextSecondary,
                Size = new Size(card.Width - 40, 20),
                Location = new Point(20, numberLabel.Bottom + 4)
            };
            card.Controls.Add(labelText);

            return card;
        }


        private List<ClientEmailMessage> CreateTestEmailsForOverview(string clientEmail, int count)
        {
            var emails = new List<ClientEmailMessage>();
            for (int i = 0; i < count; i++)
            {
                emails.Add(new ClientEmailMessage
                {
                    Subject = $"Test Email {i + 1}",
                    FromName = "Test Sender",
                    ReceivedDate = DateTime.Now.AddDays(-i),
                    IsRead = i % 2 == 0,
                    BodyPreview = "Preview text...",
                    Id = $"test_{i}",
                    Direction = i % 2 == 0 ? "Received" : "Sent"
                });
            }
            return emails;
        }


        private void SetupEmailsTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Sub-tab control - make sure it fits properly in the parent tab
            var emailTabControl = new TabControl
            {
                Location = new Point(5, 5),
                Size = new Size(Math.Max(800, tab.Width - 10), Math.Max(500, tab.Height - 10)),
                Font = new Font("Segoe UI", 9F),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Inbox tab
            var inboxTab = new TabPage("Inbox");
            SetupInboxTab(inboxTab);
            emailTabControl.TabPages.Add(inboxTab);

            // Outbox tab
            var outboxTab = new TabPage("Outbox");
            SetupOutboxTab(outboxTab);
            emailTabControl.TabPages.Add(outboxTab);

            // Add event handler to load data when tabs are selected
            emailTabControl.SelectedIndexChanged += (s, e) =>
            {
                if (emailTabControl.SelectedTab == inboxTab)
                {
                    _ = LoadInboxEmails();
                }
                else if (emailTabControl.SelectedTab == outboxTab)
                {
                    _ = LoadOutboxEmails();
                }
            };

            // Load initial data for the first tab
            _ = LoadInboxEmails();

            tab.Controls.Add(emailTabControl);
        }

        private void SetupInboxTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Toolbar with refresh, settings AND pagination - all in one row
            //var refreshBtn = new ModernButton("🔄 Refresh", false)
            //{
            //    Size = new Size(80, 30),
            //    Location = new Point(10, 10)
            //};
            //refreshBtn.Click += async (s, e) => await LoadInboxEmails();
            //tab.Controls.Add(refreshBtn);

            //var settingsBtn = new ModernButton("⚙️ Settings", false)
            //{
            //    Size = new Size(80, 30),
            //    Location = new Point(100, 10)
            //};
            //tab.Controls.Add(settingsBtn);

            // Pagination controls in the same toolbar row
            var prevButton = new ModernButton("◄ Prev", false)
            {
                Size = new Size(70, 30),
                Location = new Point(10, 10)
            };
            prevButton.Click += (s, e) =>
            {
                if (inboxCurrentPage > 1)
                {
                    inboxCurrentPage--;
                    DisplayInboxPage();
                }
            };
            tab.Controls.Add(prevButton);

            var pageLabel = new Label
            {
                Name = "inboxPaginationLabel",
                Text = "Page 1 of 1 (0 emails)",
                Location = new Point(90, 15),
                Size = new Size(250, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ModernColors.TextPrimary
            };
            tab.Controls.Add(pageLabel);

            var nextButton = new ModernButton("Next ►", false)
            {
                Size = new Size(70, 30),
                Location = new Point(350, 10)
            };
            nextButton.Click += (s, e) =>
            {
                var totalPages = (int)Math.Ceiling(allInboxEmails.Count / (double)pageSize);
                if (inboxCurrentPage < totalPages)
                {
                    inboxCurrentPage++;
                    DisplayInboxPage();
                }
            };
            tab.Controls.Add(nextButton);

            // Email grid - starts below toolbar
            inboxGrid = new DataGridView
            {
                Location = new Point(10, 50),
                Size = new Size(tab.Width - 20, tab.Height - 60),
                BackColor = ModernColors.Surface,
                ForeColor = ModernColors.TextPrimary,
                Font = new Font("Segoe UI", 9F),
                GridColor = ModernColors.Border,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                ScrollBars = ScrollBars.Both,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Columns
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Status",
                HeaderText = "●",
                Width = 40,
                Resizable = DataGridViewTriState.False
            });

            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "From",
                HeaderText = "From",
                Width = 150
            });

            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Subject",
                HeaderText = "Subject",
                Width = 200
            });

            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Preview",
                HeaderText = "Preview",
                Width = 200
            });

            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Date",
                HeaderText = "Date",
                Width = 120
            });

            // Cell formatting
            inboxGrid.CellFormatting += (s, e) =>
            {
                try
                {
                    var gridView = s as DataGridView;
                    if (gridView == null || e.RowIndex < 0 || e.RowIndex >= gridView.Rows.Count)
                        return;

                    if (gridView.Rows[e.RowIndex].Tag is ClientEmailMessage email)
                    {
                        if (!email.IsRead)
                        {
                            e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                            e.CellStyle.BackColor = ColorTranslator.FromHtml("#eff6ff");
                            e.CellStyle.SelectionBackColor = ModernColors.Primary;
                        }

                        if (e.ColumnIndex == 0)
                        {
                            e.Value = email.IsRead ? "●" : "○";
                            e.FormattingApplied = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.Logger?.Error("Error formatting email cell", ex);
                }
            };

            inboxGrid.CellDoubleClick += EmailGrid_CellDoubleClick;

            tab.Controls.Add(inboxGrid);
        }

        private void SetupOutboxTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Toolbar with refresh, settings AND pagination - all in one row
            //var refreshBtn = new ModernButton("🔄 Refresh", false)
            //{
            //    Size = new Size(80, 30),
            //    Location = new Point(10, 10)
            //};
            //refreshBtn.Click += async (s, e) => await LoadOutboxEmails();
            //tab.Controls.Add(refreshBtn);

            //var settingsBtn = new ModernButton("⚙️ Settings", false)
            //{
            //    Size = new Size(80, 30),
            //    Location = new Point(100, 10)
            //};
            //tab.Controls.Add(settingsBtn);

            // Pagination controls in the same toolbar row
            var prevButton = new ModernButton("◄ Prev", false)
            {
                Size = new Size(70, 30),
                Location = new Point(10, 10)
            };
            prevButton.Click += (s, e) =>
            {
                if (outboxCurrentPage > 1)
                {
                    outboxCurrentPage--;
                    DisplayOutboxPage();
                }
            };
            tab.Controls.Add(prevButton);

            var pageLabel = new Label
            {
                Name = "outboxPaginationLabel",
                Text = "Page 1 of 1 (0 emails)",
                Location = new Point(90, 15),
                Size = new Size(250, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ModernColors.TextPrimary
            };
            tab.Controls.Add(pageLabel);

            var nextButton = new ModernButton("Next ►", false)
            {
                Size = new Size(70, 30),
                Location = new Point(350, 10)
            };
            nextButton.Click += (s, e) =>
            {
                var totalPages = (int)Math.Ceiling(allOutboxEmails.Count / (double)pageSize);
                if (outboxCurrentPage < totalPages)
                {
                    outboxCurrentPage++;
                    DisplayOutboxPage();
                }
            };
            tab.Controls.Add(nextButton);

            // Email grid - starts below toolbar
            outboxGrid = new DataGridView
            {
                Location = new Point(10, 50),
                Size = new Size(tab.Width - 20, tab.Height - 60),
                BackColor = ModernColors.Surface,
                ForeColor = ModernColors.TextPrimary,
                Font = new Font("Segoe UI", 9F),
                GridColor = ModernColors.Border,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                ScrollBars = ScrollBars.Both,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Columns (same as inbox)
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Status",
                HeaderText = "●",
                Width = 40,
                Resizable = DataGridViewTriState.False
            });

            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "From",
                HeaderText = "From",
                Width = 150
            });

            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Subject",
                HeaderText = "Subject",
                Width = 200
            });

            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Preview",
                HeaderText = "Preview",
                Width = 200
            });

            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Date",
                HeaderText = "Date",
                Width = 120
            });

            // Cell formatting
            outboxGrid.CellFormatting += (s, e) =>
            {
                try
                {
                    var gridView = s as DataGridView;
                    if (gridView == null || e.RowIndex < 0 || e.RowIndex >= gridView.Rows.Count)
                        return;

                    if (gridView.Rows[e.RowIndex].Tag is ClientEmailMessage email)
                    {
                        if (!email.IsRead)
                        {
                            e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                            e.CellStyle.BackColor = ColorTranslator.FromHtml("#eff6ff");
                            e.CellStyle.SelectionBackColor = ModernColors.Primary;
                        }

                        if (e.ColumnIndex == 0)
                        {
                            e.Value = email.IsRead ? "●" : "○";
                            e.FormattingApplied = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.Logger?.Error("Error formatting email cell", ex);
                }
            };

            outboxGrid.CellDoubleClick += EmailGrid_CellDoubleClick;

            tab.Controls.Add(outboxGrid);
        }

        private void SetupEmailGridColumns(DataGridView gridView)
        {
            gridView.Columns.Clear();

            // Fixed column widths that work well together
            gridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Status",
                HeaderText = "●",
                Width = 30,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            gridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "From",
                HeaderText = "From",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            gridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Subject",
                HeaderText = "Subject",
                Width = 200,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            gridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Preview",
                HeaderText = "Preview",
                Width = 250,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            gridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Date",
                HeaderText = "Date",
                Width = 110,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            // Total width: 30 + 150 + 200 + 250 + 110 = 740px
            // This should fit comfortably in most layouts with room for scrollbar
        }

        private void SetupEmailGridStyling(DataGridView gridView)
        {
            // Style the headers
            gridView.ColumnHeadersDefaultCellStyle.BackColor = ModernColors.Background;
            gridView.ColumnHeadersDefaultCellStyle.ForeColor = ModernColors.TextPrimary;
            gridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = ModernColors.Background;
            gridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = ModernColors.TextPrimary;

            // Style the cells
            gridView.DefaultCellStyle.BackColor = ModernColors.Surface;
            gridView.DefaultCellStyle.ForeColor = ModernColors.TextPrimary;
            gridView.DefaultCellStyle.SelectionBackColor = ModernColors.Primary;
            gridView.DefaultCellStyle.SelectionForeColor = Color.White;
            gridView.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            gridView.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);

            // Alternating row colors for better readability
            gridView.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#f8fafc");
        }

        private void EmailSubTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabControl = sender as TabControl;
            if (tabControl != null && tabControl.SelectedTab != null)
            {
                // Refresh the selected tab's data
                if (tabControl.SelectedTab.Text == "Inbox")
                {
                    LoadInboxEmails();
                }
                else if (tabControl.SelectedTab.Text == "Outbox")
                {
                    LoadOutboxEmails();
                }
            }
        }

        private async Task LoadInboxEmails()
        {
            if (inboxGrid == null || _clientDetails == null) return;

            try
            {
                string clientEmail = _clientDetails.RecipientAddress;
                if (string.IsNullOrEmpty(clientEmail))
                {
                    allInboxEmails = new List<ClientEmailMessage>();
                    inboxCurrentPage = 1;
                    DisplayInboxPage();
                    return;
                }

                if (!EmailService.IsAvailable())
                {
                    allInboxEmails = new List<ClientEmailMessage>();
                    inboxCurrentPage = 1;
                    DisplayInboxPage();
                    return;
                }

                var emailService = new EmailService();
                var allEmails = await emailService.GetEmailsForClient(clientEmail, 100);

                if (allEmails.Any())
                {
                    var firstEmail = allEmails.First();
                }

                var inboxEmails = allEmails.Where(e => e.Direction == "Received").ToList();
                allInboxEmails = ConvertToDisplayEmails(inboxEmails);
                await EnrichEmailsWithOutlookIDs(allInboxEmails, clientEmail);

                inboxCurrentPage = 1;
                DisplayInboxPage();
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error loading inbox emails", ex);
                allInboxEmails = new List<ClientEmailMessage>();
                inboxCurrentPage = 1;
                DisplayInboxPage();
            }
        }

        private void DisplayInboxPage()
        {
            var totalPages = (int)Math.Ceiling(allInboxEmails.Count / (double)pageSize);
            var pageEmails = allInboxEmails
                .Skip((inboxCurrentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            inboxGrid.Rows.Clear();
            foreach (var email in pageEmails)
            {
                var row = new DataGridViewRow();
                row.CreateCells(inboxGrid);
                row.Cells[0].Value = email.IsRead ? "●" : "○";
                row.Cells[1].Value = email.FromName;
                row.Cells[2].Value = email.Subject;
                row.Cells[3].Value = email.BodyPreview;
                row.Cells[4].Value = email.ReceivedDate.ToString("MMM dd, yyyy hh:mm tt");
                row.Tag = email;
                inboxGrid.Rows.Add(row);
            }

            // Update pagination label if it exists
            UpdateInboxPaginationLabel(totalPages);
        }

        private void UpdateInboxPaginationLabel(int totalPages)
        {
            var paginationLabel = inboxGrid.Parent?.Controls.Find("inboxPaginationLabel", false).FirstOrDefault() as Label;
            if (paginationLabel != null)
            {
                paginationLabel.Text = $"Page {inboxCurrentPage} of {totalPages} ({allInboxEmails.Count} total emails)";
            }
        }

        private async Task LoadOutboxEmails()
        {
            if (outboxGrid == null || _clientDetails == null) return;

            try
            {
                string clientEmail = _clientDetails.RecipientAddress;
                if (string.IsNullOrEmpty(clientEmail))
                {
                    allOutboxEmails = new List<ClientEmailMessage>();
                    outboxCurrentPage = 1;
                    DisplayOutboxPage();
                    return;
                }

                if (!EmailService.IsAvailable())
                {
                    allOutboxEmails = new List<ClientEmailMessage>();
                    outboxCurrentPage = 1;
                    DisplayOutboxPage();
                    return;
                }

                var emailService = new EmailService();
                var allEmails = await emailService.GetEmailsForClient(clientEmail, 100);
                var outboxEmails = allEmails.Where(e => e.Direction == "Sent").ToList();
                allOutboxEmails = ConvertToDisplayEmails(outboxEmails);
                await EnrichEmailsWithOutlookIDs(allOutboxEmails, clientEmail);

                outboxCurrentPage = 1;
                DisplayOutboxPage();
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error loading outbox emails", ex);
                allOutboxEmails = new List<ClientEmailMessage>();
                outboxCurrentPage = 1;
                DisplayOutboxPage();
            }
        }

        private void DisplayOutboxPage()
        {
            var totalPages = (int)Math.Ceiling(allOutboxEmails.Count / (double)pageSize);
            var pageEmails = allOutboxEmails
                .Skip((outboxCurrentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            outboxGrid.Rows.Clear();
            foreach (var email in pageEmails)
            {
                var row = new DataGridViewRow();
                row.CreateCells(outboxGrid);
                row.Cells[0].Value = "●";
                row.Cells[1].Value = email.FromName;
                row.Cells[2].Value = email.Subject;
                row.Cells[3].Value = email.BodyPreview;
                row.Cells[4].Value = email.ReceivedDate.ToString("MMM dd, yyyy hh:mm tt");
                row.Tag = email;
                outboxGrid.Rows.Add(row);
            }

            UpdateOutboxPaginationLabel(totalPages);
        }

        private void UpdateOutboxPaginationLabel(int totalPages)
        {
            var paginationLabel = outboxGrid.Parent?.Controls.Find("outboxPaginationLabel", false).FirstOrDefault() as Label;
            if (paginationLabel != null)
            {
                paginationLabel.Text = $"Page {outboxCurrentPage} of {totalPages} ({allOutboxEmails.Count} total emails)";
            }
        }

        private void DisplayEmailsInGrid(List<ClientEmailMessage> emails, DataGridView gridView)
        {
            if (gridView == null) return;

            try
            {
                gridView.Rows.Clear();

                foreach (var email in emails)
                {
                    var rowIndex = gridView.Rows.Add();
                    var row = gridView.Rows[rowIndex];

                    row.Tag = email;
                    row.Cells["Status"].Value = email.IsRead ? "●" : "○";
                    row.Cells["From"].Value = email.FromName ?? "Unknown";
                    row.Cells["Subject"].Value = email.Subject ?? "No Subject";
                    row.Cells["Preview"].Value = TruncateText(email.BodyPreview ?? "", 60);
                    row.Cells["Date"].Value = email.ReceivedDate.ToString("dd-MMM-yyyy HH:mm");

                    if (!email.IsRead)
                    {
                        row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#eff6ff");
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error displaying emails in grid", ex);
            }
        }

        private async Task EnrichEmailsWithOutlookIDs(List<ClientEmailMessage> emails, string clientEmail)
        {
            try
            {
                var outlookApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
                var nameSpace = outlookApp.GetType().InvokeMember("GetNamespace",
                    System.Reflection.BindingFlags.InvokeMethod, null, outlookApp, new object[] { "MAPI" });

                // Check both Inbox and Sent Items
                int[] folderIds = { 6, 5 }; // Inbox, Sent Items

                foreach (int folderId in folderIds)
                {
                    var folder = nameSpace.GetType().InvokeMember("GetDefaultFolder",
                        System.Reflection.BindingFlags.InvokeMethod, null, nameSpace, new object[] { folderId });

                    var items = folder.GetType().GetProperty("Items").GetValue(folder);
                    if (items == null) continue;

                    int count = (int)items.GetType().GetProperty("Count").GetValue(items);

                    // Check recent emails in this folder
                    for (int i = 1; i <= Math.Min(count, 50); i++)
                    {
                        try
                        {
                            var outlookItem = items.GetType().InvokeMember("Item",
                                System.Reflection.BindingFlags.InvokeMethod, null, items, new object[] { i });

                            string subject = outlookItem.GetType().GetProperty("Subject")?.GetValue(outlookItem)?.ToString() ?? "";
                            var receivedTime = (DateTime)outlookItem.GetType().GetProperty("ReceivedTime").GetValue(outlookItem);

                            // Find matching email from Graph
                            var matchingEmail = emails.FirstOrDefault(e =>
                                string.IsNullOrEmpty(e.OutlookId) && // Not already matched
                                e.Subject == subject &&
                                Math.Abs((e.ReceivedDate - receivedTime).TotalMinutes) < 5);

                            if (matchingEmail != null)
                            {
                                matchingEmail.OutlookId = outlookItem.GetType().GetProperty("EntryID").GetValue(outlookItem).ToString();
                                Program.Logger?.Info($"Matched email for desktop opening: {subject}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.Logger?.Error($"Error processing Outlook item {i}", ex);
                            continue;
                        }
                    }
                    int enrichedCount = emails.Count(e => !string.IsNullOrEmpty(e.OutlookId));
                    MessageBox.Show($"Enrichment result: {enrichedCount} out of {emails.Count} emails got OutlookId", "Debug Enrichment");
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error enriching emails with Outlook IDs - will use browser fallback", ex);
            }
        }

        private DataGridView FindEmailGrid(string gridType)
        {
            try
            {
                // Find the main tab control in the form
                var mainTabControl = this.Controls.OfType<Panel>()
                    .SelectMany(p => p.Controls.OfType<Panel>())
                    .SelectMany(p => p.Controls.OfType<TabControl>())
                    .FirstOrDefault();

                if (mainTabControl == null)
                {
                    Program.Logger?.Error("Main tab control not found");
                    return null;
                }

                // Find the Emails tab
                var emailsTab = mainTabControl.TabPages.Cast<TabPage>()
                    .FirstOrDefault(t => t.Text.Contains("Email"));

                if (emailsTab == null)
                {
                    Program.Logger?.Error("Emails tab not found");
                    return null;
                }

                // Find the sub-tab control within the emails tab
                var subTabControl = emailsTab.Controls.OfType<TabControl>().FirstOrDefault();
                if (subTabControl == null)
                {
                    Program.Logger?.Error("Email sub-tab control not found");
                    return null;
                }

                // Find the target tab (Inbox or Outbox)
                var targetTab = subTabControl.TabPages.Cast<TabPage>()
                    .FirstOrDefault(t => t.Text.Equals(gridType == "inbox" ? "Inbox" : "Outbox", StringComparison.OrdinalIgnoreCase));

                if (targetTab == null)
                {
                    Program.Logger?.Error($"Target tab '{gridType}' not found");
                    return null;
                }

                var dataGridView = targetTab.Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView == null)
                {
                    Program.Logger?.Error($"DataGridView not found in {gridType} tab");
                }

                return dataGridView;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error finding {gridType} grid: {ex.Message}", ex);
                return null;
            }
        }

        private void ShowEmailMessage(string message, DataGridView gridView)
        {
            if (gridView != null)
            {
                gridView.Rows.Clear();
                var rowIndex = gridView.Rows.Add();
                var row = gridView.Rows[rowIndex];

                row.Cells["Status"].Value = "";
                row.Cells["From"].Value = message;
                row.Cells["Subject"].Value = "";
                row.Cells["Preview"].Value = "";
                row.Cells["Date"].Value = "";
            }
        }




        private void EmailListView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                var gridView = sender as DataGridView;
                if (gridView == null || e.RowIndex < 0 || e.RowIndex >= gridView.Rows.Count)
                    return;

                if (gridView.Rows[e.RowIndex].Tag is ClientEmailMessage email)
                {
                    // Format unread emails
                    if (!email.IsRead)
                    {
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        e.CellStyle.BackColor = ColorTranslator.FromHtml("#eff6ff");
                        e.CellStyle.SelectionBackColor = ModernColors.Primary;
                    }

                    // Format status column
                    if (e.ColumnIndex == 0) // Status column
                    {
                        e.Value = email.IsRead ? "●" : "○";
                        e.FormattingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error formatting email cell", ex);
            }
        }

        // Fix for CS1503: Ensure the correct EmailMessage type is used in the DisplayEmailsInList method call

        // Update the DisplayEmailsInList method call to use the correct namespace for the EmailMessage type
        // This ensures that the List<easiplan.app.Forms.EmailMessage> is converted to List<easiplan.app.Services.EmailMessage> if needed.

        //private void DisplayEmailsInList(List<ClientEmailMessage> emails)
        //{
        //    if (emailListView == null) return;

        //    try
        //    {
        //        emailListView.Rows.Clear();

        //        foreach (var email in emails)
        //        {
        //            var rowIndex = emailListView.Rows.Add();
        //            var row = emailListView.Rows[rowIndex];

        //            // Store email data in the row's Tag
        //            row.Tag = email;

        //            // Set cell values
        //            row.Cells["Status"].Value = email.IsRead ? "●" : "○";
        //            row.Cells["From"].Value = email.FromName ?? "Unknown";
        //            row.Cells["Subject"].Value = email.Subject ?? "No Subject";
        //            row.Cells["Preview"].Value = TruncateText(email.BodyPreview ?? "", 60);
        //            row.Cells["Date"].Value = email.ReceivedDate.ToString("dd-MMM-yyyy HH:mm");

        //            // Apply unread styling
        //            if (!email.IsRead)
        //            {
        //                row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        //                row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#eff6ff");
        //            }
        //        }

        //        // Auto-size columns for better fit
        //        emailListView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

        //        // But keep Subject and Preview as fill columns
        //        if (emailListView.Columns["Subject"] != null)
        //            emailListView.Columns["Subject"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //        if (emailListView.Columns["Preview"] != null)
        //            emailListView.Columns["Preview"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.Logger?.Error("Error displaying emails in list", ex);
        //        //ShowEmailMessage($"Error displaying emails: {ex.Message}");
        //    }
        //}

        // Helper method to truncate text for preview
        private string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return "";
            if (text.Length <= maxLength) return text;
            return text.Substring(0, maxLength - 3) + "...";
        }

        // Updated ShowEmailMessage method for DataGridView
        //private void ShowEmailMessage(string message)
        //{
        //    if (emailListView != null)
        //    {
        //        emailListView.Rows.Clear();
        //        var rowIndex = emailListView.Rows.Add();
        //        var row = emailListView.Rows[rowIndex];

        //        row.Cells["Status"].Value = "";
        //        row.Cells["From"].Value = message;
        //        row.Cells["Subject"].Value = "";
        //        row.Cells["Preview"].Value = "";
        //        row.Cells["Date"].Value = "";
        //    }
        //}

        // Updated OpenEmailInOutlook method for DataGridView
        private async Task OpenEmailInOutlook(DataGridView dataGridView, int rowIndex)
        {
            try
            {
                var row = dataGridView.Rows[rowIndex];
                var email = row.Tag as ClientEmailMessage;

                if (email != null && !string.IsNullOrEmpty(email.EntryID) && !string.IsNullOrEmpty(email.StoreID))
                {
                    OpenEmailById(email.EntryID, email.StoreID);
                }
                else
                {
                    Program.Logger?.Warn("Email does not have Outlook EntryID/StoreID");
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error opening email", ex);
            }
        }

        private void EmailGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = sender as DataGridView;
            var email = grid.Rows[e.RowIndex].Tag as ClientEmailMessage;

            OpenEmailUsingAvailableMethod(email);
        }

        private void OpenEmailUsingAvailableMethod(ClientEmailMessage email)
        {
            if (email == null) return;

            try
            {
                // Just use WebLink - it's reliable and always works
                if (!string.IsNullOrEmpty(email.WebLink))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = email.WebLink,
                        UseShellExecute = true
                    });
                    return;
                }

                MessageBox.Show("This email cannot be opened because no link was found.", "Cannot Open Email");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error opening email", ex);
                MessageBox.Show($"Could not open email: {ex.Message}", "Error");
            }
        }

        private bool TryOpenInOutlookDesktop(string outlookId)
        {
            try
            {
                var outlookApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
                var nameSpace = outlookApp.GetType().InvokeMember("GetNamespace",
                    System.Reflection.BindingFlags.InvokeMethod, null, outlookApp, new object[] { "MAPI" });

                var mailItem = nameSpace.GetType().InvokeMember("GetItemFromID",
                    System.Reflection.BindingFlags.InvokeMethod, null, nameSpace, new object[] { outlookId });

                mailItem.GetType().InvokeMember("Display",
                    System.Reflection.BindingFlags.InvokeMethod, null, mailItem, new object[] { true });

                return true;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Failed to open in Outlook desktop", ex);
                return false;
            }
        }

        private bool OpenEmailById(string entryId, string storeId)
        {
            try
            {
                var outlookApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
                var nameSpace = outlookApp.GetType().InvokeMember("GetNamespace",
                    System.Reflection.BindingFlags.InvokeMethod, null, outlookApp, new object[] { "MAPI" });

                var mailItem = nameSpace.GetType().InvokeMember("GetItemFromID",
                    System.Reflection.BindingFlags.InvokeMethod, null, nameSpace, new object[] { entryId, storeId });

                mailItem.GetType().InvokeMember("Display",
                    System.Reflection.BindingFlags.InvokeMethod, null, mailItem, new object[] { true });

                return true;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Failed to open email by EntryID", ex);
                return false;
            }
        }

        private async Task OpenEmailSafely(ClientEmailMessage email)
        {
            // Validate email data first
            if (string.IsNullOrWhiteSpace(email.Subject))
            {
                Program.Logger?.Warn("Email subject is empty or null");
                return;
            }

            // Try to open the email using COM
            bool success = await TryOpenWithCOM(email);

            if (!success)
            {
                //MessageBox.Show("COM approach failed, trying fallback methods");
                TryFallbackMethods(email);
            }
        }

        private async Task<bool> TryOpenWithCOM(ClientEmailMessage email)
        {
            object outlookApp = null;
            object nameSpace = null;

            try
            {
                // Step 1: Connect to Outlook
                Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                if (outlookType == null)
                {
                    Program.Logger?.Info("Outlook COM type not available");
                    return false;
                }

                try
                {
                    outlookApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
                    Program.Logger?.Info("Connected to existing Outlook instance");
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    Program.Logger?.Info("No existing Outlook instance, starting new one");
                    outlookApp = Activator.CreateInstance(outlookType);
                    await Task.Delay(3000); // Give Outlook time to start
                }

                if (outlookApp == null)
                {
                    Program.Logger?.Error("Could not create or connect to Outlook application");
                    return false;
                }

                // Step 2: Get MAPI namespace
                try
                {
                    nameSpace = outlookApp.GetType().InvokeMember("GetNamespace",
                        System.Reflection.BindingFlags.InvokeMethod, null, outlookApp, new object[] { "MAPI" });
                }
                catch (Exception ex)
                {
                    Program.Logger?.Error("Could not get MAPI namespace", ex);
                    return false;
                }

                if (nameSpace == null)
                {
                    Program.Logger?.Error("MAPI namespace is null");
                    return false;
                }

                // Step 3: Search for and open the email
                bool result = await SearchForEmailAndOpen(nameSpace, email);
                return result;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("COM operation failed", ex);
                return false;
            }
            finally
            {
                // Clean up COM objects
                try
                {
                    if (nameSpace != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(nameSpace);
                }
                catch { }

                try
                {
                    if (outlookApp != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp);
                }
                catch { }
            }
        }

        private async Task<bool> SearchForEmailAndOpen(object nameSpace, ClientEmailMessage email)
        {
            // Clean and prepare search subject
            string cleanSubject = email.Subject.Trim();
            if (cleanSubject.Length > 100)
                cleanSubject = cleanSubject.Substring(0, 100);

            // Escape single quotes for Outlook search
            string searchSubject = cleanSubject.Replace("'", "''");
            Program.Logger?.Info($"Searching for email: '{cleanSubject}'");

            // Try exact subject match first in Inbox
            if (await SearchFolderSafely(nameSpace, 6, $"[Subject] = '{searchSubject}'", "Inbox"))
            {
                Program.Logger?.Info("Found email in Inbox with exact subject match");
                return true;
            }

            // Try exact subject match in Sent Items
            if (await SearchFolderSafely(nameSpace, 5, $"[Subject] = '{searchSubject}'", "Sent Items"))
            {
                Program.Logger?.Info("Found email in Sent Items with exact subject match");
                return true;
            }

            // If exact match fails and subject is long enough, try partial match

            string partialSubject = cleanSubject.Substring(0, Math.Min(20, cleanSubject.Length)).Replace("'", "''");

            if (await SearchFolderSafely(nameSpace, 6, $"[Subject] LIKE '{partialSubject}*'", "Inbox (partial)"))
            {
                Program.Logger?.Info("Found email in Inbox with partial subject match");
                return true;
            }

            if (await SearchFolderSafely(nameSpace, 5, $"[Subject] LIKE '{partialSubject}*'", "Sent Items (partial)"))
            {
                Program.Logger?.Info("Found email in Sent Items with partial subject match");
                return true;
            }
            

            Program.Logger?.Info("Email not found in any folder");
            return false;
        }

        private async Task<bool> SearchFolderSafely(object nameSpace, int folderId, string searchCriteria, string folderName)
        {
            object folder = null;
            object items = null;
            object results = null;
            object mailItem = null;

            try
            {
                Program.Logger?.Info($"Searching {folderName} with criteria: {searchCriteria}");

                // Get folder
                folder = nameSpace.GetType().InvokeMember("GetDefaultFolder",
                    System.Reflection.BindingFlags.InvokeMethod, null, nameSpace, new object[] { folderId });

                if (folder == null)
                {
                    Program.Logger?.Warn($"{folderName} folder is null");
                    return false;
                }

                // Get Items property
                var itemsProperty = folder.GetType().GetProperty("Items");
                if (itemsProperty == null)
                {
                    Program.Logger?.Warn($"{folderName} has no Items property");
                    return false;
                }

                items = itemsProperty.GetValue(folder);
                if (items == null)
                {
                    Program.Logger?.Warn($"{folderName} Items collection is null - folder may be empty or not configured");
                    return false;
                }

                // Apply search restriction
                int totalCount = (int)items.GetType().GetProperty("Count").GetValue(items);

                // Check first 20 emails manually
                int maxCheck = Math.Min(totalCount, 20);
                for (int i = 1; i <= maxCheck; i++)
                {
                    object item = items.GetType().InvokeMember("Item",
                        System.Reflection.BindingFlags.InvokeMethod, null, items, new object[] { i });

                    object subjectObj = item.GetType().GetProperty("Subject")?.GetValue(item);
                    string itemSubject = subjectObj?.ToString() ?? "";

                    if (itemSubject.Contains("Test 6"))
                    {
                        MessageBox.Show($"Found matching email: '{itemSubject}'", "Found It!");
                        item.GetType().InvokeMember("Display",
                            System.Reflection.BindingFlags.InvokeMethod, null, item, new object[] { true });
                        return true;
                    }
                }

                return false;

                // Check count
                var countProperty = results.GetType().GetProperty("Count");
                if (countProperty == null)
                {
                    Program.Logger?.Warn($"Count property not available for {folderName} search results");
                    return false;
                }

                object countObj = countProperty.GetValue(results);
                if (countObj == null)
                {
                    Program.Logger?.Warn($"Count value is null for {folderName}");
                    return false;
                }

                int count = (int)countObj;
                MessageBox.Show($"Searching {folderName}: Found {count} emails with criteria: {searchCriteria}", "Folder Debug");
                Program.Logger?.Info($"Found {count} matching emails in {folderName}");

                if (count == 0)
                    return false;

                // Get first email
                mailItem = results.GetType().InvokeMember("Item",
                    System.Reflection.BindingFlags.InvokeMethod, null, results, new object[] { 1 });

                if (mailItem == null)
                {
                    Program.Logger?.Warn($"First email item is null in {folderName}");
                    return false;
                }

                // Display the email - this is the key action that opens it
                mailItem.GetType().InvokeMember("Display",
                    System.Reflection.BindingFlags.InvokeMethod, null, mailItem, new object[] { true });

                Program.Logger?.Info($"Successfully opened email from {folderName}");
                return true;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error searching {folderName}: {ex.Message}");
                return false;
            }
            finally
            {
                // Clean up all COM objects
                try { if (mailItem != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(mailItem); } catch { }
                try { if (results != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(results); } catch { }
                try { if (items != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(items); } catch { }
                try { if (folder != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(folder); } catch { }
            }
        }

        private void TryFallbackMethods(ClientEmailMessage email)
        {
            try
            {
                // Method 1: Try opening Outlook with search parameter
                if (TryOpenOutlookWithSearch(email))
                {
                    Program.Logger?.Info("Opened Outlook with search parameter");
                    return;
                }

                // Method 2: Just open Outlook (user can search manually)
                if (TryOpenOutlookBasic())
                {
                    Program.Logger?.Info("Opened Outlook - user can search manually");
                    return;
                }

                Program.Logger?.Warn("All fallback methods failed");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Fallback methods failed", ex);
            }
        }


        private async Task OpenNewOutlookWithSearch(ClientEmailMessage email)
        {
            try
            {
                // Step 1: Open New Outlook
                await OpenNewOutlook();

                // Step 2: Wait for it to load
                await Task.Delay(2000);

                // Step 3: Send Ctrl+E to open search box
                SendKeys.SendWait("^e"); // Ctrl+E opens search in Outlook

                // Step 4: Wait a moment for search box to appear
                await Task.Delay(500);

                // Step 5: Type the email subject
                string searchText = email.Subject?.Trim() ?? "";
                if (!string.IsNullOrEmpty(searchText))
                {
                    // Clean up the search text (remove quotes and special chars that might cause issues)
                    searchText = searchText.Replace("\"", "").Replace("{", "").Replace("}", "");
                    SendKeys.SendWait(searchText);

                    // Step 6: Press Enter to search
                    await Task.Delay(300);
                    SendKeys.SendWait("{ENTER}");
                }

                Program.Logger?.Info($"Opened New Outlook and searched for: {searchText}");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error with New Outlook search automation", ex);
                // Fallback: just open New Outlook
                await OpenNewOutlook();
            }
        }

        private async Task OpenNewOutlook()
        {
            try
            {
                // Try multiple ways to open New Outlook
                string[] commands = {
            "ms-outlook:",           // Protocol handler
            "outlook:",              // Alternative protocol
            "HxOutlook"              // Direct executable name
        };

                foreach (string command in commands)
                {
                    try
                    {
                        var startInfo = new ProcessStartInfo
                        {
                            FileName = command,
                            UseShellExecute = true,
                            WindowStyle = ProcessWindowStyle.Normal
                        };

                        Process.Start(startInfo);
                        Program.Logger?.Info($"Successfully opened New Outlook with: {command}");
                        return; // Success, exit
                    }
                    catch (Exception ex)
                    {
                        Program.Logger?.Info($"Command '{command}' failed: {ex.Message}");
                        continue; // Try next command
                    }
                }

                // If all else fails, try the Windows Apps folder
                string newOutlookPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Microsoft\WindowsApps\microsoft.windowscommunicationsapps_8wekyb3d8bbwe\HxOutlook.exe"
                );

                if (File.Exists(newOutlookPath))
                {
                    Process.Start(new ProcessStartInfo(newOutlookPath) { UseShellExecute = true });
                    Program.Logger?.Info("Opened New Outlook from WindowsApps folder");
                }
                else
                {
                    throw new Exception("Could not find New Outlook executable");
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Failed to open New Outlook", ex);
                throw;
            }
        }

        private async Task<bool> TryNewOutlookEmail(ClientEmailMessage email)
        {
            try
            {
                // Method 1: Try direct email opening with message ID (if we have it)
                if (!string.IsNullOrEmpty(email.Id))
                {
                    if (await TryOpenEmailById(email.Id))
                    {
                        return true;
                    }
                }

                // Method 2: Try advanced search URL
                if (await TryAdvancedOutlookSearch(email))
                {
                    return true;
                }

                // Method 3: Try basic search
                if (await TryBasicOutlookSearch(email))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("New Outlook methods failed", ex);
                return false;
            }
        }

        private async Task<bool> TryOpenEmailById(string emailId)
        {
            try
            {
                // New Outlook supports direct message URLs
                string messageUrl = $"ms-outlook://emails/message/{emailId}";

                var startInfo = new ProcessStartInfo
                {
                    FileName = messageUrl,
                    UseShellExecute = true
                };

                Process.Start(startInfo);

                // Give it a moment to process
                await Task.Delay(1000);
                return true;
            }
            catch (Exception ex)
            {
                Program.Logger?.Info($"Direct message URL failed: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TryAdvancedOutlookSearch(ClientEmailMessage email)
        {
            try
            {
                // Build search parameters
                var searchParams = new List<string>();

                if (!string.IsNullOrEmpty(email.Subject))
                {
                    string subject = Uri.EscapeDataString(email.Subject.Trim());
                    searchParams.Add($"subject:{subject}");
                }

                if (!string.IsNullOrEmpty(email.FromName))
                {
                    string from = Uri.EscapeDataString(email.FromName.Trim());
                    searchParams.Add($"from:{from}");
                }

                // Add date range (same day)
                string date = email.ReceivedDate.ToString("yyyy-MM-dd");
                searchParams.Add($"received:{date}");

                string searchQuery = string.Join(" AND ", searchParams);

                // New Outlook search URL format
                string searchUrl = $"ms-outlook://search?query={Uri.EscapeDataString(searchQuery)}";

                var startInfo = new ProcessStartInfo
                {
                    FileName = searchUrl,
                    UseShellExecute = true
                };

                Process.Start(startInfo);

                Program.Logger?.Info($"Advanced search: {searchQuery}");
                await Task.Delay(1000);
                return true;
            }
            catch (Exception ex)
            {
                Program.Logger?.Info($"Advanced search failed: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TryBasicOutlookSearch(ClientEmailMessage email)
        {
            try
            {
                if (string.IsNullOrEmpty(email.Subject))
                    return false;

                // Simple subject search
                string subject = Uri.EscapeDataString(email.Subject.Trim());
                string searchUrl = $"ms-outlook://search?query={subject}";

                var startInfo = new ProcessStartInfo
                {
                    FileName = searchUrl,
                    UseShellExecute = true
                };

                Process.Start(startInfo);

                Program.Logger?.Info($"Basic search for: {email.Subject}");
                await Task.Delay(1000);
                return true;
            }
            catch (Exception ex)
            {
                Program.Logger?.Info($"Basic search failed: {ex.Message}");
                return false;
            }
        }

        // Keep this as fallback for systems with classic Outlook
        private async Task<bool> TryClassicOutlookCOM(ClientEmailMessage email)
        {
            object outlookApp = null;

            try
            {
                Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                outlookApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");

                object nameSpace = outlookApp.GetType().InvokeMember("GetNamespace",
                    System.Reflection.BindingFlags.InvokeMethod, null, outlookApp, new object[] { "MAPI" });

                if (nameSpace == null) return false;

                // Use your existing search logic
                return await SearchAndDisplayEmail(nameSpace, email);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (outlookApp != null)
                {
                    try { System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp); }
                    catch { }
                }
            }
        }

        // Alternative approach: Try to open New Outlook directly and show helpful info
        private async Task<bool> TryNewOutlookWithInfo(ClientEmailMessage email)
        {
            try
            {
                // Open New Outlook
                bool opened = await TryOpenNewOutlookApp();

                if (opened)
                {
                    // Give user a subtle notification with search info
                    await Task.Delay(2000); // Wait for Outlook to load

                    // Use Windows notifications to help user
                    ShowSearchNotification(email);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("New Outlook with info failed", ex);
                return false;
            }
        }

        private async Task<bool> TryOpenNewOutlookApp()
        {
            try
            {
                // Try different methods to open New Outlook
                string[] outlookCommands = {
            "ms-outlook:",
            "outlook:",
            @"C:\Users\" + Environment.UserName + @"\AppData\Local\Microsoft\WindowsApps\microsoft.windowscommunicationsapps_8wekyb3d8bbwe\HxOutlook.exe"
        };

                foreach (string command in outlookCommands)
                {
                    try
                    {
                        var startInfo = new ProcessStartInfo
                        {
                            FileName = command,
                            UseShellExecute = true
                        };

                        Process.Start(startInfo);
                        await Task.Delay(500);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Program.Logger?.Info($"Command failed: {command} - {ex.Message}");
                        continue;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private void ShowSearchNotification(ClientEmailMessage email)
        {
            try
            {
                // Create a small, non-intrusive notification
                var notificationForm = new Form
                {
                    Size = new Size(350, 120),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedToolWindow,
                    TopMost = true,
                    ShowInTaskbar = false,
                    Text = "Email Search"
                };

                var label = new Label
                {
                    Text = $"Search Outlook for:\n\"{email.Subject}\"",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9)
                };

                notificationForm.Controls.Add(label);
                notificationForm.Show();

                // Auto-close after 5 seconds
                var timer = new Timer();
                timer.Interval = 5000;
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    notificationForm.Close();
                    timer.Dispose();
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Notification failed", ex);
            }
        }

        private async Task OpenSpecificEmail(ClientEmailMessage email)
        {
            object outlookApp = null;

            try
            {
                // Get Outlook application (existing instance)
                Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                outlookApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");

                // Get MAPI namespace
                object nameSpace = outlookApp.GetType().InvokeMember("GetNamespace",
                    System.Reflection.BindingFlags.InvokeMethod, null, outlookApp, new object[] { "MAPI" });

                // Search for the email and open it
                if (await SearchAndDisplayEmail(nameSpace, email))
                {
                    return; // Success - email opened
                }

                // If exact search failed, try a broader search
                await SearchAndDisplayEmailBroad(nameSpace, email);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                // Outlook not running - start it and try again
                await StartOutlookAndOpenEmail(email);
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Failed to open specific email", ex);
            }
            finally
            {
                if (outlookApp != null)
                {
                    try { System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp); }
                    catch { }
                }
            }
        }

        private async Task<bool> SearchAndDisplayEmail(object nameSpace, ClientEmailMessage email)
        {
            try
            {
                string subject = email.Subject?.Trim() ?? "";
                if (string.IsNullOrEmpty(subject)) return false;

                // Escape single quotes for Outlook search
                string searchSubject = subject.Replace("'", "''");

                // Search criteria - exact subject match
                string criteria = $"[Subject] = '{searchSubject}'";

                // Try Inbox first (folder ID 6)
                if (await SearchFolderAndDisplay(nameSpace, 6, criteria))
                    return true;

                // Try Sent Items (folder ID 5)
                if (await SearchFolderAndDisplay(nameSpace, 5, criteria))
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Search and display error", ex);
                return false;
            }
        }

        private async Task<bool> SearchAndDisplayEmailBroad(object nameSpace, ClientEmailMessage email)
        {
            try
            {
                string subject = email.Subject?.Trim() ?? "";
                if (subject.Length < 10) return false;

                // Broader search - first 20 characters of subject
                string partialSubject = subject.Substring(0, Math.Min(20, subject.Length)).Replace("'", "''");
                string criteria = $"[Subject] LIKE '{partialSubject}*'";

                // Search Inbox
                if (await SearchFolderAndDisplay(nameSpace, 6, criteria))
                    return true;

                // Search Sent Items
                if (await SearchFolderAndDisplay(nameSpace, 5, criteria))
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Broad search error", ex);
                return false;
            }
        }

        private async Task<bool> SearchFolderAndDisplay(object nameSpace, int folderId, string criteria)
        {
            object folder = null;
            object items = null;
            object results = null;

            try
            {
                // Get folder
                folder = nameSpace.GetType().InvokeMember("GetDefaultFolder",
                    System.Reflection.BindingFlags.InvokeMethod, null, nameSpace, new object[] { folderId });

                if (folder == null)
                {
                    Program.Logger?.Error($"Folder {folderId} is null");
                    return false;
                }

                // Get items with null check
                var itemsProperty = folder.GetType().GetProperty("Items");
                if (itemsProperty == null)
                {
                    Program.Logger?.Error($"Items property not found for folder {folderId}");
                    return false;
                }

                items = itemsProperty.GetValue(folder);
                if (items == null)
                {
                    Program.Logger?.Error($"Items collection is null for folder {folderId}");
                    return false;
                }

                // Apply search with null check
                results = items.GetType().InvokeMember("Restrict",
                    System.Reflection.BindingFlags.InvokeMethod, null, items, new object[] { criteria });

                if (results == null)
                {
                    Program.Logger?.Info($"Search returned null results for folder {folderId}");
                    return false;
                }

                // Check count with null check
                var countProperty = results.GetType().GetProperty("Count");
                if (countProperty == null)
                {
                    Program.Logger?.Error($"Count property not found for folder {folderId}");
                    return false;
                }

                object countObj = countProperty.GetValue(results);
                if (countObj == null)
                {
                    Program.Logger?.Error($"Count is null for folder {folderId}");
                    return false;
                }

                int count = (int)countObj;
                if (count == 0)
                {
                    Program.Logger?.Info($"No emails found in folder {folderId}");
                    return false;
                }

                // Get first email and display it with null check
                object mailItem = results.GetType().InvokeMember("Item",
                    System.Reflection.BindingFlags.InvokeMethod, null, results, new object[] { 1 });

                if (mailItem == null)
                {
                    Program.Logger?.Error($"Mail item is null in folder {folderId}");
                    return false;
                }

                // THIS OPENS THE EMAIL DIRECTLY
                mailItem.GetType().InvokeMember("Display",
                    System.Reflection.BindingFlags.InvokeMethod, null, mailItem, new object[] { true });

                Program.Logger?.Info($"Successfully opened email in folder {folderId}");

                // Clean up mail item
                try { System.Runtime.InteropServices.Marshal.ReleaseComObject(mailItem); } catch { }

                return true;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error searching folder {folderId}: {ex.Message}");
                return false;
            }
            finally
            {
                // Cleanup COM objects
                try { if (results != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(results); } catch { }
                try { if (items != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(items); } catch { }
                try { if (folder != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(folder); } catch { }
            }
        }

        private async Task StartOutlookAndOpenEmail(ClientEmailMessage email)
        {
            try
            {
                // Start Outlook if not running
                string outlookPath = GetOutlookPath();
                if (!string.IsNullOrEmpty(outlookPath))
                {
                    Process.Start(new ProcessStartInfo(outlookPath) { UseShellExecute = true });

                    // Wait for Outlook to start
                    await Task.Delay(5000);

                    // Try opening the email again
                    await OpenSpecificEmail(email);
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error starting Outlook and opening email", ex);
            }
        }

        private bool TryOpenOutlookWithEmailSearch(ClientEmailMessage email)
        {
            try
            {
                string outlookPath = GetOutlookExecutablePath();
                if (string.IsNullOrEmpty(outlookPath))
                {
                    Program.Logger?.Info("Outlook executable not found");
                    return false;
                }

                // Create a simple search term from the subject
                string searchTerm = email.Subject?.Trim() ?? "";
                if (searchTerm.Length > 50)
                    searchTerm = searchTerm.Substring(0, 50);

                // Remove problematic characters
                searchTerm = searchTerm.Replace("\"", "").Replace("'", "").Replace("&", "");

                if (string.IsNullOrEmpty(searchTerm))
                {
                    Program.Logger?.Info("Email subject is empty, opening Outlook normally");
                    return TryOpenOutlookBasic();
                }

                var startInfo = new ProcessStartInfo
                {
                    FileName = outlookPath,
                    Arguments = "",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                var process = Process.Start(startInfo);

                if (process != null)
                {
                    Program.Logger?.Info($"Opened Outlook with search for: {searchTerm}");

                    // Give user helpful info
                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            var result = MessageBox.Show(
                                $"Outlook opened with search for:\n\"{searchTerm}\"\n\nDid you find the email?",
                                "Email Search",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (result == DialogResult.No)
                            {
                                ShowEmailInfoDialog(email);
                            }
                        });
                    });

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error opening Outlook with search", ex);
                return false;
            }
        }

        private bool TryOpenOutlookBasic()
        {
            try
            {
                string outlookPath = GetOutlookPath();
                if (!string.IsNullOrEmpty(outlookPath))
                {
                    Process.Start(new ProcessStartInfo(outlookPath) { UseShellExecute = true });
                    return true;
                }

                // Try protocol as last resort
                Process.Start(new ProcessStartInfo("outlook:") { UseShellExecute = true });
                return true;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error opening Outlook", ex);
                return false;
            }
        }

        private string GetOutlookExecutablePath()
        {
            // Check common Outlook paths
            string[] possiblePaths = {
        @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE",
        @"C:\Program Files (x86)\Microsoft Office\root\Office16\OUTLOOK.EXE",
        @"C:\Program Files\Microsoft Office\Office16\OUTLOOK.EXE",
        @"C:\Program Files (x86)\Microsoft Office\Office16\OUTLOOK.EXE",
        @"C:\Program Files\Microsoft Office\root\Office15\OUTLOOK.EXE",
        @"C:\Program Files (x86)\Microsoft Office\root\Office15\OUTLOOK.EXE",
        @"C:\Program Files\Microsoft Office\Office15\OUTLOOK.EXE",
        @"C:\Program Files (x86)\Microsoft Office\Office15\OUTLOOK.EXE"
    };

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            // Try registry lookup
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\OUTLOOK.EXE"))
                {
                    if (key != null)
                    {
                        string path = key.GetValue("") as string;
                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                        {
                            return path;
                        }
                    }
                }
            }
            catch { }

            return null;
        }

        private void ShowEmailInfoDialog(ClientEmailMessage email)
        {
            try
            {
                var emailForm = new Form();
                emailForm.Text = "Email Information";
                emailForm.Size = new Size(500, 400);
                emailForm.StartPosition = FormStartPosition.CenterParent;
                emailForm.ShowIcon = false;
                emailForm.MaximizeBox = false;
                emailForm.MinimizeBox = false;

                var textBox = new TextBox();
                textBox.Multiline = true;
                textBox.ScrollBars = ScrollBars.Vertical;
                textBox.ReadOnly = true;
                textBox.Dock = DockStyle.Fill;
                textBox.Font = new Font("Consolas", 9);
                textBox.BackColor = Color.White;

                string emailInfo = $"EMAIL DETAILS:\n" +
                                  $"═══════════════════════════════════════\n\n" +
                                  $"Subject: {email.Subject ?? "N/A"}\n\n" +
                                  $"From: {email.FromName ?? "N/A"}\n\n" +
                                  $"Date: {email.ReceivedDate:dddd, MMMM dd, yyyy 'at' h:mm tt}\n\n" +
                                  $"Direction: {email.Direction ?? "N/A"}\n\n" +
                                  $"Status: {(email.IsRead ? "Read" : "Unread")}\n\n" +
                                  $"Preview:\n{email.BodyPreview ?? "N/A"}\n\n" +
                                  $"═══════════════════════════════════════\n" +
                                  $"Search for this email manually in Outlook using the subject line above.";

                textBox.Text = emailInfo;

                var buttonPanel = new Panel();
                buttonPanel.Height = 40;
                buttonPanel.Dock = DockStyle.Bottom;

                var copyButton = new Button();
                copyButton.Text = "Copy to Clipboard";
                copyButton.Size = new Size(120, 30);
                copyButton.Location = new Point(10, 5);
                copyButton.Click += (s, e) =>
                {
                    CopyEmailInfoToClipboard(email);
                    MessageBox.Show("Email information copied to clipboard.", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };

                var closeButton = new Button();
                closeButton.Text = "Close";
                closeButton.Size = new Size(80, 30);
                closeButton.Location = new Point(140, 5);
                closeButton.Click += (s, e) => emailForm.Close();

                buttonPanel.Controls.Add(copyButton);
                buttonPanel.Controls.Add(closeButton);
                emailForm.Controls.Add(textBox);
                emailForm.Controls.Add(buttonPanel);

                emailForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error showing email info dialog", ex);
                CopyEmailInfoToClipboard(email);
                MessageBox.Show("Email information copied to clipboard.", "Email Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CopyEmailInfoToClipboard(ClientEmailMessage email)
        {
            try
            {
                string clipboardText = $"Subject: {email.Subject ?? "N/A"}\n" +
                                      $"From: {email.FromName ?? "N/A"}\n" +
                                      $"Date: {email.ReceivedDate:yyyy-MM-dd HH:mm}\n" +
                                      $"Direction: {email.Direction ?? "N/A"}\n" +
                                      $"Preview: {email.BodyPreview ?? "N/A"}";

                Clipboard.SetText(clipboardText);
                Program.Logger?.Info("Email information copied to clipboard");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error copying to clipboard", ex);
            }
        }

        private async Task OpenEmailDirectly(ClientEmailMessage email)
        {
            object outlookApp = null;

            try
            {
                // Connect to Outlook
                Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                if (outlookType == null)
                {
                    throw new Exception("Outlook is not installed on this system");
                }

                try
                {
                    // Try to connect to existing Outlook instance
                    outlookApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
                }
                catch
                {
                    // Start new Outlook instance if none running
                    outlookApp = Activator.CreateInstance(outlookType);
                    await Task.Delay(2000); // Give Outlook time to start
                }

                // Get MAPI namespace
                object nameSpace = outlookApp.GetType().InvokeMember("GetNamespace",
                    System.Reflection.BindingFlags.InvokeMethod, null, outlookApp, new object[] { "MAPI" });

                // Search for the email by subject in Inbox and Sent Items
                string subject = email.Subject?.Trim() ?? "";
                if (string.IsNullOrEmpty(subject))
                {
                    throw new Exception("Email subject is empty - cannot search");
                }

                // Try Inbox first (most emails are received)
                if (await SearchAndOpenEmail(nameSpace, 6, subject, "Inbox"))
                    return;

                // Try Sent Items 
                if (await SearchAndOpenEmail(nameSpace, 5, subject, "Sent Items"))
                    return;

                // If not found
                MessageBox.Show($"Could not find email with subject:\n'{subject}'\n\nThe email may have been moved, deleted, or the subject may not match exactly.",
                               "Email Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw new Exception($"Outlook error: {ex.Message}");
            }
            finally
            {
                if (outlookApp != null)
                {
                    try
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp);
                    }
                    catch { }
                }
            }
        }

        private async Task<bool> SearchAndOpenEmail(object nameSpace, int folderId, string subject, string folderName)
        {
            try
            {
                // Get folder
                object folder = nameSpace.GetType().InvokeMember("GetDefaultFolder",
                    System.Reflection.BindingFlags.InvokeMethod, null, nameSpace, new object[] { folderId });

                if (folder == null)
                {
                    throw new Exception($"Cannot access {folderName} folder - Outlook may not be properly configured");
                }

                // Get items in folder
                object items = folder.GetType().GetProperty("Items")?.GetValue(folder);

                if (items == null)
                {
                    throw new Exception($"{folderName} folder has no items collection - Outlook may not be activated or configured");
                }

                // Search for email with exact subject
                string searchCriteria = $"[Subject] = '{subject.Replace("'", "''")}'";
                object foundItems = items.GetType().InvokeMember("Restrict",
                    System.Reflection.BindingFlags.InvokeMethod, null, items, new object[] { searchCriteria });

                if (foundItems == null)
                {
                    return false; // Search returned no results
                }

                // Check if any emails found
                int count = (int)foundItems.GetType().GetProperty("Count").GetValue(foundItems);

                if (count > 0)
                {
                    // Get first email and open it
                    object mailItem = foundItems.GetType().InvokeMember("Item",
                        System.Reflection.BindingFlags.InvokeMethod, null, foundItems, new object[] { 1 });

                    // Open the email directly
                    mailItem.GetType().InvokeMember("Display",
                        System.Reflection.BindingFlags.InvokeMethod, null, mailItem, new object[] { true });

                    return true; // Success!
                }

                return false; // Not found in this folder
            }
            catch (Exception ex)
            {
                // Re-throw with more specific error for debugging
                throw new Exception($"Error accessing {folderName}: {ex.Message}");
            }
        }

        private async Task<bool> SearchFolderForEmail(object nameSpace, int folderId, string exactSubject, string folderName)
        {
            try
            {
                Program.Logger?.Info($"Searching {folderName} for: '{exactSubject}'");

                object folder = nameSpace.GetType().InvokeMember("GetDefaultFolder",
                    System.Reflection.BindingFlags.InvokeMethod, null, nameSpace, new object[] { folderId });

                object items = folder.GetType().GetProperty("Items").GetValue(folder);

                // Use exact subject match with proper escaping
                string searchCriteria = $"[Subject] = '{exactSubject.Replace("'", "''")}'";

                object foundItems = items.GetType().InvokeMember("Restrict",
                    System.Reflection.BindingFlags.InvokeMethod, null, items, new object[] { searchCriteria });

                int count = (int)foundItems.GetType().GetProperty("Count").GetValue(foundItems);
                Program.Logger?.Info($"Found {count} emails in {folderName}");

                if (count > 0)
                {
                    object mailItem = foundItems.GetType().InvokeMember("Item",
                        System.Reflection.BindingFlags.InvokeMethod, null, foundItems, new object[] { 1 });

                    // THIS IS THE KEY LINE - Opens the email directly
                    mailItem.GetType().InvokeMember("Display",
                        System.Reflection.BindingFlags.InvokeMethod, null, mailItem, new object[] { true });

                    Program.Logger?.Info($"SUCCESS: Opened email directly from {folderName}");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error searching {folderName}: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> SearchFolderWithCriteria(object nameSpace, int folderId, string dateCriteria, string folderName, string originalSubject)
        {
            try
            {
                Program.Logger?.Info($"Searching {folderName} with date criteria...");

                object folder = nameSpace.GetType().InvokeMember("GetDefaultFolder",
                    System.Reflection.BindingFlags.InvokeMethod, null, nameSpace, new object[] { folderId });

                object items = folder.GetType().GetProperty("Items").GetValue(folder);

                object foundItems = items.GetType().InvokeMember("Restrict",
                    System.Reflection.BindingFlags.InvokeMethod, null, items, new object[] { dateCriteria });

                int count = (int)foundItems.GetType().GetProperty("Count").GetValue(foundItems);
                Program.Logger?.Info($"Found {count} emails in {folderName} for date range");

                // Look through the results for subject match (in case of slight differences)
                for (int i = 1; i <= Math.Min(count, 10); i++) // Check first 10 results
                {
                    try
                    {
                        object mailItem = foundItems.GetType().InvokeMember("Item",
                            System.Reflection.BindingFlags.InvokeMethod, null, foundItems, new object[] { i });

                        object subjectObj = mailItem.GetType().GetProperty("Subject")?.GetValue(mailItem);
                        string itemSubject = subjectObj?.ToString() ?? "";

                        // Check if subjects match (allowing for slight variations)
                        if (itemSubject.Equals(originalSubject, StringComparison.OrdinalIgnoreCase) ||
                            itemSubject.Contains(originalSubject.Substring(0, Math.Min(20, originalSubject.Length))))
                        {
                            mailItem.GetType().InvokeMember("Display",
                                System.Reflection.BindingFlags.InvokeMethod, null, mailItem, new object[] { true });

                            Program.Logger?.Info($"SUCCESS: Opened email from {folderName} using date search");
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.Logger?.Error($"Error checking item {i}: {ex.Message}");
                        continue;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error in date search for {folderName}: {ex.Message}");
                return false;
            }
        }

        // Debug method - call this to see what the email data looks like
        private void DebugEmailData(ClientEmailMessage email)
        {
            string debugInfo = $"Email Debug Info:\n" +
                              $"ID: {email.Id ?? "NULL"}\n" +
                              $"Subject: '{email.Subject ?? "NULL"}'\n" +
                              $"FromName: '{email.FromName ?? "NULL"}'\n" +
                              $"FromAddress: '{email.FromAddress ?? "NULL"}'\n" +
                              $"ReceivedDate: {email.ReceivedDate:yyyy-MM-dd HH:mm:ss}\n" +
                              $"Direction: '{email.Direction ?? "NULL"}'\n" +
                              $"IsRead: {email.IsRead}\n";
                              //$"BodyPreview: '{(email.BodyPreview?.Substring(0, Math.Min(50, email.BodyPreview.Length ?? 0)) ?? "NULL")}'";

            MessageBox.Show(debugInfo, "Email Debug Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Program.Logger?.Info(debugInfo);
        }

        private async Task OpenEmailInDesktopOutlook(ClientEmailMessage email)
        {
            try
            {
                Program.Logger?.Info($"Attempting to open email: {email.Subject ?? "No Subject"}");

                // First check if Outlook is available
                if (!TestOutlookConnection())
                {
                    Program.Logger?.Info("Outlook COM not available, falling back to search method");

                    if (TryOpenOutlookWithSearch(email))
                    {
                        MessageBox.Show($"Outlook opened with search for:\n\"{email.Subject}\"",
                                      "Email Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    // Method 1: Try to open the email DIRECTLY using late-bound COM
                    if (await TryOpenEmailDirectlyLateBound(email))
                    {
                        Program.Logger?.Info("Successfully opened email directly via COM");
                        return;
                    }
                }

                // Method 2: Fallback - Open Outlook with search
                if (TryOpenOutlookWithSearch(email))
                {
                    Program.Logger?.Info("Opened Outlook with search parameters");
                    MessageBox.Show($"Could not find email directly. Outlook opened with search for:\n\"{email.Subject}\"",
                                  "Email Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Method 3: Last resort - Just open Outlook
                if (TryOpenOutlookDesktop())
                {
                    Program.Logger?.Info("Opened Outlook desktop as fallback");
                    MessageBox.Show($"Outlook opened. Please manually search for:\n\"{email.Subject}\"",
                                  "Manual Search Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("Could not open Outlook in any way");
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error in OpenEmailInDesktopOutlook", ex);
                MessageBox.Show($"Could not open email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> TryOpenEmailDirectlyLateBound(ClientEmailMessage email)
        {
            object outlookApp = null;
            object nameSpace = null;

            try
            {
                // Get Outlook application type
                Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                if (outlookType == null)
                {
                    Program.Logger?.Info("Outlook COM not available on this system");
                    return false;
                }

                try
                {
                    // Try existing instance first
                    outlookApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
                    Program.Logger?.Info("Connected to existing Outlook instance");
                }
                catch
                {
                    try
                    {
                        // Create new instance if none exists
                        outlookApp = Activator.CreateInstance(outlookType);
                        Program.Logger?.Info("Created new Outlook instance");

                        // Give Outlook time to initialize
                        await Task.Delay(3000);
                    }
                    catch (Exception ex)
                    {
                        Program.Logger?.Error($"Could not create Outlook instance: {ex.Message}");
                        return false;
                    }
                }

                if (outlookApp == null)
                {
                    Program.Logger?.Error("Outlook application is null");
                    return false;
                }

                // Get namespace using reflection
                try
                {
                    nameSpace = outlookApp.GetType().InvokeMember("GetNamespace",
                        System.Reflection.BindingFlags.InvokeMethod, null, outlookApp, new object[] { "MAPI" });
                }
                catch (Exception ex)
                {
                    Program.Logger?.Error($"Could not get MAPI namespace: {ex.Message}");
                    return false;
                }

                if (nameSpace == null)
                {
                    Program.Logger?.Error("MAPI namespace is null");
                    return false;
                }

                // Clean up subject for search
                string cleanSubject = email.Subject?.Replace("'", "''").Trim() ?? "";
                if (string.IsNullOrEmpty(cleanSubject))
                {
                    Program.Logger?.Warn("Email subject is empty, cannot search");
                    return false;
                }

                // Truncate very long subjects to avoid search issues
                if (cleanSubject.Length > 100)
                {
                    cleanSubject = cleanSubject.Substring(0, 100);
                }

                Program.Logger?.Info($"Searching for email with subject: '{cleanSubject}'");

                // Try simpler search strategies first
                var searchStrategies = new[]
                {
            // Start with exact subject match (most reliable)
            $"[Subject] = '{cleanSubject}'",
            
            // Then try partial matches if subject is long enough
            cleanSubject.Length > 10 ? $"[Subject] LIKE '{cleanSubject.Substring(0, Math.Min(20, cleanSubject.Length))}%'" : null,
            
            // Very loose match as last resort
            cleanSubject.Length > 5 ? $"[Subject] LIKE '%{cleanSubject.Substring(0, 10)}%'" : null
        };

                // Search in Inbox first (olFolderInbox = 6)
                try
                {
                    object inbox = nameSpace.GetType().InvokeMember("GetDefaultFolder",
                        System.Reflection.BindingFlags.InvokeMethod, null, nameSpace, new object[] { 6 });

                    if (inbox != null)
                    {
                        foreach (var searchCriteria in searchStrategies.Where(s => !string.IsNullOrEmpty(s)))
                        {
                            if (await TrySearchAndOpenEmail(inbox, searchCriteria, "Inbox"))
                                return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.Logger?.Error($"Error accessing Inbox: {ex.Message}");
                }

                // Search in Sent Items (olFolderSentMail = 5)
                try
                {
                    object sentItems = nameSpace.GetType().InvokeMember("GetDefaultFolder",
                        System.Reflection.BindingFlags.InvokeMethod, null, nameSpace, new object[] { 5 });

                    if (sentItems != null)
                    {
                        foreach (var searchCriteria in searchStrategies.Where(s => !string.IsNullOrEmpty(s)))
                        {
                            if (await TrySearchAndOpenEmail(sentItems, searchCriteria, "Sent Items"))
                                return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.Logger?.Error($"Error accessing Sent Items: {ex.Message}");
                }

                Program.Logger?.Info($"Email not found in any searched folder with subject: {cleanSubject}");
                return false;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"General error in direct email opening: {ex.Message}");
                return false;
            }
            finally
            {
                // Clean up COM objects
                try
                {
                    if (nameSpace != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(nameSpace);
                }
                catch { }

                try
                {
                    if (outlookApp != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp);
                }
                catch { }
            }
        }

        private bool TestOutlookConnection()
        {
            try
            {
                Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                if (outlookType == null)
                {
                    Program.Logger?.Info("Outlook COM not available");
                    return false;
                }

                object outlookApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
                if (outlookApp != null)
                {
                    Program.Logger?.Info("Outlook is running and accessible");
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Info($"Outlook connection test failed: {ex.Message}");
            }

            return false;
        }

        // Helper method to search in a specific folder and open email if found
        private async Task<bool> TrySearchAndOpenEmail(object folder, string searchCriteria, string folderName)
        {
            object items = null;
            object restrictedItems = null;

            try
            {
                // Check if folder is valid
                if (folder == null)
                {
                    Program.Logger?.Warn($"Folder {folderName} is null");
                    return false;
                }

                // Get items collection
                items = folder.GetType().GetProperty("Items")?.GetValue(folder);
                if (items == null)
                {
                    Program.Logger?.Warn($"Items collection is null for folder {folderName}");
                    return false;
                }

                // Apply search restriction
                try
                {
                    restrictedItems = items.GetType().InvokeMember("Restrict",
                        System.Reflection.BindingFlags.InvokeMethod, null, items, new object[] { searchCriteria });
                }
                catch (Exception ex)
                {
                    Program.Logger?.Error($"Error applying search restriction in {folderName}: {ex.Message}");
                    return false;
                }

                // Check if restricted items is valid
                if (restrictedItems == null)
                {
                    Program.Logger?.Info($"No search results returned for {folderName}");
                    return false;
                }

                // Get count safely
                object countObj = restrictedItems.GetType().GetProperty("Count")?.GetValue(restrictedItems);
                if (countObj == null)
                {
                    Program.Logger?.Warn($"Could not get count from search results in {folderName}");
                    return false;
                }

                int count = (int)countObj;
                Program.Logger?.Info($"Found {count} items in {folderName} with criteria: {searchCriteria}");

                if (count > 0)
                {
                    try
                    {
                        // Get the first matching item
                        object mailItem = restrictedItems.GetType().InvokeMember("Item",
                            System.Reflection.BindingFlags.InvokeMethod, null, restrictedItems, new object[] { 1 });

                        if (mailItem != null)
                        {
                            // Display the email directly (this opens the EMAIL WINDOW)
                            mailItem.GetType().InvokeMember("Display",
                                System.Reflection.BindingFlags.InvokeMethod, null, mailItem, new object[] { true });

                            Program.Logger?.Info($"Successfully opened email from {folderName}");

                            // Clean up mail item
                            try
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(mailItem);
                            }
                            catch { }

                            return true;
                        }
                        else
                        {
                            Program.Logger?.Warn($"Mail item is null in {folderName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.Logger?.Error($"Error opening email from {folderName}: {ex.Message}");
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error searching {folderName}: {ex.Message}");
                return false;
            }
            finally
            {
                // Clean up COM objects
                try
                {
                    if (restrictedItems != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(restrictedItems);
                }
                catch { }

                try
                {
                    if (items != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(items);
                }
                catch { }
            }
        }

        private async Task<bool> TryOpenEmailViaOutlookCOM(ClientEmailMessage email)
        {
            try
            {
                dynamic outlookApp = null;

                try
                {
                    // Try to get existing Outlook instance
                    outlookApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
                    Program.Logger?.Info("Connected to existing Outlook instance");
                }
                catch
                {
                    // If no existing instance, create new one
                    Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                    if (outlookType != null)
                    {
                        outlookApp = Activator.CreateInstance(outlookType);
                        Program.Logger?.Info("Created new Outlook instance");
                    }
                }

                if (outlookApp != null)
                {
                    var nameSpace = outlookApp.GetNamespace("MAPI");

                    // Clean the subject for searching (remove special characters)
                    string cleanSubject = email.Subject?.Replace("'", "''") ?? "";
                    if (cleanSubject.Length > 50)
                        cleanSubject = cleanSubject.Substring(0, 50);

                    // Create search criteria
                    var searchDate = email.ReceivedDate.ToString("MM/dd/yyyy");
                    var searchCriteria = $"[Subject] = '{cleanSubject}' AND [ReceivedTime] >= '{searchDate}'";

                    Program.Logger?.Info($"Searching for email with criteria: {searchCriteria}");

                    // Search in Inbox first (olFolderInbox = 6)
                    var inbox = nameSpace.GetDefaultFolder(6);
                    var items = inbox.Items.Restrict(searchCriteria);

                    if (items.Count > 0)
                    {
                        var mailItem = items[1]; // Outlook collections are 1-based
                        mailItem.Display(true); // true = modal
                        Program.Logger?.Info("Found and opened email from Inbox");
                        return true;
                    }

                    // If not found in inbox, search in Sent Items (olFolderSentMail = 5)
                    var sentItems = nameSpace.GetDefaultFolder(5);
                    items = sentItems.Items.Restrict(searchCriteria);

                    if (items.Count > 0)
                    {
                        var mailItem = items[1];
                        mailItem.Display(true);
                        Program.Logger?.Info("Found and opened email from Sent Items");
                        return true;
                    }

                    // Try broader search without exact subject match
                    var broaderCriteria = $"[ReceivedTime] >= '{searchDate}' AND [ReceivedTime] <= '{email.ReceivedDate.AddDays(1):MM/dd/yyyy}'";
                    items = inbox.Items.Restrict(broaderCriteria);

                    // Look through results for partial subject match
                    for (int i = 1; i <= Math.Min(items.Count, 10); i++)
                    {
                        var item = items[i];
                        if (item.Subject != null && item.Subject.Contains(email.Subject?.Substring(0, Math.Min(20, email.Subject.Length)) ?? ""))
                        {
                            item.Display(true);
                            Program.Logger?.Info("Found and opened email with partial subject match");
                            return true;
                        }
                    }

                    Program.Logger?.Info("Email not found in Outlook folders");
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error using Outlook COM", ex);
            }

            return false;
        }

        // Add this method for search-based opening
        private bool TryOpenOutlookWithSearch(ClientEmailMessage email)
        {
            try
            {
                string outlookPath = GetOutlookPath();
                if (string.IsNullOrEmpty(outlookPath))
                    return false;

                string searchTerm = email.Subject.Trim();
                if (searchTerm.Length > 30)
                    searchTerm = searchTerm.Substring(0, 30);

                // Remove characters that might cause command line issues
                searchTerm = System.Text.RegularExpressions.Regex.Replace(searchTerm, @"[^\w\s]", " ");
                searchTerm = System.Text.RegularExpressions.Regex.Replace(searchTerm, @"\s+", " ").Trim();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    Program.Logger?.Info("Search term became empty after cleaning");
                    return TryOpenOutlookBasic();
                }

                var startInfo = new ProcessStartInfo
                {
                    FileName = outlookPath,
                    Arguments = $"/search \"{searchTerm}\"",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                Process.Start(new ProcessStartInfo("outlook:") { UseShellExecute = true });
                return true;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error opening Outlook with search", ex);
                return false;
            }
        }



        // Add this method for basic Outlook opening
        private bool TryOpenOutlookDesktop()
        {
            try
            {
                string outlookPath = GetOutlookPath();

                if (!string.IsNullOrEmpty(outlookPath))
                {
                    Process.Start(new ProcessStartInfo(outlookPath) { UseShellExecute = true });
                    return true;
                }
                else
                {
                    // Try using outlook: protocol as fallback
                    Process.Start(new ProcessStartInfo("outlook:") { UseShellExecute = true });
                    return true;
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error opening Outlook desktop", ex);
                return false;
            }
        }


        private string GetOutlookPath()
        {
            // Common Outlook installation paths
            string[] paths = {
        @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE",
        @"C:\Program Files (x86)\Microsoft Office\root\Office16\OUTLOOK.EXE",
        @"C:\Program Files\Microsoft Office\Office16\OUTLOOK.EXE",
        @"C:\Program Files (x86)\Microsoft Office\Office16\OUTLOOK.EXE",
        @"C:\Program Files\Microsoft Office\root\Office15\OUTLOOK.EXE",
        @"C:\Program Files (x86)\Microsoft Office\root\Office15\OUTLOOK.EXE"
    };

            foreach (string path in paths)
            {
                if (File.Exists(path))
                    return path;
            }

            // Try registry as fallback
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\OUTLOOK.EXE"))
                {
                    string path = key?.GetValue("")?.ToString();
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                        return path;
                }
            }
            catch { }

            return null;
        }

        private void SetupTasksTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Tasks list for the full tasks tab
            var tasksList = new ListView
            {
                Location = new Point(20, 20),
                Size = new Size(900, Math.Max(400, tab.Height - 50)),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                BackColor = ModernColors.Surface,
                ForeColor = ModernColors.TextPrimary,
                Font = new Font("Segoe UI", 9F),
                //BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            tasksList.Columns.Add("Type", 100);
            tasksList.Columns.Add("Name", 250);
            tasksList.Columns.Add("Status", 120);
            tasksList.Columns.Add("Created", 110);
            tasksList.Columns.Add("Updated", 110);
            tasksList.Columns.Add("By", 150);

            tab.Controls.Add(tasksList);

            // Load all tasks for this client
            LoadTasksIntoTab(tasksList);
        }

        private void LoadTasksIntoTab(ListView tasksList)
        {
            try
            {
                if (_clientId > 0)
                {
                    var tasks = Program.Repository.List<Instruction, int>(x => x.ClientId == _clientId)
                                                 .OrderByDescending(x => x.CreateDate)
                                                 .ToList(); // Get all tasks, not just recent ones

                    tasksList.Items.Clear();

                    foreach (var task in tasks)
                    {
                        var item = new ListViewItem(task.Type?.ToString() ?? "Unknown");
                        item.SubItems.Add(task.TaskName ?? task.Description ?? "Unknown Task");
                        item.SubItems.Add(task.Status ?? "Unknown");
                        item.SubItems.Add(task.CreateDate.ToString("dd-MMM-yyyy"));
                        item.SubItems.Add(task.UpdateDate.ToString("dd-MMM-yyyy"));
                        item.SubItems.Add(task.UpdateBy ?? "Unknown");

                        // Color coding based on status
                        string status = task.Status ?? "";
                        if (status.Contains("Completed") || status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                        {
                            item.BackColor = Color.LightGreen;
                            item.ForeColor = Color.DarkGreen;
                        }
                        else
                        {
                            item.BackColor = Color.LightCoral;
                            item.ForeColor = Color.DarkRed;
                        }

                        tasksList.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                var item = new ListViewItem($"Error loading tasks: {ex.Message}");
                tasksList.Items.Add(item);
            }
        }

        private void SetupCallsTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;
            // Your existing calls logic can go here
        }

        private void SetupWhatsAppTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;
            // Your existing WhatsApp logic can go here
        }

        private void SetupDocumentsTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;
            // Your existing documents logic can go here
        }

        private void SetupNotesTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;
            // Your existing notes logic can go here
        }

        // Existing business logic methods remain the same
        private void LoadClientData()
        {
            if (_clientId <= 0)
            {
                try
                {
                    var allClients = Finx.App.Program.ClientDetailsService.ListView(x => x.ClientId > 0);
                    var firstClient = allClients.FirstOrDefault();
                    if (firstClient != null)
                    {
                        _clientId = firstClient.ClientId;
                        _clientDetails = firstClient;
                    }
                    else
                    {
                        MessageBox.Show("No clients found in database");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading client list: " + ex.Message);
                    return;
                }
            }

            try
            {
                if (_clientDetails == null)
                {
                    var clientList = Finx.App.Program.ClientDetailsService.ListView(x => x.ClientId == _clientId);
                    _clientDetails = clientList.FirstOrDefault();
                }

                if (_clientDetails != null)
                {
                    this.Text = $"Client Dashboard - {_clientDetails.Fullname}";
                    UpdateClientSidebar();
                    RefreshOverviewEmails();
                    tabControl?.Invalidate();
                    tabControl?.Refresh();
                    LoadEmailsTab();
                }
                else
                {
                    MessageBox.Show($"Client with ID {_clientId} not found in database");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading client data: " + ex.Message);
            }
        }

        private void RefreshOverviewEmails()
        {
            try
            {
                // Find the overview tab
                var overviewTab = tabControl.TabPages.Cast<TabPage>()
                    .FirstOrDefault(t => t.Text.Contains("Overview"));

                if (overviewTab != null)
                {
                    // Find the email list in the overview tab
                    var emailList = overviewTab.Controls.OfType<ListView>()
                        .FirstOrDefault(lv => lv.Location.Y < 200); // The emails list is at the top

                    if (emailList != null)
                    {
                        LoadEmailsIntoOverview(emailList);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error refreshing overview emails", ex);
            }
        }

        private void LoadTasksIntoOverview(ListView tasksList)
        {
            try
            {
                if (_clientId > 0)
                {
                    var tasks = Program.Repository.List<Instruction, int>(x => x.ClientId == _clientId)
                                                 .OrderByDescending(x => x.CreateDate)
                                                 .Take(10)
                                                 .ToList();

                    tasksList.Items.Clear();

                    foreach (var task in tasks)
                    {
                        var item = new ListViewItem(task.Type?.ToString() ?? "Unknown"); // Task Type
                        item.SubItems.Add(task.TaskName ?? task.Description ?? "Unknown Task"); // Name
                        item.SubItems.Add(task.Status ?? "Unknown"); // Status
                        item.SubItems.Add(task.CreateDate.ToString("dd-MMM-yyyy")); // Created
                        item.SubItems.Add(task.UpdateDate.ToString("dd-MMM-yyyy")); // Updated
                        item.SubItems.Add(task.UpdateBy ?? "Unknown"); // By

                        // Color coding based on status
                        string status = task.Status ?? "";
                        if (status.Contains("Completed") || status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                        {
                            item.BackColor = Color.LightGreen;
                            item.ForeColor = Color.DarkGreen;
                        }
                        else if (status.Contains("Outstanding") ||
                                 status.Contains("Pending") ||
                                 !status.Contains("Completed"))
                        {
                            item.BackColor = Color.LightCoral;
                            item.ForeColor = Color.DarkRed;
                        }

                        tasksList.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                var item = new ListViewItem($"Error loading tasks: {ex.Message}");
                tasksList.Items.Add(item);
            }
        }

        private async void LoadEmailsTab()
        {
            if (_clientId <= 0) return;

            try
            {
                string clientEmail = _clientDetails?.RecipientAddress;

                if (string.IsNullOrEmpty(clientEmail))
                {
                    currentEmailCount = 0;
                    UpdateStatsPanel();
                    return;
                }

                if (!EmailService.IsAvailable())
                {
                    currentEmailCount = 0;
                    UpdateStatsPanel();
                    return;
                }

                var emailService = new EmailService();
                var allEmails = await emailService.GetEmailsForClient(clientEmail, 50);

                // Count the emails from the API response
                currentEmailCount = allEmails.Count;

                // Load the existing methods
                await LoadInboxEmails();
                await LoadOutboxEmails();

                // Update stats with the actual count
                UpdateStatsPanel();

                // Only initialize timer if it doesn't exist
                if (emailRefreshTimer == null)
                {
                    InitializeEmailAutoRefresh();
                }
            }
            catch (Exception ex)
            {
                currentEmailCount = 0;
                UpdateStatsPanel();
                Program.Logger?.Error("LoadEmailsTab error", ex);
            }
        }

        //private void DisplayEmailsInList(List<EmailMessage> emails)
        //{
        //    if (emailListView == null) return;

        //    emailListView.Items.Clear();

        //    foreach (var email in emails)
        //    {
        //        var status = email.IsRead ? "○" : "●";
        //        var item = new ListViewItem(status);
        //        item.SubItems.Add(email.FromName ?? "Unknown");
        //        item.SubItems.Add(email.Subject ?? "No Subject");
        //        item.SubItems.Add(email.BodyPreview ?? "");
        //        item.SubItems.Add(email.ReceivedDate.ToString("dd-MMM-yyyy HH:mm"));
        //        item.Tag = email; // Store email data

        //        if (!email.IsRead)
        //        {
        //            item.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        //            item.BackColor = ColorTranslator.FromHtml("#eff6ff");
        //        }

        //        emailListView.Items.Add(item);
        //    }
        //}

        //private List<ClientEmailMessage> CreateTestEmails(string clientEmail)
        //{
        //    var emails = new List<EmailMessage>
        //    {
        //        new EmailMessage { Subject = "Meeting Follow-up", FromName = "John Smith", ReceivedDate = DateTime.Now.AddDays(-1), IsRead = false, BodyPreview = "Thanks for the meeting today..." },
        //        new EmailMessage { Subject = "Policy Update", FromName = "Admin", ReceivedDate = DateTime.Now.AddDays(-2), IsRead = true, BodyPreview = "Your policy has been updated..." },
        //        new EmailMessage { Subject = "Annual Review", FromName = clientEmail, ReceivedDate = DateTime.Now.AddDays(-3), IsRead = true, BodyPreview = "Looking forward to our annual review..." }
        //    };
        //            return emails;
        //}

        private void InitializeEmailAutoRefresh()
        {
            try
            {
                Program.Logger?.Info("=== InitializeEmailAutoRefresh START ===");
                Program.Logger?.Info($"Current emailRefreshTimer state: {(emailRefreshTimer == null ? "NULL" : "EXISTS")}");
                Program.Logger?.Info($"autoRefreshEnabled: {autoRefreshEnabled}");

                if (emailRefreshTimer != null)
                {
                    Program.Logger?.Info("Stopping and disposing existing timer");
                    emailRefreshTimer.Tick -= EmailRefreshTimer_Tick; // Remove old handler
                    emailRefreshTimer.Stop();
                    emailRefreshTimer.Dispose();
                    emailRefreshTimer = null;
                }

                Program.Logger?.Info("Creating new Timer instance");
                emailRefreshTimer = new Timer();
                emailRefreshTimer.Interval = 2 * 60 * 1000; // 2 minutes = 120000 ms

                Program.Logger?.Info("Attaching Tick event handler");
                emailRefreshTimer.Tick += EmailRefreshTimer_Tick;

                if (autoRefreshEnabled)
                {
                    Program.Logger?.Info($"Starting timer with interval: {emailRefreshTimer.Interval}ms");
                    emailRefreshTimer.Start();
                    Program.Logger?.Info($"Timer.Enabled = {emailRefreshTimer.Enabled}");
                    Program.Logger?.Info("Email auto-refresh started - checking every 2 minutes");
                }
                else
                {
                    Program.Logger?.Info("Auto-refresh is disabled - timer NOT started");
                }

                Program.Logger?.Info("=== InitializeEmailAutoRefresh COMPLETE ===");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error initializing email auto-refresh", ex);
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab?.Text.Contains("Email") == true)
            {
                LoadEmailsTab();
            }
        }

        // Your existing event handlers remain the same
        private async void BtnTestEmails_Click(object sender, EventArgs e)
        {
            try
            {
                string clientEmail = _clientDetails?.RecipientAddress;

                if (string.IsNullOrEmpty(clientEmail))
                {
                    MessageBox.Show("Client has no email address");
                    return;
                }

                // Check if Outlook is connected
                if (!OutlookAuthenticationService.IsAuthenticated)
                {
                    MessageBox.Show("Outlook not connected. Click 'Connect Outlook' to authenticate.");
                    return;
                }

                // Show current Outlook user info
                MessageBox.Show($"Outlook connected!\n\nUser: {OutlookAuthenticationService.UserName}\nEmail: {OutlookAuthenticationService.UserEmail}");

                // Test email service
                var emailService = new EmailService();
                var emails = await emailService.GetEmailsForClient(clientEmail, 10);

                string result = $"Email search results for {clientEmail}:\n\nFound {emails.Count} emails\n\n";

                if (emails.Count > 0)
                {
                    foreach (var email in emails.Take(5))
                    {
                        result += $"Subject: {email.Subject}\n";
                        result += $"From: {email.FromName}\n";
                        result += $"Date: {email.ReceivedDate:dd-MMM-yyyy HH:mm}\n";
                        result += $"Direction: {email.Direction}\n";
                        result += "---\n";
                    }
                }

                ShowEmailResults(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Test error: {ex.Message}");
            }
        }

        private void ShowEmailResults(string results)
        {
            var emailForm = new Form();
            emailForm.Text = "Email Test Results";
            emailForm.Size = new Size(600, 400);
            emailForm.StartPosition = FormStartPosition.CenterParent;

            var textBox = new TextBox();
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Both;
            textBox.Dock = DockStyle.Fill;
            textBox.Text = results;
            textBox.ReadOnly = true;
            textBox.Font = new Font("Consolas", 9);

            emailForm.Controls.Add(textBox);
            emailForm.ShowDialog(this);
        }



        private async Task ConnectToOutlook()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                bool connected = await OutlookAuthenticationService.AuthenticateAsync(showPrompts: true);

                if (connected && OutlookAuthenticationService.IsAuthenticated)
                {
                    MessageBox.Show(
                        $"Successfully connected to Outlook!\n\n" +
                        $"User: {OutlookAuthenticationService.UserName}\n" +
                        $"Email: {OutlookAuthenticationService.UserEmail}",
                        "Outlook Connected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadEmailsTab();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to Outlook: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async Task RefreshEmailsManually()
        {
            try
            {
                if (_clientId <= 0 || _clientDetails == null)
                    return;

                ShowEmailMessage("Loading...", inboxGrid);
                ShowEmailMessage("Loading...", outboxGrid);
                await Task.Delay(500); // Brief pause for UI feedback
                LoadEmailsTab();
            }
            catch (Exception ex)
            {
                //ShowEmailMessage($"Error refreshing emails: {ex.Message}");
            }
        }

        private void BtnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (_clientDetails != null && !string.IsNullOrEmpty(_clientDetails.RecipientAddress))
                {
                    List<ClientDetailsView> recipients = new List<ClientDetailsView> { _clientDetails };
                    var frm = new frmClientCommunication(recipients, CommunicationAction.SendEmail);
                    frm.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("Client has no email address");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending email: {ex.Message}");
            }
        }

        private void BtnCall_Click(object sender, EventArgs e)
        {
            try
            {
                if (_clientDetails != null && !string.IsNullOrEmpty(_clientDetails.RecipientCell))
                {
                    MessageBox.Show($"Calling client at {_clientDetails.RecipientCell}");
                }
                else
                {
                    MessageBox.Show("Client has no phone number");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error making call: {ex.Message}");
            }
        }

        private void BtnSMS_Click(object sender, EventArgs e)
        {
            try
            {
                if (_clientDetails != null && !string.IsNullOrEmpty(_clientDetails.RecipientCell))
                {
                    List<ClientDetailsView> recipients = new List<ClientDetailsView> { _clientDetails };
                    var frm = new frmClientCommunication(recipients, CommunicationAction.SendSMS);
                    frm.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("Client has no cell number");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending SMS: {ex.Message}");
            }
        }

        //private async Task OpenEmailInOutlook(ListView listView, int rowIndex)
        //{
        //    try
        //    {
        //        if (rowIndex < 0 || rowIndex >= listView.Items.Count)
        //            return;

        //        var item = listView.Items[rowIndex];
        //        var email = item.Tag as EmailMessage;

        //        if (email != null)
        //        {
        //            string subject = email.Subject ?? "";
        //            string from = email.FromName ?? "";

        //            try
        //            {
        //                // Try to open Outlook
        //                easiplan.app.Extensions.OutlookProxyExtensions.OpenOutlookDesktop();
        //                Program.Logger?.Info($"Opened email in Outlook: {subject}");
        //            }
        //            catch (Exception outlookEx)
        //            {
        //                MessageBox.Show($"Could not open Outlook: {outlookEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error in OpenEmailInOutlook: {ex.Message}");
        //    }
        //}

        // Auto-refresh functionality

        private async void EmailRefreshTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Program.Logger?.Info("========================================");
                Program.Logger?.Info("=== TIMER TICK EVENT FIRED!!! ===");
                Program.Logger?.Info("========================================");

                if (!ShouldAutoRefresh())
                {
                    Program.Logger?.Info("ShouldAutoRefresh returned FALSE - not refreshing");
                    return;
                }

                Program.Logger?.Info("ShouldAutoRefresh returned TRUE - proceeding with refresh");
                emailRefreshTimer.Stop();

                await RefreshEmailsQuietly();

                if (autoRefreshEnabled && emailRefreshTimer != null)
                {
                    Program.Logger?.Info("Restarting timer");
                    emailRefreshTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error during email auto-refresh timer tick", ex);

                if (autoRefreshEnabled && emailRefreshTimer != null)
                    emailRefreshTimer.Start();
            }
        }

        private bool ShouldAutoRefresh()
        {
            try
            {
                Program.Logger?.Info("=== Checking if should auto-refresh ===");

                if (!autoRefreshEnabled)
                {
                    Program.Logger?.Info("Auto-refresh is disabled");
                    return false;
                }

                if (_clientId <= 0 || _clientDetails == null)
                {
                    Program.Logger?.Info($"Invalid client: ID={_clientId}");
                    return false;
                }

                if (string.IsNullOrEmpty(_clientDetails.RecipientAddress))
                {
                    Program.Logger?.Info("Client has no email address");
                    return false;
                }

                if (!OutlookAuthenticationService.IsAuthenticated)
                {
                    Program.Logger?.Info("Outlook not authenticated");
                    return false;
                }

                // REMOVED THE TAB CHECK - now refreshes on ANY tab

                var secondsSinceLastRefresh = DateTime.Now.Subtract(lastRefreshTime).TotalSeconds;
                if (secondsSinceLastRefresh < 30)
                {
                    Program.Logger?.Info($"Too soon: {secondsSinceLastRefresh}s since last refresh");
                    return false;
                }

                Program.Logger?.Info("Auto-refresh ALLOWED");
                return true;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error checking if should auto-refresh", ex);
                return false;
            }
        }

        private async Task RefreshEmailsQuietly()
        {
            try
            {
                Program.Logger?.Info("=== RefreshEmailsQuietly START ===");

                string clientEmail = _clientDetails?.RecipientAddress;

                if (string.IsNullOrEmpty(clientEmail) || !EmailService.IsAvailable())
                    return;

                // Get FRESH email count from API
                var emailService = new EmailService();
                var allEmails = await emailService.GetEmailsForClient(clientEmail, 50);

                Program.Logger?.Info($"API returned {allEmails.Count} total emails");
                Program.Logger?.Info($"Old currentEmailCount: {currentEmailCount}");

                // Update the count
                currentEmailCount = allEmails.Count;

                Program.Logger?.Info($"New currentEmailCount: {currentEmailCount}");

                // Reload inbox and outbox grids
                await LoadInboxEmails();
                await LoadOutboxEmails();

                lastRefreshTime = DateTime.Now;

                // Update stats panel
                Program.Logger?.Info("Updating stats panel...");
                UpdateStatsPanel();

                // FORCE the stats panel to repaint
                statsPanel?.Invalidate();
                statsPanel?.Update();
                statsPanel?.Refresh();

                // Update title
                this.Text = $"Client Dashboard - {_clientDetails.Fullname} ({currentEmailCount} emails) - Last refresh: {DateTime.Now:HH:mm}";

                Program.Logger?.Info($"=== RefreshEmailsQuietly COMPLETE === Total: {currentEmailCount} emails");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error during quiet email refresh", ex);
            }
        }


        private void RefreshOverviewEmailsList()
        {
            try
            {
                Program.Logger?.Info("=== RefreshOverviewEmailsList START ===");

                // Find the overview tab
                var overviewTab = tabControl.TabPages.Cast<TabPage>()
                    .FirstOrDefault(t => t.Text.Contains("Overview"));

                if (overviewTab == null)
                {
                    Program.Logger?.Info("Overview tab not found");
                    return;
                }

                // Find the email ListView (has 3 columns)
                var emailList = overviewTab.Controls.OfType<ListView>()
                    .FirstOrDefault(lv => lv.Columns.Count == 3);

                if (emailList == null)
                {
                    Program.Logger?.Info("Email ListView not found in Overview tab");
                    return;
                }

                Program.Logger?.Info($"Found email ListView with {emailList.Items.Count} items");

                // Reload it
                LoadEmailsIntoOverview(emailList);

                Program.Logger?.Info("=== RefreshOverviewEmailsList COMPLETE ===");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error refreshing overview emails list", ex);
            }
        }

        private async Task RefreshOverviewEmailsAsync()
        {
            try
            {
                Program.Logger?.Info("=== RefreshOverviewEmailsAsync START ===");

                string clientEmail = _clientDetails?.RecipientAddress;

                if (string.IsNullOrEmpty(clientEmail) || !EmailService.IsAvailable())
                {
                    Program.Logger?.Info("Cannot refresh overview - no email or service unavailable");
                    return;
                }

                // Find the overview tab
                var overviewTab = tabControl.TabPages.Cast<TabPage>()
                    .FirstOrDefault(t => t.Text.Contains("Overview"));

                if (overviewTab == null)
                {
                    Program.Logger?.Info("Overview tab not found");
                    return;
                }

                Program.Logger?.Info("Found Overview tab");

                // Find the Recent Emails ListView
                var emailList = overviewTab.Controls.OfType<ListView>()
                    .FirstOrDefault(lv => lv.Columns.Count == 3);

                if (emailList == null)
                {
                    Program.Logger?.Info($"Email ListView not found. ListViews in tab: {overviewTab.Controls.OfType<ListView>().Count()}");
                    foreach (var lv in overviewTab.Controls.OfType<ListView>())
                    {
                        Program.Logger?.Info($"  ListView: Columns={lv.Columns.Count}, Location={lv.Location}");
                    }
                    return;
                }

                Program.Logger?.Info($"Found email ListView with {emailList.Items.Count} items");

                // Reload the emails
                var emailService = new EmailService();
                var allEmails = await emailService.GetEmailsForClient(clientEmail, 5);

                Program.Logger?.Info($"Fetched {allEmails.Count} emails for overview");

                emailList.Items.Clear();
                foreach (var email in allEmails)
                {
                    var item = new ListViewItem(email.Subject ?? "No Subject");
                    item.SubItems.Add(email.FromName ?? "Unknown");
                    item.SubItems.Add(email.ReceivedDate.ToString("dd-MMM"));
                    item.Tag = email;

                    if (!email.IsRead)
                    {
                        item.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        item.BackColor = ColorTranslator.FromHtml("#eff6ff");
                    }

                    emailList.Items.Add(item);
                }

                Program.Logger?.Info($"Updated overview with {emailList.Items.Count} emails");
                Program.Logger?.Info("=== RefreshOverviewEmailsAsync COMPLETE ===");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error refreshing overview emails", ex);
            }
        }

        private void ShowNewEmailNotification(int emailCount, string clientEmail)
        {
            try
            {
                string originalTitle = this.Text;
                this.Text = $"🔔 NEW EMAIL for {_clientDetails.Fullname}";

                Timer notificationTimer = new Timer();
                notificationTimer.Interval = 3000;
                notificationTimer.Tick += (s, e) =>
                {
                    this.Text = originalTitle;
                    notificationTimer.Stop();
                    notificationTimer.Dispose();
                };
                notificationTimer.Start();

                Program.Logger?.Info($"New email notification shown for {clientEmail}");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error showing new email notification", ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_emailRefreshTimer?.Stop();
                //_emailRefreshTimer?.Dispose();
                emailRefreshTimer?.Stop();
                emailRefreshTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmClientDashboard_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }

    // Custom Modern UI Controls
    public class ModernPanel : Panel
    {
        public ModernPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw subtle border
            using (var pen = new Pen(frmClientDashboard.ModernColors.Border, 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
            base.OnPaint(e);
        }
    }

    public class ModernButton : Button
    {
        private bool isPrimary;

        public ModernButton(string text, bool primary = false)
        {
            Text = text;
            isPrimary = primary;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            Cursor = Cursors.Hand;

            UpdateColors();

            FlatAppearance.BorderSize = 1;
        }

        private void UpdateColors()
        {
            if (isPrimary)
            {
                BackColor = frmClientDashboard.ModernColors.Primary;
                ForeColor = Color.White;
                FlatAppearance.BorderColor = frmClientDashboard.ModernColors.Primary;
            }
            else
            {
                BackColor = frmClientDashboard.ModernColors.Surface;
                ForeColor = frmClientDashboard.ModernColors.TextSecondary;
                FlatAppearance.BorderColor = frmClientDashboard.ModernColors.Border;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (isPrimary)
            {
                BackColor = frmClientDashboard.ModernColors.PrimaryHover;
            }
            else
            {
                BackColor = ColorTranslator.FromHtml("#f8fafc");
                ForeColor = frmClientDashboard.ModernColors.Primary;
                FlatAppearance.BorderColor = frmClientDashboard.ModernColors.Primary;
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            UpdateColors();
            base.OnMouseLeave(e);
        }
    }

    public class ModernAvatar : Control
    {
        private string initials;

        public ModernAvatar(string initials)
        {
            this.initials = initials;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create gradient background
            using (var brush = new LinearGradientBrush(ClientRectangle,
                ColorTranslator.FromHtml("#667eea"), ColorTranslator.FromHtml("#764ba2"),
                LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillEllipse(brush, ClientRectangle);
            }

            // Draw initials - FIXED FontStyle.Bold reference
            using (var font = new Font("Segoe UI", 18F, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.White))
            {
                var size = e.Graphics.MeasureString(initials, font);
                var x = (Width - size.Width) / 2;
                var y = (Height - size.Height) / 2;
                e.Graphics.DrawString(initials, font, brush, x, y);
            }

            base.OnPaint(e);
        }
    }

    public class ModernTabControl : TabControl
    {
        public ModernTabControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer, true);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            ItemSize = new Size(90, 35);  // Smaller tabs
            SizeMode = TabSizeMode.Fixed; // Add this line
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var tab = TabPages[e.Index];
            var tabRect = GetTabRect(e.Index);

            // Draw tab background
            var backColor = e.Index == SelectedIndex ?
                frmClientDashboard.ModernColors.Surface :
                frmClientDashboard.ModernColors.Background;

            using (var brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, tabRect);
            }

            // Draw tab text
            var textColor = Color.Black;

            using (var brush = new SolidBrush(textColor))
            {
                var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                e.Graphics.DrawString(tab.Text, Font, brush, tabRect, stringFormat);
            }

            // Draw bottom border for active tab
            if (e.Index == SelectedIndex)
            {
                using (var pen = new Pen(frmClientDashboard.ModernColors.Primary, 2))
                {
                    e.Graphics.DrawLine(pen, tabRect.Left, tabRect.Bottom - 1,
                                      tabRect.Right, tabRect.Bottom - 1);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw tab area background
            using (var brush = new SolidBrush(frmClientDashboard.ModernColors.Surface))
            {
                e.Graphics.FillRectangle(brush, 0, 0, Width, ItemSize.Height + 4);
            }

            // Draw bottom border
            using (var pen = new Pen(frmClientDashboard.ModernColors.Border, 1))
            {
                e.Graphics.DrawLine(pen, 0, ItemSize.Height + 3, Width, ItemSize.Height + 3);
            }

            base.OnPaint(e);
        }
    }
}