namespace CET37Market.Common.Models
{
    using Newtonsoft.Json;
    using System;

    public class Product
    {
        [JsonProperty("id")]
        public long Id { get; set; }



        [JsonProperty("name")]
        public string Name { get; set; }



        [JsonProperty("price")]
        public double Price { get; set; }



        [JsonProperty("imageURL")]
        public string ImageUrl { get; set; }


        [JsonProperty("lastPurchase")]
        public object LastPurchase { get; set; }



        [JsonProperty("lastSale")]
        public object LastSale { get; set; }



        [JsonProperty("isAvailable")]
        public bool IsAvailable { get; set; }



        [JsonProperty("stock")]
        public long Stock { get; set; }



        [JsonProperty("user")]
        public User User { get; set; }



        [JsonProperty("imageFullPath")]
        public Uri ImageFullPath { get; set; }


        public string ListProducts
        {
            get
            {
                return $"{this.Name} {this.Price.ToString():C2}";
            }
        }
    }
}
