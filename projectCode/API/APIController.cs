using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bot_Application.API
{
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
                public Int16 id;
                public string name;
            }
            public struct CatergoriesListItem
            {
                public Catergory catergory;
            }
            public CatergoriesListItem[] categories;
        }
        public class CitiesJson
        {
            public struct City
            {
                public int id;
                public string name;
                public Int16 country_id;//optional
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
                public UInt64 collection_id;//optional
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
                public UInt64 cuisine_id;
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
            public struct RestaurantID { public UInt64 res_id; }//useless
            RestaurantID R;//useless
            public string apikey;
            public string id;
            public string name;
            public string url;
            public ResLocation location;
            public UInt64 switch_to_order_menu;
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
            public UInt64 reviews_count;
            public UInt64 reviews_start;
            public UInt16 reviews_shown;
            public ReviewsListItem[] user_reviews;
            public string Respond_to_reviews_via_Zomato_Dashboard;//url
        }

        public class Location
        {
            public string entity_type;//optional;Type of location; one of [city, zone, subzone, landmark, group, metro, street]
            public int entity_id;//optional;ID of location; (entity_id, entity_type) tuple uniquely identifies a location ,ID of location; (entity_id, entity_type) tuple uniquely identifies a location
            public string title;//optional;Name of the location
            public double latitude;//optional;Coordinates of the (centre of) location
            public double longitude;//optional;Coordinates of the (centre of) location
            public int city_id;//optional;ID of city
            public string city_name;//optional;Name of the city
            public Int16 country_id;//optional;ID of country
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
            public UInt64 id;//optional;ID of the restaurant
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
            public Int16 country_id;//ID of the country 
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
            public int res_id;//ID of restaurant for which the image was uploaded
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
            public UInt64 timestamp;//Unix timestamp for review_time_friendly
            public int likes;//No of likes received for review
            public User user;//User details of author of review
            public int comments_count;//No of comments on review 
        }
        public class User
        {
            public string name;//optional;User's name;
            public string zomato_handle;//optional;User's @handle; uniquely identifies a user on Zomato
            public string foodie_level;//optional;Text for user's foodie level
            public Int16 foodie_level_num;//optional;Number to identify user's foodie level; ranges from 0 to 10 
            public string foodie_color;//optional;Color hex code used with foodie level on Zomato
            public string profile_url;//optional;URL for user's profile on Zomato
            public string profile_deeplink;//optional;short URL for user's profile on Zomato; for use in apps or social sharing
            public string profile_image;//optional;URL for user's profile image
        }
        public class DailyMenuCategory
        {
            public UInt64 daily_menu_id;//optional;ID of the restaurant
            public string name;//optional;Name of the restaurant 
            public string start_date;//optional;Daily Menu start timestamp;
            public string end_date;//optional;Daily Menu end timestamp
            public DailyMenuItem[] dishes;//optional;Menu item in the category
        }
        public class DailyMenuItem
        {
            public UInt64 dish_id;//optional;Menu Iten ID
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
        public async Task<CitiesJson> GetCitiesAsync(string cityName, UInt64 count= UInt64.MaxValue)
        {
            HttpResponseMessage response = await this.GetAsync("cities?q=" + HttpUtility.UrlEncode(cityName)+"&count="+count, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<CitiesJson>();
            }
            else return null;
        }
        public async Task<CitiesJson> GetCitiesAsync(int[] cityIDs)
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
        public async Task<LocationJson> GetLocationAsync(string queryName, UInt64 count= UInt64.MaxValue)
        {
            HttpResponseMessage response = await this.GetAsync("locations?query=" + HttpUtility.UrlEncode(queryName)+"&count="+count, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<LocationJson>();
            }
            else return null;
        }
        public async Task<LocationJson> GetLocationAsync(string queryName, double lat, double lon, UInt64 count = UInt64.MaxValue)
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
        public async Task<DailyMenuJson> GetDailyMenuAsync(UInt64 res_id)
        {
            HttpResponseMessage response = await this.GetAsync("dailymenu?res_id=" + res_id, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<DailyMenuJson>();
            }
            else return null;
        }
        /*Get detailed restaurant information using Zomato restaurant ID. Partner Access is required to access photos and reviews*/
        public async Task<RestaurantJson> GetRestaurantAsync(UInt64 res_id)
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
        public async Task<ReviewsJson> GetReviewsAsync(UInt64 res_id, UInt64 start=0, UInt64 count = UInt64.MaxValue)
        {
            HttpResponseMessage response = await this.GetAsync("reviews?res_id=" + res_id + "&start=" + start + "&count=" + count, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ReviewsJson>();
            }
            else return null;
        }
    }
}
