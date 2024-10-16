﻿using Microsoft.AspNetCore.Http;

namespace PolisProReminder.Application.Policies.Commands.BasePolicy;

public class BasePolicyCommand
{
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public Guid InsuranceCompanyId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public bool IsPaid { get; set; }
    public string? Note { get; set; }
    public IEnumerable<Guid> InsuranceTypeIds { get; set; } = [];
    public IEnumerable<Guid> InsurerIds { get; set; } = [];
    public IEnumerable<IFormFile> Attachments { get; set; } = [];
}
