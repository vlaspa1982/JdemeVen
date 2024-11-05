

using JdemeVen.Server.Services;

namespace JdemeVen.Server.HostedServices
{
    public class RssBackgroundService : BackgroundService
    {
        private readonly ILogger<RssBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public RssBackgroundService(ILogger<RssBackgroundService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    try
                    {
                        // Získání instance RssService ze scope
                        var rssService = scope.ServiceProvider.GetRequiredService<RssService>();

                        // Načtení RSS dokumentu
                        var rssDocument = await rssService.FetchRssAsync("https://www.minikino.cz/rss");

                        _logger.LogInformation("RSS feed načten: {time}", DateTimeOffset.Now);

                        // Zpracování a uložení událostí
                        var events = await rssService.ParseRssToEvents(rssDocument);
                        await rssService.SaveEventsAsync(events);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Chyba při načítání RSS feedu");
                    }
                }

                // Nastavte interval opakování
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
