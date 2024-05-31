using AutoMapper;
using Microsoft.Extensions.Logging;
using PolisProReminder.Application.InsuranceTypes.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.InsuranceTypes;

internal class InsuranceTypesService(IInsuranceTypeRepository insuranceTypeRepository, IMapper mapper, ILogger logger) : IInsuranceTypesService
{
    public async Task<bool> Update(Guid id, CreateInsuranceTypeDto dto)
    {
        var type = await insuranceTypeRepository.GetById(id);

        if (type == null)
            return false;

        type.Name = dto.Name;

        await insuranceTypeRepository.SaveChanges();

        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var type = await insuranceTypeRepository.GetById(id);

        if (type == null)
            return false;

        await insuranceTypeRepository.Delete(type);
        await insuranceTypeRepository.SaveChanges();
        return true;
    }

    public async Task<Guid> Create(CreateInsuranceTypeDto dto)
    {
        var type = mapper.Map<InsuranceType>(dto);

        await insuranceTypeRepository.Create(type);
        await insuranceTypeRepository.SaveChanges();

        return type.Id;
    }

    public async Task<IEnumerable<InsuranceTypeDto>> GetAll()
    {
        var types = await insuranceTypeRepository.GetAll();

        return mapper.Map<List<InsuranceTypeDto>>(types);
    }

    public async Task<InsuranceTypeDto?> GetById(Guid id)
    {
        var type = await insuranceTypeRepository.GetById(id);

        return mapper.Map<InsuranceTypeDto?>(type);
    }
}
