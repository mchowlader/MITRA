using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mitra.Applications.DTOs;
using Mitra.Applications.IRepositories;
using Mitra.Domain.Entities;
using Mitra.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mitra.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public IConfiguration Configuration { get; }
    public UserRepository(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        Configuration = configuration;
    }
    public async Task<LoginResponse> LoginUserAsync(LoginDTO login)
    {
        var getUser = await FindUserByEmail(login.Email!);
        if (getUser == null) return new LoginResponse(false, "User not found");

        bool checkPassword = BCrypt.Net.BCrypt.Verify(login.Password, getUser.Password);
        if (checkPassword)
            return new LoginResponse(true, "Login Successfully", GenerateJWTToken(getUser));
        else
            return new LoginResponse(false, "Invalid Credential!");
    }
    private string GenerateJWTToken(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"] ??
            throw new InvalidOperationException("JWT Key is missing.")));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        var token = new JwtSecurityToken
        (
            issuer: Configuration["Jwt:Issuer"],
            audience: Configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.UtcNow.AddHours(24), // Example: Expires in 24 hours
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private async Task<ApplicationUser> FindUserByEmail(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO register)
    {
        var getUser = await FindUserByEmail(register.Email!);

        if (getUser is not null)
            return new RegistrationResponse(false, "User already exits");

        _context.Users.Add(new ApplicationUser()
        {
            Name = register.Name,
            Email = register.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(register.Password)
        });

        await _context.SaveChangesAsync();

        return new RegistrationResponse(true, "Registration complete");
    }
}