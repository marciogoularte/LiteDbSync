using CommonTools.Lib.fx45.ExceptionTools;
using LiteDbSync.Common.API.ServiceContracts;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Net.Http;
using System.Threading;
using System.Windows;

namespace LiteDbSync.Client.Lib45.ChangeSenders
{
    public class ChangeSender1 : IChangeSender
    {
        public async void SendLatestId(long id)
        {

            var conn = new HubConnection("http://localhost:12345/");
            var hub  = conn.CreateHubProxy("SampleHub1");

            try
            {
                await conn.Start();
                await hub.Invoke("ReceiveLatestId", id);
            }
            catch (HttpRequestException ex)
            {
                //StatusText.Content = "Unable to connect to server: Start server before connecting clients.";
                //No connection: Don't enable Send button or show chat UI
                ex.ShowAlert();
                return;
            }
            catch (Exception ex)
            {
                ex.ShowAlert();
                return;
            }

            //MessageBox.Show($"{conn.State}");
        }
    }
}
