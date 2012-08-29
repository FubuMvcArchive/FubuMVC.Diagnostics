using FubuCore.Logging;
using FubuMVC.TwitterBootstrap.Tags;
using HtmlTags;

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