namespace PolisProReminder.Entities
{
    public class InsuranceType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Policy> Policies { get; set; } = new();
    }
}
