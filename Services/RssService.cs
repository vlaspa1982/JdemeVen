using backend.Data;
using backend.Models;
using backend.Models.RssFeed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace backend.Services
{
    public class RssService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _dbContext;

        public RssService(HttpClient httpClient, ApplicationDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        public async Task<XDocument> FetchRssAsync(string url)
        {
            var response = await _httpClient.GetStringAsync(url);
            return XDocument.Parse(response);
        }

        public async Task<List<Event>> ParseRssToEvents(XDocument rssDocument)
        {
            RssFeed rssFeed = ConvertXDocumentToRssFeed(rssDocument);
            List<Event> events = new List<Event>();

            foreach (var item in rssFeed.Items)
            {
                Event newEvent = new Event
                {
                    Title = item.Title,
                    Description = item.Description,
                    Date = item.PublishDate,
                    Location = item.Location, // Zde můžeš přidat logiku, pokud máš lokaci
                    EventTypeId = 3, // Nastaveno na Film
                    OrganizerContact = null, // Pokud nejsou kontaktní informace, nastav na null
                    InvitedGuests = new List<EventInvitation>(),
                    Images = ParseImages(item),
                    Ratings = new List<EventRating>()
                };

                events.Add(newEvent);
            }

            return events;
        }


        private RssFeed ConvertXDocumentToRssFeed(XDocument rssDocument)
        {
            var rssFeed = new RssFeed
            {
                Items = new List<RssFeedItem>()
            };

            foreach (var item in rssDocument.Descendants("item"))
            {
                var rssFeedItem = new RssFeedItem
                {
                    Title = item.Element("title")?.Value,
                    Description = item.Element("description")?.Value,
                    PublishDate = DateTime.Parse(item.Element("pubDate")?.Value ?? DateTime.Now.ToString()),
                    Location = string.Empty, // Pokud není v RSS
                    Author = string.Empty, // Pokud není v RSS
                    ImageUrls = new List<string>(), // Pokud není v RSS, zde můžeš přidat logiku pro extrakci obrázků
                    Link = item.Element("link")?.Value, // Přidání odkazu
                    Guid = item.Element("guid")?.Value // Přidání GUID
                };

                rssFeed.Items.Add(rssFeedItem);
            }

            return rssFeed;
        }


        public static List<EventImage> ParseImages(RssFeedItem item)
        {
            var images = new List<EventImage>();

            foreach (var imageUrl in item.ImageUrls)
            {
                images.Add(new EventImage { ImageUrl = imageUrl.ToString() });
            }

            return images;
        }

        public async Task SaveEventsAsync(List<Event> events)
        {
            await _dbContext.Events.AddRangeAsync(events);
            await _dbContext.SaveChangesAsync();
        }
    }
}

//Event newEvent = new Event
//                {
//                    Title = item.Title,
//                    Description = item.Description,
//                    Date = item.PublishDate,
//                    Location = item.Location,
//                    EventTypeId = 3, // Nastaveno na Film
//                    OrganizerContact = item.Author,
//                    InvitedGuests = new List<EventInvitation>(),
//                    Images = ParseImages(item),
//                    Ratings = new List<EventRating>()
//                };

          
