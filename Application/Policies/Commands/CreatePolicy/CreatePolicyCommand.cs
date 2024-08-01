using MediatR;
using Microsoft.AspNetCore.Http;

namespace PolisProReminder.Application.Policies.Commands.CreatePolicy;

public class CreatePolicyCommand : IRequest<Guid>
{
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public Guid InsuranceCompanyId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly PaymentDate { get; set; }
    public bool IsPaid { get; set; }
    public Guid InsurerId { get; set; }
    public List<Guid> InsuranceTypeIds { get; set; } = [];
    public IEnumerable<IFormFile> Attachments { get; set; } = [];

}
