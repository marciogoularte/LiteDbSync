using Autofac;
using LiteDbSync.Client.Lib45;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LiteDbSync.ChangeSender.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var scope = ComponentRegistry.Build())
            {
                var win = new MainWindow();
                win.DataContext = scope.Resolve<MainSenderWindowVM>();
                win.Show();
            }
        }
    }
}
