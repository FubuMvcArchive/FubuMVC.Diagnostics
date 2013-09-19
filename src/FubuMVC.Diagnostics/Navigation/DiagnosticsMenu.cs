using FubuMVC.Diagnostics.Endpoints;
using FubuMVC.Diagnostics.ModelBinding;
using FubuMVC.Diagnostics.Packaging;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Diagnostics.Services;
using FubuMVC.Navigation;

namespace FubuMVC.Diagnostics.Navigation
{
    public class DiagnosticsMenu : NavigationRegistry
    {
        public DiagnosticsMenu()
        {
            ForMenu(DiagnosticKeys.Main);

            Add += MenuNode.ForInput<PackageDiagnosticsRequestModel>(DiagnosticKeys.ApplicationStartup);

            Add += MenuNode.ForAction<RequestsEndpoint>(DiagnosticKeys.Requests, x => x.get_requests());
            Add += MenuNode.ForInput<RoutesRequest>(DiagnosticKeys.Endpoints);

            Add += MenuNode.ForAction<ModelBindingEndpoints>(DiagnosticKeys.ModelBindingExplorer, x => x.get_binding_all());


            Add += MenuNode.Node(DiagnosticKeys.Services);
            ForMenu(DiagnosticKeys.Services);

            Add += MenuNode.ForAction<ServiceEndpoints>(DiagnosticKeys.ServicesByName, x => x.get_services_byname());
            Add += MenuNode.ForAction<ServiceEndpoints>(DiagnosticKeys.ServicesBySource, x => x.get_services_bysource());
        }
    }

    // TODO -- add tests
}