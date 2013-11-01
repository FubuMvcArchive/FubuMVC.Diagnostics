using System;
using FubuCore.Logging;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuMVC.Core.UI;
using FubuMVC.Diagnostics.ModelBinding;

namespace DiagnosticsHarness
{
    public class HomeEndpoint
    {
        private readonly FubuHtmlDocument _document;
        private readonly BehaviorGraph _graph;
        private readonly ILogger _logger;

        public HomeEndpoint(BehaviorGraph graph, FubuHtmlDocument document, ILogger logger)
        {
            _graph = graph;
            _document = document;
            _logger = logger;
        }

        public FubuContinuation Index()
        {
            //return FubuContinuation.RedirectTo<ServiceEndpoints>(x => x.get_services_byname());

            //return FubuContinuation.RedirectTo<ModelBindingSampleEndpoint>(x => x.get_deep_data());

            //return FubuContinuation.RedirectTo(new DebugRequest{
            //    FubuDebug = true
            //});

            //var chain = _graph.BehaviorFor<EndpointExplorerFubuDiagnostics>(x => x.get_endpoints(null));

            //return FubuContinuation.RedirectTo<ModelBindingFubuDiagnostics>(x => x.get_binding_all());

            return FubuContinuation.RedirectTo("/_fubu/harness");

            //return FubuContinuation.RedirectTo<EndpointExplorerFubuDiagnostics>(x => x.get_endpoints(null));


            //return FubuContinuation.RedirectTo<DescriptionEndpoints>(x => x.get_descriptions());

            //return FubuContinuation.RedirectTo(new ChainDetailsRequest
            //{
            //    Id = chain.UniqueId
            //});

//            return FubuContinuation.RedirectTo<RequestsFubuDiagnostics>(x => x.get_requests());

            //var chain = _graph.BehaviorFor<EndpointExplorerFubuDiagnostics>(x => x.get_endpoints(null));


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


            //var dashboardChain = _graph.BehaviorFor<RequestsFubuDiagnostics>(x => x.get_requests(null));
            //var literal = new LiteralTag(_document.Visualize(dashboardChain));

            //_document.Add("hr");
            //_document.Add(literal);


            //_document.WriteAssetsToHead();

            //return _document;
        }

        [WrapWith(typeof (BadBehavior))]
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