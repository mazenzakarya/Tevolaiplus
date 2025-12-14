using Tevolai.DAL.Entities;
using Tevolai.DAL.Helpers;

namespace Tevolai.DAL.Repository.ContactMessageRepo
{
    public interface IContactMessageRepository
    {
        Task AddContactMessageAsync(ContactMessage contactMessage);

        Task DeleteContactMessageAsync(int id);

        Task<List<ContactMessage>> GetAllContactMessagesAsync();

        Task<ContactMessage?> GetContactMessageByIdAsync(int id);

        Task<List<ContactMessage>> GetContactMessagesByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task<List<ContactMessage>> GetContactMessagesByReadStatusAsync(bool isRead);

        Task<PagedList<ContactMessage>> GetContactMessagesPagedAsync(int pageNumber, int pageSize);

        Task MarkAsReadAsync(int id);
    }
}