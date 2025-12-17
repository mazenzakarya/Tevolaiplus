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
            return await db.EmailCredentials
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task UpdateAsync(EmailCredential credential)
        {
            db.EmailCredentials.Update(credential);
            await db.SaveChangesAsync();
        }

        public async Task UpdateLastUsedAndAuditAsync(EmailCredential credential)
        {
            // Only update LastUsedAt and AuditLog without touching the password
            var existing = await db.EmailCredentials.FindAsync(credential.Id);
            if (existing != null)
            {
                existing.LastUsedAt = credential.LastUsedAt;
                existing.AuditLog = credential.AuditLog;
                db.Entry(existing).Property(x => x.LastUsedAt).IsModified = true;
                db.Entry(existing).Property(x => x.AuditLog).IsModified = true;
                await db.SaveChangesAsync();
            }
        }

        public async Task SoftDeleteAsync(EmailCredential credential)
        {
            var existing = await db.EmailCredentials.FindAsync(credential.Id);
            if (existing != null)
            {
                existing.IsDeleted = true;
                existing.AuditLog = credential.AuditLog;
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<EmailCredential>> GetAllAsync(bool includeDeleted = false)
        {
            var query = db.EmailCredentials.AsNoTracking();
            
            if (!includeDeleted)
                query = query.Where(e => !e.IsDeleted);
            
            return await query.ToListAsync();
        }
    }
}