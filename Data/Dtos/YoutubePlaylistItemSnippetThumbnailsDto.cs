using Newtonsoft.Json;

namespace DareTechnicalTest.Data.Dtos
{
    public class YoutubePlaylistItemSnippetThumbnailsDto
    {
        [JsonProperty("default")]
        public YoutubePlaylistItemSnippetThumbnailsDefaultDto Default { get; set; }
    }
}