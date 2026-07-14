namespace RealTimeChat.Application.Interfaces.Repositories
{
  public interface IGenericRepository<T> where T : class
  {
    Task<T?> GetByIdAsync(Guid id);

    Task addAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<bool> ExistByIdAsync(Guid id);
  }
}