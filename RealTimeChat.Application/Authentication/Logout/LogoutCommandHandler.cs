using MediatR;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.Application.Authentication.Logout;
using RealTimeChat.Application.Interfaces;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
  private readonly IApplicationDbContext _context;

  public LogoutCommandHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
  {
    var refreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken);
    if (refreshToken is null || !refreshToken.IsActive)
      return;

    refreshToken.RevokedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync(cancellationToken);
  }
}