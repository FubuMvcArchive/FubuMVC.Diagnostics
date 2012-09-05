using FubuMVC.Core;
using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Diagnostics
{
    public class DiagnosticsRegistration : IFubuRegistryExtension
    {
        public const string DIAGNOSTICS_URL_ROOT = "_fubu";

        public void Configure(FubuRegistry registry)
        {
            registry.Policies.Add<ApplyTracing>();
            registry.Import<DiagnosticsRegistry>(DIAGNOSTICS_URL_ROOT);
        }
    }
}