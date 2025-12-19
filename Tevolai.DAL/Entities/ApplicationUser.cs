using Microsoft.AspNetCore.Identity;

namespace Tevolai.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<ServicesOnDashBoard> ServicesOnDashBoards { get; set; } = new HashSet<ServicesOnDashBoard>();
    }
}