using Microsoft.EntityFrameworkCore;
using Tevolai.DAL.Context;
using Tevolai.DAL.Entities;
using Tevolai.DAL.Helpers;

namespace Tevolai.DAL.Repository.ContactMessageRepo
{
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly TevolaiDbContext db;

        public ContactMessageRepository(TevolaiDbContext db)
        {
            this.db = db;
        }

        public async Task AddContactMessageAsync(ContactMessage contactMessage)
        {
            db.ContactMessages.Add(contactMessage);
            await db.SaveChangesAsync();
        }

        public async Task<List<ContactMessage>> GetAllContactMessagesAsync()
        {
            return await db.ContactMessages.OrderByDescending(cm => cm.CreatedAt).ToListAsync();
        }

        public async Task<ContactMessage?> GetContactMessageByIdAsync(int id)
        {
            return await db.ContactMessages.FirstOrDefaultAsync(cm => cm.Id == id);
        }

        public async Task MarkAsReadAsync(int id)
        {
            var contactMessage = await db.ContactMessages.FirstOrDefaultAsync(cm => cm.Id == id);
            if (contactMessage != null)
            {
                contactMessage.IsRead = true;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteContactMessageAsync(int id)
        {
            var contactMessage = await db.ContactMessages.FirstOrDefaultAsync(cm => cm.Id == id);
            if (contactMessage != null)
            {
                db.ContactMessages.Remove(contactMessage);
                await db.SaveChangesAsync();
            }
        }

        // filtering contact messages by IsRead status
        public async Task<List<ContactMessage>> GetContactMessagesByReadStatusAsync(bool isRead)
        {
            return await db.ContactMessages
                           .Where(cm => cm.IsRead == isRead)
                           .OrderByDescending(cm => cm.CreatedAt)
                           .ToListAsync();
        }

        // pagination for contact messages
        public async Task<PagedList<ContactMessage>> GetContactMessagesPagedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await db.ContactMessages.CountAsync();
            var items = await db.ContactMessages
                                .OrderByDescending(cm => cm.CreatedAt)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
            return new PagedList<ContactMessage>(items, pageNumber, pageSize, totalCount);
        }

        // filtering by date range
        public async Task<List<ContactMessage>> GetContactMessagesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await db.ContactMessages
                           .Where(cm => cm.CreatedAt >= startDate && cm.CreatedAt <= endDate)
                           .OrderByDescending(cm => cm.CreatedAt)
                           .ToListAsync();
        }
    }
}