using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WinSecurityGuard.Models;

namespace WinSecurityGuard.Services
{
    public class SecurityMonitor
    {
        private EventLog _securityLog;
        public event EventHandler<SecurityEvent>? OnSecurityAnomaly;

        public SecurityMonitor()
        {
            _securityLog = new EventLog("Security");
            _securityLog.EntryWritten += OnEntryWritten;
        }

        public void Start()
        {
            try 
            {
                _securityLog.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                // Note: This requires Admin privileges
                Debug.WriteLine($"Error starting monitor: {ex.Message}");
            }
        }

        public void Stop()
        {
            _securityLog.EnableRaisingEvents = false;
        }

        private void OnEntryWritten(object sender, EntryWrittenEventArgs e)
        {
            var evt = AnalyzeEvent(e.Entry);
            if (evt != null)
            {
                OnSecurityAnomaly?.Invoke(this, evt);
            }
        }

        private SecurityEvent? AnalyzeEvent(EventLogEntry entry)
        {
            // Simple logic for Italy locale and simplfied language
            long id = entry.InstanceId; // Sometimes InstanceId is big, usually Casting to int is fine for standard IDs
            int eventId = (int)(id & 0xFFFF); // Win32 Event ID mask

            if (eventId == 4625) // Failed Login
            {
                return new SecurityEvent
                {
                    Timestamp = entry.TimeGenerated,
                    EventId = "4625",
                    Category = "Tentativo di Intrusione",
                    Message = "È stato rilevato un tentativo di accesso fallito al sistema.",
                    Details = entry.Message,
                    IsCritical = true
                };
            }
            else if (eventId == 4720) // New User Created
            {
                return new SecurityEvent
                {
                    Timestamp = entry.TimeGenerated,
                    EventId = "4720",
                    Category = "Creazione Utente",
                    Message = "È stato creato un nuovo utente nel sistema. Se non sei stato tu, è un rischio grave.",
                    Details = entry.Message,
                    IsCritical = true
                };
            }
            else if (eventId == 1102) // Log Cleared
            {
                 return new SecurityEvent
                {
                    Timestamp = entry.TimeGenerated,
                    EventId = "1102",
                    Category = "Manomissione Sicurezza",
                    Message = "Il registro degli eventi di sicurezza è stato cancellato!",
                    Details = entry.Message,
                    IsCritical = true
                };
            }
             else if (eventId == 4732) // Member added to local admin
            {
                 return new SecurityEvent
                {
                    Timestamp = entry.TimeGenerated,
                    EventId = "4732",
                    Category = "Scalata Privilegi",
                    Message = "Un utente è stato aggiunto al gruppo Amministratori.",
                    Details = entry.Message,
                    IsCritical = true
                };
            }

            return null;
        }
    }
}
