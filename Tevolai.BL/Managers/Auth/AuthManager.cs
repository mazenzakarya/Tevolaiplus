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
        private readonly IConfiguration _configuration;

        public AuthManager(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisterDto> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
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

            var token = GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresIn = 3600,
                UserName = user.UserName!,
                Email = user.Email
            };
        }

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName ?? ""),
        new Claim(ClaimTypes.Email, user.Email ?? "")
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}