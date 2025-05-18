using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Domain.Entities;
public class User : IdentityUser<Guid>, ISoftDeletable
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
    public bool IsDeleted { get; set; } = false;

    public Guid? AgentId { get; set; }
    public virtual User? Agent { get; set; } = null!;

    public ICollection<User> Assistants { get; set; } = [];
}

public class UserRole : IdentityRole<Guid>
{
    public UserRole() : base() { }

    public UserRole(string roleName) : base(roleName)
    {
        Name = roleName;
    }
}