using Autofac;
using CommonTools.Lib.fx45.FileSystemTools;
using FluentAssertions;
using LiteDbSync.Common.API.Configuration;
using LiteDbSync.Tests.SampleDbWriters;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace LiteDbSync.Tests.AcceptanceTests
{
    [Trait("AppendOnly", "Acceptance")]
    public class AppendOnlyDbSyncFacts
    {
        const string DB_NAME = "TestDB";

        private ILifetimeScope _recvrScope;


        [Fact(DisplayName = "Syncs 1 record")]
        public async Task Syncs1record()
        {
            var sendr = StartSenderProcess(out DbWatcherSettings srcCfg);
            var recvr = StartReceiverProcess(out DbTargetSettings targCfg);
            var srcDB = new LiteDbClient1(srcCfg);
            var trgDB = new LiteDbClient1(targCfg);

            srcDB.Insert("sample record text");
            var srcId = srcDB.GetLatestId();

            await Task.Delay(1000 * 3);
            var targId = trgDB.GetLatestId();
            targId.Should().Be(srcId);

            var rec = trgDB.GetById(targId);
            rec.Text1.Should().Be("sample record text");

            sendr.Close();
            recvr.Close();
        }


        private Process StartSenderProcess(out DbWatcherSettings watcherSettings)
        {
            var cfg  = JsonFile.Read<ChangeSenderSettings>(@"..\..\..\LiteDbSync.ChangeSender.WPF\bin\Release\ChangeSender.cfg");
            watcherSettings = cfg.WatchList[0];
            return Process.Start(@"..\..\..\LiteDbSync.ChangeSender.WPF\bin\Release\ChangeSender.exe");
        }


        private Process StartReceiverProcess(out DbTargetSettings targetSettings)
        {
            var cfg = JsonFile.Read<CatchUpWriterSettings>(@"..\..\..\LiteDbSync.CatchUpWriter.WPF\bin\Release\CatchUpWriter.cfg");
            targetSettings = cfg.Targets[0];
            return Process.Start(@"..\..\..\LiteDbSync.CatchUpWriter.WPF\bin\Release\CatchUpWriter.exe");
        }
    }
}
