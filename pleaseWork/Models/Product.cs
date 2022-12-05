using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pleaseWork.Models
{
    public class Product
    {

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "price")]
        public Double price { get; set; }

        [JsonProperty(PropertyName = "seller")]
        public string seller { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string image { get; set; }
    }
}