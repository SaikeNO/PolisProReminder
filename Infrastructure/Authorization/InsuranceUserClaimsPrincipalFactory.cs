using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PolisProReminder.Domain.Entities;
using System.Security.Claims;

namespace PolisProReminder.Infrastructure.Authorization;

public class InsuranceUserClaimsPrincipalFactory(UserManager<User> userManager,
    RoleManager<UserRole> roleManager,
    IOptions<IdentityOptions> options)
        : UserClaimsPrincipalFactory<User, UserRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var claims = await GenerateClaimsAsync(user);

        claims.AddClaim(new Claim("AgentId", user.AgentId.ToString()));

        return new ClaimsPrincipal(claims);
    }
}
