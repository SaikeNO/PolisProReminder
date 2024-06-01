using AutoMapper;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers;

internal class InsurersService(IInsurersRepository insurerRepository, IMapper mapper) : IInsurersService
{
    public async Task Update(Guid id, CreateInsurerDto dto)
    {
        var insurer = await insurerRepository.GetById(id);

        _ = insurer ?? throw new NotFoundException("Klient o podanym ID nie istnieje");

        if (await insurerRepository.GetByPeselAndId(dto.Pesel, id) != null)
            throw new AlreadyExistsException("Klient o podanym numerze PESEL już istnieje");

        insurer.Email = dto.Email;
        insurer.Pesel = dto.Pesel;
        insurer.PhoneNumber = dto.PhoneNumber;
        insurer.FirstName = dto.FirstName;
        insurer.LastName = dto.LastName;

        await insurerRepository.SaveChanges();
    }

    public async Task Delete(Guid id)
    {
        var insurer = await insurerRepository.GetById(id);

        _ = insurer ?? throw new NotFoundException("Klient o podanym ID nie istnieje");

        if (insurer.Policies.Count != 0)
            throw new NotAllowedException("Klient posiada polisy");

        await insurerRepository.Delete(insurer);
        await insurerRepository.SaveChanges();
    }

    public async Task<Guid> Create(CreateInsurerDto dto)
    {
        if (await insurerRepository.GetByPeselAndId(dto.Pesel, null) != null)
            throw new AlreadyExistsException("Klient o podanym numerze PESEL już istnieje");

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
