using Autofac;
using CommonTools.Lib.fx45.DependencyInjection;
using CommonTools.Lib.fx45.ExceptionTools;
using CommonTools.Lib.fx45.FileSystemTools;
using CommonTools.Lib.fx45.ViewModelTools;
using CommonTools.Lib.ns11.FileSystemTools;
using CommonTools.Lib.ns11.SignalRHubServers;
using LiteDbSync.Client.Lib45.ChangeSenders;
using LiteDbSync.Client.Lib45.Configuration;
using LiteDbSync.Client.Lib45.DatabaseReaders;
using LiteDbSync.Client.Lib45.HubClientProxies;
using LiteDbSync.Client.Lib45.ViewModels;
using LiteDbSync.Client.Lib45.ViewModels.SoloFileWatcher;
using LiteDbSync.Common.API.Configuration;
using LiteDbSync.Common.API.ServiceContracts;
using System;
using System.Windows;

namespace LiteDbSync.Client.Lib45.ComponentsRegistry
{
    public static class ChangeSenderComponents
    {
        private static ILifetimeScope BuildAndBeginScope(Application app)
        {
            SetDataTemplates(app);

            var b   = new ContainerBuilder();
            var cfg = ChangeSenderCfgFile.LoadOrDefault();
            b.RegisterInstance<ChangeSenderSettings>(cfg)
                            .As<IHubClientSettings>()
                            .AsSelf();

            b.Solo  <MainSenderWindowVM>();
            b.Multi <SoloFileWatcherVM>();

            b.Multi <IThrottledFileWatcher, ThrottledFileWatcher1>();
            b.Multi <ILocalDbReader, LocalDbReader1>();
            b.Multi <IChangeSender, ChangeSender1>();
            b.Multi <IChangeReceiver, ChangeReceiverClientProxy1>();

            return b.Build().BeginLifetimeScope();
        }


        private static void SetDataTemplates(Application app)
        {
            app?.SetTemplate<SoloFileWatcherVM, SoloFileWatcherUI>();
        }


        public static ILifetimeScope Build(Application app)
        {
            try
            {
                return BuildAndBeginScope(app);
            }
            catch (Exception ex)
            {
                ex.ShowAlert(true, true);
                return null;
            }
        }
    }
}
