using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Authorization;
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
        public PolicyDto UpdateIsPaidPolicy(int id, bool isPaid);
        public PolicyDto UpdatePolicy(int id, CreatePolicyDto dto);
        public void DeletePolicy(int id);
    }

    public class PolicyService : IPolicyService
    {
        private readonly InsuranceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IInsuranceTypeService _typeService;
        private readonly IInsuranceCompanyService _companyService;
        private readonly IInsurerService _insurerService;
        private readonly IAuthorizationService _authorizationService;

        public PolicyService(InsuranceDbContext dbContext, IMapper mapper, IUserContextService userContextService, IInsuranceTypeService typeService, IInsuranceCompanyService companyService, IInsurerService insurerService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
            _typeService = typeService;
            _companyService = companyService;
            _insurerService = insurerService;
            _authorizationService = authorizationService;
        }

        public void DeletePolicy(int id)
        {
            var policy = _dbContext.Policies.FirstOrDefault(p => p.Id == id);
            if (policy == null)
                throw new NotFoundException("Policy does not exists");

            _dbContext.Policies.Remove(policy);
            _dbContext.SaveChanges();
        }
        public PolicyDto UpdateIsPaidPolicy(int id, bool isPaid)
        {
            var policy = _dbContext.Policies.FirstOrDefault(p => p.Id == id);
            if (policy == null)
                throw new NotFoundException("Policy does not exists");

            policy.IsPaid = isPaid;
            _dbContext.SaveChanges();

            return _mapper.Map<PolicyDto>(policy);
        }

        public PolicyDto UpdatePolicy(int id, CreatePolicyDto dto)
        {
            List<InsuranceType> types = new();

            var policy = _dbContext
                .Policies
                .FirstOrDefault(p => p.Id == id);

            if (policy == null)
                throw new NotFoundException("Policy does not exists");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, policy,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();

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

            var dbCompany = _dbContext.InsuranceCompanies.FirstOrDefault(c => c.Id == company.Id);
            if (dbCompany == null)
            {
                _companyService.Create(_mapper.Map<CreateInsuranceCompanyDto>(company));
                _dbContext.SaveChanges();
                company = _dbContext.InsuranceCompanies.FirstOrDefault(c => c.Name == company.Name)!;
            }
            else
            {
                dbCompany.Name = company.Name;
            }

            var dbInsurer = _dbContext.Insurers.FirstOrDefault(i => i.Id == insurer.Id);
            if (dbInsurer == null)
            {
                _insurerService.CreateInsurer(_mapper.Map<CreateInsurerDto>(insurer));
                _dbContext.SaveChanges();
                insurer = _dbContext.Insurers.FirstOrDefault(i => i.Pesel == insurer.Pesel)!;
            }
            else
            {
                dbInsurer.FirstName = insurer.FirstName;
                dbInsurer.LastName = insurer.LastName;
                dbInsurer.PhoneNumber = insurer.PhoneNumber;
                dbInsurer.Email = insurer.Email;
                dbInsurer.Pesel = insurer.Pesel;
            }

            var updatePolicy = new Policy
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

            _dbContext.Policies.Update(updatePolicy);
            _dbContext.SaveChanges();

            return _mapper.Map<PolicyDto>(updatePolicy);
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

            var dbCompany = _dbContext.InsuranceCompanies.FirstOrDefault(c => c.Id == company.Id);
            if (dbCompany == null)
            {
                _companyService.Create(_mapper.Map<CreateInsuranceCompanyDto>(company));
                _dbContext.SaveChanges();
                company = _dbContext.InsuranceCompanies.FirstOrDefault(c => c.Name == company.Name)!;
            }

            var dbInsurer = _dbContext.Insurers.FirstOrDefault(i => i.Id == insurer.Id);
            if (dbInsurer == null)
            {
                _insurerService.CreateInsurer(_mapper.Map<CreateInsurerDto>(insurer));
                _dbContext.SaveChanges();
                insurer = _dbContext.Insurers.FirstOrDefault(i => i.Pesel == insurer.Pesel)!;
            }

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
                CreatedById = _userContextService.GetUserId,
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
                .Where(GetAllPredicate)
                .OrderBy(p => p.EndDate)
                .ToList();

            return _mapper.Map<List<PolicyDto>>(policies);
        }

        private bool GetAllPredicate(Policy p)
        {
            var user = _userContextService.User;
            if (user.IsInRole("Admin"))
                return true;

            if (p.CreatedById == _userContextService.GetUserId)
                return true;

            var superiorId = _userContextService.GetSuperiorId;

            if (superiorId is not null && p.CreatedById == superiorId)
                return true;

            return false;
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
