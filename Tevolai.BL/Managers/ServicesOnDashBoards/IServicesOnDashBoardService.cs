using Tevolai.BL.DTOs.ServicesOnDashBoardDto;
using Tevolai.DAL.Entities;
using Tevolai.DAL.Helpers;

namespace Tevolai.BL.Managers.ServicesOnDashBoards
{
    public interface IServicesOnDashBoardService
    {
        Task AddServiceOnDashBoardAsync(ServiesOnDashBoardDto Dto);

        Task<ServiesOnDashBoardDto?> DeleteServiceOnDashBoardAsync(int id);

        Task<List<ServiesOnDashBoardDto>> FilterServicesByCMSAsync(WebsiteCMS cms);

        Task<List<ServiesOnDashBoardDto>> FilterServicesByStatusAsync(WebsiteStatus status);

        Task<PagedList<ServiesOnDashBoardDto>> GetPaginatedServicesOnDashBoardAsync(int pageNumber, int pageSize);

        Task<ServiesOnDashBoardDto?> GetServiceOnDashBoardByIdAsync(int id);

        Task<List<ServiesOnDashBoardDto>> GetServiceOnDashBoardsAsync();

        Task<List<ServiesOnDashBoardDto>> SearchServicesByNameAsync(string serviceName);

        Task<List<ServiesOnDashBoardDto>> SortServicesByCreatedAtAsync(bool ascending);

        Task UpdateServiceOnDashBoardAsync(ServiesOnDashBoardDto Dto);
    }
}