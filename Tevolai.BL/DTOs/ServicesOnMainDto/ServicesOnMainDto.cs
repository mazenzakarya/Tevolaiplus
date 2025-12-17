using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tevolai.DAL.Entities;

namespace Tevolai.BL.DTOs.ServicesOnMainDto
{
    public class ServicesOnMainDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public bool IsActive { get; set; } = true;
        public Language Language { get; set; } = Language.English;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}