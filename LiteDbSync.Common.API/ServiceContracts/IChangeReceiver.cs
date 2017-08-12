using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface IChangeReceiver
    {
        Task<long> GetLastRemoteId     (string dbName);
        Task       SendRecordsToRemote (string dbName, List<string> records);
        Task       ReportDataAnomaly   (string dbName, string description);
    }
}
