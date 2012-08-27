using System;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.UI;
using FubuMVC.TwitterBootstrap.Collapsibles;
using HtmlTags;
using FubuMVC.Diagnostics.Shared.Tags;
using FubuCore;

namespace FubuMVC.Diagnostics.Visualization.Visualizers
{
    public class SetValueEndpoint
    {
        private readonly IPartialInvoker _partials;
        private readonly IVisualizer _visualizer;

        public SetValueEndpoint(IPartialInvoker partials, IVisualizer visualizer)
        {
            _partials = partials;
            _visualizer = visualizer;
        }

        public HtmlTag VisualizePartial(SetValueReport report)
        {
            if (_visualizer.HasVisualizer(report.Value.GetType()))
            {
                var description = "Setting value of {0} in IFubuRequest".ToFormat(report.Type.Name);
                var tag = new CollapsibleTag("SetValueRequest", description);

                var visualization = _partials.InvokeObject(report.Value, false);
                tag.SetInnerContent(visualization);

                return tag;
            }
            else
            {
                var description = "Setting value of {0} in IFubuRequest to {1}".ToFormat(report.Type.Name, report.Value.ToString());
                return new HtmlTag("div", div =>
                {
                    div.PrependGlyph("icon-hand-right");
                    div.Add("span").Text(description);
                });
                    
                    
            }
        }
    }
}