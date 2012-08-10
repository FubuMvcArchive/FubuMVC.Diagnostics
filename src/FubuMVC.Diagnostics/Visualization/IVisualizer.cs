using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using FubuCore.Descriptions;
using FubuCore.Util;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Querying;
using FubuMVC.Core.View;
using HtmlTags;
using FubuMVC.TwitterBootstrap;
using System.Linq;

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

    public class BehaviorNodeViewModel
    {
        public BehaviorNode Node { get; set; }
        public Description Description { get; set; }
        public object InputToVisualize { get; set; }
    }

    public interface IVisualizer
    {
        BehaviorNodeViewModel Visualize(BehaviorNode node);
    }

    public class Visualizer : IVisualizer
    {
        private readonly FubuCore.Util.Cache<Type, bool> _hasVisualizer;

        public Visualizer(BehaviorGraph graph)
        {
            _hasVisualizer = new Cache<Type, bool>(type =>
            {
                return graph.Behaviors.Any(x => type == x.InputType());
            });
        }

        public BehaviorNodeViewModel Visualize(BehaviorNode node)
        {
            var description = Description.For(node);


            return new BehaviorNodeViewModel{
                Description = description,
                Node = node,
                InputToVisualize = _hasVisualizer[node.GetType()] ? node : (object) description
            };
        }
    }
}