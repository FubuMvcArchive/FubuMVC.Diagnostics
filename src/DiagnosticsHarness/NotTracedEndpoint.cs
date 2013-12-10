using FubuMVC.Core;
using FubuMVC.Core.Registration.Nodes;

namespace DiagnosticsHarness
{
    public class NotTracedEndpoint
    {
        [Tag(BehaviorChain.NoTracing)]
        public string get_not_traced()
        {
            return "I should not be traced";
        } 
    }
}