using Newtonsoft.Json;

namespace DareTechnicalTest.Data.Dtos
{
    public class YoutubePlaylistItemSnippetThumbnailsDefaultDto
    {
        [JsonProperty("url")]
        public string DefaultUrl { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}