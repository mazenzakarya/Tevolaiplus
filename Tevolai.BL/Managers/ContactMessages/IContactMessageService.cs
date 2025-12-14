using Tevolai.BL.DTOs.ContactMessageDto;
using Tevolai.DAL.Helpers;

namespace Tevolai.BL.Managers.ContactMessages
{
    public interface IContactMessageService
    {
        Task AddNewContactMessage(ContactMessageDto dto);
        Task<int> CountUnreadMessagesAsync();
        Task DeleteAsync(int id);
        Task<ContactMessageDto?> GetByIdAsync(int id);
        Task<List<ContactMessageDto>> GetByReadStatusAsync(bool isRead);
        Task<PagedList<ContactMessageDto>> GetPagedAsync(int page, int pageSize);
        Task MarkAsReadAsync(int id);
    }
}