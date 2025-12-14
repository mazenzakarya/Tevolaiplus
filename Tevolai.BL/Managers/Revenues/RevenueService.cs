using Tevolai.BL.DTOs.RevenueDto;
using Tevolai.DAL.Entities;
using Tevolai.DAL.Repository.RevenueRepo;

namespace Tevolai.BL.Managers.Revenues
{
    public class RevenueService : IRevenueService
    {
        private readonly IRevenueRepository _repo;

        public RevenueService(IRevenueRepository repo)
        {
            _repo = repo;
        }

        // Add methods for revenue management
        public async Task AddRevenueAsync(RevenueDto revenue)
        {
            var entity = new Revenue
            {
                CustomerId = revenue.CustomerId,
                ServiceId = revenue.ServiceId,
                Amount = revenue.Amount,
                PaymentDate = revenue.PaymentDate,
                PaymentMethod = revenue.PaymentMethod,
                Notes = revenue.Notes
            };
            await _repo.AddNewRevenu(entity);
        }

        // Get revenue by id
        public async Task<RevenueDto?> GetRevenueByIdAsync(int id)
        {
            var entity = await _repo.GetRevenueByIdAsync(id);
            if (entity == null)
                return null;
            return new RevenueDto
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                ServiceId = entity.ServiceId,
                Amount = entity.Amount,
                PaymentDate = entity.PaymentDate,
                PaymentMethod = entity.PaymentMethod,
                Notes = entity.Notes
            };
        }

        // Edit revenue
        public async Task EditRevenueAsync(RevenueDto revenue)
        {
            var entity = await _repo.GetRevenueByIdAsync(revenue.Id);
            if (entity == null)
                throw new Exception("Revenue not found");
            entity.CustomerId = revenue.CustomerId;
            entity.ServiceId = revenue.ServiceId;
            entity.Amount = revenue.Amount;
            entity.PaymentDate = revenue.PaymentDate;
            entity.PaymentMethod = revenue.PaymentMethod;
            entity.Notes = revenue.Notes;
            await _repo.EditRevenueAsync(entity);
        }

        // Delete revenue
        public async Task DeleteRevenueAsync(int id)
        {
            if (await _repo.GetRevenueByIdAsync(id) == null)
                throw new Exception("Revenue not found");
            await _repo.DeleteRevenueAsync(id);
        }

        // Get all revenues paginated
        public async Task<List<RevenueDto>> GetAllRevenuesAsync(int pageNumber, int pageSize)
        {
            var entities = await _repo.GetAllRevenuesAsync(pageNumber, pageSize);
            return entities.Select(entity => new RevenueDto
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                ServiceId = entity.ServiceId,
                Amount = entity.Amount,
                PaymentDate = entity.PaymentDate,
                PaymentMethod = entity.PaymentMethod,
                Notes = entity.Notes
            }).ToList();
        }

        // Get revenues by date range
        public async Task<List<RevenueDto>> GetRevenuesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var entities = await _repo.GetRevenuesByDateRangeAsync(startDate, endDate);
            return entities.Select(entity => new RevenueDto
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                ServiceId = entity.ServiceId,
                Amount = entity.Amount,
                PaymentDate = entity.PaymentDate,
                PaymentMethod = entity.PaymentMethod,
                Notes = entity.Notes
            }).ToList();
        }

        // Get total revenues amount
        public async Task<decimal> GetTotalRevenuesAmountAsync()
        {
            return await _repo.GetTotalRevenuesAmountAsync();
        }

        // Additional methods for revenue management can be added here
        public async Task<decimal> GetTotalRevenuesAmountByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var revenues = await _repo.GetRevenuesByDateRangeAsync(startDate, endDate);
            return revenues.Sum(r => r.Amount);
        }
    }
}