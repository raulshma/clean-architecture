using DSU.Application.Common.Interfaces.Authentication;
using DSU.Application.Common.Interfaces.Persistence;
using DSU.Domain.Entities;

namespace DSU.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _userRepository = userRepository;
  }

  AuthenticationResult IAuthenticationService.Register(string firstName, string lastName, string email, string password)
  {
    if (_userRepository.GetUserByEmail(email) is not null)
    {
      throw new Exception("User with given email already exist.");
    }

    var user = new User
    {
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      Password = password
    };

    _userRepository.Add(user);

    Guid userId = Guid.NewGuid();
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult
    {
      User = user,
      Token = token
    };
  }
  AuthenticationResult IAuthenticationService.Login(string email, string password)
  {
    if (_userRepository.GetUserByEmail(email) is not User user)
    {
      throw new Exception("User with given email does not exist.");
    }

    if (user.Password != password)
    {
      throw new Exception("Wrong password.");
    }

    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult
    {
      User = user,
      Token = token
    };
  }

}