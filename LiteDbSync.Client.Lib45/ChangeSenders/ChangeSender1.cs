using CommonTools.Lib.fx45.ExceptionTools;
using CommonTools.Lib.ns11.SignalRHubServers;
using LiteDbSync.Common.API.ServiceContracts;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Net.Http;

namespace LiteDbSync.Client.Lib45.ChangeSenders
{
    public class ChangeSender1 : IChangeSender
    {
        private IHubClientSettings _cfg;

        public ChangeSender1(IHubClientSettings hubClientSettings)
        {
            _cfg = hubClientSettings;
        }


        public async void SendLatestId(long id)
        {
            var conn = new HubConnection(_cfg.ServerURL);
            var hub  = conn.CreateHubProxy(_cfg.HubName);

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
