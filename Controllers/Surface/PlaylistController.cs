using System.Web.Mvc;
using DareTechnicalTest.Services.CoreServices.Interfaces;
using Umbraco.Web.Mvc;

namespace DareTechnicalTest.Controllers.Surface
{
    public class PlaylistController : SurfaceController
    {
        private readonly IPlaylistItemService _playlistItemService;

        public PlaylistController(IPlaylistItemService playlistItemService)
        {
            _playlistItemService = playlistItemService;
        }

        [System.Web.Http.HttpGet]
        public ActionResult SaveAndReturnPlaylistItems(string playlist)
        {
            var playListItems = _playlistItemService.GetAndSavePlaylistItems(playlist);

            return PartialView("_playlistItems", playListItems);
        }
    }
}