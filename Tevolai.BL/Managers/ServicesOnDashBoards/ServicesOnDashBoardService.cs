using Tevolai.BL.DTOs.ServicesOnDashBoardDto;
using Tevolai.DAL.Entities;
using Tevolai.DAL.Helpers;
using Tevolai.DAL.Repository.ServicesOnDashBoardRepo;

namespace Tevolai.BL.Managers.ServicesOnDashBoards
{
    public class ServicesOnDashBoardService : IServicesOnDashBoardService
    {
        private readonly IServicesOnDashBoardRepository _service;

        public ServicesOnDashBoardService(IServicesOnDashBoardRepository service)
        {
            _service = service;
        }

        //add new services
        public async Task AddServiceOnDashBoardAsync(ServiesOnDashBoardDto Dto)
        {
            var entity = new ServicesOnDashBoard
            {
                CustomerId = Dto.CustomerId,
                ServiceName = Dto.ServiceName,
                Domain = Dto.Domain,
                UserName = Dto.UserName,
                CMS = Dto.CMS,
                HostingServer = Dto.HostingServer,
                Status = Dto.Status,
                Notes = Dto.Notes,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            await _service.AddNewServiceAsync(entity);
        }

        //get all services
        public async Task<List<ServiesOnDashBoardDto>> GetServiceOnDashBoardsAsync()
        {
            var services = await _service.GetAllServicesAsync();
            var dtoList = services.Select(s => new ServiesOnDashBoardDto
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                ServiceName = s.ServiceName,
                Domain = s.Domain,
                UserName = s.UserName,
                CMS = s.CMS,
                HostingServer = s.HostingServer,
                Status = s.Status,
                Notes = s.Notes
            }).ToList();
            return dtoList;
        }

        //get service by id
        public async Task<ServiesOnDashBoardDto?> GetServiceOnDashBoardByIdAsync(int id)
        {
            var service = await _service.GetServiceByIdAsync(id);
            if (service == null)
                return null;
            var dto = new ServiesOnDashBoardDto
            {
                Id = service.Id,
                CustomerId = service.CustomerId,
                ServiceName = service.ServiceName,
                Domain = service.Domain,
                UserName = service.UserName,
                CMS = service.CMS,
                HostingServer = service.HostingServer,
                Status = service.Status,
                Notes = service.Notes
            };
            return dto;
        }

        //delete service
        public async Task<ServiesOnDashBoardDto?> DeleteServiceOnDashBoardAsync(int id)
        {
            var service = await _service.DeleteServices(id);
            if (service == null)
                return null;
            var dto = new ServiesOnDashBoardDto
            {
                Id = service.Id,
                CustomerId = service.CustomerId,
                ServiceName = service.ServiceName,
                Domain = service.Domain,
                UserName = service.UserName,
                CMS = service.CMS,
                HostingServer = service.HostingServer,
                Status = service.Status,
                Notes = service.Notes
            };
            return dto;
        }

        // get items paginated
        public async Task<PagedList<ServiesOnDashBoardDto>> GetPaginatedServicesOnDashBoardAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _service.CountTotalServicesAsync();

            var services = await _service.GetServicesWithPaginationAsync(pageNumber, pageSize);

            var dtoList = services.Select(s => new ServiesOnDashBoardDto
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                ServiceName = s.ServiceName,
                Domain = s.Domain,
                UserName = s.UserName,
                CMS = s.CMS,
                HostingServer = s.HostingServer,
                Status = s.Status,
                Notes = s.Notes
            }).ToList();

            return new PagedList<ServiesOnDashBoardDto>(dtoList, totalCount, pageNumber, pageSize);
        }

        //update service
        public async Task UpdateServiceOnDashBoardAsync(ServiesOnDashBoardDto Dto)
        {
            var entity = new ServicesOnDashBoard
            {
                Id = Dto.Id,
                CustomerId = Dto.CustomerId,
                ServiceName = Dto.ServiceName,
                Domain = Dto.Domain,
                UserName = Dto.UserName,
                CMS = Dto.CMS,
                HostingServer = Dto.HostingServer,
                Status = Dto.Status,
                Notes = Dto.Notes
            };
            await _service.UpdateServiceAsync(entity);
        }

        // Search by Service Name
        public async Task<List<ServiesOnDashBoardDto>> SearchServicesByNameAsync(string serviceName)
        {
            var services = await _service.SearchServicesByNameAsync(serviceName);
            var dtoList = services.Select(s => new ServiesOnDashBoardDto
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                ServiceName = s.ServiceName,
                Domain = s.Domain,
                UserName = s.UserName,
                CMS = s.CMS,
                HostingServer = s.HostingServer,
                Status = s.Status,
                Notes = s.Notes
            }).ToList();
            return dtoList;
        }

        // Filter by Status
        public async Task<List<ServiesOnDashBoardDto>> FilterServicesByStatusAsync(WebsiteStatus status)
        {
            var services = await _service.FilterServicesByStatusAsync(status);
            var dtoList = services.Select(s => new ServiesOnDashBoardDto
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                ServiceName = s.ServiceName,
                Domain = s.Domain,
                UserName = s.UserName,
                CMS = s.CMS,
                HostingServer = s.HostingServer,
                Status = s.Status,
                Notes = s.Notes
            }).ToList();
            return dtoList;
        }

        // Sort by CreatedAt
        public async Task<List<ServiesOnDashBoardDto>> SortServicesByCreatedAtAsync(bool ascending)
        {
            var services = await _service.SortServicesByCreatedAtAsync(ascending);
            var dtoList = services.Select(s => new ServiesOnDashBoardDto
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                ServiceName = s.ServiceName,
                Domain = s.Domain,
                UserName = s.UserName,
                CMS = s.CMS,
                HostingServer = s.HostingServer,
                Status = s.Status,
                Notes = s.Notes
            }).ToList();
            return dtoList;
        }

        //filter by CMS
        public async Task<List<ServiesOnDashBoardDto>> FilterServicesByCMSAsync(WebsiteCMS cms)
        {
            var services = await _service.FilterServicesByCMSAsync(cms);
            var dtoList = services.Select(s => new ServiesOnDashBoardDto
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                ServiceName = s.ServiceName,
                Domain = s.Domain,
                UserName = s.UserName,
                CMS = s.CMS,
                HostingServer = s.HostingServer,
                Status = s.Status,
                Notes = s.Notes
            }).ToList();
            return dtoList;
        }

        // Count Total Services
        public async Task<int> CountTotalServicesAsync()
        {
            return await _service.CountTotalServicesAsync();
        }
    }
}