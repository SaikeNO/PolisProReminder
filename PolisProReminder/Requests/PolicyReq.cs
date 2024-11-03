namespace PolisProReminder.API.Requests;

public class PolicyReq
{
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public string InsuranceCompanyId { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string? PaymentDate { get; set; } = string.Empty;
    public bool IsPaid { get; set; }
    public string Note { get; set; } = string.Empty;
    public IEnumerable<string> InsurerIds { get; set; } = [];
    public IEnumerable<string> InsuranceTypeIds { get; set; } = [];
    public IEnumerable<string> AttachmentIds { get; set; } = [];
}
