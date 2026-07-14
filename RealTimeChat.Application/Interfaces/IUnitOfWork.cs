namespace RealTimeChat.Application.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    Task<int> SaveAsync(CancellationToken ct);
  }
}