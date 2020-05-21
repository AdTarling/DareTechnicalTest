using System.Collections.Generic;
using DareTechnicalTest.Models.Media.Video;

namespace DareTechnicalTest.Models.ViewModels
{
    public class HomePageViewModel : PageBaseViewModel
    {
        public string Header { get; set; }
        public string PageDescription { get; set; }
        public string PlaylistRefreshButtonText { get; set; }
        public string Playlist { get; set; }
        public IList<PlaylistItem> PlaylistVideos { get; set; }
    }
}