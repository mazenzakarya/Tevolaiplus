using Tevolai.DAL.Entities;

namespace Tevolai.BL.Managers.EmailCredentials
{
    public interface IEmailCredentialService
    {
        Task AddCredentialAsync(string email, string password, string userName, string? by = null);
        Task<EmailCredential?> GetCredentialAsync(int id, string? by = null);
        Task SoftDeleteCredentialAsync(int id, string? by = null);
        Task UpdateCredentialAsync(EmailCredential credential, string? newPassword = null, string? by = null);
    }
}