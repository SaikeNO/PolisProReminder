using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IBusinessInsurersRepository : IBaseInsurersRepository
{
    Task<IEnumerable<BusinessInsurer>> GetAllBusiness(Guid agentId);
    Task<(IEnumerable<BusinessInsurer>, int)> GetAllMatchingAsync(Guid agentId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<BusinessInsurer?> GetByNipRegonAndId(Guid agentId, string? nip, string? regon, Guid? id);
}