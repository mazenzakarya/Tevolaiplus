using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tevolai.DAL.Entities
{
    public enum WebsiteStatus
    {
        Active,
        Suspended,
        InProgress
    }

    public enum WebsiteCMS
    {
        WordPress,
        DotNet,
        Joomla,
        Drupal,
        Custom
    }

    public enum PaidStatus
    {
        paid,
        unpaid,
        partialPaid
    }

    public class ServicesOnDashBoard
    {
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; } = string.Empty;

        public string ServiceName { get; set; } = string.Empty; // dev can add any name to identify the service
        public string Domain { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public WebsiteCMS CMS { get; set; }
        public string? HostingServer { get; set; }
        public WebsiteStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public PaidStatus PaidStatus { get; set; } = PaidStatus.paid;

        // Navigation
        public virtual Customer? Customer { get; set; }

        public virtual ApplicationUser? ApplicationUsers { get; set; }
    }
}