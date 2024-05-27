﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Authorization;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;
using PolisProReminder.Models;

namespace PolisProReminder.Services;

public interface IPolicyService
{
    Task<int> CreatePolicy(CreatePolicyDto dto);
    Task DeletePolicy(int id);
    Task<IEnumerable<InsurerPolicyDto>> GetInsurerPolicies(int id);
    Task<PolicyDto> GetById(int id);
    Task<PolicyDto> UpdateIsPaidPolicy(int id, bool isPaid);
    Task<PolicyDto> UpdatePolicy(int id, CreatePolicyDto dto);
}

public class PolicyService(InsuranceDbContext dbContext, IMapper mapper, IUserContextService userContextService, IInsurerService insurerService, IAuthorizationService authorizationService) : IPolicyService
{
    public async Task DeletePolicy(int id)
    {
        var policy = await dbContext.Policies
            .FirstOrDefaultAsync(p => p.Id == id);

        if (policy == null)
            throw new NotFoundException("Policy does not exists");

        var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User!, policy,
           new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        if (!authorizationResult.Succeeded)
            throw new ForbidException();

        policy.IsDeleted = true;
        await dbContext.SaveChangesAsync();
    }
    public async Task<PolicyDto> UpdateIsPaidPolicy(int id, bool isPaid)
    {
        var policy = await dbContext.Policies
            .FirstOrDefaultAsync(p => p.Id == id);

        if (policy == null)
            throw new NotFoundException("Policy does not exists");

        var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User!, policy,
            new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        if (!authorizationResult.Succeeded)
            throw new ForbidException();

        policy.IsPaid = isPaid;
        await dbContext.SaveChangesAsync();

        return mapper.Map<PolicyDto>(policy);
    }

    public async Task<PolicyDto> UpdatePolicy(int id, CreatePolicyDto dto)
    {
        var policy = await dbContext
            .Policies
            .Include(p => p.InsuranceTypes)
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurer)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (policy == null)
            throw new NotFoundException("Policy does not exists");

        var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User!, policy,
            new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        if (!authorizationResult.Succeeded)
            throw new ForbidException();

        //var insurer = await insurerService.UpdateOrCreateIfNotExists(dto.Insurer);

        List<InsuranceType> newTypes = [];
        foreach (var typeId in dto.InsuranceTypeIds)
        {
            var type = await dbContext.InsuranceTypes
                .FirstOrDefaultAsync(t => t.Id == typeId);

            if (type is not null)
                newTypes.Add(type);
        }

        policy.PolicyNumber = dto.PolicyNumber;
        policy.InsuranceCompanyId = dto.InsuranceCompanyId;
        policy.StartDate = dto.StartDate;
        policy.EndDate = dto.EndDate;
        policy.PaymentDate = dto.PaymentDate;
        policy.InsurerId = dto.InsurerId;
        policy.IsPaid = dto.IsPaid;
        policy.Title = dto.Title;
        policy.InsuranceTypes.Clear();
        policy.InsuranceTypes.AddRange(newTypes);

        await dbContext.SaveChangesAsync();

        return mapper.Map<PolicyDto>(policy);
    }

    public async Task<int> CreatePolicy(CreatePolicyDto dto)
    {
        var policy = await dbContext
            .Policies
            .FirstOrDefaultAsync(p => p.PolicyNumber == dto.PolicyNumber);

        if (policy != null)
            throw new AlreadyExistsException("Policy already Exists");

        var creatingUserId = userContextService.GetUserId;

        if (userContextService.User is not null &&
            userContextService.User.IsInRole("User") &&
            userContextService.GetSuperiorId is not null)
        {
            creatingUserId = (int)userContextService.GetSuperiorId;
        }

        //var insurer = await insurerService.GetOrCreateIfNotExists(dto.Insurer);

        var types = await dbContext.InsuranceTypes
            .Where(t => dto.InsuranceTypeIds.Contains(t.Id))
            .ToListAsync();


        var createPolicy = new Policy
        {
            PolicyNumber = dto.PolicyNumber,
            InsuranceCompanyId = dto.InsuranceCompanyId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            PaymentDate = dto.PaymentDate,
            InsurerId = dto.InsurerId,
            InsuranceTypes = types,
            IsPaid = dto.IsPaid,
            Title = dto.Title,
            CreatedById = (int)creatingUserId!,
        };

        dbContext.Policies.Add(createPolicy);
        await dbContext.SaveChangesAsync();

        return createPolicy.Id;
    }

    public async Task<PolicyDto> GetById(int id)
    {
        var policy = await dbContext
            .Policies
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurer)
            .Include(p => p.InsuranceTypes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (policy == null)
            throw new NotFoundException("Insurance Policy not Found");

        var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User!, policy,
            new ResourceOperationRequirement(ResourceOperation.Read)).Result;

        if (!authorizationResult.Succeeded)
            throw new ForbidException();

        return mapper.Map<PolicyDto>(policy);
    }

    public async Task<IEnumerable<InsurerPolicyDto>> GetInsurerPolicies(int id)
    {
        var policies = (await dbContext
            .Policies
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurer)
            .Include(p => p.InsuranceTypes)
            .ToListAsync())
            .Where(p => p.Insurer.Id == id)
            .OrderBy(p => p.EndDate)
            .ToList();

        return mapper.Map<List<InsurerPolicyDto>>(policies);
    }

    private bool GetPredicate(Policy p)
    {
        var user = userContextService.User;
        if (user is not null && user.IsInRole("Admin"))
            return true;

        if (p.CreatedById == userContextService.GetUserId)
            return true;

        var superiorId = userContextService.GetSuperiorId;

        if (superiorId is not null && p.CreatedById == superiorId)
            return true;

        return false;
    }
}
