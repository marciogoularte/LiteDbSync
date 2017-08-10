using CommonTools.Lib.fx45.SignalRHubServers;
using CommonTools.Lib.fx45.ViewModelTools;

namespace LiteDbSync.Server.Lib45.ViewModels
{
    public class MainCatchUpWindowVM : MainWindowVmBase
    {
        protected override string CaptionPrefix => "Catch-up Writer";


        public MainCatchUpWindowVM(SignalRServerToggleVM signalRServerToggleVM)
        {
            ServerToggle = signalRServerToggleVM;
            ServerToggle.StartServerCmd.ExecuteIfItCan();
        }


        public SignalRServerToggleVM  ServerToggle  { get; }
    }
}
