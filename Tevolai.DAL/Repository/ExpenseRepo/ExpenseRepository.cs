using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tevolai.DAL.Context;
using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.ExpenseRepo
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly TevolaiDbContext db;

        public ExpenseRepository(TevolaiDbContext db)
        {
            this.db = db;
        }

        //add methods for expense
        public async Task AddExpenseAsync(Expense expense)
        {
            await db.Expenses.AddAsync(expense);
            await db.SaveChangesAsync();
        }

        //get expense by id
        public async Task<Expense?> GetExpenseByIdAsync(int id)
        {
            return await db.Expenses.FindAsync(id);
        }

        //update expense
        public async Task UpdateExpenseAsync(Expense expense)
        {
            var existingExpense = await GetExpenseByIdAsync(expense.Id);
            if (existingExpense != null)
            {
                existingExpense.Title = expense.Title;
                existingExpense.Amount = expense.Amount;
                existingExpense.Date = expense.Date;
                existingExpense.Category = expense.Category;
                existingExpense.Notes = expense.Notes;
            }
            await db.SaveChangesAsync();
        }

        //soft delete expense
        public async Task SoftDeleteExpenseAsync(int id)
        {
            var expense = await GetExpenseByIdAsync(id);
            if (expense != null)
            {
                expense.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        //get all expenses paginitaed
        public async Task<List<Expense>> GetAllExpensesAsync(int pageNumber, int pageSize)
        {
            return await db.Expenses
                .Where(e => !e.IsDeleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        //filter expenses by date range
        public async Task<List<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await db.Expenses
                .Where(e => !e.IsDeleted && e.Date >= startDate && e.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        }

        //filter expenses by category
        public async Task<List<Expense>> GetExpensesByCategoryAsync(string category)
        {
            return await db.Expenses
                .Where(e => !e.IsDeleted && e.Category == category)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<decimal> GetTotalExpensesAmountAsync()
        {
            return db.Expenses.SumAsync(e => e.Amount);
        }
    }
}