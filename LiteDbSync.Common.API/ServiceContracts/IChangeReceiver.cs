using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface IChangeReceiver
    {
        Task<long> GetLastRemoteId     (string dbKey);
        Task       SendRecordsToRemote (string dbKey, List<string> records);
        Task       ReportDataAnomaly   (string dbKey, string description);
    }
}
