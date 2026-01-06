using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tevolai.BL.DTOs.ApplicationUserDto;
using Tevolai.DAL.Entities;

namespace Tevolai.BL.Managers.ApplicationUsers
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _userManager.Users.CountAsync();
        }

        //get all users with roles excluding deleted users
        public async Task<List<ApplicationUserDto>> GetAllAsync()
        {
            var users = await _userManager.Users
                .Where(u => !u.IsDeleted)
                .ToListAsync();

            var result = new List<ApplicationUserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                result.Add(new ApplicationUserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email!,
                    UserName = user.UserName!,
                    PhoneNumber = user.PhoneNumber!,
                    IsActive = !user.LockoutEnabled,
                    Roles = roles.ToList()
                });
            }

            return result;
        }

        //get user by id with roles excluding deleted users
        public async Task<ApplicationUserDto?> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null || user.IsDeleted)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new ApplicationUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                UserName = user.UserName!,
                IsActive = !user.LockoutEnabled,
                Roles = roles.ToList()
            };
        }

        public async Task<ApplicationUserDto?> GetByEmailAsync(string email)
        {
            var users = await _userManager.FindByEmailAsync(email);
            if (users == null || users.IsDeleted)
                return null;
            var roles = await _userManager.GetRolesAsync(users);
            return new ApplicationUserDto
            {
                Id = users.Id,
                FullName = users.FullName,
                Email = users.Email!,
                UserName = users.UserName!,
                IsActive = !users.LockoutEnabled,
                Roles = roles.ToList()
            };
        }

        //create user with roles
        public async Task<IdentityResult> CreateAsync(AddUserDto dto)
        {
            var existingEmail = await _userManager.FindByEmailAsync(dto.Email);
            if (existingEmail != null && !existingEmail.IsDeleted)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email is already registered" });
            }

            var existingUserName = await _userManager.FindByNameAsync(dto.UserName);
            if (existingUserName != null && !existingUserName.IsDeleted)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Username is already taken" });
            }

            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return result;

            if (dto.Roles.Any())
                await _userManager.AddToRolesAsync(user, dto.Roles);

            return result;
        }

        //update user by id using anonymous type to avoid overposting
        public async Task<IdentityResult> UpdateAsync(string userId, string email, string userName, string fullName, string phoneNumber)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed();

            var otherWithEmail = await _userManager.FindByEmailAsync(email);
            if (otherWithEmail != null && otherWithEmail.Id != userId && !otherWithEmail.IsDeleted)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email is already registered" });
            }

            var otherWithUserName = await _userManager.FindByNameAsync(userName);
            if (otherWithUserName != null && otherWithUserName.Id != userId && !otherWithUserName.IsDeleted)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Username is already taken" });
            }

            var updatedUser = new
            {
                user.Id,
                Email = email,
                UserName = userName,
                FullName = fullName,
                PhoneNumber = user.PhoneNumber
            };

            user.Email = updatedUser.Email;
            user.UserName = updatedUser.UserName;
            user.FullName = updatedUser.FullName;
            user.PhoneNumber = updatedUser.PhoneNumber;

            return await _userManager.UpdateAsync(user);
        }

        //reset password
        public async Task<IdentityResult> ResetPasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result;
        }

        // soft delete user
        public async Task<IdentityResult> SoftDeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed();

            user.IsDeleted = true;

            return await _userManager.UpdateAsync(user);
        }
    }
}