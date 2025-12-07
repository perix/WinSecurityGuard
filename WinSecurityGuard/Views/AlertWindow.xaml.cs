using System;
using System.Windows;
using WinSecurityGuard.Models;

namespace WinSecurityGuard
{
    public partial class AlertWindow : Window
    {
        public AlertWindow(SecurityEvent evt)
        {
            InitializeComponent();
            TxtTitle.Text = evt.Category.ToUpper();
            TxtMessage.Text = evt.Message;

            // Position bottom-right
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 10;
            this.Top = desktopWorkingArea.Bottom - this.Height - 10;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
