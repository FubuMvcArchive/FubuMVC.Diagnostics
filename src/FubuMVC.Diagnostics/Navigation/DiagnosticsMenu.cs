using FubuMVC.Core.UI.Navigation;
using FubuMVC.Diagnostics.ModelBinding;
using FubuMVC.Diagnostics.Packaging;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Diagnostics.Routes;

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
        }
    }

    // TODO -- add tests
}