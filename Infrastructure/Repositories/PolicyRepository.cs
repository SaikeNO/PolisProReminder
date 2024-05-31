using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class PolicyRepository(InsuranceDbContext dbContext) : IPolicyRepository
{

}
