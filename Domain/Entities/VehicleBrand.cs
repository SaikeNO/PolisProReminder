namespace PolisProReminder.Domain.Entities;

public class VehicleBrand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual List<Vehicle> Vehicles { get; set; } = [];
}
