using FubuMVC.Core.Behaviors.Chrome;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Chrome;
using FubuMVC.Diagnostics.Runtime;
using System.Linq;
using HtmlTags;

namespace FubuMVC.Diagnostics.Requests
{
    [Chrome(typeof(DashboardChrome), Title = "Request Visualization")]
    public class RequestVisualizationEndpoint
    {
        private readonly IRequestHistoryCache _cache;
        private readonly BehaviorGraph _graph;

        public RequestVisualizationEndpoint(IRequestHistoryCache cache, BehaviorGraph graph)
        {
            _cache = cache;
            _graph = graph;
        }

        public HttpRequestVisualization get_requests_Id(RequestLog request)
        {
            var log = _cache.RecentReports().FirstOrDefault(x => x.Id == request.Id);
            // TODO -- what if Log doesn't exist?

            var chain = _graph.Behaviors.FirstOrDefault(x => x.UniqueId == log.ChainId);

            return new HttpRequestVisualization(log, chain);
        }
    }

    public class BehaviorChainTraceTag : HtmlTag
    {
        public BehaviorChainTraceTag(BehaviorChain chain, RequestLog log) : base("ul")
        {

        }
    }
}