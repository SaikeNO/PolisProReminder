using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Schedulers;

public class ArchivePoliciesService(IServiceScopeFactory scopeFactory, ILogger logger) : BackgroudService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var now = DateTime.Now;
                var midnight = DateTime.Today.AddDays(1);
                var timeUntilMidnight = midnight - now;

                await Task.Delay(timeUntilMidnight, stoppingToken);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<InsuranceDbContext>();

                    var records = await dbContext.Policies
                        .Where(p => !p.IsArchived && !p.IsDeleted)
                        .ToListAsync(stoppingToken);

                    foreach (var record in records)
                    {
                        if (record.EndDate < DateTime.Now)
                        {
                            record.IsArchived = true;
                        }
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);

                    string message = $"Zarchiwizowano polisy: {string.Join(", ", records.Select(record => record.Id).ToArray())}";
                    logger.LogInformation(message);

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Wystąpił błąd podczas archiwizowania polis");
            }
        }
    }
}