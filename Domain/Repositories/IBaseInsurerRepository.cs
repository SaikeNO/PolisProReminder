using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IBaseInsurersRepository
{
    Task<IEnumerable<BaseInsurer>> GetAll(Guid agentId);
    Task<BaseInsurer?> GetById(Guid agentId, Guid id);
    Task<Guid> Create(BaseInsurer entity);
    Task SaveChanges();
}