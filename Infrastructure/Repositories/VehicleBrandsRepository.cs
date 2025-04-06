using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class VehicleBrandsRepository(InsuranceDbContext dbContext) : IVehicleBrandsRepository
{
    public async Task<IEnumerable<VehicleBrand>> GetAll(CancellationToken cancellationToken = default)
    {
        var brands = await dbContext
            .VehicleBrands
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return brands;
    }

    public async Task<VehicleBrand?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var brand = await dbContext
            .VehicleBrands
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

        return brand;
    }

    public async Task Delete(VehicleBrand entity, CancellationToken cancellationToken = default)
    {
        dbContext.Remove(entity);

        await SaveChanges(cancellationToken);
    }

    public async Task<Guid> Create(VehicleBrand entity, CancellationToken cancellationToken = default)
    {
        await dbContext.VehicleBrands.AddAsync(entity, cancellationToken);
        await SaveChanges(cancellationToken);

        return entity.Id;
    }

    public Task SaveChanges(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}
