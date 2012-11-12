using FubuMVC.Core;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Diagnostics.Chrome;
using FubuMVC.Diagnostics.Navigation;
using FubuMVC.Navigation;

namespace FubuMVC.Diagnostics
{
    public class DiagnosticsChromeExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Policies.Add<NavigationRootPolicy>(x =>
            {
                x.ForKey(DiagnosticKeys.Main);
                x.WrapWithChrome<DashboardChrome>();

                x.Alter(chain => chain.BehaviorChain.Route.Prepend("_fubu"));
            });

            registry.Policies.Add<DiagnosticsMenu>();
        }
    }
}