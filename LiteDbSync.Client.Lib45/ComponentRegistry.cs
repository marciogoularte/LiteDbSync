using Autofac;
using LiteDbSync.Client.Lib45.ChangeSenders;
using LiteDbSync.Client.Lib45.DatabaseReaders;
using LiteDbSync.Client.Lib45.FileWatchers;
using LiteDbSync.Client.Lib45.Schedulers;
using LiteDbSync.Client.Lib45.ViewModels;
using LiteDbSync.Common.API.ServiceContracts;

namespace LiteDbSync.Client.Lib45
{
    public static class ComponentRegistry
    {
        public static ILifetimeScope Build()
        {
            var buildr = new ContainerBuilder();

            buildr.RegisterType<ChangeEventThrottler1>()
                            .As<IChangeEventThrottler>();

            buildr.RegisterType<MainSenderWindowVM>()
                            .AsSelf();

            buildr.RegisterType<LdbFileWatcher1>()
                            .As<ILdbFileWatcher>();

            buildr.RegisterType<LocalDbReader1>()
                            .As<ILocalDbReader>();

            buildr.RegisterType<ChangeSender1>()
                            .As<IChangeSender>();

            return buildr.Build().BeginLifetimeScope();
        }
    }
}
