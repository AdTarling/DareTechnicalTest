using System.Collections.Generic;
using Newtonsoft.Json;

namespace DareTechnicalTest.Data.Dtos
{
    public class YoutubePlaylistDto
    {
        [JsonProperty("etag")]
        public string ETag { get; set; }

        [JsonProperty("items")]
        public IList<YoutubePlaylistItemDto> PlaylistItems { get; set; }
    }
}