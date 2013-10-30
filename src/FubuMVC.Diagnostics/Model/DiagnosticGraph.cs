using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuCore.Util;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Diagnostics.Model
{
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