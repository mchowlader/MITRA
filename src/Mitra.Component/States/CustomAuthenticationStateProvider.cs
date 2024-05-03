using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mitra.Component.States;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string LocalStorageKey = "auth";
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private readonly ILocalStorageService _storageService;
    public CustomAuthenticationStateProvider(ILocalStorageService storageService)
    {
        _storageService = storageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = await _storageService.GetItemAsStringAsync(LocalStorageKey)!;
        if (string.IsNullOrEmpty(token))
            return await Task.FromResult(new AuthenticationState(_anonymous));

        var (name, email) = GetClaimns(token);
        if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            return await Task.FromResult(new AuthenticationState(_anonymous));

        var claims = SetClaimnsPrincipal(name, email);
        if(claims is null)
            return await Task.FromResult(new AuthenticationState(_anonymous));

        else
            return await Task.FromResult(new AuthenticationState(claims));
    }

    public static ClaimsPrincipal SetClaimnsPrincipal(string name, string email)
    {
        if (name is null || email is null) return new ClaimsPrincipal();

        return new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(ClaimTypes.Name, name!),
                new(ClaimTypes.Email, email!)
            ], "JwtAuth"));
    }

    private static (string, string) GetClaimns(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken)) return (null!, null!);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);
        var name = token.Claims.FirstOrDefault(_=> _.Type == ClaimTypes.Name)!.Value;
        var email = token.Claims.FirstOrDefault(_=> _.Type == ClaimTypes.Email)!.Value;

        return (name, email);    
    }

    public async Task UpdateAuthenticationState(string jwtToken)
    {
        var claims = new ClaimsPrincipal();
        if(!string.IsNullOrEmpty(jwtToken))
        {
            var (name, email) = GetClaimns(jwtToken);
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email)) return;

            var setClaims = SetClaimnsPrincipal(name, email);
            if(setClaims is null) return;


        }
        else
        {
            await _storageService.RemoveItemAsync(LocalStorageKey);
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
    }
}