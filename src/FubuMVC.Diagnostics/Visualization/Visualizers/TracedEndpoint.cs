using FubuMVC.Core.Registration.Diagnostics;
using HtmlTags;

namespace FubuMVC.Diagnostics.Visualization.Visualizers
{
    public class TracedEndpoint
    {
        public HtmlTag VisualizeTraced(Traced traced)
        {
            return new CommentTag(traced.Text);
        }
    }
}