using Newtonsoft.Json;

namespace DareTechnicalTest.Data.Dtos
{
    public class YoutubePlaylistItemSnippetResourceId
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("videoId")]
        public string VideoId { get; set; }
    }
}