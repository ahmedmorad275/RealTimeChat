namespace RealTimeChat.Application.Authentication.Register
{
  public class RegisterResponse
  {
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
  }
}