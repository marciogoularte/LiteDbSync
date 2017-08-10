using CommonTools.Lib.ns11.SignalRHubServers;

namespace LiteDbSync.Common.API.Configuration
{
    public class CatchUpWriterSettings : ISignalRServerSettings
    {
        public string ServerURL { get; set; }


        public static CatchUpWriterSettings CreateDefault()
        {
            return new CatchUpWriterSettings
            {
                ServerURL = "ServerURL goes here"
            };
        }
    }
}
