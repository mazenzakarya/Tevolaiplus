using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.DTOs.CustomerDto;
using Tevolai.BL.Managers.Customers;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        // Add New Customer
        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDto dto)
        {
            try
            {
                await _service.AddCustomerAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Get Customer By Id
        [HttpGet("GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var customer = await _service.GetCustomerByIdAsync(id);
                if (customer == null)
                    return NotFound(new { error = "Customer not found" });
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Update Customer
        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto dto)
        {
            try
            {
                await _service.UpdateCustomerAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Delete Customer
        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _service.DeleteCustomerAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Get All Customers
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _service.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Search Customers
        [HttpGet("SearchCustomers")]
        public async Task<IActionResult> SearchCustomers([FromQuery] string query)
        {
            try
            {
                var customers = await _service.SearchCustomersAsync(query);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}