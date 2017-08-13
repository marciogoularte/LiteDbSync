using CommonTools.Lib.fx45.LiteDbTools;
using CommonTools.Lib.ns11.LiteDbTools;
using LiteDB;
using SampleClient.Writer;

namespace LiteDbSync.Tests.SampleDbWriters
{
    public class LiteDbClient1
    {
        private ILiteDbSettings   _cfg;
        private TypelessDbWriter1 _db;

        public LiteDbClient1(ILiteDbSettings liteDbSettings)
        {
            _cfg = liteDbSettings;
            _db  = new TypelessDbWriter1();
        }


        public long GetLatestId()
            => _db.GetLatestId(_cfg.DbFilePath, _cfg.CollectionName);

        //public static SampleWriter1 Create(out string dbFilepath)
        //{
        //    dbFilepath = Path.GetTempFileName();
        //    return new SampleWriter1(dbFilepath);
        //}


        public void Insert(string text)
        {
            using (var db = ConnectToRepo())
            {
                var rec = new SampleRecord { Text1 = text };
                db.Insert(rec);
            }
        }


        public int Count()
        {
            using (var db = ConnectToRepo())
                return db.Query<SampleRecord>().Count();
        }


        public SampleRecord GetById(long id)
        {
            using (var db = ConnectToRepo())
                return db.Query<SampleRecord>()
                    .SingleById(id);
        }


        private LiteRepository ConnectToRepo()
        {
            var mapr = new BsonMapper();
            var conn = $"Filename={_cfg.DbFilePath}";

            mapr.RegisterAutoId<ulong>(v => v == 0,
                (db, col) => (ulong)db.Count(col) + 1);

            return new LiteRepository(conn, mapr);
        }
    }
}
