using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.InsuranceTypes.Dtos;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;

namespace PolisProReminder.Services;

public interface IInsuranceTypeService
{
    Task<int> CreateInsuranceType(CreateInsuranceTypeDto dto);
    Task DeleteInsuranceType(int id);
    Task<IEnumerable<InsuranceTypeDto>> GetAll();
    Task<InsuranceTypeDto> GetById(int id);
    Task Update(int id, CreateInsuranceTypeDto dto);
}

public class InsuranceTypeService(InsuranceDbContext dbContext, IMapper mapper) : IInsuranceTypeService
{
    public async Task Update(int id, CreateInsuranceTypeDto dto)
    {
        var type = await dbContext
           .InsuranceTypes
           .FirstOrDefaultAsync(t => t.Id == id);

        if (type == null)
            throw new NotFoundException("Insurance Type does not exist");

        type.Name = dto.Name;

        await dbContext.SaveChangesAsync();
    }
    public async Task DeleteInsuranceType(int id)
    {
        var type = await dbContext
           .InsuranceTypes
           .FirstOrDefaultAsync(t => t.Id == id);

        if (type == null)
            throw new NotFoundException("Insurance Type does not exist");

        dbContext.InsuranceTypes.Remove(type);
        await dbContext.SaveChangesAsync();
    }

    public async Task<int> CreateInsuranceType(CreateInsuranceTypeDto dto)
    {
        var type = await dbContext
            .InsuranceTypes
            .FirstOrDefaultAsync(t => t.Name == dto.Name);

        if (type != null)
            throw new AlreadyExistsException("Insurance Type already exists");

        var createType = mapper.Map<InsuranceType>(dto);
        await dbContext.InsuranceTypes.AddAsync(createType);
        await dbContext.SaveChangesAsync();

        return createType.Id;
    }

    public async Task<IEnumerable<InsuranceTypeDto>> GetAll()
    {
        var types = await dbContext
            .InsuranceTypes
            .ToListAsync();

        return mapper.Map<List<InsuranceTypeDto>>(types);
    }

    public async Task<InsuranceTypeDto> GetById(int id)
    {
        var type = await dbContext
            .InsuranceTypes
            .FirstOrDefaultAsync(i => i.Id == id);

        if (type == null)
            throw new NotFoundException("Insurance Type not found");

        return mapper.Map<InsuranceTypeDto>(type);
    }
}
