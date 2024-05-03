namespace Mitra.Domain.Repositories.Command.Base;

public interface ICommandRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}