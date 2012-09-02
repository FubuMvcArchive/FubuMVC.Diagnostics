using FubuCore;
using FubuCore.Descriptions;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.UI;
using FubuMVC.Diagnostics.Visualization;
using NUnit.Framework;
using FubuTestingSupport;

namespace FubuMVC.Diagnostics.Tests.Visualization
{
    [TestFixture]
    public class VisualizerTester
    {

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