using System;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using SmallInt = System.Int16;
using Int = System.Int32;
using BigInt = System.Int64;
using TinyInt = System.Byte;

namespace Bot_Application.API
{
    public class YelpClient : HttpClient
    {
        //doc https://www.yelp.com/developers/documentation/v3/get_started
        static readonly string defaultApiKey = "_9OMQKqPnoC-XconiQ6HR5l5Z_edOEb12rKFMXh9OUlQYJ_AEFmvsbWihrL2r5kWOpR7Sz3SxXeaSW_vHNNXMvtHER_gmbZizzBfaW20RsP3Nf3pkS6KYFXTQZmOWnYx";
        static readonly string defaultClientID = "iHSYKEmFeOvvOjM4d58kbw";
        [Serializable]
        public class CoordinatesJsonObject
        {
            public decimal? latitude;
            public decimal? longitude;
            public bool ShouldSerializelatitude() => latitude != null;
            public void Resetlatitude() => latitude = null;
        }
        [Serializable]
        public class CategoriesJsonObject
        {
            public string alias;//Alias of a category, when searching for business in certain categories, use alias rather than the title.
            public string title;//Title of a category for display purpose.
        }
        [Serializable]
        public class LocationJsonObject
        {
            public string address1;//Street address of this business.
            public string address2;//Street address of this business, continued.
            public string address3;//Street address of this business, continued.
            public string city;//City of this business.
            public string country;//ISO 3166-1 alpha-2 country code of this business.
            public string[] display_address;//Array of strings that if organized vertically give an address that is in the standard address format for the business's country.
            public string state;//ISO 3166-2 (with a few exceptions) state code of this business.
            public string zip_code;
        }
        [Serializable]
        public class UserJsonObject
        {
            public string name;
            public string image_url;//URL of the user's profile photo.
        }
        [Serializable]
        public class BusinessesJsonObject
        {
            public string id;
            public string name;
            public string phone;
            public string price;//Price level of the business. Value is one of $, $$, $$$ and $$$$.
            public float? rating;//Rating for this business (value ranges from 1, 1.5, ... 4.5, 5).
            public CategoriesJsonObject[] categories;
            public CoordinatesJsonObject coordinates;
            public LocationJsonObject location;
            public Int? review_count;
            public string[] transactions;//List of Yelp transactions that the business is registered for. Current supported values are pickup, delivery and restaurant_reservation.
            public string url;
            public string display_phone;
            public decimal? distance;//Distance in meters from the search location. This returns meters regardless of the locale.
            public string image_url;
        }
        [Serializable]
        public class ReviewJsonObject
        {
            public string id;
            public string text;
            public string url;//URL of this review.
            public byte? rating;//Rating of this review.0-5
            public DateTime? time_created;//The time that the review was created in PST.
            public UserJsonObject user;
        }
        [Serializable]
        public class SearchResultJson
        {
            public SmallInt total;
            public BusinessesJsonObject[] businesses;//List of business Yelp finds based on the search criteria.
        }
        [Serializable]
        public class ReviewResultJson
        {
            public Int? total;
            public ReviewJsonObject[] reviews;
        }

        public YelpClient()
        {
            this.BaseAddress = new Uri("https://api.yelp.com/v3/");
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", defaultApiKey);
        }
        public SearchResultJson GetSearchResultJson(double latitude, double longitude, UInt16 radius = 40000, byte limit = 50)
        {
            string requestURI = string.Format("businesses/search?term=food&latitude={0}&longitude={1}&radius={2}&limit={3}", latitude, longitude, radius, limit);

            HttpResponseMessage response = GetAsync(requestURI).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<SearchResultJson>().Result;
            }
            return null;
        }
        public ReviewResultJson GetReviewResultJson(string resID)
        {
            string requestString = string.Format("businesses/{0}/reviews", HttpUtility.UrlEncode(resID));
            HttpResponseMessage response = GetAsync(requestString).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ReviewResultJson>().Result;
            }
            return null;
        }
    }
}