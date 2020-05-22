using System;
using Newtonsoft.Json;

namespace DareTechnicalTest.Data.Dtos
{
    public class YoutubePlaylistItemSnippetDto
    {
        [JsonProperty("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("resourceId")]
        public YoutubePlaylistItemSnippetResourceId ResourceId { get; set; }

        [JsonProperty("thumbnails")]
        public YoutubePlaylistItemSnippetThumbnailsDto Thumbnails { get; set; }
    }
}