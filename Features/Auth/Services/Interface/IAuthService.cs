using DemoAppBE.Features.Auth.DTOs;
using DemoAppBE.Shared;

namespace DemoAppBE.Features.Auth.Services.Interface
{
    public interface IAuthService:IService
    {
        Task<Result> loginUserAsync(LoginRequestDTO loginRequestDto);
        Task<Result> registerUserAsync(RegisterRequestDTO registerRequestDto);
        Task<Result> getUserDetails(int Id);

        Task<Result> generateRefreshTokenAsync(int UserId, string RefreshToken);
    }
}
