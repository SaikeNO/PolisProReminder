﻿using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Commands.UpdatePolicyCommand;

public class UpdatePolicyCommandHandler(IUserContext userContext,
    IPoliciesRepository policiesRepository, 
    IInsuranceTypesRepository insuranceTypesRepository) : IRequestHandler<UpdatePolicyCommand>
{
    public async Task Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");
     
        var policy = await policiesRepository.GetById(currentUser.AgentId, request.Id);

        _ = policy ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        if (await policiesRepository.GetByNumber(currentUser.AgentId, request.PolicyNumber) != null)
            throw new AlreadyExistsException("Polisa o podanym numerze już istnieje");

        List<InsuranceType> newTypes = [];
        foreach (var typeId in request.InsuranceTypeIds)
        {
            var type = await insuranceTypesRepository.GetById(typeId);

            if (type is not null)
                newTypes.Add(type);
        }

        policy.PolicyNumber = request.PolicyNumber;
        policy.InsuranceCompanyId = request.InsuranceCompanyId;
        policy.StartDate = request.StartDate;
        policy.EndDate = request.EndDate;
        policy.PaymentDate = request.PaymentDate;
        policy.InsurerId = request.InsurerId;
        policy.IsPaid = request.IsPaid;
        policy.Title = request.Title;
        policy.InsuranceTypes.Clear();
        policy.InsuranceTypes.AddRange(newTypes);

        await policiesRepository.SaveChanges();
    }
}
