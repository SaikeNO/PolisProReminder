using AutoMapper;
using PolisProReminder.Application.InsuranceTypes.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.InsuranceTypes;

internal class InsuranceTypesService(IInsuranceTypesRepository insuranceTypeRepository, IMapper mapper) : IInsuranceTypesService
{
    public async Task Update(Guid id, CreateInsuranceTypeDto dto)
    {
        var type = await insuranceTypeRepository.GetById(id);

        _ = type ?? throw new NotFoundException("Typ o podanym ID nie istnieje");

        type.Name = dto.Name;

        await insuranceTypeRepository.SaveChanges();
    }

    public async Task Delete(Guid id)
    {
        var type = await insuranceTypeRepository.GetById(id);

        _ = type ?? throw new NotFoundException("Typ o podanym ID nie istnieje");

        await insuranceTypeRepository.Delete(type);
        await insuranceTypeRepository.SaveChanges();
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
