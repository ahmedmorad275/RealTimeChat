namespace RealTimeChat.Domain.Exceptions
{
  public class AppException : Exception
  {
    public int StatusCode { get; init; }
    public string Title { get; init; } = string.Empty;

    public AppException(string msg, int statusCode, string title) : base(msg)
    {
      StatusCode = statusCode;
      Title = title;
    }
  }
}