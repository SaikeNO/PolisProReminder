using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;
using PolisProReminder.Models;

namespace PolisProReminder.Services;

public interface IInsurerService
{
    Task<int> CreateInsurer(CreateInsurerDto dto);
    Task DeleteInsurer(int id);
    Task<IEnumerable<InsurerDto>> GetAll();
    Task<InsurerDto> GetById(int id);
    Task<Insurer> GetOrCreateIfNotExists(PolicyInsurerDto dto);
    Task Update(int id, CreateInsurerDto dto);
    Task<Insurer> UpdateOrCreateIfNotExists(PolicyInsurerDto dto);
}

public class InsurerService(InsuranceDbContext dbContext, IMapper mapper) : IInsurerService
{
    public async Task Update(int id, CreateInsurerDto dto)
    {
        var insurer = await dbContext
           .Insurers
           .FirstOrDefaultAsync(i => i.Id == id);

        if (insurer == null)
            throw new NotFoundException("Insurer does not exist");

        var checkPesel = await dbContext.Insurers.FirstOrDefaultAsync(i => i.Pesel == dto.Pesel && i.Id != id);

        if (checkPesel != null)
            throw new AlreadyExistsException("Klient o podanym numerze PESEL już istnieje");

        insurer.Email = dto.Email;
        insurer.Pesel = dto.Pesel;
        insurer.PhoneNumber = dto.PhoneNumber;
        insurer.FirstName = dto.FirstName;
        insurer.LastName = dto.LastName;

        await dbContext.SaveChangesAsync();
    }

    public async Task<Insurer> UpdateOrCreateIfNotExists(PolicyInsurerDto dto)
    {
        var insurer = mapper.Map<Insurer>(dto);

        var dbInsurer = await dbContext.Insurers
            .FirstOrDefaultAsync(i => i.Id == insurer.Id);

        if (dbInsurer == null)
        {
            CreateInsurer(mapper.Map<CreateInsurerDto>(insurer));

            insurer = await dbContext.Insurers
                .FirstOrDefaultAsync(i => i.Pesel == insurer.Pesel)!;
        }
        else
        {
            dbInsurer.FirstName = insurer.FirstName;
            dbInsurer.LastName = insurer.LastName;
            dbInsurer.PhoneNumber = insurer.PhoneNumber;
            dbInsurer.Email = insurer.Email;
            dbInsurer.Pesel = insurer.Pesel;
            await dbContext.SaveChangesAsync();
        }

        return insurer;
    }

    public async Task DeleteInsurer(int id)
    {
        var insurer = await dbContext
            .Insurers
            .Include(i => i.Policies)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (insurer == null)
            throw new NotFoundException("Insurer does not exist");

        if (insurer.Policies.Any())
            throw new NotAllowedException("Insurer has policies");

        dbContext.Insurers.Remove(insurer);
        await dbContext.SaveChangesAsync();
    }

    public async Task<int> CreateInsurer(CreateInsurerDto dto)
    {
        var insurer = await dbContext
            .Insurers
            .FirstOrDefaultAsync(i => i.Pesel == dto.Pesel);

        if (insurer != null)
            throw new AlreadyExistsException("Insurer already exists");

        var createInsurer = mapper.Map<Insurer>(dto);
        await dbContext.Insurers.AddAsync(createInsurer);
        await dbContext.SaveChangesAsync();

        return createInsurer.Id;
    }

    public async Task<Insurer> GetOrCreateIfNotExists(PolicyInsurerDto dto)
    {
        var insurer = mapper.Map<Insurer>(dto);

        var dbInsurer = dbContext.Insurers
            .FirstOrDefaultAsync(i => i.Id == insurer.Id);

        if (dbInsurer == null)
        {
            await CreateInsurer(mapper.Map<CreateInsurerDto>(insurer));
            await dbContext.SaveChangesAsync();
            insurer = await dbContext.Insurers
                .FirstOrDefaultAsync(i => i.Pesel == insurer.Pesel)!;
        }

        return insurer;
    }

    public async Task<IEnumerable<InsurerDto>> GetAll()
    {
        var insurers = await dbContext
            .Insurers
            .Include(i => i.Policies.OrderBy(p => p.EndDate))
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .ToListAsync();

        return mapper.Map<List<InsurerDto>>(insurers);
    }

    public async Task<InsurerDto> GetById(int id)
    {
        var insurer = await dbContext
            .Insurers
            .Include(i => i.Policies.OrderBy(p => p.EndDate))
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (insurer == null)
            throw new NotFoundException("Insurer not found");

        return mapper.Map<InsurerDto>(insurer);
    }

}
