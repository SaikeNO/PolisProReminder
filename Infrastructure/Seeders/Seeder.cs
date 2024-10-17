using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Seeders;

internal class Seeder(InsuranceDbContext dbContext, IPasswordHasher<User> passwordHasher) : ISeeder
{
    public async Task SeedDeployment()
    {
        if (dbContext.Database.CanConnect())
        {
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.VehicleBrands.Any())
            {
                var vehiclesBrands = GetVehicleBrands();
                dbContext.VehicleBrands.AddRange(vehiclesBrands);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    public async Task Seed()
    {
        if (dbContext.Database.CanConnect())
        {
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Users.Any())
            {
                var users = GetUsers();
                await dbContext.Users.AddRangeAsync(users);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.UserRoles.Any())
            {
                var userRoles = await GetUserRoles();
                await dbContext.UserRoles.AddRangeAsync(userRoles);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.InsuranceTypes.Any())
            {
                var types = await GetInsuranceTypes();
                dbContext.InsuranceTypes.AddRange(types);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.InsuranceCompanies.Any())
            {
                var companies = await GetInsuranceCompanies();
                dbContext.InsuranceCompanies.AddRange(companies);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.IndividualInsurers.Any())
            {
                var insurers = await GetInsurers();
                dbContext.IndividualInsurers.AddRange(insurers);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.VehicleBrands.Any())
            {
                var vehiclesBrands = GetVehicleBrands();
                dbContext.VehicleBrands.AddRange(vehiclesBrands);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Vehicles.Any())
            {
                var vehicles = await GetVehicles();
                dbContext.Vehicles.AddRange(vehicles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private async Task<IEnumerable<IdentityUserRole<Guid>>> GetUserRoles()
    {
        var roles = await dbContext.Roles.ToListAsync();
        var users = await dbContext.Users.ToListAsync();

        List<IdentityUserRole<Guid>> userRoles = [
            new()
            {
                RoleId = roles.First(r => r.Name == UserRoles.Admin).Id,
                UserId = users.First(u => u.Email == "admin@email.com").Id
            },
            new()
            {
                RoleId = roles.First(r => r.Name == UserRoles.Agent).Id,
                UserId = users.First(u => u.Email == "agent1@email.com").Id
            },
            new()
            {
                RoleId = roles.First(r => r.Name == UserRoles.Agent).Id,
                UserId = users.First(u => u.Email == "agent2@email.com").Id
            },
            new()
            {
                RoleId = roles.First(r => r.Name == UserRoles.User).Id,
                UserId = users.First(u => u.Email == "user1@email.com").Id
            },
            new()
            {
                RoleId = roles.First(r => r.Name == UserRoles.User).Id,
                UserId = users.First(u => u.Email == "user2@email.com").Id
            }
            ];

        return userRoles;
    }

    private IEnumerable<User> GetUsers()
    {
        var admin = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Mateusz",
            LastName = "Lengiewicz",
            Email = "admin@email.com",
            NormalizedEmail = "admin@email.com".ToUpper(),
            UserName = "admin",
            SecurityStamp = Guid.NewGuid().ToString(),
            NormalizedUserName = "admin".ToUpper(),
        };

        admin.AgentId = admin.Id;
        admin.PasswordHash = passwordHasher.HashPassword(admin, "password");

        var agent = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Agent1",
            LastName = "Lengiewicz",
            Email = "agent1@email.com",
            NormalizedEmail = "agent1@email.com".ToUpper(),
            UserName = "agent1",
            SecurityStamp = Guid.NewGuid().ToString(),
            NormalizedUserName = "agent1".ToUpper(),
        };

        agent.AgentId = agent.Id;
        agent.PasswordHash = passwordHasher.HashPassword(agent, "password");

        var agent2 = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Agent2",
            LastName = "Lengiewicz",
            Email = "agent2@email.com",
            NormalizedEmail = "agent2@email.com".ToUpper(),
            UserName = "agent2",
            SecurityStamp = Guid.NewGuid().ToString(),
            NormalizedUserName = "agent2".ToUpper(),
        };

        agent2.AgentId = agent2.Id;
        agent2.PasswordHash = passwordHasher.HashPassword(agent2, "password");

        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Użytkownik",
            LastName = "Lengiewicz",
            Email = "user1@email.com",
            NormalizedEmail = "user1@email.com".ToUpper(),
            AgentId = agent.Id,
            UserName = "user1",
            SecurityStamp = Guid.NewGuid().ToString(),
            NormalizedUserName = "user1".ToUpper(),
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "password");

        var user2 = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Użytkownik",
            LastName = "Lengiewicz",
            Email = "user2@email.com",
            NormalizedEmail = "user2@email.com".ToUpper(),
            AgentId = agent2.Id,
            UserName = "user2",
            SecurityStamp = Guid.NewGuid().ToString(),
            NormalizedUserName = "user2".ToUpper(),
        };

        user2.PasswordHash = passwordHasher.HashPassword(user2, "password");

        return [admin, agent, agent2, user, user2];
    }

    private IEnumerable<UserRole> GetRoles()
    {
        List<UserRole> roles = [
            new(UserRoles.User)
            {
                Id = Guid.NewGuid(),
                NormalizedName = UserRoles.User.ToUpper(),
            },
            new(UserRoles.Agent)
            {
                Id = Guid.NewGuid(),
                NormalizedName = UserRoles.Agent.ToUpper(),
            },
            new(UserRoles.Admin)
            {
                Id = Guid.NewGuid(),
                NormalizedName = UserRoles.Admin.ToUpper(),
            }
            ];

        return roles;
    }

    private async Task<IEnumerable<InsuranceType>> GetInsuranceTypes()
    {
        var users = await dbContext.Users.ToListAsync();
        List<InsuranceType> types = [
            new()
            {
                Name = "OC",
                CreatedByAgentId = users.First(u => u.Email == "agent2@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent2@email.com").Id
            },
            new()
            {
                Name = "OC",
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id
            },
            new()
            {
                Name = "AC",
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id
            },
            new()
            {
                Name = "Na życie",
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "user1@email.com").Id
            }
            ];

        return types;
    }

    private async Task<IEnumerable<InsuranceCompany>> GetInsuranceCompanies()
    {
        var users = await dbContext.Users.ToListAsync();
        List<InsuranceCompany> companies = [
            new()
            {
                Name = "Sopockie Towarzystwo Ubezpieczeń ERGO Hestia",
                ShortName = "ERGO Hestia",
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id
            },
            new()
            {
                Name = "Powszechny Zakład Ubezpieczeń",
                ShortName = "PZU",
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id
            },
            ];

        return companies;
    }

    private async Task<IEnumerable<IndividualInsurer>> GetInsurers()
    {
        var users = await dbContext.Users.ToListAsync();

        List<IndividualInsurer> insurers = [
            new ("Mateusz", "Lengiewicz", "44051401458", "800666209", "mat.len@test.com", "16-130", "Sitkowo", "Sitkowo 22")
            {
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id
            },

            new ("Jan", "Kowalski", "77020201233", "098666222", "janek.szparek@test.com", "16-130", "Sitkowo", "Sitkowo 22")
            {
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id
            },

            new ("Janusz", "Wariat", "88101006122", "111222333", "januszex@test.com", "16-130", "Sitkowo", "Sitkowo 22")
            {
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id
            }
            ];

        return insurers;
    }

    private IEnumerable<VehicleBrand> GetVehicleBrands()
    {
        List<VehicleBrand> vehicleBrands = [
            new(){ Name = "Audi" },
            new(){ Name = "BMW" },
            new(){ Name = "Citroen" },
            new(){ Name = "Dacia" },
            new(){ Name = "Fiat" },
            new(){ Name = "Ford" },
            new(){ Name = "Hyundai" },
            new(){ Name = "Kia" },
            new(){ Name = "Mercedes" },
            new(){ Name = "Nissan" },
            new(){ Name = "Open" },
            new(){ Name = "Peugeot" },
            new(){ Name = "Renault" },
            new(){ Name = "SEAT" },
            new(){ Name = "Skoda" },
            new(){ Name = "Toyota" },
            new(){ Name = "Volkswagen" },
            new(){ Name = "Volvo" },
            new(){ Name = "Abarth" },
            new(){ Name = "Alfa Romeo" },
            new(){ Name = "Aplina" },
            new(){ Name = "Apline" },
            new(){ Name = "Aston Martin" },
            new(){ Name = "BAIC" },
            new(){ Name = "Bentley" },
            new(){ Name = "BYD" },
            new(){ Name = "Cupra" },
            new(){ Name = "Dodge" },
            new(){ Name = "DS" },
            new(){ Name = "Ferrari" },
            new(){ Name = "Fuso" },
            new(){ Name = "Honda" },
            new(){ Name = "Hongqi" },
            new(){ Name = "Ineos" },
            new(){ Name = "Isuzu" },
            new(){ Name = "Iveco" },
            new(){ Name = "JAC" },
            new(){ Name = "Jaguar" },
            new(){ Name = "Jeep" },
            new(){ Name = "Kawasaki" },
            new(){ Name = "Lamborghini" },
            new(){ Name = "Land Rover" },
            new(){ Name = "LEVC" },
            new(){ Name = "Lexus" },
            new(){ Name = "MAN" },
            new(){ Name = "Maserati" },
            new(){ Name = "Maxus" },
            new(){ Name = "Mazda" },
            new(){ Name = "McLaren" },
            new(){ Name = "MG" },
            new(){ Name = "MINI" },
            new(){ Name = "Mitsubishi" },
            new(){ Name = "Nio" },
            new(){ Name = "Piaggio" },
            new(){ Name = "Porsche" },
            new(){ Name = "RAM" },
            new(){ Name = "Renault Trucks" },
            new(){ Name = "Rolls-Royce" },
            new(){ Name = "Seres" },
            new(){ Name = "Skywell" },
            new(){ Name = "SsangYong" },
            new(){ Name = "Subaru" },
            new(){ Name = "Suzuki" },
            new(){ Name = "Tesla" },
            new(){ Name = "Andoria" },
            new(){ Name = "Autobianchi" },
            new(){ Name = "Cadillac" },
            new(){ Name = "Caterham" },
            new(){ Name = "Chevrolet" },
            new(){ Name = "Chrysler" },
            new(){ Name = "Corvette" },
            new(){ Name = "Daewoo" },
            new(){ Name = "Daewoo Motor" },
            new(){ Name = "Damis" },
            new(){ Name = "DMC" },
            new(){ Name = "FSC" },
            new(){ Name = "FSO" },
            new(){ Name = "GAZ" },
            new(){ Name = "GAZ-AVC" },
            new(){ Name = "Hummer" },
            new(){ Name = "Infiniti" },
            new(){ Name = "Intrall" },
            new(){ Name = "Izera" },
            new(){ Name = "Lada" },
            new(){ Name = "Lancia" },
            new(){ Name = "LDV" },
            new(){ Name = "Ligier" },
            new(){ Name = "Lotus" },
            new(){ Name = "LTI" },
            new(){ Name = "Maybach" },
            new(){ Name = "Polski Fiat" },
            new(){ Name = "Rover" },
            new(){ Name = "Saab" },
            new(){ Name = "Smart" },
            new(){ Name = "Tata" },
            new(){ Name = "WSK Mielec" },
            new(){ Name = "ZD" },
            ];

        return vehicleBrands;
    }
    private async Task<IEnumerable<Vehicle>> GetVehicles()
    {
        var users = await dbContext.Users.ToListAsync();
        var insurers = await dbContext.IndividualInsurers.ToListAsync();
        var vehicleBrands = await dbContext.VehicleBrands.ToListAsync();

        List<Vehicle> vehicles = [
                new(){
                    CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                    CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id,
                    FirstRegistrationDate = DateOnly.FromDateTime(DateTime.Now),
                    RegistrationNumber = "BI998FM",
                    Insurers = [insurers.First(i => i.Pesel == "44051401458")],
                    VIN = "4Y1SL65848Z411439",
                    Name = "Seat Leon 1.5 TSI",
                    VehicleBrand = vehicleBrands.First(b => b.Name == "SEAT")
                },
                new(){
                    CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                    CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id,
                    FirstRegistrationDate = DateOnly.FromDateTime(DateTime.Now),
                    RegistrationNumber = "BSK23UE",
                    Insurers = [insurers.First(i => i.Pesel == "44051401458")],
                    VIN = "1J4GK38K66W221344",
                    Name = "VW Touran 1.4 MPI",
                    VehicleBrand = vehicleBrands.First(b => b.Name == "Volkswagen")
                }
            ];

        return vehicles;
    }
}
