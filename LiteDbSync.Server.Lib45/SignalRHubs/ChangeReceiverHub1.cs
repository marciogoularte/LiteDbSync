using CommonTools.Lib.fx45.ViewModelTools;
using CommonTools.Lib.ns11.CollectionTools;
using LiteDbSync.Common.API.Configuration;
using LiteDbSync.Common.API.ServiceContracts;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LiteDbSync.Server.Lib45.SignalRHubs
{
    [HubName("ChangeReceiverHub")]
    public class ChangeReceiverHub1 : Hub, IChangeReceiver
    {
        private ILocalDbWriter        _db;
        private CatchUpWriterSettings _cfg;
        private CommonLogListVM       _log;


        public ChangeReceiverHub1(ILocalDbWriter localDbWriter,
                                  CatchUpWriterSettings catchUpWriterSettings,
                                  CommonLogListVM commonLogListVM)
        {
            _db  = localDbWriter;
            _cfg = catchUpWriterSettings;
            _log = commonLogListVM;
        }


        public async  Task<long> GetLastRemoteId(string dbName)
        {
            await Task.Delay(0);
            var c = GetTargetSettings(dbName);
            try
            {
                return _db.GetLatestId(c.DbFilePath, c.CollectionName);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                return -1;
            }
        }


        public async Task ReportDataAnomaly(string dbName, string description)
        {
            await Task.Delay(0);
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show(description, "«DATA_ANOMALY»");
            }
            )).Start();
        }


        public async Task SendRecordsToRemote(string dbName, List<string> records)
        {
            await Task.Delay(0);
            //MessageBox.Show(string.Join(L.f, records), "ChangeReceiverHub1 received the records");
            var c = GetTargetSettings(dbName);
            try
            {
                _db.Insert(c.DbFilePath, c.CollectionName, records);
                _log.Add($"{records.Count:N0} record(s) inserted into “{c.UniqueDbName}”.");
            }
            catch (Exception ex)
            {
                _log.Add(ex);
            }
        }


        private DbTargetSettings GetTargetSettings(string dbName)
        {
            return _cfg.Targets.GetOne(_ 
                => _.UniqueDbName.Trim().ToLower() 
                        == dbName.Trim().ToLower(), "UniqueDbName == dbName");
        }
    }
}
