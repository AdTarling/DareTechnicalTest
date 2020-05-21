using DareTechnicalTest.Models.ViewModels;
using Umbraco.Core.Models.PublishedContent;

namespace DareTechnicalTest.Services.PageServices.Interfaces
{
    public interface IHomePageService
    {
        HomePageViewModel GetViewModel(IPublishedContent pageContent, string playlist);
    }
}
