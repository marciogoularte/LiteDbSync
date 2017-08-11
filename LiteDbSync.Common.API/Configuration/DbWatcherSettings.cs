namespace LiteDbSync.Common.API.Configuration
{
    public class DbWatcherSettings
    {
        public string  DbFilePath      { get; set; }
        public string  UniqueDbName    { get; set; }
        public string  CollectionName  { get; set; }
        public uint    IntervalMS      { get; set; }


        public static DbWatcherSettings CreateDefault()
        {
            return new DbWatcherSettings
            {
                DbFilePath     = "DbFilePath goes here",
                UniqueDbName   = "distinct from others in the list",
                CollectionName = "CollectionName goes here",
                IntervalMS     = 1000
            };
        }
    }
}
