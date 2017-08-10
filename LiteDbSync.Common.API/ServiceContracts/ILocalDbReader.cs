using System.Collections.Generic;

namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface ILocalDbReader
    {
        long         GetLatestId (string dbFilepath, string collectionName);
        List<string> GetRecords  (string dbFilepath, string collectionName, long startId, long endId);
    }
}
