namespace Tevolai.DAL.Entities
{
    public enum Language
    {
        English,
        Arabic,
    }

    public class ServicesOnMain
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Language Language { get; set; } = Language.English;
        public bool IsDeleted { get; set; } = false;

        // Navigation
        public virtual ICollection<Revenue>? Revenues { get; set; } = new HashSet<Revenue>();
    }
}