namespace Tevolai.DAL.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

        // Navigation
        public ICollection<ServicesOnDashBoard>? Services { get; set; } = new HashSet<ServicesOnDashBoard>();

        public ICollection<Revenue>? Revenues { get; set; } = new HashSet<Revenue>();
        public ICollection<EmailCredential>? EmailCredentials { get; set; } = new HashSet<EmailCredential>();
    }
}