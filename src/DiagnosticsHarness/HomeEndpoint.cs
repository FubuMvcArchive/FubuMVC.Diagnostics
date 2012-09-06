using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticsHarness.ModelBinding;
using FubuCore.Logging;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuMVC.Core.UI;
using FubuMVC.Diagnostics.Chains;
using FubuMVC.Diagnostics.Dashboard;
using FubuMVC.Diagnostics.ModelBinding;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Diagnostics.Routes;
using FubuMVC.Diagnostics.Services;
using HtmlTags;
using FubuMVC.Diagnostics.Visualization;

namespace DiagnosticsHarness
{
    public class HomeEndpoint
    {
        private readonly BehaviorGraph _graph;
        private readonly FubuHtmlDocument _document;
        private readonly ILogger _logger;

        public HomeEndpoint(BehaviorGraph graph, FubuHtmlDocument document, ILogger logger )
        {
            _graph = graph;
            _document = document;
            _logger = logger;
        }

        public FubuContinuation Index()
        {
            return FubuContinuation.RedirectTo<ServiceEndpoints>(x => x.get_services_byname());

            //return FubuContinuation.RedirectTo<ModelBindingSampleEndpoint>(x => x.get_deep_data());

            //return FubuContinuation.RedirectTo(new DebugRequest{
            //    FubuDebug = true
            //});

            //var chain = _graph.BehaviorFor<RouteExplorerEndpoint>(x => x.get_routes(null));

            //return FubuContinuation.RedirectTo<ModelBindingEndpoints>(x => x.get_binding_all());

            //return FubuContinuation.RedirectTo<RouteExplorerEndpoint>(x => x.get_routes(null));


            //return FubuContinuation.RedirectTo<DescriptionEndpoints>(x => x.get_descriptions());

            //return FubuContinuation.RedirectTo(new ChainDetailsRequest
            //{
            //    Id = chain.UniqueId
            //});

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

        [WrapWith(typeof(BadBehavior))]
        public string get_hello(DebugRequest request)
        {
            _logger.Debug("some trace message just to see it");

            return "Hello!";
        }
    }



    public class BadBehavior : BasicBehavior
    {
        public BadBehavior() : base(PartialBehavior.Executes)
        {
        }

        protected override DoNext performInvoke()
        {
            throw new NotImplementedException();
        }
    }

    public class DebugRequest
    {
        [QueryString]
        public bool FubuDebug { get; set; }
    }
}