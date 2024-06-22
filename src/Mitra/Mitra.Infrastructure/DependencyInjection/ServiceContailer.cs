using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mitra.Applications.IServices;
using Mitra.Applications.Services;
using Mitra.Domain.IRepositories;
using Mitra.Infrastructure.Data;
using Mitra.Infrastructure.Repositories;
using Mitra.Infrastructure.UnitOfWorks;

namespace Mitra.Infrastructure.DependencyInjection;

public static class ServiceContailer
{
    public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<ApplicationDbContext>(option => option
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b
            .MigrationsAssembly(typeof(ServiceContailer)
            .Assembly
            .FullName)), ServiceLifetime.Scoped);

        services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        //services
        //    .AddAuthentication(option =>
        //    {
        //        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(options =>
        //    {
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidateLifetime = true,
        //            ValidIssuer = configuration["Jwt:Issuer"],
        //            ValidAudience = configuration["Jwt:Audience"],
        //            IssuerSigningKey = new SymmetricSecurityKey
        //            (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
        //        };
        //    });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IContactRepository, ContactRepository>();

        return services;
    }
}