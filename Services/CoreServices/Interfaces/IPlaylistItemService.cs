using System.Collections.Generic;
using DareTechnicalTest.Models.Media.Video;
using Umbraco.Core.Models.PublishedContent;

namespace DareTechnicalTest.Services.CoreServices.Interfaces
{
    public interface IPlaylistItemService
    {
        IList<PlaylistItem> GetAndSavePlaylistItems(string playlist);
        IList<PlaylistItem> GetPlaylistItemsFromContent(IPublishedContent content, string playlist);
    }
}
