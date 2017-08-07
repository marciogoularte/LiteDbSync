namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface IChangeEventThrottler
    {
        void StartWatching(string ldbFilepath, int minIntervalMS);
    }
}
