using FubuCore.Binding.InMemory;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Diagnostics.Runtime;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Diagnostics.Tests
{
    [TestFixture]
    public class DiagnosticSettings_TraceLevel_Registration_Tester
    {
        BehaviorGraph verboseGraph = BehaviorGraph.BuildFrom(r =>
        {
            r.Import<DiagnosticsRegistration>();
            r.AlterSettings<DiagnosticsSettings>(x =>
            {
                x.TraceLevel = TraceLevel.Verbose;
            });
        });

        BehaviorGraph productionGraph = BehaviorGraph.BuildFrom(r =>
        {
            r.Import<DiagnosticsRegistration>();
            r.AlterSettings<DiagnosticsSettings>(x =>
            {
                x.TraceLevel = TraceLevel.Production;
            });
        });

        BehaviorGraph noneGraph = BehaviorGraph.BuildFrom(r =>
        {
            r.Import<DiagnosticsRegistration>();
            r.AlterSettings<DiagnosticsSettings>(x =>
            {
                x.TraceLevel = TraceLevel.None;
            });
        });

        [Test]
        public void RecordingBinding_logger_is_registered_if_trace_level_is_verbose()
        {
            verboseGraph.Services.DefaultServiceFor<IBindingLogger>()
                .Type.ShouldEqual(typeof (RecordingBindingLogger));

        }

        [Test]
        public void NulloBindingLogger_if_trace_level_is_none()
        {
            noneGraph.Services.DefaultServiceFor<IBindingLogger>()
                .Type.ShouldEqual(typeof (NulloBindingLogger));
        }

        [Test]
        public void NulloBindingLogger_if_trace_level_is_production()
        {
            productionGraph.Services.DefaultServiceFor<IBindingLogger>()
                .Type.ShouldEqual(typeof(NulloBindingLogger));
        }

    }
}