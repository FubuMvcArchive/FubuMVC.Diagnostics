using FubuCore.Descriptions;
using FubuMVC.Diagnostics.Runtime;
using HtmlTags;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestsFubuDiagnostics
    {
        private readonly IRequestHistoryCache _cache;

        public RequestsFubuDiagnostics(IRequestHistoryCache cache)
        {
            _cache = cache;
        }

        [System.ComponentModel.Description("Requests:Request visualization for the most recent requests")]
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