namespace RealTimeChat.Domain.Exceptions
{
  public class ValidationException : AppException
  {
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors) : base("One or more validation errors occured.", 400, "Validation Error")
    {
      Errors = errors;
    }
  }
}