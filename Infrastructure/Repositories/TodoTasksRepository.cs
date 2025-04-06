using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class TodoTasksRepository(InsuranceDbContext dbContext) : ITodoTasksRepository
{
    private readonly InsuranceDbContext _dbContext = dbContext;

    public async Task<IEnumerable<TodoTask>> GetAll(Guid userId)
    {
        var tasks = await _dbContext
            .TodoTasks
            .Where(t => t.CreatedByUserId == userId)
            .OrderBy(t => t.Order)
            .AsNoTracking()
            .ToListAsync();

        return tasks;
    }

    public async Task<TodoTask?> Get(Guid userId, Guid id)
    {
        var task = await _dbContext
            .TodoTasks
            .Where(t => t.CreatedByUserId == userId
                    && t.Id == id)
            .FirstOrDefaultAsync();

        return task;
    }

    public void Delete(TodoTask entity) => _dbContext.Remove(entity);

    public async Task<Guid> Create(TodoTask entity)
    {
        await _dbContext.AddAsync(entity);

        return entity.Id;
    }

    public Task SaveChanges(CancellationToken cancellationToken = default) => _dbContext.SaveChangesAsync(cancellationToken);
}
