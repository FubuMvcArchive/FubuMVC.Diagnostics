using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Http;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using FubuMVC.Diagnostics.Model;
using HtmlTags;

namespace FubuMVC.Diagnostics
{
    public static class FubuPageExtensions
    {
        public static HtmlTag DiagnosticMenu(this IFubuPage page)
        {
            return page.Get<DiagnosticMenuTag>();
        }

        public static string DiagnosticsTitle(this IFubuPage page)
        {
            return page.Get<IDiagnosticContext>().Title();
        }
    }

    public class DiagnosticMenuTag : HtmlTag
    {
        public DiagnosticMenuTag(IDiagnosticContext context, ICurrentHttpRequest currentHttpRequest, IUrlRegistry urls) : base("ul")
        {
            AddClass("nav");

            var group = context.CurrentGroup();

            var index = group.Index();
            if (index != null)
            {
                addLink(index, context, currentHttpRequest);
            }
            else
            {
                var url = urls.UrlFor(new GroupRequest {Name = group.Name});
                var li = Add("li");
                li.Add("a").Attr("href", url).Text(group.Name).Attr("title", group.Description);

                if (context.CurrentChain() == null)
                {
                    li.AddClass("active");
                }
            }

            group.Links().Each(x => addLink(x, context, currentHttpRequest));
        }

        private void addLink(DiagnosticChain diagnosticChain, IDiagnosticContext context, ICurrentHttpRequest currentHttpRequest)
        {
            var url = currentHttpRequest.ToFullUrl(diagnosticChain.GetRoutePattern());
            var li = Add("li");
            li.Add("a").Attr("href", url).Text(diagnosticChain.Title).Attr("title", diagnosticChain.Description);

            if (context.CurrentChain() == diagnosticChain)
            {
                li.AddClass("active");
            }
        }
    }

   
}