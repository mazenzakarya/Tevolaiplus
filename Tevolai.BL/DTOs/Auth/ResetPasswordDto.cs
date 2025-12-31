using System;
using System.Collections.Generic;
using System.Text;

namespace Tevolai.BL.DTOs.Auth
{
    public class ResetPasswordDto
    {
        public string NewPassword { get; set; } = string.Empty;
    }
}