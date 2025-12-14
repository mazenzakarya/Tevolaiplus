using Microsoft.AspNetCore.Identity;
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

        public AuthManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterDto> register(RegisterDto registerDto)  // change it to token dto later
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };
            var result = _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Result.Succeeded)
            {
                throw new Exception("Registeration Failed");
            }
            return registerDto;
        }

        public async Task<string> login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            var emailUser = await _userManager.FindByEmailAsync(loginDto.UserName);
            if (user == null && emailUser == null)
            {
                throw new Exception("User not found");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid password");
            }
            TokenUserDto tokenUserDto = new TokenUserDto
            {
                Id = user.Id,
                Email = user.Email ?? "",
                UserName = user.UserName ?? ""
            };
            return await tokenGenerator(tokenUserDto);
        }

        private async Task<string> tokenGenerator(TokenUserDto user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.UserName ?? "")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This-is-very-secyrity-key-it-should-be-32-chars"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}