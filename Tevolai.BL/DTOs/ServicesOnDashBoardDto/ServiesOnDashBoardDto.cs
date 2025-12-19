using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tevolai.DAL.Entities;

namespace Tevolai.BL.DTOs.ServicesOnDashBoardDto
{
    public class ServiesOnDashBoardDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string ServiceName { get; set; } = string.Empty; // dev can add any name to identify the service
        public string Domain { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string ApplicationUserId { get; set; } = string.Empty;
        public WebsiteCMS CMS { get; set; }
        public string? HostingServer { get; set; }
        public WebsiteStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}