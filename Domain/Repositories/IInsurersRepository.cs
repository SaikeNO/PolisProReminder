using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IInsurersRepository
{
    Task<Guid> Create(Insurer entity);
    Task Delete(Insurer entity);
    Task<IEnumerable<Insurer>> GetAll(string agentId);
    Task<Insurer?> GetById(string agentId, Guid id);
    Task<(IEnumerable<Insurer>, int)> GetAllMatchingAsync(string agentId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<Insurer?> GetByPeselAndId(string agentId, string pesel, Guid? id);
    Task SaveChanges();
}
