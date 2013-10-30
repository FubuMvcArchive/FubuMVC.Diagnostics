using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bottles;
using FubuCore.Util;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

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

    [ApplicationLevel]
    public class DiagnosticGraph
    {
        private readonly Cache<string, DiagnosticGroup> _groups = new Cache<string, DiagnosticGroup>();

        public void Add(Assembly assembly)
        {
            IEnumerable<ActionCall> calls = DiagnosticGroup.FindCalls(assembly);
            if (calls.Any())
            {
                var group = new DiagnosticGroup(assembly, calls);
                _groups[group.Name] = group;
            }
        }

        public DiagnosticGroup FindGroup(string name)
        {
            return _groups[name];
        }

        public IEnumerable<DiagnosticGroup> Groups()
        {
            return _groups;
        }
    }
}