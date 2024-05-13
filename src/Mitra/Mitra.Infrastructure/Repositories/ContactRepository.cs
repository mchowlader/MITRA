using Microsoft.EntityFrameworkCore;
using Mitra.Domain.IRepositories;
using Mitra.Domain.Entities;
using Mitra.Infrastructure.Data;

namespace Mitra.Infrastructure.Repositories;

public class ContactRepository : Repository<Contact>, IContactRepository
{
    public ContactRepository(ApplicationDbContext context) : base((DbContext)context)
    {
    }
}