using System;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Diagnostics.Visualization
{
    public interface IVisualizer
    {
        BehaviorNodeViewModel ToVisualizationSubject(BehaviorNode node);

        bool HasVisualizer(Type type);
        RequestStepTag VisualizeStep(RequestStep step);
    }
}