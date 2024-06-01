using Microsoft.AspNetCore.Identity;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Seeders;

internal class Seeder(InsuranceDbContext dbContext, IPasswordHasher<User> passwordHasher) : ISeeder
{
    public async Task Seed()
    {
        if (dbContext.InsuranceCompanies.Any()
            && dbContext.Insurers.Any()
            && dbContext.InsuranceTypes.Any()
            && dbContext.Policies.Any()
            && dbContext.Users.Any()
            && dbContext.Roles.Any())
        {
            return;
        }

        var roleUser = new Role() { Name = "USER" };
        var roleAgent = new Role() { Name = "AGENT" };
        var roleAdmin = new Role() { Name = "ADMIN" };


        await dbContext.Roles.AddRangeAsync([roleUser, roleAgent, roleAdmin]);
        await dbContext.SaveChangesAsync();

        var admin = new User()
        {
            Name = "admin",
            FirstName = "Mateusz",
            LastName = "Lengiewicz",
            Email = "adres@email.com",
            Role = roleAdmin,
            RoleId = roleAdmin.Id,
        };

        admin.PasswordHash = passwordHasher.HashPassword(admin, "password");

        var agent = new User()
        {
            Name = "agent",
            FirstName = "Agent1",
            LastName = "Lengiewicz",
            Email = "adres@email.com",
            Role = roleAgent,
            RoleId = roleAgent.Id,
        };

        agent.PasswordHash = passwordHasher.HashPassword(agent, "password");

        var agent2 = new User()
        {
            Name = "agent2",
            FirstName = "Agent1",
            LastName = "Lengiewicz",
            Email = "adres@email.com",
            Role = roleAgent,
            RoleId = roleAgent.Id,
        };

        agent2.PasswordHash = passwordHasher.HashPassword(agent2, "password");

        var user = new User()
        {
            Name = "user",
            FirstName = "Użytkownik",
            LastName = "Lengiewicz",
            Email = "adres@email.com",
            Role = roleUser,
            RoleId = roleUser.Id,
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "password");

        var user2 = new User()
        {
            Name = "user2",
            FirstName = "Użytkownik",
            LastName = "Lengiewicz",
            Email = "adres@email.com",
            Role = roleUser,
            RoleId = roleUser.Id,
        };

        user2.PasswordHash = passwordHasher.HashPassword(user2, "password");

        await dbContext.Users.AddRangeAsync([user, user2, agent, agent2, admin]);
        await dbContext.SaveChangesAsync();

        var insurer1 = new Insurer()
        {
            FirstName = "Mateusz",
            LastName = "Lengiewicz",
            Pesel = "44051401458",
            Email = "mat.len@test.com",
            PhoneNumber = "800666209",
        };

        var insurer2 = new Insurer()
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Pesel = "77020201233",
            Email = "janek.szparek@test.com",
            PhoneNumber = "098666222",
        };

        var insurer3 = new Insurer()
        {
            FirstName = "Janusz",
            LastName = "Wariat",
            Pesel = "88101006122",
            Email = "januszex@test.com",
            PhoneNumber = "111222333",
        };

        await dbContext.Insurers.AddRangeAsync([insurer1, insurer2, insurer3]);
        await dbContext.SaveChangesAsync();

        var insuranceCompany1 = new InsuranceCompany
        {
            Name = "Sopockie Towarzystwo Ubezpieczeń ERGO Hestia",
            ShortName = "ERGO Hestia"
        };
        var insuranceCompany2 = new InsuranceCompany
        {
            Name = "Powszechny Zakład Ubezpieczeń",
            ShortName = "PZU"
        };

        await dbContext.InsuranceCompanies.AddRangeAsync([insuranceCompany1, insuranceCompany2]);
        await dbContext.SaveChangesAsync();


        var insuranceType1 = new InsuranceType { Name = "OC" };
        var insuranceType2 = new InsuranceType { Name = "AC" };
        var insuranceType3 = new InsuranceType { Name = "na życie" };

        var policy1 = new Policy
        {
            InsurerId = insurer1.Id,
            InsuranceCompany = insuranceCompany1,
            InsuranceTypes = [insuranceType1, insuranceType2],
            StartDate = new DateTime(2024, 1, 1, 0, 0, 0),
            EndDate = new DateTime(2025, 1, 1, 23, 59, 59),
            PaymentDate = new DateTime(2024, 1, 7, 0, 0, 0),
            IsPaid = false,
            PolicyNumber = "NRXY0000000089",
            Title = "Polisa samochodowa BI001FA",
            CreatedById = agent.Id,
        };

        var policy2 = new Policy
        {
            InsurerId = insurer1.Id,
            InsuranceCompany = insuranceCompany2,
            InsuranceTypes = [insuranceType1, insuranceType3],
            StartDate = new DateTime(2023, 9, 12, 0, 0, 0),
            EndDate = new DateTime(2025, 9, 12, 23, 59, 59),
            PaymentDate = new DateTime(2023, 9, 19, 0, 0, 0),
            IsPaid = true,
            PolicyNumber = "AA08691222ABBC",
            Title = "Polisa na życie",
            CreatedById = agent.Id,

        };

        var policy3 = new Policy
        {
            InsurerId = insurer2.Id,
            InsuranceCompany = insuranceCompany2,
            InsuranceTypes = [insuranceType1],
            StartDate = new DateTime(2023, 9, 12, 0, 0, 0),
            EndDate = new DateTime(2025, 9, 12, 23, 59, 59),
            PaymentDate = new DateTime(2023, 9, 19, 0, 0, 0),
            IsPaid = true,
            PolicyNumber = "AA08691222ABBC",
            Title = "Polisa samochodowa BIA011HH",
            CreatedById = agent2.Id,

        };

        await dbContext.Policies.AddRangeAsync([policy1, policy2, policy3]);
        await dbContext.SaveChangesAsync();
    }
}
