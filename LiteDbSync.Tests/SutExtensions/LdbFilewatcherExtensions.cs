using CommonTools.Lib.ns11.FileSystemTools;
using Moq;
using System;

namespace LiteDbSync.Tests.SutExtensions
{
    static class LdbFilewatcherExtensions
    {
        public static void RaiseFileChanged(this Mock<IFileChangeWatcher> moq)
        {
            moq.Raise(_ => _.FileChanged += null, EventArgs.Empty);
        }
    }
}
