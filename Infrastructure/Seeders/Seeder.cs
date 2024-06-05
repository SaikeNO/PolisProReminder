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

            if (!dbContext.Insurers.Any())
            {
                var insurers = await GetInsurers();
                dbContext.Insurers.AddRange(insurers);
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
            UserName = "admin",
            NormalizedUserName = "admin".ToUpper(),
            AgentId = "1",
        };

        admin.PasswordHash = passwordHasher.HashPassword(admin, "password");

        var agent = new User()
        {
            FirstName = "Agent1",
            LastName = "Lengiewicz",
            Email = "agent1@email.com",
            NormalizedEmail = "agent1@email.com".ToUpper(),
            UserName = "agent1",
            NormalizedUserName = "agent1".ToUpper(),
        };

        agent.AgentId = agent.Id;
        agent.PasswordHash = passwordHasher.HashPassword(agent, "password");

        var agent2 = new User()
        {
            FirstName = "Agent2",
            LastName = "Lengiewicz",
            Email = "agent2@email.com",
            NormalizedEmail = "agent2@email.com".ToUpper(),
            UserName = "agent2",
            NormalizedUserName = "agent2".ToUpper(),
        };

        agent2.AgentId = agent2.Id;
        agent2.PasswordHash = passwordHasher.HashPassword(agent2, "password");

        var user = new User()
        {
            FirstName = "Użytkownik",
            LastName = "Lengiewicz",
            Email = "user1@email.com",
            NormalizedEmail = "user1@email.com".ToUpper(),
            AgentId = agent.Id,
            UserName = "user1",
            NormalizedUserName = "user1".ToUpper(),
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "password");

        var user2 = new User()
        {
            FirstName = "Użytkownik",
            LastName = "Lengiewicz",
            Email = "user2@email.com",
            NormalizedEmail = "user2@email.com".ToUpper(),
            AgentId = agent2.Id,
            UserName = "user2",
            NormalizedUserName = "user2".ToUpper(),
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

    private async Task<IEnumerable<Insurer>> GetInsurers()
    {
        var users = await dbContext.Users.ToListAsync();

        List<Insurer> insurers = [
            new ()
            {
                FirstName = "Mateusz",
                LastName = "Lengiewicz",
                Pesel = "44051401458",
                Email = "mat.len@test.com",
                PhoneNumber = "800666209",
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id
            },

            new ()
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Pesel = "77020201233",
                Email = "janek.szparek@test.com",
                PhoneNumber = "098666222",
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id
            },

            new ()
            {
                FirstName = "Janusz",
                LastName = "Wariat",
                Pesel = "88101006122",
                Email = "januszex@test.com",
                PhoneNumber = "111222333",
                CreatedByAgentId = users.First(u => u.Email == "agent1@email.com").Id,
                CreatedByUserId = users.First(u => u.Email == "agent1@email.com").Id
            }
            ];

        return insurers;
    }
}
