using System.Linq;
using System.Reflection;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Diagnostics.Model;
using FubuMVC.StructureMap;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Diagnostics.Tests.Model
{
    [TestFixture]
    public class Diagnostic_Chain_Scanning_Integrated_Tester
    {
        private readonly BehaviorGraph theGraph = FubuApplication.DefaultPolicies()
            .StructureMap().Bootstrap().Factory.Get<BehaviorGraph>();

        [Test]
        public void has_the_diagnostic_chains_from_the_application_assembly()
        {
            var group = theGraph.Settings
                .Get<DiagnosticGraph>()
                .FindGroup(Assembly.GetExecutingAssembly().GetName().Name);

            group.Links().Select(x => x.GetRoutePattern())
                .ShouldHaveTheSameElementsAs("_fubu/fake/link", "_fubu/fake/else", "_fubu/fake", "_fubu/fake/simple");
            
        }

        [Test]
        public void has_the_diagnostic_group_for_FubuMVC_Diagnostics_itself()
        {
            var group = theGraph.Settings
                .Get<DiagnosticGraph>()
                .FindGroup(typeof(DiagnosticGraph).Assembly.GetName().Name);

            group.Links().Select(x => x.GetRoutePattern())
                .ShouldHaveTheSameElementsAs("_fubu/binding/all", "_fubu/endpoints", "_fubu", "_fubu/package/logs", "_fubu/requests");
        }
    }
}