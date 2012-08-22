using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestVisualizationEndpoint
    {
        public HttpRequestVisualization get_requests_Id(RequestLog request)
        {
            return new HttpRequestVisualization();
        }
    }
}