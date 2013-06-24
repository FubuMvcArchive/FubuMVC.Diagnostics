using FubuMVC.Diagnostics.Runtime;
using HtmlTags;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestsEndpoint
    {
        private readonly IRequestHistoryCache _cache;

        public RequestsEndpoint(IRequestHistoryCache cache)
        {
            _cache = cache;
        }

        public RequestsViewModel get_requests()
        {
            return new RequestsViewModel
            {
                Table = new RequestTable(_cache.RecentReports())
            };
        }
    }

    public class RequestsViewModel{
        public RequestTable Table { get; set; }
    }
}