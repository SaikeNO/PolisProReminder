using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;
using PolisProReminder.Models.Insurer;

namespace PolisProReminder.Services
{
    public interface IInsurerService
    {
        IEnumerable<InsurerDto> GetAll();
        InsurerDto GetById(int id);
        int CreateInsurer(CreateInsurerDto dto);
        void DeleteInsurer(int id);
        void Update(int id, CreateInsurerDto dto);
    }

    public class InsurerService : IInsurerService
    {
        private readonly InsuranceDbContext _dbContext;
        private readonly IMapper _mapper;
        public InsurerService(InsuranceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Update(int id, CreateInsurerDto dto)
        {
            var insurer = _dbContext
               .Insurers
               .FirstOrDefault(i => i.Id == id);

            if (insurer == null)
                throw new NotFoundException("Insurer does not exist");

            insurer.Email = dto.Email;
            insurer.Pesel = dto.Pesel;
            insurer.PhoneNumber = dto.PhoneNumber;
            insurer.FirstName = dto.FirstName;
            insurer.LastName = dto.LastName;

            _dbContext.SaveChanges();
        }
        public void DeleteInsurer(int id)
        {
            var insurer = _dbContext
                .Insurers
                .FirstOrDefault(i => i.Id == id);

            if (insurer == null)
                throw new NotFoundException("Insurer does not exist");
            
            _dbContext.Insurers.Remove(insurer);
            _dbContext.SaveChanges();
        }

        public int CreateInsurer(CreateInsurerDto dto)
        {
            var insurer = _dbContext
                .Insurers
                .FirstOrDefault(i => i.Pesel == dto.Pesel);

            if (insurer != null)
                throw new AlreadyExistsException("Insurer already exists");

            var createInsurer = _mapper.Map<Insurer>(dto);
            _dbContext.Insurers.Add(createInsurer);
            _dbContext.SaveChanges();

            return createInsurer.Id;
        }

        public IEnumerable<InsurerDto> GetAll()
        {
            var insurers = _dbContext
                .Insurers
                .Include(i => i.Policies.OrderBy(p => p.EndDate))
                .ThenInclude(p => p.InsuranceCompany)
                .Include(i => i.Policies)
                .ThenInclude(p => p.InsuranceTypes)
                .ToList();

            return _mapper.Map<List<InsurerDto>>(insurers);
        }

        public InsurerDto GetById(int id)
        {
            var insurer = _dbContext
                .Insurers
                .Include(i => i.Policies.OrderBy(p => p.EndDate))
                .ThenInclude(p => p.InsuranceCompany)
                .Include(i => i.Policies)
                .ThenInclude(p => p.InsuranceTypes)
                .FirstOrDefault(i => i.Id == id);

            if (insurer == null)
                throw new NotFoundException("Insurer not found");

            return _mapper.Map<InsurerDto>(insurer);
        }

    }
}
