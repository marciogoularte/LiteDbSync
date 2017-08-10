using CommonTools.Lib.fx45.DependencyInjection;
using LiteDbSync.Server.Lib45.ComponentsRegistry;
using LiteDbSync.Server.Lib45.ViewModels;
using Microsoft.Owin.Hosting;
using System.Windows;

namespace LiteDbSync.CatchUpWriter.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //using (var scope = CatchUpWriterComponents.Build(this))
            //{
            //    if (scope.TryResolveOrAlert<MainCatchUpWindowVM>
            //                           (out MainCatchUpWindowVM vm))
            //    {
            //        var win = new MainWindow();
            //        win.DataContext = vm;
            //        win.Show();
            //    }
            //    else
            //        this.Shutdown();
            //}


            //using (var app = WebApp.Start<Startup>("http://localhost:12345/"))
            //{
            //    var win = new MainWindow();
            //    win.DataContext = app;
            //    win.Show();
            //}

            var win = new MainWindow();
            win.Show();
        }
    }
}
