using FubuMVC.Diagnostics.Endpoints;
using FubuMVC.Diagnostics.ModelBinding;
using FubuMVC.Diagnostics.Packaging;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Navigation;

namespace FubuMVC.Diagnostics.Navigation
{
    public class DiagnosticsMenu : NavigationRegistry
    {
        public DiagnosticsMenu()
        {
            ForMenu(DiagnosticKeys.Main);

            Add += MenuNode.ForInput<PackageDiagnosticsRequestModel>(DiagnosticKeys.ApplicationStartup);

            Add += MenuNode.ForAction<RequestsFubuDiagnostics>(DiagnosticKeys.Requests, x => x.get_requests());
            Add += MenuNode.ForInput<RoutesRequest>(DiagnosticKeys.Endpoints);

            Add += MenuNode.ForAction<ModelBindingFubuDiagnostics>(DiagnosticKeys.ModelBindingExplorer, x => x.get_binding_all());


            Add += MenuNode.Node(DiagnosticKeys.Services);
            ForMenu(DiagnosticKeys.Services);
        }
    }

    // TODO -- add tests
}