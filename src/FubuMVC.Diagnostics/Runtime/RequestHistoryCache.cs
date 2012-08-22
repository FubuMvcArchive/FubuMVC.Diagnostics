using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;

namespace FubuMVC.Diagnostics.Runtime
{
    public class RequestHistoryCache : IRequestHistoryCache
    {
        private readonly Queue<RequestLog> _reports = new Queue<RequestLog>();
        private readonly DiagnosticsSettings _settings;

        public RequestHistoryCache(DiagnosticsSettings settings)
        {
            _settings = settings;
        }

        public void Store(RequestLog log)
        {
            _reports.Enqueue(log);
            while (_reports.Count > _settings.MaxRequests)
            {
                _reports.Dequeue();
            }
        }

        public IEnumerable<RequestLog> RecentReports()
        {
            return _reports.ToList();
        }
    }
}