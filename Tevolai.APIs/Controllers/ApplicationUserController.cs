using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.DTOs.ApplicationUserDto;
using Tevolai.BL.DTOs.Auth;
using Tevolai.BL.Managers.ApplicationUsers;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserService _service;

        public ApplicationUserController(IApplicationUserService service)
        {
            _service = service;
        }

        // GET: api/ApplicationUser/Count
        [HttpGet("Count")]
        public async Task<IActionResult> GetUsersCount()
        {
            var count = await _service.GetUsersCountAsync();
            Console.WriteLine("tst");
            return Ok(new { Count = count });
        }

        // GET: api/ApplicationUser
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        // GET: api/ApplicationUser/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(user);
        }

        // GET: api/ApplicationUser/ByEmail?email=...
        [HttpGet("ByEmail")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var user = await _service.GetByEmailAsync(email);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(user);
        }

        // POST: api/ApplicationUser
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUserDto dto)
        {
            var result = await _service.CreateAsync(dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "User created successfully" });
        }

        // PUT: api/ApplicationUser/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] dynamic model)
        {
            // نموذج anonymous: { email, userName, fullName }
            string email = model.email;
            string userName = model.userName;
            string fullName = model.fullName;

            var result = await _service.UpdateAsync(id, email, userName, fullName);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "User updated successfully" });
        }

        // PUT: api/ApplicationUser/ResetPassword/{id}
        [HttpPut("ResetPassword/{id}")]
        public async Task<IActionResult> ResetPassword(string id, [FromBody] ResetPasswordDto model)
        {
            var result = await _service.ResetPasswordAsync(id, model.NewPassword);
            if (!result.Succeeded)
                return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

            return Ok(new { Message = "Password reset successfully" });
        }

        // DELETE: api/ApplicationUser/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(string id)
        {
            var result = await _service.SoftDeleteAsync(id);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "User soft deleted successfully" });
        }
    }
}