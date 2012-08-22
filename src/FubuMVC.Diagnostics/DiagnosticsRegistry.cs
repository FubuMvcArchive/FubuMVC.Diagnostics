using FubuCore.Binding.InMemory;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Security;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Runtime.Tracing;

namespace FubuMVC.Diagnostics
{
    public class DiagnosticsRegistry : FubuRegistry
    {
        public DiagnosticsRegistry()
        {
            Services<DiagnosticServiceRegistry>();

            // TODO -- make all of this unnecessary
            Actions.IncludeClassesSuffixedWithEndpoint();


            Views
                .TryToAttachWithDefaultConventions();
        }
    }

    public class DiagnosticServiceRegistry : ServiceRegistry
    {
        public DiagnosticServiceRegistry()
        {
            SetServiceIfNone<IBindingLogger, RecordingBindingLogger>();
            SetServiceIfNone<IDebugDetector, DebugDetector>();
            ReplaceService<IDebugReport, DebugReport>();
            ReplaceService<IDebugDetector, DebugDetector>();
            ReplaceService<IAuthorizationPolicyExecutor, RecordingAuthorizationPolicyExecutor>();
            ReplaceService<IBindingHistory, BindingHistory>();
            SetServiceIfNone<IRequestHistoryCache, RequestHistoryCache>();
        }
    }
}