namespace Tevolai.BL.DTOs.RevenueDto
{
    public class RevenueDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ServiceId { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string? PaymentMethod { get; set; }

        public string? Notes { get; set; }
    }
}