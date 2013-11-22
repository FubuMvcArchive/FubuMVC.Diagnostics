using System;
using System.Collections.Generic;
using FubuCore;
using FubuCore.Descriptions;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Diagnostics.Chains;
using FubuMVC.Diagnostics.Endpoints;
using FubuMVC.Diagnostics.Visualization;
using FubuMVC.TwitterBootstrap.Collapsibles;
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
                Report = new RouteReport(chain, null, null),
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
                Report = new RouteReport(chain, null, null),
                Chain = chain
            }.Title.ShouldEqual("SomeEndpoint.Go() : String");
        }

        [Test]
        public void Title_if_there_is_no_route_no_action_but_a_writer()
        {
            var chain = BehaviorChain.ForWriter(new FakeWriterNode());

            new ChainVisualization
            {
                Report = new RouteReport(chain, null, null),
                Chain = chain
            }.Title.ShouldEqual("Fake Writer");
        }

        [Test]
        public void show_nothing_for_the_route_if_there_is_nothing()
        {
            var chain = new BehaviorChain();

            var visualization = new ChainVisualization{
                Chain = chain
            };

            visualization.RouteTag.Render().ShouldBeFalse();
        }

        [Test]
        public void show_route_description_in_collapsed_body_for_a_route()
        {
            var chain = new BehaviorChain
            {
                Route = new RouteDefinition("something")
            };

            var visualization = new ChainVisualization
            {
                Chain = chain
            };

            var tag = visualization.RouteTag.As<CollapsibleTag>();

            tag.Render().ShouldBeTrue();
            tag.ToString().ShouldContain(new DescriptionBodyTag(Description.For(chain.Route)).ToString());

        }
    }

    [Title("Fake Writer")]
    public class FakeWriterNode : WriterNode
    {
        protected override ObjectDef toWriterDef()
        {
            throw new NotImplementedException();
        }

        protected override void createDescription(Description description)
        {
            
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