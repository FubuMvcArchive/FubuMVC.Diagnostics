using FubuMVC.Core.UI.Navigation;
using FubuMVC.Diagnostics.ModelBinding;
using FubuMVC.Diagnostics.Packaging;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Diagnostics.Routes;
using FubuMVC.Diagnostics.Services;

namespace FubuMVC.Diagnostics.Navigation
{
    public class DiagnosticsMenu : NavigationRegistry
    {
        public DiagnosticsMenu()
        {
            ForMenu(DiagnosticKeys.Main);

            Add += MenuNode.ForInput<PackageDiagnosticsRequestModel>(DiagnosticKeys.ApplicationStartup);

            Add += MenuNode.ForAction<RequestsEndpoint>(DiagnosticKeys.Requests, x => x.get_requests());
            Add += MenuNode.ForInput<RoutesRequest>(DiagnosticKeys.Routes);

            Add += MenuNode.ForAction<ModelBindingEndpoints>(DiagnosticKeys.ModelBindingExplorer, x => x.get_binding_all());


            Add += MenuNode.Node(DiagnosticKeys.Services);
            ForMenu(DiagnosticKeys.Services);

            Add += MenuNode.ForAction<ServiceEndpoints>(DiagnosticKeys.ServicesByName, x => x.get_services_byname());
            Add += MenuNode.ForAction<ServiceEndpoints>(DiagnosticKeys.ServicesBySource, x => x.get_services_bysource());
        }
    }

    // TODO -- add tests
}