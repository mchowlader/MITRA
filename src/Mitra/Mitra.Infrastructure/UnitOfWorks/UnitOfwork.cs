using Mitra.Domain.IRepositories;
using Mitra.Infrastructure.Data;
using System;

namespace Mitra.Infrastructure.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private bool _disposed = false;

    public IContactRepository ContactRepository { get; private set; }

    public UnitOfWork(IContactRepository contactRepository, ApplicationDbContext applicationDbContext)
    {
        ContactRepository = contactRepository;
        _context = applicationDbContext;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        {
            _disposed = true;
            // is used to indicate to the garbage collector (GC) that finalization is unnecessary for the current object
            GC.SuppressFinalize(this);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if(disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}