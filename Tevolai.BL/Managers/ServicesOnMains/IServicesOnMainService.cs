using Tevolai.BL.DTOs.ServicesOnMainDto;

namespace Tevolai.BL.Managers.ServicesOnMains
{
    public interface IServicesOnMainService
    {
        Task AddServiceOnMainAsync(ServicesOnMainDto service);
        Task DeleteServiceOnMainAsync(int id);
        Task<List<ServicesOnMainDto>> GetAllActiveServicesOnMainAsync();
        Task<ServicesOnMainDto?> GetServiceOnMainByIdAsync(int id);
        Task<List<ServicesOnMainDto>> GetServicesOnMainByCategoryAsync(string category);
        Task UpdateServiceOnMainAsync(ServicesOnMainDto service);
    }
}