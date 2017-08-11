using LiteDbSync.Common.API.Configuration;
using System.Collections.ObjectModel;

namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface IChangeSender
    {
        void SendChangesIfAny(DbWatcherSettings fileWatcherSettings);
        ObservableCollection<string> Logs { get; }
    }
}
