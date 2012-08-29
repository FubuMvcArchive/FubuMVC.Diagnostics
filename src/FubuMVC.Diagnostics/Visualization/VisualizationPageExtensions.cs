using System.Collections.Generic;
using System.Linq;
using System.Text;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.UI;
using FubuMVC.Core.View;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.TwitterBootstrap;
using FubuMVC.TwitterBootstrap.Tags;
using HtmlTags;

namespace FubuMVC.Diagnostics.Visualization
{
    public static class VisualizationPageExtensions
    {
        public static string Visualize(this IFubuPage page, BehaviorNode node)
        {
            var visualizer = page.Get<Visualizer>(); // TODO -- change this
            var model = visualizer.ToVisualizationSubject(node);

            return page.CollapsiblePartialFor(model).Title(model.Description.Title).ToString();
        }

        public static string Visualize(this IFubuPage page, IEnumerable<BehaviorNode> nodes)
        {
            var builder = new StringBuilder();

            nodes.Each(x => builder.Append(page.Visualize(x)));

            return builder.ToString();
        }

        public static TagList Visualize(this IFubuPage page, IEnumerable<RequestStep> steps)
        {
            var visualizer = page.Get<IVisualizer>();

            var tags = steps.Select(step =>
            {
                var input = visualizer.ToVisualizationSubject(step.Log);
                var content = page.PartialFor(input);

                return new RequestStepTag(step, content.ToString()).PrependAnchor();
            });

            return new TagList(tags);
        }
    }
}