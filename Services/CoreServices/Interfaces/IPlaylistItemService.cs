using System.Collections.Generic;
using DareTechnicalTest.Models.Media.Video;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;

namespace DareTechnicalTest.Services.CoreServices.Interfaces
{
    public interface IPlaylistItemService
    {
        IList<PlaylistItem> GetPublishedPlaylistItems(IPublishedContent pageContent, string playlist);

        IList<PlaylistItem> GetAndSavePlaylistItems(string playlist, IContentService contentService);
    }
}
