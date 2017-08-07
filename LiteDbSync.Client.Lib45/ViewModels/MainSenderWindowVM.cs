using LiteDbSync.Common.API.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LiteDbSync.Client.Lib45.ViewModels
{
    public class MainSenderWindowVM
    {
        private IChangeEventThrottler _throtl;

        public MainSenderWindowVM(IChangeEventThrottler changeEventThrottler)
        {
            _throtl = changeEventThrottler;

            StartWatchingCmd = CreateStartWatchingCmd();
        }


        public ICommand StartWatchingCmd { get; }


        private ICommand CreateStartWatchingCmd()
        {
            throw new NotImplementedException();
        }
    }
}
