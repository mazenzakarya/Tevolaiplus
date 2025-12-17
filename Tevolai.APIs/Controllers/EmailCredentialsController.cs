using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.Managers.EmailCredentials;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailCredentialsController : ControllerBase
    {
        private readonly IEmailCredentialService _service;

        public EmailCredentialsController(IEmailCredentialService service)
        {
            _service = service;
        }

        // ===================== Add Credential =====================
        public record AddEmailCredentialRequest(
            string EmailAddress,
            string Password,
            string UserName,
            int? CustomerId,
            string? HostingPlatform,
            string? Notes
        );

        [HttpPost("AddCredential")]
        public async Task<IActionResult> AddCredentialAsync([FromBody] AddEmailCredentialRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.EmailAddress) || 
                string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.UserName))
                return BadRequest(new { error = "EmailAddress, Password, and UserName are required" });

            try
            {
                await _service.AddCredentialAsync(
                    request.EmailAddress,
                    request.Password,
                    request.UserName,
                    request.CustomerId,
                    request.HostingPlatform,
                    request.Notes,
                    "API"
                );

                return Ok(new { message = "Credential added successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ===================== Get Credential (without password) =====================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCredentialAsync(int id)
        {
            try
            {
                var credential = await _service.GetCredentialAsync(id, includeDecryptedPassword: false, by: "API");
                if (credential == null)
                    return NotFound(new { error = "Credential not found" });

                return Ok(credential);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ===================== Get Credential with Decrypted Password =====================
        [HttpGet("{id}/with-password")]
        public async Task<IActionResult> GetCredentialWithPasswordAsync(int id)
        {
            try
            {
                var credential = await _service.GetCredentialAsync(id, includeDecryptedPassword: true, by: "API");
                if (credential == null)
                    return NotFound(new { error = "Credential not found" });

                return Ok(credential);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ===================== Get All Credentials =====================
        [HttpGet]
        public async Task<IActionResult> GetAllCredentialsAsync([FromQuery] bool includeDeleted = false)
        {
            try
            {
                var credentials = await _service.GetAllCredentialsAsync(includeDeleted);
                return Ok(credentials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ===================== Update Credential =====================
        public record UpdateEmailCredentialRequest(
            int Id,
            string? UserName,
            string? Password,
            int? CustomerId,
            string? HostingPlatform,
            string? Notes
        );

        [HttpPut("UpdateCredential")]
        public async Task<IActionResult> UpdateCredentialAsync([FromBody] UpdateEmailCredentialRequest request)
        {
            if (request == null || request.Id == 0)
                return BadRequest(new { error = "Invalid data. Id is required." });

            try
            {
                await _service.UpdateCredentialAsync(
                    request.Id,
                    request.UserName,
                    request.Password,
                    request.CustomerId,
                    request.HostingPlatform,
                    request.Notes,
                    "API"
                );

                return Ok(new { message = "Credential updated successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ===================== Soft Delete Credential =====================
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteCredentialAsync(int id)
        {
            try
            {
                await _service.SoftDeleteCredentialAsync(id, "API");
                return Ok(new { message = "Credential soft deleted successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}