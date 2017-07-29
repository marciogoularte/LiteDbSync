using Moq;
using System.Threading;

namespace LiteDbSync.Tests.SutExtensions
{
    public class Any
    {
        public static ulong             ULong => It.IsAny<ulong>();
        public static uint              UInt  => It.IsAny<uint>();
        public static string            Text  => It.IsAny<string>();
        public static object            Obj   => It.IsAny<object>();
        public static CancellationToken Tkn   => It.IsAny<CancellationToken>();
    }
}
