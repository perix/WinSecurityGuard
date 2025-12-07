using System;
using System.Media;
using System.Windows;
using WinSecurityGuard.Models;

namespace WinSecurityGuard.Services
{
    public class NotificationService
    {
        public void ShowAlert(SecurityEvent securityEvent)
        {
            // Play Sound
            try
            {
                SystemSounds.Hand.Play(); // Critical sound
            }
            catch { }

            // Show Popup on UI thread
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var alert = new AlertWindow(securityEvent);
                alert.Show();
            });
        }
    }
}
