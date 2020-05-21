using System.Collections.Generic;
using System.Linq;
using DareTechnicalTest.Constants;
using DareTechnicalTest.Models.Media.Video;
using DareTechnicalTest.Services.CoreServices.Interfaces;
using StackExchange.Profiling.Internal;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace DareTechnicalTest.Services.CoreServices
{
    public class PlaylistItemService : IPlaylistItemService
    {
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
    }
}