using System.Web.Mvc;
using DareTechnicalTest.Services.CoreServices.Interfaces;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;

namespace DareTechnicalTest.Controllers.Surface
{
    public class PlaylistController : SurfaceController
    {
        private readonly IPlaylistItemService _playlistItemService;
        private readonly IContentService _contentService;

        public PlaylistController(IPlaylistItemService playlistItemService)
        {
            _playlistItemService = playlistItemService;
            _contentService = Services.ContentService;
        }

        [System.Web.Http.HttpGet]
        public ActionResult SaveAndReturnPlaylistItems(string playlist)
        {
            var playListItems = _playlistItemService.GetAndSavePlaylistItems(playlist, _contentService);

            return PartialView("_playlistItems", playListItems);
        }
    }
}