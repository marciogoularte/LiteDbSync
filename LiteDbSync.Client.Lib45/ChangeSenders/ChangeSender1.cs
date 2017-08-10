using CommonTools.Lib.fx45.SignalRClients;
using CommonTools.Lib.ns11.SignalRHubServers;
using LiteDbSync.Common.API.ServiceContracts;
using Microsoft.AspNet.SignalR.Client;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace LiteDbSync.Client.Lib45.ChangeSenders
{
    public class ChangeSender1 : IChangeSender
    {
        private IHubClientSettings     _cfg;
        private SynchronizationContext _ui;
        private ILocalDbReader         _local;

        public ChangeSender1(IHubClientSettings hubClientSettings,
                             ILocalDbReader localDbReader)
        {
            _cfg   = hubClientSettings;
            _ui    = SynchronizationContext.Current;
            _local = localDbReader;
        }


        public ObservableCollection<string> Logs { get; } = new ObservableCollection<string>();


        //public async void SendChangesIfAny(long localId)
        //{
        //    using (var conn = new HubConnection(_cfg.ServerURL))
        //    {
        //        try
        //        {
        //            var svrId = await GetLastServerId(conn);
        //            if (svrId == localId) return;
        //            //await SendLocalRecords(svrId + 1, localId);
        //        }
        //        catch (Exception ex)
        //        {
        //            Log(ex.Info(true, true));
        //        }
        //    }
        //}


        private async Task<long> GetLastServerId(HubConnection conn)
        {
            Log($"Connecting to {_cfg.HubName} on {_cfg.ServerURL} ...");
            var hub   = await conn.ConnectToHub(_cfg.HubName);
            var methd = nameof(IChangeReceiver.GetLastServerId);
            var svrId = await hub.Invoke<long>(methd);
            Log($"Latest Id on server: [{svrId:N0}]");
            return svrId;
        }


        //private Task SendLocalRecords(long startId, long endId)
        //{
        //    var recs = _local.GetRecords()
        //}


        private void Log(string message) => AsUI(_ => Logs.Insert(0, message));
        private void AsUI(SendOrPostCallback action) =>_ui.Send(action, null);
    }
}
