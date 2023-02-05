namespace ChristiesScraper.Models;

public class Item
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Artist { get; set; }
    public string Image { get; set; }
    public string LocalImage { get; set; }
    public string Url { get; set; }
    public bool ImageDownloaded { get; set; }
}