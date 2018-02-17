using System.Data.SqlClient;
using System;
using System.Threading.Tasks;

using SmallInt = System.Int16;
using Int = System.Int32;
using BigInt = System.Int64;

static class PublicData//the Encapsulation of all providers' databases
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
            public Restaurant(SmallInt CityID, string resName, string address)
            {
                City_ID = CityID;
                Name = resName;
                Locality_address = address;
            }
            private Int ID;//internally assigned id;primary key
            public SmallInt City_ID { private set; get; }//not null
            public string Name { private set; get; }//not null
            public string Locality_address { private set; get; }//not null
            public string Description
            {
                set { description = value; is_description_init = true; }
                get { return description; }
            }
            public double Locality_latitude
            {
                set { locality_latitude = value; is_locality_latitude_init = true; }
                get { return locality_latitude; }
            }
            public double Locality_longitude
            {
                set { locality_longitude = value; is_locality_longitude_init = true; }
                get { return locality_longitude; }
            }
            public bool Is_delivering
            {
                set { is_delivering = value; is_is_delivering_init = true; }
                get { return is_delivering; }
            }
            public string Locality_zipcode
            {
                set { locality_zipcode = value; is_locality_zipcode_init = true; }
                get { return locality_zipcode; }
            }
            public SmallInt Currency_Nation_ID
            {
                set { currency_Nation_ID = value; is_currency_Nation_ID_init = true; }
                get { return currency_Nation_ID; }
            }
            public string Restaurant_type
            {
                set { restaurant_type = value; is_restaurant_type_init = true; }
                get { return restaurant_type; }
            }
            public DateTime Open_time
            {
                set { open_time = value; is_open_time_init = true; }
                get { return open_time; }
            }
            public DateTime Close_time
            {
                set { close_time = value; is_close_time_init = true; }
                get { return close_time; }
            }
            public float Average_cost_by_one
            {
                set { average_cost_by_one = value; is_average_cost_by_one_init = true; }
                get { return average_cost_by_one; }
            }
            public float Average_cost_by_two
            {
                set { average_cost_by_two = value; is_average_cost_by_two_init = true; }
                get { return average_cost_by_two; }
            }
            public string ImageURL
            {
                set { imageURL = value; is_imageURL_init = true; }
                get { return imageURL; }
            }
            public string PhotosURL
            {
                set { photosURL = value; is_photoURL_init = true; }
                get { return photosURL; }
            }
            public string EventsURL
            {
                set { eventsURL = value; is_eventsURL_init = true; }
                get { return eventsURL; }
            }
            public string HomepageURL
            {
                set { homepageURL = value; is_homepageURL_init = true; }
                get { return homepageURL; }
            }
            public SmallInt DataProviderID
            {
                set { dataProviderID = value; is_dataProviderID_init = true; }
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
                Name = name; Price = price;
            }
            private BigInt ID;//unique worldwide id in a day;primary key
            public string Name//not null
            {
                private set; get;
            }
            public float Price//not null
            {
                private set; get;
            }
            public string Description
            {
                set { description = value; is_description_init = true; }
                get { return description; }
            }
            public SmallInt Cuisine_ID
            {
                set { cuisine_ID = value; is_cuisine_ID_init = true; }
                get { return cuisine_ID; }
            }
            public SmallInt Currency_Nation_ID
            {
                set { currency_Nation_ID = value; is_currency_Nation_ID_init = true; }
                get { return currency_Nation_ID; }
            }
            public string PhotosURL
            {
                set { photosURL = value; is_photosURL_init = true; }
                get { return photosURL; }
            }
            public SmallInt DataProviderID
            {
                set { dataProviderID = value; is_dataProviderID_init = true; }
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
                private set; get;
            }
            public string Name_English
            {
                set { name_English = value; is_name_English_init = true; }
                get { return name_English; }
            }
            public SmallInt Original_country_ID
            {
                set { original_country_ID = value; is_original_country_ID_init = true; }
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
                set { user_name = value; is_user_name_init = true; }
                get { return user_name; }
            }
            public SmallInt User_level//0~100
            {
                set { user_level = value; is_user_level_init = true; }
                get { return user_level; }
            }
            public Int Likes
            {
                set { likes = value; is_likes_init = true; }
                get { return likes; }
            }
            public string Comment
            {
                set { comment = value; is_comment_init = true; }
                get { return comment; }
            }
            public SmallInt DataProviderID//id in table DatabaseProviders
            {
                set { dataProviderID = value; is_dataProviderID_init = true; }
                get { return dataProviderID; }
            }
            public SmallInt NationID//user's nationality
            {
                set { nationID = value; is_nationID_init = true; }
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
        using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_Cities.Header + "ORDER BY[ID]ASC", connection))
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
        using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_Restaurants.Header + "ORDER BY[ID]ASC", connection))
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
        using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_DatabaseProviders.Header + "ORDER BY[ID]ASC", connection))
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
        using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_Dishes.Header + "ORDER BY[ID]ASC", connection))
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
    public static Task<bool> ContainsProvider(SqlConnection connection, string name)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_DatabaseProviders.Header + "WHERE[name]=@name", connection))
        {
            Check.Parameters.AddWithValue("@name", name);
            using (SqlDataReader reader = Check.ExecuteReader())
                if (reader.Read()) result = true;
        }
        return Task.FromResult<bool>(result);
    }
    public static Task<bool> ContainsProvider(SqlConnection connection, SmallInt providerID)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_DatabaseProviders.Header + "WHERE[ID]=@providerID", connection))
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
    public static Task<bool> ContainsNation(SqlConnection connection, SmallInt ISO3166NationNumericCode)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_Nations.Header + "WHERE[ID]=@nationID", connection))
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
    public static Task<bool> ContainsCity(SqlConnection connection, SmallInt cityID)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_Cities.Header + "WHERE[ID]=@cityID", connection))
        {
            Check.Parameters.AddWithValue("@cityID", cityID);
            using (SqlDataReader reader = Check.ExecuteReader())
                if (reader.Read()) result = true;
        }
        return Task.FromResult<bool>(result);
    }
    public static Task<bool> ContainsCity(SqlConnection connection, string LocaleName)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_Cities.Header + "WHERE[name_locale]=@localeName", connection))
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
    public static Task<bool> ContainsRestaurant(SqlConnection connection, SmallInt cityID, string name)
    {
        bool result = false;
        using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_Restaurants.Header + "WHERE[name]=@name AND [city_ID]=@cityID", connection))
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
    public static Task<SmallInt> GetCityID(SqlConnection connection, string LocaleName)
    {
        SmallInt ID = 0;
        using (SqlCommand GetID = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
            + Table_Cities.Header + "WHERE[name_locale]=@localeName", connection))
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
            "INSERT INTO" + DatabaseHeader + Table_DatabaseProviders.Header + Table_DatabaseProviders.Columns
            + "VALUES(@id,@name,@homepage)", connection))
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
        using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Nations.Header + Table_Nations.Columns
            + "VALUES(@id,@EnglishName,@LocaleName,@currency)", connection))
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
        using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Cities.Header + Table_Cities.Columns
            + "VALUES(@id,@LocaleName,@EnglishName)", connection))
        {
            Insertion.Parameters.AddWithValue("@LocaleName", LocaleName);
            Insertion.Parameters.AddWithValue("@EnglishName", EnglishName);
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
        string insertCommandString = "INSERT INTO" + DatabaseHeader + Table_Restaurants.Header + Table_Restaurants.Columns
            + "VALUES(@id,@cityID,@name,@address,@description,@lat,@lon,@zip,@currencyID,@type,@open,@close,@costOne,@costTwo,@delivery,@image,@photo,@event,@homepage,@providerID)";
        using (SqlCommand Insertion = new SqlCommand(insertCommandString, connection))
        {
            Insertion.Parameters.AddWithValue("@cityID", res.City_ID);
            Insertion.Parameters.AddWithValue("@name", res.Name);
            Insertion.Parameters.AddWithValue("@address", res.Locality_address);
            if (res.is_description_init) Insertion.Parameters.AddWithValue("@description", res.Description);
            else Insertion.Parameters.AddWithValue("@description", DBNull.Value);
            if (res.is_locality_latitude_init) Insertion.Parameters.AddWithValue("@lat", res.Locality_latitude);
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
            if (res.is_is_delivering_init) Insertion.Parameters.AddWithValue("@delivery", res.Is_delivering);
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
        string insertionCommandString = "INSERT INTO" + DatabaseHeader + Table_Dishes.Header + Table_Dishes.Columns
            + "VALUES(@id,@name,@price,@description,@cuisineID,@currencyID,@photo,@providerID)";
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
        using (SqlCommand Insertion = new SqlCommand(insertionCommandString, connection))
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
    public static async Task<SmallInt> AddCuisineAsync(SqlConnection connection, Table_Cuisines.Cuisine cuisine)
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
    public static async Task<BigInt> AddRatingCommentAsync(SqlConnection connection, SmallInt resID, Table_RatingComments.RatingComment rat)
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
    public static async Task<BigInt> AddRatingCommentAsync(SqlConnection connection, BigInt dishID, bool isDishRating, Table_RatingComments.RatingComment rat)
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
        insertionCommandString = "INSERT INTO" + DatabaseHeader + Union_Table_Dishes_RatingComments.Header + Union_Table_Dishes_RatingComments.Columns
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