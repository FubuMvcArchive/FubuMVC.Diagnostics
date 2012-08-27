using System.Collections.Generic;
using System.Linq;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Shared.Tags;
using FubuMVC.TwitterBootstrap.Collapsibles;

namespace FubuMVC.Diagnostics.Requests
{
    public class ResponseHeadersTag : CollapsibleTag
    {
        public static readonly string ElementId = "response-headers";
        public static readonly string Heading = "Response Headers";

        public ResponseHeadersTag(RequestLog log) : base(ElementId, Heading)
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