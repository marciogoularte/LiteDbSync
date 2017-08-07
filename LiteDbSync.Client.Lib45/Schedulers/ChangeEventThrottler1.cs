using LiteDbSync.Common.API.ServiceContracts;
using System;

namespace LiteDbSync.Client.Lib45.Schedulers
{
    public class ChangeEventThrottler1 : IChangeEventThrottler
    {
        private ILdbFileWatcher _watchr;
        private IChangeSender   _sendr;
        private ILocalDbReader  _readr;
        private int             _minIntrvlMS;
        private DateTime        _lastSend;


        public ChangeEventThrottler1(ILdbFileWatcher ldbFileWatcher,
                                     ILocalDbReader localDbReader,
                                     IChangeSender changeSender)
        {
            _watchr = ldbFileWatcher;
            _sendr  = changeSender;
            _readr  = localDbReader;
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

            var latestId = _readr.GetLatestId();
            _sendr.SendLatestId(latestId);
            _lastSend = DateTime.Now;
        }


        private bool IsTooSoon()
        {
            var elapsd = (DateTime.Now - _lastSend).TotalMilliseconds;
            return elapsd < _minIntrvlMS;
        }
    }
}
