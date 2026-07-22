using RealTimeChat.Domain.Exceptions;

public class ConflictException : AppException
{
    public ConflictException(string msg) : base(msg, 409, "Conflict")
    {
    }
}