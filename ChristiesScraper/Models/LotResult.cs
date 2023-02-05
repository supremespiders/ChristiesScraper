namespace ChristiesScraper.Models;

public class LotResult
{
    public Save_lot_api_endpoint save_lot_api_endpoint { get; set; }
    public Lot_search_api_endpoint lot_search_api_endpoint { get; set; }
    public Filters filters { get; set; }
    public Sort[] sort { get; set; }
    public int total_pages { get; set; }
    public bool show_sort { get; set; }
    public bool show_save_switch { get; set; }
    public bool show_keyword_search { get; set; }
    public bool show_themes { get; set; }
    public Lots[] lots { get; set; }
    public Ui_state ui_state { get; set; }
    public string online_only_checkout_txt { get; set; }
    public Online_only_bid_status online_only_bid_status { get; set; }
    public int total_hits_filtered { get; set; }
    
public class Save_lot_api_endpoint
{
    public string url { get; set; }
    public string path { get; set; }
    public string method { get; set; }
    public Parameters parameters { get; set; }
    public int timeout_ms { get; set; }
}

public class Parameters
{
    public string apikey { get; set; }
    public bool is_static { get; set; }
}

public class Lot_search_api_endpoint
{
    public string url { get; set; }
    public string path { get; set; }
    public string method { get; set; }
    public Parameters1 parameters { get; set; }
}

public class Parameters1
{
    public string language { get; set; }
    public string saleid { get; set; }
    public string page { get; set; }
}

public class Filters
{
    public Groups[] groups { get; set; }
}

public class Groups
{
    public string title_txt { get; set; }
    public int low { get; set; }
    public int high { get; set; }
    public string display { get; set; }
    public string id { get; set; }
    public int total { get; set; }
    public Filters1[] filters { get; set; }
}

public class Filters1
{
    public int index { get; set; }
    public string type { get; set; }
    public string label_txt { get; set; }
    public string id { get; set; }
    public int total { get; set; }
    public bool hide { get; set; }
}

public class Sort
{
    public string sort_id { get; set; }
    public string label_txt { get; set; }
    public bool initial { get; set; }
}

public class Lots
{
    public string end_date_unformatted { get; set; }
    public string start_date_unformatted { get; set; }
    public string object_id { get; set; }
    public string lot_id_txt { get; set; }
    public string event_type { get; set; }
    public string start_date { get; set; }
    public string end_date { get; set; }
    public string registration_closing_date { get; set; }
    public string countdown_start_date { get; set; }
    public string url { get; set; }
    public string title_primary_txt { get; set; }
    public string title_secondary_txt { get; set; }
    public string consigner_information { get; set; }
    public string description_txt { get; set; }
    public Image image { get; set; }
    public bool estimate_visible { get; set; }
    public bool estimate_on_request { get; set; }
    public bool price_on_request { get; set; }
    public string estimate_low { get; set; }
    public string estimate_high { get; set; }
    public string estimate_txt { get; set; }
    public string price_realised { get; set; }
    public string price_realised_txt { get; set; }
    public string current_bid_txt { get; set; }
    public bool is_saved { get; set; }
    public bool show_save { get; set; }
    public bool has_no_bids { get; set; }
    public string bid_count_txt { get; set; }
    public bool extended { get; set; }
    public string server_time { get; set; }
    public int total_seconds_remaining { get; set; }
    public Sale sale { get; set; }
    public bool lot_withdrawn { get; set; }
    public Bid_status bid_status { get; set; }
    public int seconds_until_bidding { get; set; }
    public bool show_timer { get; set; }
    public string current_bid { get; set; }
}

public class Image
{
    public string image_src { get; set; }
    public string image_mobile_src { get; set; }
    public string image_tablet_src { get; set; }
    public string image_desktop_src { get; set; }
    public string image_alt_text { get; set; }
    public string image_url { get; set; }
}

public class Sale
{
    public string time_zone { get; set; }
    public string date_txt { get; set; }
    public string start_date { get; set; }
    public string end_date { get; set; }
    public bool is_in_progress { get; set; }
}

public class Bid_status
{
    public string txt { get; set; }
    public string status { get; set; }
}

public class Ui_state
{
    public bool hide_timer { get; set; }
    public bool hide_cta { get; set; }
    public bool hide_bid_information { get; set; }
    public bool show_restriction_link { get; set; }
}

public class Online_only_bid_status
{
    public string status { get; set; }
    public string txt { get; set; }
    public string error_txt { get; set; }
}


}