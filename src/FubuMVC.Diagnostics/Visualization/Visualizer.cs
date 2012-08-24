using System;
using System.Linq;
using FubuCore.Descriptions;
using FubuCore.Util;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Diagnostics.Visualization
{
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

        public BehaviorNodeViewModel ToVisualizationSubject(BehaviorNode node)
        {
            var description = Description.For(node);


            return new BehaviorNodeViewModel{
                Description = description,
                Node = node,
                InputToVisualize = _hasVisualizer[node.GetType()] ? node : (object) description
            };
        }

        public object ToVisualizationSubject(object target)
        {
            return _hasVisualizer[target.GetType()] ? target : new CollapsedDescription(target);
        }
    }
}