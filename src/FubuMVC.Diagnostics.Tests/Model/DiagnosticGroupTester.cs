using System.Reflection;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Model;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Diagnostics.Tests.Model
{
    [TestFixture]
    public class DiagnosticGroupTester
    {
        private DiagnosticGroup theGroup = new DiagnosticGroup(Assembly.GetExecutingAssembly(), new ActionCall[0]);

        [Test]
        public void should_pick_up_the_url_from_the_FubuDiagnosticsConfiguration_class_in_that_assembly()
        {
            theGroup.Url.ShouldEqual("fake");
        }

        [Test]
        public void should_pick_up_the_title_from_the_configuration_type_in_the_assembly()
        {
            theGroup.Title.ShouldEqual(new FubuDiagnosticsConfiguration().Title);
        }

        [Test]
        public void should_pick_up_the_description_from_the_configuration_type_in_the_assembly()
        {
            theGroup.Description.ShouldEqual(new FubuDiagnosticsConfiguration().Description);
        }

        [Test]
        public void build_a_group_for_an_assembly_with_no_diagnostics_configuration()
        {
            var group = new DiagnosticGroup(typeof (TestFixtureAttribute).Assembly, new ActionCall[0]);

            group.Url.ShouldEqual("nunit.framework");
            group.Title.ShouldEqual("nunit.framework");
            group.Description.ShouldBeNull();
        }

    }

    public class FubuDiagnosticsConfiguration
    {
        public string Url = "fake";
        public string Title = "Fake Diagnostics";
        public string Description = "Some fake diagnostics";
    }
}