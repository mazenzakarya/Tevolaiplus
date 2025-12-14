using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tevolai.BL.DTOs.ExpenseDto;
using Tevolai.DAL.Repository.ExpenseRepo;

namespace Tevolai.BL.Managers.Expenses
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repo;

        public ExpenseService(IExpenseRepository repo)
        {
            _repo = repo;
        }

        //add new expense
        public async Task AddExpenseAsync(ExpenseDto expense)
        {
            var entity = new DAL.Entities.Expense
            {
                Title = expense.Title,
                Amount = expense.Amount,
                Category = expense.Category,
                Date = expense.Date,
                Notes = expense.Notes,
                IsDeleted = false
            };
            await _repo.AddExpenseAsync(entity);
        }

        //get expense by id
        public async Task<ExpenseDto?> GetExpenseByIdAsync(int id)
        {
            var entity = await _repo.GetExpenseByIdAsync(id);
            if (entity == null || entity.IsDeleted)
                return null;
            return new ExpenseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Amount = entity.Amount,
                Category = entity.Category,
                Date = entity.Date,
                Notes = entity.Notes,
                IsDeleted = entity.IsDeleted
            };
        }

        //update expense
        public async Task UpdateExpenseAsync(ExpenseDto expense)
        {
            var entity = await _repo.GetExpenseByIdAsync(expense.Id);
            if (entity == null || entity.IsDeleted)
                throw new Exception("Expense not found");
            entity.Title = expense.Title;
            entity.Amount = expense.Amount;
            entity.Category = expense.Category;
            entity.Date = expense.Date;
            entity.Notes = expense.Notes;
            await _repo.UpdateExpenseAsync(entity);
        }

        // soft delete expense
        public async Task SoftDeleteExpenseAsync(int id)
        {
            var entity = await _repo.GetExpenseByIdAsync(id);
            if (entity == null || entity.IsDeleted)
                throw new Exception("Expense not found");
            await _repo.SoftDeleteExpenseAsync(id);
        }

        //get all expenses
        public async Task<List<ExpenseDto>> GetAllExpensesAsync(int pageNumber, int pageSize)
        {
            var entities = await _repo.GetAllExpensesAsync(pageNumber, pageSize);
            return entities
                .Where(e => !e.IsDeleted)
                .Select(entity => new ExpenseDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Amount = entity.Amount,
                    Category = entity.Category,
                    Date = entity.Date,
                    Notes = entity.Notes,
                    IsDeleted = entity.IsDeleted
                })
                .ToList();
        }

        //get total expenses amount
        public async Task<decimal> GetTotalExpensesAmountAsync()
        {
            return await _repo.GetTotalExpensesAmountAsync();
        }
    }
}