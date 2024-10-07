using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Domain.Entities;
public class User : IdentityUser<Guid>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid Id
    {
        get { return base.Id; }
        set { base.Id = value; }
    }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public Guid AgentId { get; set; }
}

public class UserRole : IdentityRole<Guid>
{
    public UserRole() : base() { }

    public UserRole(string roleName) : base(roleName)
    {
        Name = roleName;
    }
}