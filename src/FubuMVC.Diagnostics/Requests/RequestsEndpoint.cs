using HtmlTags;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestsEndpoint
    {
        public RequestsViewModel get_requests()
        {
            return new RequestsViewModel();
        }
    }

    public class RequestsViewModel{}
}