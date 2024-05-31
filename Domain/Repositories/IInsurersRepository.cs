using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IInsurersRepository
{
    Task<Guid> Create(Insurer entity);
    Task Delete(Insurer entity);
    Task<IEnumerable<Insurer>> GetAll();
    Task<Insurer?> GetById(Guid id);
    Task<(IEnumerable<Insurer>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task SaveChanges();
}
