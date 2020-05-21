using System.Web.Mvc;
using DareTechnicalTest.Models.ViewModels;
using Umbraco.Web.Mvc;

namespace DareTechnicalTest.Controllers
{
    public class BaseController : RenderMvcController
    {
        public ActionResult BaseIndex(PageBaseViewModel viewModel)
        {
            return CurrentTemplate(viewModel);
        }
    }
}