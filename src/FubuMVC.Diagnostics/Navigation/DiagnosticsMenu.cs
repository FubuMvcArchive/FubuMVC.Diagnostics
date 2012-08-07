using FubuMVC.Core.UI.Navigation;
using FubuMVC.Diagnostics.Dashboard;
using FubuMVC.Diagnostics.Features.Packaging;
using FubuMVC.Diagnostics.Requests;

namespace FubuMVC.Diagnostics.Navigation
{
    public class DiagnosticsMenu : NavigationRegistry
    {
        public DiagnosticsMenu()
        {
            ForMenu(DiagnosticKeys.Main);
            //Add += MenuNode.ForInput<DashboardRequestModel>(DiagnosticKeys.Dashboard);
            //Add += MenuNode.ForInput<HtmlConventionsRequestModel>(DiagnosticKeys.HtmlConventions);
            Add += MenuNode.ForInput<PackageDiagnosticsRequestModel>(DiagnosticKeys.ApplicationStartup);

            Add += MenuNode.ForInput<RequestsQuery>(DiagnosticKeys.Requests);
            Add += MenuNode.ForInput<RoutesRequest>(DiagnosticKeys.Routes);
        }
    }

    // TODO -- add tests
}