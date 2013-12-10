using System;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Runtime.Logging;
using FubuMVC.Core.Urls;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Runtime.Tracing;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Diagnostics.Tests.Runtime.Tracing
{
    [TestFixture]
    public class DiagnosticBehaviorTester : InteractionContext<DiagnosticBehavior>
    {
        private IActionBehavior theInnerBehavior;
        private StubUrlRegistry theUrls;

        protected override void beforeEach()
        {
            Services.Inject<IExceptionHandlingObserver>(new ExceptionHandlingObserver());

            theUrls = new StubUrlRegistry();
            Services.Inject<IUrlRegistry>(theUrls);

            theInnerBehavior = MockFor<IActionBehavior>();
            ClassUnderTest.Inner = theInnerBehavior;
        }

        [Test]
        public void if_the_inner_behavior_fails_with_unhandled_exception_mark_the_request_as_a_failure()
        {
            var exception = new NotImplementedException();

            theInnerBehavior.Expect(x => x.Invoke()).Throw(exception);

            Exception<NotImplementedException>.ShouldBeThrownBy(() =>
            {
                ClassUnderTest.Invoke();
            }).ShouldBeTheSameAs(exception);

            var traceMock = MockFor<IRequestTrace>();
            traceMock.AssertWasCalled(x => x.MarkAsFailedRequest());
            traceMock.AssertWasCalled(x => x.Log(Arg<ExceptionReport>.Matches(
                y => y.ExceptionType == exception.GetType().Name)));
        }

        [Test]
        public void invoke_starts_the_request_session()
        {
            ClassUnderTest.Invoke();

            MockFor<IRequestTrace>().AssertWasCalled(x => x.Start());
        }


        [Test]
        public void invoke_invokes_The_inner()
        {
            ClassUnderTest.Invoke();
            theInnerBehavior.AssertWasCalled(x => x.Invoke());
        }

        [Test]
        public void invoke_partial_invokes_the_inner()
        {
            ClassUnderTest.InvokePartial();

            theInnerBehavior.AssertWasCalled(x => x.InvokePartial());
        }

        [Test]
        public void when_invoking_and_not_in_debug_mode_do_not_write_the_debug_call()
        {
            MockFor<IDebugDetector>().Stub(x => x.IsDebugCall()).Return(false);

            ClassUnderTest.Invoke();

            MockFor<IOutputWriter>().AssertWasNotCalled(x => x.RedirectToUrl(null), x => x.IgnoreArguments());
        }

        [Test]
        public void when_invoking_in_debug_mode_call_the_debug_call_handler_to_render_the_debug_screen()
        {
            MockFor<IDebugDetector>().Stub(x => x.IsDebugCall()).Return(true);

            var log = new RequestLog();

            var theSessionUrl = theUrls.UrlFor(log);
            MockFor<IRequestTrace>().Stub(x => x.Current)
                .Return(log);

            ClassUnderTest.Invoke();

            MockFor<IOutputWriter>().AssertWasCalled(x => x.RedirectToUrl(theSessionUrl));
            
        }
    }
}