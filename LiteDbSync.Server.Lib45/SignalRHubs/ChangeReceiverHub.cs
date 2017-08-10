using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading;
using System.Windows;

namespace LiteDbSync.Server.Lib45.SignalRHubs
{
    [HubName("ChangeReceiverHub")]
    public class ChangeReceiverHub : Hub
    {
        public void ReceiveLatestId(long id)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show($"hub received Id:  {id}");
            }
            )).Start();
        }
    }
}
