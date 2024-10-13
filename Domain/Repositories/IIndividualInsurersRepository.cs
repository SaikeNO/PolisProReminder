using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IIndividualInsurersRepository : IBaseInsurersRepository
{
    Task<IEnumerable<BaseInsurer>> GetAllIndividual(Guid agentId);
    Task<(IEnumerable<IndividualInsurer>, int)> GetAllMatchingAsync(Guid agentId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<IndividualInsurer?> GetByPeselAndId(Guid agentId, string pesel, Guid? id);
}