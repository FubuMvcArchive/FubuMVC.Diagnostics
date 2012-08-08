using FubuCore.Binding.InMemory;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Security;
using FubuMVC.Diagnostics.Chrome;
using FubuMVC.Diagnostics.Core.Infrastructure;
using FubuMVC.Diagnostics.Features.Html.Preview;
using FubuMVC.Diagnostics.Features.Html.Preview.Decorators;
using FubuMVC.Diagnostics.Navigation;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Runtime.Assets;
using FubuMVC.Diagnostics.Runtime.Tracing;

namespace FubuMVC.Diagnostics
{
    public class DiagnosticsRegistry : FubuRegistry
    {
        public DiagnosticsRegistry()
        {
            setupDiagnosticServices();

            
            Actions.IncludeClassesSuffixedWithEndpoint();

            //Actions
            //    .IncludeType<BasicAssetDiagnostics>();

            Views
                .TryToAttachWithDefaultConventions()
                .RegisterActionLessViews(x => x.ViewModel == typeof(DashboardChrome)); // TODO -- hate this.  Make it unnecessary.

            Navigation<DiagnosticsMenu>();

            ApplyConvention<NavigationRootPolicy>(x =>
            {
                x.ForKey(DiagnosticKeys.Main);
                x.WrapWithChrome<DashboardChrome>();

                x.Alter(chain => chain.Route.Prepend("_fubu"));
            });
        }

        private void setupDiagnosticServices()
        {
            Services(x =>
            {
                x.SetServiceIfNone<IBindingLogger, RecordingBindingLogger>();
                x.SetServiceIfNone<IDebugDetector, DebugDetector>();
                x.SetServiceIfNone<IDebugCallHandler, DebugCallHandler>();
                x.ReplaceService<IDebugReport, DebugReport>();
                x.ReplaceService<IDebugDetector, DebugDetector>();
                x.ReplaceService<IAuthorizationPolicyExecutor, RecordingAuthorizationPolicyExecutor>();
                x.ReplaceService<IBindingHistory, BindingHistory>();
                x.SetServiceIfNone<IRequestHistoryCache, RequestHistoryCache>();

                x.SetServiceIfNone<IPreviewModelActivator, PreviewModelActivator>();
                x.SetServiceIfNone<IPreviewModelTypeResolver, PreviewModelTypeResolver>();
                x.SetServiceIfNone<IPropertySourceGenerator, PropertySourceGenerator>();
                x.SetServiceIfNone<IModelPopulator, ModelPopulator>();
                x.SetServiceIfNone<ITagGeneratorFactory, TagGeneratorFactory>();

                x.SetServiceIfNone<IHtmlConventionsPreviewContextFactory, HtmlConventionsPreviewContextFactory>();

                x.ReplaceService<IDebugCallHandler, DiagnosticsDebugCallHandler>();

                x.Scan(scan =>
                {
                    scan
                        .Applies
                        .ToThisAssembly()
                        .ToAllPackageAssemblies();

                    scan
                        .AddAllTypesOf<IPreviewModelDecorator>();
                });
            });
        }
    }
}