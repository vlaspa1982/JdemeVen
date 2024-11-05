namespace JdemeVen.Server.Models.RssFeed
{
    public class RssFeed
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Uri Link { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public List<RssFeedItem> Items { get; set; } = new List<RssFeedItem>();
    }
}
