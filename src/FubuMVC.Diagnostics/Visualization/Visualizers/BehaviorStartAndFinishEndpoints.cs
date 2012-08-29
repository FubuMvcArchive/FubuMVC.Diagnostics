using FubuCore.Descriptions;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.TwitterBootstrap.Tags;
using HtmlTags;

namespace FubuMVC.Diagnostics.Visualization.Visualizers
{
    public class BehaviorStartAndFinishEndpoints
    {
        public HtmlTag VisualizePartial(BehaviorStart start)
        {
            var description = Description.For(start.Correlation.Node);

            return new HtmlTag("div", div =>
            {
                div.Append(new LiteralTag("Starting "));
                div.Add("i").Text(description.Title);
                div.PrependGlyph("icon-chevron-down");
            });
        }

        public HtmlTag VisualizePartial(BehaviorFinish finish)
        {
            var description = Description.For(finish.Correlation.Node);

            var tag = new HtmlTag("div").AddClass("behavior-finish");

            tag.Add("span").Text("Finished ").Add("i").Text(description.Title);
            if (!finish.Succeeded)
            {
                tag.AddClass("exception");
            }

            tag.PrependGlyph("icon-chevron-up");

            return tag;
        }
    }
}