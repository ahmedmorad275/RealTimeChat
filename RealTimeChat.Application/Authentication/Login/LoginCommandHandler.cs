using MediatR;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.Application.Authentication;
using RealTimeChat.Application.Authentication.Login;
using RealTimeChat.Application.Interfaces.Services;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Domain.Exceptions;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
  private readonly UserManager<ApplicationUser> _manager;
  private readonly IJwtService _service;

  public LoginCommandHandler(UserManager<ApplicationUser> manager, IJwtService service)
  {
    _manager = manager;
    _service = service;
  }

  public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await _manager.FindByEmailAsync(request.Email);

    if (user == null)

      throw new BadRequestException("Invalid Email or Password.");

    var result = await _manager.CheckPasswordAsync(user, request.Password);

    if (!result)
      throw new BadRequestException("Invalid Email or Password.");

    var token = _service.GenerateAccessToken(user);

    return new AuthResponse()
    {
      Id = user.Id,
      FullName = user.FullName,
      Email = user.Email!,
      Token = token
    };

  }
}