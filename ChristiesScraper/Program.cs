using ChristiesScraper.Services;

try
{
    var scraper = new Scraper();
    await scraper.Start();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}