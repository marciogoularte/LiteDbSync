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
                ServerURL = "http://localhost:1234",
                HubName   = "ChangeReceiverHub",
                WatchList = new List<DbWatcherSettings>
                {
                    DbWatcherSettings.CreateDefault(),
                    DbWatcherSettings.CreateDefault(),
                }
            };
        }
    }
}
