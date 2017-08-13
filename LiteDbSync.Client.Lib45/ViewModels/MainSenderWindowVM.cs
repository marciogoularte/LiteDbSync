using Autofac;
using CommonTools.Lib.fx45.InputTools;
using CommonTools.Lib.fx45.ViewModelTools;
using CommonTools.Lib.ns11.InputTools;
using LiteDbSync.Client.Lib45.ViewModels.SoloFileWatcher;
using LiteDbSync.Common.API.Configuration;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LiteDbSync.Client.Lib45.ViewModels
{
    public class MainSenderWindowVM : MainWindowVmBase
    {
        protected override string CaptionPrefix => "Change Sender";


        public MainSenderWindowVM(ChangeSenderSettings changeSenderSettings) : base()
        {
            Config = changeSenderSettings;

            WatchAllCmd = R2Command.Relay(StartWatchingAll);
            WatchAllCmd.ExecuteIfItCan();
        }


        public ChangeSenderSettings  Config       { get; }
        public IR2Command            WatchAllCmd  { get; }

        public ObservableCollection<SoloFileWatcherVM> WatchList { get; } = new ObservableCollection<SoloFileWatcherVM>();


        public void ResolveInternals(ILifetimeScope scope)
        {
            foreach (var watchedFile in Config.WatchList)
            {
                var soloWatchr = scope.Resolve<SoloFileWatcherVM>();
                soloWatchr.SetTarget(watchedFile);
                WatchList.Add(soloWatchr);
            }
        }


        protected override async Task BeforeExitApp()
        {
            StartBeingBusy("Stopping Watchers ...");

            foreach (var soloWatchr in WatchList)
            {
                soloWatchr.StopWatchingCmd.ExecuteIfItCan();
                await Task.Delay(1000 * 1);
            }
        }


        private void StartWatchingAll()
        {
            foreach (var soloWatchr in WatchList)
                soloWatchr.StartWatchingCmd.ExecuteIfItCan();
        }
    }
}
