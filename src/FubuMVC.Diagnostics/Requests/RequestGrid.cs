using System;
using System.Collections.Generic;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.SlickGrid;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestGrid : GridDefinition<RequestLog>
    {
        public RequestGrid()
        {
            SourceIs<RequestSource>();

            Column(x => x.LocalTime).Title("Time (Local)");
            Column(x => x.Url);
            Column(x => x.HttpMethod).Title("Method");
            Column(x => x.HttpStatus).Title("Status");
            
            Column(x => x.ContentType).Title("Content Type");
            Column(x => x.ExecutionTime).Title("Duration (ms)");

            Data(x => x.ReportUrl);
        }
    }

    public class RequestSource : IGridDataSource<RequestLog>
    {
        private readonly IRequestHistoryCache _cache;

        public RequestSource(IRequestHistoryCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<RequestLog> GetData()
        {
            return _cache.RecentReports();
        }
    }
}