using System.ComponentModel.DataAnnotations.Schema;

namespace Tevolai.DAL.Entities
{
    public class Revenue
    {
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string? PaymentMethod { get; set; }

        public string? Notes { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Navigation
        public virtual Customer? Customer { get; set; }

        public virtual ServicesOnDashBoard? Service { get; set; }
    }
}