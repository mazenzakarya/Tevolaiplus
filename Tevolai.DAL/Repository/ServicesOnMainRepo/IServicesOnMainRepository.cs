using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.ServicesOnMainRepo
{
    public interface IServicesOnMainRepository
    {
        Task AddServiceOnMainAsync(ServicesOnMain service);
        Task<List<ServicesOnMain>> GetAllActiveServicesOnMainAsync();
        Task<ServicesOnMain?> GetServiceOnMainByIdAsync(int id);
        Task<List<ServicesOnMain>> GetServicesOnMainByCategoryAsync(string category);
        Task SoftDeleteServiceOnMainAsync(int id);
        Task UpdateServiceOnMainAsync(ServicesOnMain service);
    }
}