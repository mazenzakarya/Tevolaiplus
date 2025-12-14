using Tevolai.BL.DTOs.RevenueDto;

namespace Tevolai.BL.Managers.Revenues
{
    public interface IRevenueService
    {
        Task AddRevenueAsync(RevenueDto revenue);
        Task DeleteRevenueAsync(int id);
        Task EditRevenueAsync(RevenueDto revenue);
        Task<List<RevenueDto>> GetAllRevenuesAsync(int pageNumber, int pageSize);
        Task<RevenueDto?> GetRevenueByIdAsync(int id);
        Task<List<RevenueDto>> GetRevenuesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalRevenuesAmountAsync();
        Task<decimal> GetTotalRevenuesAmountByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}