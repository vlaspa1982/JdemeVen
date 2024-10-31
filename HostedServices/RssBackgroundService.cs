namespace backend.HostedServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using backend.Services;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    public class RssBackgroundService : BackgroundService
    {
        private readonly RssService _rssService;
        private readonly ILogger<RssBackgroundService> _logger;

        public RssBackgroundService(RssService rssService, ILogger<RssBackgroundService> logger)
        {
            _rssService = rssService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var rssDocument = await _rssService.FetchRssAsync("https://www.minikino.cz/rss");

                    _logger.LogInformation("RSS feed načten: {time}", DateTimeOffset.Now);

                    var events = await _rssService.ParseRssToEvents(rssDocument); // Předáno XDocument
                    await _rssService.SaveEventsAsync(events); // Uložení událostí do databáze
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Chyba při načítání RSS feedu");
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }

}
