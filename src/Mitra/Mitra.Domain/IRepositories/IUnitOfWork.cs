namespace Mitra.Domain.IRepositories;

public interface IUnitOfWork : IDisposable
{
    public Task SaveChangesAsync();
    IContactRepository ContactRepository { get; }
}