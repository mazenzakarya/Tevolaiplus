using Tevolai.BL.DTOs.EmailCredentialDto;
using Tevolai.DAL.Entities;

namespace Tevolai.BL.Managers.EmailCredentials
{
    public interface IEmailCredentialService
    {
        Task AddCredentialAsync(string email, string password, string userName, int? customerId = null, string? hostingPlatform = null, string? notes = null, string? by = null);
        Task<EmailCredentialDto?> GetCredentialAsync(int id, bool includeDecryptedPassword = false, string? by = null);
        Task SoftDeleteCredentialAsync(int id, string? by = null);
        Task UpdateCredentialAsync(int id, string? userName = null, string? newPassword = null, int? customerId = null, string? hostingPlatform = null, string? notes = null, string? by = null);
        Task<List<EmailCredentialDto>> GetAllCredentialsAsync(bool includeDeleted = false);
    }
}