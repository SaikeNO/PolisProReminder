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
        private readonly IInsurerService _insurerService;
        private readonly IAuthorizationService _authorizationService;

        public PolicyService(InsuranceDbContext dbContext, IMapper mapper, IUserContextService userContextService, IInsurerService insurerService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
            _insurerService = insurerService;
            _authorizationService = authorizationService;
        }

        public void DeletePolicy(int id) // Need to do soft delete
        {
            var policy = _dbContext.Policies
                .FirstOrDefault(p => p.Id == id);

            if (policy == null)
                throw new NotFoundException("Policy does not exists");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, policy,
               new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();

            _dbContext.Policies.Remove(policy);
            _dbContext.SaveChanges();
        }
        public PolicyDto UpdateIsPaidPolicy(int id, bool isPaid)
        {
            var policy = _dbContext.Policies
                .FirstOrDefault(p => p.Id == id);

            if (policy == null)
                throw new NotFoundException("Policy does not exists");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, policy,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();

            policy.IsPaid = isPaid;
            _dbContext.SaveChanges();

            return _mapper.Map<PolicyDto>(policy);
        }

        public PolicyDto UpdatePolicy(int id, CreatePolicyDto dto)
        {
            var policy = _dbContext
                .Policies
                .Include(p => p.InsuranceTypes)
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Insurer)
                .FirstOrDefault(p => p.Id == id);

            if (policy == null)
                throw new NotFoundException("Policy does not exists");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, policy,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();

            var insurer = _insurerService.UpdateOrCreateIfNotExists(dto.Insurer);

            List<InsuranceType> newTypes = new();
            foreach (var item in dto.InsuranceTypes)
            {
                var type = _dbContext.InsuranceTypes
                    .FirstOrDefault(t => t.Id == item.Id);

                if (type is not null) 
                    newTypes.Add(type);
            }

            policy.PolicyNumber = dto.PolicyNumber;
            policy.InsuranceCompanyId = dto.InsuranceCompany.Id;
            policy.StartDate = dto.StartDate;
            policy.EndDate = dto.EndDate;
            policy.PaymentDate = dto.PaymentDate;
            policy.InsurerId = insurer.Id;
            policy.IsPaid = dto.IsPaid;
            policy.Title = dto.Title;
            policy.InsuranceTypes.Clear();
            policy.InsuranceTypes.AddRange(newTypes);

            _dbContext.SaveChanges();

            return _mapper.Map<PolicyDto>(policy);
        }

        public int CreatePolicy(CreatePolicyDto dto)
        {
            var policy = _dbContext
                .Policies
                .FirstOrDefault(p => p.PolicyNumber == dto.PolicyNumber);

            if (policy != null)
                throw new AlreadyExistsException("Policy already Exists");

            int creatingUserId = _userContextService.GetUserId;

            if (_userContextService.User.IsInRole("User") && _userContextService.GetSuperiorId is not null)
            {
                creatingUserId = (int)_userContextService.GetSuperiorId;
            }

            var createPolicy = new Policy
            {
                PolicyNumber = dto.PolicyNumber,
                InsuranceCompanyId = dto.InsuranceCompany.Id,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                PaymentDate = dto.PaymentDate,
                InsurerId = _insurerService.GetOrCreateIfNotExists(dto.Insurer).Id,
                InsuranceTypes = _mapper.Map<List<InsuranceType>>(dto.InsuranceTypes),
                IsPaid = dto.IsPaid,
                Title = dto.Title,
                CreatedById = creatingUserId,
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
                .Where(GetPredicate)
                .OrderBy(p => p.EndDate)
                .ToList();

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

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, policy,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();

            return _mapper.Map<PolicyDto>(policy);
        }

        private bool GetPredicate(Policy p)
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
    }
}
