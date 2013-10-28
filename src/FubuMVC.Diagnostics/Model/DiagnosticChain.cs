using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
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

        public static DiagnosticChain For<T>(DiagnosticGroup group, Expression<Action<T>> method)
        {
            var call = ActionCall.For(method);
            return new DiagnosticChain(group, call);
        }


        public DiagnosticChain(DiagnosticGroup group, ActionCall call)
        {
            if (call.Method.Name == "Index")
            {
                Route = new RouteDefinition("{0}/{1}".ToFormat(DiagnosticsUrl, group.Url));
                Title = group.Title;
                Description = group.Description;
            }
            else
            {
                Route = call.ToRouteDefinition();
                MethodToUrlBuilder.Alter(Route, call);
                Route.Prepend(group.Url);
                Route.Prepend(DiagnosticsUrl);

                Title = Route.Pattern.Split('/').Last().Capitalize();
                Description = string.Empty;

                call.ForAttributes<DescriptionAttribute>(att => {
                    if (att.Description.Contains(":"))
                    {
                        var parts = att.Description.ToDelimitedArray(':');
                        Title = parts.First();
                        Description = parts.Last();
                    }
                    else
                    {
                        Title = att.Description;
                    }
                });
            }

            AddToEnd(new ChromeNode(typeof(DashboardChrome)));
            
            AddToEnd(call);
        }

        public DiagnosticGroup Group { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsLink()
        {
            return Route.RespondsToMethod("GET") && Route.Input == null;
        }
    }
}