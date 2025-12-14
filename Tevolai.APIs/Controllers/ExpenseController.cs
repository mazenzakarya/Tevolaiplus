using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tevolai.BL.DTOs.ExpenseDto;
using Tevolai.BL.Managers.Expenses;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            await _service.AddExpenseAsync(dto);
            return Ok();
        }

        // Get Expense By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var expense = await _service.GetExpenseByIdAsync(id);
            if (expense == null)
                return NotFound();
            return Ok(expense);
        }

        // Update Expense
        [HttpPut]
        public async Task<IActionResult> UpdateExpense([FromBody] ExpenseDto dto)
        {
            await _service.UpdateExpenseAsync(dto);
            return Ok();
        }

        // Delete Expense
        [HttpDelete]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            await _service.SoftDeleteExpenseAsync(id);
            return Ok();
        }

        // Get All Expenses
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses(int pageNumper, int pageSize)
        {
            var expenses = await _service.GetAllExpensesAsync(pageNumper, pageSize);
            return Ok(expenses);
        }

        // Get Total Expenses Count
        [HttpGet("count")]
        public async Task<IActionResult> GetTotalExpensesCount()
        {
            var count = await _service.GetTotalExpensesAmountAsync();
            return Ok(count);
        }
    }
}