using System;
using System.Diagnostics;
using System.IO;

namespace easiplan.app.Extensions
{
    public static class OutlookProxyExtensions
    {
        /// <summary>
        /// Opens the default Outlook desktop application
        /// </summary>
        /// <param name="proxy">The OutlookProxy instance (can be null)</param>
        public static void OpenOutlookDesktop(this za.co.easiworx.office365.net.OutlookProxy proxy)
        {
            OpenOutlookDesktop();
        }

        /// <summary>
        /// Opens Outlook with a new email to the specified address
        /// </summary>
        /// <param name="proxy">The OutlookProxy instance (can be null)</param>
        /// <param name="emailAddress">The email address to send to</param>
        public static void OpenOutlookToEmailAddress(this za.co.easiworx.office365.net.OutlookProxy proxy, string emailAddress)
        {
            OpenOutlookToEmailAddress(emailAddress);
        }

        /// <summary>
        /// Static method to open Outlook desktop application
        /// </summary>
        public static void OpenOutlookDesktop()
        {
            try
            {
                // Try multiple common Outlook installation paths
                string[] outlookPaths = {
                    @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE",
                    @"C:\Program Files (x86)\Microsoft Office\root\Office16\OUTLOOK.EXE",
                    @"C:\Program Files\Microsoft Office\Office16\OUTLOOK.EXE",
                    @"C:\Program Files (x86)\Microsoft Office\Office16\OUTLOOK.EXE",
                    @"C:\Program Files\Microsoft Office\root\Office15\OUTLOOK.EXE",
                    @"C:\Program Files (x86)\Microsoft Office\root\Office15\OUTLOOK.EXE",
                    @"C:\Program Files\Microsoft Office\Office15\OUTLOOK.EXE",
                    @"C:\Program Files (x86)\Microsoft Office\Office15\OUTLOOK.EXE"
                };

                bool outlookLaunched = false;
                foreach (string path in outlookPaths)
                {
                    if (File.Exists(path))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = path,
                            UseShellExecute = true
                        });
                        outlookLaunched = true;
                        break;
                    }
                }

                if (!outlookLaunched)
                {
                    // Fallback: try to open via protocol
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "outlook:",
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error launching Outlook: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Static method to open Outlook with a new email to specified address
        /// </summary>
        /// <param name="emailAddress">The email address to send to</param>
        public static void OpenOutlookToEmailAddress(string emailAddress)
        {
            try
            {
                if (string.IsNullOrEmpty(emailAddress))
                {
                    OpenOutlookDesktop();
                    return;
                }

                var mailtoUrl = $"mailto:{emailAddress}";
                Process.Start(new ProcessStartInfo
                {
                    FileName = mailtoUrl,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error opening Outlook to email address: {ex.Message}", ex);
            }
        }
    }
}