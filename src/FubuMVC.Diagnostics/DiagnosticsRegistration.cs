using FubuCore.Binding.InMemory;
using FubuCore.Logging;
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

            registry.Configure(graph => {
                var settings = graph.Settings.Get<DiagnosticsSettings>();

                if (settings.TraceLevel == TraceLevel.Verbose)
                {
                    graph.Services.Clear(typeof(IBindingLogger));
                    graph.Services.AddService<IBindingLogger, RecordingBindingLogger>();

                    graph.Services.Clear(typeof(IBindingHistory));
                    graph.Services.AddService<IBindingHistory, BindingHistory>();

                    graph.Services.AddService<ILogListener, RequestTraceListener>();
                }

                if (settings.TraceLevel == TraceLevel.Production)
                {
                    graph.Services.AddService<ILogListener, ProductionModeTraceListener>();
                }
            });
        }
    }
}