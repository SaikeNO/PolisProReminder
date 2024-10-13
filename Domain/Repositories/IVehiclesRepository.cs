using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IVehiclesRepository
{
    Task<Guid> Create(Vehicle entity);
    void Delete(Vehicle entity);
    Task<IEnumerable<Vehicle>> GetAll(Guid agentId);
    Task<(IEnumerable<Vehicle>, int)> GetAllMatchingAsync(Guid agentId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<Vehicle?> GetById(Guid agentId, Guid id);
    Task<Vehicle?> GetByRegistrationNumber(Guid agentId, string registrationNumber, Guid? vehicleId);
    Task SaveChanges();
}