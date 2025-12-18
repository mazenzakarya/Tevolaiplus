using Microsoft.EntityFrameworkCore;
using Tevolai.DAL.Context;
using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.RevenueRepo
{
    public class RevenueRepository : IRevenueRepository
    {
        private readonly TevolaiDbContext db;

        public RevenueRepository(TevolaiDbContext db)
        {
            this.db = db;
        }

        // GET ALL REVENUES WITH PAGINATION
        public async Task<List<Revenue>> GetAllRevenuesAsync(int pageNumber, int pageSize)
        {
            return await db.Revenues
                     .OrderBy(r => r.PaymentDate)
                     .Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
                     .AsNoTracking()
                     .ToListAsync();
        }

        // GET TOTAL COUNT OF REVENUES
        public async Task<decimal> GetTotalRevenuesAmountAsync()
        {
            return await db.Revenues.AsNoTracking().SumAsync(r => r.Amount) - await db.Expenses.AsNoTracking().SumAsync(e => e.Amount);
        }

        // ADD NEW REVENUE
        public async Task AddNewRevenu(Revenue revenue)
        {
            await db.Revenues.AddAsync(revenue);
            await db.SaveChangesAsync();
        }

        //filter revenues by date range
        public async Task<List<Revenue>> GetRevenuesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await db.Revenues
                           .Where(r => r.PaymentDate >= startDate && r.PaymentDate <= endDate)
                           .AsNoTracking()
                           .ToListAsync();
        }

        // GET REVENUE BY ID
        public async Task<Revenue?> GetRevenueByIdAsync(int id)
        {
            return await db.Revenues.FindAsync(id);
        }

        //edit revenue
        public async Task EditRevenueAsync(Revenue revenue)
        {
            var existingRevenue = await GetRevenueByIdAsync(revenue.Id);
            if (existingRevenue == null)
            {
                throw new Exception("Revenue not found");
            }
            else
            {
                existingRevenue.CustomerId = revenue.CustomerId;
                existingRevenue.ServiceId = revenue.ServiceId;
                existingRevenue.Amount = revenue.Amount;
                existingRevenue.PaymentDate = revenue.PaymentDate;
                existingRevenue.PaymentMethod = revenue.PaymentMethod;
                existingRevenue.Notes = revenue.Notes;
            }
            await db.SaveChangesAsync();
        }

        //delete revenue
        public async Task DeleteRevenueAsync(int id)
        {
            var revenue = await GetRevenueByIdAsync(id);
            if (revenue == null)
            {
                throw new Exception("Item not found");
            }
            else
            {
                revenue.IsDeleted = true;
            }
        }
    }
}