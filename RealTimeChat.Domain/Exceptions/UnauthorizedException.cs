namespace RealTimeChat.Domain.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string msg) : base(msg, 401, "Unauthorized")
        {
        }
    }
}