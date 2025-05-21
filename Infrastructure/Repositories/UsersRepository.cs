using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class UsersRepository(InsuranceDbContext dbContext, UserManager<User> userManager) : IUsersRepository
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
        => await dbContext.Users.NotDeleted().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

    public async Task<IEnumerable<User>> GetAssistantsAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _userManager.Users
            .Where(x => x.AgentId == userId
                && x.Id != userId
                && !x.IsDeleted)
            .ToListAsync(cancellationToken);

    public async Task UnlockUserAsync(User user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        user.LockoutEnabled = false;
        user.LockoutEnd = null;
        await _userManager.UpdateAsync(user);
    }

    public async Task LockoutUserAsync(User user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.MaxValue;
        await _userManager.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(User user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        user.IsDeleted = true;
        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.MaxValue;
        await _userManager.UpdateAsync(user);
    }

    public async Task UpdateUser(User user) => await _userManager.UpdateAsync(user);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await dbContext.SaveChangesAsync(cancellationToken);
}
