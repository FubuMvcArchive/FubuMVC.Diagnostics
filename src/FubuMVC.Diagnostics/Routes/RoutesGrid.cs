using FubuMVC.SlickGrid;

namespace FubuMVC.Diagnostics.Routes
{
    public class RoutesGrid : GridDefinition<RouteReport>
    {
        public RoutesGrid()
        {
            SourceIs<RouteSource>();

            
            Column(x => x.Route);
            Column(x => x.Constraints);
            Column(x => x.Action);

            Data(x => x.url);
        }
    }
}