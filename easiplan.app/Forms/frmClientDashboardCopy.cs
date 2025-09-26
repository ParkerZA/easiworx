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

namespace easiplan.app.Forms
{
    public partial class frmClientDashboard : MetroForm
    {
        private int _clientId;
        private ClientDetailsView _clientDetails;
        private bool _isLoadingEmails = false;
        private Timer _emailRefreshTimer;
        private bool _autoRefreshEnabled = true;
        private int _refreshIntervalMinutes = 3;
        private int currentEmailCount = 0;
        private string _currentEasiWorxUser => Program.User?.Username ?? "DefaultUser";

        private Timer emailRefreshTimer;
        private bool autoRefreshEnabled = true;
        private int refreshIntervalMinutes = 1;
        private int lastEmailCount = 0;
        private DateTime lastRefreshTime = DateTime.MinValue;
        private DataGridView inboxGrid;
        private DataGridView outboxGrid;

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
            this.Controls.Clear();

            // Create sidebar - simple panel with fixed width
            clientSidebar = new Panel
            {
                Width = 250,
                Dock = DockStyle.Left,
                BackColor = ModernColors.Surface,
                Padding = new Padding(16)
            };

            // Add border to sidebar
            clientSidebar.Paint += (s, e) =>
            {
                using (var pen = new Pen(ModernColors.Border, 1))
                {
                    e.Graphics.DrawLine(pen, clientSidebar.Width - 1, 0, clientSidebar.Width - 1, clientSidebar.Height);
                }
            };

            this.Controls.Add(clientSidebar);

            // Create main content - simple panel that fills remaining space
            mainContent = new Panel
            {
                Dock = DockStyle.Fill,  // This fills the remaining space after sidebar
                BackColor = ModernColors.Background,
                Padding = new Padding(0)
            };

            CreateMainContentControls();
            this.Controls.Add(mainContent);
        }

        private Panel CreateSimpleMainContent(Panel parentPanel)
        {
            // Header panel
            var headerPanel = new Panel
            {
                Size = new Size(parentPanel.Width, 60),
                Location = new Point(0, 0),
                BackColor = ModernColors.Surface,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Add border
            headerPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(ModernColors.Border, 1))
                {
                    e.Graphics.DrawLine(pen, 0, headerPanel.Height - 1, headerPanel.Width, headerPanel.Height - 1);
                }
            };

            // Title
            var title = new Label
            {
                Text = "Client Dashboard",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(20, 18),
                Size = new Size(300, 24)
            };
            headerPanel.Controls.Add(title);

            // Buttons - simple positioning
            var refreshBtn = new ModernButton("🔄 Refresh", false)
            {
                Size = new Size(85, 32),
                Location = new Point(headerPanel.Width - 200, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            refreshBtn.Click += async (s, e) => await RefreshEmailsManually();

            var settingsBtn = new ModernButton("⚙️ Settings", false)
            {
                Size = new Size(85, 32),
                Location = new Point(headerPanel.Width - 105, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            headerPanel.Controls.Add(refreshBtn);
            headerPanel.Controls.Add(settingsBtn);

            // Tab control - simple positioning
            tabControl = new TabControl
            {
                Location = new Point(0, 60),
                Size = new Size(parentPanel.Width, parentPanel.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular)
            };

            // Create tabs
            SetupAllTabs();

            parentPanel.Controls.Add(headerPanel);
            parentPanel.Controls.Add(tabControl);

            return parentPanel;
        }

        private void SetupAllTabs()
        {
            // Overview tab
            var overviewTab = new TabPage("Overview");
            SetupOverviewTab(overviewTab);
            tabControl.TabPages.Add(overviewTab);

            // Emails tab
            var emailsTab = new TabPage("Emails");
            SetupEmailsTab(emailsTab);
            tabControl.TabPages.Add(emailsTab);

            // Other tabs
            var tasksTab = new TabPage("Tasks");
            var callsTab = new TabPage("Calls");
            var whatsappTab = new TabPage("WhatsApp");
            var documentsTab = new TabPage("Documents");
            var notesTab = new TabPage("Notes");

            tabControl.TabPages.AddRange(new[] { tasksTab, callsTab, whatsappTab, documentsTab, notesTab });
        }

        private void CreateMainContentControls()
        {
            // Header panel
            var headerPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = ModernColors.Surface,
                Padding = new Padding(16)
            };

            // Add border to header
            headerPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(ModernColors.Border, 1))
                {
                    e.Graphics.DrawLine(pen, 0, headerPanel.Height - 1, headerPanel.Width, headerPanel.Height - 1);
                }
            };

            // Title
            var title = new Label
            {
                Text = "Client Dashboard",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(16, 18),
                Size = new Size(300, 24)
            };
            headerPanel.Controls.Add(title);

            // Refresh button
            var refreshBtn = new ModernButton("🔄 Refresh", false)
            {
                Size = new Size(85, 32),
                Location = new Point(headerPanel.Width - 195, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            refreshBtn.Click += async (s, e) => await RefreshEmailsManually();
            headerPanel.Controls.Add(refreshBtn);

            // Settings button
            var settingsBtn = new ModernButton("⚙️ Settings", false)
            {
                Size = new Size(85, 32),
                Location = new Point(headerPanel.Width - 105, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            headerPanel.Controls.Add(settingsBtn);

            mainContent.Controls.Add(headerPanel);

            // Tab control
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,  // Fills remaining space after header
                Font = new Font("Segoe UI", 9F, FontStyle.Regular)
            };

            SetupAllTabs();
            mainContent.Controls.Add(tabControl);
        }

        private void SetupSimpleOverviewTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Stats panel - simple positioning
            statsPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(tab.Width - 40, 100),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            tab.Controls.Add(statsPanel);

            // Recent Emails section
            var emailHeader = new Label
            {
                Text = "Recent Emails",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 140),
                Size = new Size(200, 25),
                ForeColor = ModernColors.TextPrimary
            };
            tab.Controls.Add(emailHeader);

            var emailList = new ListView
            {
                Location = new Point(20, 170),
                Size = new Size(tab.Width - 40, 150),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = ModernColors.Surface,
                Font = new Font("Segoe UI", 9F)
            };

            emailList.Columns.Add("Subject", 250);
            emailList.Columns.Add("From", 150);
            emailList.Columns.Add("Date", 100);

            tab.Controls.Add(emailList);

            // Recent Tasks section
            var tasksHeader = new Label
            {
                Text = "Recent Tasks",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 340),
                Size = new Size(200, 25),
                ForeColor = ModernColors.TextPrimary
            };
            tab.Controls.Add(tasksHeader);

            var tasksList = new ListView
            {
                Location = new Point(20, 370),
                Size = new Size(tab.Width - 40, 200),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = ModernColors.Surface,
                Font = new Font("Segoe UI", 9F)
            };

            tasksList.Columns.Add("Type", 80);
            tasksList.Columns.Add("Name", 200);
            tasksList.Columns.Add("Status", 100);
            tasksList.Columns.Add("Created", 80);
            tasksList.Columns.Add("Updated", 80);
            tasksList.Columns.Add("By", 100);

            tab.Controls.Add(tasksList);

            // Load data
            LoadEmailsIntoOverview(emailList);
            LoadTasksIntoOverview(tasksList);
            UpdateStatsPanel();
        }

        private void SetupSimpleEmailsTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Simple nested tab control
            var emailSubTabs = new TabControl
            {
                Location = new Point(10, 10),
                Size = new Size(tab.Width - 20, tab.Height - 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Font = new Font("Segoe UI", 9F)
            };

            // Inbox tab
            var inboxTab = new TabPage("Inbox");
            SetupSimpleInboxTab(inboxTab);
            emailSubTabs.TabPages.Add(inboxTab);

            // Outbox tab
            var outboxTab = new TabPage("Outbox");
            SetupSimpleOutboxTab(outboxTab);
            emailSubTabs.TabPages.Add(outboxTab);

            tab.Controls.Add(emailSubTabs);
        }

        private void SetupSimpleInboxTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Simple toolbar
            var toolbar = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(tab.Width - 20, 40),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var refreshBtn = new ModernButton("🔄 Refresh", false)
            {
                Size = new Size(80, 30),
                Location = new Point(0, 5)
            };
            refreshBtn.Click += async (s, e) => await LoadInboxEmails();

            var settingsBtn = new ModernButton("⚙️ Settings", false)
            {
                Size = new Size(80, 30),
                Location = new Point(90, 5)
            };

            toolbar.Controls.Add(refreshBtn);
            toolbar.Controls.Add(settingsBtn);
            tab.Controls.Add(toolbar);

            // Simple DataGridView
            var inboxGrid = new DataGridView
            {
                Name = "inboxListView",
                Location = new Point(10, 60),
                Size = new Size(tab.Width - 20, tab.Height - 70),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = ModernColors.Surface,
                Font = new Font("Segoe UI", 9F),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Simple column setup
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "●", Width = 30 });
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "From", HeaderText = "From", Width = 150 });
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subject", HeaderText = "Subject", Width = 250 });
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Preview", HeaderText = "Preview", Width = 200 });
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Date", HeaderText = "Date", Width = 120 });

            // Store reference for later use
            this.inboxGrid = inboxGrid;

            tab.Controls.Add(inboxGrid);
        }

        private void SetupSimpleOutboxTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Simple toolbar
            var toolbar = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(tab.Width - 20, 40),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var refreshBtn = new ModernButton("🔄 Refresh", false)
            {
                Size = new Size(80, 30),
                Location = new Point(0, 5)
            };
            refreshBtn.Click += async (s, e) => await LoadOutboxEmails();

            var settingsBtn = new ModernButton("⚙️ Settings", false)
            {
                Size = new Size(80, 30),
                Location = new Point(90, 5)
            };

            toolbar.Controls.Add(refreshBtn);
            toolbar.Controls.Add(settingsBtn);
            tab.Controls.Add(toolbar);

            // Simple DataGridView
            var outboxGrid = new DataGridView
            {
                Name = "outboxListView",
                Location = new Point(10, 60),
                Size = new Size(tab.Width - 20, tab.Height - 70),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = ModernColors.Surface,
                Font = new Font("Segoe UI", 9F),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Simple column setup
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "●", Width = 30 });
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "From", HeaderText = "From", Width = 150 });
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subject", HeaderText = "Subject", Width = 250 });
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Preview", HeaderText = "Preview", Width = 200 });
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Date", HeaderText = "Date", Width = 120 });

            // Store reference for later use
            this.outboxGrid = outboxGrid;

            tab.Controls.Add(outboxGrid);
        }




        private Panel CreateClientSidebar()
        {
            var sidebar = new ModernPanel
            {
                Width = 260, // Reduced width to prevent overlap
                Dock = DockStyle.Left,
                BackColor = ModernColors.Surface,
                Padding = new Padding(12)
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

            int yPos = 20;

            // Client name
            var nameLabel = new Label
            {
                Text = _clientDetails.Fullname,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(20, yPos),
                Size = new Size(210, 25)
            };
            clientSidebar.Controls.Add(nameLabel);
            yPos += 35;

            // Client ID
            var idLabel = new Label
            {
                Text = $"ID: {_clientDetails.IdentificationNo}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = ModernColors.TextSecondary,
                Location = new Point(20, yPos),
                Size = new Size(210, 20)
            };
            clientSidebar.Controls.Add(idLabel);
            yPos += 40;

            // Contact section header
            var contactHeader = new Label
            {
                Text = "CONTACT INFORMATION",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = ModernColors.TextSecondary,
                Location = new Point(20, yPos),
                Size = new Size(210, 18)
            };
            clientSidebar.Controls.Add(contactHeader);
            yPos += 25;

            // Email
            var emailLabel = new Label
            {
                Text = $"Email: {_clientDetails.RecipientAddress ?? "N/A"}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(20, yPos),
                Size = new Size(210, 18)
            };
            clientSidebar.Controls.Add(emailLabel);
            yPos += 25;

            // Cell
            var cellLabel = new Label
            {
                Text = $"Cell: {_clientDetails.RecipientCell ?? "N/A"}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = ModernColors.TextPrimary,
                Location = new Point(20, yPos),
                Size = new Size(210, 18)
            };
            clientSidebar.Controls.Add(cellLabel);
            yPos += 50;

            // Quick action buttons
            var emailBtn = new ModernButton("📧 Email", true)
            {
                Size = new Size(100, 32),
                Location = new Point(20, yPos)
            };
            emailBtn.Click += BtnEmail_Click;
            clientSidebar.Controls.Add(emailBtn);

            var callBtn = new ModernButton("📞 Call", false)
            {
                Size = new Size(100, 32),
                Location = new Point(130, yPos)
            };
            callBtn.Click += BtnCall_Click;
            clientSidebar.Controls.Add(callBtn);
            yPos += 45;

            var smsBtn = new ModernButton("💬 SMS", false)
            {
                Size = new Size(100, 32),
                Location = new Point(20, yPos)
            };
            smsBtn.Click += BtnSMS_Click;
            clientSidebar.Controls.Add(smsBtn);

            var testBtn = new ModernButton("🔍 Test", false)
            {
                Size = new Size(100, 32),
                Location = new Point(130, yPos)
            };
            testBtn.Click += BtnTestEmails_Click;
            clientSidebar.Controls.Add(testBtn);
            yPos += 45;

            // Connect Outlook button
            var outlookBtn = new ModernButton("📮 Connect Outlook", false)
            {
                Size = new Size(210, 32),
                Location = new Point(20, yPos)
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
                Width = 180,
                Location = new Point(header.Width - 196, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent
            };

            var refreshBtn = new ModernButton("🔄 Refresh", false)
            {
                Size = new Size(85, 32),
                Location = new Point(0, 0)
            };
            refreshBtn.Click += async (s, e) => await RefreshEmailsManually();

            var settingsBtn = new ModernButton("⚙️ Settings", false)
            {
                Size = new Size(85, 32),
                Location = new Point(95, 0)
            };

            buttonContainer.Controls.Add(refreshBtn);
            buttonContainer.Controls.Add(settingsBtn);
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

            // Stats panel
            statsPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(600, 100), // Fixed size to avoid calculation issues
                BackColor = Color.Transparent
            };
            tab.Controls.Add(statsPanel);

            // Recent Emails
            var emailHeader = new Label
            {
                Text = "Recent Emails",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 140),
                Size = new Size(200, 25),
                ForeColor = ModernColors.TextPrimary
            };
            tab.Controls.Add(emailHeader);

            var emailList = new ListView
            {
                Location = new Point(20, 170),
                Size = new Size(800, 150), // Fixed size
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = ModernColors.Surface,
                Font = new Font("Segoe UI", 9F)
            };

            emailList.Columns.Add("Subject", 300);
            emailList.Columns.Add("From", 200);
            emailList.Columns.Add("Date", 120);
            tab.Controls.Add(emailList);

            // Recent Tasks
            var tasksHeader = new Label
            {
                Text = "Recent Tasks",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 340),
                Size = new Size(200, 25),
                ForeColor = ModernColors.TextPrimary
            };
            tab.Controls.Add(tasksHeader);

            var tasksList = new ListView
            {
                Location = new Point(20, 370),
                Size = new Size(800, 200), // Fixed size
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = ModernColors.Surface,
                Font = new Font("Segoe UI", 9F)
            };

            tasksList.Columns.Add("Type", 80);
            tasksList.Columns.Add("Name", 250);
            tasksList.Columns.Add("Status", 100);
            tasksList.Columns.Add("Created", 120);
            tasksList.Columns.Add("Updated", 120);
            tasksList.Columns.Add("By", 130);
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
                displayEmails.Add(new ClientEmailMessage
                {
                    Id = email.Id,
                    Subject = email.Subject,
                    FromName = email.FromName,
                    FromAddress = email.ToAddresses, // Using ToAddresses as a proxy
                    BodyPreview = email.BodyPreview,
                    ReceivedDate = email.ReceivedDate,
                    IsRead = email.IsRead,
                    Direction = email.Direction
                });
            }

            return displayEmails;
        }

        private void UpdateStatsPanel()
        {
            if (statsPanel == null) return;

            try
            {
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

            // Nested tab control for inbox/outbox
            var emailSubTabs = new TabControl
            {
                Location = new Point(10, 10),
                Size = new Size(900, 500), // Fixed size to avoid issues
                Font = new Font("Segoe UI", 9F)
            };

            // Inbox tab
            var inboxTab = new TabPage("Inbox");
            SetupInboxTab(inboxTab);
            emailSubTabs.TabPages.Add(inboxTab);

            // Outbox tab
            var outboxTab = new TabPage("Outbox");
            SetupOutboxTab(outboxTab);
            emailSubTabs.TabPages.Add(outboxTab);

            tab.Controls.Add(emailSubTabs);
        }
        private void SetupInboxTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Toolbar
            var refreshBtn = new ModernButton("🔄 Refresh", false)
            {
                Size = new Size(80, 30),
                Location = new Point(10, 10)
            };
            refreshBtn.Click += async (s, e) => await LoadInboxEmails();
            tab.Controls.Add(refreshBtn);

            var settingsBtn = new ModernButton("⚙️ Settings", false)
            {
                Size = new Size(80, 30),
                Location = new Point(100, 10)
            };
            tab.Controls.Add(settingsBtn);

            // Email grid
            inboxGrid = new DataGridView
            {
                Name = "inboxListView",
                Location = new Point(10, 50),
                Size = new Size(860, 420), // Fixed size
                BackColor = ModernColors.Surface,
                Font = new Font("Segoe UI", 9F),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            };

            // Columns
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "●", Width = 40 });
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "From", HeaderText = "From", Width = 180 });
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subject", HeaderText = "Subject", Width = 280 });
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Preview", HeaderText = "Preview", Width = 240 });
            inboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Date", HeaderText = "Date", Width = 120 });

            tab.Controls.Add(inboxGrid);
        }

        private void SetupOutboxTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;

            // Toolbar
            var refreshBtn = new ModernButton("🔄 Refresh", false)
            {
                Size = new Size(80, 30),
                Location = new Point(10, 10)
            };
            refreshBtn.Click += async (s, e) => await LoadOutboxEmails();
            tab.Controls.Add(refreshBtn);

            var settingsBtn = new ModernButton("⚙️ Settings", false)
            {
                Size = new Size(80, 30),
                Location = new Point(100, 10)
            };
            tab.Controls.Add(settingsBtn);

            // Email grid
            outboxGrid = new DataGridView
            {
                Name = "outboxListView",
                Location = new Point(10, 50),
                Size = new Size(860, 420), // Fixed size
                BackColor = ModernColors.Surface,
                Font = new Font("Segoe UI", 9F),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            };

            // Columns
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "●", Width = 40 });
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "From", HeaderText = "From", Width = 180 });
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subject", HeaderText = "Subject", Width = 280 });
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Preview", HeaderText = "Preview", Width = 240 });
            outboxGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Date", HeaderText = "Date", Width = 120 });

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
            try
            {
                string clientEmail = _clientDetails?.RecipientAddress;

                if (string.IsNullOrEmpty(clientEmail))
                {
                    ShowEmailMessage("Client has no email address configured.", "inbox");
                    return;
                }

                if (!EmailService.IsAvailable())
                {
                    ShowEmailMessage("Outlook not connected. Click 'Connect Outlook' to authenticate.", "inbox");
                    return;
                }

                var emailService = new EmailService();
                var allEmails = await emailService.GetEmailsForClient(clientEmail, 50);

                // Filter for received emails (inbox)
                var inboxEmails = allEmails.Where(e => e.Direction == "Received").ToList();
                var displayEmails = ConvertToDisplayEmails(inboxEmails);

                DisplayEmailsInGrid(displayEmails, "inbox");

                // Update stats after loading emails
                UpdateStatsPanel();

                Program.Logger?.Info($"Loaded {inboxEmails.Count} inbox emails for {clientEmail}");
            }
            catch (Exception ex)
            {
                ShowEmailMessage($"Error loading inbox: {ex.Message}", "inbox");
                Program.Logger?.Error("Error loading inbox emails", ex);
            }
        }

        private async Task LoadOutboxEmails()
        {
            try
            {
                string clientEmail = _clientDetails?.RecipientAddress;

                if (string.IsNullOrEmpty(clientEmail))
                {
                    ShowEmailMessage("Client has no email address configured.", "outbox");
                    return;
                }

                if (!EmailService.IsAvailable())
                {
                    ShowEmailMessage("Outlook not connected. Click 'Connect Outlook' to authenticate.", "outbox");
                    return;
                }

                var emailService = new EmailService();
                var allEmails = await emailService.GetEmailsForClient(clientEmail, 50);

                // Filter for sent emails (outbox)
                var outboxEmails = allEmails.Where(e => e.Direction == "Sent").ToList();
                var displayEmails = ConvertToDisplayEmails(outboxEmails);

                DisplayEmailsInGrid(displayEmails, "outbox");

                // Update stats after loading emails
                UpdateStatsPanel();

                Program.Logger?.Info($"Loaded {outboxEmails.Count} outbox emails for {clientEmail}");
            }
            catch (Exception ex)
            {
                ShowEmailMessage($"Error loading outbox: {ex.Message}", "outbox");
                Program.Logger?.Error("Error loading outbox emails", ex);
            }
        }

        private void DisplayEmailsInGrid(List<ClientEmailMessage> emails, string gridType)
        {
            var gridView = FindEmailGrid(gridType);
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

                gridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

                if (gridView.Columns["Subject"] != null)
                    gridView.Columns["Subject"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (gridView.Columns["Preview"] != null)
                    gridView.Columns["Preview"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error displaying {gridType} emails", ex);
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

        private void ShowEmailMessage(string message, string gridType)
        {
            var gridView = FindEmailGrid(gridType);
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
            if (string.IsNullOrEmpty(text))
                return "";

            if (text.Length <= maxLength)
                return text;

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
        // Simplified version without COM interop
        private async Task OpenEmailInOutlook(DataGridView dataGridView, int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= dataGridView.Rows.Count)
                    return;

                var row = dataGridView.Rows[rowIndex];
                var email = row.Tag as ClientEmailMessage;

                if (email != null)
                {
                    // Try to open Outlook with search first
                    if (!TryOpenOutlookWithSearch(email))
                    {
                        // Fallback to just opening Outlook
                        TryOpenOutlookDesktop();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error opening email in Outlook", ex);
                MessageBox.Show($"Error opening email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool TryOpenOutlookWithSearch(ClientEmailMessage email)
        {
            try
            {
                string outlookPath = GetOutlookPath();

                if (!string.IsNullOrEmpty(outlookPath))
                {
                    // Create search term from email subject
                    string searchTerm = email.Subject?.Replace("\"", "'") ?? "";
                    if (searchTerm.Length > 50)
                        searchTerm = searchTerm.Substring(0, 50);

                    var startInfo = new ProcessStartInfo
                    {
                        FileName = outlookPath,
                        Arguments = $"/search \"{searchTerm}\"",
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Normal
                    };

                    Process.Start(startInfo);
                    Program.Logger?.Info($"Opened Outlook with search: {searchTerm}");
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

        private bool TryOpenOutlookDesktop()
        {
            try
            {
                string outlookPath = GetOutlookPath();

                if (!string.IsNullOrEmpty(outlookPath))
                {
                    Process.Start(new ProcessStartInfo(outlookPath) { UseShellExecute = true });
                    Program.Logger?.Info("Opened Outlook desktop application");
                    return true;
                }
                else
                {
                    // Try using outlook: protocol as fallback
                    Process.Start(new ProcessStartInfo("outlook:") { UseShellExecute = true });
                    Program.Logger?.Info("Opened Outlook using outlook: protocol");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error opening Outlook desktop", ex);
                MessageBox.Show("Could not find or open Outlook desktop application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private string GetOutlookPath()
        {
            try
            {
                // Common Outlook installation paths
                string[] outlookPaths = {
            @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE",
            @"C:\Program Files (x86)\Microsoft Office\root\Office16\OUTLOOK.EXE",
            @"C:\Program Files\Microsoft Office\Office16\OUTLOOK.EXE",
            @"C:\Program Files (x86)\Microsoft Office\Office16\OUTLOOK.EXE",
            @"C:\Program Files\Microsoft Office\root\Office15\OUTLOOK.EXE",
            @"C:\Program Files (x86)\Microsoft Office\root\Office15\OUTLOOK.EXE"
        };

                foreach (string path in outlookPaths)
                {
                    if (File.Exists(path))
                        return path;
                }

                // Try to find via registry
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\OUTLOOK.EXE"))
                {
                    if (key != null)
                    {
                        string path = key.GetValue("") as string;
                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                            return path;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error($"Error finding Outlook path: {ex.Message}");
            }

            return null;
        }

        private void SetupTasksTab(TabPage tab)
        {
            tab.BackColor = ModernColors.Background;
            tab.Padding = new Padding(24);

            var tasksList = new ListView
            {
                Location = new Point(24, 24),
                Size = new Size(tab.Width - 48, tab.Height - 48),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = ModernColors.Surface,
                Font = new Font("Segoe UI", 9F),
                //BorderStyle = BorderStyle.FixedSingle
            };

            // Add the same columns as overview
            tasksList.Columns.Add("Type", 80);
            tasksList.Columns.Add("Name", 250);
            tasksList.Columns.Add("Status", 100);
            tasksList.Columns.Add("Created", 100);
            tasksList.Columns.Add("Updated", 100);
            tasksList.Columns.Add("By", 120);

            LoadTasksIntoTab(tasksList);
            tab.Controls.Add(tasksList);
        }

        private void LoadTasksIntoTab(ListView tasksList)
        {
            try
            {
                if (_clientId > 0)
                {
                    var tasks = Program.Repository.List<Instruction, int>(x => x.ClientId == _clientId)
                                                 .OrderByDescending(x => x.CreateDate)
                                                 .ToList(); // Get all tasks, not just 10

                    tasksList.Items.Clear();

                    foreach (var task in tasks)
                    {
                        var item = new ListViewItem(task.Type?.ToString() ?? "Unknown");
                        item.SubItems.Add(task.TaskName ?? task.Description ?? "Unknown Task");
                        item.SubItems.Add(task.Status ?? "Unknown");
                        item.SubItems.Add(task.CreateDate.ToString("dd-MMM-yyyy"));
                        item.SubItems.Add(task.UpdateDate.ToString("dd-MMM-yyyy"));
                        item.SubItems.Add(task.UpdateBy ?? "Unknown");

                        // Same color coding
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

        private async void RefreshOverviewEmails()
        {
            try
            {
                var overviewTab = tabControl.TabPages[0];

                // Find the email ListView (first one with 3 columns)
                var emailList = overviewTab.Controls.OfType<ListView>()
                                           .FirstOrDefault(lv => lv.Columns.Count == 3 && lv.Columns[0].Text == "Subject");
                if (emailList != null)
                {
                     LoadEmailsIntoOverview(emailList);
                }

                // Find the tasks ListView (second one with 3 columns)
                var tasksList = overviewTab.Controls.OfType<ListView>()
                                           .FirstOrDefault(lv => lv.Columns.Count == 3 && lv.Columns[0].Text == "Task");
                if (tasksList != null)
                {
                    LoadTasksIntoOverview(tasksList);
                }
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error refreshing overview", ex);
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

                InitializeEmailAutoRefresh();
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
                if (emailRefreshTimer != null)
                {
                    emailRefreshTimer.Stop();
                    emailRefreshTimer.Dispose();
                }

                emailRefreshTimer = new Timer();
                emailRefreshTimer.Interval = 2 * 60 * 1000; // 2 minutes
                emailRefreshTimer.Tick += EmailRefreshTimer_Tick;

                if (autoRefreshEnabled)
                {
                    emailRefreshTimer.Start();
                    Program.Logger?.Info("Email auto-refresh started - checking every 2 minutes");
                }
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

                ShowEmailMessage("Loading...", "inbox");
                ShowEmailMessage("Loading...", "outbox");
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
                if (!ShouldAutoRefresh())
                    return;

                Program.Logger?.Info("Auto-refreshing emails...");

                emailRefreshTimer.Stop();
                await RefreshEmailsQuietly();

                if (autoRefreshEnabled)
                    emailRefreshTimer.Start();
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error during email auto-refresh", ex);

                if (autoRefreshEnabled && emailRefreshTimer != null)
                    emailRefreshTimer.Start();
            }
        }

        private bool ShouldAutoRefresh()
        {
            try
            {
                if (!autoRefreshEnabled)
                    return false;

                if (_clientId <= 0 || _clientDetails == null)
                    return false;

                if (string.IsNullOrEmpty(_clientDetails.RecipientAddress))
                    return false;

                if (!OutlookAuthenticationService.IsAuthenticated)
                    return false;

                if (tabControl.SelectedTab?.Text.Contains("Email") != true)
                    return false;

                if (DateTime.Now.Subtract(lastRefreshTime).TotalSeconds < 30)
                    return false;

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
                string clientEmail = _clientDetails?.RecipientAddress;

                if (string.IsNullOrEmpty(clientEmail) || !EmailService.IsAvailable())
                    return;

                await LoadInboxEmails();
                await LoadOutboxEmails();

                lastRefreshTime = DateTime.Now;

                // Update title with refresh time
                var emailService = new EmailService();
                var allEmails = await emailService.GetEmailsForClient(clientEmail, 50);
                var totalCount = allEmails.Count;

                this.Text = $"Client Dashboard - {_clientDetails.Fullname} ({totalCount} emails) - Last refresh: {DateTime.Now:HH:mm}";

                Program.Logger?.Info($"Email auto-refresh completed. Found {totalCount} emails.");

            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error during quiet email refresh", ex);
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
                _emailRefreshTimer?.Stop();
                _emailRefreshTimer?.Dispose();
                emailRefreshTimer?.Stop();
                emailRefreshTimer?.Dispose();
            }
            base.Dispose(disposing);
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