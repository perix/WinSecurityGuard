using System;
using System.Windows;
using WinSecurityGuard.Services;
using WinSecurityGuard.Models;
using System.Windows.Forms; // Alias or explicit usage
using Application = System.Windows.Application; // disambiguate

namespace WinSecurityGuard
{
    public partial class App : Application
    {
        private NotifyIcon _notifyIcon;
        private MainWindow _historyWindow;
        private SecurityMonitor _monitor;
        private NotificationService _notificationService;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Initialize Services
            _historyWindow = new MainWindow(); // Created but not shown defined by StartupUri removal
            _notificationService = new NotificationService();
            _monitor = new SecurityMonitor();

            // Subscribe
            _monitor.OnSecurityAnomaly += Monitor_OnSecurityAnomaly;

            // Start Monitor
            _monitor.Start();

            // Create Tray Icon
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = System.Drawing.SystemIcons.Shield; // Generic shield icon
            _notifyIcon.Visible = true;
            _notifyIcon.Text = "WinSecurityGuard - Monitor Attivo";
            
            // Context Menu
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Apri Storico", null, (s, args) => ShowHistory());
            contextMenu.Items.Add("Simula Attacco (Test)", null, (s, args) => SimulateAttack());
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("Esci", null, (s, args) => Shutdown());
            _notifyIcon.ContextMenuStrip = contextMenu;

            // Double Click
            _notifyIcon.DoubleClick += (s, args) => ShowHistory();
        }

        private void Monitor_OnSecurityAnomaly(object sender, SecurityEvent e)
        {
             // 1. Add to History
             _historyWindow.AddEvent(e);

             // 2. Alert
             _notificationService.ShowAlert(e);
        }

        private void SimulateAttack()
        {
            var fakeEvent = new SecurityEvent
            {
                Timestamp = DateTime.Now,
                EventId = "TEST",
                Category = "Test di Sicurezza",
                Message = "Questa è una simulazione di attacco per verificare che gli avvisi funzionino correttamente.",
                Details = "Nessun pericolo reale.",
                IsCritical = true
            };
            Monitor_OnSecurityAnomaly(this, fakeEvent);
        }

        private void ShowHistory()
        {
            _historyWindow.Show();
            _historyWindow.Activate();
            _historyWindow.WindowState = WindowState.Normal;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _monitor?.Stop();
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
            }
        }
    }
}
