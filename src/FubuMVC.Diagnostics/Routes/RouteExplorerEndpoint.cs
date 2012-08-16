namespace FubuMVC.Diagnostics.Routes
{
    public class RoutesRequest{}

    public class RouteExplorerEndpoint
    {
        public RouteExplorerModel get_routes(RoutesRequest request)
        {
            return new RouteExplorerModel();
        } 
    }
}