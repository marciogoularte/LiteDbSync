using LiteDbSync.Common.API.ServiceContracts;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace LiteDbSync.Server.Lib45.SignalRHubs
{
    [HubName("ChangeReceiverHub")]
    public class ChangeReceiverHub1 : Hub, IChangeReceiver
    {
        public long GetLastServerId()
        {
            return 23;
        }
    }
}
