using System.Configuration;
using DareTechnicalTest.Constants;
using DareTechnicalTest.Models.ViewModels;
using DareTechnicalTest.Services.CoreServices.Interfaces;
using DareTechnicalTest.Services.PageServices.Interfaces;
using StackExchange.Profiling.Internal;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace DareTechnicalTest.Services.PageServices
{
    public class HomePageService : PageBaseService, IHomePageService
    {
        private readonly IPlaylistItemService _playlistItemService;

        public HomePageService(IPlaylistItemService playlistItemService)
        {
            _playlistItemService = playlistItemService;
        }

        public HomePageViewModel GetViewModel(IPublishedContent pageContent, string playlist)
        {
            var viewModel = new HomePageViewModel();

            if (pageContent == null)
            {
                return viewModel;
            }

            PopulateBaseProperties(viewModel, pageContent);

            viewModel.Header = pageContent.Value<string>(PropertyAliases.Home.Header);
            viewModel.PageDescription = pageContent.Value<string>(PropertyAliases.Home.PageDescription);
            viewModel.PlaylistRefreshButtonText = pageContent.Value<string>(PropertyAliases.Home.PlaylistRefreshButtonText);

            if (playlist.IsNullOrWhiteSpace())
            {
                playlist = ConfigurationManager.AppSettings[AppSettings.DefaultPlaylist];
            }

            viewModel.Playlist = playlist;
            return viewModel;
        }
    }
}