using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.RevenueRepo
{
    public interface IRevenueRepository
    {
        Task AddNewRevenu(Revenue revenue);
        Task DeleteRevenueAsync(int id);
        Task EditRevenueAsync(Revenue revenue);
        Task<List<Revenue>> GetAllRevenuesAsync(int pageNumber, int pageSize);
        Task<Revenue?> GetRevenueByIdAsync(int id);
        Task<List<Revenue>> GetRevenuesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalRevenuesAmountAsync();
    }
}