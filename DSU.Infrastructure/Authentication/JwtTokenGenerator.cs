using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using DSU.Application.Common.Interfaces.Services;
using DSU.Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.Options;

namespace DSU.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly JwtSettings _jwtSettings;
  private readonly IDateTimeProvider _dateTimeProvider;

  public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
  {
    _dateTimeProvider = dateTimeProvider;
    _jwtSettings = jwtOptions.Value;
  }

  public string GenerateToken(Guid userId, string firstName, string lastName)
  {
    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
      SecurityAlgorithms.HmacSha256);
    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
      new Claim(JwtRegisteredClaimNames.GivenName, firstName),
      new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var securityToken = new JwtSecurityToken(
      claims: claims,
      signingCredentials: signingCredentials,
      issuer: _jwtSettings.Issuer,
      audience: _jwtSettings.Audience,
      expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes)
      );
    return new JwtSecurityTokenHandler().WriteToken(securityToken);
  }
}