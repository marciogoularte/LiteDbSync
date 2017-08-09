namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface ILocalDbReader
    {
        long GetLatestId(string dbFilepath, string collectionName);
    }
}
