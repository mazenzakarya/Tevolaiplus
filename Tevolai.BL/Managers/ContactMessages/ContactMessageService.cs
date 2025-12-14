using Tevolai.BL.DTOs.ContactMessageDto;
using Tevolai.DAL.Entities;
using Tevolai.DAL.Helpers;
using Tevolai.DAL.Repository.ContactMessageRepo;

namespace Tevolai.BL.Managers.ContactMessages
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IContactMessageRepository _repo;

        public ContactMessageService(IContactMessageRepository repo)
        {
            _repo = repo;
        }

        public async Task AddNewContactMessage(ContactMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("Email is required");

            if (string.IsNullOrWhiteSpace(dto.Message))
                throw new Exception("Message is required");

            var entity = new ContactMessage
            {
                FullName = dto.Name,
                Email = dto.Email,
                Message = dto.Message,
            };

            await _repo.AddContactMessageAsync(entity);
        }

        // Pagination
        public async Task<PagedList<ContactMessageDto>> GetPagedAsync(int page, int pageSize)
        {
            var data = await _repo.GetContactMessagesPagedAsync(page, pageSize);

            var mappedItems = data.Items.Select(x => new ContactMessageDto
            {
                Id = x.Id,
                Name = x.FullName,
                Email = x.Email,
                Message = x.Message,
                CreatedAt = x.CreatedAt
            }).ToList();

            return new PagedList<ContactMessageDto>(
                mappedItems,
                data.PageNumber,
                data.PageSize,
                data.TotalCount
            );
        }

        //get by id
        public async Task<ContactMessageDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetContactMessageByIdAsync(id);
            if (entity == null) return null;
            return new ContactMessageDto
            {
                Id = entity.Id,
                Name = entity.FullName,
                Email = entity.Email,
                Message = entity.Message,
                CreatedAt = entity.CreatedAt
            };
        }

        //mark as read
        public async Task MarkAsReadAsync(int id)
        {
            await _repo.MarkAsReadAsync(id);
        }

        //delete
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteContactMessageAsync(id);
        }

        //filter by read status
        public async Task<List<ContactMessageDto>> GetByReadStatusAsync(bool isRead)
        {
            var entities = await _repo.GetContactMessagesByReadStatusAsync(isRead);
            return entities.Select(x => new ContactMessageDto
            {
                Id = x.Id,
                Name = x.FullName,
                Email = x.Email,
                Message = x.Message,
                CreatedAt = x.CreatedAt
            }).ToList();
        }

        //count unread messages
        public async Task<int> CountUnreadMessagesAsync()
        {
            var unreadMessages = await _repo.GetContactMessagesByReadStatusAsync(false);
            return unreadMessages.Count;
        }
    }
}