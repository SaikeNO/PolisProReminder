using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IBaseInsurersRepository<TEntity> where TEntity : BaseInsurer
{
    Task<IEnumerable<TEntity>> GetAll(Guid agentId);
    Task<TEntity?> GetById(Guid agentId, Guid id);
    Task<Guid> Create(TEntity entity);
    Task SaveChanges();
}