using FubuCore.Binding.InMemory;
using FubuCore.Logging;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Diagnostics.Model;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Runtime.Tracing;
using FubuMVC.Diagnostics.Visualization;

namespace FubuMVC.Diagnostics
{
    public class DiagnosticsRegistry : FubuPackageRegistry
    {
        // default policies are good enough
    }

    public class DiagnosticServiceRegistry : ServiceRegistry
    {
        public DiagnosticServiceRegistry()
        {
            SetServiceIfNone<IBindingLogger, RecordingBindingLogger>();
            SetServiceIfNone<IDebugDetector, DebugDetector>();
            ReplaceService<IDebugDetector, DebugDetector>();
            ReplaceService<IBindingHistory, BindingHistory>();
            SetServiceIfNone<IRequestHistoryCache, RequestHistoryCache>();

            AddService<IRequestTraceObserver, RequestHistoryObserver>();
            AddService<ILogListener, RequestTraceListener>();

            SetServiceIfNone<IRequestTrace, RequestTrace>();
            SetServiceIfNone<IRequestLogBuilder, RequestLogBuilder>();

            SetServiceIfNone<IVisualizer, Visualizer>();
            SetServiceIfNone<IDiagnosticContext, DiagnosticContext>();
        }
    }
}