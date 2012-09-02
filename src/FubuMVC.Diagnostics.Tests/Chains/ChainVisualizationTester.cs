using System;
using System.Collections.Generic;
using FubuCore.Descriptions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Diagnostics.Chains;
using FubuMVC.Diagnostics.Routes;
using NUnit.Framework;
using FubuTestingSupport;

namespace FubuMVC.Diagnostics.Tests.Chains
{
    [TestFixture]
    public class ChainVisualizationTester
    {
        [Test]
        public void Title_if_there_is_a_route()
        {
            var chain = new BehaviorChain{
                Route = new RouteDefinition("some/pattern")
            };
            var visualization = new ChainVisualization()
            {
                Report = new RouteReport(chain, null),
                Chain = chain
            };

            visualization.Title.ShouldEqual("some/pattern");
        }

        [Test]
        public void Title_if_there_is_no_route_partial_with_controller_action()
        {
            var chain = new BehaviorChain();
            chain.IsPartialOnly = true;
            chain.AddToEnd(ActionCall.For<SomeEndpoint>(x => x.Go()));

            new ChainVisualization{
                Report = new RouteReport(chain, null),
                Chain = chain
            }.Title.ShouldEqual("SomeEndpoint.Go() : String");
        }

        [Test]
        public void Title_if_there_is_no_route_no_action_but_a_writer()
        {
            var chain = BehaviorChain.ForWriter(new FakeWriterNode());

            new ChainVisualization
            {
                Report = new RouteReport(chain, null),
                Chain = chain
            }.Title.ShouldEqual("Fake Writer");
        }
    }

    [Title("Fake Writer")]
    public class FakeWriterNode : WriterNode
    {
        protected override ObjectDef toWriterDef()
        {
            throw new NotImplementedException();
        }

        public override Type ResourceType
        {
            get { return typeof(string); }
        }

        public override IEnumerable<string> Mimetypes
        {
            get { yield break; }
        }
    }

    public class SomeEndpoint
    {
        public string Go()
        {
            return "Hello.";
        }

        public string GoElse()
        {
            return "Hello.";
        }
    }
}