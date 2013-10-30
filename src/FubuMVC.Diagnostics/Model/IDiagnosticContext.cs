using FubuMVC.Core.Http;

namespace FubuMVC.Diagnostics.Model
{
    public interface IDiagnosticContext
    {
        DiagnosticGroup CurrentGroup();
        DiagnosticChain CurrentChain();
    }

    public class DiagnosticContext : IDiagnosticContext
    {
        private readonly ICurrentChain _currentChain;

        public DiagnosticContext(ICurrentChain currentChain)
        {
            _currentChain = currentChain;
        }

        public DiagnosticGroup CurrentGroup()
        {
            var current = CurrentChain();
            return current == null ? null : current.Group;
        }

        public DiagnosticChain CurrentChain()
        {
            return _currentChain.OriginatingChain as DiagnosticChain;
        }
    }
}