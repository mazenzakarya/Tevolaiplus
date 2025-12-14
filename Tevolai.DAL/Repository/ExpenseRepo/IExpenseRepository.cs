using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.ExpenseRepo
{
    public interface IExpenseRepository
    {
        Task AddExpenseAsync(Expense expense);

        Task<List<Expense>> GetAllExpensesAsync(int pageNumber, int pageSize);

        Task<decimal> GetTotalExpensesAmountAsync();

        Task<Expense?> GetExpenseByIdAsync(int id);

        Task<List<Expense>> GetExpensesByCategoryAsync(string category);

        Task<List<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task SoftDeleteExpenseAsync(int id);

        Task UpdateExpenseAsync(Expense expense);
    }
}