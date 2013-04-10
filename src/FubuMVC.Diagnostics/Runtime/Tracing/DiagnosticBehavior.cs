using System;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Runtime.Logging;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    public class DiagnosticBehavior : WrappingBehavior
    {
        private readonly IDebugDetector _detector;
        private readonly IRequestTrace _trace;
        private readonly IOutputWriter _writer;
        private readonly IExceptionHandlingObserver _exceptionObserver;

        public DiagnosticBehavior(IRequestTrace trace, IDebugDetector detector, IOutputWriter writer,
            IExceptionHandlingObserver exceptionObserver)
        {
            _trace = trace;
            _detector = detector;
            _writer = writer;
            _exceptionObserver = exceptionObserver;
        }

        protected override void invoke(Action action)
        {
            _trace.Start();

            try
            {
                action();
            }
            catch (Exception ex)
            {
                _trace.MarkAsFailedRequest();

                if (!_exceptionObserver.WasObserved(ex))
                {
                    _trace.Log(new ExceptionReport("Request failed", ex));
                    _exceptionObserver.RecordHandled(ex);
                }

                throw;
            }
            finally
            {
                _trace.MarkFinished();

                if (_detector.IsDebugCall())
                {
                    _writer.RedirectToUrl(_trace.LogUrl);
                }
            }
        }
    }
}