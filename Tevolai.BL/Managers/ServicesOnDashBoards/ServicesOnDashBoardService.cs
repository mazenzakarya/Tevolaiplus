using Tevolai.BL.DTOs.ApplicationUserDto;
using Tevolai.BL.DTOs.ServicesOnDashBoardDto;
using Tevolai.BL.DTOs.ServicesOnMainDto;
using Tevolai.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Tevolai.DAL.Helpers;
using Tevolai.DAL.Repository.ServicesOnDashBoardRepo;

namespace Tevolai.BL.Managers.ServicesOnDashBoards
{
    public class ServicesOnDashBoardService : IServicesOnDashBoardService
    {
        private readonly IServicesOnDashBoardRepository _repo;

        public ServicesOnDashBoardService(IServicesOnDashBoardRepository service)
        {
            _repo = service;
        }

        //add new services
        public async Task AddServiceOnDashBoardAsync(ServiesOnDashBoardDto Dto)
        {
            var entity = new ServicesOnDashBoard
            {
                ServiceName = Dto.ServiceName,
                Domain = Dto.Domain,
                UserName = Dto.UserName,
                CMS = Dto.CMS,
                CustomerId = Dto.CustomerId,
                HostingServer = Dto.HostingServer,
                ApplicationUserId = Dto.ApplicationUserId,
                Status = Dto.Status,
                Notes = Dto.Notes,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            await _repo.AddNewServiceAsync(entity);
        }

        //get all services
        public async Task<List<ServiesOnDashBoardDto>> GetServiceOnDashBoardsAsync()
        {
            var services = await _repo.GetAllServicesAsync();
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
                CreatedAt = DateTime.UtcNow,
                Notes = s.Notes
            }).ToList();
            return dtoList;
        }

        //get service by id
        public async Task<ServiesOnDashBoardDto?> GetServiceOnDashBoardByIdAsync(int id)
        {
            var service = await _repo.GetServiceByIdAsync(id);
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
            var service = await _repo.DeleteServices(id);
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
            var totalCount = await _repo.CountTotalServicesAsync();

            var services = await _repo.GetServicesWithPaginationAsync(pageNumber, pageSize);

            var dtoList = services.Select(s => new ServiesOnDashBoardDto
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                ServiceName = s.ServiceName,
                Domain = s.Domain,
                UserName = s.UserName,
                CMS = s.CMS,
                CreatedAt = DateTime.UtcNow,
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
            await _repo.UpdateServiceAsync(entity);
        }

        // Search by Service Name
        public async Task<List<ServiesOnDashBoardDto>> SearchServicesByNameAsync(string serviceName)
        {
            var services = await _repo.SearchServicesByNameAsync(serviceName);
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
            var services = await _repo.FilterServicesByStatusAsync(status);
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
            var services = await _repo.SortServicesByCreatedAtAsync(ascending);
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
            var services = await _repo.FilterServicesByCMSAsync(cms);
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
            return await _repo.CountTotalServicesAsync();
        }

        // filter services on main by application user
        public async Task<List<ServiesOnDashBoardDto>> FilterServicesByCustomerAsync(string userId)
        {
            var entity = _repo.FilterServicesByCustomerAsync(userId);
            var dtoList = entity.Select(s => new ServiesOnDashBoardDto
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
            }).ToListAsync();
            return await dtoList;
        }
    }
}