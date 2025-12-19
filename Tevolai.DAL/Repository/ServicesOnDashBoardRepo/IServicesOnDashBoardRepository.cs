using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.ServicesOnDashBoardRepo
{
    public interface IServicesOnDashBoardRepository
    {
        Task AddNewServiceAsync(ServicesOnDashBoard service);

        Task<int> CountTotalServicesAsync();

        Task<ServicesOnDashBoard> DeleteServices(int id);

        Task<List<ServicesOnDashBoard>> FilterServicesByCMSAsync(WebsiteCMS cms);

        Task<List<ServicesOnDashBoard>> FilterServicesByStatusAsync(WebsiteStatus status);

        Task<List<ServicesOnDashBoard>> GetAllForCustomerAsync(int customerId);

        Task<List<ServicesOnDashBoard>> GetAllServicesAsync();

        Task<ServicesOnDashBoard?> GetServiceByIdAsync(int id);

        Task<List<ServicesOnDashBoard>> GetServicesWithPaginationAsync(int pageNumber, int pageSize);

        Task<List<ServicesOnDashBoard>> SearchServicesByNameAsync(string serviceName);

        Task<List<ServicesOnDashBoard>> SortServicesByCreatedAtAsync(bool ascending = true);

        Task UpdateServiceAsync(ServicesOnDashBoard service);

        IQueryable<ServicesOnDashBoard> FilterServicesByCustomerAsync(string userId);
    }
}