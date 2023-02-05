using System.Net;
using System.Text.RegularExpressions;
using ChristiesScraper.Extensions;
using ChristiesScraper.Models;
using ExcelHelperExe;
using Newtonsoft.Json;

namespace ChristiesScraper.Services
{
    public class Scraper
    {
        private HttpClient _client = new(new HttpClientHandler()
        {
            UseCookies = false,
            AutomaticDecompression = DecompressionMethods.All
        });

        public async Task Start()
        {
            // var html = await _client.GetHtml("https://www.christies.com/en/results");
            // var c=html.StringBetween("\"component\":\"","\"");
            //await GetUrlsThreaded();
           //await GetMainUrls();
           var items = JsonConvert.DeserializeObject<List<Item>>(await File.ReadAllTextAsync("items"));
          // await items.SaveToExcel("output.xlsx");
           await DownloadImage(items.First());
           // for (var i = 0; i < items.Count; i++)
           // {
           //     var item = items[i];
           //     item.LocalImage = $"{i + 1}.jpg";
           // }
           // await File.WriteAllTextAsync("items",JsonConvert.SerializeObject(items));
        }

        private async Task DownloadImage(Item item)
        {
            await _client.DownloadFile(item.Image, item.LocalImage);
        }
        
        private async Task GetUrlsThreaded()
        {
            var mainUrls = await File.ReadAllLinesAsync("mainUrls");
            var items = await mainUrls.Parallel(GetDetails,10);
            await File.WriteAllTextAsync("items",JsonConvert.SerializeObject(items));
            Console.WriteLine("completed");
        }

        private async Task GetUrls()
        {
            var mainUrls = await File.ReadAllLinesAsync("mainUrls");
            var items = new List<Item>();
            for (var i = 0; i < mainUrls.Length; i++)
            {
                var mainUrl = mainUrls[i];
               
                // var saleId = "";
                // var g = false;
                // if (mainUrl.Contains("?SaleID="))
                //     saleId = mainUrl.StringBetween("?SaleID=", "&");
                // else
                // {
                //     var x = mainUrl.LastIndexOf("-", StringComparison.Ordinal);
                //     saleId = mainUrl.Substring(x + 1).Replace("/", "");
                //     g = true;
                // }
                //
                // if (string.IsNullOrEmpty(saleId)) throw new KnownException($"SaleId is null : {mainUrl}");
                // if (saleId.Length < 3 || saleId.Length > 6) throw new KnownException($"SaleId is weird : {mainUrl}");
                
                var it = await GetDetails(mainUrl);
                if(it.Count==0) Console.WriteLine($"------------------- Weird : {mainUrl} gave 0");
                items.AddRange(it);
                Console.WriteLine($"completed : {i+1} / {mainUrls.Length}, collected : {items.Count}");
            }
            await File.WriteAllTextAsync("items",JsonConvert.SerializeObject(items));
            Console.WriteLine("completed");
        }
        private  Regex digitsOnly = new Regex(@"[^\d]");   
        private async Task<List<Item>> GetDetails(string url)
        {
            var parts = url.Split(',');
            if (parts.Length != 2) throw new KnownException($"parts not right Main url : {url}");
            string eventId=parts[0];
            string saleId = parts[1];
            saleId = digitsOnly.Replace(saleId, "");
            string id = null;
            // if (!g)
            // {
            //     var html = await _client.GetHtml(mainUrl);
            //     id = html.StringBetween("auctionEventId: '", "'");
            // }
            // if (id == null) id = saleId;
            var items = new List<Item>();
            var p = 1;
            do
            {
                //var json = await _client.GetHtml($"https://onlineonly.christies.com/sale/searchLots?action=paging&language=en&page={p}&saleid={id}&sid&sortby=LotNumber");
                var u = $"https://www.christies.com/api/discoverywebsite/auctionpages/lotsearch?action=sort&language=en&page={p}&saleid={eventId}&salenumber={saleId}&saletype=Sale&sortby=lotnumber";
                var json = await _client.GetHtml(u);
                if (!json.StartsWith("{"))
                {
                    Console.WriteLine("issue");
                }
                var r = JsonConvert.DeserializeObject<LotResult>(json);
                foreach (var lot in r.lots)
                {
                    if (lot.image.image_src == null)
                    {
                        Console.WriteLine("s");
                    }

                    var v = lot.url;
                    if (!v.StartsWith($"https"))
                        v = $"https://onlineonly.christies.com/{v}";
                    items.Add(new Item()
                    {
                        Title = lot.title_primary_txt,
                        Artist = lot.consigner_information,
                        Subtitle = lot.title_secondary_txt,
                        Image = lot.image.image_src,
                        Url =v
                    });
                }

                if (items.Count == r.total_hits_filtered) break;
                p++;
            } while (true);

            return items;
        }

        private async Task GetMainUrls()
        {
            var urls = new List<string>();
            var url = "https://www.christies.com/api/discoverywebsite/auctioncalendar/auctionresults?language=en&month=2&year=2023&component=e7d92272-7bcc-4dba-ae5b-28e4f3729ae8";
            do
            {
                Console.WriteLine(url);
                var json = await _client.GetHtml(url);
                var r = JsonConvert.DeserializeObject<AuctionResult>(json);
                foreach (var e in r.events)
                {
                    //urls.Add(e.landing_url);
                    var eventId = e.event_id;
                    var saleId = e.analytics_id.Replace("Sale-", "");
                    urls.Add($"{eventId},{saleId}");
                }

                url = r?.page_previous?.url;
                if (string.IsNullOrEmpty(url)) break;
                url = $"https://www.christies.com/{url}";
            } while (true);

            await File.WriteAllLinesAsync("mainUrls", urls);
            Console.WriteLine("completed");
        }
    }
}