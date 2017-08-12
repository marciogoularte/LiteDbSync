using CommonTools.Lib.fx45.SignalRClients;
using CommonTools.Lib.ns11.SignalRHubServers;
using LiteDbSync.Common.API.ServiceContracts;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace LiteDbSync.Client.Lib45.HubClientProxies
{
    public class ChangeReceiverClientProxy1 : IHubClientProxy, IChangeReceiver
    {
        private IHubClientSettings _cfg;
        private HubConnection      _conn;
        private IHubProxy          _hub;


        public ChangeReceiverClientProxy1(IHubClientSettings hubClientSettings)
        {
            _cfg = hubClientSettings;
        }


        public async Task<long> GetLastRemoteId(string dbName)
        {
            if (_conn == null) await Connect();
            var methd = nameof(IChangeReceiver.GetLastRemoteId);
            return await _hub.Invoke<long>(methd, dbName);
        }


        public async Task SendRecordsToRemote(string dbName, List<string> records)
        {
            if (_conn == null) await Connect();
            await _hub.Invoke(nameof(IChangeReceiver.SendRecordsToRemote), dbName, records);
        }


        public async Task ReportDataAnomaly(string dbName, string description)
        {
            if (_conn == null) await Connect();
            await _hub.Invoke(nameof(IChangeReceiver.ReportDataAnomaly), dbName, description);
        }


        public async Task Connect()
        {
            _conn = new HubConnection(_cfg.ServerURL);
            _hub  = await _conn.ConnectToHub(_cfg.HubName);
        }


        public void Disconnect()
        {
            try
            {
                _hub = null;
                _conn?.Dispose();
                _conn = null;
            }
            catch { }
        }


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Disconnect();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
