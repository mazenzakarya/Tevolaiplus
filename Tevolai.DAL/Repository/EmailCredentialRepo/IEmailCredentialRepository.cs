
using Tevolai.DAL.Entities;

namespace Tevolai.DAL.Repository.EmailCredentialRepo
{
    public interface IEmailCredentialRepository
    {
        Task AddAsync(EmailCredential credential);
        Task<EmailCredential?> GetByIdAsync(int id);
        Task SoftDeleteAsync(EmailCredential credential);
        Task UpdateAsync(EmailCredential credential);
    }
}