using FubuMVC.Core;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Diagnostics.Chrome;
using FubuMVC.Diagnostics.Navigation;

namespace FubuMVC.Diagnostics
{
    public class DiagnosticsChromeExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.ApplyConvention<NavigationRootPolicy>(x =>
            {
                x.ForKey(DiagnosticKeys.Main);
                x.WrapWithChrome<DashboardChrome>();

                x.Alter(chain => chain.Route.Prepend("_fubu"));
            });

            registry.Navigation<DiagnosticsMenu>();
        }
    }
}