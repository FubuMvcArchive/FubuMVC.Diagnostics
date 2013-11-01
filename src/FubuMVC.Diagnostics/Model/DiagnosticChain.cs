using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using FubuCore;
using FubuMVC.Core.Behaviors.Chrome;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Diagnostics.Chrome;

namespace FubuMVC.Diagnostics.Model
{
    public class DiagnosticChain : BehaviorChain
    {
        public const string DiagnosticsUrl = "_fubu";
        private bool _isIndex;

        public DiagnosticChain(DiagnosticGroup group, ActionCall call)
        {
            Group = group;

            if (call.Method.Name == "Index")
            {
                setupAsIndex(@group);
            }
            else
            {
                buildRouteFromActionCall(@group, call);

                readTitleAndDescription(call);
            }

            if (IsLink())
            {
                AddToEnd(new ChromeNode(typeof (DashboardChrome))
                {
                    Title = () => Title
                });
            }

            AddToEnd(call);
        }

        public bool IsIndex
        {
            get { return _isIndex; }
        }

        public DiagnosticGroup Group { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public static DiagnosticChain For<T>(DiagnosticGroup group, Expression<Action<T>> method)
        {
            ActionCall call = ActionCall.For(method);
            return new DiagnosticChain(group, call);
        }

        private void setupAsIndex(DiagnosticGroup @group)
        {
            Route = new RouteDefinition("{0}/{1}".ToFormat(DiagnosticsUrl, @group.Url).TrimEnd('/'));
            Title = @group.Title;
            Description = @group.Description;
            _isIndex = true;
        }

        private void buildRouteFromActionCall(DiagnosticGroup @group, ActionCall call)
        {
            Route = call.ToRouteDefinition();
            MethodToUrlBuilder.Alter(Route, call);
            Route.Prepend(@group.Url);
            Route.Prepend(DiagnosticsUrl);
        }

        private void readTitleAndDescription(ActionCall call)
        {
            Title = Route.Pattern.Split('/').Last().Capitalize();
            Description = string.Empty;

            call.ForAttributes<DescriptionAttribute>(att => {
                if (att.Description.Contains(":"))
                {
                    string[] parts = att.Description.ToDelimitedArray(':');
                    Title = parts.First();
                    Description = parts.Last();
                }
                else
                {
                    Title = att.Description;
                }
            });
        }

        public bool IsLink()
        {
            return Route.RespondsToMethod("GET") && (Route.Input == null || !Route.Input.RouteParameters.Any()) &&
                   !IsPartialOnly;
        }
    }
}