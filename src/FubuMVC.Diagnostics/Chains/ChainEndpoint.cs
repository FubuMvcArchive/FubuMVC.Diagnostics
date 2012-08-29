using System;
using System.Linq;
using FubuCore.Descriptions;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.UI;
using FubuMVC.Core.Urls;
using FubuMVC.Diagnostics.Routes;
using FubuMVC.Diagnostics.Shared.Tags;
using FubuMVC.Diagnostics.Visualization;
using FubuMVC.TwitterBootstrap.Tags;
using HtmlTags;
using System.Collections.Generic;

namespace FubuMVC.Diagnostics.Chains
{
    public class ChainEndpoint
    {
        private readonly FubuHtmlDocument _document;
        private readonly BehaviorGraph _graph;
        private readonly IUrlRegistry _urls;

        public ChainEndpoint(IUrlRegistry urls, BehaviorGraph graph, FubuHtmlDocument document)
        {
            _urls = urls;
            _graph = graph;
            _document = document;
        }

        public HtmlTag get_chain_Id(ChainRequest request)
        {
            _document.Asset("twitterbootstrap");
            _document.Asset("diagnostics/bootstrap.overrides.css");
            _document.WriteAssetsToHead();

            var top = _document.Push("div");

            var chain = _graph.Behaviors.FirstOrDefault(x => x.UniqueId == request.Id);
            var report = new RouteReport(chain, _urls);

            // TODO -- what if chain doesn't exist?
            var details = buildDetails(report);
            var behaviorsTag = createBehaviorList(chain);

            details.AddDetail("Behaviors", behaviorsTag);

            //createBehaviorList(chain);

            //visualizeChain(chain);

            return top;
        }

        private HtmlTag createBehaviorList(BehaviorChain chain)
        {
            var div = new HtmlTag("div").Id("chain-summary");

            int level = 0;
            chain.Each(node =>
            {
                var description = Description.For(node);

                var child = div.Add("div").AddClass("node-title");

                if (level > 0)
                {
                    var image = _document.Image("arrow-turn-000-left-icon.png");
                    image.Style("padding-left", (level*5) + "px");

                    child.Append(image);
                }

                child.Add("span").Text(description.Title);

                level++;
            });

            return div;
        }

        private void visualizeChain(BehaviorChain chain)
        {
            var literal = new LiteralTag(_document.Visualize(chain));
            _document.Add(literal);
        }

        private DetailsTableTag buildDetails(RouteReport report)
        {
            var builder = new DetailTableBuilder(_document);
            builder.AddDetail("Route", report.Route);
            builder.AddDetail("Http Verbs", report.Constraints);

            builder.AddDetail("Url Category", report.UrlCategory);
            builder.AddDetail("Origin", report.Origin);

            builder.AddDetail("Input Type", report.InputModel);
            builder.AddDetail("Resource Type", report.ResourceType);

            builder.AddDetail("Accepts", report.Accepts);

            builder.AddDetail("Content Type", report.ContentType);

            return builder.DetailTag;
        }
    }
}