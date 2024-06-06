using AutoMapper;
using PolisProReminder.Application.InsuranceTypes.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.InsuranceTypes;

internal class InsuranceTypesService(IUserContext userContext, 
    IInsuranceTypesRepository insuranceTypeRepository, 
    IMapper mapper) : IInsuranceTypesService
{
    private readonly CurrentUser CurrentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");
    public async Task Update(Guid id, CreateInsuranceTypeDto dto)
    {
        var type = await insuranceTypeRepository.GetById(CurrentUser.AgentId, id);

        _ = type ?? throw new NotFoundException("Typ o podanym ID nie istnieje");

        type.Name = dto.Name;

        await insuranceTypeRepository.SaveChanges();
    }

    public async Task Delete(Guid id)
    {
        var type = await insuranceTypeRepository.GetById(CurrentUser.AgentId, id);

        _ = type ?? throw new NotFoundException("Typ o podanym ID nie istnieje");

        await insuranceTypeRepository.Delete(type);
        await insuranceTypeRepository.SaveChanges();
    }

    public async Task<Guid> Create(CreateInsuranceTypeDto dto)
    {
        var type = mapper.Map<InsuranceType>(dto);

        type.CreatedByUserId = CurrentUser.Id;
        type.CreatedByAgentId = CurrentUser.AgentId;

        await insuranceTypeRepository.Create(type);

        return type.Id;
    }

    public async Task<IEnumerable<InsuranceTypeDto>> GetAll()
    {
        var types = await insuranceTypeRepository.GetAll(CurrentUser.AgentId);

        return mapper.Map<List<InsuranceTypeDto>>(types);
    }

    public async Task<InsuranceTypeDto?> GetById(Guid id)
    {
        var type = await insuranceTypeRepository.GetById(CurrentUser.AgentId, id);

        return mapper.Map<InsuranceTypeDto?>(type);
    }
}
