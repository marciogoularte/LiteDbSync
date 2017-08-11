using CommonTools.Lib.ns11.SignalRHubServers;
using System.Collections.Generic;

namespace LiteDbSync.Common.API.Configuration
{
    public class ChangeSenderSettings : IHubClientSettings
    {
        public string   ServerURL  { get; set; }
        public string   HubName    { get; set; }

        public List<DbWatcherSettings>  WatchList  { get; set; }

        public static ChangeSenderSettings CreateDefault()
        {
            return new ChangeSenderSettings
            {
                ServerURL = "http://server.url.go:1234",
                HubName   = "HubNameHere",
                WatchList = new List<DbWatcherSettings>
                {
                    DbWatcherSettings.CreateDefault(),
                    DbWatcherSettings.CreateDefault(),
                }
            };
        }
    }
}
