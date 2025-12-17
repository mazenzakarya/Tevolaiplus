using Tevolai.BL.DTOs.ServicesOnMainDto;
using Tevolai.DAL.Entities;

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

        Task<List<ServicesOnMainDto>> GetServicesOnMainByLanguage(Language language);
    }
}