using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Diagnostics.Visualization
{
    public interface IVisualizer
    {
        BehaviorNodeViewModel ToVisualizationSubject(BehaviorNode node);

        object ToVisualizationSubject(object target);
    }
}