using System;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    public class DiagnosticBehavior : BasicBehavior
    {
        private readonly IDebugDetector _detector;
        private readonly Action _initialize;
        private readonly IDebugReport _report;

        public DiagnosticBehavior(IDebugReport report, IDebugDetector detector, IRequestHistoryCache history) : base(PartialBehavior.Ignored)
        {
            _report = report;
            _detector = detector;

            _initialize = () => history.AddReport(report);
        }

        public IActionBehavior Inner { get; set; }

        protected override DoNext performInvoke()
        {
            _initialize();
            return DoNext.Continue;
        }

        protected override void afterInsideBehavior()
        {
            write();
        }

        private void write()
        {
            _report.MarkFinished();

            if (!_detector.IsDebugCall()) return;

            _detector.UnlatchWriting();
        }
    }
}