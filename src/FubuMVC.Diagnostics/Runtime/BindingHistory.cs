using FubuCore.Binding.InMemory;
using FubuCore.Binding.Logging;

namespace FubuMVC.Diagnostics.Runtime
{
    public class BindingHistory : IBindingHistory
    {
        private readonly RequestLog _log;

        public BindingHistory(RequestLog log)
        {
            _log = log;
        }

        public void Store(BindingReport report)
        {
            // do nothing
        }
    }
}