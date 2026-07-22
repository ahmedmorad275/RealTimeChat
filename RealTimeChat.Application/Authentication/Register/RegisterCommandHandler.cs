using MediatR;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.Application.Interfaces.Services;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Domain.Exceptions;

namespace RealTimeChat.Application.Authentication.Register
{
  public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private IJwtService _jwtService;
    public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
      _userManager = userManager;
      _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
      var existUser = await _userManager.FindByEmailAsync(request.Email);

      if (existUser is not null)
        throw new ConflictException("Email already exists.");

      var user = new ApplicationUser()
      {
        Email = request.Email,
        FullName = request.FullName,
        UserName = request.Email
      };

      var result = await _userManager.CreateAsync(user, request.Password);

      if (!result.Succeeded)
        throw new BadRequestException(result.Errors.First().Description);

      var token = _jwtService.GenerateAccessToken(user);

      return new AuthResponse()
      {
        Email = user.Email,
        FullName = user.FullName,
        Id = user.Id,
        Token = token
      };
    }
  }
}