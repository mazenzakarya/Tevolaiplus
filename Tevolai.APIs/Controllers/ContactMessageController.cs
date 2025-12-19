using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.DTOs.ContactMessageDto;
using Tevolai.BL.Managers.ContactMessages;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessageController : ControllerBase
    {
        private readonly IContactMessageService _service;

        public ContactMessageController(IContactMessageService service)
        {
            _service = service;
        }

        //add new contact message
        [HttpPost("AddContactMessage")]
        public async Task<IActionResult> AddContactMessage([FromBody] ContactMessageDto dto)
        {
            try
            {
                await _service.AddNewContactMessage(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        //get paged contact messages
        [HttpGet("GetPagedContactMessages")]
        public async Task<IActionResult> GetPagedContactMessages([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var pagedMessages = await _service.GetPagedAsync(page, pageSize);
                return Ok(pagedMessages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        //get contact message by id
        [HttpGet("GetContactMessageById/{id}")]
        public async Task<IActionResult> GetContactMessageById(int id)
        {
            try
            {
                var message = await _service.GetByIdAsync(id);
                if (message == null)
                    return NotFound(new { error = "Contact message not found" });
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        //delete contact message by id
        [HttpDelete("DeleteContactMessage/{id}")]
        public async Task<IActionResult> DeleteContactMessage(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        //mark contact message as read
        [HttpPut("MarkAsRead/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            try
            {
                await _service.MarkAsReadAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        //filter contact messages by read status
        [HttpGet("GetContactMessagesByReadStatus")]
        public async Task<IActionResult> GetContactMessagesByReadStatus([FromQuery] bool isRead)
        {
            try
            {
                var messages = await _service.GetByReadStatusAsync(isRead);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}