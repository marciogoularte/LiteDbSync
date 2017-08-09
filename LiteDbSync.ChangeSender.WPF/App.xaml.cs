using CommonTools.Lib.fx45.DependencyInjection;
using LiteDbSync.Client.Lib45.ComponentsRegistry;
using LiteDbSync.Client.Lib45.ViewModels;
using System.Windows;

namespace LiteDbSync.ChangeSender.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var scope = ChangeSenderComponents.Build(this))
            {
                if (scope.TryResolveOrAlert<MainSenderWindowVM>
                                       (out MainSenderWindowVM vm))
                {
                    var win = new MainWindow();
                    vm.ResolveInternals(scope);
                    win.DataContext = vm;
                    win.Show();
                }
                else
                    this.Shutdown();
            }
        }
    }
}
