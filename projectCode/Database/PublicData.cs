using System.Data.SqlClient;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using SmallInt = System.Int16;
using Int = System.Int32;
using BigInt = System.Int64;
using TinyInt = System.Byte;

namespace Bot_Application.Database
{
    static class PublicData//the Encapsulation of all providers' databases
    {
        public const string DatabaseHeader = "[PublicData].";
        //tables
        public static class DataProvider
        {
            public const string Header = "[dbo].[DataProvider]";
            public const string Columns = "([ID],[name],[homepage])";
            //smallint nvarchar nvarchar
            public class Record
            {
                public SmallInt ID;
                public string name { private set; get; }
                public string homepage;
                public Record(string name) { this.name = name; }
                internal Record(SmallInt ID, string name) { this.ID = ID; this.name = name; }
            }
        }
        public static class Nation//archive
        {
            public const string Header = "[dbo].[Nation]";
            public const string Columns = @"([ID],[nameLocale],[ISO3166_1ALPHA2],[nameEnglish],
            [ISO4217CurrencyCode],[currencySymbol])";
            //smallint char(2) varchar nvarchar char(3)
            public class Record
            {
                public SmallInt ID;
                public string nameLocale;
                public string ISO3166_1ALPHA2;
                public string nameEnglish;
                public string ISO4217CurrencyCode;
                public char? currencySymbol;
                public Record(string nameLocale) { this.nameLocale = nameLocale; }
                internal Record(SmallInt ID, string nameLocale) { this.ID = ID; this.nameLocale = nameLocale; }
            }
        }
        private static class Nation_City//archive
        {
            public const string Header = "[dbo].[Nation_City]";
            public const string Columns = "([nationID],[cityID])";
            //smallint smallint
        }
        public static class City//archive
        {
            public const string Header = "[dbo].[City]";
            public const string Columns = "([ID],[nameLocale],[ISO3166_2Code],[nameEnglish])";
            //smallint varchar nvarchar varchar
            public class Record
            {
                public SmallInt ID;
                public string nameLocale { private set; get; }
                public string nameEnglish;
                public Record(string nameLocale) { this.nameLocale = nameLocale; }
                internal Record(SmallInt ID, string nameLocale) { this.ID = ID; this.nameLocale = nameLocale; }
            }
        }
        private static class City_Res
        {
            public const string Header = "[dbo].[City_Res]";
            public const string Columns = "([cityID],[resID])";
            //smallint int
        }
        public static class Res//may need update
        {
            public const string Header = "[dbo].[Res]";
            public const string Columns = @"([ID],[name],[address],[priceLevel],[description],[latitude],[longitude],
            [zipcode],[currencyID],[openTime],[closeTime],[avgCostByOne],[avgCostByTwo],
            [image],[photos],[events],[homepage],[phone],[phoneToShow],[DataProviderID],[providerPage],[customID])";
            //int nvarchar nvarchar tinyint nvarchar decimal(19,17) decimal(19,16)
            //varchar smallint char(4) char(4) decimal(19,2) decimal(19,2)
            //nvarchar nvarchar nvarchar nvarchar varchar smallint providerPage
            public class Record
            {
                public Int ID;
                public string name { private set; get; }
                public string address;
                public TinyInt? priceLevel;
                public string description;
                public double? latitude;
                public double? longitude;
                public string zipcode;
                public SmallInt? currencyID;
                public string openTime;
                public string closeTime;
                public double? avgCostByOne;
                public double? avgCostByTwo;
                public string image;//url
                public string photos;//url
                public string events;//url
                public string homepage;//url
                public string phone;//phone num
                public string phoneToShow;
                public SmallInt? DataProviderID;
                public string providerPage;
                public string customID;
                public Record(string name) { this.name = name; }
                internal Record(Int ID, string name) { this.ID = ID; this.name = name; }
            }
        }
        private static class Res_CateCuisine
        {
            public const string Header = "[dbo].[Res_CateCuisine]";
            public const string Columns = "([resID],[cuisineID])";
            //int smallint
        }
        private static class Res_CateDishType
        {
            public const string Header = "[dbo].[Res_CateDishType]";
            public const string Columns = "([resID],[dishTypeID])";
            //int int
        }
        private static class Res_CateEstablish
        {
            public const string Header = "[dbo].[Res_CateEstablish]";
            public const string Columns = "([resID],[estID])";
            //int smallint
        }
        private static class Res_CateOther
        {
            public const string Header = "[dbo].[Res_CateOther]";
            public const string Columns = "([resID],[otherID])";
            //int int
        }
        private static class Res_CateTaste
        {
            public const string Header = "[dbo].[Res_CateTaste]";
            public const string Columns = "([resID],[tasteID])";
            //int tinyint
        }
        private static class Res_Dish
        {
            public const string Header = "[dbo].[Res_Dish]";
            public const string Columns = "([resID],[dishID])";
            //int bigint
        }
        private static class Res_Rating
        {
            public const string Header = "[dbo].[Res_Rating]";
            public const string Columns = "([resID],[ratingID])";
            //int bigint
        }
        private static class Res_Transaction
        {
            public const string Header = "[dbo].[Res_Transaction]";
            public const string Columns = "([resID],[transID],[providerID])";
            //int tinyint smallint
        }
        public static class Dish
        {
            public const string Header = "[dbo].[Dishes]";
            public const string Columns = @"([ID],[name],[price],[description],
            [currencyID],[photos],[DataProviderID],[providerPage],[customID])";
            //bigint nvarchar decimal(19,2) nvarchar smallint nvarchar smallint nvarchar
            public class Record
            {
                public BigInt ID;
                public string name { private set; get; }
                public double? price;
                public string description;
                public SmallInt? currencyID;
                public string photos;
                public SmallInt? DataProviderID;
                public string providerPage;
                public string customID;
                public Record(string name) { this.name = name; }
                internal Record(BigInt ID, string name) { this.ID = ID; this.name = name; }
            }
        }
        private static class Dish_CateCuisine
        {
            public const string Header = "[dbo].[Dish_CateCuisine]";
            public const string Columns = "([dishID],[cuisineID])";
            //bigint smallint
        }
        private static class Dish_CateOther
        {
            public const string Header = "[dbo].[Dish_CateOther]";
            public const string Columns = "([dishID],[otherID])";
            //bigint int
        }
        private static class Dish_CateTaste
        {
            public const string Header = "[dbo].[Dish_CateTaste]";
            public const string Columns = "([dishID],[tasteID])";
            //bigint tinyint
        }
        private static class Dish_Rating
        {
            public const string Header = "[dbo].[Dish_Rating]";
            public const string Columns = "([dishID],[ratingID])";
            //bigint bigint
        }
        public static class CateCuisine
        {
            public const string Header = "[dbo].[CateCuisine]";
            public const string Columns = "([ID],[name],[originalCountryID])";
            //smallint nvarchar smallint
            public class Record
            {
                public SmallInt ID;
                public string name { private set; get; }
                public SmallInt? originalCountryID;
                public Record(string name) { this.name = name; }
                internal Record(SmallInt ID, string name) { this.ID = ID; this.name = name; }
            }
        }
        public static class CateDishType
        {
            public const string Header = "[dbo].[CateDishType]";
            public const string Columns = "([ID],[name])";
            //smallint nvarchar
            public class Record
            {
                public Int ID;
                public string name { private set; get; }
                public Record(string name) { this.name = name; }
                internal Record(Int ID, string name) { this.ID = ID; this.name = name; }
            }
        }
        public static class CateEstablish
        {
            public const string Header = "[dbo].[CateEstablish]";
            public const string Columns = "([ID],[name])";
            //smallint nvarchar
            public class Record
            {
                public SmallInt ID;
                public string name { private set; get; }
                public Record(string name) { this.name = name; }
                internal Record(SmallInt ID, string name) { this.ID = ID; this.name = name; }
            }
        }
        public static class CateTaste
        {
            public const string Header = "[dbo].[CateTaste]";
            public const string Columns = "([ID],[name])";
            //tinyint nvarchar
            public class Record
            {
                public TinyInt ID;
                public string name { private set; get; }
                public Record(string name) { this.name = name; }
                internal Record(TinyInt ID, string name) { this.ID = ID; this.name = name; }
            }
        }
        public static class CateOther
        {
            public const string Header = "[dbo].[CateOther]";
            public const string Columns = "([ID],[name])";
            //int nvarchar
            public class Record
            {
                public Int ID;
                public string name { private set; get; }
                public Record(string name) { this.name = name; }
                internal Record(Int ID, string name) { this.ID = ID; this.name = name; }
            }
        }
        public static class Rating//may need frequent update
        {
            public const string Header = "[dbo].[Rating]";
            public const string Columns = @"([ID],[rating],[userName],[userLevel],[likes]," +
                "[comment],[nationID],[DataProviderID])";
            //bigint tinyint nvarchar tinyint int nvarchar smallint smallint
            public class Record
            {
                public BigInt ID;
                public TinyInt rating { private set; get; }//0-100
                public string userName;
                public TinyInt? userLevel;//0-100
                public Int? likes;
                public string comment;
                public SmallInt? DataProviderID;
                public SmallInt? nationID;
                public Record(TinyInt rating) { this.rating = rating; }
                internal Record(BigInt ID, TinyInt rating) { this.ID = ID; this.rating = rating; }
            }
        }
        public static class Transaction
        {
            public const string Header = "[dbo].[Transaction]";
            public const string Columns = "([ID],[type],[link])";
            //tinyint nvarchar nvarchar
            public class Record
            {
                public TinyInt ID;
                public string type { private set; get; }
                public string link;
                public Record(string type) { this.type = type; }
                internal Record(TinyInt ID, string type) { this.ID = ID; this.type = type; }
            }
        }

        internal static SmallInt NewCityID(SqlConnection con)
        {
            SmallInt result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + City.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SmallInt curID = SmallInt.MinValue;
                        for (; curID <= SmallInt.MaxValue; ++curID)
                        {
                            if (curID < (SmallInt)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (SmallInt)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = SmallInt.MinValue;
                }
            }
            return result;
        }
        internal static Int NewResID(SqlConnection con)
        {
            Int result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + Res.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Int curID = Int.MinValue;
                        for (; curID <= Int.MaxValue; ++curID)
                        {
                            if (curID < (Int)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (Int)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = Int.MinValue;
                }
            }
            return result;
        }
        internal static SmallInt NewDataProviderID(SqlConnection con)
        {
            SmallInt result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + DataProvider.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SmallInt curID = SmallInt.MinValue;
                        for (; curID <= SmallInt.MaxValue; ++curID)
                        {
                            if (curID < (SmallInt)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (SmallInt)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = SmallInt.MinValue;
                }
            }
            return result;
        }
        internal static BigInt NewDishID(SqlConnection con)
        {
            BigInt result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + Dish.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        BigInt curID = BigInt.MinValue;
                        for (; curID <= BigInt.MaxValue; ++curID)
                        {
                            if (curID < (BigInt)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (BigInt)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = BigInt.MinValue;
                }
            }
            return result;
        }
        internal static SmallInt NewCateCuisineID(SqlConnection con)
        {
            SmallInt result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + CateCuisine.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SmallInt curID = SmallInt.MinValue;
                        for (; curID <= SmallInt.MaxValue; ++curID)
                        {
                            if (curID < (SmallInt)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (SmallInt)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = SmallInt.MinValue;
                }
            }
            return result;
        }
        internal static Int NewCateDishTypeID(SqlConnection con)
        {
            Int result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + CateDishType.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Int curID = Int.MinValue;
                        for (; curID <= Int.MaxValue; ++curID)
                        {
                            if (curID < (Int)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (Int)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = Int.MinValue;
                }
            }
            return result;
        }
        internal static SmallInt NewCateEstablishID(SqlConnection con)
        {
            SmallInt result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + CateEstablish.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SmallInt curID = SmallInt.MinValue;
                        for (; curID <= SmallInt.MaxValue; ++curID)
                        {
                            if (curID < (SmallInt)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (SmallInt)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = SmallInt.MinValue;
                }
            }
            return result;
        }
        internal static TinyInt NewCateTasteID(SqlConnection con)
        {
            TinyInt result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + CateTaste.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        TinyInt curID = TinyInt.MinValue;
                        for (; curID <= TinyInt.MaxValue; ++curID)
                        {
                            if (curID < (TinyInt)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (TinyInt)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = TinyInt.MinValue;
                }
            }
            return result;
        }
        internal static Int NewCateOtherID(SqlConnection con)
        {
            Int result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + CateOther.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Int curID = Int.MinValue;
                        for (; curID <= Int.MaxValue; ++curID)
                        {
                            if (curID < (Int)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (Int)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = Int.MinValue;
                }
            }
            return result;
        }
        internal static BigInt NewRatingID(SqlConnection con)
        {
            BigInt result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + Rating.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        BigInt curID = BigInt.MinValue;
                        for (; curID <= BigInt.MaxValue; ++curID)
                        {
                            if (curID < (BigInt)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (BigInt)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = BigInt.MinValue;
                }
            }
            return result;
        }
        internal static TinyInt NewTransactionID(SqlConnection con)
        {
            TinyInt result = 0;
            using (SqlCommand GetIDs = new SqlCommand("SELECT[ID]FROM"
                + Transaction.Header + "ORDER BY[ID]ASC", con))
            {
                using (SqlDataReader reader = GetIDs.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        TinyInt curID = TinyInt.MinValue;
                        for (; curID <= TinyInt.MaxValue; ++curID)
                        {
                            if (curID < (TinyInt)reader[0])
                            {
                                result = curID;
                                break;
                            }
                            else
                            {
                                if (reader.Read()) ;
                                else
                                {
                                    result = (TinyInt)(curID + 1);//throw overflow if id is used up
                                    break;
                                }
                            }
                        }
                    }
                    else result = TinyInt.MinValue;
                }
            }
            return result;
        }
        internal static Task<Res.Record> ReadAsRestaurant(SqlDataReader reader)
        {
            Res.Record res = null;
            if (reader.Read())
            {
                res = new Res.Record((Int)reader[0], reader.GetString(1))
                {
                    address = reader.IsDBNull(2) ? null : reader.GetString(2),
                    description = reader.IsDBNull(4) ? null : reader.GetString(4),
                    zipcode = reader.IsDBNull(7) ? null : reader.GetString(7),
                    openTime = reader.IsDBNull(9) ? null : reader.GetString(9),
                    closeTime = reader.IsDBNull(10) ? null : reader.GetString(10),
                    image = reader.IsDBNull(13) ? null : reader.GetString(13),
                    photos = reader.IsDBNull(14) ? null : reader.GetString(14),
                    events = reader.IsDBNull(15) ? null : reader.GetString(15),
                    homepage = reader.IsDBNull(16) ? null : reader.GetString(16),
                    phone = reader.IsDBNull(17) ? null : reader.GetString(17),
                    phoneToShow = reader.IsDBNull(18) ? null : reader.GetString(18),
                    providerPage = reader.IsDBNull(20) ? null : reader.GetString(20),
                    customID = reader.IsDBNull(21) ? null : reader.GetString(21)

                };
                if (!reader.IsDBNull(3)) res.priceLevel = (TinyInt)reader[3];
                if (!reader.IsDBNull(5)) res.latitude = Convert.ToDouble(reader[5]);
                if (!reader.IsDBNull(6)) res.longitude = Convert.ToDouble(reader[6]);
                if (!reader.IsDBNull(8)) res.currencyID = (SmallInt)reader[8];
                if (!reader.IsDBNull(11)) res.avgCostByOne = Convert.ToDouble(reader[11]);
                if (!reader.IsDBNull(12)) res.avgCostByTwo = Convert.ToDouble(reader[12]);
                if (!reader.IsDBNull(19)) res.DataProviderID = (SmallInt)reader[19];
            }
            return Task.FromResult(res);
        }
        internal static Task<Res.Record[]> ReadAsRestaurants(SqlDataReader reader)
        {
            List<Res.Record> result = new List<Res.Record>();
            while (reader.Read())
            {
                Res.Record res = new Res.Record((Int)reader[0], reader.GetString(1))
                {
                    address = reader.IsDBNull(2) ? null : reader.GetString(2),
                    description = reader.IsDBNull(4) ? null : reader.GetString(4),
                    zipcode = reader.IsDBNull(7) ? null : reader.GetString(7),
                    openTime = reader.IsDBNull(9) ? null : reader.GetString(9),
                    closeTime = reader.IsDBNull(10) ? null : reader.GetString(10),
                    image = reader.IsDBNull(13) ? null : reader.GetString(13),
                    photos = reader.IsDBNull(14) ? null : reader.GetString(14),
                    events = reader.IsDBNull(15) ? null : reader.GetString(15),
                    homepage = reader.IsDBNull(16) ? null : reader.GetString(16),
                    phone = reader.IsDBNull(17) ? null : reader.GetString(17),
                    phoneToShow = reader.IsDBNull(18) ? null : reader.GetString(18),
                    providerPage = reader.IsDBNull(20) ? null : reader.GetString(20),
                    customID = reader.IsDBNull(21) ? null : reader.GetString(21)
                };
                if (!reader.IsDBNull(3)) res.priceLevel = (TinyInt)reader[3];
                if (!reader.IsDBNull(5)) res.latitude = Convert.ToDouble(reader[5]);
                if (!reader.IsDBNull(6)) res.longitude = Convert.ToDouble(reader[6]);
                if (!reader.IsDBNull(8)) res.currencyID = (SmallInt)reader[8];
                if (!reader.IsDBNull(11)) res.avgCostByOne = Convert.ToDouble(reader[11]);
                if (!reader.IsDBNull(12)) res.avgCostByTwo = Convert.ToDouble(reader[12]);
                if (!reader.IsDBNull(19)) res.DataProviderID = (SmallInt)reader[19]; result.Add(res);
            }
            return Task.FromResult(result.ToArray());
        }
        internal static Task<Dish.Record> ReadAsDish(SqlDataReader reader)
        {
            Dish.Record dish = null;
            if (reader.Read())
            {
                dish = new Dish.Record((BigInt)reader[0], reader.GetString(1))
                {
                    description = reader.IsDBNull(3) ? null : reader.GetString(3),
                    photos = reader.IsDBNull(5) ? null : reader.GetString(5),
                    providerPage = reader.IsDBNull(7) ? null : reader.GetString(7),
                    customID = reader.IsDBNull(8) ? null : reader.GetString(8)
                };
                if (!reader.IsDBNull(2)) dish.price = Convert.ToDouble(reader[2]);
                if (!reader.IsDBNull(4)) dish.currencyID = (SmallInt)reader[4];
                if (!reader.IsDBNull(6)) dish.DataProviderID = (SmallInt)reader[6];
            }
            return Task.FromResult(dish);
        }
        internal static Task<Dish.Record[]> ReadAsDishes(SqlDataReader reader)
        {
            List<Dish.Record> result = new List<Dish.Record>();
            while (reader.Read())
            {
                Dish.Record dish = new Dish.Record((BigInt)reader[0], reader.GetString(1))
                {
                    description = reader.IsDBNull(3) ? null : reader.GetString(3),
                    photos = reader.IsDBNull(5) ? null : reader.GetString(5),
                    providerPage = reader.IsDBNull(7) ? null : reader.GetString(7),
                    customID = reader.IsDBNull(8) ? null : reader.GetString(8)
                };
                if (!reader.IsDBNull(2)) dish.price = Convert.ToDouble(reader[2]);
                if (!reader.IsDBNull(4)) dish.currencyID = (SmallInt)reader[4];
                if (!reader.IsDBNull(6)) dish.DataProviderID = (SmallInt)reader[6];
                result.Add(dish);
            }
            return Task.FromResult(result.ToArray());
        }
        internal static Task<CateCuisine.Record> ReadAsCateCuisine(SqlDataReader reader)
        {
            CateCuisine.Record cuisine = null;
            if (reader.Read())
            {
                cuisine = new CateCuisine.Record((SmallInt)reader[0], reader.GetString(1));
                if (!reader.IsDBNull(2)) cuisine.originalCountryID = (SmallInt)reader[2];
            }
            return Task.FromResult(cuisine);
        }
        internal static Task<CateCuisine.Record[]> ReadAsCateCuisines(SqlDataReader reader)
        {
            List<CateCuisine.Record> result = new List<CateCuisine.Record>();
            while (reader.Read())
            {
                CateCuisine.Record cuisine = new CateCuisine.Record((SmallInt)reader[0], reader.GetString(1));
                if (!reader.IsDBNull(2)) cuisine.originalCountryID = (SmallInt)reader[2];
                result.Add(cuisine);
            }
            return Task.FromResult(result.ToArray());
        }
        internal static Task<CateDishType.Record> ReadAsCateDishType(SqlDataReader reader)
        {
            CateDishType.Record dishType = null;
            if (reader.Read())
            {
                dishType = new CateDishType.Record((Int)reader[0], reader.GetString(1));
            }
            return Task.FromResult(dishType);
        }
        internal static Task<CateDishType.Record[]> ReadAsCateDishTypes(SqlDataReader reader)
        {
            List<CateDishType.Record> result = new List<CateDishType.Record>();
            while (reader.Read())
            {
                CateDishType.Record dishType = new CateDishType.Record((Int)reader[0], reader.GetString(1));
                result.Add(dishType);
            }
            return Task.FromResult(result.ToArray());
        }
        internal static Task<CateEstablish.Record> ReadAsCateEstablish(SqlDataReader reader)
        {
            CateEstablish.Record establishment = null;
            if (reader.Read())
            {
                establishment = new CateEstablish.Record((SmallInt)reader[0], reader.GetString(1));
            }
            return Task.FromResult(establishment);
        }
        internal static Task<CateEstablish.Record[]> ReadAsCateEstablishs(SqlDataReader reader)
        {
            List<CateEstablish.Record> result = new List<CateEstablish.Record>();
            while (reader.Read())
            {
                CateEstablish.Record establishment = new CateEstablish.Record((SmallInt)reader[0], reader.GetString(1));
                result.Add(establishment);
            }
            return Task.FromResult(result.ToArray());
        }
        internal static Task<CateOther.Record> ReadAsCateOther(SqlDataReader reader)
        {
            CateOther.Record other = null;
            if (reader.Read())
            {
                other = new CateOther.Record((Int)reader[0], reader.GetString(1));
            }
            return Task.FromResult(other);
        }
        internal static Task<CateOther.Record[]> ReadAsCateOthers(SqlDataReader reader)
        {
            List<CateOther.Record> result = new List<CateOther.Record>();
            while (reader.Read())
            {
                CateOther.Record other = new CateOther.Record((Int)reader[0], reader.GetString(1));
                result.Add(other);
            }
            return Task.FromResult(result.ToArray());
        }
        internal static Task<CateTaste.Record> ReadAsCateTaste(SqlDataReader reader)
        {
            CateTaste.Record taste = null;
            if (reader.Read())
            {
                taste = new CateTaste.Record((TinyInt)reader[0], reader.GetString(1));
            }
            return Task.FromResult(taste);
        }
        internal static Task<CateTaste.Record[]> ReadAsCateTastes(SqlDataReader reader)
        {
            List<CateTaste.Record> result = new List<CateTaste.Record>();
            while (reader.Read())
            {
                CateTaste.Record taste = new CateTaste.Record((TinyInt)reader[0], reader.GetString(1));
                result.Add(taste);
            }
            return Task.FromResult(result.ToArray());
        }
        internal static Task<Rating.Record> ReadAsRating(SqlDataReader reader)
        {
            Rating.Record rating = null;
            if (reader.Read())
            {
                rating = new Rating.Record((BigInt)reader[0], (TinyInt)reader[1])
                {
                    userName = reader.IsDBNull(2) ? null : reader.GetString(2),
                    comment = reader.IsDBNull(5) ? null : reader.GetString(5),
                };
                if (!reader.IsDBNull(3)) rating.userLevel = (TinyInt)reader[3];
                if (!reader.IsDBNull(4)) rating.likes = (Int)reader[4];
                if (!reader.IsDBNull(6)) rating.DataProviderID = (SmallInt)reader[6];
                if (!reader.IsDBNull(7)) rating.nationID = (SmallInt)reader[7];
            }
            return Task.FromResult(rating);
        }
        internal static Task<Rating.Record[]> ReadAsRatings(SqlDataReader reader)
        {
            List<Rating.Record> result = new List<Rating.Record>();
            while (reader.Read())
            {
                Rating.Record rating = new Rating.Record((BigInt)reader[0], (TinyInt)reader[1])
                {
                    userName = reader.IsDBNull(2) ? null : reader.GetString(2),
                    comment = reader.IsDBNull(5) ? null : reader.GetString(5),
                };
                if (!reader.IsDBNull(3)) rating.userLevel = (TinyInt)reader[3];
                if (!reader.IsDBNull(4)) rating.likes = (Int)reader[4];
                if (!reader.IsDBNull(6)) rating.DataProviderID = (SmallInt)reader[6];
                if (!reader.IsDBNull(7)) rating.nationID = (SmallInt)reader[7];
                result.Add(rating);
            }
            return Task.FromResult(result.ToArray());
        }
        internal static Task<Transaction.Record> ReadAsTransaction(SqlDataReader reader)
        {
            Transaction.Record trans = null;
            if (reader.Read())
            {
                trans = new Transaction.Record((TinyInt)reader[0], reader.GetString(1));
                trans.link = reader.IsDBNull(2) ? null : reader.GetString(2);
            }
            return Task.FromResult(trans);
        }
        internal static Task<Transaction.Record[]> ReadAsTransactions(SqlDataReader reader)
        {
            List<Transaction.Record> result = new List<Transaction.Record>();
            while (reader.Read())
            {
                Transaction.Record trans = new Transaction.Record((TinyInt)reader[0], reader.GetString(1));
                trans.link = reader.IsDBNull(2) ? null : reader.GetString(2);
                result.Add(trans);
            }
            return Task.FromResult(result.ToArray());
        }
        //do not check whether IDs exist, do not check whether ralations are exist
        internal static Task Add_Res(SqlConnection con, Res.Record res, Int ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Res.Header + Res.Columns
                + "VALUES(@ID,@name,@address,@priceLevel,@description,@latitude,@longitude,"
                + "@zipcode,@currencyID,@openTime,@closeTime,@avgCostByOne,@avgCostByTwo,"
                + "@image,@photos,@events,@homepage,@phone,@phoneToShow,@DataProviderID,@providerPage,@customID)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@name", res.name);
                if (res.address != null && res.address.Trim() != "")
                    Insert.Parameters.AddWithValue("@address", res.address.Trim());
                else Insert.Parameters.AddWithValue("@address", DBNull.Value);
                if (res.priceLevel.HasValue) Insert.Parameters.AddWithValue("@priceLevel", res.priceLevel.Value);
                else Insert.Parameters.AddWithValue("@priceLevel", DBNull.Value);
                if (res.description != null && res.description.Trim() != "")
                    Insert.Parameters.AddWithValue("@description", res.description.Trim());
                else Insert.Parameters.AddWithValue("@description", DBNull.Value);
                if (res.latitude.HasValue) Insert.Parameters.AddWithValue("@latitude", res.latitude.Value);
                else Insert.Parameters.AddWithValue("@latitude", DBNull.Value);
                if (res.longitude.HasValue) Insert.Parameters.AddWithValue("@longitude", res.longitude.Value);
                else Insert.Parameters.AddWithValue("@longitude", DBNull.Value);
                if (res.zipcode != null && res.zipcode.Trim() != "")
                    Insert.Parameters.AddWithValue("@zipcode", res.zipcode.Trim());
                else Insert.Parameters.AddWithValue("@zipcode", DBNull.Value);
                if (res.currencyID.HasValue) Insert.Parameters.AddWithValue("@currencyID", res.currencyID.Value);
                else Insert.Parameters.AddWithValue("@currencyID", DBNull.Value);
                if (res.openTime != null && res.openTime.Length > 0)
                    Insert.Parameters.AddWithValue("@openTime", res.openTime);
                else Insert.Parameters.AddWithValue("@openTime", DBNull.Value);
                if (res.closeTime != null && res.closeTime.Length > 0)
                    Insert.Parameters.AddWithValue("@closeTime", res.closeTime);
                else Insert.Parameters.AddWithValue("@closeTime", DBNull.Value);
                if (res.avgCostByOne.HasValue) Insert.Parameters.AddWithValue("@avgCostByOne", res.avgCostByOne.Value);
                else Insert.Parameters.AddWithValue("@avgCostByOne", DBNull.Value);
                if (res.avgCostByTwo.HasValue) Insert.Parameters.AddWithValue("@avgCostByTwo", res.avgCostByTwo.Value);
                else Insert.Parameters.AddWithValue("@avgCostByTwo", DBNull.Value);
                if (res.image != null && res.image.Trim() != "")
                    Insert.Parameters.AddWithValue("@image", res.image.Trim());
                else Insert.Parameters.AddWithValue("@image", DBNull.Value);
                if (res.photos != null && res.photos.Trim() != "")
                    Insert.Parameters.AddWithValue("@photos", res.photos.Trim());
                else Insert.Parameters.AddWithValue("@photos", DBNull.Value);
                if (res.events != null && res.events.Trim() != "")
                    Insert.Parameters.AddWithValue("@events", res.events.Trim());
                else Insert.Parameters.AddWithValue("@events", DBNull.Value);
                if (res.homepage != null && res.homepage.Trim() != "")
                    Insert.Parameters.AddWithValue("@homepage", res.homepage.Trim());
                else Insert.Parameters.AddWithValue("@homepage", DBNull.Value);
                if (res.phone != null && res.phone.Trim() != "")
                    Insert.Parameters.AddWithValue("@phone", res.phone.Trim());
                else Insert.Parameters.AddWithValue("@phone", DBNull.Value);
                if (res.phoneToShow != null && res.phoneToShow.Trim() != "")
                    Insert.Parameters.AddWithValue("@phoneToShow", res.phoneToShow.Trim());
                else Insert.Parameters.AddWithValue("@phoneToShow", DBNull.Value);
                if (res.DataProviderID.HasValue) Insert.Parameters.AddWithValue("@DataProviderID", res.DataProviderID);
                else Insert.Parameters.AddWithValue("@DataProviderID", DBNull.Value);
                if (res.providerPage != null && res.providerPage.Trim() != "")
                    Insert.Parameters.AddWithValue("@providerPage", res.providerPage.Trim());
                else Insert.Parameters.AddWithValue("@providerPage", DBNull.Value);
                if (res.customID != null && res.customID.Trim() != "")
                    Insert.Parameters.AddWithValue("@customID", res.customID);
                else Insert.Parameters.AddWithValue("@customID", DBNull.Value);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Dish(SqlConnection con, Dish.Record dish, BigInt ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Dish.Header + Dish.Columns
                + "VALUES(@ID,@name,@price,@description@currencyID,@photos,@DataProviderID,@providerPage@customID)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@name", dish.name);
                if (dish.price.HasValue) Insert.Parameters.AddWithValue("@price", dish.price.Value);
                else Insert.Parameters.AddWithValue("@price", DBNull.Value);
                if (dish.description != null && dish.description.Trim() != "")
                    Insert.Parameters.AddWithValue("@description", dish.description.Trim());
                else Insert.Parameters.AddWithValue("@description", DBNull.Value);
                if (dish.currencyID.HasValue) Insert.Parameters.AddWithValue("@currencyID", dish.currencyID.Value);
                else Insert.Parameters.AddWithValue("@currencyID", DBNull.Value);
                if (dish.photos != null && dish.photos.Trim() != "")
                    Insert.Parameters.AddWithValue("@photos", dish.photos.Trim());
                else Insert.Parameters.AddWithValue("@photos", DBNull.Value);
                if (dish.DataProviderID.HasValue) Insert.Parameters.AddWithValue("@DataProviderID", dish.DataProviderID.Value);
                else Insert.Parameters.AddWithValue("@DataProviderID", DBNull.Value);
                if (dish.providerPage != null && dish.providerPage.Trim() != "")
                    Insert.Parameters.AddWithValue("@providerPage", dish.providerPage.Trim());
                else Insert.Parameters.AddWithValue("@providerPage", DBNull.Value);
                if (dish.customID != null) Insert.Parameters.AddWithValue("@customID", dish.customID);
                else Insert.Parameters.AddWithValue("@customID", DBNull.Value);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_CateCuisine(SqlConnection con, CateCuisine.Record cuisine, SmallInt ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + CateCuisine.Header + CateCuisine.Columns
                + "VALUES(@ID,@name,@originalCountryID)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@name", cuisine.name);
                if (cuisine.originalCountryID.HasValue) Insert.Parameters.AddWithValue("@originalCountryID", cuisine.originalCountryID.Value);
                else Insert.Parameters.AddWithValue("@originalCountryID", DBNull.Value);
            }
            return Task.CompletedTask;
        }
        internal static Task Add_CateDishType(SqlConnection con, CateDishType.Record dishType, Int ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + CateDishType.Header + CateDishType.Columns
                + "VALUES(@ID,@name)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@name", dishType.name);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_CateEstablish(SqlConnection con, CateEstablish.Record est, SmallInt ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + CateEstablish.Header + CateEstablish.Columns
                + "VALUES(@ID,@name)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@name", est.name);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_CateOther(SqlConnection con, CateOther.Record other, Int ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + CateOther.Header + CateOther.Columns
                + "VALUES(@ID,@name)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@name", other.name);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_CateTaste(SqlConnection con, CateTaste.Record taste, TinyInt ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + CateTaste.Header + CateTaste.Columns
                + "VALUES(@ID,@name)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@name", taste.name);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_City(SqlConnection con, City.Record city, SmallInt ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + City.Header + City.Columns
                + "VALUES(@ID,@nameLocale,@ISO3166_2Code,@nameEnglish)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@nameLocale", city.nameLocale);
                if (city.nameEnglish != null && city.nameEnglish.Trim() != "")
                    Insert.Parameters.AddWithValue("@nameEnglish", city.nameEnglish.Trim());
                else Insert.Parameters.AddWithValue("@nameEnglish", DBNull.Value);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_DateProvider(SqlConnection con, DataProvider.Record provider, SmallInt ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + DataProvider.Header + DataProvider.Columns
                + "VALUES(@ID,@name,@homepage)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@name", provider.name);
                if (provider.homepage != null && provider.homepage.Trim() != "")
                    Insert.Parameters.AddWithValue("@homepage", provider.homepage.Trim());
                else Insert.Parameters.AddWithValue("@homepage", DBNull.Value);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Nation(SqlConnection con, Nation.Record nation, SmallInt ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Nation.Header + Nation.Columns
                + "VALUES(@ID,@nameLocale,@ISO3166_1ALPHA2,@nameEnglish,@ISO4217CurrencyCode,@currencySymbol)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@nameLocale", nation.nameLocale);
                if (nation.ISO3166_1ALPHA2 != null && nation.ISO3166_1ALPHA2.Length > 0)
                    Insert.Parameters.AddWithValue("@ISO3166_1ALPHA2", nation.ISO3166_1ALPHA2);
                else Insert.Parameters.AddWithValue("@ISO3166_1ALPHA2", DBNull.Value);
                if (nation.nameEnglish != null && nation.nameEnglish.Trim() != "")
                    Insert.Parameters.AddWithValue("@nameEnglish", nation.nameEnglish.Trim());
                else Insert.Parameters.AddWithValue("@nameEnglish", DBNull.Value);
                if (nation.ISO4217CurrencyCode != null && nation.ISO4217CurrencyCode.Length > 0)
                    Insert.Parameters.AddWithValue("@ISO4217CurrencyCode", nation.ISO4217CurrencyCode);
                else Insert.Parameters.AddWithValue("@ISO4217CurrencyCode", DBNull.Value);
                if (nation.currencySymbol.HasValue) Insert.Parameters.AddWithValue("@currencySymbol", nation.currencySymbol.Value);
                else Insert.Parameters.AddWithValue("@currencySymbol", DBNull.Value);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Rating(SqlConnection con, Rating.Record rating, BigInt ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Rating.Header + Rating.Columns
                + "VALUES(@ID,@rating,@userName,@userLevel,@likes,@comment,@DataProviderID,@nationID)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@rating", rating.rating);
                if (rating.userName != null) Insert.Parameters.AddWithValue("@userName", rating.userName);
                else Insert.Parameters.AddWithValue("@userName", DBNull.Value);
                if (rating.userLevel.HasValue) Insert.Parameters.AddWithValue("@userLevel", rating.userLevel.Value);
                else Insert.Parameters.AddWithValue("@userLevel", DBNull.Value);
                if (rating.likes.HasValue) Insert.Parameters.AddWithValue("@likes", rating.likes.Value);
                else Insert.Parameters.AddWithValue("@likes", DBNull.Value);
                if (rating.comment != null && rating.comment.Trim() != "")
                    Insert.Parameters.AddWithValue("@comment", rating.comment.Trim());
                else Insert.Parameters.AddWithValue("@comment", DBNull.Value);
                if (rating.DataProviderID.HasValue) Insert.Parameters.AddWithValue("@DataProviderID", rating.DataProviderID.Value);
                else Insert.Parameters.AddWithValue("@DataProviderID", DBNull.Value);
                if (rating.nationID.HasValue) Insert.Parameters.AddWithValue("@nationID", rating.nationID.Value);
                else Insert.Parameters.AddWithValue("@nationID", DBNull.Value);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Transaction(SqlConnection con, Transaction.Record trans, TinyInt ID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Transaction.Header + Transaction.Columns
                + "VALUES(@ID,@type,@link)", con))
            {
                Insert.Parameters.AddWithValue("@ID", ID);
                Insert.Parameters.AddWithValue("@type", trans.type);
                if (trans.link != null && trans.link.Trim() != "")
                    Insert.Parameters.AddWithValue("@link", trans.link.Trim());
                else Insert.Parameters.AddWithValue("@link", DBNull.Value);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_City_Res(SqlConnection con, SmallInt cityID, Int resID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + City_Res.Header + City_Res.Columns
                + "VALUES(@cityID,@resID)", con))
            {
                Insert.Parameters.AddWithValue("@cityID", cityID);
                Insert.Parameters.AddWithValue("@resID", resID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Dish_CateCuisine(SqlConnection con, BigInt dishID, SmallInt cuisineID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Dish_CateCuisine.Header + Dish_CateCuisine.Columns
                + "VALUES(@dishID,@cuisineID)", con))
            {
                Insert.Parameters.AddWithValue("@dishID", dishID);
                Insert.Parameters.AddWithValue("@cuisineID", cuisineID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Dish_CateOther(SqlConnection con, BigInt dishID, Int otherID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Dish_CateOther.Header + Dish_CateOther.Columns
                + "VALUES(@dishID,@otherID)", con))
            {
                Insert.Parameters.AddWithValue("@dishID", dishID);
                Insert.Parameters.AddWithValue("@otherID", otherID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Dish_CateTaste(SqlConnection con, BigInt dishID, TinyInt tasteID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Dish_CateTaste.Header + Dish_CateTaste.Columns
                + "VALUES(@dishID,@tasteID)", con))
            {
                Insert.Parameters.AddWithValue("@dishID", dishID);
                Insert.Parameters.AddWithValue("@tasteID", tasteID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Dish_Rating(SqlConnection con, BigInt dishID, BigInt ratingID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Dish_Rating.Header + Dish_Rating.Columns
                + "VALUES(@dishID,@ratingID)", con))
            {
                Insert.Parameters.AddWithValue("@dishID", dishID);
                Insert.Parameters.AddWithValue("@ratingID", ratingID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Nation_City(SqlConnection con, SmallInt nationID, SmallInt cityID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Nation_City.Header + Nation_City.Columns
                + "VALUES(@nationID,@cityID)", con))
            {
                Insert.Parameters.AddWithValue("@nationID", nationID);
                Insert.Parameters.AddWithValue("@cityID", cityID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Res_CateCuisine(SqlConnection con, Int resID, SmallInt cuisineID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Res_CateCuisine.Header + Res_CateCuisine.Columns
                + "VALUES(@resID,@cuisineID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", resID);
                Insert.Parameters.AddWithValue("@cuisineID", cuisineID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Res_CateDishType(SqlConnection con, Int resID, Int dishTypeID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Res_CateDishType.Header + Res_CateDishType.Columns
                + "VALUES(@resID,@dishTypeID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", resID);
                Insert.Parameters.AddWithValue("@dishTypeID", dishTypeID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Res_CateEstablish(SqlConnection con, Int resID, SmallInt estID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Res_CateEstablish.Header + Res_CateEstablish.Columns
                + "VALUES(@resID,@estID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", resID);
                Insert.Parameters.AddWithValue("@estID", estID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Res_CateOther(SqlConnection con, Int resID, Int otherID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Res_CateOther.Header + Res_CateOther.Columns
                + "VALUES(@resID,@otherID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", resID);
                Insert.Parameters.AddWithValue("@otherID", otherID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Res_CateTaste(SqlConnection con, Int resID, TinyInt tasteID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Res_CateTaste.Header + Res_CateTaste.Columns
                + "VALUES(@resID,@tasteID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", resID);
                Insert.Parameters.AddWithValue("@tasteID", tasteID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Res_Rating(SqlConnection con, Int resID, BigInt ratingID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Res_Rating.Header + Res_Rating.Columns
                + "VALUES(@resID,@ratingID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", resID);
                Insert.Parameters.AddWithValue("@ratingID", ratingID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Res_Dish(SqlConnection con, Int resID, BigInt dishID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Res_Dish.Header + Res_Dish.Columns
                + "VALUES(@resID,@dishID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", resID);
                Insert.Parameters.AddWithValue("@dishID", dishID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }
        internal static Task Add_Res_Transaction(SqlConnection con, Int resID, TinyInt transID, SmallInt providerID)
        {
            using (SqlCommand Insert = new SqlCommand("INSERT INTO" + Res_Transaction.Header
                + "VALUES(@resID,@transID,@providerID)", con))
            {
                Insert.Parameters.AddWithValue("@resID", resID);
                Insert.Parameters.AddWithValue("@transID", transID);
                Insert.Parameters.AddWithValue("@providerID", providerID);
                Insert.ExecuteNonQuery();
            }
            return Task.CompletedTask;
        }



        //Check existence
        public static Task<bool> Is_NationSupported(SqlConnection con, SmallInt ISO3166CountryNumericCode)
        {
            bool result = false;
            using (SqlCommand Search = new SqlCommand("SELECT[ID]FROM" + Nation.Header
                + "WHERE[ID]=@id", con))
            {
                Search.Parameters.AddWithValue("@id", ISO3166CountryNumericCode);
                using (SqlDataReader reader = Search.ExecuteReader()) if (reader.Read()) result = true;
            }
            return Task.FromResult(result);
        }
        public static Task<SmallInt[]> Is_CitySupported(SqlConnection con, string name)
        {
            List<SmallInt> possibleIDs = new List<SmallInt>();
            using (SqlCommand Search = new SqlCommand("SELECT[ID]FROM" + City.Header
                + "WHERE[nameEnglish]=@name OR[nameLocale]=@name", con))
            {
                Search.Parameters.AddWithValue("@name", name);
                using (SqlDataReader reader = Search.ExecuteReader())
                    while (reader.Read()) possibleIDs.Add((SmallInt)reader[0]);
            }
            return Task.FromResult(possibleIDs.ToArray());
        }
        public static Task<string> Is_CitySupported(SqlConnection con, SmallInt cityID)
        {
            string name = null;
            using (SqlCommand Search = new SqlCommand("SELECT[nameLocale]FROM" + City.Header + "WHERE[ID]=@cityID", con))
            {
                Search.Parameters.AddWithValue("@cityID", cityID);
                using (SqlDataReader reader = Search.ExecuteReader())
                    if (reader.Read()) name = reader.GetString(0);
            }
            return Task.FromResult(name);
        }
        public static Task<Int[]> Has_Restaurant(SqlConnection con, SmallInt cityID, string name)
        {
            List<Int> possibleIDs = new List<Int>();
            using (SqlCommand Search = new SqlCommand("SELECT[ID]FROM" + Res.Header + "WHERE[ID]IN(" +
                "SELECT[resID]FROM" + City_Res.Header + "WHERE[cityID]=@cityID)" +
                "AND[name]=@name", con))
            {
                Search.Parameters.AddWithValue("@name", name);
                Search.Parameters.AddWithValue("@cityID", cityID);
                using (SqlDataReader reader = Search.ExecuteReader())
                    while (reader.Read()) possibleIDs.Add((Int)reader[0]);
            }
            return Task.FromResult(possibleIDs.ToArray());
        }
        public static Task<string> Has_Restaurant(SqlConnection con, Int resID)
        {
            string name = null;
            using (SqlCommand Search = new SqlCommand("SELECT[name]FROM" + City.Header + "WHERE[ID]=@resID", con))
            {
                Search.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Search.ExecuteReader())
                    if (reader.Read()) name = reader.GetString(0);
            }
            return Task.FromResult(name);

        }
        public static Task<string> Has_Dish(SqlConnection con, BigInt dishID)
        {
            string name = null;
            using (SqlCommand Search = new SqlCommand("SELECT[name]FROM" + Dish.Header + "WHERE[ID]=@dishID", con))
            {
                Search.Parameters.AddWithValue("@dishID", dishID);
                using (SqlDataReader reader = Search.ExecuteReader())
                    if (reader.Read()) name = reader.GetString(0);
            }
            return Task.FromResult(name);
        }
        public static Task<BigInt[]> Has_Dish(SqlConnection con, Int resID, string name)
        {
            List<BigInt> possibleIDs = new List<BigInt>();
            using (SqlCommand Search = new SqlCommand("SELECT[ID]FROM" + Dish.Header + "WHERE[ID]IN(" +
                "SELECT[dishID]FROM" + Res_Dish.Header + "WHERE[resID]=@resID)" +
                "AND[name]=@name", con))
            {
                Search.Parameters.AddWithValue("@resID", resID);
                Search.Parameters.AddWithValue("@name", name);
                using (SqlDataReader reader = Search.ExecuteReader())
                    while (reader.Read()) possibleIDs.Add((BigInt)reader[0]);
            }
            return Task.FromResult(possibleIDs.ToArray());
        }

        //Get records
        public static Task<Res.Record> Get_Restaurant(SqlConnection con, Int resID)
        {
            Res.Record result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + Res.Header + "WHERE[ID]=@id", con))
            {
                Search.Parameters.AddWithValue("@id", resID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsRestaurant(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<Dish.Record> Get_Dish(SqlConnection con, BigInt dishID)
        {
            Dish.Record result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + Dish.Header + "WHERE[ID]=@id", con))
            {
                Search.Parameters.AddWithValue("@id", dishID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsDish(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<Res.Record[]> Get_Restaurants_Nearby(SqlConnection con, double lat, double lon, SmallInt cityID, int radius)
        {
            Res.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + Res.Header + "WHERE[ID]IN(" +
                "SELECT[resID]FROM" + City_Res.Header + "WHERE[cityID]=@cityID)" +
                "AND ABS(" + Res.Header + ".[latitude]-@lat)<@radius" +
                "AND ABS(" + Res.Header + ".[longitude]-@lon)<@radius", con))
            {
                Search.Parameters.AddWithValue("@cityID", cityID);
                Search.Parameters.AddWithValue("@lat", lat);
                Search.Parameters.AddWithValue("@lon", lon);
                Search.Parameters.AddWithValue("@radius", radius);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsRestaurants(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<Dish.Record[]> Get_Restaurant_Menu(SqlConnection con, Int resID)
        {
            Dish.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + Dish.Header + "WHERE[ID]IN(" +
                "SELECT[dishID]FROM" + Res_Dish.Header + "WHERE[resID]=@resID", con))
            {
                Search.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsDishes(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<Rating.Record[]> Get_Restaurant_Rating(SqlConnection con, Int resID)
        {
            Rating.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + Rating.Header + "WHERE[ID]IN(" +
                "SELECT[ratingID]FROM" + Res_Dish.Header + "WHERE[resID]=@resID", con))
            {
                Search.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsRatings(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<CateCuisine.Record[]> Get_Restaurant_Cuisines(SqlConnection con, Int resID)
        {
            CateCuisine.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + CateCuisine.Header + "WHERE[ID]IN(" +
                "SELECT[cuisineID]FROM" + Res_CateCuisine.Header + "WHERE[resID]=@resID", con))
            {
                Search.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsCateCuisines(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<CateDishType.Record[]> Get_Restaurant_DishTypes(SqlConnection con, Int resID)
        {
            CateDishType.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + CateDishType.Header + "WHERE[ID]IN(" +
                "SELECT[dishTypeID]FROM" + Res_CateDishType.Header + "WHERE[resID]=@resID", con))
            {
                Search.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsCateDishTypes(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<CateEstablish.Record[]> Get_Restaurant_Establishments(SqlConnection con, Int resID)
        {
            CateEstablish.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + CateEstablish.Header + "WHERE[ID]IN(" +
                "SELECT[estID]FROM" + Res_CateEstablish.Header + "WHERE[resID]=@resID", con))
            {
                Search.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsCateEstablishs(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<CateTaste.Record[]> Get_Restaurant_Taste(SqlConnection con, Int resID)
        {
            CateTaste.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + CateTaste.Header + "WHERE[ID]IN(" +
                "SELECT[tasteID]FROM" + Res_CateTaste.Header + "WHERE[resID]=@resID", con))
            {
                Search.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsCateTastes(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<CateOther.Record[]> Get_Restaurant_OtherTags(SqlConnection con, Int resID)
        {
            CateOther.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + CateOther.Header + "WHERE[ID]IN(" +
                "SELECT[otherID]FROM" + Res_CateTaste.Header + "WHERE[resID]=@resID", con))
            {
                Search.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsCateOthers(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<Transaction.Record[]> Get_Restaurant_Transactions(SqlConnection con, Int resID)
        {
            Transaction.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + Transaction.Header + "WHERE[ID]IN(" +
                "SELECT[transID]FROM" + Res_Transaction.Header + "WHERE[resID]=@resID", con))
            {
                Search.Parameters.AddWithValue("@resID", resID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsTransactions(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<CateCuisine.Record[]> Get_Dish_Cuisines(SqlConnection con, BigInt dishID)
        {
            CateCuisine.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + CateCuisine.Header + "WHERE[ID]IN(" +
                "SELECT[cuisineID]FROM" + Dish_CateCuisine.Header + "WHERE[dishID]=@dishID", con))
            {
                Search.Parameters.AddWithValue("@dishID", dishID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsCateCuisines(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<CateTaste.Record[]> Get_Dish_Taste(SqlConnection con, BigInt dishID)
        {
            CateTaste.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + CateTaste.Header + "WHERE[ID]IN(" +
                "SELECT[tasteID]FROM" + Dish_CateTaste.Header + "WHERE[dishID]=@dishID", con))
            {
                Search.Parameters.AddWithValue("@dishID", dishID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsCateTastes(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<CateOther.Record[]> Get_Dish_OtherTags(SqlConnection con, BigInt dishID)
        {
            CateOther.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + CateOther.Header + "WHERE[ID]IN(" +
                "SELECT[otherID]FROM" + Dish_CateTaste.Header + "WHERE[dishID]=@dishID", con))
            {
                Search.Parameters.AddWithValue("@dishID", dishID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsCateOthers(reader).Result;
                }
            }
            return Task.FromResult(result);
        }
        public static Task<Rating.Record[]> Get_Dish_Rating(SqlConnection con, BigInt dishID)
        {
            Rating.Record[] result = null;
            using (SqlCommand Search = new SqlCommand("SELECT*FROM" + Rating.Header + "WHERE[ID]IN(" +
                "SELECT[ratingID]FROM" + Dish_Rating.Header + "WHERE[dishID]=@dishID", con))
            {
                Search.Parameters.AddWithValue("@dishID", dishID);
                using (SqlDataReader reader = Search.ExecuteReader())
                {
                    result = ReadAsRatings(reader).Result;
                }
            }
            return Task.FromResult(result);
        }

        //Add records, caution:ID won't be read, because PublicData generates internal ID for any record, return internal assigned id
        public static async Task<Int?> Add_Restaurant(SqlConnection con, Res.Record res, SmallInt cityID)
        {
            Int? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewResID(con);
                    await Add_Res(con, res, newID.Value);
                    await Add_City_Res(con, cityID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
        public static async Task<BigInt?> Add_Restaurant_Rating(SqlConnection con, Rating.Record rating, Int resID)
        {
            BigInt? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewRatingID(con);
                    await Add_Rating(con, rating, newID.Value);
                    await Add_Res_Rating(con, resID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
        public static async Task<SmallInt?> Add_Restaurant_CateCuisine(SqlConnection con, CateCuisine.Record cuisine, Int resID)
        {
            SmallInt? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewCateCuisineID(con);
                    await Add_CateCuisine(con, cuisine, newID.Value);
                    await Add_Res_CateCuisine(con, resID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
        public static async Task<Int?> Add_Restaurant_CateDishType(SqlConnection con, CateDishType.Record dishType, Int resID)
        {
            Int? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewCateDishTypeID(con);
                    await Add_CateDishType(con, dishType, newID.Value);
                    await Add_Res_CateDishType(con, resID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
        public static async Task<SmallInt?> Add_Restaurant_CateEstablish(SqlConnection con, CateEstablish.Record est, Int resID)
        {
            SmallInt? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewCateEstablishID(con);
                    await Add_CateEstablish(con, est, newID.Value);
                    await Add_Res_CateEstablish(con, resID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
        public static async Task<TinyInt?> Add_Restaurant_CateTaste(SqlConnection con, CateTaste.Record taste, Int resID)
        {
            TinyInt? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewCateTasteID(con);
                    await Add_CateTaste(con, taste, newID.Value);
                    await Add_Res_CateTaste(con, resID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
        public static async Task<Int?> Add_Restaurant_CateOther(SqlConnection con, CateOther.Record other, Int resID)
        {
            Int? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewCateOtherID(con);
                    await Add_CateOther(con, other, newID.Value);
                    await Add_Res_CateOther(con, resID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
        public static async Task<BigInt?> Add_Dish(SqlConnection con, Dish.Record dish, Int resID)
        {
            BigInt? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewDishID(con);
                    await Add_Dish(con, dish, newID.Value);
                    await Add_Res_Dish(con, resID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
        public static async Task<BigInt?> Add_Dish_Rating(SqlConnection con, Rating.Record rating, BigInt dishID)
        {
            BigInt? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewRatingID(con);
                    await Add_Rating(con, rating, newID.Value);
                    await Add_Dish_Rating(con, dishID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;

        }
        public static async Task<SmallInt?> Add_Dish_CateCuisine(SqlConnection con, CateCuisine.Record cuisine, Int resID)
        {
            SmallInt? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewCateCuisineID(con);
                    await Add_CateCuisine(con, cuisine, newID.Value);
                    await Add_Dish_CateCuisine(con, resID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
        public static async Task<TinyInt?> Add_Dish_CateTaste(SqlConnection con, CateTaste.Record taste, Int resID)
        {
            TinyInt? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewCateTasteID(con);
                    await Add_CateTaste(con, taste, newID.Value);
                    await Add_Dish_CateTaste(con, resID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
        public static async Task<Int?> Add_Dish_CateOther(SqlConnection con, CateOther.Record other, Int resID)
        {
            Int? newID = null;
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    newID = NewCateOtherID(con);
                    await Add_CateOther(con, other, newID.Value);
                    await Add_Dish_CateOther(con, resID, newID.Value);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    newID = null;
                }
            }
            return newID;
        }
    }
}
