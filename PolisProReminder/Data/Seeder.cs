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
                && dbContext.InsurancePolicies.Any())
            {
                return;
            }


            var insurer1 = new Insurer()
            {
                FirstName = "Mateusz",
                LastName = "Lengiewicz",
                Pesel = "12345678901",
                Email = "mat.len@test.com",
                PhoneNumber = "800666209",
            };

            var insurer2 = new Insurer()
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Pesel = "098765432111",
                Email = "janek.szparek@test.com",
                PhoneNumber = "098666222",
            };

            dbContext.Insurers.AddRange(new List<Insurer> { insurer1, insurer2 });
            dbContext.SaveChanges();

            var insuranceCompany1 = new InsuranceCompany { Name = "Ergo Hestia" };
            var insuranceCompany2 = new InsuranceCompany { Name = "PZU" };

            dbContext.InsuranceCompanies.AddRange(new List<InsuranceCompany> { insuranceCompany1, insuranceCompany2 });
            dbContext.SaveChanges();


            var insuranceType1 = new InsuranceType { Name = "OC" };
            var insuranceType2 = new InsuranceType { Name = "AC" };
            var insuranceType3 = new InsuranceType { Name = "na życie" };

            var policy1 = new InsurancePolicy
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

            var policy2 = new InsurancePolicy
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

            var policy3 = new InsurancePolicy
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

            dbContext.InsurancePolicies.AddRange(new List<InsurancePolicy> { policy1, policy2, policy3 });
            dbContext.SaveChanges();
        }
    }
}
