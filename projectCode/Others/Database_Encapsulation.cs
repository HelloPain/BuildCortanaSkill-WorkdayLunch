using System.Data.SqlClient;
using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

using SmallInt = System.Int16;
using Int = System.Int32;
using BigInt = System.Int64;

public class ZomatoClient : HttpClient
{
    //doc: https://developers.zomato.com/documentation#/
    static readonly string[] requestType =
    {
          "categories","cities","collections","cuisines","establishments","geocode",//common
          "location_details","locations",//location
          "dailymenu","restaurant","reviews","search"//restaurant
        };
    public class CategoriesJson
    {
        public struct Catergory
        {
            public SmallInt id;
            public string name;
        }
        public struct CatergoriesListItem : IComparable<CatergoriesListItem>
        {
            public Catergory categories;

            int IComparable<CatergoriesListItem>.CompareTo(CatergoriesListItem other)
            {
                return categories.id - other.categories.id;
            }
        }
        public CatergoriesListItem[] categories;
    }
    public class CitiesJson
    {
        public struct City
        {
            public int id;
            public string name;
            public SmallInt country_id;//optional
            public string country_name;//optional
            public bool is_state;//optional
            public int state_id;//optional
            public string state_name;//optional
            public string state_code;//optional
        }
        public City[] location_suggestions;
        public string status;
        public int has_more;
        public int has_total;
    }
    public class CollectionsJson
    {
        public struct Collection
        {
            public BigInt collection_id;//optional
            public int res_count;//optional
            public string share_url;//optional
            public string title;//optional
            public string url;//optional
            public string description;//optional
            public string image_url;//optional
        }
        public struct CollectionListObject
        {
            public Collection collection;
        }
        public CollectionListObject[] collections;
        public int has_more;
        public string share_url;   //list of the collections
        public string display_text;
        public int has_total;
    }
    public class CuisinesJson
    {
        public struct Cuisine
        {
            public BigInt cuisine_id;
            public string cuisine_name;
        }
        public struct CuisineListObject
        {
            public Cuisine cuisine;
        }
        public CuisineListObject[] cuisines;
    }
    public class EstablishmentJson
    {
        public struct Establishment
        {
            public int id;
            public string name;
        }
        public struct EstablishmentListObject
        {
            public Establishment establishment;
        }
        public EstablishmentListObject[] establishments;
    }
    public class GeocodeJson
    {
        public class NearbyRestaurantsListObject
        {
            public RestaurantL3 restaurant;
        }
        public Location location;//optional
        public Popularity popularity;//optional
        public string link;//optional;URL of the web search page of the locality
        public NearbyRestaurantsListObject[] nearby_restaurants;//optional;list of nearby restaurants
    }
    public class LocationJson
    {
        public Location[] location_suggestions;
        public string status;
        public int has_more;
        public int has_total;
    }
    public class LocationDetialsJson
    {
        //class Popularity
        public Popularity popularity;
        public Location location;//optional;location struct 
        public RestaurantL3[] best_rated_restaurants;//optional;list of top rated restaurants in location
    }
    public class DailyMenuJson
    {
        public DailyMenuCategory[] daily_menu;//optional;List of restaurant's menu details
    }
    public class RestaurantJson
    {
        public string apikey;
        public string id;
        public string name;
        public string url;
        public ResLocation location;
        public BigInt switch_to_order_menu;
        public int average_cost_for_two;//optional;Average price of a meal for two people
        public int price_range;//optional;Price bracket of the restaurant (1 being pocket friendly and 4 being the costliest)
        public string currency;//optional;Local currency symbol; to be used with price
        public string thumb;//optional;URL of the low resolution header image of restaurant
        public string featured_image;//optional;URL of the high resolution header image of restaurant
        public string photos_url;//optional;URL of the restaurant's photos page
        public string menu_url;//optional;URL of the restaurant's menu page
        public string events_url;//optional;URL of the restaurant's events page
        public UserRating user_rating;//optional;Restaurant rating details
        public bool has_online_delivery;//optional;Whether the restaurant has online delivery enabled or not
        public bool is_delivering_now;//optional;Valid only if has_online_delivery = 1; whether the restaurant is accepting online orders right now
        public bool has_table_booking;//optional;Whether the restaurant has table reservation enabled or not
        public string deeplink;// optional;Short URL of the restaurant page; for use in apps or social shares,
        public string cuisines;//optional;List of cuisines served at the restaurant in csv format
        public int all_reviews_count;//optional;[Partner access] Number of reviews for the restaurant
        public int photo_count;//optional;[Partner access] Total number of photos for the restaurant, at max 10 photos for partner access
        public string phone_numbers;//optional;[Partner access] Restaurant's contact numbers in csv format
        public Photo[] photos;//optional;[Partner access] List of restaurant photos
        public Review[] all_reviews;//optional;[Partner access] List of restaurant reviews 
    }
    public class ReviewsJson
    {
        public struct ReviewsListItem
        {
            public Review review;
        }
        public BigInt reviews_count;
        public BigInt reviews_start;
        public SmallInt reviews_shown;
        public ReviewsListItem[] user_reviews;
        public string Respond_to_reviews_via_Zomato_Dashboard;//url
    }

    public class Location
    {
        public string entity_type;//optional;Type of location; one of [city, zone, subzone, landmark, group, metro, street]
        public int entity_id;//optional;ID of location; (entity_id, entity_type) tuple uniquely identifies a location
        public string title;//optional;Name of the location
        public double latitude;//optional;Coordinates of the (centre of) location
        public double longitude;//optional;Coordinates of the (centre of) location
        public int city_id;//optional;ID of city
        public string city_name;//optional;Name of the city
        public SmallInt country_id;//optional;ID of country
        public string country_name;//optional;Name of the country
    }
    public class Popularity
    {
        public float popularity;//Foodie index of a location out of 5.00
        public float nightlife_index;//Nightlife index of a location out of 5.00
        public string[] top_cuisines;//optional;Most popular cuisines in the locality
    }
    public class RestaurantL3
    {
        public BigInt id;//optional;ID of the restaurant
        public string name;//optional;Name of the restaurant
        public string url;//optional;URL of the restaurant page
        public ResLocation location;//optional;Restaurant location details
        public int average_cost_for_two;//optional;Average price of a meal for two people
        public int price_range;//optional;Price bracket of the restaurant (1 being pocket friendly and 4 being the costliest)
        public string currency;//optional;Local currency symbol; to be used with price
        public string thumb;//optional;URL of the low resolution header image of restaurant
        public string featured_image;//optional;URL of the high resolution header image of restaurant
        public string photos_url;//optional;URL of the restaurant's photos page
        public string menu_url;//optional;URL of the restaurant's menu page
        public string events_url;//optional;URL of the restaurant's events page
        public UserRating user_rating;//optional;Restaurant rating details
        public bool has_online_delivery;//optional;Whether the restaurant has online delivery enabled or not
        public bool is_delivering_now;//optional;Valid only if has_online_delivery = 1; whether the restaurant is accepting online orders right now
        public bool has_table_booking;//optional;Whether the restaurant has table reservation enabled or not
        public string deeplink;// optional;Short URL of the restaurant page; for use in apps or social shares,
        public string cuisines;//optional;List of cuisines served at the restaurant in csv format
        public int all_reviews_count;//optional;[Partner access] Number of reviews for the restaurant
        public int photo_count;//optional;[Partner access] Total number of photos for the restaurant, at max 10 photos for partner access
        public string phone_numbers;//optional;[Partner access] Restaurant's contact numbers in csv format
        public Photo[] photos;//optional;[Partner access] List of restaurant photos
        public Review[] all_reviews;//optional;[Partner access] List of restaurant reviews 
    }
    public class ResLocation
    {
        public string address;//Complete address of the restaurant
        public string locality;//Name of the locality
        public string city;//Name of the city
        public double latitude;//Coordinates of the restaurant
        public double longitude;//Coordinates of the restaurant
        public string zipcode;//Zipcode
        public SmallInt country_id;//ID of the country 
    }
    public class UserRating
    {
        public float aggregate_rating;//optional;Restaurant rating on a scale of 0.0 to 5.0 in increments of 0.1
        public string rating_text;//optional;Short description of the rating
        public string ratinng_color;//optional;Color hex code used with the rating on Zomato
        public int votes;//optional;Number of ratings received 
    }
    public class Photo
    {
        public string id;//ID of the photo
        public string url;//URL of the image file
        public string thumb_url;//URL for 200 X 200 thumb image file
        public User user;//User who uploaded the photo
        public BigInt res_id;//ID of restaurant for which the image was uploaded
        public string caption;//Caption of the photo
        public int timestamp;//Unix timestamp when the photo was uploaded
        public string friendly_time;//User friendly time string; denotes when the photo was uploaded
        public int width;//Image width in pixel; usually 640
        public int height;//Image height in pixel; usually 640
        public int comments_count;//Number of comments on photo
        public int likes_count;//Number of likes on photo 
    }
    public class Review
    {
        public float rating;//Rating on scale of 0 to 5 in increments of 0.5
        public string review_text;//Review text
        public int id;//ID of the review
        public string rating_color;//Color hex code used with the rating on Zomato
        public string review_time_friendly;//User friendly time string corresponding to time of review posting
        public string rating_text;//Short description of the rating
        public BigInt timestamp;//Unix timestamp for review_time_friendly
        public int likes;//No of likes received for review
        public User user;//User details of author of review
        public int comments_count;//No of comments on review 
    }
    public class User
    {
        public string name;//optional;User's name;
        public string zomato_handle;//optional;User's @handle; uniquely identifies a user on Zomato
        public string foodie_level;//optional;Text for user's foodie level
        public SmallInt foodie_level_num;//optional;Number to identify user's foodie level; ranges from 0 to 10 
        public string foodie_color;//optional;Color hex code used with foodie level on Zomato
        public string profile_url;//optional;URL for user's profile on Zomato
        public string profile_deeplink;//optional;short URL for user's profile on Zomato; for use in apps or social sharing
        public string profile_image;//optional;URL for user's profile image
    }
    public class DailyMenuCategory
    {
        public BigInt daily_menu_id;//optional;ID of the restaurant
        public string name;//optional;Name of the restaurant 
        public string start_date;//optional;Daily Menu start timestamp;
        public string end_date;//optional;Daily Menu end timestamp
        public DailyMenuItem[] dishes;//optional;Menu item in the category
    }
    public class DailyMenuItem
    {
        public BigInt dish_id;//optional;Menu Iten ID
        public string name;//optional;Menu Item Title
        public string price;//optional;Menu Item Price
    }

    public ZomatoClient() : base()
    {
        base.BaseAddress = new Uri("https://developers.zomato.com/api/v2.1/");
        base.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        base.DefaultRequestHeaders.Add("user-key", "8e4b611eaf47262f7d5ab7d7c8b25cfb");

    }

    /*Get a list of categories. List of all restaurants categorized under a particular restaurant type can be obtained using /Search API with Category ID as inputs*/
    public async Task<CategoriesJson> GetCategoriesAsync()
    {
        HttpResponseMessage response = await this.GetAsync("categories", HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            string tmp = response.Content.ReadAsStringAsync().Result;
            return await response.Content.ReadAsAsync<CategoriesJson>();
        }
        else return null;
    }
    /*Find the Zomato ID and other details for a city . You can obtain the Zomato City ID in one of the following ways:

        City Name in the Search Query - Returns list of cities matching the query
        Using coordinates - Identifies the city details based on the coordinates of any location inside a city

     * If you already know the Zomato City ID, this API can be used to get other details of the city.
     * count -- max number of results needed
     */
    public async Task<CitiesJson> GetCitiesAsync(string cityName, BigInt count = BigInt.MaxValue)
    {
        HttpResponseMessage response = await this.GetAsync("cities?q=" + HttpUtility.UrlEncode(cityName) + "&count=" + count, HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CitiesJson>();
        }
        else return null;
    }
    public async Task<CitiesJson> GetCitiesAsync(Int[] cityIDs)
    {
        if (cityIDs.Length == 0) return null;
        StringBuilder request = new StringBuilder("cities?city_ids=");
        request.Append(cityIDs[0].ToString());
        if (cityIDs.Length > 1)
            for (int index = 2; index < cityIDs.Length; ++index)
            {
                request.Append("%2C"); request.Append(cityIDs[index]);
            }
        HttpResponseMessage response = await this.GetAsync(request.ToString(), HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CitiesJson>();
        }
        else return null;
    }
    public async Task<CitiesJson> GetCitiesAsync(double lat, double lon)
    {
        StringBuilder request = new StringBuilder("cities?lat=");
        request.Append(lat.ToString());
        request.Append("&lon=");
        request.Append(lon.ToString());
        HttpResponseMessage response = await this.GetAsync(request.ToString(), HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CitiesJson>();
        }
        else return null;
    }
    /*Returns Zomato Restaurant Collections in a City. The location/City input can be provided in the following ways -

        Using Zomato City ID
        Using coordinates of any location within a city

     *List of all restaurants listed in any particular Zomato Collection can be obtained using the '/search' API with Collection ID and Zomato City ID as the input
     */
    public async Task<CollectionsJson> GetCollectionsAsync(int cityID)
    {
        HttpResponseMessage response = await this.GetAsync("collections?city_id=" + cityID, HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CollectionsJson>();
        }
        else return null;
    }
    public async Task<CollectionsJson> GetCollectionsAsync(double lat, double lon)
    {
        StringBuilder request = new StringBuilder("collections?lat=");
        request.Append(lat);
        request.Append("&lon=");
        request.Append(lon);
        HttpResponseMessage response = await this.GetAsync(request.ToString(), HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CollectionsJson>();
        }
        else return null;
    }
    /*Get a list of all cuisines of restaurants listed in a city. The location/city input can be provided in the following ways -

        Using Zomato City ID
        Using coordinates of any location within a city

     *List of all restaurants serving a particular cuisine can be obtained using '/search' API with cuisine ID and location details
     */
    public async Task<CuisinesJson> GetCuisionAsync(int cityID)
    {
        HttpResponseMessage response = await this.GetAsync("cuisines?city_id=" + cityID, HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CuisinesJson>();
        }
        else return null;

    }
    public async Task<CuisinesJson> GetCuisionAsync(double lat, double lon)
    {
        StringBuilder request = new StringBuilder("cuisines?lat=");
        request.Append(lat);
        request.Append("&lon=");
        request.Append(lon);
        HttpResponseMessage response = await this.GetAsync(request.ToString(), HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CuisinesJson>();
        }
        else return null;
    }
    /*Get a list of restaurant types in a city. The location/City input can be provided in the following ways -

        Using Zomato City ID
        Using coordinates of any location within a city

     *List of all restaurants categorized under a particular restaurant type can obtained using /Search API with Establishment ID and location details as inputs
     */
    public async Task<EstablishmentJson> GetEstablishmentAsync(int cityID)
    {
        HttpResponseMessage response = await this.GetAsync("establishments?city_id=" + cityID, HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<EstablishmentJson>();
        }
        else return null;
    }
    public async Task<EstablishmentJson> GetEstablishmentAsync(double lat, double lon)
    {
        StringBuilder request = new StringBuilder("establishments?lat=");
        request.Append(lat);
        request.Append("&lon=");
        request.Append(lon);
        HttpResponseMessage response = await this.GetAsync(request.ToString(), HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<EstablishmentJson>();
        }
        else return null;
    }
    /*Get Foodie and Nightlife Index, list of popular cuisines and nearby restaurants around the given coordinates*/
    public async Task<GeocodeJson> GetGeocodeAsync(double lat, double lon)
    {
        StringBuilder request = new StringBuilder("geocode?lat=");
        request.Append(lat);
        request.Append("&lon=");
        request.Append(lon);
        HttpResponseMessage response = await this.GetAsync(request.ToString(), HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<GeocodeJson>();
        }
        else return null;
    }
    /*Search for Zomato locations by keyword. Provide coordinates to get better search results*/
    public async Task<LocationJson> GetLocationAsync(string queryName, BigInt count = BigInt.MaxValue)
    {
        HttpResponseMessage response = await this.GetAsync("locations?query=" + HttpUtility.UrlEncode(queryName) + "&count=" + count, HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<LocationJson>();
        }
        else return null;
    }
    public async Task<LocationJson> GetLocationAsync(string queryName, double lat, double lon, BigInt count = BigInt.MaxValue)
    {
        StringBuilder request = new StringBuilder("locations?query=" + HttpUtility.UrlEncode(queryName) + '&');
        request.Append(lat);
        request.Append("&lon=");
        request.Append(lon);
        request.Append("&count=");
        request.Append(count);
        HttpResponseMessage response = await this.GetAsync(request.ToString(), HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<LocationJson>();
        }
        else return null;
    }
    /*Get Foodie Index, Nightlife Index, Top Cuisines and Best rated restaurants in a given location*/
    public async Task<LocationDetialsJson> GetLocationDetialsAsync(int entity_id, string entity_type)
    {
        StringBuilder request = new StringBuilder("location_details?entity_id=");
        request.Append(entity_id);
        request.Append("&entity_type=");
        request.Append(entity_type);
        HttpResponseMessage response = await this.GetAsync(request.ToString(), HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<LocationDetialsJson>();
        }
        else return null;

    }
    /*Get daily menu using Zomato restaurant ID*/
    public async Task<DailyMenuJson> GetDailyMenuAsync(BigInt res_id)
    {
        HttpResponseMessage response = await this.GetAsync("dailymenu?res_id=" + res_id, HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<DailyMenuJson>();
        }
        else return null;
    }
    /*Get detailed restaurant information using Zomato restaurant ID. Partner Access is required to access photos and reviews*/
    public async Task<RestaurantJson> GetRestaurantAsync(BigInt res_id)
    {
        HttpResponseMessage response = await this.GetAsync("restaurant?res_id=" + res_id, HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<RestaurantJson>();
        }
        else return null;
    }
    /*Get restaurant reviews using the Zomato restaurant ID. Only 5 latest reviews are available under the Basic API plan.*/
    ///fetch results after this offset
    public async Task<ReviewsJson> GetReviewsAsync(BigInt res_id, BigInt start = 0, BigInt count = BigInt.MaxValue)
    {
        HttpResponseMessage response = await this.GetAsync("reviews?res_id=" + res_id + "&start=" + start + "&count=" + count, HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<ReviewsJson>();
        }
        else return null;
    }
}

static class Database//the Encapsulation of all databases
{
    public const string DatabaseHeader = "[Database_Encapsulation].";
    public static class Table_DatabaseProviders
    {
        public const string Header = "[dbo].[DatabaseProviders]";
        public const string Columns = "([ID],[name],[homepageURL])";
        private class DatabaseProvider
        {
            public SmallInt ID;//internally assigned ID,primary key
            public string name;//not nulls
            public string homepageURL;
        }
    }
    public static class Table_Nations//archive
    {
        public const string Header = "[dbo].[Nations]";
        public const string Columns = "([ID],[name_English],[name_locale],[currency])";
        private class Nation
        {
            public SmallInt ID;//country_id;SmallInt; ISO 3166;primary key
            public string name_English;//full English name;varchar;not null
            public string name_locale;//full locale name;nvarchar
            public string currency;//;ISO 4217;char(3);not null
        }
    }
    public static class Table_Cities//archive
    {
        public const string Header = "[dbo].[Cities]";
        public const string Columns = "([ID],[name_locale],[name_English])";
        private class City
        {
            private SmallInt ID;//internally assigned id;primary key;
            public string name_locale;//not null
            public string name_English;
        }
    }
    public static class Table_Restaurants//may need update
    {
        public const string Header = "[dbo].[Restaurants]";
        public const string Columns = @"([ID],[city_ID],[name],[locality_address],[description],[locality_latitude],[locality_longitude],
            [locality_zipcode],[currency_Nation_ID],[restaurant_type],[open_time],[close_time],[average_cost_by_one],
            [average_cost_by_two],[is_delivering],[imageURL],[photosURL],[eventsURL],[homepageURL],[DataProviderID])";
        public class Restaurant
        {
            public Restaurant(SmallInt CityID,string resName,string address)
            {
                City_ID = CityID;
                Name = resName;
                Locality_address = address;
            }
            private Int ID ;//internally assigned id;primary key
            public SmallInt City_ID { private set; get; }//not null
            public string Name { private set; get; }//not null
            public string Locality_address { private set; get; }//not null
            public string Description
            {
                set { description = value;is_description_init = true; }
                get { return description; }
            }
            public double Locality_latitude
            {
                set { locality_latitude = value;is_locality_latitude_init = true; }
                get { return locality_latitude; }
            }
            public double Locality_longitude
            {
                set { locality_longitude = value;is_locality_longitude_init = true; }
                get { return locality_longitude; }
            }
            public bool Is_delivering
            {
                set { is_delivering = value;is_is_delivering_init = true; }
                get { return is_delivering; }
            }
            public string Locality_zipcode
            {
                set {locality_zipcode=value; is_locality_zipcode_init = true; }
                get { return locality_zipcode; }
            }
            public SmallInt Currency_Nation_ID
            {
                set { currency_Nation_ID = value;is_currency_Nation_ID_init = true; }
                get { return currency_Nation_ID; }
            }
            public string Restaurant_type
            {
                set { restaurant_type = value;is_restaurant_type_init = true; }
                get { return restaurant_type; }
            }
            public DateTime Open_time
            {
                set { open_time = value;is_open_time_init = true; }
                get { return open_time; }
            }
            public DateTime Close_time
            {
                set { close_time = value;is_close_time_init = true; }
                get { return close_time; }
            }
            public float Average_cost_by_one
            {
                set { average_cost_by_one = value;is_average_cost_by_one_init = true; }
                get { return average_cost_by_one; }
            }
            public float Average_cost_by_two
            {
                set { average_cost_by_two = value;is_average_cost_by_two_init = true; }
                get { return average_cost_by_two; }
            }
            public string ImageURL
            {
                set { imageURL = value;is_imageURL_init = true; }
                get { return imageURL; }
            }
            public string PhotosURL
            {
                set { photosURL = value;is_photoURL_init = true; }
                get { return photosURL; }
            }
            public string EventsURL
            {
                set { eventsURL = value;is_eventsURL_init = true; }
                get { return eventsURL; }
            }
            public string HomepageURL
            {
                set { homepageURL = value;is_homepageURL_init = true; }
                get { return homepageURL; }
            }
            public SmallInt DataProviderID
            {
                set { dataProviderID = value;is_dataProviderID_init = true; }
                get { return dataProviderID; }
            }

            private string description;
            protected internal bool is_description_init = false;
            private double locality_latitude;//decimal(8,6)
            private double locality_longitude;//decinal(9,6)
            private bool is_delivering;
            protected internal bool is_locality_latitude_init = false;
            protected internal bool is_locality_longitude_init = false;
            private string locality_zipcode;
            protected internal bool is_locality_zipcode_init = false;
            private SmallInt currency_Nation_ID;
            protected internal bool is_currency_Nation_ID_init = false;
            private string restaurant_type;
            protected internal bool is_restaurant_type_init = false;
            private DateTime open_time;//UTC
            protected internal bool is_open_time_init = false;
            private DateTime close_time;//UTC
            protected internal bool is_close_time_init = false;
            private float average_cost_by_one;//decimal(7,2)
            protected internal bool is_average_cost_by_one_init = false;
            private float average_cost_by_two;//two persions;decimal(7,2)
            protected internal bool is_average_cost_by_two_init = false;
            protected internal bool is_is_delivering_init = false;
            private string imageURL;
            protected internal bool is_imageURL_init = false;
            private string photosURL;
            protected internal bool is_photoURL_init = false;
            private string eventsURL;
            protected internal bool is_eventsURL_init = false;
            private string homepageURL;
            protected internal bool is_homepageURL_init = false;
            private SmallInt dataProviderID;//id in table DatabaseProviders
            protected internal bool is_dataProviderID_init = false;
        }
    }
    public static class Table_Dishes//daily update
    {
        public const string Header = "[dbo].[Dishes]";
        public const string Columns = "([ID],[name],[price],[description],[cuisine_ID],[currency_Nation_ID],[photosURL],[DataProviderID])";
        public class Dish
        {
            public Dish(string name, float price)
            {
                Name = name;Price = price;
            }
            private BigInt ID;//unique worldwide id in a day;primary key
            public string Name//not null
            {
                private set;get;
            }
            public float Price//not null
            {
                private set;get;
            }
            public string Description
            {
                set { description = value;is_description_init = true; }
                get { return description; }
            }
            public SmallInt Cuisine_ID
            {
                set { cuisine_ID = value;is_cuisine_ID_init = true; }
                get { return cuisine_ID; }
            }
            public SmallInt Currency_Nation_ID
            {
                set { currency_Nation_ID = value;is_currency_Nation_ID_init = true; }
                get { return currency_Nation_ID; }
            }
            public string PhotosURL
            {
                set { photosURL = value;is_photosURL_init = true; }
                get { return photosURL; }
            }
            public SmallInt DataProviderID
            {
                set { dataProviderID = value;is_dataProviderID_init = true; }
                get { return dataProviderID; }
            }

            protected internal bool is_description_init = false;
            private string description;
            protected internal bool is_cuisine_ID_init = false;
            private SmallInt cuisine_ID;
            protected internal bool is_currency_Nation_ID_init = false;
            private SmallInt currency_Nation_ID;
            protected internal bool is_photosURL_init = false;
            private string photosURL;
            protected internal bool is_dataProviderID_init = false;
            private SmallInt dataProviderID;//id in table DatabaseProviders
        }
    }
    public static class Table_Cuisines//archive
    {
        public const string Header = "[dbo].[Cuisines]";
        public const string Columns = "([ID],[name_locale],[original_country_ID],[name_English])";
        public class Cuisine
        {
            public Cuisine(string LocaleName)
            {
                Name_locale = LocaleName;
            }
            private SmallInt ID;//internally assigned id, never change;primary key
            public string Name_locale//not null
            {
                private set;get;
            }
            public string Name_English
            {
                set { name_English = value;is_name_English_init = true; }
                get { return name_English; }
            }
            public SmallInt Original_country_ID
            {
                set { original_country_ID = value;is_original_country_ID_init = true; }
                get { return original_country_ID; }
            }

            protected internal bool is_name_English_init = false;
            private string name_English;
            protected internal bool is_original_country_ID_init = false;
            private SmallInt original_country_ID;
        }
    }
    public static class Table_RatingComments//may need frequent update
    {
        public const string Header = "[dbo].[RatingComments]";
        public const string Columns = "([ID],[rating],[user_name],[user_level],[comment],[DataProviderID],[nationID])";
        public class RatingComment
        {
            public RatingComment(SmallInt rating)
            {
                Rating = rating;
            }
            private BigInt ID;//internally assigned id, never change;primary key
            public SmallInt Rating//0~100;not null
            { private set; get; }
            public string User_name
            {
                set { user_name = value;is_user_name_init = true; }
                get { return user_name; }
            }
            public SmallInt User_level//0~100
            {
                set { user_level = value;is_user_level_init = true; }
                get { return user_level; }
            }
            public Int Likes
            {
                set { likes = value; is_likes_init = true; }
                get { return likes; }
            }
            public string Comment
            {
                set { comment = value;is_comment_init = true; }
                get { return comment; }
            }
            public SmallInt DataProviderID//id in table DatabaseProviders
            {
                set { dataProviderID = value;is_dataProviderID_init = true; }
                get { return dataProviderID; }
            }
            public SmallInt NationID//user's nationality
            {
                set { nationID = value;is_nationID_init = true; }
                get { return nationID; }
            }

            protected internal bool is_user_name_init = false;
            private string user_name;
            protected internal bool is_user_level_init = false;
            private SmallInt user_level;//0~100
            protected internal bool is_likes_init = false;
            private Int likes;
            protected internal bool is_comment_init = false;
            private string comment;
            protected internal bool is_dataProviderID_init = false;
            private SmallInt dataProviderID;//id in table DatabaseProviders
            protected internal bool is_nationID_init = false;
            private SmallInt nationID;//user's nationality
        }
    }
    private static class Union_Table_Nations_Cities//archive
    {
        public const string Header = "[dbo].[Nations_Cities]";
        public const string Columns = "([nation_ID],[city_ID])";
        public class Nation_City_Union
        {
            public SmallInt nation_ID;//not null
            public SmallInt city_ID;//not null
        }
    }
    private static class Union_Table_Cities_Restaurants
    {
        public const string Header = "[dbo].[Cities_Restaurants]";
        public const string Columns = "([city_ID],[restaurant_ID])";
        public class City_Restaurant_Union
        {
            public SmallInt city_ID;//primary key
            public Int restaurant_ID;//not null
        }
    }
    private static class Union_Table_Cities_Cuisines
    {
        public const string Header = "Cities_Cuisines";
        public const string Columns = "([city_ID],[cuisine_ID])";
        public class City_Cuisine_Union
        {
            public SmallInt city_ID;//primary key
            public SmallInt cuisine_ID;//not null
        }
    }
    private static class Union_Table_Restaurants_Cuisines
    {
        public const string Header = "[dbo].[Restaurants_Cuisines]";
        public const string Columns = "([restaurant_ID],[cuisine_ID])";
        public class Restaurant_Cuisine_Union
        {
            public Int restaurant_ID;//not null
            public SmallInt cuisine_ID;//not null
        }
    }
    private static class Union_Table_Restaurants_Dishes
    {
        public const string Header = "[dbo].[Restaurants_Dishes]";
        public const string Columns = "([restaurant_ID],[dish_ID])";
        public class Restaurants_Dish
        {
            public Int restaurant_ID;//not null
            public BigInt dish_ID;//not null
        }
    }
    private static class Union_Table_Restaurants_RatingComments
    {
        public const string Header = "[dbo].[Restaurants_RatingComments]";
        public const string Columns = "([restaurant_ID],[ratingComment_ID])";
        public class Restaurant_RatingComment
        {
            public Int restaurant_ID;//primary key
            public BigInt ratingComment_ID;//not null
        }
    }
    private static class Union_Table_Dishes_Cuisines
    {
        public const string Header = "[dbo].[Dishes_Cuisines]";
        public const string Columns = "([dish_ID],[cuisine_ID])";
        public class Dish_Cuisine
        {
            public BigInt dish_ID;//not null
            public SmallInt cuisine_ID;//not null;
        }
    }
    private static class Union_Table_Dishes_RatingComments
    {
        public const string Header = "[dbo].[Dishes_RatingComments]";
        public const string Columns = "([dish_ID],[ratingComment_ID])";
        public class Dish_RatingComment
        {
            public BigInt dish_ID;//primary key
            public BigInt ratingComment_ID;//not null
        }
    }

    public class InvalidParameterException : Exception
    {

    }

    //
    // Summary:
    //     check for exist IDs and give a new city id
    //
    // Returns:
    //     new city's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    //   T:System.OverflowException:
    //     all id uesd up
    private static Task<SmallInt> GenerateNewCityID(SqlConnection connection)
    {
        SmallInt result = SmallInt.MinValue;
        using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_Cities.Header+"ORDER BY[ID]ASC", connection))
        {
            using (SqlDataReader reader = GetIDs.ExecuteReader())
            {
                SmallInt formerID = SmallInt.MinValue;
                bool found = false;
                bool first = true;
                while (reader.Read())
                {
                    SmallInt curID = (SmallInt)reader[0];
                    if (curID == SmallInt.MaxValue) throw new System.OverflowException();
                    if (curID > formerID + 1)
                    {
                        result = (SmallInt)(formerID + 1);
                        found = true;
                        break;
                    }
                    first = false;
                    formerID = curID;
                }
                if (!found)
                {
                    if (first) result = SmallInt.MinValue;
                    else result = (SmallInt)(formerID + 1);
                }
            }
        }
        return Task.FromResult<SmallInt>(result);
    }
    //
    // Summary:
    //     check for exist IDs and give a new restaurant id
    //
    // Returns:
    //     new restaurant's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    //   T:System.OverflowException:
    //     all id uesd up
    private static Task<Int> GenerateNewRestaurantID(SqlConnection connection)
    {
        Int result = Int.MinValue;
        using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_Restaurants.Header+"ORDER BY[ID]ASC", connection))
        {
            using (SqlDataReader reader = GetIDs.ExecuteReader())
            {
                Int formerID = Int.MinValue;
                bool found = false;
                bool first = true;
                while (reader.Read())
                {
                    Int curID = (Int)reader[0];
                    if (curID == Int.MaxValue) throw new System.OverflowException();
                    if (curID > formerID + 1)
                    {
                        result = (Int)(formerID + 1);
                        found = true;
                        break;
                    }
                    first = false;
                    formerID = curID;
                }
                if (!found)
                {
                    if (first) result = Int.MinValue;
                    else result = (Int)(formerID + 1);
                }
            }
        }
        return Task.FromResult<Int>(result);
    }
    //
    // Summary:
    //     check for exist IDs and give a new provider id
    //
    // Returns:
    //     new provider's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    //   T:System.OverflowException:
    //     all id uesd up
    private static Task<SmallInt> GenerateNewProviderID(SqlConnection connection)
    {
        SmallInt result = SmallInt.MinValue;
        using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_DatabaseProviders.Header+"ORDER BY[ID]ASC", connection))
        {
            using (SqlDataReader reader = GetIDs.ExecuteReader())
            {
                SmallInt formerID = SmallInt.MinValue;
                bool found = false;
                bool first = true;
                while (reader.Read())
                {
                    SmallInt curID = (SmallInt)reader[0];
                    if (curID == SmallInt.MaxValue) throw new System.OverflowException();
                    if (curID > formerID + 1)
                    {
                        result = (SmallInt)(formerID + 1);
                        found = true;
                        break;
                    }
                    first = false;
                    formerID = curID;
                }
                if (!found)
                {
                    if (first) result = SmallInt.MinValue;
                    else result = (SmallInt)(formerID + 1);
                }
            }
        }
        return Task.FromResult<SmallInt>(result);
    }
    //
    // Summary:
    //     check for exist IDs and give a new dish id
    //
    // Returns:
    //     new dish's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    //   T:System.OverflowException:
    //     all id uesd up
    private static Task<BigInt> GenerateNewDishID(SqlConnection connection)
    {
        BigInt result = BigInt.MinValue;
        using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_Dishes.Header+"ORDER BY[ID]ASC", connection))
        {
            using (SqlDataReader reader = GetIDs.ExecuteReader())
            {
                BigInt formerID = BigInt.MinValue;
                bool found = false;
                bool first = true;
                while (reader.Read())
                {
                    BigInt curID = (BigInt)reader[0];
                    if (curID == BigInt.MaxValue) throw new System.OverflowException();
                    if (curID > formerID + 1)
                    {
                        result = (BigInt)(formerID + 1);
                        found = true;
                        break;
                    }
                    first = false;
                    formerID = curID;
                }
                if (!found)
                {
                    if (first) result = BigInt.MinValue;
                    else result = (BigInt)(formerID + 1);
                }
            }
        }
        return Task.FromResult<BigInt>(result);
    }
    //
    // Summary:
    //     check for exist IDs and give a new cuisine id
    //
    // Returns:
    //     new cuisine's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    //   T:System.OverflowException:
    //     all id uesd up
    private static Task<SmallInt> GenerateNewCuisineID(SqlConnection connection)
    {
        SmallInt result = SmallInt.MinValue;
        using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_Cuisines.Header + "ORDER BY[ID]ASC", connection))
        {
            using (SqlDataReader reader = GetIDs.ExecuteReader())
            {
                SmallInt formerID = SmallInt.MinValue;
                bool found = false;
                bool first = true;
                while (reader.Read())
                {
                    SmallInt curID = (SmallInt)reader[0];
                    if (curID == SmallInt.MaxValue) throw new System.OverflowException();
                    if (curID > formerID + 1)
                    {
                        result = (SmallInt)(formerID + 1);
                        found = true;
                        break;
                    }
                    first = false;
                    formerID = curID;
                }
                if (!found)
                {
                    if (first) result = SmallInt.MinValue;
                    else result = (SmallInt)(formerID + 1);
                }
            }
        }
        return Task.FromResult<SmallInt>(result);
    }
    //
    // Summary:
    //     check for exist IDs and give a new rating id
    //
    // Returns:
    //     new rating's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    //   T:System.OverflowException:
    //     all id uesd up
    private static Task<BigInt> GenerateNewRatingCommentID(SqlConnection connection)
    {
        BigInt result = BigInt.MinValue;
        using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_RatingComments.Header + "ORDER BY[ID]ASC", connection))
        {
            using (SqlDataReader reader = GetIDs.ExecuteReader())
            {
                BigInt formerID = BigInt.MinValue;
                bool found = false;
                bool first = true;
                while (reader.Read())
                {
                    BigInt curID = (BigInt)reader[0];
                    if (curID == BigInt.MaxValue) throw new System.OverflowException();
                    if (curID > formerID + 1)
                    {
                        result = (BigInt)(formerID + 1);
                        found = true;
                        break;
                    }
                    first = false;
                    formerID = curID;
                }
                if (!found)
                {
                    if (first) result = BigInt.MinValue;
                    else result = (BigInt)(formerID + 1);
                }
            }
        }
        return Task.FromResult<BigInt>(result);
    }
    //
    // Summary:
    //     check for the provider
    //
    // Returns:
    //     whether contains the provider
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static Task<bool> ContainsProvider(SqlConnection connection,string name)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_DatabaseProviders.Header+"WHERE[name]=@name", connection))
        {
            Check.Parameters.AddWithValue("@name", name);
            using (SqlDataReader reader = Check.ExecuteReader())
                if (reader.Read()) result = true;
        }
        return Task.FromResult<bool>(result);
    }
    public static Task<bool> ContainsProvider(SqlConnection connection,SmallInt providerID)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_DatabaseProviders.Header+"WHERE[ID]=@providerID", connection))
        {
            Check.Parameters.AddWithValue("@providerID", providerID);
            using (SqlDataReader reader = Check.ExecuteReader())
                if (reader.Read()) result = true;
        }
        return Task.FromResult<bool>(result);
    }
    //
    // Summary:
    //     check for the nation
    //
    // Returns:
    //     whether contains the nation
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static Task<bool> ContainsNation(SqlConnection connection,SmallInt ISO3166NationNumericCode)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_Nations.Header+"WHERE[ID]=@nationID", connection))
        {
            Check.Parameters.AddWithValue("@nationID", ISO3166NationNumericCode);
            using (SqlDataReader reader = Check.ExecuteReader())
                if (reader.Read()) result = true;
        }
        return Task.FromResult<bool>(result);
    }
    //
    // Summary:
    //     check for the city
    //
    // Returns:
    //     whether contains the city
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static Task<bool> ContainsCity(SqlConnection connection,SmallInt cityID)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_Cities.Header+"WHERE[ID]=@cityID", connection))
        {
            Check.Parameters.AddWithValue("@cityID", cityID);
            using (SqlDataReader reader = Check.ExecuteReader())
                if (reader.Read()) result = true;
        }
        return Task.FromResult<bool>(result);
    }
    public static Task<bool> ContainsCity(SqlConnection connection,string LocaleName)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_Cities.Header+"WHERE[name_locale]=@localeName", connection))
        {
            Check.Parameters.AddWithValue("@localeName", LocaleName);
            using (SqlDataReader reader = Check.ExecuteReader())
                if (reader.Read()) result = true;
        }
        return Task.FromResult<bool>(result);
    }
    //
    // Summary:
    //     check for the eestaurant
    //
    // Returns:
    //     whether contains the restaurant
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static Task<bool> ContainsRestaurant(SqlConnection connection,SmallInt cityID, string name )
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_Restaurants.Header+"WHERE[name]=@name AND [city_ID]=@cityID", connection))
        {
            Check.Parameters.AddWithValue("@name", name);
            Check.Parameters.AddWithValue("@cityID", cityID);
            using (SqlDataReader reader = Check.ExecuteReader())
                if (reader.Read()) result = true;
        }
        return Task.FromResult<bool>(result);
    }
    //
    // Summary:
    //     add new provider
    //
    // Return:
    //   new provider's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //     city name not exist
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static Task<SmallInt> GetCityID(SqlConnection connection,string LocaleName)
    {
        SmallInt ID=0;
        using(SqlCommand GetID = new SqlCommand("SELECT[ID]FROM"+DatabaseHeader
            +Table_Cities.Header+"WHERE[name_locale]=@localeName", connection))
        {
            GetID.Parameters.AddWithValue("@localeName", LocaleName);
            using (SqlDataReader reader = GetID.ExecuteReader())
            {
                reader.Read();
                ID = (SmallInt)reader[0];
            }
                
        }
        return Task.FromResult<SmallInt>(ID);
    }
    //
    // Summary:
    //     add new provider
    //
    // Return:
    //   new provider's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static async Task<SmallInt> AddProviderAsync(SqlConnection connection, string name, string homepageURL)//fixed
    {
        Task<SmallInt> newID = GenerateNewProviderID(connection);
        using (SqlCommand Insertion = new SqlCommand(
            "INSERT INTO"+DatabaseHeader+Table_DatabaseProviders.Header+Table_DatabaseProviders.Columns
            +"VALUES(@id,@name,@homepage)", connection))
        {
            Insertion.Parameters.AddWithValue("@name", name);
            Insertion.Parameters.AddWithValue("@homepage", homepageURL);
            Insertion.Parameters.AddWithValue("@id", await newID);
            Insertion.ExecuteNonQuery();
        }
        return await newID;
    }
    public static async Task<SmallInt> AddProviderAsync(SqlConnection connection, string name)
    {
        Task<SmallInt> newID = GenerateNewProviderID(connection);
        using (SqlCommand Insertion = new SqlCommand(
            "INSERT INTO" + DatabaseHeader + Table_DatabaseProviders.Header + Table_DatabaseProviders.Columns
            + "VALUES(@id,@name,@homepage)", connection))
        {
            Insertion.Parameters.AddWithValue("@name", name);
            Insertion.Parameters.AddWithValue("@homepage", DBNull.Value);
            Insertion.Parameters.AddWithValue("@id", await newID);
            Insertion.ExecuteNonQuery();
        }
        return await newID;
    }
    //
    // Summary:
    //     add new nation
    //
    // Parameters:
    //   ID: ISO 3166 Country Numeric Code
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static Task AddNation(SmallInt ID, SqlConnection connection, string EnglishName, string LocaleName, string currencyCode)
    {
        using (SqlCommand Insertion = new SqlCommand("INSERT INTO"+DatabaseHeader+Table_Nations.Header+Table_Nations.Columns
            +"VALUES(@id,@EnglishName,@LocaleName,@currency)", connection))
        {
            Insertion.Parameters.AddWithValue("@id", ID);
            Insertion.Parameters.AddWithValue("@EnglishName", EnglishName);
            Insertion.Parameters.AddWithValue("@LocaleName", LocaleName);
            Insertion.Parameters.AddWithValue("@currency", currencyCode);
            Insertion.ExecuteNonQuery();
        }
        return Task.CompletedTask;
    }
    //
    // Summary:
    //   add a new city
    //
    // Return:
    //   new city's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static async Task<SmallInt> AddCityAsync(SqlConnection connection, SmallInt nationID, string LocaleName)
    {
        Task<SmallInt> newID = GenerateNewCityID(connection);
        //insert into Cities
        using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Cities.Header + Table_Cities.Columns
            + "VALUES(@id,@LocaleName,@EnglishName)", connection))
        {
            Insertion.Parameters.AddWithValue("@LocaleName", LocaleName);
            Insertion.Parameters.AddWithValue("@EnglishName", DBNull.Value);
            Insertion.Parameters.AddWithValue("@id", await newID);
            Insertion.ExecuteNonQuery();
        }
        //update Table Nations_Cities
        using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Union_Table_Nations_Cities.Header + Union_Table_Nations_Cities.Columns
            + "VALUES(@nationID,@cityID)", connection))
        {
            Insertion.Parameters.AddWithValue("@nationID", nationID);
            Insertion.Parameters.AddWithValue("@cityID", await newID);
            Insertion.ExecuteNonQuery();
        }
        return await newID;
    }
    public static async Task<SmallInt> AddCityAsync(SqlConnection connection, SmallInt nationID, string LocaleName, string EnglishName)
    {
        Task<SmallInt> newID = GenerateNewCityID(connection);
        using (SqlCommand Insertion = new SqlCommand("INSERT INTO"+DatabaseHeader+Table_Cities.Header+Table_Cities.Columns
            + "VALUES(@id,@LocaleName,@EnglishName)", connection))
        {
            Insertion.Parameters.AddWithValue("@LocaleName", LocaleName);
            Insertion.Parameters.AddWithValue("@EnglishName", EnglishName);
            Insertion.Parameters.AddWithValue("@id", await newID);
            Insertion.ExecuteNonQuery();
        }
        //update Table Nations_Cities
        using (SqlCommand Insertion = new SqlCommand("INSERT INTO"+DatabaseHeader+Union_Table_Nations_Cities.Header+Union_Table_Nations_Cities.Columns
            +"VALUES(@nationID,@cityID)", connection))
        {
            Insertion.Parameters.AddWithValue("@nationID", nationID);
            Insertion.Parameters.AddWithValue("@cityID", await newID);
            Insertion.ExecuteNonQuery();
        }
        return await newID;
    }
    //
    // Summary:
    //   add a new restaurant
    //
    // Parameters:
    //   bool[] init: 
    //     array of initialized fields' (by order). For example: bool[0]==true means city_ID will be assigned the passed value
    //     length can be any value which greater than 3 (front 3 fields must be assigned)
    //     default value is false
    //
    // Return:
    //   new restaurant's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static async Task<Int> AddRestaurantAsync(SqlConnection connection, Table_Restaurants.Restaurant res)
    {
        Task<Int> newID = GenerateNewRestaurantID(connection);
        string insertCommandString = "INSERT INTO" +DatabaseHeader+Table_Restaurants.Header+Table_Restaurants.Columns
            +"VALUES(@id,@cityID,@name,@address,@description,@lat,@lon,@zip,@currencyID,@type,@open,@close,@costOne,@costTwo,@delivery,@image,@photo,@event,@homepage,@providerID)";
        using(SqlCommand Insertion = new SqlCommand(insertCommandString, connection))
        {
            Insertion.Parameters.AddWithValue("@cityID", res.City_ID);
            Insertion.Parameters.AddWithValue("@name", res.Name);
            Insertion.Parameters.AddWithValue("@address", res.Locality_address);
            if (res.is_description_init) Insertion.Parameters.AddWithValue("@description", res.Description);
            else Insertion.Parameters.AddWithValue("@description", DBNull.Value);
            if(res.is_locality_latitude_init) Insertion.Parameters.AddWithValue("@lat", res.Locality_latitude);
            else Insertion.Parameters.AddWithValue("@lat", DBNull.Value);
            if (res.is_locality_longitude_init) Insertion.Parameters.AddWithValue("@lon", res.Locality_longitude);
            else Insertion.Parameters.AddWithValue("@lon", DBNull.Value);
            if (res.is_locality_zipcode_init) Insertion.Parameters.AddWithValue("@zip", res.Locality_zipcode);
            else Insertion.Parameters.AddWithValue("@zip", DBNull.Value);
            if (res.is_currency_Nation_ID_init) Insertion.Parameters.AddWithValue("@currencyID", res.Currency_Nation_ID);
            else Insertion.Parameters.AddWithValue("@currencyID", DBNull.Value);
            if (res.is_restaurant_type_init) Insertion.Parameters.AddWithValue("@type", res.Restaurant_type);
            else Insertion.Parameters.AddWithValue("@type", DBNull.Value);
            if (res.is_open_time_init) Insertion.Parameters.AddWithValue("@open", res.Open_time);
            else Insertion.Parameters.AddWithValue("@open", DBNull.Value);
            if (res.is_close_time_init) Insertion.Parameters.AddWithValue("@close", res.Close_time);
            else Insertion.Parameters.AddWithValue("@close", DBNull.Value);
            if (res.is_average_cost_by_one_init) Insertion.Parameters.AddWithValue("@costOne", res.Average_cost_by_one);
            else Insertion.Parameters.AddWithValue("@costOne", DBNull.Value);
            if (res.is_average_cost_by_two_init) Insertion.Parameters.AddWithValue("@costTwo", res.Average_cost_by_two);
            else Insertion.Parameters.AddWithValue("@costTwo", DBNull.Value);
            if (res.is_is_delivering_init)Insertion.Parameters.AddWithValue("@delivery", res.Is_delivering);
            else Insertion.Parameters.AddWithValue("@delivery", DBNull.Value);
            if (res.is_imageURL_init) Insertion.Parameters.AddWithValue("@image", res.ImageURL);
            else Insertion.Parameters.AddWithValue("@image", DBNull.Value);
            if (res.is_photoURL_init) Insertion.Parameters.AddWithValue("@photo", res.PhotosURL);
            else Insertion.Parameters.AddWithValue("@photo", DBNull.Value);
            if (res.is_eventsURL_init) Insertion.Parameters.AddWithValue("@event", res.EventsURL);
            else Insertion.Parameters.AddWithValue("@event", DBNull.Value);
            if (res.is_homepageURL_init) Insertion.Parameters.AddWithValue("@homepage", res.HomepageURL);
            else Insertion.Parameters.AddWithValue("@homepage", DBNull.Value);
            if (res.is_dataProviderID_init) Insertion.Parameters.AddWithValue("@providerID", res.DataProviderID);
            else Insertion.Parameters.AddWithValue("@providerID", DBNull.Value);
            Insertion.Parameters.AddWithValue("@id", await newID);
            Insertion.ExecuteNonQuery();
        }
        //update uinon table Cities_Restaurant
        using (SqlCommand Insertion = new SqlCommand("INSERT INTO[Database_Encapsulation].[dbo].[Cities_Restaurants]([city_ID],[restaurant_ID])VALUES(@cityID,@resID)", connection))
        {
            Insertion.Parameters.AddWithValue("@cityID", res.City_ID);
            Insertion.Parameters.AddWithValue("@resID", await newID);
            Insertion.ExecuteNonQuery();
        }
        return await newID;
    }
    //
    // Summary:
    //   add a new dish
    //
    // Return:
    //   new dish's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static async Task<BigInt> AddDishAsync(SqlConnection connection, Int restaurantID, Table_Dishes.Dish dish)
    {
        Task<BigInt> newID = GenerateNewDishID(connection);
        string insertionCommandString = "INSERT INTO"+DatabaseHeader+Table_Dishes.Header+Table_Dishes.Columns
            +"VALUES(@id,@name,@price,@description,@cuisineID,@currencyID,@photo,@providerID)";
        using (SqlCommand Insertion = new SqlCommand(insertionCommandString, connection))
        {
            Insertion.Parameters.AddWithValue("@name", dish.Name);
            Insertion.Parameters.AddWithValue("@price", dish.Price);
            if (dish.is_description_init) Insertion.Parameters.AddWithValue("@description", dish.Description);
            else Insertion.Parameters.AddWithValue("@description", DBNull.Value);
            if (dish.is_cuisine_ID_init) Insertion.Parameters.AddWithValue("@cuisineID", dish.Cuisine_ID);
            else Insertion.Parameters.AddWithValue("@cuisineID", DBNull.Value);
            if (dish.is_currency_Nation_ID_init) Insertion.Parameters.AddWithValue("@currencyID", dish.Currency_Nation_ID);
            else Insertion.Parameters.AddWithValue("@currencyID", DBNull.Value);
            if (dish.is_photosURL_init) Insertion.Parameters.AddWithValue("@photo", dish.PhotosURL);
            else Insertion.Parameters.AddWithValue("@photo", DBNull.Value);
            if (dish.is_dataProviderID_init) Insertion.Parameters.AddWithValue("@providerID", dish.DataProviderID);
            else Insertion.Parameters.AddWithValue("@providerID", DBNull.Value);
            Insertion.Parameters.AddWithValue("@id", await newID);
            Insertion.ExecuteNonQuery();
        }
        //update Restaurants_Dishes
        insertionCommandString = "INSERT INTO" + DatabaseHeader + Union_Table_Restaurants_Dishes.Header + Union_Table_Restaurants_Dishes.Columns
            + "VALUES(@resID,@dishID)";
        using (SqlCommand Insertion = new SqlCommand(insertionCommandString,connection))
        {
            Insertion.Parameters.AddWithValue("@resID", restaurantID);
            Insertion.Parameters.AddWithValue("@dishID", await newID);
            Insertion.ExecuteNonQuery();
        }
        return await newID;
    }
    //
    // Summary:
    //   add a new cuisine
    //
    // Return:
    //   new cuisine's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static async Task<SmallInt> AddCuisineAsync(SqlConnection connection,Table_Cuisines.Cuisine cuisine)
    {
        Task<SmallInt> newID = GenerateNewCuisineID(connection);
        string insertionCommandString = "INSERT INTO" + DatabaseHeader + Table_Cuisines.Header + Table_Cuisines.Columns
            + "VALUES(@id,@localeName,@originalID,@EnglishName)";
        using (SqlCommand Insertion = new SqlCommand(insertionCommandString, connection))
        {
            Insertion.Parameters.AddWithValue("@localeName", cuisine.Name_locale);
            if (cuisine.is_original_country_ID_init) Insertion.Parameters.AddWithValue("@originalID", cuisine.Original_country_ID);
            else Insertion.Parameters.AddWithValue("@originalID", DBNull.Value);
            if (cuisine.is_name_English_init) Insertion.Parameters.AddWithValue("@EnglishName", cuisine.Name_English);
            else Insertion.Parameters.AddWithValue("@EnglishName", DBNull.Value);
            Insertion.Parameters.AddWithValue("@id", await newID);
            Insertion.ExecuteNonQuery();
        }
        return await newID;
    }
    //
    // Summary:
    //   add a new user rating
    //
    // Parameters:
    //   resID: indicate that this is a rating to a restaurant, resID is the ID of the restaurant
    //   dishID: indicate that this is a rating to a dish, dishID is the ID of the dish
    //
    // Return:
    //   new user rating's id
    //
    // Exceptions:
    //   T:System.InvalidCastException:
    //   T:System.Data.SqlClient.SqlException:
    //   T:System.InvalidOperationException:
    //   T:System.IO.IOException:
    //   T:System.ObjectDisposedException:
    public static async Task<BigInt> AddRatingCommentAsync(SqlConnection connection,SmallInt resID,Table_RatingComments.RatingComment rat)
    {
        Task<BigInt> newID = GenerateNewRatingCommentID(connection);
        string insertionCommandString = "INSERT INTO" + DatabaseHeader + Table_RatingComments.Header + Table_RatingComments.Columns
            + "VALUES(@id,@rating,@userName,@userLevel,@comment,@providerID,@nationID)";
        using (SqlCommand Insertion = new SqlCommand(insertionCommandString, connection))
        {
            Insertion.Parameters.AddWithValue("@rating", rat.Rating);
            if (rat.is_user_name_init) Insertion.Parameters.AddWithValue("@userName", rat.User_name);
            else Insertion.Parameters.AddWithValue("@userName", DBNull.Value);
            if (rat.is_user_level_init) Insertion.Parameters.AddWithValue("@userLevel", rat.User_level);
            else Insertion.Parameters.AddWithValue("@userLevel", DBNull.Value);
            if (rat.is_comment_init) Insertion.Parameters.AddWithValue("@comment", rat.Comment);
            else Insertion.Parameters.AddWithValue("@comment", DBNull.Value);
            if (rat.is_dataProviderID_init) Insertion.Parameters.AddWithValue("@providerID", rat.DataProviderID);
            else Insertion.Parameters.AddWithValue("@providerID", DBNull.Value);
            if (rat.is_nationID_init) Insertion.Parameters.AddWithValue("@nationID", rat.NationID);
            else Insertion.Parameters.AddWithValue("@nationID", DBNull.Value);
            Insertion.Parameters.AddWithValue("@id", await newID);
            Insertion.ExecuteNonQuery();
        }
        //update Restaurants_RatingComments
        insertionCommandString = "INSERT INTO" + DatabaseHeader + Union_Table_Restaurants_RatingComments.Header + Union_Table_Restaurants_RatingComments.Columns
            + "VALUES(@resID,@ratID)";
        using (SqlCommand Insertion = new SqlCommand(insertionCommandString, connection))
        {
            Insertion.Parameters.AddWithValue("@resID", resID);
            Insertion.Parameters.AddWithValue("@ratID", await newID);
            Insertion.ExecuteNonQuery();
        }
        return await newID;
    }
    public static async Task<BigInt> AddRatingCommentAsync(SqlConnection connection,BigInt dishID,bool isDishRating ,Table_RatingComments.RatingComment rat)
    {
        Task<BigInt> newID = GenerateNewRatingCommentID(connection);
        string insertionCommandString = "INSERT INTO" + DatabaseHeader +Table_RatingComments.Header+Table_RatingComments.Columns
            + "VALUES(@id,@rating,@userName,@userLevel,@comment,@providerID,@nationID)";
        using (SqlCommand Insertion = new SqlCommand(insertionCommandString, connection))
        {
            Insertion.Parameters.AddWithValue("@rating", rat.Rating);
            if (rat.is_user_name_init) Insertion.Parameters.AddWithValue("@userName", rat.User_name);
            else Insertion.Parameters.AddWithValue("@userName", DBNull.Value);
            if (rat.is_user_level_init) Insertion.Parameters.AddWithValue("@userLevel", rat.User_level);
            else Insertion.Parameters.AddWithValue("@userLevel", DBNull.Value);
            if (rat.is_comment_init) Insertion.Parameters.AddWithValue("@comment", rat.Comment);
            else Insertion.Parameters.AddWithValue("@comment", DBNull.Value);
            if (rat.is_dataProviderID_init) Insertion.Parameters.AddWithValue("@providerID", rat.DataProviderID);
            else Insertion.Parameters.AddWithValue("@providerID", DBNull.Value);
            if (rat.is_nationID_init) Insertion.Parameters.AddWithValue("@nationID", rat.NationID);
            else Insertion.Parameters.AddWithValue("@nationID", DBNull.Value);
            Insertion.Parameters.AddWithValue("@id", await newID);
            Insertion.ExecuteNonQuery();
        }
        //update Restaurants_RatingComments
        insertionCommandString = "INSERT INTO" + DatabaseHeader +Union_Table_Dishes_RatingComments.Header+Union_Table_Dishes_RatingComments.Columns
            + "VALUES(@dishID,@ratID)";
        using (SqlCommand Insertion = new SqlCommand(insertionCommandString, connection))
        {
            Insertion.Parameters.AddWithValue("@dishID", dishID);
            Insertion.Parameters.AddWithValue("@ratID", await newID);
            Insertion.ExecuteNonQuery();
        }
        return await newID;
    }
}
