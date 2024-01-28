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

    }
}
