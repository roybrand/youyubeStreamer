using System.Text.Json;
using System.Text.Json.Serialization;

namespace matrixYT.Models
{
    public class Repo
    {
        public Item[] items { get; set; }
    }

    public class Item
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("owner")]
        
        public Owner Owner { get; set; }

         
    }   

    public class Owner
    {
       [JsonPropertyName("avatar_url")]
        public string Avatar_url { get; set; }  
    }
}