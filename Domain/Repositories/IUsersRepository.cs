using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IUsersRepository
{
    Task<User> GetAgentAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task LockoutUserAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
    void UpdateUser(User user);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
