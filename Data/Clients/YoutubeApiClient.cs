using System;
using System.Configuration;
using System.Net;
using DareTechnicalTest.Constants;
using DareTechnicalTest.Data.Clients.Interfaces;
using DareTechnicalTest.Data.Dtos;
using Newtonsoft.Json;

namespace DareTechnicalTest.Data.Clients
{
    public class YoutubeApiClient : IYoutubeApiClient
    {
        public YoutubePlaylistDto GetPlaylist(string playlistId)
        {
            var youtubePlaylistDto = new YoutubePlaylistDto();

            using (var webClient = new WebClient())
            {
                try
                {
                    var youtubeApiBase = ConfigurationManager.AppSettings[AppSettings.YoutubeApiBaseUrl];
                    var youtubePlaylistEndpoint =
                        ConfigurationManager.AppSettings[AppSettings.YoutubePlaylistsEndpoint];
                    var youtubeApiKey = ConfigurationManager.AppSettings[AppSettings.YoutubeApiKey];

                    var youtubePlaylistsEndpointWithParameters =
                        string.Format(youtubePlaylistEndpoint, playlistId, youtubeApiKey);
                    var youtubePlaylistsFullApiEndpoint = $"{youtubeApiBase}{youtubePlaylistsEndpointWithParameters}";

                    var response = webClient.DownloadString(youtubePlaylistsFullApiEndpoint);

                    youtubePlaylistDto = JsonConvert.DeserializeObject<YoutubePlaylistDto>(response);
                }
                catch (Exception e)
                {
                    // log exception here
                }

            }

            return youtubePlaylistDto;
        }
    }
}