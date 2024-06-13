using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class VehicleBrandsRepository(InsuranceDbContext dbContext) : IVehicleBrandsRepository
{
    public async Task<IEnumerable<VehicleBrand>> GetAll()
    {
        var brands = await dbContext
            .VehicleBrands
            .AsNoTracking()
            .ToListAsync();

        return brands;
    }

    public async Task<VehicleBrand?> GetById(Guid id)
    {
        var brand = await dbContext
            .VehicleBrands
            .FirstOrDefaultAsync(i => i.Id == id);

        return brand;
    }

    public async Task Delete(VehicleBrand entity)
    {
        dbContext.Remove(entity);

        await dbContext.SaveChangesAsync();
    }

    public async Task<Guid> Create(VehicleBrand entity)
    {
        await dbContext.VehicleBrands.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
