using Mitra.Applications.DTOs;

namespace Mitra.Applications.Services.Authentication;

public interface IUserService
{
    Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO register);
    Task<LoginResponse> LoginUserAsync(LoginDTO login);
}