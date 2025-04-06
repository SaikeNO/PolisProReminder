using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IVehiclesRepository
{
    Task<Guid> Create(Vehicle entity, CancellationToken cancellation = default);
    void Delete(Vehicle entity);
    Task<IEnumerable<Vehicle>> GetAll(Guid agentId, CancellationToken cancellationToken = default);
    Task<(IEnumerable<Vehicle>, int)> GetAllMatchingAsync(Guid agentId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken = default);
    Task<Vehicle?> GetById(Guid agentId, Guid id, CancellationToken cancellationToken = default);
    Task<Vehicle?> GetByRegistrationNumber(Guid agentId, string registrationNumber, Guid? vehicleId, CancellationToken cancellationToken = default);
    Task SaveChanges(CancellationToken cancellationToken = default);
}