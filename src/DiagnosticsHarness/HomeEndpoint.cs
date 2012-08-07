using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core.Registration;
using HtmlTags;

namespace DiagnosticsHarness
{
    public class HomeEndpoint
    {
        private readonly BehaviorGraph _graph;

        public HomeEndpoint(BehaviorGraph graph)
        {
            _graph = graph;
        }

        public HtmlDocument Index()
        {
            var document = new HtmlDocument();

            document.Title = "FubuMVC.Diagnostics Harness";

            document.Add("a").Text("Diagnostics Home Page").Attr("href","_fubu");

            _graph.Behaviors.Each(chain =>
            {
                if (chain.Route != null)
                {
                    document.Add("p").Add("a").Text(chain.Route.Pattern).Attr("href", chain.GetRoutePattern());
                }
            });


            return document;
        }
    }
}