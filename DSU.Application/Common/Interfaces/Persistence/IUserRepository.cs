using DSU.Domain.Entities;

namespace DSU.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
  User? GetUserByEmail(string email);
  void Add(User user);
}