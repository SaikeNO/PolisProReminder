namespace PolisProReminder.Domain.Entities;

public class VehicleBrand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual IEnumerable<Vehicle> Vehicles { get; set; } = null!;
}
