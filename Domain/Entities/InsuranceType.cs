﻿namespace PolisProReminder.Domain.Entities;

public class InsuranceType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string CreatedByUserId { get; set; } = null!;
    public string CreatedByAgentId { get; set; } = null!;
    public List<Policy> Policies { get; set; } = new();
}
