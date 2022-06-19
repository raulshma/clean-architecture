using DSU.Domain.Entities;

namespace DSU.Application.Services.Authentication;
public record AuthenticationResult
{
  public User User { get; init; }
  public string Token { get; set; }
}