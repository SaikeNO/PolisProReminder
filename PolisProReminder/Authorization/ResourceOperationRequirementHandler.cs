using Microsoft.AspNetCore.Authorization;
using PolisProReminder.Entities;
using System.Security.Claims;

namespace PolisProReminder.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Policy>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Policy policy)
        {
            if (context.User.IsInRole("Admin")) // Admin
            {
                context.Succeed(requirement);
            }

            var id = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value); // Agent
            if(policy.CreatedById == id)
            {
                context.Succeed(requirement);
            }

            var superiorId = int.Parse(context.User.FindFirst(c => c.Type == "SuperiorId").Value); // User
            if (policy.CreatedById == superiorId && (
                requirement.ResourceOperation == ResourceOperation.Read || 
                requirement.ResourceOperation == ResourceOperation.Update
                ))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;


        }
    }
}
