using FubuCore.Descriptions;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Visualization;
using NUnit.Framework;
using FubuTestingSupport;

namespace FubuMVC.Diagnostics.Tests.Visualization
{
    [TestFixture]
    public class VisualizerTester
    {
        [Test]
        public void use_The_object_itself_if_there_is_a_chain_with_that_type()
        {
            var graph = BehaviorGraph.BuildFrom(x =>
            {
                x.Actions.IncludeType<FakeEndpoint>();
            });

            var visualizer = new Visualizer(graph);

            var fakeInput = new FakeInput();
            visualizer.ToVisualizationSubject(fakeInput).ShouldBeTheSameAs(fakeInput);
        }

        [Test]
        public void use_a_Description_object_if_there_is_no_chain_for_an_object()
        {
            var graph = BehaviorGraph.BuildEmptyGraph();

            var visualizer = new Visualizer(graph);
            var fakeInput = new ThingWithNoVisualizer();
            visualizer.ToVisualizationSubject(fakeInput).ShouldBeOfType<CollapsedDescription>();
        }

        public class ThingWithNoVisualizer{}

        public class FakeInput
        {
            
        }

        public class FakeEndpoint
        {
            public string Visualize(FakeInput input)
            {
                return "something";
            }
        }
    }
}