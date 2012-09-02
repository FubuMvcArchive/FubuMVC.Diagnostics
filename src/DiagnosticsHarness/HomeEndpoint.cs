using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuMVC.Core.UI;
using FubuMVC.Diagnostics.Chains;
using FubuMVC.Diagnostics.Dashboard;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Diagnostics.Routes;
using HtmlTags;
using FubuMVC.Diagnostics.Visualization;

namespace DiagnosticsHarness
{
    public class HomeEndpoint
    {
        private readonly BehaviorGraph _graph;
        private readonly FubuHtmlDocument _document;

        public HomeEndpoint(BehaviorGraph graph, FubuHtmlDocument document )
        {
            _graph = graph;
            _document = document;
        }

        public FubuContinuation Index()
        {
            var chain = _graph.BehaviorFor<RouteExplorerEndpoint>(x => x.get_routes(null));
            return FubuContinuation.RedirectTo(new ChainDetailsRequest{
                Id = chain.UniqueId
            });

//            return FubuContinuation.RedirectTo<RequestsEndpoint>(x => x.get_requests());

            //var chain = _graph.BehaviorFor<RouteExplorerEndpoint>(x => x.get_routes(null));


            //return FubuContinuation.RedirectTo(new ChainRequest
            //{
            //    Id = chain.UniqueId
            //});

            //_document.Title = "FubuMVC.Diagnostics Harness";

            //_document.Asset("twitterbootstrap");

            //_document.Add("a").Text("Diagnostics Home Page").Attr("href","_fubu");

            //_graph.Behaviors.Each(chain =>
            //{
            //    if (chain.Route != null)
            //    {
            //        _document.Add("p").Add("a").Text(chain.Route.Pattern).Attr("href", chain.GetRoutePattern());
            //    }
            //});


            //var dashboardChain = _graph.BehaviorFor<RequestsEndpoint>(x => x.get_requests(null));
            //var literal = new LiteralTag(_document.Visualize(dashboardChain));

            //_document.Add("hr");
            //_document.Add(literal);


            //_document.WriteAssetsToHead();

            //return _document;
        }
    }
}