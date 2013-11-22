using System;
using System.Collections.Generic;
using FubuCore;
using FubuCore.Descriptions;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Endpoints;
using FubuMVC.Diagnostics.Visualization;
using FubuMVC.TwitterBootstrap.Collapsibles;
using FubuMVC.TwitterBootstrap.Tags;
using HtmlTags;
using System.Linq;

namespace FubuMVC.Diagnostics.Chains
{
    public class ChainVisualization : IRedirectable
    {
        public static readonly string RouteDescId = "route-desc";

        public RouteReport Report { get; set; }
        public BehaviorChain Chain { get; set; }

        public LiteralTag BehaviorVisualization { get; set; }

        public string Title
        {
            get {
                return TitleForChain(Chain);
            }
        }

        public static string TitleForChain(BehaviorChain chain)
        {
            if (chain.GetRoutePattern().IsNotEmpty())
            {
                return chain.GetRoutePattern();
            }

            if (chain.GetRoutePattern() == string.Empty)
            {
                return "(home)";
            }

            if (chain.Calls.Any())
            {
                return chain.Calls.Select(x => x.Description).Join(", ");
            }

            if (chain.HasOutput() && chain.Output.Writers.Any())
            {
                return chain.Output.Writers.Select(x => Description.For(x).Title).Join(", ");
            }

            if (chain.InputType() != null)
            {
                return "Handler for " + chain.InputType().FullName;
            }

            return "BehaviorChain " + chain.UniqueId;
        }

        public DetailsTableTag Details { get; set; }
        public FubuContinuation RedirectTo { get; set; }


        public BehaviorOutlineTag BehaviorsOutline
        {
            get
            {
                return new BehaviorOutlineTag(Chain);
            }
        }

        public HtmlTag RouteTag
        {
            get
            {
                if (Chain.Route == null)
                {
                    return new HtmlTag("div").Render(false);
                }

                var description = Description.For(Chain.Route);
                var collapsedTag = new CollapsibleTag(RouteDescId, "Route");
                collapsedTag.PrependAnchor();
                collapsedTag.AppendContent(new DescriptionBodyTag(description));

                return collapsedTag;
            }
        }
    }
}