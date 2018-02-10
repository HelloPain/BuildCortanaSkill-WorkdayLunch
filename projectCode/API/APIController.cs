using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Application.API
{
    public class APIController
    {
        public readonly string HttpHead;
        readonly protected string APIKey;
        public const string ResieveDataType = "json";
        public APIController(string httpHead,string key)
        {
            HttpHead = httpHead;APIKey = key;
        }
    }
    //public class GoogleMapAPIController : APIController
    //{
    //    readonly static string[] RequestType = {
    //        "nearbysearch/",
    //        "textsearch/",
    //        "radarsearch/"};
    //    public async Task<Json> GetNearbySearchDateAsync(Location userLocation, string keyword)
    //    {
    //        StringBuilder requst = new StringBuilder(HttpRequestHead);
    //        requst.Append(RequestType[0]); requst.Append("json?");
    //        requst.Append("key="); requst.Append(APIKey); requst.Append('&');
    //        requst.Append("location="); requst.Append(userLocation.ToString());
    //        if (keyword != "")
    //        {
    //            requst.Append('&'); requst.Append("keyword="); requst.Append(keyword);
    //        }

    //    }
    //}
    //public class BingMapAPIController : APIController
    //{
    //    readonly static string[] RequestType = {
    //        "nearby/",
    //        "bbox/",//bounding box
    //        "radarsearch/"};
    //    public async Task<Json> GetAreaSearchDateAsync(Location userLocation, string keyword)
    //    {
    //        StringBuilder requst = new StringBuilder(HttpRequestHead);
    //        requst.Append(RequestType[0]); requst.Append("json?");
    //        requst.Append("key="); requst.Append(APIKey); requst.Append('&');
    //        requst.Append("location="); requst.Append(userLocation.ToString());
    //        if (keyword != "")
    //        {
    //            requst.Append('&'); requst.Append("keyword="); requst.Append(keyword);
    //        }

    //    }
    //}
    public class BaiduMapAPIController:APIController
    {
        public readonly string[] searchTypes = { "search?", "detial?" };
        private StringBuilder requstString;
        public BaiduMapAPIController(string httpHead,string APIKey,int searchType) : base(httpHead, APIKey)
        {
            requstString = new StringBuilder(base.HttpHead);
            requstString.Append(searchTypes[searchType]);
        }
        public async Task<Json> GetSearchDateAsync(Location userLocation, string keywords)
        {
            
        }
    }
    public class Location
    {
        public double latitude;
        public double longitude;
        public override string ToString()
        {
            return latitude.ToString() + ',' + longitude.ToString();
        }
    }
    public class Json
    {

    }
}