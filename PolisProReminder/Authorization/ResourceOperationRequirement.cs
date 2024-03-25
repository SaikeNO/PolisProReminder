using Microsoft.AspNetCore.Authorization;

namespace PolisProReminder.Authorization;

public enum ResourceOperation
{
    Create,
    Read,
    Update,
    Delete
}
public class ResourceOperationRequirement(ResourceOperation resourceOperation) : IAuthorizationRequirement
{
    public ResourceOperation ResourceOperation { get; } = resourceOperation;
}
