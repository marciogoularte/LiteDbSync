using System;

namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface ILdbFileWatcher
    {
        event EventHandler FileChanged;
        void  StartWatching (string ldbFilepath);
        void  StopWatching  ();
    }
}
