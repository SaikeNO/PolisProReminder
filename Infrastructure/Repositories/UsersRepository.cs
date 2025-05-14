using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class UsersRepository(InsuranceDbContext dbContext) : IUsersRepository
{
    public async Task<User> GetAgentAsync(Guid userId, CancellationToken cancellationToken = default) => await dbContext.Users.Where(u => u.AgentId == userId).FirstAsync(cancellationToken);

    public async Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default) => await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
}
