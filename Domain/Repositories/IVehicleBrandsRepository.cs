using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IVehicleBrandsRepository
{
    Task<Guid> Create(VehicleBrand entity, CancellationToken cancellationToken = default);
    Task Delete(VehicleBrand entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<VehicleBrand>> GetAll(CancellationToken cancellationToken = default);
    Task<VehicleBrand?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task SaveChanges(CancellationToken cancellationToken = default);
}
