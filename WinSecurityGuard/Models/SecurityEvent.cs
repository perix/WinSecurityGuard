using System;

namespace WinSecurityGuard.Models
{
    public class SecurityEvent
    {
        public DateTime Timestamp { get; set; }
        public string? EventId { get; set; }
        public string? Category { get; set; } // e.g. "Accesso Fallito"
        public string? Message { get; set; }  // Simplified message
        public string? Details { get; set; }  // Raw or detailed info
        public bool IsCritical { get; set; }
    }
}
