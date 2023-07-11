using BillardHazard.Services;

namespace BillardHazard
{
    public class PeriodicDeleteGamesTeams : IHostedService
    {
        private readonly ILogger<PeriodicDeleteGamesTeams> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PeriodicDeleteGamesTeams(ILogger<PeriodicDeleteGamesTeams> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ExecuteAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<BhContext>();

                    ServicesSQL.DeleteGamesAndAssociatedTeams(dbContext);
                }

                await Task.Delay(TimeSpan.FromSeconds(20), cancellationToken);
            }
        }
    }
}
