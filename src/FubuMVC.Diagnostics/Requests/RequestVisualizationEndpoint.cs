using FubuMVC.Core.Behaviors.Chrome;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Chrome;
using FubuMVC.Diagnostics.Runtime;
using System.Linq;
using HtmlTags;
using System.Collections.Generic;

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
        public BehaviorChainTraceTag(IEnumerable<BehaviorNode> chain, RequestLog log) : base("ul")
        {
            AddClasses("nav", "nav-list");
            Add("li").AddClass("nav-header").Text("Behaviors");

            chain.Where(NotDiagnosticNode).Each(node => Append(new BehaviorNodeTraceTag(node, log)));
        }

        public static bool NotDiagnosticNode(BehaviorNode node)
        {
            if (node is DiagnosticNode || node is BehaviorTracerNode) return false;

            return true;
        }
    }
}