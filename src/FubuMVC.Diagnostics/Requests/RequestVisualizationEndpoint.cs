using FubuMVC.Core.Behaviors.Chrome;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Urls;
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
        private readonly IUrlRegistry _urls;

        public RequestVisualizationEndpoint(IRequestHistoryCache cache, BehaviorGraph graph, IUrlRegistry urls)
        {
            _cache = cache;
            _graph = graph;
            _urls = urls;
        }

        public HttpRequestVisualization get_requests_Id(RequestLog request)
        {
            var log = _cache.Find(request.Id);
            
            if (log == null)
            {
                return new HttpRequestVisualization(null, null){
                    RedirectTo = FubuContinuation.RedirectTo<RequestVisualizationEndpoint>(x => x.get_requests_missing())
                };
            }

            var chain = _graph.Behaviors.FirstOrDefault(x => x.UniqueId == log.ChainId);

            return new HttpRequestVisualization(log, chain);
        }

        public HtmlTag get_requests_missing()
        {
            return new HtmlTag("p", p =>
            {
                p.Add("span").Text("This request is not longer in the request cache.  ");
                p.Add("a").Text("Return to the Request Explorer").Attr("href", _urls.UrlFor<RequestsEndpoint>());
            });
        }
    }

}