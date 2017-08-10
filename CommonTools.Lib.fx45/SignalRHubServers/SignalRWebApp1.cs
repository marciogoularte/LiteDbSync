using CommonTools.Lib.ns11.SignalRHubServers;
using System;
using System.Threading.Tasks;

namespace CommonTools.Lib.fx45.SignalRHubServers
{
    public class SignalRWebApp1 : ISignalRWebApp
    {
        private IDisposable _webApp;


        public void StartServer(string serverURI)
        {
            throw new NotImplementedException();
        }


        public void StopServer()
        {
            try { _webApp?.Dispose(); }
            catch { }
            _webApp = null;
        }


        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    StopServer();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
