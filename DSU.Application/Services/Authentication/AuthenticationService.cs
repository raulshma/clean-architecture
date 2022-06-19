namespace DSU.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  AuthenticationResult IAuthenticationService.Login(string email, string password)
  {
    return new AuthenticationResult
    {
      Email = email,
      Id = Guid.NewGuid(),
      Token = "token"
    };
  }

  AuthenticationResult IAuthenticationService.Register(string firstName, string lastName, string email, string password)
  {
    return new AuthenticationResult
    {
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      Id = Guid.NewGuid(),
      Token = "token"
    };
  }
}