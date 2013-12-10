using FubuMVC.Core.Urls;
using FubuMVC.Diagnostics.Chains;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.TwitterBootstrap.Tags;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestLinksTag : OutlineTag
    {
        public RequestLinksTag(RequestLog log, IUrlRegistry urls)
        {
            AddHeader("Chain Details");

            Add("li").Add("a").Id("chain-summary").Attr("href", "#").Text("View Summary");
            Add("li").Add("span/a").Attr("href", urls.UrlFor(new ChainDetailsRequest{Id = log.ChainId})).Text("View Details").AddClass("external").Attr("target", "_blank");
        }
    }
}