using Microsoft.EntityFrameworkCore;
using Tevolai.DAL.Context;
using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.ServicesOnDashBoardRepo
{
    public class ServicesOnDashBoardRepository : IServicesOnDashBoardRepository
    {
        private readonly TevolaiDbContext db;

        public ServicesOnDashBoardRepository(TevolaiDbContext db)
        {
            this.db = db;
        }

        public async Task AddNewServiceAsync(ServicesOnDashBoard service)
        {
            await db.ServicesOnDashBoards.AddAsync(service);
            await db.SaveChangesAsync();
        }

        public async Task<List<ServicesOnDashBoard>> GetAllServicesAsync()
        {
            return await db.ServicesOnDashBoards
                .ToListAsync();
        }

        public async Task<ServicesOnDashBoard?> GetServiceByIdAsync(int id)
        {
            return await db.ServicesOnDashBoards
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ServicesOnDashBoard> DeleteServices(int id)
        {
            var service = await GetServiceByIdAsync(id);
            if (service != null)
            {
                service.IsDeleted = true;
                await db.SaveChangesAsync();
            }
            return service!;
        }

        public async Task UpdateServiceAsync(ServicesOnDashBoard service)
        {
            var existingService = await GetServiceByIdAsync(service.Id);
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            else
            {
                existingService!.ServiceName = service.ServiceName;
                existingService.Domain = service.Domain;
                existingService.UserName = service.UserName;
                existingService.CMS = service.CMS;
                existingService.HostingServer = service.HostingServer;
                existingService.Status = service.Status;
                existingService.Notes = service.Notes;
            }
            await db.SaveChangesAsync();
        }

        // Get All for Customer
        public async Task<List<ServicesOnDashBoard>> GetAllForCustomerAsync(int customerId)
        {
            return await db.ServicesOnDashBoards
                .Where(s => s.CustomerId == customerId)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        // Pagination
        public async Task<List<ServicesOnDashBoard>> GetServicesWithPaginationAsync(int pageNumber, int pageSize)
        {
            return await db.ServicesOnDashBoards
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Search by Service Name
        public async Task<List<ServicesOnDashBoard>> SearchServicesByNameAsync(string serviceName)
        {
            return await db.ServicesOnDashBoards
                .Where(s => s.ServiceName.Contains(serviceName))
                .ToListAsync();
        }

        // Filter by Status
        public async Task<List<ServicesOnDashBoard>> FilterServicesByStatusAsync(WebsiteStatus status)
        {
            return await db.ServicesOnDashBoards
                .Where(s => s.Status == status)
                .ToListAsync();
        }

        // Sort by CreatedAt
        public async Task<List<ServicesOnDashBoard>> SortServicesByCreatedAtAsync(bool ascending = true)
        {
            if (ascending)
            {
                return await db.ServicesOnDashBoards
                    .OrderBy(s => s.CreatedAt)
                    .ToListAsync();
            }
            else
            {
                return await db.ServicesOnDashBoards
                    .OrderByDescending(s => s.CreatedAt)
                    .ToListAsync();
            }
        }

        // Count Total Services
        public async Task<int> CountTotalServicesAsync()
        {
            return await db.ServicesOnDashBoards.CountAsync();
        }

        //filter by CMS
        public async Task<List<ServicesOnDashBoard>> FilterServicesByCMSAsync(WebsiteCMS cms)
        {
            return await db.ServicesOnDashBoards
                .Where(s => s.CMS == cms)
                .ToListAsync();
        }
    }
}