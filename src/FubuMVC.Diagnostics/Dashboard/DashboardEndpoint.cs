using FubuMVC.Core;

namespace FubuMVC.Diagnostics.Dashboard
{
    public class DashboardEndpoint
    {
        [UrlPattern("_fubu")]
        public DashboardModel Execute(DashboardRequestModel request)
        {
            return new DashboardModel();
        }
    }
}