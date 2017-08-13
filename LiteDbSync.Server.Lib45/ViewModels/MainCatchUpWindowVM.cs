using CommonTools.Lib.fx45.SignalRServers;
using CommonTools.Lib.fx45.ViewModelTools;
using LiteDbSync.Common.API.Configuration;

namespace LiteDbSync.Server.Lib45.ViewModels
{
    public class MainCatchUpWindowVM : MainWindowVmBase
    {
        protected override string CaptionPrefix => "Catch-up Writer";


        public MainCatchUpWindowVM(SignalRServerToggleVM signalRServerToggleVM,
                                   CommonLogListVM commonLogListVM,
                                   CatchUpWriterSettings catchUpWriterSettings)
        {
            Config       = catchUpWriterSettings;
            CommonLogs   = commonLogListVM;
            ServerToggle = signalRServerToggleVM;
            ServerToggle.StartServerCmd.ExecuteIfItCan();
        }


        public CatchUpWriterSettings  Config        { get; }
        public SignalRServerToggleVM  ServerToggle  { get; }
        public CommonLogListVM        CommonLogs    { get; }
    }
}
