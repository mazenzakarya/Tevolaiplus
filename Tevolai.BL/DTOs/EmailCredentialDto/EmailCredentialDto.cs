namespace Tevolai.BL.DTOs.EmailCredentialDto
{
    public class EmailCredentialDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string DecryptedPassword { get; set; } = string.Empty; // Only populated when needed
        public string UserName { get; set; } = string.Empty;
        public string? HostingPlatform { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public string? AuditLog { get; set; }
    }
}

