using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.Managers.EmailCredentials;
using Tevolai.DAL.Entities;

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
    string? HostingPlatform,
    string? Notes
);

        [HttpPost("AddCredentialAsync")]
        public async Task<IActionResult> AddCredentialAsync([FromBody] AddEmailCredentialRequest request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            try
            {
                await _service.AddCredentialAsync(
                    request.EmailAddress,
                    request.Password,
                    request.UserName,
                    "API"
                );

                return Ok(new { message = "Credential added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ===================== Get Credential =====================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCredentialAsync(int id)
        {
            try
            {
                var credential = await _service.GetCredentialAsync(id, "API");
                if (credential == null)
                    return NotFound("Credential not found");

                // يمكنك هنا إرسال الباسورد مفكوك (اختياري)
                return Ok(credential);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ===================== Update Credential =====================
        [HttpPut("UpdateCredentialAsync")]
        public async Task<IActionResult> UpdateCredentialAsync([FromBody] EmailCredential emailCredential)
        {
            if (emailCredential == null || emailCredential.Id == 0)
                return BadRequest("Invalid data");

            try
            {
                var existingCredential = await _service.GetCredentialAsync(emailCredential.Id, "API");
                if (existingCredential == null)
                    return NotFound("Credential not found");

                existingCredential.UserName = emailCredential.UserName;

                // إذا تم إرسال باسورد جديد، يحدث التشفير
                await _service.UpdateCredentialAsync(existingCredential, emailCredential.EncryptedPassword, "API");

                return Ok("Credential updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ===================== Soft Delete Credential =====================
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteCredentialAsync(int id)
        {
            try
            {
                await _service.SoftDeleteCredentialAsync(id, "API");
                return Ok("Credential soft deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}