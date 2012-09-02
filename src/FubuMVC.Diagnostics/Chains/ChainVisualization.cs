using System;
using System.Collections.Generic;
using FubuCore;
using FubuCore.Descriptions;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Routes;
using FubuMVC.TwitterBootstrap.Tags;
using HtmlTags;
using System.Linq;

namespace FubuMVC.Diagnostics.Chains
{
    public class ChainVisualization : IRedirectable
    {
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
    }
}