﻿using Microsoft.AspNetCore.Identity;

namespace PolisProReminder.Domain.Entities;
public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public Guid AgentId { get; set; }
}