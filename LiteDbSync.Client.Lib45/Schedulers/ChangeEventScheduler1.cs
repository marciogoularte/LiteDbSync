using LiteDbSync.Common.API.ServiceContracts;
using System;

namespace LiteDbSync.Client.Lib45.Schedulers
{
    public class ChangeEventScheduler1
    {
        private ILdbFileWatcher     _watchr;
        private IChangeSender       _sendr;
        private int                 _minIntrvlMS;
        private DateTime            _lastSend;


        public ChangeEventScheduler1(ILdbFileWatcher ldbFileWatcher,
                                     IChangeSender changeSender)
        {
            _watchr = ldbFileWatcher;
            _sendr  = changeSender;
        }


        public void StartWatching(string ldbFilepath, int minIntervalMS)
        {
            _minIntrvlMS = minIntervalMS;
            _watchr.FileChanged += _watchr_FileChanged;
            _watchr.StartWatching(ldbFilepath);
        }



        private void _watchr_FileChanged(object sender, EventArgs e)
        {
            if (IsTooSoon()) return;

            _sendr.SendLatestId(123);
            _lastSend = DateTime.Now;
        }


        private bool IsTooSoon()
        {
            var elapsd = (DateTime.Now - _lastSend).TotalMilliseconds;
            return elapsd < _minIntrvlMS;
        }
    }
}
