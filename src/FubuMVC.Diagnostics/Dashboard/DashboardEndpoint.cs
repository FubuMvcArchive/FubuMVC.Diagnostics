using FubuMVC.Core;
using FubuMVC.Core.Behaviors.Chrome;
using FubuMVC.Diagnostics.Chrome;
using HtmlTags;

namespace FubuMVC.Diagnostics.Dashboard
{
    public class DashboardEndpoint
    {
        [UrlPattern("_fubu"), Chrome(typeof(DashboardChrome))]
        public DashboardModel get_fubu(DashboardRequestModel request)
        {
            return new DashboardModel();
        }
    }
}