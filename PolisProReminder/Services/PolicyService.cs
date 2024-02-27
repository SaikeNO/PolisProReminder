using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;
using PolisProReminder.Models;

namespace PolisProReminder.Services
{
    public interface IPolicyService
    {
        IEnumerable<PolicyDto> GetAll();
        public PolicyDto GetById(int id);
        public int CreatePolicy(CreatePolicyDto dto);

    }

    public class PolicyService : IPolicyService
    {
        private readonly InsuranceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IInsuranceTypeService _typeService;

        public PolicyService(InsuranceDbContext dbContext, IMapper mapper, IInsuranceTypeService typeService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _typeService = typeService;
        }

        public int CreatePolicy(CreatePolicyDto dto)
        {
            List<InsuranceType> types = new();

            var policy = _dbContext
                .Policies
                .FirstOrDefault(p => p.PolicyNumber == dto.PolicyNumber);

            if (policy != null)
                throw new AlreadyExistsException("Policy already Exists");

            foreach (var t in dto.InsuranceTypes)
            {
                var type = _dbContext.InsuranceTypes.FirstOrDefault(c => c.Id == t.Id);
                if (type == null)
                {
                    //if(t.Name.Length > 30)

                    _typeService.CreateInsuranceType(_mapper.Map<CreateInsuranceTypeDto>(t));
                    _dbContext.SaveChanges();
                    type = _dbContext.InsuranceTypes.FirstOrDefault(c => c.Name == t.Name);
                    types.Add(type!);
                }
                else
                { 
                    types.Add(type);
                }
            }

            var company = _mapper.Map<InsuranceCompany>(dto.InsuranceCompany);
            var insurer = _mapper.Map<Insurer>(dto.Insurer);

            var createPolicy = new Policy
            {
                PolicyNumber = dto.PolicyNumber,
                InsuranceCompanyId = company.Id,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                PaymentDate = dto.PaymentDate,
                InsurerId = insurer.Id,
                InsuranceTypes = types,
                IsPaid = dto.IsPaid,
                Title = dto.Title,
            };

            _dbContext.Policies.Add(createPolicy);
            _dbContext.SaveChanges();

            return createPolicy.Id;
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
