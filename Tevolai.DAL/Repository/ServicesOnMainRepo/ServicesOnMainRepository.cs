using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tevolai.DAL.Context;
using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.ServicesOnMainRepo
{
    public class ServicesOnMainRepository : IServicesOnMainRepository
    {
        private readonly TevolaiDbContext db;

        public ServicesOnMainRepository(TevolaiDbContext db)
        {
            this.db = db;
        }

        //add new service on main
        public async Task AddServiceOnMainAsync(ServicesOnMain service)
        {
            await db.ServicesOnMains.AddAsync(service);
            await db.SaveChangesAsync();
        }

        //get service on main by id
        public async Task<ServicesOnMain?> GetServiceOnMainByIdAsync(int id)
        {
            return await db.ServicesOnMains.FindAsync(id);
        }

        //update service on main
        public async Task UpdateServiceOnMainAsync(ServicesOnMain service)
        {
            var existingService = await GetServiceOnMainByIdAsync(service.Id);
            if (existingService != null)
            {
                existingService.Title = service.Title;
                existingService.Description = service.Description;
                existingService.ImageUrl = service.ImageUrl;
                existingService.Price = service.Price;
                existingService.Category = service.Category;
                existingService.IsActive = service.IsActive;
                existingService.Language = service.Language;
            }
            await db.SaveChangesAsync();
        }

        //soft delete service on main
        public async Task SoftDeleteServiceOnMainAsync(int id)
        {
            var service = await GetServiceOnMainByIdAsync(id);
            if (service != null)
            {
                service.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        //get all active services on main
        public async Task<List<ServicesOnMain>> GetAllActiveServicesOnMainAsync()
        {
            return await db.ServicesOnMains.Where(s => !s.IsDeleted).ToListAsync();
        }

        // filter services on main by category
        public async Task<List<ServicesOnMain>> GetServicesOnMainByCategoryAsync(string category)
        {
            return await db.ServicesOnMains
                           .Where(s => s.Category == category && !s.IsDeleted)
                           .ToListAsync();
        }

        // get all services on main by language
        public async Task<List<ServicesOnMain>> GetServicesOnMainByLanguage(Language language)
        {
            return await db.ServicesOnMains
                           .Where(s => s.Language == language && !s.IsDeleted)
                           .ToListAsync();
        }
    }
}