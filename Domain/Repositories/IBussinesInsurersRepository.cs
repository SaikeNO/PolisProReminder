using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IBussinesInsurersRepository : IBaseInsurersRepository<BussinesInsurer>
{
    Task<(IEnumerable<BussinesInsurer>, int)> GetAllMatchingAsync(Guid agentId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<BussinesInsurer?> GetByNipRegonAndId(Guid agentId, string nip, string regon, Guid? id);
}