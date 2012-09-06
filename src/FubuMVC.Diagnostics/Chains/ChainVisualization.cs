using System;
using System.Collections.Generic;
using FubuCore;
using FubuCore.Descriptions;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Routes;
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
            get
            {
                if (Chain.GetRoutePattern().IsNotEmpty())
                {
                    return Chain.GetRoutePattern();
                }

                if (Chain.Calls.Any())
                {
                    return Report.Action.Join(", ");
                }

                if (Chain.Output != null && Chain.Output.Writers.Any())
                {
                    return Chain.Output.Writers.Select(x => Description.For(x).Title).Join(", ");
                }

                return "BehaviorChain " + Chain.UniqueId;
            }
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