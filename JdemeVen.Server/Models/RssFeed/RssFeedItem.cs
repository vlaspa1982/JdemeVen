namespace JdemeVen.Server.Models.RssFeed
{
    public class RssFeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string Location { get; set; }
        public string Author { get; set; }
        public List<string> ImageUrls { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
    }
}
