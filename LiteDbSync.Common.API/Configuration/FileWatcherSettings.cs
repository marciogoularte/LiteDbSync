namespace LiteDbSync.Common.API.Configuration
{
    public class FileWatcherSettings
    {
        public string  DbFilePath      { get; set; }
        public string  CollectionName  { get; set; }
        public uint    IntervalMS      { get; set; }
    }
}
