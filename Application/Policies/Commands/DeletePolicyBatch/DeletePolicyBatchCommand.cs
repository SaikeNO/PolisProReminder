using MediatR;

namespace PolisProReminder.Application.Policies.Commands.DeletePolicyBatch;

public class DeletePolicyBatchCommand : IRequest
{
    public IEnumerable<Guid> Ids { get; set; } = [];
}

