using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.DTOs.ExpenseDto;
using Tevolai.BL.Managers.Expenses;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service)
        {
            _service = service;
        }

        // Add New Expense
        [HttpPost]
        public async Task<IActionResult> AddExpense([FromBody] ExpenseDto dto)
        {
            try
            {
                await _service.AddExpenseAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Get Expense By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            try
            {
                var expense = await _service.GetExpenseByIdAsync(id);
                if (expense == null)
                    return NotFound(new { error = "Expense not found" });
                return Ok(expense);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Update Expense
        [HttpPut]
        public async Task<IActionResult> UpdateExpense([FromBody] ExpenseDto dto)
        {
            try
            {
                await _service.UpdateExpenseAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Delete Expense
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            try
            {
                await _service.SoftDeleteExpenseAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Get All Expenses
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses([FromQuery] int pageNumper = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var expenses = await _service.GetAllExpensesAsync(pageNumper, pageSize);
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Get Total Expenses Count
        [HttpGet("count")]
        public async Task<IActionResult> GetTotalExpensesCount()
        {
            try
            {
                var count = await _service.GetTotalExpensesAmountAsync();
                return Ok(new { totalAmount = count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}