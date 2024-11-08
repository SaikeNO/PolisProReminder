using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface ITodoTasksRepository
{
    Task<Guid> Create(TodoTask entity);
    void Delete(TodoTask entity);
    Task<IEnumerable<TodoTask>> GetAll(Guid userId);
    Task<TodoTask?> Get(Guid userId, Guid id);
    Task SaveChanges();
}
