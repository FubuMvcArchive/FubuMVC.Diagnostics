using System;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Diagnostics.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Diagnostics.Tests.Requests
{
    [TestFixture]
    public class RequestVisualizationEndpointTester : InteractionContext<RequestVisualizationEndpoint>
    {
        [Test]
        public void when_the_request_log_cannot_be_found_it_should_redirect()
        {
            var request = new RequestLog();
            MockFor<IRequestHistoryCache>().Stub(x => x.Find(request.Id)).Return(null);

            ClassUnderTest.get_requests_Id(request)
                .RedirectTo.AssertWasRedirectedTo<RequestVisualizationEndpoint>(x => x.get_requests_missing());
        }

        [Test]
        public void when_the_request_log_can_be_found()
        {
            var graph = new BehaviorGraph();
            var chain = new BehaviorChain();
            graph.AddChain(chain);

            Services.Inject(graph);
            
            var request = new RequestLog();
            var actualLog = new RequestLog();
            actualLog.ChainId = chain.UniqueId;

            MockFor<IRequestHistoryCache>().Stub(x => x.Find(request.Id)).Return(actualLog);

            var visualization = ClassUnderTest.get_requests_Id(request);

            visualization.RedirectTo.ShouldBeNull();

            visualization.Log.ShouldBeTheSameAs(actualLog);
            visualization.Chain.ShouldBeTheSameAs(chain);
        }
    }
}