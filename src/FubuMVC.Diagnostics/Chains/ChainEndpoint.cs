using FubuMVC.Core.Registration;
using HtmlTags;
using System.Linq;

namespace FubuMVC.Diagnostics.Chains
{
    public class ChainEndpoint
    {
        private readonly BehaviorGraph _graph;

        public ChainEndpoint(BehaviorGraph graph)
        {
            _graph = graph;
        }

        public ChainViewModel get_chain_Id(ChainRequest request)
        {
            var chain = _graph.Behaviors.FirstOrDefault(x => x.UniqueId == request.Id);
            // TODO -- what if chain doesn't exist?

            return new ChainViewModel();
        }
    }

    public class ChainViewModel
    {
        
    }
}