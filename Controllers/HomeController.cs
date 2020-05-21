using System.Web.Mvc;
using DareTechnicalTest.Services.PageServices.Interfaces;
using Umbraco.Web.Models;

namespace DareTechnicalTest.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHomePageService _homePageService;

        public HomeController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        public ActionResult Index(ContentModel model, string playlist)
        {
            var viewModel = _homePageService.GetViewModel(model.Content, playlist);
            return base.BaseIndex(viewModel);
        }
    }
}