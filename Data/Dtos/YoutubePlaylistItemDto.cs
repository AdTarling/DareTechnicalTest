using Newtonsoft.Json;

namespace DareTechnicalTest.Data.Dtos
{
    public class YoutubePlaylistItemDto
    {
        [JsonProperty("snippet")]
        public YoutubePlaylistItemSnippetDto Snippet { get; set; }
    }
}