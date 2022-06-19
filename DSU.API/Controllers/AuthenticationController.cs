using DSU.Application.Services.Authentication;
using DSU.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DSU.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
  private readonly IAuthenticationService _authenticationService;

  public AuthenticationController(IAuthenticationService authenticationService)
  {
    _authenticationService = authenticationService;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
    var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);
    return Ok(response);
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var authResult = _authenticationService.Login(request.Email, request.Password);
    var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);
    return Ok(response);
  }
}