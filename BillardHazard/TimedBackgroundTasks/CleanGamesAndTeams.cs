using BillardHazard.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BillardHazard.TimedBackgroundTasks
{
    /// <summary>
    /// Erase periodically and automatically the games older than a variable date and their Teams associated
    /// </summary>
    public class CleanGamesAndTeams : BackgroundService
    {
        private readonly ILogger<CleanGamesAndTeams> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        /// <summary>
        // All the games older than this variable, and their Teams associated, will be erase 
        /// </summary>
        private readonly int DAYS_IN_THE_PAST = 2;
        /// <summary>
        // Cleaning frequency 
        /// </summary>
        private readonly PeriodicTimer CLEANING_PERIODICITY = new(TimeSpan.FromDays(1));

        public CleanGamesAndTeams(ILogger<CleanGamesAndTeams> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            using PeriodicTimer timer = CLEANING_PERIODICITY;

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
                
                ServicesSQL.DeleteOldGames(dbContext, DAYS_IN_THE_PAST);
                _logger.LogInformation($"All Games old more than {DAYS_IN_THE_PAST} days and their associated Teams are deleted !");
            }
        }
    }
}
