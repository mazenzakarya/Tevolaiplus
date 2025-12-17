using Tevolai.BL.DTOs.EmailCredentialDto;
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

        public async Task AddCredentialAsync(string email, string password, string userName, int? customerId = null, string? hostingPlatform = null, string? notes = null, string? by = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required", nameof(email));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required", nameof(password));
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("UserName is required", nameof(userName));

            var credential = new EmailCredential
            {
                EmailAddress = email,
                EncryptedPassword = encryptionService.Encrypt(password),
                UserName = userName,
                CustomerId = customerId,
                HostingPlatform = hostingPlatform,
                Notes = notes
            };

            credential.AddAuditEvent("Created", by);

            await repository.AddAsync(credential);
        }

        public async Task<EmailCredentialDto?> GetCredentialAsync(int id, bool includeDecryptedPassword = false, string? by = null)
        {
            var credential = await repository.GetByIdAsync(id);
            if (credential == null) return null;

            // Store encrypted password before updating (NEVER decrypt in entity)
            var encryptedPassword = credential.EncryptedPassword;
            
            // Create a temporary credential for audit update
            var tempCredential = new EmailCredential
            {
                Id = credential.Id,
                LastUsedAt = DateTime.UtcNow,
                AuditLog = credential.AuditLog
            };
            tempCredential.AddAuditEvent("Accessed", by);
            
            // Update only LastUsedAt and AuditLog, NOT the password
            await repository.UpdateLastUsedAndAuditAsync(tempCredential);

            // Map to DTO and decrypt password only if requested (using the original encrypted value)
            var dto = new EmailCredentialDto
            {
                Id = credential.Id,
                CustomerId = credential.CustomerId,
                EmailAddress = credential.EmailAddress,
                UserName = credential.UserName,
                HostingPlatform = credential.HostingPlatform,
                Notes = credential.Notes,
                CreatedAt = credential.CreatedAt,
                LastUsedAt = tempCredential.LastUsedAt,
                AuditLog = tempCredential.AuditLog
            };

            // Only decrypt if explicitly requested - use the original encrypted password
            if (includeDecryptedPassword)
            {
                dto.DecryptedPassword = encryptionService.Decrypt(encryptedPassword);
            }

            return dto;
        }

        public async Task UpdateCredentialAsync(int id, string? userName = null, string? newPassword = null, int? customerId = null, string? hostingPlatform = null, string? notes = null, string? by = null)
        {
            var credential = await repository.GetByIdAsync(id);
            if (credential == null)
                throw new KeyNotFoundException("Credential not found");

            // Update only provided fields
            if (!string.IsNullOrWhiteSpace(userName))
                credential.UserName = userName;
            
            if (customerId.HasValue)
                credential.CustomerId = customerId;
            
            if (hostingPlatform != null)
                credential.HostingPlatform = hostingPlatform;
            
            if (notes != null)
                credential.Notes = notes;

            // Encrypt new password if provided
            if (!string.IsNullOrWhiteSpace(newPassword))
                credential.EncryptedPassword = encryptionService.Encrypt(newPassword);

            credential.AddAuditEvent("Updated", by);
            await repository.UpdateAsync(credential);
        }

        public async Task SoftDeleteCredentialAsync(int id, string? by = null)
        {
            var credential = await repository.GetByIdAsync(id);
            if (credential == null)
                throw new KeyNotFoundException("Credential not found");

            credential.AddAuditEvent("SoftDeleted", by);
            await repository.SoftDeleteAsync(credential);
        }

        public async Task<List<EmailCredentialDto>> GetAllCredentialsAsync(bool includeDeleted = false)
        {
            var credentials = await repository.GetAllAsync(includeDeleted);
            
            return credentials.Select(c => new EmailCredentialDto
            {
                Id = c.Id,
                CustomerId = c.CustomerId,
                EmailAddress = c.EmailAddress,
                UserName = c.UserName,
                HostingPlatform = c.HostingPlatform,
                Notes = c.Notes,
                CreatedAt = c.CreatedAt,
                LastUsedAt = c.LastUsedAt,
                AuditLog = c.AuditLog
                // Never include decrypted password in list
            }).ToList();
        }
    }
}