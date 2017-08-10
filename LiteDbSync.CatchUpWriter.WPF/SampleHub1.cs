using Microsoft.AspNet.SignalR;
using System.Threading;
using System.Windows;

namespace LiteDbSync.CatchUpWriter.WPF
{
    public class SampleHub1 : Hub
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
