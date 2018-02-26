using System.Collections.Generic;
using System.Data.SqlClient;
using SmallInt = System.Int16;
using Int = System.Int32;
using BigInt = System.Int64;
using TinyInt = System.Byte;

namespace Bot_Application.Database
{
    //makes all third party databases look like one
    static class PublicDataAdapter
    {
        public static void UpdateRestaurantsAndRelation(SqlConnection YelpDataConnection, SqlConnection PublicDataConnection)
        {
            YelpData.Restaurants.Record[] restaurants = YelpData.Get_All_Restaurants(YelpDataConnection).Result;
            List<Int> newResIDs = new List<Int>(restaurants.Length);
            YelpData.Categories.Record[] cates = YelpData.Get_All_Categories(YelpDataConnection).Result;
            List<Int> newCateIDs = new List<Int>(cates.Length);
            YelpData.Reviews.Record[] reviews = YelpData.Get_All_Reviews(YelpDataConnection).Result;
            List<BigInt> newRevIDs = new List<BigInt>(reviews.Length);
            YelpData.Transactions.Record[] transactions = YelpData.Get_All_Transactions(YelpDataConnection).Result;
            List<TinyInt> newTranIDs = new List<TinyInt>(transactions.Length);

            foreach (var restaurant in restaurants)
            {
                PublicData.Res.Record res = new PublicData.Res.Record(restaurant.name)
                {
                    latitude = restaurant.latitude,
                    longitude = restaurant.longitude,
                    zipcode = restaurant.zip_code,
                    currencyID = 840,
                    image = restaurant.image_url,
                    homepage = restaurant.url,
                    DataProviderID = -32768,
                    providerPage = restaurant.url,
                    customID = restaurant.Yelp_ID
                };
                if (restaurant.price != null && restaurant.price.Trim() != "")
                    res.priceLevel = (TinyInt)(restaurant.price.Trim().Length * 25);
                if (restaurant.address1 != null)
                {
                    res.address = restaurant.address1;
                    if (restaurant.address2 != null)
                    {
                        res.address += ", " + restaurant.address2;
                        if (restaurant.address3 != null)
                            res.address += ", " + restaurant.address3;
                    }
                }
                if (restaurant.phone != null)
                {
                    res.phone = restaurant.phone;
                    if (restaurant.display_phone != null)
                        res.phone += " or " + restaurant.display_phone;
                }
                else if (restaurant.display_phone != null)
                    res.phone = restaurant.display_phone;
                Int newID = PublicData.NewResID(PublicDataConnection);
                PublicData.Add_Res(PublicDataConnection, res, newID).Wait();
                newResIDs.Add(newID);
            }
            foreach (var cate in cates)
            {
                PublicData.CateOther.Record other = new PublicData.CateOther.Record(cate.title);
                Int newID = PublicData.NewCateOtherID(PublicDataConnection);
                PublicData.Add_CateOther(PublicDataConnection, other, newID).Wait();
                newCateIDs.Add(newID);
            }
            for (int resIndex = 0; resIndex < restaurants.Length; ++resIndex)
            {
                SmallInt[] catesID = YelpData.Get_Restaurant_Categories(YelpDataConnection, restaurants[resIndex].ID).Result;
                foreach (var cateID in catesID)
                {
                    int catesIndex = 0;
                    bool found = false;
                    //find
                    for (; catesIndex < cates.Length; ++catesIndex)
                    {
                        if (cates[catesIndex].ID == cateID)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        PublicData.Add_Res_CateOther(PublicDataConnection, newResIDs[resIndex], newCateIDs[catesIndex]).Wait();
                }
            }
            cates = null;
            newCateIDs = null;
            foreach (var rev in reviews)
            {
                PublicData.Rating.Record rating = new PublicData.Rating.Record(rev.rating.Value)
                {
                    userName = rev.user_name,
                    comment = rev.text,
                    DataProviderID = -32768,
                };
                BigInt newID = PublicData.NewRatingID(PublicDataConnection);
                PublicData.Add_Rating(PublicDataConnection, rating, newID);
                newRevIDs.Add(newID);
            }
            for (int resIndex = 0; resIndex < restaurants.Length; ++resIndex)
            {
                BigInt[] oldRevIDs = YelpData.Get_Restaurant_Reviews(YelpDataConnection, restaurants[resIndex].ID).Result;
                foreach (var oldRevID in oldRevIDs)
                {
                    int oldRevIDsIndex = 0;
                    bool found = false;
                    for (; oldRevIDsIndex < reviews.Length; ++oldRevIDsIndex)
                    {
                        if (reviews[oldRevIDsIndex].ID == oldRevID)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        PublicData.Add_Res_Rating(PublicDataConnection, newResIDs[resIndex], newRevIDs[oldRevIDsIndex]).Wait();
                }
            }
            reviews = null;
            newRevIDs = null;
            foreach (var tran in transactions)
            {
                PublicData.Transaction.Record newTran = new PublicData.Transaction.Record(tran.type);
                TinyInt newID = PublicData.NewTransactionID(PublicDataConnection);
                PublicData.Add_Transaction(PublicDataConnection, newTran, newID).Wait();
                newTranIDs.Add(newID);
            }
            for (int resIndex = 0; resIndex < restaurants.Length; ++resIndex)
            {
                SmallInt[] oldTrans = YelpData.Get_Restaurant_Transactions(YelpDataConnection, restaurants[resIndex].ID).Result;
                foreach (var oldTran in oldTrans)
                {
                    int transIndex = 0;
                    bool found = false;
                    //find
                    for (; transIndex < transactions.Length; ++transIndex)
                    {
                        if (transactions[transIndex].ID == oldTran)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        PublicData.Add_Res_Transaction(PublicDataConnection, newResIDs[resIndex], newTranIDs[transIndex], -32768).Wait();
                }
            }

        }
    }
}
