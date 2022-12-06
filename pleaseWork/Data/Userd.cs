using System;
using Newtonsoft.Json;
namespace pleaseWork.Data
{
	public class Userd
	{
		
	
            [JsonProperty(PropertyName = "UsernameTextValue")]
            public string UsernameTextValue { get; set; }

            [JsonProperty(PropertyName = "PasswordTextValue")]
            public string PasswordTextValue{ get; set; }

            [JsonProperty(PropertyName = "SelectedOption")]
            public string SelectedOption { get; set; }
           
            [JsonProperty(PropertyName = "NameTextValue")]
            public string NameTextValue { get; set; }

            [JsonProperty(PropertyName = "EmailTextValue")]
            public string EmailTextValue { get; set; }
    
	}
}

