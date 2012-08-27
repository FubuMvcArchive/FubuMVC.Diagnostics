using FubuCore.Logging;
using HtmlTags;
using FubuMVC.Diagnostics.Shared.Tags;

namespace FubuMVC.Diagnostics.Visualization.Visualizers
{
    public class StringMessageEndpoint
    {
        public HtmlTag VisualizePartial(StringMessage message)
        {
            return new HtmlTag("div", div =>
            {
                div.AddClass("comment");
                div.PrependGlyph("icon-comment");
                div.Add("span").Text(message.Message);
            });
        }
    }
}