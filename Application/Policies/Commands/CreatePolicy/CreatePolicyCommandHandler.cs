using MediatR;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Commands.CreatePolicy;

public class CreatePolicyCommandHandler(IPoliciesRepository policiesRepository,
    IInsuranceTypesRepository insuranceTypesRepository) : IRequestHandler<CreatePolicyCommand, Guid>
{
    public async Task<Guid> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
    {
        var policy = await policiesRepository.GetByNumber(request.PolicyNumber);

        if (policy != null)
            throw new AlreadyExistsException("Polisa o podanym numerze już istnieje");

        var types = await insuranceTypesRepository.GetManyByIds(request.InsuranceTypeIds);

        var createPolicy = new Policy
        {
            PolicyNumber = request.PolicyNumber,
            InsuranceCompanyId = request.InsuranceCompanyId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PaymentDate = request.PaymentDate,
            InsurerId = request.InsurerId,
            InsuranceTypes = types.ToList(),
            IsPaid = request.IsPaid,
            Title = request.Title,
        };

        var id = await policiesRepository.Create(createPolicy);
        return id;
    }
}
