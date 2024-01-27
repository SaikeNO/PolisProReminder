namespace PolisProReminder.Entities
{
    public class InsurancePolicy
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string PolicyNumber { get; set; } = null!;
        public int InsurerId { get; set; }
        public int InsuranceCompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; } = false;

        public virtual Insurer Insurer { get; set; } = null!;
        public virtual InsuranceCompany InsuranceCompany { get; set; } = null!;
        public virtual List<InsuranceType> InsuranceTypes { get; set; } = new();
    }
}
