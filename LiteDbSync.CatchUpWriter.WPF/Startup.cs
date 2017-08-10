using Autofac;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Owin;
using System.Reflection;

namespace LiteDbSync.CatchUpWriter.WPF
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var buildr = new ContainerBuilder();
            var hubCfg = new HubConfiguration
            {
                EnableDetailedErrors = true
            };


            buildr.RegisterHubs(Assembly.GetExecutingAssembly());


            // Set the dependency resolver to be Autofac.
            var container = buildr.Build();
            hubCfg.Resolver = new AutofacDependencyResolver(container);

            // OWIN SIGNALR SETUP:

            // Register the Autofac middleware FIRST, then the standard SignalR middleware.
            app.UseAutofacMiddleware(container);
            app.MapSignalR("/signalr", hubCfg);

            // To add custom HubPipeline modules, you have to get the HubPipeline
            // from the dependency resolver, for example:
            //var hubPipeline = config.Resolver.Resolve<IHubPipeline>();
            //hubPipeline.AddModule(new MyPipelineModule());
        }
    }
}