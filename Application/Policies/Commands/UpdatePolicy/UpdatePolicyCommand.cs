﻿using MediatR;
using Microsoft.AspNetCore.Http;

namespace PolisProReminder.Application.Policies.Commands.UpdatePolicy;

public class UpdatePolicyCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public Guid InsuranceCompanyId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public bool IsPaid { get; set; }
    public string? Note { get; set; }
    public Guid InsurerId { get; set; }
    public List<Guid> InsuranceTypeIds { get; set; } = [];
    public IEnumerable<IFormFile> Attachments { get; set; } = [];
}
