using System;

namespace DareTechnicalTest.Models.Media.Video
{
    public class PlaylistItem
    {
        public string PlaylistItemTitle { get; set; }
        public string PlaylistItemThumbnailUrl { get; set; }
        public string PlaylistItemVideoUrl { get; set; }
        public DateTime PlaylistUploadDate { get; set; }
    }
}