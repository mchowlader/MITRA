using Microsoft.EntityFrameworkCore;
using Mitra.Domain.Entities;

namespace Mitra.Infrastructure.Data;

internal class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
}