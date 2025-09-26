using System;
using System.Threading.Tasks;
using System.Threading;
using za.co.easiworx.office365.net;
using za.co.easiworx.office365.net.models;
using za.co.easiworx.office365.net.Exceptions;
using Finx.App.Extensions;
using Finx.App;

namespace easiplan.app.Services
{
    /// <summary>
    /// Centralized Outlook authentication service that manages the connection throughout the application
    /// </summary>
    public static class OutlookAuthenticationService
    {
        private static bool _isInitialized = false;
        private static bool _isAuthenticated = false;
        private static string _userEmail = null;
        private static string _userName = null;
        private static OutlookProxy _outlookProxy = null;

        /// <summary>
        /// Check if Outlook is currently authenticated and available
        /// </summary>
        public static bool IsAuthenticated => _isAuthenticated && _outlookProxy != null;

        /// <summary>
        /// Get the current authenticated user's email
        /// </summary>
        public static string UserEmail => _userEmail;

        /// <summary>
        /// Get the current authenticated user's display name
        /// </summary>
        public static string UserName => _userName;

        /// <summary>
        /// Get the OutlookProxy instance (null if not authenticated)
        /// </summary>
        public static OutlookProxy OutlookProxy => _outlookProxy;

        /// <summary>
        /// Attempt to authenticate with Outlook immediately after EasiWorx login
        /// </summary>
        /// <param name="showPrompts">Whether to show user prompts and messages</param>
        /// <returns>True if authentication successful or user chose to skip</returns>
        public static async Task<bool> AuthenticateAsync(bool showPrompts = true)
        {
            if (_isAuthenticated && _outlookProxy != null)
            {
                // Already authenticated, test if still valid
                try
                {
                    var profile = await _outlookProxy.GetMyProfile();
                    if (profile != null)
                    {
                        return true; // Still valid
                    }
                }
                catch
                {
                    // Authentication expired, need to re-authenticate
                    _isAuthenticated = false;
                    _outlookProxy = null;
                }
            }

            try
            {
                Program.Logger?.Info("Starting Outlook authentication...");

                if (showPrompts)
                {
                    var result = System.Windows.Forms.MessageBox.Show(
                        "EasiWorx can integrate with your Outlook account to:\n\n" +
                        "• View client emails directly in the Client Dashboard\n" +
                        "• Track email communications with clients\n" +
                        "• Enable quick email actions\n\n" +
                        "This will open a browser window for you to sign in with your Microsoft account.\n\n" +
                        "Would you like to connect to Outlook now?\n\n" +
                        "(You can skip this and connect later from the Client Dashboard)",
                        "Connect to Outlook?",
                        System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Question);

                    if (result == System.Windows.Forms.DialogResult.No)
                    {
                        Program.Logger?.Info("User chose to skip Outlook authentication");
                        return true; // User chose to skip, allow login to continue
                    }
                }

                // Create a new OutlookProxy with timeout protection
                using (var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(3)))
                {
                    _outlookProxy = new OutlookProxy(new AuthenticationModel());

                    // Test authentication by getting user profile
                    var userProfile = await GetUserProfileWithTimeout(_outlookProxy, cancellationTokenSource.Token);

                    if (userProfile != null)
                    {
                        _isAuthenticated = true;
                        _userEmail = userProfile.Mail ?? userProfile.UserPrincipalName;
                        _userName = userProfile.DisplayName;

                        // Store in Program class for backward compatibility
                        Program.OutlookProxy = _outlookProxy;
                        Program.IsOutlookAuthenticated = true;
                        Program.OutlookUserEmail = _userEmail;

                        Program.Logger?.Info($"Outlook authentication successful for: {_userName} ({_userEmail})");

                        if (showPrompts)
                        {
                            System.Windows.Forms.MessageBox.Show(
                                $"Successfully connected to Outlook!\n\n" +
                                $"User: {_userName}\n" +
                                $"Email: {_userEmail}\n\n" +
                                "You can now view client emails in the Client Dashboard.",
                                "Outlook Connected",
                                System.Windows.Forms.MessageBoxButtons.OK,
                                System.Windows.Forms.MessageBoxIcon.Information);
                        }

                        return true;
                    }
                    else
                    {
                        throw new Exception("Failed to retrieve user profile");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Program.Logger?.Warn("Outlook authentication timed out");
                return HandleAuthenticationError("Outlook authentication timed out. Please try again later.", showPrompts);
            }
            catch (Office365AuthenticationFailedException ex)
            {
                Program.Logger?.Error("Outlook authentication failed", ex);
                return HandleAuthenticationError($"Outlook authentication failed: {ex.Message}", showPrompts);
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Unexpected error during Outlook authentication", ex);
                return HandleAuthenticationError($"Error connecting to Outlook: {ex.Message}", showPrompts);
            }
        }

        /// <summary>
        /// Get user profile with timeout protection to prevent stack overflow
        /// </summary>
        private static async Task<Microsoft.Graph.User> GetUserProfileWithTimeout(OutlookProxy proxy, CancellationToken cancellationToken)
        {
            var task = proxy.GetMyProfile();

            // Wait for the task with cancellation support
            while (!task.IsCompleted && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(100, CancellationToken.None); // Check every 100ms
            }

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Authentication timed out");
            }

            if (task.IsFaulted)
            {
                throw task.Exception?.GetBaseException() ?? new Exception("Unknown error getting user profile");
            }

            return task.Result;
        }

        /// <summary>
        /// Handle authentication errors and give user options
        /// </summary>
        private static bool HandleAuthenticationError(string errorMessage, bool showPrompts)
        {
            _isAuthenticated = false;
            _outlookProxy = null;
            Program.OutlookProxy = null;
            Program.IsOutlookAuthenticated = false;

            if (!showPrompts)
                return true; // Silent failure, allow login to continue

            var result = System.Windows.Forms.MessageBox.Show(
                $"{errorMessage}\n\n" +
                "Would you like to continue without Outlook integration?\n\n" +
                "You can try connecting to Outlook later from the Client Dashboard.",
                "Outlook Connection Failed",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Warning);

            return result == System.Windows.Forms.DialogResult.Yes;
        }

        /// <summary>
        /// Manually disconnect from Outlook
        /// </summary>
        public static void Disconnect()
        {
            try
            {
                _isAuthenticated = false;
                _userEmail = null;
                _userName = null;
                _outlookProxy = null;

                // Clear Program class references
                Program.OutlookProxy = null;
                Program.IsOutlookAuthenticated = false;
                Program.OutlookUserEmail = null;

                Program.Logger?.Info("Disconnected from Outlook");
            }
            catch (Exception ex)
            {
                Program.Logger?.Error("Error disconnecting from Outlook", ex);
            }
        }

        /// <summary>
        /// Try to reconnect to Outlook (useful for expired sessions)
        /// </summary>
        public static async Task<bool> ReconnectAsync()
        {
            Disconnect();
            return await AuthenticateAsync(showPrompts: true);
        }

        /// <summary>
        /// Silent authentication attempt (no user prompts)
        /// </summary>
        public static async Task<bool> TryAuthenticateSilentlyAsync()
        {
            return await AuthenticateAsync(showPrompts: false);
        }

        /// <summary>
        /// Check if Outlook authentication is available and working
        /// </summary>
        public static async Task<bool> TestConnectionAsync()
        {
            if (!_isAuthenticated || _outlookProxy == null)
                return false;

            try
            {
                var profile = await _outlookProxy.GetMyProfile();
                return profile != null;
            }
            catch
            {
                _isAuthenticated = false;
                return false;
            }
        }
    }
}