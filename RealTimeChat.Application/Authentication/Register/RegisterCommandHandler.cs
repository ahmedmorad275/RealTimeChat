using MediatR;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.Application.Interfaces.Services;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Authentication.Register
{
  public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private IJwtService _jwtService;
    public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
      _userManager = userManager;
      _jwtService = jwtService;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
      var existUser = await _userManager.FindByEmailAsync(request.Email);

      if (existUser is not null)
        throw new ArgumentException("Email already exists.");

      var user = new ApplicationUser()
      {
        Email = request.Email,
        FullName = request.FullName,
        UserName = request.Email
      };

      var result = await _userManager.CreateAsync(user, request.Password);

      if (!result.Succeeded)
        throw new Exception(result.Errors.First().Description);

      var token = _jwtService.GenerateAccessToken(user);

      return new RegisterResponse()
      {
        Email = user.Email,
        FullName = user.FullName,
        Id = user.Id,
        Token = token
      };
    }
  }
}