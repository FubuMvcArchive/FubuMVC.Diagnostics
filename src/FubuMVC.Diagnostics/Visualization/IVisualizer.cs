using System;
using FubuCore.Descriptions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Diagnostics.Runtime;
using HtmlTags;

namespace FubuMVC.Diagnostics.Visualization
{
    public interface IVisualizer
    {
        BehaviorNodeViewModel ToVisualizationSubject(BehaviorNode node);

        bool HasVisualizer(Type type);
        RequestStepTag VisualizeStep(RequestStep step);
        HtmlTag VisualizeDescription(Description description, bool ellided = true);
    }
}