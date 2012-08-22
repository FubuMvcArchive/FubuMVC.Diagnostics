using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
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
        private CurrentRequest theCurrentRequest;
        private IActionBehavior theInnerBehavior;

        protected override void beforeEach()
        {
            theCurrentRequest = new CurrentRequest();
            MockFor<IFubuRequest>().Stub(x => x.Get<CurrentRequest>()).Return(theCurrentRequest);

            theInnerBehavior = MockFor<IActionBehavior>();
            ClassUnderTest.InsideBehavior = theInnerBehavior;
        }

        [Test]
        public void invoke_starts_the_request_session()
        {
            ClassUnderTest.Invoke();

            MockFor<IRequestTrace>().AssertWasCalled(x => x.Start());
        }

        [Test]
        public void invoke_partial_does_not_start_a_new_request_history()
        {
            ClassUnderTest.InvokePartial();

            MockFor<IRequestTrace>().AssertWasNotCalled(x => x.Start());
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

            var theSessionUrl = "some url";
            MockFor<IRequestTrace>().Stub(x => x.LogUrl).Return(theSessionUrl);

            ClassUnderTest.Invoke();

            MockFor<IOutputWriter>().AssertWasCalled(x => x.RedirectToUrl(theSessionUrl));
            
        }
    }
}