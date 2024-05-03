using Mitra.Applications.DTOs;
using System.Net.Http.Json;

namespace Mitra.Applications.Services.Authentication;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<LoginResponse> LoginUserAsync(LoginDTO login)
    {
        var response = await _httpClient
            .PostAsJsonAsync("api/account/login", login);

        var result = await response
            .Content
            .ReadFromJsonAsync<LoginResponse>();

        return result!;
    }

    public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO register)
    {
        var response = await _httpClient
            .PostAsJsonAsync("api/account/register", register);

        var result = response
            .Content
            .ReadFromJsonAsync<RegistrationResponse>();

        return result.Result!;
    }
}