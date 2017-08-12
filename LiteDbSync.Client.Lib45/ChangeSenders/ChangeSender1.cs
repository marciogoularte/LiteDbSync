using CommonTools.Lib.ns11.ExceptionTools;
using LiteDbSync.Common.API.Configuration;
using LiteDbSync.Common.API.ServiceContracts;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace LiteDbSync.Client.Lib45.ChangeSenders
{
    public class ChangeSender1 : IChangeSender, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SynchronizationContext _ui;
        private ILocalDbReader         _local;
        private bool                   _isBusy;
        private IChangeReceiver        _hub;


        public ChangeSender1(ILocalDbReader localDbReader,
                             IChangeReceiver changeReceiverClientProxy)
        {
            _ui    = SynchronizationContext.Current;
            _local = localDbReader;
            _hub   = changeReceiverClientProxy;
        }


        public long   LocalId   { get; private set; }
        public long   RemoteId  { get; private set; }

        public ObservableCollection<string> Logs { get; } = new ObservableCollection<string>();


        public void SendChangesIfAny(DbWatcherSettings dbCfg)
        {
            if (_isBusy)
            {
                Log("‹ChangeSender› cannot process the request to [SendChangesIfAny] while a previous request is running.");
                return;
            }
            _isBusy = true;

            Task.Run(async () =>
            {
                try
                {
                    await SendChangesIfAnyAsync(dbCfg);
                }
                catch (Exception ex)
                {
                    Log(ex.Info(true, true));
                }
                _isBusy = false;
            });
        }


        private async Task SendChangesIfAnyAsync(DbWatcherSettings cfg)
        {
            LocalId  = _local.GetLatestId(cfg.DbFilePath, cfg.CollectionName);
            RemoteId = await GetLastServerId(cfg);
            if (RemoteId < 0) return;
            if (RemoteId == LocalId) return;

            if (RemoteId > LocalId)
            {
                await ReportRemoteIdGreaterThanLocal(cfg);
                return;
            }
            await SendChangesToServer(cfg);
            await SendChangesIfAnyAsync(cfg);
        }


        private async Task SendChangesToServer(DbWatcherSettings cfg)
        {
            var recs = _local.GetRecords(cfg.DbFilePath,
                            cfg.CollectionName, RemoteId + 1, LocalId);

            Log($"Sending {recs.Count:N0} record(s) to remote database ...");

            await _hub.SendRecordsToRemote(cfg.UniqueDbName, recs);
        }


        private async Task ReportRemoteIdGreaterThanLocal(DbWatcherSettings cfg)
        {
            var msg = $"Remote ID [{RemoteId}] is greater than Local ID [{LocalId}].";
            Log("«DATA_ANOMALY»  " + msg);
            await _hub.ReportDataAnomaly(cfg.UniqueDbName, msg);
        }


        private async Task<long> GetLastServerId(DbWatcherSettings cfg)
        {
            var newId = await _hub.GetLastRemoteId(cfg.UniqueDbName);

            if (newId != RemoteId)
                Log($"Latest remote Id: [{newId:N0}]");

            return newId;
        }


        private void Log(string message)
        {
            var now = DateTime.Now.ToShortTimeString();
            AsUI(_ => Logs.Insert(0, $"[{now}]  {message}"));
        }
        private void AsUI(SendOrPostCallback action) =>_ui.Send(action, null);
    }
}
