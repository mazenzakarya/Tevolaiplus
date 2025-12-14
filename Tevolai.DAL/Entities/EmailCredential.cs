using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Tevolai.DAL.Entities
{
    public class EmailCredential
    {
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }

        public string EmailAddress { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? HostingPlatform { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? LastUsedAt { get; set; }

        public virtual Customer? Customer { get; set; }

        // AuditLog بصيغة JSON
        public string? AuditLog { get; set; }

        // مساعدة لإضافة حدث جديد
        public void AddAuditEvent(string action, string? by = null)
        {
            var events = string.IsNullOrEmpty(AuditLog)
                ? new List<AuditEvent>()
                : JsonSerializer.Deserialize<List<AuditEvent>>(AuditLog)!;

            events.Add(new AuditEvent
            {
                Action = action,
                By = by,
                Date = DateTime.UtcNow
            });

            AuditLog = JsonSerializer.Serialize(events);
        }
    }

    //Audit event class (helper)
    public class AuditEvent
    {
        public string Action { get; set; } = string.Empty;
        public string? By { get; set; }
        public DateTime Date { get; set; }
    }
}