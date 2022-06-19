using DSU.Application.Common.Interfaces.Services;

namespace DSU.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}