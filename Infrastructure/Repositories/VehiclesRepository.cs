using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.Vehicles.Dtos;
using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace PolisProReminder.Infrastructure.Repositories;

internal class VehiclesRepository(InsuranceDbContext dbContext) : IVehiclesRepository
{
    public void Delete(Vehicle entity)
    {
        entity.IsDeleted = true;
    }

    public async Task<Guid> Create(Vehicle entity)
    {
        await dbContext.Vehicles.AddAsync(entity);
        await SaveChanges();

        return entity.Id;
    }

    public async Task<IEnumerable<Vehicle>> GetAll(Guid agentId)
    {
        var vehicles = await dbContext
            .Vehicles
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(v => v.Insurers)
            .Include(v => v.Policies)
            .Include(v => v.VehicleBrand)
            .ToListAsync();

        return vehicles;
    }

    public async Task<Vehicle?> GetById(Guid agentId, Guid id)
    {
        var vehicle = await dbContext
            .Vehicles
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(v => v.Insurers)
            .Include(v => v.Policies)
            .Include(v => v.VehicleBrand)
            .FirstOrDefaultAsync(v => v.Id == id);

        return vehicle;
    }

    public async Task<Vehicle?> GetByRegistrationNumber(Guid agentId, string registrationNumber, Guid? vehicleId)
    {
        var vehicle = await dbContext
            .Vehicles
            .AsNoTracking()
            .CreatedByAgent(agentId)
            .NotDeleted()
            .FirstOrDefaultAsync(v => v.RegistrationNumber == registrationNumber && v.Id != vehicleId);

        return vehicle;
    }

    public async Task<(IEnumerable<Vehicle>, int)> GetAllMatchingAsync(Guid agentId,
        string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Vehicles
            .AsNoTracking()
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(p => p.Insurers)
            .Include(p => p.Policies)
            .Include(v => v.VehicleBrand)
            .Where(p => searchPhraseLower == null || p.VIN.ToLower().Contains(searchPhraseLower)
                                                    || p.Name.ToLower().Contains(searchPhraseLower)
                                                    || p.RegistrationNumber.ToLower().Contains(searchPhraseLower)
                                                    || p.VehicleBrand.Name.ToLower().Contains(searchPhraseLower))
            .FilterByInsurer(searchPhraseLower, dbContext);

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
