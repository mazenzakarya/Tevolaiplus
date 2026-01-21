using Tevolai.BL.DTOs.ServicesOnMainDto;
using Tevolai.DAL.Entities;
using Tevolai.DAL.Repository.ServicesOnMainRepo;

namespace Tevolai.BL.Managers.ServicesOnMains
{
    public class ServicesOnMainService : IServicesOnMainService
    {
        private readonly IServicesOnMainRepository _repo;

        public ServicesOnMainService(IServicesOnMainRepository repo)
        {
            _repo = repo;
        }

        //add new service on main
        public async Task AddServiceOnMainAsync(ServicesOnMainDto service)
        {
            var entity = new ServicesOnMain
            {
                Title = service.Title,
                Description = service.Description,
                Price = service.Price,
                ImageUrl = service.ImageUrl,
                Category = service.Category,
                IsActive = service.IsActive,
                Language = service.Language,
                CreatedAt = DateTime.UtcNow,
            };
            await _repo.AddServiceOnMainAsync(entity);
        }

        //get service on main by id
        public async Task<ServicesOnMainDto?> GetServiceOnMainByIdAsync(int id)
        {
            var entity = await _repo.GetServiceOnMainByIdAsync(id);
            if (entity == null || entity.IsDeleted)
                return null;
            return new ServicesOnMainDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                Language = entity.Language,
                ImageUrl = entity.ImageUrl,
                Category = entity.Category,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                IsDeleted = entity.IsDeleted
            };
        }

        //update service on main
        public async Task UpdateServiceOnMainAsync(ServicesOnMainDto service)
        {
            var entity = await _repo.GetServiceOnMainByIdAsync(service.Id);
            if (entity == null || entity.IsDeleted)
                throw new Exception("Service not found");
            entity.Title = service.Title;
            entity.Description = service.Description;
            entity.Price = service.Price;
            entity.Language = service.Language;
            entity.ImageUrl = service.ImageUrl;
            entity.Category = service.Category;
            entity.IsActive = service.IsActive;
            await _repo.UpdateServiceOnMainAsync(entity);
        }

        //soft delete service on main
        public async Task DeleteServiceOnMainAsync(int id)
        {
            var entity = await _repo.GetServiceOnMainByIdAsync(id);
            if (entity == null || entity.IsDeleted)
                throw new Exception("Service not found");
            entity.IsDeleted = true;
            await _repo.UpdateServiceOnMainAsync(entity);
        }

        //get all active services on main
        public async Task<List<ServicesOnMainDto>> GetAllActiveServicesOnMainAsync()
        {
            var entities = await _repo.GetAllActiveServicesOnMainAsync();
            return entities.Select(entity => new ServicesOnMainDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Category = entity.Category,
                IsActive = entity.IsActive,
                Language = entity.Language,
                CreatedAt = entity.CreatedAt,
                IsDeleted = entity.IsDeleted
            }).ToList();
        }

        // filter services on main by category
        public async Task<List<ServicesOnMainDto>> GetServicesOnMainByCategoryAsync(string category)
        {
            var entities = await _repo.GetServicesOnMainByCategoryAsync(category);
            return entities.Select(entity => new ServicesOnMainDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Category = entity.Category,
                IsActive = entity.IsActive,
                Language = entity.Language,
                CreatedAt = entity.CreatedAt,
                IsDeleted = entity.IsDeleted
            }).ToList();
        }

        // filter services on main by language
        public async Task<List<ServicesOnMainDto>> GetServicesOnMainByLanguage(Language language)
        {
            var entities = await _repo.GetServicesOnMainByLanguage(language);
            return entities.Select(entity => new ServicesOnMainDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Category = entity.Category,
                IsActive = entity.IsActive,
                Language = entity.Language,
                CreatedAt = entity.CreatedAt,
                IsDeleted = entity.IsDeleted
            }).ToList();
        }
    }
}