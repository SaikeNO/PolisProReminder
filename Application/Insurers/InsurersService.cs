using AutoMapper;
using Microsoft.Extensions.Logging;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers;

internal class InsurersService(IInsurerRepository insurerRepository, IMapper mapper, ILogger logger) : IInsurersService
{
    public async Task<bool> Update(Guid id, CreateInsurerDto dto)
    {
        var insurer = await insurerRepository.GetById(id);
        if (insurer == null)
            return false;

        insurer.Email = dto.Email;
        insurer.Pesel = dto.Pesel;
        insurer.PhoneNumber = dto.PhoneNumber;
        insurer.FirstName = dto.FirstName;
        insurer.LastName = dto.LastName;

        await insurerRepository.SaveChanges();
        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var insurer = await insurerRepository.GetById(id);

        if (insurer == null)
            return false;

        await insurerRepository.Delete(insurer);
        await insurerRepository.SaveChanges();

        return true;
    }

    public async Task<Guid> Create(CreateInsurerDto dto)
    {
        var insurer = mapper.Map<Insurer>(dto);
        await insurerRepository.Create(insurer);
        await insurerRepository.SaveChanges();

        return insurer.Id;
    }

    public async Task<IEnumerable<InsurerDto>> GetAll()
    {
        var insurers = await insurerRepository.GetAll();

        return mapper.Map<List<InsurerDto>>(insurers);
    }

    public async Task<InsurerDto?> GetById(Guid id)
    {
        var insurer = await insurerRepository.GetById(id);

        return mapper.Map<InsurerDto?>(insurer);
    }
}
