using System.Collections.Generic;

namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface ILocalDbWriter : ILocalDbReader
    {
        void Insert(string dbFilepath, string collectionName, List<string> records);
    }
}
