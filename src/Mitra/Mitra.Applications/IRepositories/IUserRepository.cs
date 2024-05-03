using Mitra.Applications.DTOs;

namespace Mitra.Applications.IRepositories;

public interface IUserRepository
{
    Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO register);
    Task<LoginResponse> LoginUserAsync(LoginDTO login);
}