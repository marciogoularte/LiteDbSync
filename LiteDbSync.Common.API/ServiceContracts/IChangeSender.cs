using System;
using System.Collections.Generic;
using System.Text;

namespace LiteDbSync.Common.API.ServiceContracts
{
    public interface IChangeSender
    {
        void SendLatestId(ulong latestId);
    }
}
