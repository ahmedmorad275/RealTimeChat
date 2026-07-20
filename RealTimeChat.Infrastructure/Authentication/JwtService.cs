using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RealTimeChat.Application.Interfaces.Services;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Infrastructure.Authentication
{
  public class JwtService : IJwtService
  {
    private readonly JwtOptions _options;

    public JwtService(JwtOptions options)
    {
      _options = options;
    }

    public string GenerateAccessToken(ApplicationUser user)
    {
      var claims = new List<Claim>()
      {
        new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new (JwtRegisteredClaimNames.Email, user.Email!),
        new (JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
        new (ClaimTypes.Name, user.FullName)

      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        issuer: _options.Issuer,
        audience: _options.Audience,
        signingCredentials: creds,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenExpirationMinutes));

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
      var token = RandomNumberGenerator.GetBytes(64).ToString();

      return token!;
    }
  }
}