using Tevolai.DAL.Entities;
using Tevolai.DAL.Repository.EmailCredentialRepo;

namespace Tevolai.BL.Managers.EmailCredentials
{
    public class EmailCredentialService : IEmailCredentialService
    {
        private readonly IEmailCredentialRepository repository;
        private readonly EncryptionService encryptionService;

        public EmailCredentialService(IEmailCredentialRepository repository, EncryptionService encryptionService)
        {
            this.repository = repository;
            this.encryptionService = encryptionService;
        }

        public async Task AddCredentialAsync(string email, string password, string userName, string? by = null)
        {
            var credential = new EmailCredential
            {
                EmailAddress = email,
                EncryptedPassword = encryptionService.Encrypt(password),
                UserName = userName
            };

            credential.AddAuditEvent("Created", by);

            await repository.AddAsync(credential);
        }

        public async Task<EmailCredential?> GetCredentialAsync(int id, string? by = null)
        {
            var credential = await repository.GetByIdAsync(id);
            if (credential == null) return null;

            credential.EncryptedPassword = encryptionService.Decrypt(credential.EncryptedPassword);
            credential.LastUsedAt = DateTime.UtcNow;
            credential.AddAuditEvent("Accessed", by);

            await repository.UpdateAsync(credential);
            return credential;
        }

        public async Task UpdateCredentialAsync(EmailCredential credential, string? newPassword = null, string? by = null)
        {
            if (!string.IsNullOrEmpty(newPassword))
                credential.EncryptedPassword = encryptionService.Encrypt(newPassword);

            credential.AddAuditEvent("Updated", by);

            await repository.UpdateAsync(credential);
        }

        public async Task SoftDeleteCredentialAsync(int id, string? by = null)
        {
            var credential = await repository.GetByIdAsync(id);
            if (credential == null) throw new Exception("Credential not found");

            credential.AddAuditEvent("SoftDeleted", by);
            await repository.SoftDeleteAsync(credential);
        }
    }
}