using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DareTechnicalTest.Constants;
using DareTechnicalTest.Data.Clients.Interfaces;
using DareTechnicalTest.Data.Dtos;
using DareTechnicalTest.Models;
using DareTechnicalTest.Models.Media.Video;
using DareTechnicalTest.Services.CoreServices.Interfaces;
using StackExchange.Profiling.Internal;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace DareTechnicalTest.Services.CoreServices
{
    public class PlaylistItemService : IPlaylistItemService
    {
        private readonly IYoutubeApiClient _youtubeApiClient;
        private readonly IUmbracoContextFactory _context;

        public PlaylistItemService(IYoutubeApiClient youtubeApiClient, IUmbracoContextFactory context)
        {
            _youtubeApiClient = youtubeApiClient;
            _context = context;
        }

        public IList<PlaylistItem> GetPublishedPlaylistItems(IPublishedContent pageContent, string playlist)
        {
            var playlistItems = new List<PlaylistItem>();

            if (pageContent == null || playlist.IsNullOrWhiteSpace())
            {
                return playlistItems;
            }

            var playlistContentInformation = GetPlaylistContentInformation(playlist);

            if (playlistContentInformation.PlaylistItemsContent == null ||
                !playlistContentInformation.PlaylistItemsContent.Any())
            {
                return playlistItems;
            }

            var youtubeVideoUrlBase = ConfigurationManager.AppSettings[AppSettings.YoutubeVideoUrlBase];
            foreach (var playlistItemContent in playlistContentInformation.PlaylistItemsContent)
            {
                playlistItems.Add(new PlaylistItem
                {
                    PlaylistItemTitle =
                        playlistItemContent.Value<string>(PropertyAliases.PlaylistItem.PlaylistItemTitle),
                    PlaylistItemVideoUrl =
                        playlistItemContent.Value<string>(PropertyAliases.PlaylistItem.PlaylistItemVideoUrl),
                    PlaylistItemThumbnailUrl =
                        playlistItemContent.Value<string>(PropertyAliases.PlaylistItem.PlaylistItemThumbnailUrl),
                    PlaylistUploadDate =
                        playlistItemContent.Value<DateTime>(PropertyAliases.PlaylistItem.PlaylistUploadDate)
                });
            }

            return playlistItems.OrderByDescending(p => p.PlaylistUploadDate).ToList();
        }

        public IList<PlaylistItem> GetAndSavePlaylistItems(string playlist, IContentService contentService)
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
            }
            else if (playlist.Equals(guitarSolosPlaylistName, StringComparison.OrdinalIgnoreCase))
            {
                playlistId = guitarSolosPlaylistId;
            }

            var youtubePlaylistDto = _youtubeApiClient.GetPlaylist(playlistId);
            if (youtubePlaylistDto?.PlaylistItems == null || !youtubePlaylistDto.PlaylistItems.Any())
            {
                return playlistItems;
            }

            CreateYoutubeItemContentFromDto(
                youtubePlaylistDto.PlaylistItems.OrderByDescending(pi => pi.Snippet.PublishedAt).ToList(),
                playlistItems);

            SavePlaylistItems(playlistItems, playlist, contentService);

            return playlistItems;
        }

        private void SavePlaylistItems(IList<PlaylistItem> playlistContentItems, string playlist,
            IContentService contentService)
        {
            var playlistContentInformation = GetPlaylistContentInformation(playlist);
            if (playlistContentInformation.PlaylistItemsContent == null
                || !playlistContentInformation.PlaylistItemsContent.Any()
                || playlistContentInformation.PlaylistId <= default(int))
            {
                return;
            }

            foreach (var currentPlaylistItem in playlistContentInformation.PlaylistItemsContent)
            {
                var currentPlaylistItemContent = contentService.GetById(currentPlaylistItem.Id);
                contentService.Delete(currentPlaylistItemContent);
            }

            foreach (var playlistContentItem in playlistContentItems)
            {
                var content = contentService.Create(playlistContentItem.PlaylistItemTitle,
                    playlistContentInformation.PlaylistId,
                    DocumentTypes.PlaylistItem);
                content.SetValue(PropertyAliases.PlaylistItem.PlaylistItemTitle,
                    playlistContentItem.PlaylistItemTitle);
                content.SetValue(PropertyAliases.PlaylistItem.PlaylistItemThumbnailUrl,
                    playlistContentItem.PlaylistItemThumbnailUrl);
                content.SetValue(PropertyAliases.PlaylistItem.PlaylistItemVideoUrl,
                    playlistContentItem.PlaylistItemVideoUrl);
                content.SetValue(PropertyAliases.PlaylistItem.PlaylistUploadDate,
                    playlistContentItem.PlaylistUploadDate);
                contentService.SaveAndPublish(content);
            }
        }

        private PlaylistInformation GetPlaylistContentInformation(string playlist)
        {
            var playlistInformation = new PlaylistInformation();

            using (var cref = _context.EnsureUmbracoContext())
            {
                var cache = cref.UmbracoContext.Content;
                var globalContentRoot =
                    cache.GetAtRoot().FirstOrDefault(r => r.IsDocumentType(DocumentTypes.GlobalContent));

                var videoRoot = globalContentRoot?.Children.FirstOrDefault(c => c.IsDocumentType(DocumentTypes.Video));

                var targetPlaylist =
                    videoRoot?.Children.FirstOrDefault(v =>
                        v.IsDocumentType(DocumentTypes.Playlist) && v.Name.Equals(playlist));

                playlistInformation.PlaylistItemsContent =
                    targetPlaylist?.Children.Where(c => c.IsDocumentType(DocumentTypes.PlaylistItem));
                playlistInformation.PlaylistId = targetPlaylist?.Id ?? 0;

                return playlistInformation;
            }
        }

        private void CreateYoutubeItemContentFromDto(IList<YoutubePlaylistItemDto> playlistDtoItems,
            IList<PlaylistItem> playListContentItems)
        {
            if (playlistDtoItems == null || !playlistDtoItems.Any() || playListContentItems == null)
            {
                return;
            }

            var youtubeVideoUrlBase = ConfigurationManager.AppSettings[AppSettings.YoutubeVideoUrlBase];

            foreach (var playlistDtoItem in playlistDtoItems)
            {
                playListContentItems.Add(new PlaylistItem
                {
                    PlaylistItemTitle = playlistDtoItem.Snippet.Title,
                    PlaylistItemVideoUrl = $"{youtubeVideoUrlBase}{playlistDtoItem.Snippet.ResourceId.VideoId}",
                    PlaylistItemThumbnailUrl = playlistDtoItem.Snippet.Thumbnails.Default.DefaultUrl,
                    PlaylistUploadDate = playlistDtoItem.Snippet.PublishedAt
                });
            }
        }
    }
}