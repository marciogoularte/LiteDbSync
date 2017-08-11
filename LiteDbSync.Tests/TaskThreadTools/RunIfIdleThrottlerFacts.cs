using CommonTools.Lib.ns11.TaskThreadTools;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Xunit;

namespace LiteDbSync.Tests.TaskThreadTools
{
    [Trait("RunIfIdleThrottler", "Solitary")]
    public class RunIfIdleThrottlerFacts
    {
        [Fact(DisplayName = "Runs task if idle")]
        public async Task Runstaskifidle()
        {
            var doer = new SampleTask();
            var sut  = new RunIfIdleThrottler(() 
                        => doer.DoSomething());
            doer.InvokeCount.Should().Be(0);

            sut.RunIfIdle();
            doer.Input = "a";

            await Task.Delay(10);

            doer.InvokeCount.Should().Be(1);
            doer.Output.Should().Be("processed:a");
        }


        [Fact(DisplayName = "Won't run if still running")]
        public async Task Wontrunifstillrunning()
        {
            var doer = new SampleTask(100);
            var sut  = new RunIfIdleThrottler(() 
                        => doer.DoSomething());
            doer.InvokeCount.Should().Be(0);

            doer.Input = "a";
            sut.RunIfIdle();

            doer.Input = "b";
            sut.RunIfIdle();

            doer.Input = "c";
            sut.RunIfIdle();

            doer.Input = "d";
            sut.RunIfIdle();

            doer.Input = "e";
            sut.RunIfIdle();

            doer.Input = "f";
            sut.RunIfIdle();

            await Task.Delay(1000);
            doer.InvokeCount.Should().Be(1);
            doer.Output.Should().Be("processed:a");

            await Task.Delay(1000);
            doer.InvokeCount.Should().Be(1);
            doer.Output.Should().Be("processed:a");
        }



        public class SampleTask
        {
            private int _delayMS;
            private ConcurrentQueue<int> _queue = new ConcurrentQueue<int>();

            public SampleTask(int delayMS = 1)
            {
                _delayMS = delayMS;
            }


            public int InvokeCount => _queue.Count;

            public string Input  { get; set; }
            public string Output { get; private set; }

            public async Task DoSomething()
            {
                _queue.Enqueue(0);
                await Task.Delay(_delayMS);
                Output = $"processed:{Input}";
            }
        }
    }


}
