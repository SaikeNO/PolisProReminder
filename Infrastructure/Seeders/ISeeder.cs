namespace PolisProReminder.Infrastructure.Seeders;

public interface ISeeder
{
    Task Seed();
    Task SeedDeployment();
}