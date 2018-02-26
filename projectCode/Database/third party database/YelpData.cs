using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bot_Application.API;
using System.Collections.Generic;
using SmallInt = System.Int16;
using Int = System.Int32;
using BigInt = System.Int64;
using TinyInt = System.Byte;

namespace Bot_Application.Database.third_party_database
{
    static class YelpData
    {
        public static readonly string DatabaseHeader = "[YelpData].";
        public static class Categories
        {
            public static string Header = "[dbo].[Categories]";
            public static string Columns = "([ID],[title],[alias])";
            public class Record
            {
                public SmallInt ID { private set; get; }
                public string title { private set; get; }
                public string alias { private set; get; }
                public Record(string title, string alias) { this.title = title; this.alias = alias; }
                internal Record(SmallInt ID, string title, string alias)
                {
                    this.ID = ID; this.title = title; this.alias = alias;
                }
            }
        }
        public static class Transactions
        {
            public static string Header = "[dbo].[Transactions]";
            public static string Columns = "([ID],[type])";
            public class Record
            {
                public SmallInt ID { private set; get; }
                public string type { private set; get; }
                public Record(string type) { this.type = type; }
                internal Record(SmallInt ID, string type) { this.ID = ID; this.type = type; }
            }
        }
        public static class Restaurants
        {
            public static string Header = "[dbo].[Restaurants]";
            public static string Columns = @"([ID],[Yelp_ID],[name],[latitude],[longitude],[phone],
               [display_phone],[image_url],[address1],[address2],[address3],[city],[country],
               [state],[zip_code],[price],[rating],[review_count],[url])";
            public class Record
            {
                public Int ID { private set; get; }
                public string Yelp_ID { private set; get; }
                public string name { private set; get; }
                public double? latitude;
                public double? longitude;
                public string phone;
                public string display_phone;
                public string image_url;
                public string address1;
                public string address2;
                public string address3;
                public string city;
                public string country;
                public string state;
                public string zip_code;
                public string price;
                public float? rating;//0.0-5.0
                public Int? review_count;
                public string url;
                public Record(string Yelp_ID, string name) { this.Yelp_ID = Yelp_ID; this.name = name; }
                internal Record(Int ID, string Yelp_ID, string name)
                {
                    this.ID = ID; this.Yelp_ID = Yelp_ID; this.name = name;
                }
            }
        }
        public static class Reviews
        {
            public static string Header = "[dbo].[Reviews]";
            public static string Columns = "([ID],[Yelp_ID],[text],[url],[rating],[time_created],[user_name])";
            public class Record
            {
                public BigInt ID { private set; get; }
                public string Yelp_ID { private set; get; }
                public string text;
                public string url;
                public TinyInt? rating;
                public DateTime? time_created;
                public string user_name;
                public Record(string Yelp_ID) { this.Yelp_ID = Yelp_ID; }
                internal Record(BigInt ID, string Yelp_ID) { this.ID = ID; this.Yelp_ID = Yelp_ID; }
            }
        }
        static private class Restaurants_Categories
        {
            public static string Header = "[dbo].[Restaurants_Categories]";
            public static string Columns = "([RestaurantID],[CategoryID])";
        }
        static private class Restaurants_Reviews
        {
            public static string Header = "[dbo].[Restaurants_Reviews]";
            public static string Columns = "([RestaurantID],[ReviewID])";
        }
        static private class Restaurants_Transactions
        {
            public static string Header = "[dbo].[Restaurants_Transactions]";
            public static string Columns = "([RestaurantID],[TransactionID])";
        }
        static private Task<SmallInt?> GenerateCategoryID(SqlConnection con, SmallInt offset = 0)
        {
            SmallInt? result = SmallInt.MinValue;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + Categories.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    SmallInt formerID = SmallInt.MinValue;
                    bool found = false;
                    bool first = true;
                    while (reader.Read())
                    {
                        SmallInt curID = (SmallInt)reader[0];
                        if (curID == SmallInt.MaxValue)
                        {
                            result = null;
                            found = true;
                        }
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
            return Task.FromResult(result);
        }
        static private Task<SmallInt?> GenerateTransactionID(SqlConnection con)
        {
            SmallInt? result = SmallInt.MinValue;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + Transactions.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    SmallInt formerID = SmallInt.MinValue;
                    bool found = false;
                    bool first = true;
                    while (reader.Read())
                    {
                        SmallInt curID = (SmallInt)reader[0];
                        if (curID == SmallInt.MaxValue)
                        {
                            result = null;
                            found = true;
                        }
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
            return Task.FromResult(result);
        }
        static private Task<Int?> GenerateRestaurantID(SqlConnection con)
        {
            Int? result = Int.MinValue;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + Restaurants.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    Int formerID = Int.MinValue;
                    bool found = false;
                    bool first = true;
                    while (reader.Read())
                    {
                        Int curID = (Int)reader[0];
                        if (curID == Int.MaxValue)
                        {
                            result = null;
                            found = true;
                        }
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
            return Task.FromResult(result);

        }
        static private Task<BigInt?> GenerateReviewID(SqlConnection con)
        {
            BigInt? result = BigInt.MinValue;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + Reviews.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    BigInt formerID = BigInt.MinValue;
                    bool found = false;
                    bool first = true;
                    while (reader.Read())
                    {
                        BigInt curID = (BigInt)reader[0];
                        if (curID == BigInt.MaxValue)
                        {
                            result = null;
                            found = true;
                        }
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
            return Task.FromResult(result);

        }
        static private Task AddRestaurant(SqlConnection con, Int internalID, YelpClient.BusinessesJsonObject res)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Restaurants.Header + Restaurants.Columns
        + "VALUES(@id,@yelpID,@name,@lat,@lon,@phone,@displayPhone,@image,@address1,@address2," +
        "@address3,@city,@country,@state,@zip,@price,@rating,@reviewCount,@url)", con))
            {
                Insert.Parameters.AddWithValue("@id", internalID);
                Insert.Parameters.AddWithValue("@yelpID", res.id);
                Insert.Parameters.AddWithValue("@name", res.name);
                if (res.coordinates != null)
                {
                    if (res.coordinates.latitude.HasValue) Insert.Parameters.AddWithValue("@lat", res.coordinates.latitude.Value);
                    else Insert.Parameters.AddWithValue("@lat", DBNull.Value);
                    if (res.coordinates.longitude.HasValue) Insert.Parameters.AddWithValue("@lon", res.coordinates.longitude.Value);
                    else Insert.Parameters.AddWithValue("@lon", DBNull.Value);
                }
                if (res.phone != null && res.phone.Trim() != "")
                    Insert.Parameters.AddWithValue("@phone", res.phone.Trim());
                else Insert.Parameters.AddWithValue("@phone", DBNull.Value);
                if (res.display_phone != null && res.display_phone.Trim() != "")
                    Insert.Parameters.AddWithValue("@displayPhone", res.display_phone.Trim());
                else Insert.Parameters.AddWithValue("@displayPhone", DBNull.Value);
                if (res.image_url != null && res.image_url.Trim() != "")
                    Insert.Parameters.AddWithValue("@image", res.image_url.Trim());
                else Insert.Parameters.AddWithValue("@image", DBNull.Value);
                if (res.location != null)
                {
                    if (res.location.address1 != null && res.location.address1.Trim() != "")
                        Insert.Parameters.AddWithValue("@address1", res.location.address1.Trim());
                    else Insert.Parameters.AddWithValue("@address1", DBNull.Value);
                    if (res.location.address2 != null && res.location.address2.Trim() != "")
                        Insert.Parameters.AddWithValue("@address2", res.location.address2.Trim());
                    else Insert.Parameters.AddWithValue("@address2", DBNull.Value);
                    if (res.location.address3 != null && res.location.address3.Trim() != "")
                        Insert.Parameters.AddWithValue("@address3", res.location.address3.Trim());
                    else Insert.Parameters.AddWithValue("@address3", DBNull.Value);
                    if (res.location.city != null && res.location.city.Trim() != "")
                        Insert.Parameters.AddWithValue("@city", res.location.city.Trim() != "");
                    else Insert.Parameters.AddWithValue("@city", DBNull.Value);
                    if (res.location.country != null && res.location.country.Trim() != "")
                        Insert.Parameters.AddWithValue("@country", res.location.country.Trim());
                    else Insert.Parameters.AddWithValue("@country", DBNull.Value);
                    if (res.location.state != null && res.location.state.Trim() != "")
                        Insert.Parameters.AddWithValue("@state", res.location.state);
                    else Insert.Parameters.AddWithValue("@state", DBNull.Value);
                    if (res.location.zip_code != null && res.location.zip_code.Trim() != "")
                        Insert.Parameters.AddWithValue("@zip", res.location.zip_code);
                    else Insert.Parameters.AddWithValue("@zip", DBNull.Value);
                }
                if (res.price != null && res.price.Trim() != "")
                    Insert.Parameters.AddWithValue("@price", res.price);
                else Insert.Parameters.AddWithValue("@price", DBNull.Value);
                if (res.rating.HasValue) Insert.Parameters.AddWithValue("@rating", res.rating.Value);
                else Insert.Parameters.AddWithValue("@rating", DBNull.Value);
                if (res.review_count.HasValue) Insert.Parameters.AddWithValue("@reviewCount", res.review_count.Value);
                else Insert.Parameters.AddWithValue("@reviewCount", DBNull.Value);
                if (res.url != null && res.url.Trim() != "")
                    Insert.Parameters.AddWithValue("@url", res.url);
                else Insert.Parameters.AddWithValue("@url", DBNull.Value);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static private Task RemoveRestaurant(SqlConnection con, Int internalID)
        {
            using (SqlCommand Delete = new SqlCommand("DELETE FROM" + Restaurants.Header
                + "WHERE[ID]=@id"))
            {
                Delete.Parameters.AddWithValue("@id", internalID);
                Delete.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static private Int? HasRestaurant(SqlConnection con, string Yelp_ID)
        {
            Int? result = null;
            using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + Restaurants.Header
                + "WHERE[Yelp_ID]=@id", con))
            {
                Check.Parameters.AddWithValue("@id", Yelp_ID);
                using (SqlDataReader reader = Check.ExecuteReader())
                    if (reader.Read()) result = (Int)reader[0];
            }
            return result;
        }
        static private string HasRestaurant(SqlConnection con, Int internalResID)
        {
            string result = null;
            using (SqlCommand Check = new SqlCommand("SELECT[Yelp_ID]FROM" + Restaurants.Header
                + "WHERE[ID]=@id", con))
            {
                Check.Parameters.AddWithValue("@id", internalResID);
                using (SqlDataReader reader = Check.ExecuteReader())
                    if (reader.Read()) result = reader.GetString(0);
            }
            return result;
        }
        static public Int? AddRestaurantAndManage(SqlConnection con, YelpClient.BusinessesJsonObject res)
        {
            Int? result = null;
            if (res != null)
            {
                if (!HasRestaurant(con, res.id).HasValue)
                {
                    Int? newID = GenerateRestaurantID(con).Result;
                    if (newID.HasValue)
                    {
                        //manage other Tables
                        Task<bool> ManageCategoriesTask = null;
                        Task<bool> ManageTransactionsTask = null;
                        if (res.categories != null && res.categories.Length > 0)
                            ManageCategoriesTask = ManageCategories(con, res.categories, newID.Value);
                        if (res.transactions != null && res.transactions.Length > 0)
                            ManageTransactionsTask = ManageTransactions(con, res.transactions, newID.Value);
                        bool categoriesResult = false, transactionsResult = false;
                        if (res.categories == null || res.categories.Length > 0 || ManageCategoriesTask == null || ManageCategoriesTask.Result)
                            categoriesResult = true;
                        if (res.transactions == null || ManageTransactionsTask == null || ManageTransactionsTask.Result)
                            transactionsResult = true;
                        if (categoriesResult && transactionsResult)
                        {
                            AddRestaurant(con, newID.Value, res).Wait();
                            result = newID;
                        }
                    }
                }
            }
            return result;
        }
        static public bool AddReviewsAndManage(SqlConnection con, YelpClient.ReviewResultJson reviews, Int internalResID)
        {
            bool result = true;
            var Reviews = reviews?.reviews;
            if (Reviews != null && Reviews.Length > 0)
            {
                foreach (var rev in Reviews)
                {
                    if (rev != null)
                    {
                        BigInt? revID = HasReview(con, rev.id, internalResID);
                        if (revID.HasValue)
                            AddRestaurants_Reviews(con, revID.Value, internalResID);
                        else
                        {
                            BigInt? newID = GenerateReviewID(con).Result;
                            if (newID.HasValue)
                            {
                                if (!HasReview(con, newID.Value, internalResID))
                                    AddReview(con, newID.Value, rev, internalResID);
                                //add to Restaurants_Reviews
                                AddRestaurants_Reviews(con, newID.Value, internalResID);
                            }
                            else
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                }
            }
            else result = false;
            return result;
        }
        static private Task AddCategory(SqlConnection con, SmallInt internalID, string title, string alias)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Categories.Header + Categories.Columns
                + "VALUES(@id,@title,@alias)", con))
            {
                Insert.Parameters.AddWithValue("@id", internalID);
                Insert.Parameters.AddWithValue("@title", title);
                Insert.Parameters.AddWithValue("@alias", alias);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static private Task RemoveCategory(SqlConnection con, SmallInt internalID)
        {
            using (SqlCommand Delete = new SqlCommand("DELETE FROM" + Categories.Header
                + "WHERE[ID]=@id", con))
            {
                Delete.Parameters.AddWithValue("@id", internalID);
                Delete.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static public SmallInt? HasCategory(SqlConnection con, string title)
        {
            SmallInt? result = null;
            if (title != null)
            {
                using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + Categories.Header
                    + "WHERE[title]=@title", con))
                {
                    Check.Parameters.AddWithValue("@title", title);
                    using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = (SmallInt)reader[0];
                }
            }
            return result;
        }
        static private bool HasCategory(SqlConnection con, SmallInt internalID)
        {
            bool result = false;
            using (SqlCommand Check = new SqlCommand("SELECT[ID]" + Categories.Header
                + "WHERE[ID]=@id", con))
            {
                Check.Parameters.AddWithValue("@id", internalID);
                using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
            }
            return result;
        }
        static private Task AddTransaction(SqlConnection con, SmallInt internalID, string name)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Transactions.Header + Transactions.Columns
                + "VALUES(@id,@name)", con))
            {
                Insert.Parameters.AddWithValue("@id", internalID);
                Insert.Parameters.AddWithValue("@name", name);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static private Task DeletTransaction(SqlConnection con, SmallInt internalID)
        {
            using (SqlCommand Delete = new SqlCommand("DELETE FROM" + Transactions.Header
                + "WHERE[ID]=@id"))
            {
                Delete.Parameters.AddWithValue("@id", internalID);
                Delete.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static public SmallInt? HasTransaction(SqlConnection con, string name)
        {
            SmallInt? result = null;
            if (name != null)
            {
                using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + Transactions.Header
                    + "WHERE[transactionName]=@name", con))
                {
                    Check.Parameters.AddWithValue("@name", name);
                    using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = (SmallInt)reader[0];
                }
            }
            return result;
        }
        static private bool HasTransaction(SqlConnection con, SmallInt internalID)
        {
            bool result = false;
            using (SqlCommand Check = new SqlCommand("SELECT[ID]" + Transactions.Header
                + "WHERE[ID]=@id", con))
            {
                Check.Parameters.AddWithValue("@id", internalID);
                using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
            }
            return result;
        }
        static private bool HasReview(SqlConnection con, BigInt internalRevID, Int internalResID)
        {
            bool result = false;
            using (SqlCommand Check = new SqlCommand("SELECT[ReviewID]FROM" + Restaurants_Reviews.Header
                + "WHERE[RestaurantID]=@resID AND[ReviewID]=@revID", con))
            {
                Check.Parameters.AddWithValue("@resID", internalResID);
                Check.Parameters.AddWithValue("@revID", internalRevID);
                using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
            }
            return result;
        }
        static private BigInt? HasReview(SqlConnection con, string reviewYelpID, Int internalResID)
        {
            BigInt? result = null;
            using (SqlCommand Check = new SqlCommand("SELECT" + Reviews.Header + ".[ID]FROM" + Reviews.Header
                + "," + Restaurants_Reviews.Header + "WHERE" + Restaurants_Reviews.Header + ".[RestaurantID]=@resID"
                + " AND" + Reviews.Header + ".[Yelp_ID]=@revYelpID", con))
            {
                Check.Parameters.AddWithValue("@resID", internalResID);
                Check.Parameters.AddWithValue("@revYelpID", reviewYelpID);
                using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = (BigInt)reader[0];
            }
            return result;
        }
        static private Task AddReview(SqlConnection con, BigInt internalRevID, YelpClient.ReviewJsonObject review, Int internalResID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Reviews.Header + Reviews.Columns
                + "VALUES(@id,@yelpID,@text,@url,@rating,@timeCreated,@userName)", con))
            {
                Insert.Parameters.AddWithValue("@id", internalRevID);
                Insert.Parameters.AddWithValue("@yelpID", review.id);
                if (review.text != null && review.text.Trim() != "") Insert.Parameters.AddWithValue("@text", review.text);
                else Insert.Parameters.AddWithValue("@text", DBNull.Value);
                if (review.url != null && review.url.Trim() != "") Insert.Parameters.AddWithValue("@url", review.url);
                else Insert.Parameters.AddWithValue("@url", DBNull.Value);
                if (review.rating.HasValue) Insert.Parameters.AddWithValue("@rating", review.rating.Value);
                else Insert.Parameters.AddWithValue("@rating", DBNull.Value);
                if (review.time_created.HasValue) Insert.Parameters.AddWithValue("@timeCreated", review.time_created.Value);
                else Insert.Parameters.AddWithValue("@timeCreated", DBNull.Value);
                if (review.user != null && review.user.name != null && review.user.name.Trim() != "")
                    Insert.Parameters.AddWithValue("@userName", review.user.name);
                else Insert.Parameters.AddWithValue("@userName", DBNull.Value);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        //return true if success
        static public Task<bool> ManageCategories(SqlConnection con, YelpClient.CategoriesJsonObject[] cateObjects, Int internalResID)
        {
            Task<bool> result = Task.FromResult(true);
            foreach (var cate in cateObjects)
            {
                SmallInt? cateID = HasCategory(con, cate.title);
                if (cateID.HasValue)
                    AddRestaurants_Categories(con, cateID.Value, internalResID);
                else
                {
                    SmallInt? newID = GenerateCategoryID(con).Result;
                    if (newID.HasValue)
                    {
                        AddCategory(con, newID.Value, cate.title, cate.alias);
                        AddRestaurants_Categories(con, newID.Value, internalResID);
                    }
                    else
                    {
                        result = Task.FromResult(false);
                        break;
                    }
                }
            }
            return result;
        }
        static private Task AddRestaurants_Categories(SqlConnection con, SmallInt internalCategoryID, Int InternalRestaurantID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Restaurants_Categories.Header + Restaurants_Categories.Columns
                + "VALUES(@resID,@cateID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", InternalRestaurantID);
                Insert.Parameters.AddWithValue("@cateID", internalCategoryID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static public Task<bool> ManageTransactions(SqlConnection con, string[] transactions, Int internalResID)
        {
            Task<bool> result = Task.FromResult(true);
            foreach (var name in transactions)
            {
                if (name != null)
                {
                    SmallInt? transID = HasTransaction(con, name);
                    if (transID.HasValue)
                        AddRestaurants_Transactions(con, transID.Value, internalResID);
                    else
                    {
                        SmallInt? newID = GenerateTransactionID(con).Result;
                        if (newID.HasValue)
                        {
                            if (!HasTransaction(con, name).HasValue)
                                AddTransaction(con, newID.Value, name);
                            //add to Restaurants_Transactions
                            AddRestaurants_Transactions(con, newID.Value, internalResID);
                        }
                        else
                        {
                            result = Task.FromResult(false);
                            break;
                        }
                    }
                }
            }
            return result;
        }
        static private Task AddRestaurants_Transactions(SqlConnection con, SmallInt internalTransactionID, Int internalResID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Restaurants_Transactions.Header + Restaurants_Transactions.Columns
                + "VALUES(@resID,@transID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", internalResID);
                Insert.Parameters.AddWithValue("@transID", internalTransactionID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static private Task AddRestaurants_Reviews(SqlConnection con, BigInt internalRevID, Int internalResID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Restaurants_Reviews.Header + Restaurants_Reviews.Columns
                + "VALUES(@resID,@revID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", internalResID);
                Insert.Parameters.AddWithValue("@revID", internalRevID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }


        //Get
        static public Task<Categories.Record[]> Get_All_Categories(SqlConnection con)
        {
            List<Categories.Record> result = new List<Categories.Record>();
            using (SqlCommand Get = new SqlCommand("SELECT*FROM" + Categories.Header, con))
            {
                using (SqlDataReader reader = Get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Categories.Record cate = new Categories.Record((SmallInt)reader[0], reader.GetString(1), reader.GetString(2));
                        result.Add(cate);
                    }
                }
            }
            return Task.FromResult(result.ToArray());
        }
        static public Task<Categories.Record> Get_Category(SqlConnection con, SmallInt cateID)
        {
            Categories.Record cate = null;
            using (SqlCommand Get = new SqlCommand("SELECT*FROM" + Categories.Header + "WHERE[ID]=@cateID", con))
            {
                Get.Parameters.AddWithValue("@cateID", cateID);
                using (SqlDataReader reader = Get.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cate = new Categories.Record((SmallInt)reader[0], reader.GetString(1), reader.GetString(2));
                    }
                }
            }
            return Task.FromResult(cate);
        }
        static public Task<Restaurants.Record[]> Get_All_Restaurants(SqlConnection con)
        {
            List<Restaurants.Record> result = new List<Restaurants.Record>();
            using (SqlCommand Get = new SqlCommand("SELECT*FROM" + Restaurants.Header, con))
            {
                using (SqlDataReader reader = Get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Restaurants.Record res = new Restaurants.Record((Int)reader[0], reader.GetString(1), reader.GetString(2))
                        {
                            phone = reader.IsDBNull(5) ? null : reader.GetString(5),
                            display_phone = reader.IsDBNull(6) ? null : reader.GetString(6),
                            image_url = reader.IsDBNull(7) ? null : reader.GetString(7),
                            address1 = reader.IsDBNull(8) ? null : reader.GetString(8),
                            address2 = reader.IsDBNull(9) ? null : reader.GetString(9),
                            address3 = reader.IsDBNull(10) ? null : reader.GetString(10),
                            city = reader.IsDBNull(11) ? null : reader.GetString(11),
                            country = reader.IsDBNull(12) ? null : reader.GetString(12),
                            state = reader.IsDBNull(13) ? null : reader.GetString(13),
                            zip_code = reader.IsDBNull(14) ? null : reader.GetString(14),
                            price = reader.IsDBNull(15) ? null : reader.GetString(15),
                            url = reader.IsDBNull(18) ? null : reader.GetString(18)
                        };
                        if (!reader.IsDBNull(3)) res.latitude = Convert.ToDouble(reader[3]);
                        if (!reader.IsDBNull(4)) res.longitude = Convert.ToDouble(reader[4]);
                        if (!reader.IsDBNull(16)) res.rating = float.Parse(reader[16].ToString());
                        if (!reader.IsDBNull(17)) res.review_count = (Int)reader[17];
                        result.Add(res);
                    }
                }
            }
            return Task.FromResult(result.ToArray());
        }
        static public Task<Transactions.Record[]> Get_All_Transactions(SqlConnection con)
        {
            List<Transactions.Record> result = new List<Transactions.Record>();
            using (SqlCommand Get = new SqlCommand("SELECT*FROM" + Transactions.Header, con))
            {
                using (SqlDataReader reader = Get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Transactions.Record((SmallInt)reader[0], reader.GetString(1)));
                    }
                }
            }
            return Task.FromResult(result.ToArray());
        }
        static public Task<Reviews.Record[]> Get_All_Reviews(SqlConnection con)
        {
            List<Reviews.Record> result = new List<Reviews.Record>();
            using (SqlCommand Get = new SqlCommand("SELECT*FROM" + Reviews.Header, con))
            {
                using (SqlDataReader reader = Get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Reviews.Record review = new Reviews.Record((BigInt)reader[0], reader.GetString(1))
                        {
                            text = reader.IsDBNull(2) ? null : reader.GetString(2),
                            url = reader.IsDBNull(3) ? null : reader.GetString(3),
                            user_name = reader.IsDBNull(6) ? null : reader.GetString(6)
                        };
                        if (!reader.IsDBNull(4)) review.rating = (TinyInt)reader[4];
                        if (!reader.IsDBNull(5)) review.time_created = reader.GetDateTime(5);
                        result.Add(review);
                    }
                }
            }
            return Task.FromResult(result.ToArray());
        }
        static public Task<BigInt[]> Get_Restaurant_Reviews(SqlConnection con, Int resID)
        {
            List<BigInt> result = new List<BigInt>();
            using (SqlCommand Get = new SqlCommand("SELECT[ReviewID]FROM" + Restaurants_Reviews.Header + "WHERE[RestaurantID]=@resID", con))
            {
                Get.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Get.ExecuteReader())
                {
                    while (reader.Read()) result.Add((BigInt)reader[0]);
                }
            }
            return Task.FromResult(result.ToArray());
        }
        static public Task<SmallInt[]> Get_Restaurant_Categories(SqlConnection con, Int resID)
        {
            List<SmallInt> result = new List<SmallInt>();
            using (SqlCommand Get = new SqlCommand("SELECT[CategoryID]FROM" + Restaurants_Categories.Header + "WHERE[RestaurantID]=@resID", con))
            {
                Get.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Get.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add((SmallInt)reader[0]);
                }
            }
            return Task.FromResult(result.ToArray());
        }
        static public Task<SmallInt[]> Get_Restaurant_Transactions(SqlConnection con, Int resID)
        {
            List<SmallInt> result = new List<SmallInt>();
            using (SqlCommand Get = new SqlCommand("SELECT[TransactionID]FROM" + Restaurants_Transactions.Header + "WHERE[RestaurantID]=@resID", con))
            {
                Get.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Get.ExecuteReader())
                {
                    while (reader.Read()) result.Add((SmallInt)reader[0]);
                }
            }
            return Task.FromResult(result.ToArray());
        }
        static public Task<KeyValuePair<Int, BigInt>[]> Get_All_Restaurants_Reviews_Relation(SqlConnection con)
        {
            List<KeyValuePair<Int, BigInt>> result = new List<KeyValuePair<Int, BigInt>>();
            using (SqlCommand Get = new SqlCommand("SELECT*FROM" + Restaurants_Reviews.Header, con))
            {
                using (SqlDataReader reader = Get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new KeyValuePair<Int, BigInt>((Int)reader[0], (BigInt)reader[1]));
                    }
                }
            }
            return Task.FromResult(result.ToArray());
        }
        static public Task<KeyValuePair<Int, SmallInt>[]> Get_All_Restaurants_Transactions_Relation(SqlConnection con)
        {
            List<KeyValuePair<Int, SmallInt>> result = new List<KeyValuePair<Int, SmallInt>>();
            using (SqlCommand Get = new SqlCommand("SELECT*FROM" + Restaurants_Transactions.Header, con))
            {
                using (SqlDataReader reader = Get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new KeyValuePair<Int, SmallInt>((Int)reader[0], (SmallInt)reader[1]));
                    }
                }
            }
            return Task.FromResult(result.ToArray());
        }
    }
}
