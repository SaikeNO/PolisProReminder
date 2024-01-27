using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;
using PolisProReminder.Models;

namespace PolisProReminder.Services
{
    public interface IInsurancePolicySerivce
    {
        IEnumerable<InsurancePolicyDto> GetAll();
        public InsurancePolicyDto GetById(int id);
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
                .Include(p => p.InsuranceTypes)
                .ToList();

            return _mapper.Map<List<InsurancePolicyDto>>(policies);
        }

        public InsurancePolicyDto GetById(int id)
        {
            var policy = _dbContext
                .InsurancePolicies
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Insurer)
                .Include(p => p.InsuranceTypes)
                .FirstOrDefault(p => p.Id == id);

            if (policy == null)
                throw new NotFoundException("Task not Found");

            return _mapper.Map<InsurancePolicyDto>(policy);
        }
    }
}
