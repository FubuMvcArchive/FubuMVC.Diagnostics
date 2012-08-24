using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Diagnostics.Visualization
{
    public interface IVisualizer
    {
        BehaviorNodeViewModel Visualize(BehaviorNode node);
    }
}