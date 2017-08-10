using System;
using Autofac;
using CommonTools.Lib.fx45.ViewModelTools;
using CommonTools.Lib.ns11.InputTools;
using System.Collections.ObjectModel;
using CommonTools.Lib.fx45.InputTools;
using CommonTools.Lib.ns11.SignalRHubServers;
using LiteDbSync.Common.API.Configuration;
using CommonTools.Lib.fx45.SignalRHubServers;

namespace LiteDbSync.Server.Lib45.ViewModels
{
    public class MainCatchUpWindowVM : MainWindowVmBase
    {
        protected override string CaptionPrefix => "Catch-up Writer";

        private ISignalRWebApp        _signlr;
        private CatchUpWriterSettings _cfg;


        public MainCatchUpWindowVM(SignalRServerToggleVM signalRServerToggleVM)
        {
            ServerToggle = signalRServerToggleVM;
        }


        public SignalRServerToggleVM  ServerToggle  { get; }






    }
}
