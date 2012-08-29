using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.TwitterBootstrap.Tags;

namespace FubuMVC.Diagnostics.Requests
{
    public class BehaviorChainTraceTag : OutlineTag
    {
        public BehaviorChainTraceTag(IEnumerable<BehaviorNode> chain, RequestLog log)
        {
            AddHeader("Behaviors");

            chain.Where(NotDiagnosticNode).Each(node => Append(new BehaviorNodeTraceTag(node, log)));
        }

        public static bool NotDiagnosticNode(BehaviorNode node)
        {
            if (node is DiagnosticNode || node is BehaviorTracerNode) return false;

            return true;
        }
    }
}