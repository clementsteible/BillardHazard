using BillardHazard.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BillardHazard.TimedBackgroundTasks
{
    public class TimedHostedService : BackgroundService
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            using PeriodicTimer timer = new(TimeSpan.FromDays(1));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    DoWork();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Timed Hosted Service is stopping.");
            }
        }

        // Could also be a async method, that can be awaited in ExecuteAsync above
        private void DoWork()
        {
            _logger.LogInformation("Timed Hosted Service running.");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BhContext>();

                ServicesSQL.DeleteGamesAndAssociatedTeams(dbContext);
                _logger.LogInformation("All Games old more than 2 days and their associated Teams are deleted !");
            }
        }
    }
}
