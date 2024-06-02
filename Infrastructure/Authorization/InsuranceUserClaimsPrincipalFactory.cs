using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PolisProReminder.Domain.Entities;
using System.Security.Claims;

namespace PolisProReminder.Infrastructure.Authorization;

public class InsuranceUserClaimsPrincipalFactory(UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options)
        : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);

        if (user.SuperiorId != null)
            id.AddClaim(new Claim("SuperiorId", user.SuperiorId));

        return new ClaimsPrincipal(id);
    }
}
