using FubuMVC.Diagnostics.Runtime;
using System.Linq;
using System.Collections.Generic;
using FubuCore;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestLogDataOutlineTag : OutlineTag
    {
        public RequestLogDataOutlineTag(RequestLog log)
        {
            AddHeader("Data");

            log.RequestData.Reports.Where(x => x.Values.Any()).Each(report =>
            {
                AddNode(report.Header(), report.ElementId());
            });


            if (log.ResponseHeaders.Any())
            {
                AddNode(ResponseHeadersTag.Heading, ResponseHeadersTag.ElementId);
            }
        }
    }
}