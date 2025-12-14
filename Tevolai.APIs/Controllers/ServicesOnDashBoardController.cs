using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.Managers.ServicesOnDashBoards;
using Tevolai.BL.DTOs.ServicesOnDashBoardDto;
using Tevolai.BL.DTOs.ServicesOnMainDto;
using Tevolai.DAL.Entities;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesOnDashBoardController : ControllerBase
    {
        private readonly IServicesOnDashBoardService _service;

        public ServicesOnDashBoardController(IServicesOnDashBoardService service)
        {
            _service = service;
        }

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
    }
}