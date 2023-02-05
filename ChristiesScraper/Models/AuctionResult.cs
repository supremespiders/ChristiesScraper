namespace ChristiesScraper.Models;

public class AuctionResult
{
    public Auction_results_api_endpoint auction_results_api_endpoint { get; set; }
    public object page_next { get; set; }
    public Page_previous page_previous { get; set; }
    public Options options { get; set; }
    public object[] banners { get; set; }
    public Filters filters { get; set; }
    public Titles[] titles { get; set; }
    public Events[] events { get; set; }

public class Auction_results_api_endpoint
{
    public string url { get; set; }
    public string path { get; set; }
    public Parameters parameters { get; set; }
}

public class Parameters
{
    public string language { get; set; }
    public string month { get; set; }
    public string year { get; set; }
    public string component { get; set; }
}

public class Page_previous
{
    public string title_txt { get; set; }
    public string url { get; set; }
}

public class Options
{
    public string min_date { get; set; }
    public string max_date { get; set; }
    public Groups groups { get; set; }
}

public class Groups
{
    public Month month { get; set; }
    public Year year { get; set; }
}

public class Month
{
    public string title_txt { get; set; }
    public bool is_open { get; set; }
    public int total { get; set; }
    public string display { get; set; }
    public Filters1[] filters { get; set; }
    public object filter_groups { get; set; }
    public object type { get; set; }
}

public class Filters1
{
    public string label_txt { get; set; }
    public string id { get; set; }
    public int total { get; set; }
    public string analytics_id { get; set; }
    public string type { get; set; }
    public bool show { get; set; }
}

public class Year
{
    public string title_txt { get; set; }
    public bool is_open { get; set; }
    public int total { get; set; }
    public string display { get; set; }
    public Filters2[] filters { get; set; }
    public object filter_groups { get; set; }
    public object type { get; set; }
}

public class Filters2
{
    public string label_txt { get; set; }
    public string id { get; set; }
    public int total { get; set; }
    public string analytics_id { get; set; }
    public string type { get; set; }
    public bool show { get; set; }
}

public class Filters
{
    public int total { get; set; }
    public Groups1[] groups { get; set; }
}

public class Groups1
{
    public string title_txt { get; set; }
    public bool is_open { get; set; }
    public int total { get; set; }
    public string display { get; set; }
    public Filters3[] filters { get; set; }
    public Filter_groups filter_groups { get; set; }
    public string type { get; set; }
}

public class Filters3
{
    public string label_txt { get; set; }
    public string id { get; set; }
    public int total { get; set; }
    public string analytics_id { get; set; }
    public string type { get; set; }
    public bool show { get; set; }
}

public class Filter_groups
{
    public Event_auctions event_auctions { get; set; }
    public Event_other event_other { get; set; }
}

public class Event_auctions
{
    public string title_txt { get; set; }
    public bool is_open { get; set; }
    public int total { get; set; }
    public string display { get; set; }
    public Filters4[] filters { get; set; }
    public object filter_groups { get; set; }
    public object type { get; set; }
}

public class Filters4
{
    public string label_txt { get; set; }
    public string id { get; set; }
    public int total { get; set; }
    public string analytics_id { get; set; }
    public object type { get; set; }
    public bool show { get; set; }
}

public class Event_other
{
    public string title_txt { get; set; }
    public bool is_open { get; set; }
    public int total { get; set; }
    public string display { get; set; }
    public object[] filters { get; set; }
    public object filter_groups { get; set; }
    public object type { get; set; }
}

public class Titles
{
    public string id { get; set; }
    public string title_txt { get; set; }
    public string subtitle_txt { get; set; }
    public string type { get; set; }
    public string analytics_id { get; set; }
}

public class Events
{
    public string title_id { get; set; }
    public string event_id { get; set; }
    public string filter_ids { get; set; }
    public bool is_on_view { get; set; }
    public string on_view_txt { get; set; }
    public bool is_followed { get; set; }
    public bool is_in_progress { get; set; }
    public string landing_url { get; set; }
    public string title_txt { get; set; }
    public string subtitle_txt { get; set; }
    public string date_display_txt { get; set; }
    public string date_sr_txt { get; set; }
    public string status_txt { get; set; }
    public string location_txt { get; set; }
    public string sale_total_txt { get; set; }
    public string sale_total_value_txt { get; set; }
    public string cta_txt { get; set; }
    public Image image { get; set; }
    public string start_date { get; set; }
    public string end_date { get; set; }
    public bool is_active { get; set; }
    public bool is_live { get; set; }
    public string analytics_id { get; set; }
}

public class Image
{
    public string alt_text { get; set; }
    public Placeholder placeholder { get; set; }
    public Breakpoints[] breakpoints { get; set; }
}

public class Placeholder
{
    public string name { get; set; }
    public Srcset[] srcset { get; set; }
}

public class Srcset
{
    public int width { get; set; }
    public string src { get; set; }
}

public class Breakpoints
{
    public string name { get; set; }
    public Srcset1[] srcset { get; set; }
}

public class Srcset1
{
    public int width { get; set; }
    public string src { get; set; }
}


}