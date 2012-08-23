using System;
using FubuMVC.Core;
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

        public DiagnosticBehavior(IRequestTrace trace, IDebugDetector detector, IOutputWriter writer)
        {
            _trace = trace;
            _detector = detector;
            _writer = writer;
        }

        protected override void invoke(Action action)
        {
            _trace.Start();

            try
            {
                action();
            }
            catch (UnhandledFubuException ex)
            {
                _trace.MarkAsFailedRequest();
                throw ex.InnerException;
            }
            catch (Exception ex)
            {
                _trace.MarkAsFailedRequest();
                _trace.Log(new ExceptionReport("Request failed", ex));
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