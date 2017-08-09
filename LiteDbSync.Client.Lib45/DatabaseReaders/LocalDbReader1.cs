using LiteDB;
using LiteDbSync.Common.API.ServiceContracts;

namespace LiteDbSync.Client.Lib45.DatabaseReaders
{
    public class LocalDbReader1 : ILocalDbReader
    {


        public long GetLatestId(string dbFilepath, string collectionName)
        {
            using (var db = CreateConnection(dbFilepath))
            {
                return db.GetCollection(collectionName).Max();
            }
        }


        private LiteDatabase CreateConnection(string filepath)
        {
            var connStr = $"Filename={filepath};Mode=ReadOnly;";
            return new LiteDatabase(connStr);
        }
    }
}
