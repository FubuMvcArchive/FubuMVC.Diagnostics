using FubuMVC.Diagnostics.Runtime;
using FubuMVC.TwitterBootstrap.Tags;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestLinksTag : OutlineTag
    {
        public RequestLinksTag(RequestLog log)
        {
            AddHeader("Chain Details");

            Add("li").Add("a").Id("chain-summary").Attr("href", "#").Text("View Summary");
            Add("li").Add("span/a").Attr("href", log.DetailsUrl).Text("View Details").AddClass("external").Attr("target", "_blank");
        }
    }
}