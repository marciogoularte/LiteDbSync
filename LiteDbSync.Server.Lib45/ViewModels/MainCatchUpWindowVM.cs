using CommonTools.Lib.fx45.SignalRServers;
using CommonTools.Lib.fx45.ViewModelTools;

namespace LiteDbSync.Server.Lib45.ViewModels
{
    public class MainCatchUpWindowVM : MainWindowVmBase
    {
        protected override string CaptionPrefix => "Catch-up Writer";


        public MainCatchUpWindowVM(SignalRServerToggleVM signalRServerToggleVM,
                                   CommonLogListVM commonLogListVM)
        {
            CommonLogs   = commonLogListVM;
            ServerToggle = signalRServerToggleVM;
            ServerToggle.StartServerCmd.ExecuteIfItCan();
        }


        public SignalRServerToggleVM  ServerToggle  { get; }
        public CommonLogListVM        CommonLogs    { get; }
    }
}
