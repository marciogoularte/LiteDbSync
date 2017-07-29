using Autofac;
using LiteDbSync.Client.Lib45.FileWatchers;
using LiteDbSync.Common.API.ServiceContracts;

namespace LiteDbSync.Client.Lib45
{
    public static class ComponentRegistry
    {
        public static ILifetimeScope Build()
        {
            var buildr = new ContainerBuilder();

            buildr.RegisterType<LdbFileWatcher1>()
                            .As<ILdbFileWatcher>();

            return buildr.Build().BeginLifetimeScope();
        }
    }
}
