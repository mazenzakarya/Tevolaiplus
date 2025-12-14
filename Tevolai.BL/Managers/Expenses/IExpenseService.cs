using Tevolai.BL.DTOs.ExpenseDto;

namespace Tevolai.BL.Managers.Expenses
{
    public interface IExpenseService
    {
        Task AddExpenseAsync(ExpenseDto expense);
        Task<List<ExpenseDto>> GetAllExpensesAsync(int pageNumber, int pageSize);
        Task<ExpenseDto?> GetExpenseByIdAsync(int id);
        Task<decimal> GetTotalExpensesAmountAsync();
        Task SoftDeleteExpenseAsync(int id);
        Task UpdateExpenseAsync(ExpenseDto expense);
    }
}