using System;
using System.Globalization;
using System.Collections.Generic;

namespace UserInfo
{
    enum IngredientType { }//error, should be at Catering

    [Serializable]
    class BasicProfile
    {
        public string Name { get; set; }
        public UInt16 Age { get; set; }
        public UInt16 Weight_KG { get; set; }
        public string DeliveryAddress { get; set; }
        public List<IngredientType> Allergies { get; set; }//ingredients never eat
    }

    [Serializable]
    class Preference
    {

    }

    class HealthProfile
    {
        
    }

    enum OrderState { }
    [Serializable]
    class Order
    {
        public OrderState State;
        public DateTime MakeOrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderCompleteTime { get; set; }
        public Comment AlgorithmComment { get; set; }
        public Comment DishComment { get; set; }
    }


    class Comment
    {
        string CommentWords;
    }
}