using System.Web;
using FubuCore.Descriptions;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.UI;
using FubuMVC.Diagnostics.Visualization;
using FubuMVC.Diagnostics.Visualization.Visualizers;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Diagnostics.Tests.Visualization.Visualizers
{
    [TestFixture]
    public class SetValueEndpointTester : InteractionContext<SetValueEndpoint>
    {
        [Test]
        public void render_when_there_is_no_visualizer_just_uses_ToString()
        {
            var something = new Something("Red");
            var report = new SetValueReport(something);

            MockFor<IVisualizer>().Stub(x => x.HasVisualizer(typeof (Something))).Return(false);

            var tag = ClassUnderTest.VisualizePartial(report);

            tag.ToString().ShouldEqual("<div><i class=\"icon-hand-right\"></i> <span>Setting value of Something in IFubuRequest to Something named Red</span></div>");
        }

        [Test]
        public void render_when_there_is_a_visualizer()
        {
            var something = new Something("Red");
            var report = new SetValueReport(something);

            MockFor<IVisualizer>().Stub(x => x.HasVisualizer(typeof(Something))).Return(true);

            MockFor<IPartialInvoker>().Stub(x => x.InvokeObject(something, false)).Return("some html");

            var tag = ClassUnderTest.VisualizePartial(report);

            tag.ToString().ShouldContain(
                "Setting value of Something in IFubuRequest");

            tag.ToString().ShouldContain("some html");
        }
    }

    [Title("Something")]
    public class Something
    {
        private readonly string _name;

        public Something(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return string.Format("Something named {0}", _name);
        }
    }
}