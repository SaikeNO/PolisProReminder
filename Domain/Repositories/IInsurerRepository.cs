using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IInsurerRepository
{
    Task<Guid> Create(Insurer entity);
    Task Delete(Insurer entity);
    Task<IEnumerable<Insurer>> GetAll();
    Task<Insurer?> GetById(Guid id);
    Task SaveChanges();
}
