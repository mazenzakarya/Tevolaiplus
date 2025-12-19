using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.DTOs.ServicesOnDashBoardDto;
using Tevolai.BL.DTOs.ServicesOnMainDto;
using Tevolai.BL.Managers.ServicesOnDashBoards;
using Tevolai.DAL.Entities;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServicesOnDashBoardController : ControllerBase
    {
        private readonly IServicesOnDashBoardService _service;

        public ServicesOnDashBoardController(IServicesOnDashBoardService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        // add new service on dashboard
        [HttpPost("AddServiceOnDashBoard")]
        public async Task<IActionResult> AddServiceOnDashBoard(ServiesOnDashBoardDto dto)
        {
            try
            {
                await _service.AddServiceOnDashBoardAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        // get all services on dashboard
        [HttpGet("GetAllServicesOnDashBoard")]
        public async Task<IActionResult> GetAllServicesOnDashBoard()
        {
            try
            {
                var services = await _service.GetServiceOnDashBoardsAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        // get service on dashboard by id
        [HttpGet("GetServiceOnDashBoardById/{id}")]
        public async Task<IActionResult> GetServiceOnDashBoardById(int id)
        {
            try
            {
                var service = await _service.GetServiceOnDashBoardByIdAsync(id);
                if (service == null)
                    return NotFound();
                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        // delete service on dashboard by id
        [HttpDelete("DeleteServiceOnDashBoard/{id}")]
        public async Task<IActionResult> DeleteServiceOnDashBoard(int id)
        {
            try
            {
                await _service.DeleteServiceOnDashBoardAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // update service on dashboard
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateServiceOnDashBoard")]
        public async Task<IActionResult> UpdateServiceOnDashBoard(ServiesOnDashBoardDto Dto)
        {
            try
            {
                await _service.UpdateServiceOnDashBoardAsync(Dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // search services by name
        [Authorize(Roles = "Admin")]
        [HttpGet("SearchServicesByName/{serviceName}")]
        public async Task<IActionResult> SearchServicesByName(string serviceName)
        {
            try
            {
                var services = await _service.SearchServicesByNameAsync(serviceName);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // filter services by CMS
        [Authorize(Roles = "Admin")]
        [HttpGet("FilterServicesByCMS/{cms}")]
        public IActionResult SearchServicesById(WebsiteCMS cms)
        {
            try
            {
                var services = _service.FilterServicesByCMSAsync(cms);

                if (services is not null)
                    return Ok(services);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
            //
        }

        // filter services by status
        [Authorize(Roles = "Admin")]
        [HttpGet("FilterServicesByStatus/{status}")]
        public async Task<IActionResult> FilterServicesByStatus(WebsiteStatus status)
        {
            try
            {
                var services = await _service.FilterServicesByStatusAsync(status);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // sort services by created at
        [Authorize(Roles = "Admin")]
        [HttpGet("SortServicesByCreatedAt/{ascending}")]
        public async Task<IActionResult> SortServicesByCreatedAt(bool ascending)
        {
            try
            {
                var services = await _service.SortServicesByCreatedAtAsync(ascending);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // get services on dashboard by application user id
        [HttpGet("GetServicesByUserId/{userId}")]
        public async Task<IActionResult> GetServicesByUserId(string userId)
        {
            try
            {
                var services = await _service.FilterServicesByCustomerAsync(userId);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}