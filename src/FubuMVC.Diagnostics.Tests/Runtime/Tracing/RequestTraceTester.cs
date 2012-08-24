using System;
using System.Collections.Generic;
using System.Threading;
using FubuMVC.Core.Http;
using FubuMVC.Core.Http.Headers;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Runtime.Tracing;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;
using System.Linq;

namespace FubuMVC.Diagnostics.Tests.Runtime.Tracing
{
    [TestFixture]
    public class when_marking_as_a_Failed_request : InteractionContext<RequestTrace>
    {
        [Test]
        public void mark_as_failed()
        {
            ClassUnderTest.Current = new RequestLog();

            ClassUnderTest.MarkAsFailedRequest();

            ClassUnderTest.Current.Failed.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class when_starting_a_new_request : InteractionContext<RequestTrace>
    {
        private RequestLog theLog;

        protected override void beforeEach()
        {
            theLog = new RequestLog();

            MockFor<IRequestLogBuilder>().Stub(x => x.BuildForCurrentRequest())
                .Return(theLog);

            ClassUnderTest.Stopwatch.IsRunning.ShouldBeFalse();

            ClassUnderTest.Start();
        }

        [Test]
        public void starts_a_new_request_log()
        {
            ClassUnderTest.Current.ShouldBeTheSameAs(theLog);
        }

        [Test]
        public void stores_the_new_log_with_the_cache()
        {
            MockFor<IRequestHistoryCache>().AssertWasCalled(x => x.Store(theLog));
        }

        [Test]
        public void starts_the_stopwatch()
        {
            ClassUnderTest.Stopwatch.IsRunning.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class when_logging_a_message : InteractionContext<RequestTrace>
    {
        protected override void beforeEach()
        {
            ClassUnderTest.Current = new RequestLog();

            ClassUnderTest.Stopwatch.Start();
            Thread.Sleep(10);
            ClassUnderTest.Stopwatch.Stop();

            ClassUnderTest.Log("something");
        }

        [Test]
        public void logs_to_the_current_request_log_with_the_stopwatch_time()
        {
            ClassUnderTest.Current.AllSteps().Single()
                .ShouldEqual(new RequestStep(ClassUnderTest.Stopwatch.ElapsedMilliseconds, "something"));
        }
    }

    [TestFixture]
    public class when_marking_the_current_log_as_finished : InteractionContext<RequestTrace>
    {
        private IEnumerable<Header> theHeaders;

        protected override void beforeEach()
        {
            ClassUnderTest.Current = new RequestLog();

            theHeaders = new Header[]
                              {new Header("a", "1"), new Header("b", "2")};
            MockFor<IResponse>().Stub(x => x.AllHeaders()).Return(theHeaders);

            ClassUnderTest.Stopwatch.Start();
            Thread.Sleep(10);

            ClassUnderTest.MarkFinished();
        }

        [Test]
        public void places_all_the_response_headers_onto_the_request_log()
        {
            ClassUnderTest.Current.ResponseHeaders.ShouldHaveTheSameElementsAs(theHeaders);
        }

        [Test]
        public void should_stop_the_stopwatch()
        {
            ClassUnderTest.Stopwatch.IsRunning.ShouldBeFalse();
        }

        [Test]
        public void should_mark_the_execution_time_on_the_RequestLog()
        {
            ClassUnderTest.Current.ExecutionTime.ShouldEqual(ClassUnderTest.Stopwatch.ElapsedMilliseconds);
        }
    }
}