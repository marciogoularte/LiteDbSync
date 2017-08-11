using CommonTools.Lib.fx45.InputTools;
using CommonTools.Lib.fx45.ViewModelTools;
using CommonTools.Lib.ns11.FileSystemTools;
using CommonTools.Lib.ns11.InputTools;
using LiteDbSync.Common.API.Configuration;
using LiteDbSync.Common.API.ServiceContracts;
using System;
using System.IO;

namespace LiteDbSync.Client.Lib45.ViewModels.SoloFileWatcher
{
    public class SoloFileWatcherVM : ViewModelBase
    {
        private IThrottledFileWatcher _watchr;
        private DbWatcherSettings   _cfg;


        public SoloFileWatcherVM(IThrottledFileWatcher throttledFileWatcher,
                                 IChangeSender changeSender)
        {
            _watchr = throttledFileWatcher;
            Sender  = changeSender;
            _watchr.FileChanged += _watchr_FileChanged;

            StartWatchingCmd = R2Command.Relay(StartWatchingLDB);
            StopWatchingCmd  = R2Command.Relay(StopWatchingLDB);
        }


        public IR2Command     StartWatchingCmd  { get; }
        public IR2Command     StopWatchingCmd   { get; }
        public IChangeSender  Sender            { get; }


        private void StartWatchingLDB()
        {
            _watchr.IntervalMS = _cfg.IntervalMS;
            _watchr.StartWatching(_cfg.DbFilePath);
        }


        private void StopWatchingLDB()
        {
            _watchr.StopWatching();
        }


        private void _watchr_FileChanged(object sender, EventArgs e)
        {
            Sender.SendChangesIfAny(_cfg);
        }


        internal void SetTarget(DbWatcherSettings fileWatcherSettings)
        {
            _cfg = fileWatcherSettings;
            var nme = Path.GetFileName(_cfg.DbFilePath);
            var typ = _cfg.CollectionName;
            var ims = $"every {_cfg.IntervalMS:n0} ms";
            UpdateTitle($"Watching “{nme}” ‹{typ}› [{ims}]");
        }
    }
}
