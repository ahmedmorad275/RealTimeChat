using MediatR;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Interfaces.Services;
using RealTimeChat.Domain.Exceptions;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Authentication.RefreshToken
{
  public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
  {
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _service;

    public RefreshTokenCommandHandler(IApplicationDbContext context, IJwtService service)
    {
      _context = context;
      _service = service;
    }

    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
      var storedToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == request.Token, cancellationToken);
      if (storedToken is null || !storedToken.IsActive)
        throw new UnauthorizedException("Invalid Token.");

      var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == storedToken.UserId);
      if (user is null)
        throw new UnauthorizedException("Invalid Token.");

      var accessToken = _service.GenerateAccessToken(user);
      var generatedRefreshToken = _service.GenerateRefreshToken();
      var refreshTokenExpiry = _service.GetRefreshTokenExpiryDate();

      var refreshToken = new Domain.Entities.RefreshToken()
      {
        ExpiresAt = refreshTokenExpiry,
        UserId = user.Id,
        Token = generatedRefreshToken
      };

      storedToken.ReplacedByToken = generatedRefreshToken;
      storedToken.RevokedAt = DateTime.UtcNow;

      await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
      await _context.SaveChangesAsync(cancellationToken);

      return new AuthResponse()
      {
        Email = user.Email!,
        FullName = user.FullName,
        Token = accessToken,
        RefreshToken = generatedRefreshToken,
        Id = user.Id
      };
    }
  }
}