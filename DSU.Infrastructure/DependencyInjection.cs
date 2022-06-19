using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DSU.Infrastructure.Services;
using DSU.Infrastructure.Authentication;
using DSU.Application.Common.Interfaces.Services;
using DSU.Application.Common.Interfaces.Authentication;

namespace DSU.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
   ConfigurationManager configuration)
  {
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
    
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    return services;
  }
}