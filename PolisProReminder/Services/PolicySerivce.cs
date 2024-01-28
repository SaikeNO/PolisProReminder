using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;
using PolisProReminder.Models.InsurancePolicy;

namespace PolisProReminder.Services
{
    public interface IInsurancePolicySerivce
    {
        IEnumerable<PolicyDto> GetAll();
        public PolicyDto GetById(int id);
    }

    public class PolicySerivce : IInsurancePolicySerivce
    {
        private readonly InsuranceDbContext _dbContext;
        private readonly IMapper _mapper;

        public PolicySerivce(InsuranceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<PolicyDto> GetAll()
        {
            var policies = _dbContext
                .Policies
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Insurer)
                .Include(p => p.InsuranceTypes)
                .ToList()
                .OrderBy(p => p.EndDate);

            return _mapper.Map<List<PolicyDto>>(policies);
        }

        public PolicyDto GetById(int id)
        {
            var policy = _dbContext
                .Policies
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Insurer)
                .Include(p => p.InsuranceTypes)
                .FirstOrDefault(p => p.Id == id);

            if (policy == null)
                throw new NotFoundException("Insurance Policy not Found");

            return _mapper.Map<PolicyDto>(policy);
        }
    }
}
