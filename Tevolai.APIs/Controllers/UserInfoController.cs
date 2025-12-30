using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tevolai.DAL.Entities;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserInfoController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserInfoController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("my-roles")]
        public async Task<IActionResult> GetMyRoles()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var roles = await _userManager.GetRolesAsync(user);
            var tokenRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            return Ok(new
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                RolesInDatabase = roles,
                RolesInToken = tokenRoles,
                HasAdminRole = roles.Contains("Admin"),
                TokenHasAdminRole = tokenRoles.Contains("Admin"),
                Message = tokenRoles.Any() 
                    ? "Token has roles. If database has Admin but token doesn't, please login again." 
                    : "Token has no roles. Please login again to get a fresh token with roles."
            });
        }
    }
}

