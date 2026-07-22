namespace RealTimeChat.Domain.Exceptions
{
  public class NotFoundException : AppException
  {
    public NotFoundException(string msg) : base(msg, 404, "Not Found")
    {
    }
    public NotFoundException(object entity, object id) : base($"{entity.GetType()} with Id [{id}] not found.", 404, "Not Found")
    {
    }
  }
}