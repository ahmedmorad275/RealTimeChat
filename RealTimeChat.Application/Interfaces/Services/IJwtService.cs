using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Interfaces.Services
{
  public interface IJwtService
  {
    string GenerateAccessToken(ApplicationUser user);
    string GenerateRefreshToken();
    DateTime GetRefreshTokenExpiryDate();
  }
}