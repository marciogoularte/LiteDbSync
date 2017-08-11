using CommonTools.Lib.ns11.StringTools;
using LiteDbSync.Common.API.ServiceContracts;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Threading;

namespace LiteDbSync.Server.Lib45.SignalRHubs
{
    [HubName("ChangeReceiverHub")]
    public class ChangeReceiverHub1 : Hub, IChangeReceiver
    {
        private ILocalDbWriter _db;


        public ChangeReceiverHub1(ILocalDbWriter localDbWriter)
        {
            _db = localDbWriter;
        }


        public async  Task<long> GetLastRemoteId(string dbKey)
        {
            await Task.Delay(0);
            return 123;
            //return _db.GetLatestId()
        }


        public async Task ReportDataAnomaly(string dbKey, string description)
        {
            await Task.Delay(0);
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show(description, "«DATA_ANOMALY»");
            }
            )).Start();
        }


        public async Task SendRecordsToRemote(string dbKey, List<string> records)
        {
            await Task.Delay(0);
            //new Thread(new ThreadStart(delegate
            //{
            //    MessageBox.Show($"latestId:  {latestId}");
            //}
            //)).Start();
            MessageBox.Show(string.Join(L.f, records), "ChangeReceiverHub1 received the records");
        }
    }
}
