using Tevolai.BL.DTOs.Auth;

namespace Tevolai.BL.Managers.Auth
{
    public interface IAuthManager
    {
        Task<string> login(LoginDto loginDto);
        Task<RegisterDto> register(RegisterDto registerDto);
    }
}