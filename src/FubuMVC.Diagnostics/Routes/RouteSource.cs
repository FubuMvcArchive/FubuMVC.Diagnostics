using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using FubuMVC.Diagnostics.Chains;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.SlickGrid;

namespace FubuMVC.Diagnostics.Routes
{
    public class RouteSource : IGridDataSource<RouteReport, RouteQuery>
    {
        private static readonly string Namespace = Assembly.GetExecutingAssembly().GetName().Name;

        private readonly BehaviorGraph _graph;
        private readonly IUrlRegistry _urls;

        public RouteSource(BehaviorGraph graph, IUrlRegistry urls)
        {
            _graph = graph;
            _urls = urls;
        }

        public IEnumerable<RouteReport> GetData(RouteQuery query)
        {
            return _graph.Behaviors.Where(IsNotDiagnosticRoute).Select(x => RouteReport.ForChain(x, _urls));
        }

        public static bool IsNotDiagnosticRoute(BehaviorChain chain)
        {
            if (chain.Route != null)
            {
                if (chain.Route.Pattern.Contains("_data/" + typeof(RequestLog).Name)) return false;
                if (chain.Route.Pattern.Contains("_data/" + typeof(RouteReport).Name)) return false;

                return !chain.Route.Pattern.Contains(DiagnosticsRegistration.DIAGNOSTICS_URL_ROOT);
            }

            if (chain.As<ITracedModel>().AllEvents().OfType<ChainImported>().Any(x => x.Source.Action.As<RegistryImport>().Registry is DiagnosticsRegistry))
            {
                return false;
            }


            if (chain.Calls.Any(x => x.HandlerType.Assembly == Assembly.GetExecutingAssembly()))
            {
                return false;
            }

            if (chain.Calls.Any(x => x.HasInput && x.InputType().Assembly == Assembly.GetExecutingAssembly()))
            {
                return false;
            }

            if (chain.HasOutput() && chain.ResourceType().Assembly == Assembly.GetExecutingAssembly())
            {
                return false;
            }

            if (chain.Output != null)
            {
                if (chain.Output.Writers.OfType<ViewNode>().Any(x => x.View.Namespace.Contains("Visualization.Visualizers")))
                {
                    return false;
                }
            }

            return true;
        }
    }
}