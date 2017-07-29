using Autofac.Extras.Moq;
using LiteDbSync.Client.Lib45.Schedulers;
using LiteDbSync.Common.API.ServiceContracts;
using LiteDbSync.Tests.SutExtensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LiteDbSync.Tests.Schedulers
{
    [Trait("ChangeEventScheduler1", "Solitary")]
    public class ChangeEventScheduler1Facts
    {
        [Fact(DisplayName = "Calls Post on FileChanged")]
        public void CallsPostonFileChanged()
        {
            var sut    = New<ChangeEventScheduler1>(out AutoMock moq);
            var watchr = moq.Mock<ILdbFileWatcher>();
            var sendr  = moq.Mock<IChangeSender>();
            sut.StartWatching("abc", 1000);

            watchr.RaiseFileChanged();

            sendr.Verify(_ => _.SendLatestId(Any.ULong));
        }


        [Fact(DisplayName = "Won't Post if below minimum interval")]
        public void WontPostifbelowminimuminterval()
        {
            var sut    = New<ChangeEventScheduler1>(out AutoMock moq);
            var watchr = moq.Mock<ILdbFileWatcher>();
            var sendr  = moq.Mock<IChangeSender>();
            sut.StartWatching("abc", 1000);

            watchr.RaiseFileChanged();
            watchr.RaiseFileChanged();

            sendr.Verify(_ => _.SendLatestId(Any.ULong), Times.Once);
        }


        [Fact(DisplayName = "Waits for interval before next send")]
        public async Task Waitsforintervalbeforenextsend()
        {
            var sut    = New<ChangeEventScheduler1>(out AutoMock moq);
            var watchr = moq.Mock<ILdbFileWatcher>();
            var sendr  = moq.Mock<IChangeSender>();
            sut.StartWatching("abc", 200);

            watchr.RaiseFileChanged();
            watchr.RaiseFileChanged();
            watchr.RaiseFileChanged();
            watchr.RaiseFileChanged();
            watchr.RaiseFileChanged();
            await Task.Delay(210);
            watchr.RaiseFileChanged();

            sendr.Verify(_ => _.SendLatestId(Any.ULong), Times.Exactly(2));
        }


        private T New<T>(out AutoMock moq) where T : ChangeEventScheduler1
        {
            moq = AutoMock.GetLoose();
            return moq.Create<T>();
        }
    }
}
