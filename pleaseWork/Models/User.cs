using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pleaseWork.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "username")]
        public string username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string password { get; set; }

        [JsonProperty(PropertyName = "farmer")]
        public bool farmer { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string email { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string image { get; set; }

        [JsonProperty(PropertyName = "cart")]
        public List<Product> cart { get; set; }

       /* [JsonProperty(PropertyName = "loggedIn")]
        public bool loggedIn { get; set; }*/
    }
}
