using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IVehiclesRepository
{
    Task<Guid> Create(Vehicle entity);
    Task Delete(Vehicle entity);
    Task<IEnumerable<Vehicle>> GetAll(string agentId);
    Task<(IEnumerable<Vehicle>, int)> GetAllMatchingAsync(string agentId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<Vehicle?> GetById(string agentId, Guid id);
    Task<Vehicle?> GetByVin(string agentId, string vin);
    Task SaveChanges();
}