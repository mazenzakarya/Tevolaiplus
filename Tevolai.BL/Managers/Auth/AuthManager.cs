using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tevolai.BL.DTOs.Auth;
using Tevolai.DAL.Entities;

namespace Tevolai.BL.Managers.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthManager(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<RegisterDto> Register(RegisterDto registerDto)
        {
            var existingByEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingByEmail != null)
            {
                throw new Exception("Email is already registered");
            }

            var existingByUserName = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existingByUserName != null)
            {
                throw new Exception("Username is already taken");
            }

            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                PhoneNumber = registerDto.PhoneNumber,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            // Assign default "Customer" role to new registrations
            await EnsureRoleExistsAsync("Customer");
            await _userManager.AddToRoleAsync(user, "Customer");

            return registerDto;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new Exception("Invalid email or password");

            var isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isValid)
                throw new Exception("Invalid email or password");

            // Verify roles are loaded
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any())
            {
                const string defaultRole = "Customer";
                await EnsureRoleExistsAsync(defaultRole);
                await _userManager.AddToRoleAsync(user, defaultRole);
                roles = await _userManager.GetRolesAsync(user);
            }

            var token = await GenerateTokenAsync(user);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresIn = 3600,
                UserName = user.UserName!,
                Email = user.Email,
                Id = user.Id
            };
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    new Claim(ClaimTypes.Email, user.Email ?? "")
                };

            var roles = await _userManager.GetRolesAsync(user);
            if (roles != null && roles.Any())
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"))
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task EnsureRoleExistsAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return;
            }

            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}