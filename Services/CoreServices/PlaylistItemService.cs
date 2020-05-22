using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DareTechnicalTest.Constants;
using DareTechnicalTest.Data.Clients.Interfaces;
using DareTechnicalTest.Models.Media.Video;
using DareTechnicalTest.Services.CoreServices.Interfaces;
using StackExchange.Profiling.Internal;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace DareTechnicalTest.Services.CoreServices
{
    public class PlaylistItemService : IPlaylistItemService
    {
        private readonly IYoutubeApiClient _youtubeApiClient;

        public PlaylistItemService(IYoutubeApiClient youtubeApiClient)
        {
            _youtubeApiClient = youtubeApiClient;
        }

        public IList<PlaylistItem> GetPlaylistItemsFromContent(IPublishedContent content, string playlist)
        {
            var playlistItems = new List<PlaylistItem>();

            if (content == null || playlist.IsNullOrWhiteSpace())
            {
                return playlistItems;
            }

            var root = content.Root();

            var videoRoot = root.Children.FirstOrDefault(c => c.IsDocumentType(DocumentTypes.Video));

            if (videoRoot == null)
            {
                return playlistItems;
            }

            var targetPlaylist =
                videoRoot.Children.FirstOrDefault(v =>
                    v.IsDocumentType(DocumentTypes.Video) && v.Name.Equals(playlist));

            if (targetPlaylist == null)
            {
                return playlistItems;
            }

            var playlistContent = targetPlaylist.Children.Where(p => p.IsDocumentType(DocumentTypes.PlaylistItem));

            if (!playlistContent.Any())
            {
                return playlistItems;
            }

            foreach (var playlistContentItem in playlistContent)
            {
                var playlistItem = new PlaylistItem()
                {
                    PlaylistItemTitle =
                        playlistContentItem.Value<string>(PropertyAliases.PlaylistItem.PlaylistItemTitle)
                };
            }

            return playlistItems;
        }

        public IList<PlaylistItem> GetAndSavePlaylistItems(string playlist)
        {
            var playlistItems = new List<PlaylistItem>();

            if (playlist.IsNullOrWhiteSpace())
            {
                return playlistItems;
            }

            var playlistId = string.Empty;

            var bbcNewsPlaylistName = ConfigurationManager.AppSettings[AppSettings.BbcNewsPlaylistName];
            var guitarSolosPlaylistName = ConfigurationManager.AppSettings[AppSettings.GuitarSolosPlaylistName];
            var bbcNewsPlaylistId = ConfigurationManager.AppSettings[AppSettings.BbcNewsPlaylistId];
            var guitarSolosPlaylistId = ConfigurationManager.AppSettings[AppSettings.GuitarSolosPlaylistId];

            if (playlist.Equals(bbcNewsPlaylistName, StringComparison.OrdinalIgnoreCase))
            {
                playlistId = bbcNewsPlaylistId;
            }else if (playlist.Equals(guitarSolosPlaylistName, StringComparison.OrdinalIgnoreCase))
            {
                playlistId = guitarSolosPlaylistId;
            }

            var youtubePlaylistDto = _youtubeApiClient.GetPlaylist(playlistId);

            // create umbraco items and save
            // either retrieve from umbraco or return for partial render

            return playlistItems;
        }
    }
}