using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace DareTechnicalTest.Models
{
    public class PlaylistInformation
    {
        public int PlaylistId { get; set; }
        public IEnumerable<IPublishedContent> PlaylistItemsContent { get; set; }
    }
}