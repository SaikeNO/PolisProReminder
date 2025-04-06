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

    public async Task<Guid> Create(Vehicle entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Vehicles.AddAsync(entity, cancellationToken);
        await SaveChanges(cancellationToken);

        return entity.Id;
    }

    public async Task<IEnumerable<Vehicle>> GetAll(Guid agentId, CancellationToken cancellationToken = default)
    {
        var vehicles = await dbContext
            .Vehicles
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(v => v.Insurers)
            .Include(v => v.Policies)
            .Include(v => v.VehicleBrand)
            .ToListAsync(cancellationToken);

        return vehicles;
    }

    public async Task<Vehicle?> GetById(Guid agentId, Guid id, CancellationToken cancellationToken = default)
    {
        var vehicle = await dbContext
            .Vehicles
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(v => v.Insurers)
            .Include(v => v.Policies)
            .Include(v => v.VehicleBrand)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

        return vehicle;
    }

    public async Task<Vehicle?> GetByRegistrationNumber(Guid agentId, string registrationNumber, Guid? vehicleId, CancellationToken cancellationToken = default)
    {
        var vehicle = await dbContext
            .Vehicles
            .AsNoTracking()
            .CreatedByAgent(agentId)
            .NotDeleted()
            .FirstOrDefaultAsync(v => v.RegistrationNumber == registrationNumber && v.Id != vehicleId, cancellationToken);

        return vehicle;
    }

    public async Task<(IEnumerable<Vehicle>, int)> GetAllMatchingAsync(Guid agentId,
        string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection,
        CancellationToken cancellationToken = default)
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
             .Where(v => searchPhraseLower == null
                || v.VIN.ToLower().Contains(searchPhraseLower)
                || v.Name.ToLower().Contains(searchPhraseLower)
                || v.RegistrationNumber.ToLower().Contains(searchPhraseLower)
                || v.VehicleBrand.Name.ToLower().Contains(searchPhraseLower)
                || dbContext.Set<IndividualInsurer>()
                    .FilterBySearchPhrase(searchPhraseLower)
                    .Any(i => v.Insurers.Select(insurer => insurer.Id).Contains(i.Id))
                || dbContext.Set<BusinessInsurer>()
                    .FilterBySearchPhrase(searchPhraseLower)
                    .Any(b => v.Insurers.Select(insurer => insurer.Id).Contains(b.Id)));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

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
            .ToListAsync(cancellationToken);

        return (vehicles, totalCount);
    }

    public Task SaveChanges(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}
