using System.Collections.Generic;
using System.Linq;
using Bottles;
using FubuMVC.Core;
using FubuMVC.Core.Registration;

namespace FubuMVC.Diagnostics.Model
{
    [ConfigurationType(ConfigurationType.Policy)]
    public class DiagnosticChainsPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var diagnosticGraph = graph.Settings.Get<DiagnosticGraph>();
            diagnosticGraph.Add(graph.ApplicationAssembly);

            PackageRegistry.PackageAssemblies.Each(diagnosticGraph.Add);

            diagnosticGraph.Groups().SelectMany(x => x.Chains).Each(graph.AddChain);
        }
    }
}