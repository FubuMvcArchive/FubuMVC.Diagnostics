using FubuMVC.Diagnostics.Runtime;
using System.Linq;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestVisualizationEndpoint
    {
        private readonly IRequestHistoryCache _cache;

        public RequestVisualizationEndpoint(IRequestHistoryCache cache)
        {
            _cache = cache;
        }

        public HttpRequestVisualization get_requests_Id(RequestLog request)
        {
            var log = _cache.RecentReports().FirstOrDefault(x => x.Id == request.Id);

            return new HttpRequestVisualization(log);
        }
    }
}