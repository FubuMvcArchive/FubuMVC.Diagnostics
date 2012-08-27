using System.Collections.Generic;
using System.Linq;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Shared.Tags;
using FubuMVC.TwitterBootstrap.Collapsibles;

namespace FubuMVC.Diagnostics.Requests
{
    public class ResponseHeadersTag : CollapsibleTag
    {
        public ResponseHeadersTag(RequestLog log)
            : base("response-headers", "Response Headers")
        {
            if (!log.ResponseHeaders.Any())
            {
                Render(false);
                return;
            }

            var detailsTag = new DetailsTableTag();
            log.ResponseHeaders.OrderBy(x => x.Name).Each(header => detailsTag.AddDetail(header.Name, header.Value));

            SetInnerContent(detailsTag.ToString());
        }
    }
}