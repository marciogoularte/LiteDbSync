using LiteDB;
using LiteDbSync.Common.API.ServiceContracts;

namespace LiteDbSync.Client.Lib45.DatabaseReaders
{
    public class LocalDbReader1 : ILocalDbReader
    {
        private const string CONN_STR = "";
        private const string COLXN_NAME = "";


        public ulong GetLatestId()
        {
            using (var db = new LiteDatabase(CONN_STR))
            {
                return db.GetCollection(COLXN_NAME).Max();
            }
        }
    }
}
