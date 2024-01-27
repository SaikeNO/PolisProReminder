using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Entities;
using PolisProReminder.Models;

namespace PolisProReminder.Services
{
    public interface IInsurancePolicySerivce
    {
        IEnumerable<InsurancePolicyDto> GetAll();
    }

    public class InsurancePolicySerivce : IInsurancePolicySerivce
    {
        private readonly InsuranceDbContext _dbContext;
        private readonly IMapper _mapper;

        public InsurancePolicySerivce(InsuranceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<InsurancePolicyDto> GetAll()
        {
            var policies = _dbContext
                .InsurancePolicies
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Insurer)
                .Include (p => p.InsuranceTypes)
                .ToList();

            return _mapper.Map<List<InsurancePolicyDto>>(policies);
        }
    }
}
