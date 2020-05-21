using System.Collections.Generic;
using DareTechnicalTest.Models.Media.Video;
using Umbraco.Core.Models.PublishedContent;

namespace DareTechnicalTest.Services.CoreServices.Interfaces
{
    public interface IPlaylistItemService
    {
        IList<PlaylistItem> GetPlaylistItemsFromContent(IPublishedContent content, string playlist);
    }
}
