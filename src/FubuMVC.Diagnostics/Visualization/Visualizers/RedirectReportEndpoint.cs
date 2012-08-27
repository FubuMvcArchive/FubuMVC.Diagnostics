using FubuMVC.Core.Runtime.Logging;
using HtmlTags;
using FubuMVC.Diagnostics.Shared.Tags;

namespace FubuMVC.Diagnostics.Visualization.Visualizers
{
    public class RedirectReportEndpoint
    {
        public HtmlTag Visualize(RedirectReport report)
        {
            return new HtmlTag("div", div =>
            {
                div.PrependGlyph("icon-info-sign");
                div.Add("span").Text("Redirected the browser to " + report.Url);
            });
        }
    }
}