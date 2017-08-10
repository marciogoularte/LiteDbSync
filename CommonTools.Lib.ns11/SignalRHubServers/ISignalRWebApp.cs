using System;
using System.Threading.Tasks;

namespace CommonTools.Lib.ns11.SignalRHubServers
{
    public interface ISignalRWebApp : IDisposable
    {
        void StartServer(string serverURI);
        void StopServer();
    }
}
