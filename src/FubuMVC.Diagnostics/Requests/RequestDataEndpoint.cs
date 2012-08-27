using System;
using FubuCore.Binding.Values;
using FubuMVC.Diagnostics.Shared.Tags;
using FubuMVC.TwitterBootstrap.Collapsibles;
using HtmlTags;
using System.Collections.Generic;
using System.Linq;
using FubuCore;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestDataEndpoint
    {
        public HtmlTag RequestDataPartial(ValueReport report)
        {
            var tag = new HtmlTag("div").Id("request-data");

            var dataTags = report.Reports.Where(x => x.Values.Any()).Select(toTag);

            tag.Append(dataTags);

            return tag;
        }

        private static HtmlTag toTag(ValueSourceReport report)
        {
            var table = new DetailsTableTag();
            report.Values.OrderBy(x => x.Key).Each(pair => table.AddDetail(pair.Key, pair.Value));

            var tag = new CollapsibleTag("request-data-" + report.Name, report.Name.SplitPascalCase());
            tag.SetInnerContent(table.ToString());

            return tag;
        }
    }
}