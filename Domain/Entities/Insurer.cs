﻿namespace PolisProReminder.Domain.Entities;
public class Insurer
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Pesel { get; set; }
    public virtual List<Policy> Policies { get; set; } = null!;
}
