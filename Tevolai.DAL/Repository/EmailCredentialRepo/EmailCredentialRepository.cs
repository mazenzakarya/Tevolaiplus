using Microsoft.EntityFrameworkCore;
using Tevolai.DAL.Context;
using System.Text.Json;
using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.EmailCredentialRepo
{
    public class EmailCredentialRepository : IEmailCredentialRepository
    {
        private readonly TevolaiDbContext db;

        public EmailCredentialRepository(TevolaiDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(EmailCredential credential)
        {
            await db.EmailCredentials.AddAsync(credential);
            await db.SaveChangesAsync();
        }

        public async Task<EmailCredential?> GetByIdAsync(int id)
        {
            return await db.EmailCredentials.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task UpdateAsync(EmailCredential credential)
        {
            db.EmailCredentials.Update(credential);
            await db.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(EmailCredential credential)
        {
            credential.IsDeleted = true;
            db.EmailCredentials.Update(credential);
            await db.SaveChangesAsync();
        }
    }
}