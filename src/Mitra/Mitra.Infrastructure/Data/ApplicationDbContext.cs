using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mitra.Domain.Entities;

namespace Mitra.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Contact>(c =>
        {
            c.HasKey("Id");
            c.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        base.OnModelCreating(builder);
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
}