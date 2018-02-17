using Bot_Application.API;
using System.Data.SqlClient;
using System.Threading.Tasks;

static class ZomatoData//database for Zomato.com
{
    public const string DatabaseHeader = "[ZomatoData].";
    public static class Table_Categories
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
    public static class Table_Cities
    {
        public const string Header = "[dbo].[Cities]";
        public const string Columns = "([ID],[name],[country_id],[is_state],[state_id],[state_name],[state_code])";
        public static async Task<bool> AddNewCityAsync(SqlConnection connection, ZomatoClient client, double lat, double lon)
        {
            ZomatoClient.CitiesJson newJson = await client.GetCitiesAsync(lat, lon);
            if (newJson.location_suggestions.Length == 0) return false;
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Cities.Header + Table_Cities.Columns
                + "VALUES(@id,@name,@countryID,@isState,@stateID,@stateName,@stateCode)"))
            {
                Insertion.Parameters.AddWithValue("@id", newJson.location_suggestions[0].id);
                Insertion.Parameters.AddWithValue("@name", newJson.location_suggestions[0].name);
                Insertion.Parameters.AddWithValue("@countryID", newJson.location_suggestions[0].country_id);
                Insertion.Parameters.AddWithValue("@isState", newJson.location_suggestions[0].is_state);
                Insertion.Parameters.AddWithValue("@stateID", newJson.location_suggestions[0].state_id);
                Insertion.Parameters.AddWithValue("@stateName", newJson.location_suggestions[0].state_name);
                Insertion.Parameters.AddWithValue("@stateCode", newJson.location_suggestions[0].state_code);
                Insertion.ExecuteNonQuery();
            }
            return true;
        }
    }
    public static class Table_Cuisines
    {
        public const string Header = "[dbo].[Cuisines]";
        public const string Columns = "([ID],[name])";
        //static public async void RenewCuisinesAsync(SqlConnection connection, ZomatoClient client, SmallInt cityID)
        //{
        //    Task<ZomatoClient.CuisinesJson> newJson = client.GetCuisionAsync(cityID);
        //    using (SqlCommand Clear = new SqlCommand("DELETE FROM" + DatabaseHeader)) Clear.ExecuteNonQuery();
        //    ZomatoClient.CuisinesJson json = await newJson;
        //    for (int i = 0; i < json.cuisines.Length; ++i)
        //    {
        //        using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Header + Columns
        //            + "VALUES(@id,@name)"))
        //        {
        //            Insertion.Parameters.AddWithValue("@id", json.cuisines[i].cuisine.cuisine_id);
        //            Insertion.Parameters.AddWithValue("@name", json.cuisines[i].cuisine.cuisine_name);
        //            Insertion.ExecuteNonQuery();
        //        }
        //    }
        //    //todo: mapping former records
        //}
    }
    public static class Table_Restaurants
    {
        public const string Header = "[dbo].[Restaurants]";
        public const string Columns = @"([ID],[name],[address],[latitude],[longtitude],[zipcode],[URL],[price_range],
            [average_cost_for_two],[currency_symbol],[thumURL],[iamgeURL],[photosURL],[menuURL],[eventsURL],
            [aggregate_rating],[rating_text],[votes],[has_online_delivery],[has_table_booking],[cuisines],[phone_number])";
        public static async void AddNearbyRestaurantsAsync(SqlCommand connection, ZomatoClient client, double lat, double lon)//only available for test
        {

        }
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
        public const string Columns = "([ID],[rating],[review_text],[likes],[user_name])";
    }
}