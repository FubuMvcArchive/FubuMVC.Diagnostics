using System.Collections.Generic;
using System.Text;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.View;
using FubuMVC.TwitterBootstrap;

namespace FubuMVC.Diagnostics.Visualization
{
    public static class NodeVisualizationPageExtensions
    {
        public static string Visualize(this IFubuPage page, BehaviorNode node)
        {
            var visualizer = page.Get<Visualizer>(); // TODO -- change this
            var model = visualizer.Visualize(node);

            return page.CollapsiblePartialFor(model).Title(model.Description.Title).ToString();
        }

        public static string Visualize(this IFubuPage page, IEnumerable<BehaviorNode> nodes)
        {
            var builder = new StringBuilder();

            nodes.Each(x => builder.Append(page.Visualize(x)));

            return builder.ToString();
        }
    }
}