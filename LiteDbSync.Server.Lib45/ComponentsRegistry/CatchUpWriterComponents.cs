using Autofac;
using Autofac.Integration.SignalR;
using CommonTools.Lib.fx45.DependencyInjection;
using CommonTools.Lib.fx45.ExceptionTools;
using CommonTools.Lib.fx45.SignalRHubServers;
using CommonTools.Lib.ns11.SignalRHubServers;
using LiteDbSync.Common.API.Configuration;
using LiteDbSync.Server.Lib45.Configuration;
using LiteDbSync.Server.Lib45.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Reflection;
using System.Windows;

namespace LiteDbSync.Server.Lib45.ComponentsRegistry
{
    public static class CatchUpWriterComponents
    {
        private static ILifetimeScope BuildAndBeginScope(Application app)
        {
            var b   = new ContainerBuilder();
            b.RegisterHubs(Assembly.GetExecutingAssembly());

            var cfg = CatchUpWriterCfgFile.LoadOrDefault();
            b.RegisterInstance<CatchUpWriterSettings>(cfg)
                            .As<ISignalRServerSettings>()
                            .AsSelf();

            b.Solo <MainCatchUpWindowVM>();
            b.Solo <SignalRServerToggleVM>();
            b.Solo <ISignalRWebApp, SignalRWebApp1>();

            //b.Multi<IThrottledFileWatcher, ThrottledFileWatcher1>();
            //b.Multi<ILocalDbReader, LocalDbReader1>();
            //b.Multi<IChangeSender, ChangeSender1>();

            SetDataTemplates(app);

            var containr = b.Build();
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(containr);

            return containr.BeginLifetimeScope();
        }


        private static void SetDataTemplates(Application app)
        {
            //app.SetTemplate<SoloFileWatcherVM, SoloFileWatcherUI>();
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
