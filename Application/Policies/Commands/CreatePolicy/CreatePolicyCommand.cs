using MediatR;
using PolisProReminder.Application.Policies.Commands.BasePolicy;

namespace PolisProReminder.Application.Policies.Commands.CreatePolicy;

public class CreatePolicyCommand : BasePolicyCommand, IRequest<Guid> { }
