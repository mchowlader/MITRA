using Microsoft.AspNetCore.Mvc;
using Mitra.Applications.DTOs;
using Mitra.Domain.IRepositories;

namespace Mitra.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserRepository _user;

    public AccountController(IUserRepository user)
    {
        _user = user;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LogIn(LoginDTO login)
    {
        //var result = await _user.LoginUserAsync(login);
        //return Ok(result);
        return default;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegisterUserDTO register)
    {
        //var result = await _user.RegisterUserAsync(register);
        //return Ok(result);
        return default;

    }
}