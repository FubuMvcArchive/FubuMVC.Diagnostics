using FubuCore.Binding;
using FubuCore.Descriptions;
using FubuMVC.Diagnostics.Visualization;
using HtmlTags;

namespace FubuMVC.Diagnostics.ModelBinding
{
    public class ModelBindingEndpoints
    {
        private readonly BindingRegistry _bindingRegistry;
        private readonly IVisualizer _visualizer;

        public ModelBindingEndpoints(BindingRegistry bindingRegistry, IVisualizer visualizer)
        {
            _bindingRegistry = bindingRegistry;
            _visualizer = visualizer;
        }

        public ModelBindingExplorerViewModel get_binding_all()
        {
            var description = Description.For(_bindingRegistry);

            return new ModelBindingExplorerViewModel{
                ModelBindingGraphTag = new DescriptionBodyTag(description)
            };
        }
    }

    public class ModelBindingExplorerViewModel
    {
        public HtmlTag ModelBindingGraphTag { get; set; }
    }
}