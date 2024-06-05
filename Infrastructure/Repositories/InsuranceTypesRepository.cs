﻿using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class InsuranceTypesRepository(InsuranceDbContext dbContext) : IInsuranceTypesRepository
{
    
    public async Task<IEnumerable<InsuranceType>> GetAll(string agentId)
    {
        var types = await dbContext
            .InsuranceTypes
            .Where(t => t.CreatedByUserId == agentId)
            .ToListAsync();

        return types;
    }

    public async Task<IEnumerable<InsuranceType>> GetManyByIds(string agentId, IEnumerable<Guid> ids)
    {
        var types = await dbContext.InsuranceTypes
            .Where(t => t.CreatedByUserId == agentId && ids.Contains(t.Id))
            .ToListAsync();

        return types;
    }

    public async Task<InsuranceType?> GetById(string agentId, Guid id)
    {
        var type = await dbContext
            .InsuranceTypes
            .Where(t => t.CreatedByUserId == agentId)
            .FirstOrDefaultAsync(i => i.Id == id);

        return type;
    }

    public async Task Delete(InsuranceType entity)
    {
        dbContext.Remove(entity);

        await dbContext.SaveChangesAsync();
    }

    public async Task<Guid> Create(InsuranceType entity)
    {
        await dbContext.InsuranceTypes.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
