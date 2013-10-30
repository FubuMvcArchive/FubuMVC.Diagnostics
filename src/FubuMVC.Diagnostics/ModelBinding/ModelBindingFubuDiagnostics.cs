using FubuCore.Binding;
using FubuCore.Descriptions;
using FubuMVC.Core;
using FubuMVC.Diagnostics.Visualization;
using HtmlTags;

namespace FubuMVC.Diagnostics.ModelBinding
{
    public class ModelBindingFubuDiagnostics
    {
        private readonly BindingRegistry _bindingRegistry;

        public ModelBindingFubuDiagnostics(BindingRegistry bindingRegistry)
        {
            _bindingRegistry = bindingRegistry;
        }

        [UrlPattern("binding/all")]
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