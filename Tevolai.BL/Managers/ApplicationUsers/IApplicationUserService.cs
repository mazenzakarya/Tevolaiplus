using Microsoft.AspNetCore.Identity;
using Tevolai.BL.DTOs.ApplicationUserDto;

namespace Tevolai.BL.Managers.ApplicationUsers
{
    public interface IApplicationUserService
    {
        Task<IdentityResult> CreateAsync(AddUserDto dto);

        Task<List<ApplicationUserDto>> GetAllAsync();

        Task<ApplicationUserDto?> GetByEmailAsync(string email);

        Task<ApplicationUserDto?> GetByIdAsync(string id);

        Task<int> GetUsersCountAsync();

        Task<IdentityResult> ResetPasswordAsync(string userId, string newPassword);

        Task<IdentityResult> SoftDeleteAsync(string userId);

        Task<IdentityResult> UpdateAsync(string userId, string email, string userName, string fullName);
    }
}