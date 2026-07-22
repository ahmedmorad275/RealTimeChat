namespace RealTimeChat.Domain.Exceptions
{
  public class BadRequestException : AppException
  {
    public BadRequestException(string msg) : base(msg, 400, "Bad Request")
    {
    }
  }
}