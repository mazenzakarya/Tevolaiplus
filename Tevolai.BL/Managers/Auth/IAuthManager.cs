using Tevolai.BL.DTOs.Auth;

namespace Tevolai.BL.Managers.Auth
{
    public interface IAuthManager
    {
        Task<AuthResponseDto> Login(LoginDto loginDto);

        Task<RegisterDto> Register(RegisterDto registerDto);
    }
}