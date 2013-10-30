using FubuMVC.Core;
using FubuMVC.Diagnostics.Model;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Visualization;

namespace FubuMVC.Diagnostics
{
    public class DiagnosticsRegistration : IFubuRegistryExtension
    {
        public const string DIAGNOSTICS_URL_ROOT = "_fubu";

        public void Configure(FubuRegistry registry)
        {
            registry.Policies.Add<ApplyTracing>();
			registry.Policies.Add<DescriptionVisualizationPolicy>();
            registry.Import<DiagnosticsRegistry>(DIAGNOSTICS_URL_ROOT);
            registry.Policies.Add<DiagnosticChainsPolicy>();
        }
    }
}