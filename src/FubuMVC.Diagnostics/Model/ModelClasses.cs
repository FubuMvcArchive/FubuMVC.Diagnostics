using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.Caching;
using FubuCore;
using FubuCore.Util;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Diagnostics.Model
{
    public class DiagnosticGraph
    {
        public static readonly string DiagnosticsSuffix = "FubuDiagnostics";
        private readonly Cache<string, DiagnosticGroup> _groups = new Cache<string, DiagnosticGroup>();

        public void Add(Assembly assembly)
        {
            var source = new ActionSource();
            source.Applies.ToAssembly(assembly);
            source.IncludeTypesNamed(x => x.EndsWith(DiagnosticsSuffix));

            var calls = source.As<IActionSource>().FindActions(null);
            if (calls.Any())
            {
                var group = new DiagnosticGroup(assembly, calls);
                _groups[group.Name] = group;
            }

            // might do nothing
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

    public class DiagnosticGroup
    {
        private readonly IList<DiagnosticChain> _chains = new List<DiagnosticChain>(); 

        public DiagnosticGroup(Assembly assembly, IEnumerable<ActionCall> calls)
        {
            Title = assembly.GetName().Name;
            Url = Title.ToLower();


            try
            {
                var type = assembly.GetExportedTypes()
                    .Where(x => x.IsConcreteWithDefaultCtor() && x.Name == "FubuDiagnosticsConfiguration")
                    .FirstOrDefault();

                if (type != null)
                {
                    var configuration = Activator.CreateInstance(type);
                    Title = tryGet(configuration, "Title") ?? Title;
                    Description = tryGet(configuration, "Description") ?? Description;
                    Url = tryGet(configuration, "Url") ?? Url;

                }
            }
            catch (Exception)
            {
                // just ignore it.  Too many problems happen w/ the type scanning
            }
        }

        private string tryGet(object configuration, string fieldName)
        {
            var field = configuration.GetType().GetField(fieldName);
            if (field != null)
            {
                return field.GetValue(configuration) as string;
            }

            return null;
        }

        // Look for "Index" first
        public DiagnosticChain Default()
        {
            throw new NotImplementedException();
        }

        // Only GET's w/ no arguments
        public IEnumerable<DiagnosticChain> Links()
        {
            return _chains.Where(x => x.IsLink()).OrderBy(x => x.Title);
        } 

        public string Url { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}