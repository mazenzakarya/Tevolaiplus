using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.DTOs.RevenueDto;
using Tevolai.BL.Managers.Revenues;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly IRevenueService _service;

        public RevenueController(IRevenueService service)
        {
            _service = service;
        }

        // Get Total Revenues Amount
        [HttpGet("TotalRevenues")]
        public async Task<IActionResult> GetTotalRevenuesAmount()
        {
            try
            {
                var totalRevenues = await _service.GetTotalRevenuesAmountAsync();
                return Ok(totalRevenues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Add Revenue
        [HttpPost("AddRevenue")]
        public async Task<IActionResult> AddRevenue(RevenueDto dto)
        {
            await _service.AddRevenueAsync(dto);
            return Ok();
        }

        // Get Revenue by Id
        [HttpGet("GetRevenueById/{id}")]
        public async Task<IActionResult> GetRevenueById(int id)
        {
            var revenue = await _service.GetRevenueByIdAsync(id);
            if (revenue == null)
                return NotFound();
            return Ok(revenue);
        }

        // Edit Revenue
        [HttpPut("EditRevenue")]
        public async Task<IActionResult> EditRevenue(RevenueDto dto)
        {
            try
            {
                await _service.EditRevenueAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Delete Revenue
        [HttpDelete("DeleteRevenue/{id}")]
        public async Task<IActionResult> DeleteRevenue(int id)
        {
            try
            {
                await _service.DeleteRevenueAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}