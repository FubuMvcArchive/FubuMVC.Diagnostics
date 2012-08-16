using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Urls;
using FubuMVC.SlickGrid;

namespace FubuMVC.Diagnostics.Routes
{
    public class RouteSource : IGridDataSource<RouteReport, RouteQuery>
    {
        private readonly BehaviorGraph _graph;
        private readonly IUrlRegistry _urls;

        public RouteSource(BehaviorGraph graph, IUrlRegistry urls)
        {
            _graph = graph;
            _urls = urls;
        }

        public IEnumerable<RouteReport> GetData(RouteQuery query)
        {
            return _graph.Behaviors.Select(x => new RouteReport(x, _urls));
        }
    }
}