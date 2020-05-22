using DareTechnicalTest.Data.Clients;
using DareTechnicalTest.Data.Clients.Interfaces;
using DareTechnicalTest.Services.CoreServices;
using DareTechnicalTest.Services.CoreServices.Interfaces;
using DareTechnicalTest.Services.PageServices;
using DareTechnicalTest.Services.PageServices.Interfaces;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace DareTechnicalTest.Composers
{
    public class DependencyComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IPageBaseService, PageBaseService>();
            composition.Register<IHomePageService, HomePageService>();
            composition.Register<IPlaylistItemService, PlaylistItemService>();
            composition.Register<IYoutubeApiClient, YoutubeApiClient>();
        }
    }
}