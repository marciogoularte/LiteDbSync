using Autofac;
using CommonTools.Lib.fx45.DependencyInjection;
using LiteDbSync.Server.Lib45.ComponentsRegistry;
using LiteDbSync.Server.Lib45.ViewModels;
using System.Windows;

namespace LiteDbSync.CatchUpWriter.WPF
{
    public partial class App : Application
    {
        private ILifetimeScope _scope;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _scope = CatchUpWriterComponents.Build(this);
            if (_scope.TryResolveOrAlert<MainCatchUpWindowVM>
                                    (out MainCatchUpWindowVM vm))
            {
                var win = new MainWindow();
                win.DataContext = vm;
                win.Show();
            }
            else
                this.Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            try { _scope?.Dispose(); }
            catch { }
        }
    }
}
