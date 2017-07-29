using LiteDbSync.Common.API.ServiceContracts;
using Moq;
using System;

namespace LiteDbSync.Tests.SutExtensions
{
    static class LdbFilewatcherExtensions
    {
        public static void RaiseFileChanged(this Mock<ILdbFileWatcher> moq)
        {
            moq.Raise(_ => _.FileChanged += null, EventArgs.Empty);
        }
    }
}
