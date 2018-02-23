using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bot_Application.API;
using SmallInt = System.Int16;
using Int = System.Int32;
using BigInt = System.Int64;
using TinyInt = System.Byte;

namespace Bot_Application.Database.third_party_database
{
    static class YelpData
    {
        static readonly string DatabaseHeader = "[YelpData].";
        static class Table_Categories
        {
            public static string Header = "[dbo].[Categories]";
            public static string Columns = "([ID],[title],[alias])";
        }
        static class Table_Transactions
        {
            public static string Header = "[dbo].[Transactions]";
            public static string Columns = "([ID],[transactionName])";
        }
        static class Table_Restaurants
        {
            public static string Header = "[dbo].[Restaurants]";
            public static string Columns = @"([ID],[Yelp_ID],[name],[latitude],[longitude],[phone],
               [display_phone],[image_url],[address1],[address2],[address3],[city],[country],
               [state],[zip_code],[price],[rating],[review_count],[url])";
        }
        static class Table_Reviews
        {
            public static string Header = "[dbo].[Reviews]";
            public static string Columns = "([ID],[Yelp_ID],[text],[url],[rating],[time_created],[user_name])";
        }
        static private class Table_Restaurants_Categories
        {
            public static string Header = "[dbo].[Restaurants_Categories]";
            public static string Columns = "([RestaurantID],[CategoryID])";
        }
        static private class Table_Restaurants_Reviews
        {
            public static string Header = "[dbo].[Restaurants_Reviews]";
            public static string Columns = "([RestaurantID],[ReviewID])";
        }
        static private class Table_Restaurants_TransactionID
        {
            public static string Header = "[dbo].[Restaurants_Transactions]";
            public static string Columns = "([RestaurantID],[TransactionID])";
        }
        //internal id, not YelpID, return null if faild
        static private Task<SmallInt?> GenerateCategoryID(SqlConnection con, SmallInt offset = 0)
        {
            SmallInt? result = SmallInt.MinValue;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
                + Table_Categories.Header + "ORDER BY[ID]ASC", con))
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
        //internal id, not YelpID, return null if faild
        static private Task<SmallInt?> GenerateTransactionID(SqlConnection con)
        {
            SmallInt? result = SmallInt.MinValue;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
                + Table_Transactions.Header + "ORDER BY[ID]ASC", con))
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
        //internal id, not YelpID, return null if faild
        static private Task<Int?> GenerateRestaurantID(SqlConnection con)
        {
            Int? result = Int.MinValue;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
                + Table_Restaurants.Header + "ORDER BY[ID]ASC", con))
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
        //internal id, not YelpID, return null if faild
        static private Task<BigInt?> GenerateReviewID(SqlConnection con)
        {
            BigInt? result = BigInt.MinValue;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader
                + Table_Reviews.Header + "ORDER BY[ID]ASC", con))
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
        //simple add or remove restaurant to Table_Restaurants
        static private Task AddRestaurant(SqlConnection con, Int internalID, YelpClient.BusinessesJsonObject res)
        {
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Restaurants.Header + Table_Restaurants.Columns
        + "VALUES(@id,@yelpID,@name,@lat,@lon,@phone,@displayPhone,@image,@address1,@address2," +
        "@address3,@city,@country,@state,@zip,@price,@rating,@reviewCount,@url)", con))
            {
                Insertion.Parameters.AddWithValue("@id", internalID);
                Insertion.Parameters.AddWithValue("@yelpID", res.id);
                Insertion.Parameters.AddWithValue("@name", res.name);
                if (res.coordinates != null)
                {
                    if (res.coordinates.latitude.HasValue) Insertion.Parameters.AddWithValue("@lat", res.coordinates.latitude.Value);
                    else Insertion.Parameters.AddWithValue("@lat", DBNull.Value);
                    if (res.coordinates.longitude.HasValue) Insertion.Parameters.AddWithValue("@lon", res.coordinates.longitude.Value);
                    else Insertion.Parameters.AddWithValue("@lon", DBNull.Value);
                }
                if (res.phone != null && res.phone.Trim() != "")
                    Insertion.Parameters.AddWithValue("@phone", res.phone.Trim());
                else Insertion.Parameters.AddWithValue("@phone", DBNull.Value);
                if (res.display_phone != null && res.display_phone.Trim() != "")
                    Insertion.Parameters.AddWithValue("@displayPhone", res.display_phone.Trim());
                else Insertion.Parameters.AddWithValue("@displayPhone", DBNull.Value);
                if (res.image_url != null && res.image_url.Trim() != "")
                    Insertion.Parameters.AddWithValue("@image", res.image_url.Trim());
                else Insertion.Parameters.AddWithValue("@image", DBNull.Value);
                if (res.location != null)
                {
                    if (res.location.address1 != null && res.location.address1.Trim() != "")
                        Insertion.Parameters.AddWithValue("@address1", res.location.address1.Trim());
                    else Insertion.Parameters.AddWithValue("@address1", DBNull.Value);
                    if (res.location.address2 != null && res.location.address2.Trim() != "")
                        Insertion.Parameters.AddWithValue("@address2", res.location.address2.Trim());
                    else Insertion.Parameters.AddWithValue("@address2", DBNull.Value);
                    if (res.location.address3 != null && res.location.address3.Trim() != "")
                        Insertion.Parameters.AddWithValue("@address3", res.location.address3.Trim());
                    else Insertion.Parameters.AddWithValue("@address3", DBNull.Value);
                    if (res.location.city != null && res.location.city.Trim() != "")
                        Insertion.Parameters.AddWithValue("@city", res.location.city.Trim() != "");
                    else Insertion.Parameters.AddWithValue("@city", DBNull.Value);
                    if (res.location.country != null && res.location.country.Trim() != "")
                        Insertion.Parameters.AddWithValue("@country", res.location.country.Trim());
                    else Insertion.Parameters.AddWithValue("@country", DBNull.Value);
                    if (res.location.state != null && res.location.state.Trim() != "")
                        Insertion.Parameters.AddWithValue("@state", res.location.state);
                    else Insertion.Parameters.AddWithValue("@state", DBNull.Value);
                    if (res.location.zip_code != null && res.location.zip_code.Trim() != "")
                        Insertion.Parameters.AddWithValue("@zip", res.location.zip_code);
                    else Insertion.Parameters.AddWithValue("@zip", DBNull.Value);
                }
                if (res.price != null && res.price.Trim() != "")
                    Insertion.Parameters.AddWithValue("@price", res.price);
                else Insertion.Parameters.AddWithValue("@price", DBNull.Value);
                if (res.rating.HasValue) Insertion.Parameters.AddWithValue("@rating", res.rating.Value);
                else Insertion.Parameters.AddWithValue("@rating", DBNull.Value);
                if (res.review_count.HasValue) Insertion.Parameters.AddWithValue("@reviewCount", res.review_count.Value);
                else Insertion.Parameters.AddWithValue("@reviewCount", DBNull.Value);
                if (res.url != null && res.url.Trim() != "")
                    Insertion.Parameters.AddWithValue("@url", res.url);
                else Insertion.Parameters.AddWithValue("@url", DBNull.Value);
                Insertion.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static private Task RemoveRestaurant(SqlConnection con, Int internalID)
        {
            using (SqlCommand Delete = new SqlCommand("DELETE FROM" + DatabaseHeader + Table_Restaurants.Header
                + "WHERE[ID]=@id"))
            {
                Delete.Parameters.AddWithValue("@id", internalID);
                Delete.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static private bool HasRestaurant(SqlConnection con, string Yelp_ID)
        {
            bool result = false;
            using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader + Table_Restaurants.Header
                + "WHERE[Yelp_ID]=@id"))
            {
                Check.Parameters.AddWithValue("@id", Yelp_ID);
                using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
            }
            return result;
        }
        //return internal assigned id, do not add reviews, return null if faild, call Categories and Transactions management function
        static public Int? AddRestaurantAndManage(SqlConnection con, YelpClient.BusinessesJsonObject res)
        {
            Int? result = null;
            if (res != null)
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
                    BigInt? newID = GenerateReviewID(con).Result;
                    if (newID.HasValue)
                    {
                        if (rev != null && !HasReview(con, newID.Value, internalResID))
                        {
                            AddReview(con, newID.Value, rev, internalResID);
                            //add to Restaurants_Reviews
                            AddRestaurants_Reviews(con, newID.Value, internalResID);
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            else result = false;
            return result;
        }
        //name should not be null
        static private Task AddCategory(SqlConnection con, SmallInt internalID, string title, string alias)
        {
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Categories.Header + Table_Categories.Columns
                + "VALUES(@id,@title,@alias)", con))
            {
                Insertion.Parameters.AddWithValue("@id", internalID);
                Insertion.Parameters.AddWithValue("@title", title);
                Insertion.Parameters.AddWithValue("@alias", alias);
                Insertion.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static private Task RemoveCategory(SqlConnection con, SmallInt internalID)
        {
            using (SqlCommand Delete = new SqlCommand("DELETE FROM" + DatabaseHeader + Table_Categories.Header
                + "WHERE[ID]=@id", con))
            {
                Delete.Parameters.AddWithValue("@id", internalID);
                Delete.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static public bool HasCategory(SqlConnection con, string title)
        {
            bool result = false;
            if (title != null)
            {
                using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader + Table_Categories.Header
                    + "WHERE[title]=@title", con))
                {
                    Check.Parameters.AddWithValue("@title", title);
                    using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
                }
            }
            return result;
        }
        static private bool HasCategory(SqlConnection con, SmallInt internalID)
        {
            bool result = false;
            using (SqlCommand Check = new SqlCommand("SELECT[ID]" + DatabaseHeader + Table_Categories.Header
                + "WHERE[ID]=@id", con))
            {
                Check.Parameters.AddWithValue("@id", internalID);
                using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
            }
            return result;
        }
        static private Task AddTransaction(SqlConnection con, SmallInt internalID, string name)
        {
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Transactions.Header + Table_Transactions.Columns
                + "VALUES(@id,@name)", con))
            {
                Insertion.Parameters.AddWithValue("@id", internalID);
                Insertion.Parameters.AddWithValue("@name", name);
                Insertion.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static private Task DeletTransaction(SqlConnection con, SmallInt internalID)
        {
            using (SqlCommand Delete = new SqlCommand("DELETE FROM" + DatabaseHeader + Table_Transactions.Header
                + "WHERE[ID]=@id"))
            {
                Delete.Parameters.AddWithValue("@id", internalID);
                Delete.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static public bool HasTransaction(SqlConnection con, string name)
        {
            bool result = false;
            if (name != null)
            {
                using (SqlCommand Check = new SqlCommand("SELECT[ID]FROM" + DatabaseHeader + Table_Transactions.Header
                    + "WHERE[transactionName]=@name", con))
                {
                    Check.Parameters.AddWithValue("@name", name);
                    using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
                }
            }
            return result;
        }
        static private bool HasTransaction(SqlConnection con, SmallInt internalID)
        {
            bool result = false;
            using (SqlCommand Check = new SqlCommand("SELECT[ID]" + DatabaseHeader + Table_Transactions.Header
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
            using (SqlCommand Check = new SqlCommand("SELECT[ReviewID]FROM" + DatabaseHeader + Table_Restaurants_Reviews.Header
                + "WHERE[RestaurantID]=@resID AND[ReviewID]=@revID", con))
            {
                Check.Parameters.AddWithValue("@resID", internalResID);
                Check.Parameters.AddWithValue("@revID", internalRevID);
                using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
            }
            return result;
        }
        static private bool HasReview(SqlConnection con, string reviewYelpID, Int internalResID)
        {
            bool result = false;
            using (SqlCommand Check = new SqlCommand("SELECT[ReviewID]FROM" + DatabaseHeader + Table_Reviews.Header
                + "WHERE[RestaurantID]=@resID AND[ReviewID]=@revID", con))
            {
                Check.Parameters.AddWithValue("@resID", internalResID);
                Check.Parameters.AddWithValue("@revID", reviewYelpID);
                using (SqlDataReader reader = Check.ExecuteReader()) if (reader.Read()) result = true;
            }
            return result;
        }
        static private Task AddReview(SqlConnection con, BigInt internalRevID, YelpClient.ReviewJsonObject review, Int internalResID)
        {
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Reviews.Header + Table_Reviews.Columns
                + "VALUES(@id,@yelpID,@text,@url,@rating,@timeCreated,@userName)", con))
            {
                Insertion.Parameters.AddWithValue("@id", internalRevID);
                Insertion.Parameters.AddWithValue("@yelpID", review.id);
                if (review.text != null && review.text.Trim() != "") Insertion.Parameters.AddWithValue("@text", review.text);
                else Insertion.Parameters.AddWithValue("@text", DBNull.Value);
                if (review.url != null && review.url.Trim() != "") Insertion.Parameters.AddWithValue("@url", review.url);
                else Insertion.Parameters.AddWithValue("@url", DBNull.Value);
                if (review.rating.HasValue) Insertion.Parameters.AddWithValue("@rating", review.rating.Value);
                else Insertion.Parameters.AddWithValue("@rating", DBNull.Value);
                if (review.time_created.HasValue) Insertion.Parameters.AddWithValue("@timeCreated", review.time_created.Value);
                else Insertion.Parameters.AddWithValue("@timeCreated", DBNull.Value);
                if (review.user != null && review.user.name != null && review.user.name.Trim() != "")
                    Insertion.Parameters.AddWithValue("@userName", review.user.name);
                else Insertion.Parameters.AddWithValue("@userName", DBNull.Value);
                Insertion.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        //return true if success
        static public Task<bool> ManageCategories(SqlConnection con, YelpClient.CategoriesJsonObject[] cateObjects, Int internalResID)
        {
            Task<bool> result = Task.FromResult(true);
            foreach (var cate in cateObjects)
            {
                if (!HasCategory(con, cate.title))
                {
                    SmallInt? newID = GenerateCategoryID(con).Result;
                    if (newID.HasValue)
                    {
                        AddCategory(con, newID.Value, cate.title, cate.alias);
                        //add to Table_Restaurants_Categories
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
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Restaurants_Categories.Header + Table_Restaurants_Categories.Columns
                + "VALUES(@resID,@cateID)", con))
            {
                Insertion.Parameters.AddWithValue("@resID", InternalRestaurantID);
                Insertion.Parameters.AddWithValue("@cateID", internalCategoryID);
                Insertion.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static public Task<bool> ManageTransactions(SqlConnection con, string[] transactions, Int internalResID)
        {
            Task<bool> result = Task.FromResult(true);
            foreach (var name in transactions)
            {
                if (name != null && !HasTransaction(con, name))
                {
                    SmallInt? newID = GenerateTransactionID(con).Result;
                    if (newID.HasValue)
                    {
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
            return result;
        }
        static private Task AddRestaurants_Transactions(SqlConnection con, SmallInt internalTransactionID, Int internalResID)
        {
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Restaurants_TransactionID.Header + Table_Restaurants_TransactionID.Columns
                + "VALUES(@resID,@transID)", con))
            {
                Insertion.Parameters.AddWithValue("@resID", internalResID);
                Insertion.Parameters.AddWithValue("@transID", internalTransactionID);
                Insertion.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        static private Task AddRestaurants_Reviews(SqlConnection con, BigInt internalRevID, Int internalResID)
        {
            using (SqlCommand Insertion = new SqlCommand("INSERT INTO" + DatabaseHeader + Table_Restaurants_Reviews.Header + Table_Restaurants_Reviews.Columns
                + "VALUES(@resID,@revID)", con))
            {
                Insertion.Parameters.AddWithValue("@resID", internalResID);
                Insertion.Parameters.AddWithValue("@revID", internalRevID);
                Insertion.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
    }
}