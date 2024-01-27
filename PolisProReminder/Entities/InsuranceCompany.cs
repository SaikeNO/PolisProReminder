namespace PolisProReminder.Entities
{
    public class InsuranceCompany
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual List<Policy> Policies { get; set; } = new();
    }
}
