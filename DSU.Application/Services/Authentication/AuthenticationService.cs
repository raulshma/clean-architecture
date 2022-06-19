using DSU.Application.Common.Interfaces.Authentication;

namespace DSU.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;

  public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  AuthenticationResult IAuthenticationService.Register(string firstName, string lastName, string email, string password)
  {
    Guid userId = Guid.NewGuid();
    var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

    return new AuthenticationResult
    {
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      Id = userId,
      Token = token
    };
  }
  AuthenticationResult IAuthenticationService.Login(string email, string password)
  {
    return new AuthenticationResult
    {
      Email = email,
      Id = Guid.NewGuid(),
      Token = "token"
    };
  }

}