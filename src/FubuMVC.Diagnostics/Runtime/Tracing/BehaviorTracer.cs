using System;
using FubuCore.Logging;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    public class BehaviorTracer : WrappingBehavior
    {
        readonly BehaviorCorrelation _correlation;
        readonly ILogger _logger;
        readonly IExceptionHandlingObserver _exceptionObserver;

        public BehaviorTracer(BehaviorCorrelation correlation, ILogger logger, IExceptionHandlingObserver exceptionObserver)
        {
            _correlation = correlation;
            _logger = logger;
            _exceptionObserver = exceptionObserver;
        }

        protected override void invoke(Action action)
        {
            _logger.DebugMessage(() => new BehaviorStart(_correlation));

            try
            {
                action();
                _logger.DebugMessage(() => new BehaviorFinish(_correlation));
            }
            catch (Exception ex)
            {
                if (!_exceptionObserver.WasObserved(ex))
                {
                    _logger.DebugMessage(() =>
                    {
                        var log = new BehaviorFinish(_correlation);
                        log.LogException(ex);

                        return log;
                    });

                    _exceptionObserver.RecordHandled(ex);
                }

                throw;
            }
        }
    }
}