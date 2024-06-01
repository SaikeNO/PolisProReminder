using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Seeders;

internal class Seeder(InsuranceDbContext dbContext, IPasswordHasher<User> passwordHasher) : ISeeder
{
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

            if (!dbContext.InsuranceTypes.Any())
            {
                var types = GetInsuranceTypes();
                dbContext.InsuranceTypes.AddRange(types);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.InsuranceCompanies.Any())
            {
                var companies = GetInsuranceCompanies();
                dbContext.InsuranceCompanies.AddRange(companies);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Insurers.Any())
            {
                var insurers = GetInsurers();
                dbContext.Insurers.AddRange(insurers);
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
        }
    }

    private async Task<IEnumerable<IdentityUserRole<string>>> GetUserRoles()
    {
        var roles = await dbContext.Roles.ToListAsync();
        var users = await dbContext.Users.ToListAsync();

        var admin = roles.FirstOrDefault(r => r.NormalizedName == UserRoles.Admin);

        List<IdentityUserRole<string>> userRoles = [
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
            FirstName = "Mateusz",
            LastName = "Lengiewicz",
            Email = "admin@email.com",
            NormalizedEmail = "admin@email.com".ToUpper(),
        };

        admin.PasswordHash = passwordHasher.HashPassword(admin, "password");

        var agent = new User()
        {
            FirstName = "Agent1",
            LastName = "Lengiewicz",
            Email = "agent1@email.com",
            NormalizedEmail = "agent1@email.com".ToUpper(),
        };

        agent.PasswordHash = passwordHasher.HashPassword(agent, "password");


        var agent2 = new User()
        {
            FirstName = "Agent2",
            LastName = "Lengiewicz",
            Email = "agent2@email.com",
            NormalizedEmail = "agent2@email.com".ToUpper(),
        };

        agent2.PasswordHash = passwordHasher.HashPassword(agent2, "password");

        var user = new User()
        {
            FirstName = "Użytkownik",
            LastName = "Lengiewicz",
            Email = "user1@email.com",
            NormalizedEmail = "user1@email.com".ToUpper(),
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "password");

        var user2 = new User()
        {
            FirstName = "Użytkownik",
            LastName = "Lengiewicz",
            Email = "user2@email.com",
            NormalizedEmail = "user2@email.com".ToUpper(),
        };

        user2.PasswordHash = passwordHasher.HashPassword(user2, "password");

        return [admin, agent, agent2, user, user2];
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = [
            new(UserRoles.User)
            {
                NormalizedName = UserRoles.User.ToUpper(),
            },
            new(UserRoles.Agent)
            {
                NormalizedName = UserRoles.Agent.ToUpper(),
            },
            new(UserRoles.Admin)
            {
                NormalizedName = UserRoles.Admin.ToUpper(),
            }
            ];

        return roles;
    }

    private IEnumerable<InsuranceType> GetInsuranceTypes()
    {
        List<InsuranceType> types = [
            new(){ Name = "OC" },
            new(){ Name = "AC" },
            new(){ Name = "Na życie" }
            ];

        return types;
    }

    private IEnumerable<InsuranceCompany> GetInsuranceCompanies()
    {
        List<InsuranceCompany> companies = [
            new()
            {
                Name = "Sopockie Towarzystwo Ubezpieczeń ERGO Hestia",
                ShortName = "ERGO Hestia"
            },
            new()
            {
                Name = "Powszechny Zakład Ubezpieczeń",
                ShortName = "PZU"
            },
            ];

        return companies;
    }

    private IEnumerable<Insurer> GetInsurers()
    {
        List<Insurer> insurers = [
            new ()
            {
                FirstName = "Mateusz",
                LastName = "Lengiewicz",
                Pesel = "44051401458",
                Email = "mat.len@test.com",
                PhoneNumber = "800666209",
            },

            new ()
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Pesel = "77020201233",
                Email = "janek.szparek@test.com",
                PhoneNumber = "098666222",
            },

            new ()
            {
                FirstName = "Janusz",
                LastName = "Wariat",
                Pesel = "88101006122",
                Email = "januszex@test.com",
                PhoneNumber = "111222333",
            }
            ];

        return insurers;
    }
}
