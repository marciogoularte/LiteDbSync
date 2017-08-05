using LiteDbSync.Common.API.ServiceContracts;
using System;
using System.IO;

namespace LiteDbSync.Client.Lib45.FileWatchers
{
    public class LdbFileWatcher1 : ILdbFileWatcher, IDisposable
    {
        private      EventHandler _fileChanged;
        public event EventHandler  FileChanged
        {
            add    { _fileChanged -= value; _fileChanged += value; }
            remove { _fileChanged -= value; }
        }

        private FileSystemWatcher _fsWatchr;



        public void StartWatching(string ldbFilepath)
        {
            if (_fsWatchr != null) return;

            if (!File.Exists(ldbFilepath))
                throw new FileNotFoundException($"LDB file not found:{Environment.NewLine}{ldbFilepath}");

            var dir = Path.GetDirectoryName(ldbFilepath);
            var nme = Path.GetFileName(ldbFilepath);

            _fsWatchr                     = new FileSystemWatcher(dir, nme);
            _fsWatchr.NotifyFilter        = NotifyFilters.LastWrite;
            _fsWatchr.Changed            += new FileSystemEventHandler(OnLdbChanged);
            _fsWatchr.EnableRaisingEvents = true;
        }


        private void OnLdbChanged(object sender, FileSystemEventArgs e)
        {
            _fileChanged?.Invoke(this, EventArgs.Empty);
        }


        public void StopWatching()
        {
            if (_fsWatchr == null) return;
            _fsWatchr.EnableRaisingEvents = false;
            _fsWatchr = null;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    StopWatching();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
