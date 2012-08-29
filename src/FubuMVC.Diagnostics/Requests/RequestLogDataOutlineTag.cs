using System.Collections.Generic;
using System.Linq;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.TwitterBootstrap.Tags;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestLogDataOutlineTag : OutlineTag
    {
        public RequestLogDataOutlineTag(RequestLog log)
        {
            AddHeader("Data");

            log.RequestData.Reports.Where(x => x.Values.Any()).Each(
                report =>
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