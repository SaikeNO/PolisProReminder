namespace PolisProReminder.Entities
{
    public class InsuranceCompany
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual List<InsurancePolicy> InsurancePolicies { get; set; } = new();
    }
}
