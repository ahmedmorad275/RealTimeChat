using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Application.Authentication;
using RealTimeChat.Application.Authentication.Login;
using RealTimeChat.Application.Authentication.Register;

namespace RealTimeChat.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterCommand command)
    {
      var response = await _mediator.Send(command);
      return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginCommand command)
    {
      var response = await _mediator.Send(command);
      return Ok(response);
    }
  }
}