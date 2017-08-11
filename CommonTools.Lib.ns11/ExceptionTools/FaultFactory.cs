using System;
using System.Runtime.CompilerServices;

namespace CommonTools.Lib.ns11.ExceptionTools
{
    public static class Fault
    {
        public static InvalidOperationException CallFirst(string requiredMethod, [CallerMemberName] string callerMemberName = null)
        {
            var msg = $"Please call method “{requiredMethod}” before calling “{callerMemberName}”.";
            return new InvalidOperationException(msg);
        }
    }
}
