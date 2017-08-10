using System.Collections.Generic;

namespace LiteDbSync.Common.API.Configuration
{
    public class ChangeSenderSettings
    {
        public List<FileWatcherSettings>  WatchList  { get; set; }

        public static ChangeSenderSettings CreateDefault()
        {
            return new ChangeSenderSettings
            {
                WatchList = new List<FileWatcherSettings>
                {
                    new FileWatcherSettings
                    {
                        DbFilePath     = "DbFilePath goes here",
                        CollectionName = "CollectionName goes here",
                        IntervalMS     = 1000
                    },
                    new FileWatcherSettings
                    {
                        DbFilePath     = "DbFilePath goes here",
                        CollectionName = "CollectionName goes here",
                        IntervalMS     = 1000
                    },
                }
            };
        }
    }
}
