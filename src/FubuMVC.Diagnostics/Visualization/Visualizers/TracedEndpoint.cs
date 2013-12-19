using FubuMVC.Core;
using FubuMVC.Core.Registration.Diagnostics;
using HtmlTags;

namespace FubuMVC.Diagnostics.Visualization.Visualizers
{
    public class TracedEndpoint
    {
        [FubuPartial]
        public HtmlTag VisualizeTraced(Traced traced)
        {
            return new CommentTag(traced.Text);
        }
    }
}