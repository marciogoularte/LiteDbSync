using LiteDbSync.Server.Lib45.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace LiteDbSync.CatchUpWriter.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        protected override async void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            var vm = DataContext as MainCatchUpWindowVM;
            await vm.ServerToggle.StopServerCmd.RunAsync();
            vm.ExitCmd.ExecuteIfItCan();
        }
    }
}
