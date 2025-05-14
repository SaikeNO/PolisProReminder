using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IUsersRepository
{
    Task<User> GetAgentAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
