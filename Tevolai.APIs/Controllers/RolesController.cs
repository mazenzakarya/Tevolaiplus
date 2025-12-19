using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tevolai.DAL.Entities;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public record CreateRoleRequest(string RoleName);

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RoleName))
                return BadRequest(new { message = "RoleName is required" });

            var roleName = request.RoleName.Trim();

            if (await _roleManager.RoleExistsAsync(roleName))
                return Conflict(new { message = "Role already exists" });

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Role created", roleName });
        }

        public record AssignRoleRequest(string RoleName, string? UserId, string? Email);

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RoleName))
                return BadRequest(new { message = "RoleName is required" });

            if (string.IsNullOrWhiteSpace(request.UserId) && string.IsNullOrWhiteSpace(request.Email))
                return BadRequest(new { message = "UserId or Email is required" });

            var roleName = request.RoleName.Trim();

            if (!await _roleManager.RoleExistsAsync(roleName))
                return NotFound(new { message = "Role not found" });

            ApplicationUser? user = null;
            if (!string.IsNullOrWhiteSpace(request.UserId))
                user = await _userManager.FindByIdAsync(request.UserId.Trim());
            else if (!string.IsNullOrWhiteSpace(request.Email))
                user = await _userManager.FindByEmailAsync(request.Email.Trim());

            if (user == null)
                return NotFound(new { message = "User not found" });

            if (await _userManager.IsInRoleAsync(user, roleName))
                return Ok(new { message = "User already has role", roleName });

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Role assigned", roleName, userId = user.Id });
        }

        //delete role from user
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveRole([FromBody] AssignRoleRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RoleName))
                return BadRequest(new { message = "RoleName is required" });
            if (string.IsNullOrWhiteSpace(request.UserId) && string.IsNullOrWhiteSpace(request.Email))
                return BadRequest(new { message = "UserId or Email is required" });
            var roleName = request.RoleName.Trim();
            if (!await _roleManager.RoleExistsAsync(roleName))
                return NotFound(new { message = "Role not found" });
            ApplicationUser? user = null;
            if (!string.IsNullOrWhiteSpace(request.UserId))
                user = await _userManager.FindByIdAsync(request.UserId.Trim());
            else if (!string.IsNullOrWhiteSpace(request.Email))
                user = await _userManager.FindByEmailAsync(request.Email.Trim());
            if (user == null)
                return NotFound(new { message = "User not found" });
            if (!await _userManager.IsInRoleAsync(user, roleName))
                return Ok(new { message = "User does not have the role", roleName });
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(new { message = "Role removed", roleName, userId = user.Id });
        }
    }
}