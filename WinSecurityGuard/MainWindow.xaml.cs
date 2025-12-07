using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using WinSecurityGuard.Models;

namespace WinSecurityGuard
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<SecurityEvent> Events { get; set; } = new ObservableCollection<SecurityEvent>();

        public MainWindow()
        {
            InitializeComponent();
            LogsGrid.ItemsSource = Events;
        }

        public void AddEvent(SecurityEvent evt)
        {
            Dispatcher.Invoke(() => 
            {
                Events.Insert(0, evt); // Add to top
            });
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // Don't close, just hide
            e.Cancel = true;
            this.Hide();
        }
    }
}