namespace PolisProReminder.API.Requests;

public class CreatePolicyReq
{
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public string InsuranceCompanyId { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string PaymentDate { get; set; }
    public bool IsPaid { get; set; }
    public string InsurerId { get; set; }
    public List<string> InsuranceTypeIds { get; set; } = [];
}
