using LiteDbSync.Common.API.ServiceContracts;
using System.Threading;
using System.Windows;

namespace LiteDbSync.Client.Lib45.ChangeSenders
{
    public class ChangeSender1 : IChangeSender
    {
        public void SendLatestId(ulong latestId)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show($"latestId:  {latestId}");
            }
            )).Start();
        }
    }
}
