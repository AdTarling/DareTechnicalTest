using DareTechnicalTest.Models.ViewModels;
using DareTechnicalTest.Services.PageServices.Interfaces;
using Umbraco.Core.Models.PublishedContent;

namespace DareTechnicalTest.Services.PageServices
{
    public class PageBaseService : IPageBaseService
    {
        public void PopulateBaseProperties(PageBaseViewModel viewModel, IPublishedContent pageContent)
        {
            if (viewModel == null || pageContent == null)
            {
                return;
            }

            // shared seo properties can go here
        }
    }
}