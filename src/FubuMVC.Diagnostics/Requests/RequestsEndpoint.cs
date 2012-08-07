using HtmlTags;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestsEndpoint
    {
        public HtmlTag get_requests(RequestsQuery request)
        {
            return new HtmlTag("H1").Text("Request Explorer (Forthcoming)");
        }
    }

    public class RequestsQuery { }
}