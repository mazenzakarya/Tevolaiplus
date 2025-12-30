using System.Text.Json;
using Tevolai.BL.DTOs.EmailCredentialDto;
using Tevolai.DAL.Entities;
using Tevolai.DAL.Repository.EmailCredentialRepo;

namespace Tevolai.BL.Managers.EmailCredentials
{
    public class EmailCredentialService : IEmailCredentialService
    {
        private readonly IEmailCredentialRepository _repository;
        private readonly EncryptionService _encryptionService;

        public EmailCredentialService(IEmailCredentialRepository repository, EncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }

        public async Task AddCredentialAsync(string email, string password, string userName, int? customerId = null, string? hostingPlatform = null, string? notes = null, string? by = null)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Email, Password, and UserName are required");

            var encryptedPassword = _encryptionService.Encrypt(password);
            var auditLog = JsonSerializer.Serialize(new
            {
                Action = "Created",
                By = by ?? "System",
                Timestamp = DateTime.UtcNow
            });

            var credential = new EmailCredential
            {
                EmailAddress = email,
                EncryptedPassword = encryptedPassword,
                UserName = userName,
                CustomerId = customerId,
                HostingPlatform = hostingPlatform,
                Notes = notes,
                CreatedAt = DateTime.UtcNow,
                AuditLog = auditLog,
                IsDeleted = false
            };

            await _repository.AddAsync(credential);
        }

        public async Task<EmailCredentialDto?> GetCredentialAsync(int id, bool includeDecryptedPassword = false, string? by = null)
        {
            var credential = await _repository.GetByIdAsync(id);
            if (credential == null)
                return null;

            // Update last used and audit log
            credential.LastUsedAt = DateTime.UtcNow;
            var auditEntry = new
            {
                Action = "Accessed",
                By = by ?? "System",
                Timestamp = DateTime.UtcNow,
                IncludePassword = includeDecryptedPassword
            };
            var existingAudit = string.IsNullOrEmpty(credential.AuditLog) 
                ? new List<object>() 
                : JsonSerializer.Deserialize<List<object>>(credential.AuditLog) ?? new List<object>();
            existingAudit.Add(auditEntry);
            credential.AuditLog = JsonSerializer.Serialize(existingAudit);
            await _repository.UpdateLastUsedAndAuditAsync(credential);

            var dto = new EmailCredentialDto
            {
                Id = credential.Id,
                CustomerId = credential.CustomerId,
                EmailAddress = credential.EmailAddress,
                UserName = credential.UserName,
                HostingPlatform = credential.HostingPlatform,
                Notes = credential.Notes,
                CreatedAt = credential.CreatedAt,
                LastUsedAt = credential.LastUsedAt,
                AuditLog = credential.AuditLog
            };

            if (includeDecryptedPassword)
            {
                dto.DecryptedPassword = _encryptionService.Decrypt(credential.EncryptedPassword);
            }

            return dto;
        }

        public async Task<List<EmailCredentialDto>> GetAllCredentialsAsync(bool includeDeleted = false)
        {
            var credentials = await _repository.GetAllAsync(includeDeleted);
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
                AuditLog = c.AuditLog,
                DecryptedPassword = string.Empty // Never include password in list
            }).ToList();
        }

        public async Task UpdateCredentialAsync(int id, string? userName = null, string? newPassword = null, int? customerId = null, string? hostingPlatform = null, string? notes = null, string? by = null)
        {
            var credential = await _repository.GetByIdAsync(id);
            if (credential == null)
                throw new KeyNotFoundException("Credential not found");

            if (!string.IsNullOrWhiteSpace(userName))
                credential.UserName = userName;
            if (customerId.HasValue)
                credential.CustomerId = customerId;
            if (!string.IsNullOrWhiteSpace(hostingPlatform))
                credential.HostingPlatform = hostingPlatform;
            if (notes != null)
                credential.Notes = notes;
            if (!string.IsNullOrWhiteSpace(newPassword))
                credential.EncryptedPassword = _encryptionService.Encrypt(newPassword);

            var auditEntry = new
            {
                Action = "Updated",
                By = by ?? "System",
                Timestamp = DateTime.UtcNow
            };
            var existingAudit = string.IsNullOrEmpty(credential.AuditLog) 
                ? new List<object>() 
                : JsonSerializer.Deserialize<List<object>>(credential.AuditLog) ?? new List<object>();
            existingAudit.Add(auditEntry);
            credential.AuditLog = JsonSerializer.Serialize(existingAudit);

            await _repository.UpdateAsync(credential);
        }

        public async Task SoftDeleteCredentialAsync(int id, string? by = null)
        {
            var credential = await _repository.GetByIdAsync(id);
            if (credential == null)
                throw new KeyNotFoundException("Credential not found");

            var auditEntry = new
            {
                Action = "Deleted",
                By = by ?? "System",
                Timestamp = DateTime.UtcNow
            };
            var existingAudit = string.IsNullOrEmpty(credential.AuditLog) 
                ? new List<object>() 
                : JsonSerializer.Deserialize<List<object>>(credential.AuditLog) ?? new List<object>();
            existingAudit.Add(auditEntry);
            credential.AuditLog = JsonSerializer.Serialize(existingAudit);

            await _repository.SoftDeleteAsync(credential);
        }
    }
}
