using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IUsersRepository
{
    Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetAssistantsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UnlockUserAsync(User user, CancellationToken cancellationToken = default);
    Task LockoutUserAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateUser(User user);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
