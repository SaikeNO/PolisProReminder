using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.Vehicles.Dtos;
using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace PolisProReminder.Infrastructure.Repositories;

internal class VehiclesRepository(InsuranceDbContext dbContext) : IVehiclesRepository
{
    public async Task Delete(Vehicle entity)
    {
        entity.IsDeleted = true;
        await dbContext.SaveChangesAsync();
    }

    public async Task<Guid> Create(Vehicle entity)
    {
        dbContext.Vehicles.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<Vehicle>> GetAll(string agentId)
    {
        var vehicles = await dbContext
            .Vehicles
            .Include(v => v.Insurer)
            .Include(v => v.Policies)
            .Where(v => v.CreatedByAgentId == agentId)
            .ToListAsync();

        return vehicles;
    }

    public async Task<Vehicle?> GetById(string agentId, Guid id)
    {
        var vehicles = await dbContext
            .Vehicles
            .Include(v => v.Insurer)
            .Include(v => v.Policies)
            .Where(v => v.CreatedByAgentId == agentId)
            .FirstOrDefaultAsync(v => v.Id == id);

        return vehicles;
    }

    public async Task<Vehicle?> GetByVin(string agentId, string vin)
    {
        var vehicles = await dbContext
            .Vehicles
            .Where(v => v.CreatedByAgentId == agentId)
            .FirstOrDefaultAsync(v => v.VIN == vin);

        return vehicles;
    }

    public async Task<(IEnumerable<Vehicle>, int)> GetAllMatchingAsync(string agentId,
        string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Vehicles
            .Where(v => v.CreatedByAgentId == agentId)
            .Include(p => p.Insurer)
            .Include(p => p.Policies)
            .Where(p => (searchPhraseLower == null || p.VIN.ToLower().Contains(searchPhraseLower)
                                                    || p.Name.ToLower().Contains(searchPhraseLower)
                                                    || p.RegistrationNumber.ToLower().Contains(searchPhraseLower)
                         && p.IsDeleted == false));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null && sortDirection != SortDirection.None)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Vehicle, object>>>
            {
                { nameof(VehicleDto.Name).ToLower(), v => v.Name },
                { nameof(VehicleDto.VIN).ToLower(), v => v.VIN },
                { nameof(VehicleDto.RegistrationNumber).ToLower(), v => v.RegistrationNumber },
                { nameof(VehicleDto.FirstRegistrationDate).ToLower(), v => v.FirstRegistrationDate },
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Asc
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }
        var vehicles = await baseQuery
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToListAsync();

        return (vehicles, totalCount);
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
