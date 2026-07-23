using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RealTimeChat.Application.Authentication.RefreshToken
{
  public class RefreshTokenCommand : IRequest<AuthResponse>
  {
    [Required]
    [MaxLength(200)]
    public string Token { get; set; } = string.Empty;
  }
}