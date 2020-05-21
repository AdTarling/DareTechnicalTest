using DareTechnicalTest.Models.ViewModels;
using Umbraco.Core.Models.PublishedContent;

namespace DareTechnicalTest.Services.PageServices.Interfaces
{
    public interface IPageBaseService
    {
        void PopulateBaseProperties(PageBaseViewModel viewModel, IPublishedContent pageContent);
    }
}
