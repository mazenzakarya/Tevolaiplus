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
            await _service.AddCustomerAsync(dto);
            return Ok();
        }

        // Get Customer By Id
        [HttpGet("GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        // Update Customer
        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto dto)
        {
            await _service.UpdateCustomerAsync(dto);
            return Ok();
        }

        // Delete Customer
        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _service.DeleteCustomerAsync(id);
            return Ok();
        }

        // Get All Customers
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _service.GetAllCustomersAsync();
            return Ok(customers);
        }

        // Search Customers
        [HttpGet("SearchCustomers")]
        public async Task<IActionResult> SearchCustomers([FromQuery] string query)
        {
            var customers = await _service.SearchCustomersAsync(query);
            return Ok(customers);
        }
    }
}