using DareTechnicalTest.Data.Dtos;

namespace DareTechnicalTest.Data.Clients.Interfaces
{
    public interface IYoutubeApiClient
    {
        YoutubePlaylistDto GetPlaylist(string playlistId);
    }
}
