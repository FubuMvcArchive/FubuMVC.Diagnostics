using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    public class DiagnosticBehavior : BasicBehavior
    {
        private readonly IDebugDetector _detector;
        private readonly IRequestTrace _trace;
        private readonly IOutputWriter _writer;

        public DiagnosticBehavior(IRequestTrace trace, IDebugDetector detector, IOutputWriter writer)
            : base(PartialBehavior.Ignored)
        {
            _trace = trace;
            _detector = detector;
            _writer = writer;
        }

        protected override DoNext performInvoke()
        {
            _trace.Start();
            return DoNext.Continue;
        }

        protected override void afterInsideBehavior()
        {
            write();
        }

        private void write()
        {
            _trace.MarkFinished();

            if (_detector.IsDebugCall())
            {
                _writer.RedirectToUrl(_trace.LogUrl);
            }
        }
    }
}