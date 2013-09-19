using System.Linq;
using System.Reflection;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Diagnostics.Endpoints
{
    public class RoutesRequest{}

    public class EndpointExplorerEndpoint
    {
        private static readonly string Namespace = Assembly.GetExecutingAssembly().GetName().Name;

        private readonly BehaviorGraph _graph;
        private readonly IUrlRegistry _urls;

        public EndpointExplorerEndpoint(BehaviorGraph graph, IUrlRegistry urls)
        {
            _graph = graph;
            _urls = urls;
        }




        public EndpointExplorerModel get_endpoints(RoutesRequest request)
        {
            var reports = _graph.Behaviors.Where(IsNotDiagnosticRoute).Select(x => RouteReport.ForChain(x, _urls)).OrderBy(x => x.Route);

            return new EndpointExplorerModel
            {
                EndpointsTable = new EndpointsTable(reports)
            };
        }

        public static bool IsNotDiagnosticRoute(BehaviorChain chain)
        {
            if (chain.Route != null)
            {
                if (chain.Route.Pattern.Contains("_data/" + typeof(RequestLog).Name)) return false;
                if (chain.Route.Pattern.Contains("_data/" + typeof(RouteReport).Name)) return false;

                return !chain.Route.Pattern.Contains(DiagnosticsRegistration.DIAGNOSTICS_URL_ROOT);
            }

            // TODO -- figure out how to do this again
            //            if (chain.As<ITracedModel>().AllEvents().OfType<ChainImported>().Any(x => x.Source.ProvenanceChain.OfType<ServiceRegistryProvenance>().Any(x => x.))
            //            {
            //                return false;
            //            }


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

            if (chain.HasOutput())
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