using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IVehicleBrandsRepository
{
    Task<Guid> Create(VehicleBrand entity);
    Task Delete(VehicleBrand entity);
    Task<IEnumerable<VehicleBrand>> GetAll();
    Task<VehicleBrand?> GetById(Guid id);
    Task SaveChanges();
}
