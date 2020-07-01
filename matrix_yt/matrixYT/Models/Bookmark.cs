using System.Text.Json;
using System.Text.Json.Serialization;


namespace matrixYT.Models

{
    public class Bookmark
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("avatar")]        
        public string Avatar { get; set; }
    }
}