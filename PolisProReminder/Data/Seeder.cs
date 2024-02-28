using PolisProReminder.Entities;

namespace PolisProReminder.Data
{
    public static class Seeder
    {
        public static void Seed(InsuranceDbContext dbContext)
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

            var roleUser = new Role() { Name = "User" };
            var roleAdmin = new Role() { Name = "Admin" };
            
            dbContext.Roles.AddRange(new List<Role>() { roleUser, roleAdmin });
            dbContext.SaveChanges();
            
            var user = new User()
            {
                Name = "test",
                FirstName = "Mateusz",
                LastName = "Lengiewicz",
                Email = "adres@email.com",
                Role = roleAdmin,
                RoleId = roleAdmin.Id,
                Password = "password",
            };
            
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

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

            dbContext.Insurers.AddRange(new List<Insurer> { insurer1, insurer2, insurer3 });
            dbContext.SaveChanges();

            var insuranceCompany1 = new InsuranceCompany { Name = "Ergo Hestia" };
            var insuranceCompany2 = new InsuranceCompany { Name = "PZU" };

            dbContext.InsuranceCompanies.AddRange(new List<InsuranceCompany> { insuranceCompany1, insuranceCompany2 });
            dbContext.SaveChanges();


            var insuranceType1 = new InsuranceType { Name = "OC" };
            var insuranceType2 = new InsuranceType { Name = "AC" };
            var insuranceType3 = new InsuranceType { Name = "na życie" };

            var policy1 = new Policy
            {
                InsurerId = insurer1.Id,
                InsuranceCompany = insuranceCompany1,
                InsuranceTypes = new List<InsuranceType> { insuranceType1, insuranceType2 },
                StartDate = new DateTime(2024, 1, 1, 0, 0, 0),
                EndDate = new DateTime(2025, 1, 1, 23, 59, 59),
                PaymentDate = new DateTime(2024, 1, 7, 0, 0, 0),
                IsPaid = false,
                PolicyNumber = "NRXY0000000089",
                Title = "Polisa samochodowa BI001FA",
            };

            var policy2 = new Policy
            {
                InsurerId = insurer1.Id,
                InsuranceCompany = insuranceCompany2,
                InsuranceTypes = new List<InsuranceType> { insuranceType1, insuranceType3 },
                StartDate = new DateTime(2023, 9, 12, 0, 0, 0),
                EndDate = new DateTime(2025, 9, 12, 23, 59, 59),
                PaymentDate = new DateTime(2023, 9, 19, 0, 0, 0),
                IsPaid = true,
                PolicyNumber = "AA08691222ABBC",
                Title = "Polisa na życie",
            };

            var policy3 = new Policy
            {
                InsurerId = insurer2.Id,
                InsuranceCompany = insuranceCompany2,
                InsuranceTypes = new List<InsuranceType> { insuranceType1 },
                StartDate = new DateTime(2023, 9, 12, 0, 0, 0),
                EndDate = new DateTime(2025, 9, 12, 23, 59, 59),
                PaymentDate = new DateTime(2023, 9, 19, 0, 0, 0),
                IsPaid = true,
                PolicyNumber = "AA08691222ABBC",
                Title = "Polisa samochodowa BIA011HH",
            };

            dbContext.Policies.AddRange(new List<Policy> { policy1, policy2, policy3 });
            dbContext.SaveChanges();
        }
    }
}
