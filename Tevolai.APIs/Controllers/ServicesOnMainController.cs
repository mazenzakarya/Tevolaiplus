using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.DTOs.ServicesOnMainDto;
using Tevolai.BL.Managers.ServicesOnMains;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesOnMainController : ControllerBase
    {
        private readonly IServicesOnMainService _service;

        public ServicesOnMainController(IServicesOnMainService service)
        {
            _service = service;
        }

        // get all services on main
        [HttpGet]
        public async Task<IActionResult> GetAllServicesOnMain()
        {
            try
            {
                var services = await _service.GetAllActiveServicesOnMainAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOnMainById(int id)
        {
            try
            {
                var service = await _service.GetServiceOnMainByIdAsync(id);
                if (service == null)
                    return NotFound();
                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddServiceOnMain([FromBody] ServicesOnMainDto service)
        {
            try
            {
                await _service.AddServiceOnMainAsync(service);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateServiceOnMain([FromBody] ServicesOnMainDto service)
        {
            try
            {
                await _service.UpdateServiceOnMainAsync(service);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceOnMain(int id)
        {
            try
            {
                await _service.DeleteServiceOnMainAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}