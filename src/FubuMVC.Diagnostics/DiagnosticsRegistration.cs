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
            registry.Policies.Add<DiagnosticChainsPolicy>();
            registry.Services<DiagnosticServiceRegistry>();
        }
    }
}