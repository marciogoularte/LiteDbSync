using CommonTools.Lib.ns11.SignalRHubServers;
using System.Collections.Generic;

namespace LiteDbSync.Common.API.Configuration
{
    public class CatchUpWriterSettings : ISignalRServerSettings
    {
        public string ServerURL { get; set; }

        public List<DbTargetSettings>  Targets  { get; set; }


        public static CatchUpWriterSettings CreateDefault()
        {
            return new CatchUpWriterSettings
            {
                ServerURL = "http://localhost:1234",
                Targets   = new List<DbTargetSettings>
                {
                    DbTargetSettings.CreateDefault(),
                    DbTargetSettings.CreateDefault(),
                },
            };
        }
    }
}
