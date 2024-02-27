using AutoMapper;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;
using PolisProReminder.Models;

namespace PolisProReminder.Services
{
    public interface IInsuranceCompanyService
    {
        InsuranceCompanyDto Get(int id);
        IEnumerable<InsuranceCompanyDto> GetAll();
        InsuranceCompanyDto Create(CreateInsuranceCompanyDto dto);
        void Delete(int id);
        InsuranceCompanyDto Update(int id, CreateInsuranceCompanyDto dto);
    }

    public class InsuranceCompanyService : IInsuranceCompanyService
    {
        private readonly InsuranceDbContext _dbContext;
        private readonly IMapper _mapper;

        public InsuranceCompanyService(InsuranceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<InsuranceCompanyDto> GetAll()
        {
            var companies = _dbContext
                .InsuranceCompanies
                .ToList();

            return _mapper.Map<List<InsuranceCompanyDto>>(companies);
        }

        public InsuranceCompanyDto Get(int id)
        {
            var company = _dbContext
                .InsuranceCompanies
                .FirstOrDefault(x => x.Id == id);

            if (company == null)
                throw new NotFoundException("Insurance Company not found");

            return _mapper.Map<InsuranceCompanyDto>(company);
        }

        public InsuranceCompanyDto Update(int id, CreateInsuranceCompanyDto dto)
        {
            var company = _dbContext
                .InsuranceCompanies
                .FirstOrDefault(c => c.Id == id);

            if (company == null)
                throw new NotFoundException("Insurance Company not found");

            company.Name = dto.Name;

            var updatedCompany = _dbContext
                .InsuranceCompanies
                .Update(company);

            _dbContext.SaveChanges();

            return _mapper.Map<InsuranceCompanyDto>(updatedCompany.Entity);
        }

        public void Delete(int id)
        {
            var company = _dbContext
                .InsuranceCompanies
                .FirstOrDefault(x => x.Id == id);

            if (company == null)
                throw new NotFoundException("Insurance Company not found");

            _dbContext
                .InsuranceCompanies
                .Remove(company);

            _dbContext.SaveChanges();
        }

        public InsuranceCompanyDto Create(CreateInsuranceCompanyDto dto)
        {
            var company = _dbContext
                .InsuranceCompanies
                .FirstOrDefault(c => c.Name == dto.Name);

            if (company != null)
                throw new AlreadyExistsException($"{company.Name} already exists");

            var createCompany = _mapper.Map<InsuranceCompany>(dto);

            var createdCompany = _dbContext
                .InsuranceCompanies
                .Add(createCompany);

            _dbContext.SaveChanges();

            return _mapper.Map<InsuranceCompanyDto>(createdCompany.Entity);
        }

    }
}
