using System.Collections.ObjectModel;

namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface IChangeSender
    {
        //void SendChangesIfAny(long latestId);

        ObservableCollection<string> Logs { get; }
    }
}
