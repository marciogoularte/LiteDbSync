using LiteDB;
using LiteDbSync.Common.API.ServiceContracts;
using System.Collections.Generic;

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


        public List<string> GetRecords(string dbFilepath, 
            string collectionName, long startId, long endId)
        {
            var list = new List<string>();
            using (var db = CreateConnection(dbFilepath))
            {
                var coll    = db.GetCollection(collectionName);
                var matches = coll.Find(Query.Between("_id", startId, endId));

                foreach (var bson in matches)
                    list.Add(Serialize(bson));
            }
            return list;
        }


        private string Serialize(BsonDocument bson)
        {
            return JsonSerializer.Serialize(bson, false, false);
        }


        private LiteDatabase CreateConnection(string filepath)
        {
            var connStr = $"Filename={filepath};Mode=ReadOnly;";
            return new LiteDatabase(connStr);
        }
    }
}
