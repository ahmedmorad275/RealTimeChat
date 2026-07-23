using MediatR;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.Application.Authentication;
using RealTimeChat.Application.Authentication.Login;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Interfaces.Services;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Domain.Exceptions;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly UserManager<ApplicationUser> _manager;
    private readonly IJwtService _service;
    private readonly IApplicationDbContext _context;

    public LoginCommandHandler(UserManager<ApplicationUser> manager, IJwtService service, IApplicationDbContext context)
    {
        _manager = manager;
        _service = service;
        _context = context;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _manager.FindByEmailAsync(request.Email);

        if (user == null)

            throw new BadRequestException("Invalid Email or Password.");

        var result = await _manager.CheckPasswordAsync(user, request.Password);

        if (!result)
            throw new BadRequestException("Invalid Email or Password.");

        var accessToken = _service.GenerateAccessToken(user);
        var generatedRefreshToken = _service.GenerateRefreshToken();
        var refreshTokenExpiry = _service.GetRefreshTokenExpiryDate();

        var refreshToken = new RefreshToken()
        {
            ExpiresAt = refreshTokenExpiry,
            UserId = user.Id,
            Token = generatedRefreshToken
        };

        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthResponse()
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email!,
            Token = accessToken,
            RefreshToken = generatedRefreshToken
        };

    }
}