using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tevolai.BL.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}