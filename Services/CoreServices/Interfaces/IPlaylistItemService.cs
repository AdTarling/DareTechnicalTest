using System.Collections.Generic;
using DareTechnicalTest.Models.Media.Video;
using Umbraco.Core.Services;

namespace DareTechnicalTest.Services.CoreServices.Interfaces
{
    public interface IPlaylistItemService
    {
        IList<PlaylistItem> GetAndSavePlaylistItems(string playlist, IContentService contentService);
    }
}
