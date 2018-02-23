using System;
using System.Threading;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bot_Application.API;
using SmallInt = System.Int16;
using Int = System.Int32;
using BigInt = System.Int64;
using TinyInt = System.Byte;


namespace Bot_Application.Database.third_party_database
{
    static class ZomatoData//database for Zomato.com
    {
        public const string DatabaseHeader = "[ZomatoData].";
        private static class Table_Categories
        {
            public const string Header = "[dbo].[Categories]";
            public const string Columns = "([ID],[name])";
            public static async void RenewCategoriesAsync(SqlConnection connection, ZomatoClient client)
            {
                Task<ZomatoClient.CategoriesJson> newJson = client.GetCategoriesAsync();
                using (SqlCommand Clear = new SqlCommand("DELETE FROM" + DatabaseHeader)) Clear.ExecuteNonQuery();
                ZomatoClient.CategoriesJson json = await newJson;
                for (int i = 0; i < json.categories.Length; ++i)
                {
                    using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Header + Columns
                        + "VALUES(@id,@name)"))
                    {
                        Insertion.Parameters.AddWithValue("@id", json.categories[i].categories.id);
                        Insertion.Parameters.AddWithValue("@name", json.categories[i].categories.name);
                        Insertion.ExecuteNonQuery();
                    }
                }
                //todo: mapping former records
            }
        }
        private static class Table_Nations
        {
            public const string Header = "[dbo].[Nations]";
            public const string Columns = "([zomatoCountryID],[zomatoCountryName])";
        }
        public static class Table_Cities
        {
            public const string Header = "[dbo].[Cities]";
            public const string Columns = "([ID],[name],[country_id],[is_state],[state_id],[state_name],[state_code])";
        }
        public static class Table_Cuisines
        {
            public const string Header = "[dbo].[Cuisines]";
            public const string Columns = "([ID],[name])";
        }
        public static class Table_Restaurants
        {
            public const string Header = "[dbo].[Restaurants]";
            public const string Columns = @"([ID],[name],[address],[latitude],[longtitude],[zipcode],[URL],[price_range],
            [average_cost_for_two],[currency_symbol],[thumURL],[imageURL],[photosURL],[menuURL],[eventsURL],
            [aggregate_rating],[votes],[has_online_delivery],[has_table_booking],[cuisines],[phone_number])";
        }
        public static class Table_DailyMenuItems
        {
            public const string Header = "[dbo].[DailyMenuItems]";
            public const string Columns = "([ID],[name],[price])";
        }
        public static class Table_Photos
        {
            public const string Header = "[dbo].[Photos]";
            public const string Columns = "([ID],[URL],[thumbURL])";
        }
        public static class Table_Reviews
        {
            public const string Header = "[dbo].[Reviews]";
            public const string Columns = "([ID],[rating],[raing_text],[review_text],[timestamp],[likes],[user_name])";
        }
        private static class Union_Table_Restaurants_DailyMenuItems
        {
            public const string Header = "[dbo].[Restaurants_DailyMenuItems]";
            public const string Columns = "([restaurant_ID],[dailyMenuItem_ID])";
        }
        private static class Union_Table_Restaurants_Reviews
        {
            public const string Header = "[dbo].[Restaurants_Reviews]";
            public const string Columns = "([restaurant_ID],[Review_ID])";
        }

        //Basic Methods: Do not check, throw exceptions
        private static Task AddCity(SqlConnection con, ZomatoClient.CitiesJson.City city)
        {
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Cities.Header + Table_Cities.Columns
                + "VALUES(@id,@name,@countryID,@isState,@stateID,@stateName,@stateCode)", con))
            {
                Insertion.Parameters.AddWithValue("@id", city.id);
                Insertion.Parameters.AddWithValue("@name", city.name);
                Insertion.Parameters.AddWithValue("@countryID", city.country_id);
                Insertion.Parameters.AddWithValue("@isState", city.is_state);
                Insertion.Parameters.AddWithValue("@stateID", city.state_id);
                Insertion.Parameters.AddWithValue("@stateName", city.state_name);
                Insertion.Parameters.AddWithValue("@stateCode", city.state_code);
                Insertion.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        public static void AddReview(SqlConnection con, ZomatoClient.Review review, BigInt resID)
        {
            //update Table_Reviews
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Reviews.Header + Table_Reviews.Columns
                + "VALUES(@id,@rating,@ratingText,@reviewText,@time,@likes,@userName)", con))
            {
                if (review.id.HasValue) Insertion.Parameters.AddWithValue("@id", review.id.Value);
                else Insertion.Parameters.AddWithValue("@id", DBNull.Value);
                if (review.rating.HasValue) Insertion.Parameters.AddWithValue("@rating", review.rating.Value);
                else Insertion.Parameters.AddWithValue("@rating", DBNull.Value);
                if (review.rating_text != null && review.rating_text.Trim() != "") Insertion.Parameters.AddWithValue("@ratingText", review.rating_text);
                else Insertion.Parameters.AddWithValue("@ratingText", DBNull.Value);
                if (review.review_text != null && review.review_text.Trim() != "") Insertion.Parameters.AddWithValue("@reviewText", review.review_text);
                else Insertion.Parameters.AddWithValue("@reviewText", DBNull.Value);
                if (review.timestamp.HasValue) Insertion.Parameters.AddWithValue("@time", review.timestamp.Value);
                else Insertion.Parameters.AddWithValue("@time", DBNull.Value);
                if (review.user.name != null && review.user.name.Trim() != "") Insertion.Parameters.AddWithValue("@userName", review.user.name);
                else Insertion.Parameters.AddWithValue("@userName", DBNull.Value);
                Insertion.ExecuteNonQuery();
            }
            //update Union_Table_Restaurants_Reviews
            if (review.id.HasValue)
            {
                using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Union_Table_Restaurants_Reviews.Header + Union_Table_Restaurants_Reviews.Columns
                    + "VALUES(@resID,@reviewID)", con))
                {
                    Insertion.Parameters.AddWithValue("@resID", resID);
                    Insertion.Parameters.AddWithValue("@reviewID", review.id);
                    Insertion.ExecuteNonQuery();
                }
            }
        }
        public static void AddDailyMenuItem(SqlConnection con, ZomatoClient.DailyMenuItem dish, Int resID)
        {
            //update Table_DailyMenuItems
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_DailyMenuItems.Header + Table_DailyMenuItems.Columns
                + "VALUES(@id,@name,@price)", con))
            {
                if (dish.dish_id.HasValue) Insertion.Parameters.AddWithValue("@id", dish.dish_id.Value);
                else Insertion.Parameters.AddWithValue("@id", DBNull.Value);
                if (dish.name != null && dish.name.Trim() != "") Insertion.Parameters.AddWithValue("@name", dish.name);
                else Insertion.Parameters.AddWithValue("@name", DBNull.Value);
                if (dish.price != null && dish.price.Trim() != "") Insertion.Parameters.AddWithValue("@price", dish.name);
                else Insertion.Parameters.AddWithValue("@price", DBNull.Value);
                Insertion.ExecuteNonQuery();
            }
            //update Union_Table_Restaurants_DailyMenuItems
            if (dish.dish_id.HasValue)
            {
                using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Union_Table_Restaurants_DailyMenuItems.Header + Union_Table_Restaurants_DailyMenuItems.Columns
                    + "VALUES(@resID,@dishID)", con))
                {
                    Insertion.Parameters.AddWithValue("@resID", resID);
                    Insertion.Parameters.AddWithValue("@dishID", dish.dish_id.Value);
                    Insertion.ExecuteNonQuery();
                }
            }
        }
        public static async Task AddRestaurantAsync(SqlConnection con, ZomatoClient client, ZomatoClient.RestaurantL3 res)
        {
            //init reviews and set relation table
            if (res.all_reviews != null)
            {
                foreach (var review in res.all_reviews)
                    AddReview(con, review, res.id.Value);
            }
            //init DailyMenuItems and set relation table
            ZomatoClient.DailyMenuJson dailyMenuJson = null;
            bool redo = true;
            while (redo)
            {
                using (var cts = new CancellationTokenSource())
                {
                    using (Task<ZomatoClient.DailyMenuJson> dailyMenuJsonTask = client.GetDailyMenuAsync(res.id.Value, cts.Token))
                    {
                        for (int sleepCount = 0; !dailyMenuJsonTask.IsCompleted && sleepCount < 5; ++sleepCount) { Thread.Sleep(1000); }
                        if (!dailyMenuJsonTask.IsCanceled || !dailyMenuJsonTask.IsCompleted) { cts.Cancel(); Console.WriteLine(" DailyMenu cancelled"); }
                        else
                        {
                            dailyMenuJson = dailyMenuJsonTask.Result;
                            redo = false;
                        }
                        if (dailyMenuJson != null && dailyMenuJson.daily_menu != null)
                        {
                            foreach (var dishCategory in dailyMenuJson.daily_menu)
                            {
                                foreach (var dish in dishCategory.dishes)
                                {
                                    AddDailyMenuItem(con, dish, res.id.Value);
                                }
                            }
                        }

                    }
                }
                if (redo) Thread.Sleep(10000);
            }
            //init Other info
            string insertCommandString = "INSERT INTO" + DatabaseHeader + Table_Restaurants.Header + Table_Restaurants.Columns
                + @"VALUES(@id,@name,@address,@lat,@lon,@zip,@URL,@priceRange,@costTwo,@currencySymbol,@thum,@image,
                @photos,@menu,@events,@rating,@votes,@onlineDelivery,@tableBooking,@cuisines,@phone)";
            using (SqlCommand Insertion = new SqlCommand(insertCommandString, con))
            {
                if (res.id.HasValue) Insertion.Parameters.AddWithValue("@id", res.id.Value);
                else Insertion.Parameters.AddWithValue("@id", DBNull.Value);
                if (res.name != null && res.name.Trim() != "") Insertion.Parameters.AddWithValue("@name", res.name);
                else Insertion.Parameters.AddWithValue("@name", DBNull.Value);
                if (res.location.address != null && res.location.address.Trim() != "") Insertion.Parameters.AddWithValue("@address", res.location.address);
                else Insertion.Parameters.AddWithValue("@address", DBNull.Value);
                if (res.location.latitude.HasValue) Insertion.Parameters.AddWithValue("@lat", res.location.latitude.Value);
                else Insertion.Parameters.AddWithValue("@lat", DBNull.Value);
                if (res.location.longitude.HasValue) Insertion.Parameters.AddWithValue("@lon", res.location.longitude.Value);
                else Insertion.Parameters.AddWithValue("@lon", DBNull.Value);
                if (res.location.zipcode != null && res.location.zipcode.Trim() != "") Insertion.Parameters.AddWithValue("@zip", res.location.zipcode);
                else Insertion.Parameters.AddWithValue("@zip", DBNull.Value);
                if (res.url != null && res.url.Trim() != "") Insertion.Parameters.AddWithValue("@URL", res.url);
                else Insertion.Parameters.AddWithValue("@URL", DBNull.Value);
                if (res.price_range.HasValue) Insertion.Parameters.AddWithValue("@priceRange", res.price_range.Value);
                else Insertion.Parameters.AddWithValue("@priceRange", DBNull.Value);
                if (res.average_cost_for_two.HasValue) Insertion.Parameters.AddWithValue("@costTwo", res.average_cost_for_two.Value);
                else Insertion.Parameters.AddWithValue("@costTwo", DBNull.Value);
                if (res.currency != null && res.currency.Trim() != "") Insertion.Parameters.AddWithValue("@currencySymbol", res.currency);
                else Insertion.Parameters.AddWithValue("@currencySymbol", DBNull.Value);
                if (res.thumb != null && res.thumb.Trim() != "") Insertion.Parameters.AddWithValue("@thum", res.thumb);
                else Insertion.Parameters.AddWithValue("@thum", DBNull.Value);
                if (res.featured_image != null && res.featured_image.Trim() != "") Insertion.Parameters.AddWithValue("@image", res.featured_image);
                else Insertion.Parameters.AddWithValue("@image", DBNull.Value);
                if (res.photos_url != null && res.photos_url.Trim() != "") Insertion.Parameters.AddWithValue("@photos", res.photos_url);
                else Insertion.Parameters.AddWithValue("@photos", DBNull.Value);
                if (res.menu_url != null && res.menu_url.Trim() != "") Insertion.Parameters.AddWithValue("@menu", res.menu_url);
                else Insertion.Parameters.AddWithValue("@menu", DBNull.Value);
                if (res.events_url != null && res.events_url.Trim() != "") Insertion.Parameters.AddWithValue("@events", res.events_url);
                else Insertion.Parameters.AddWithValue("@events", DBNull.Value);
                if (res.user_rating.aggregate_rating.HasValue) Insertion.Parameters.AddWithValue("@rating", res.user_rating.aggregate_rating.Value);
                else Insertion.Parameters.AddWithValue("@rating", DBNull.Value);
                if (res.user_rating.votes.HasValue) Insertion.Parameters.AddWithValue("@votes", res.user_rating.votes.Value);
                else Insertion.Parameters.AddWithValue("@votes", DBNull.Value);
                if (res.has_online_delivery.HasValue) Insertion.Parameters.AddWithValue("@onlineDelivery", res.has_online_delivery.Value);
                else Insertion.Parameters.AddWithValue("@onlineDelivery", DBNull.Value);
                if (res.has_table_booking.HasValue) Insertion.Parameters.AddWithValue("@tableBooking", res.has_table_booking.Value);
                else Insertion.Parameters.AddWithValue("@tableBooking", DBNull.Value);
                if (res.cuisines != null && res.cuisines.Trim() != "") Insertion.Parameters.AddWithValue("@cuisines", res.cuisines);
                else Insertion.Parameters.AddWithValue("@cuisines", DBNull.Value);
                if (res.phone_numbers != null && res.phone_numbers.Trim() != "") Insertion.Parameters.AddWithValue("@phone", res.phone_numbers);
                else Insertion.Parameters.AddWithValue("@phone", DBNull.Value);
                Insertion.ExecuteNonQuery();
            }
        }
        public static Task<bool> ContainsRestaurants(SqlConnection con, BigInt resID)
        {
            bool result = false;
            using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader + Table_Restaurants.Header +
                "WHERE[ID]=@id", con))
            {
                Check.Parameters.AddWithValue("@id", resID);
                using (SqlDataReader reader = Check.ExecuteReader())
                    if (reader.Read()) result = true;
            }
            return Task.FromResult(result);
        }
        public static Task<bool> ContainsNation(SqlConnection con, string name)
        {
            bool result = false;
            //Check if Contains the Nation
            using (SqlCommand Check = new SqlCommand("SELECT[zomatoCountryID]FROM" + DatabaseHeader + Table_Cities.Header
                + "WHERE[zomatoCountryName]=@name", con))
            {
                using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
            }
            return Task.FromResult(result);
        }
        public static Task<bool> ContainsCityAsync(SqlConnection con, string name)
        {
            bool result = false;
            //Check if Contains the city
            using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader + Table_Cities.Header
                + "WHERE[name]=@name", con))
            {
                using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
            }
            return Task.FromResult(result);
        }
    }
}
