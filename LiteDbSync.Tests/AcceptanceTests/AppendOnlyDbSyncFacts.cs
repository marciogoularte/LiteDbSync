﻿using Autofac;
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
        [Fact(DisplayName = "Syncs 1 record")]
        public async Task Syncs1record()
        {
            var sendr = StartSenderProcess(out DbWatcherSettings srcCfg);
            var recvr = StartReceiverProcess(out DbTargetSettings targCfg);
            var srcDB = new LiteDbClient1(srcCfg);
            var trgDB = new LiteDbClient1(targCfg);

            await Task.Delay(1000 * 2);
            srcDB.Insert("sample record text");
            var srcId = srcDB.GetLatestId();

            await Task.Delay(1000 * 2);
            var targId = trgDB.GetLatestId();
            targId.Should().Be(srcId);

            var rec = trgDB.GetById(targId);
            rec.Text1.Should().Be("sample record text");

            sendr.CloseMainWindow();
            recvr.CloseMainWindow();
        }


        private Process StartSenderProcess(out DbWatcherSettings watcherSettings)
        {
            var cfg  = JsonFile.Read<ChangeSenderSettings>("ChangeSender.cfg");
            watcherSettings = cfg.WatchList[0];
            return Process.Start(@"..\..\..\LiteDbSync.ChangeSender.WPF\bin\Debug\ChangeSender.exe");
        }


        private Process StartReceiverProcess(out DbTargetSettings targetSettings)
        {
            var cfg = JsonFile.Read<CatchUpWriterSettings>("CatchUpWriter.cfg");
            targetSettings = cfg.Targets[0];
            return Process.Start(@"..\..\..\LiteDbSync.CatchUpWriter.WPF\bin\Debug\CatchUpWriter.exe");
        }
    }
}
