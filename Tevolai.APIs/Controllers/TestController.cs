using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var isInAdminRole = User.IsInRole("Admin");
            
            return Ok(new
            {
                UserName = User.Identity?.Name,
                IsAuthenticated = User.Identity?.IsAuthenticated,
                AllClaims = claims,
                Roles = roles,
                IsInAdminRole = isInAdminRole,
                HasAdminRole = roles.Contains("Admin")
            });
        }
    }
}

