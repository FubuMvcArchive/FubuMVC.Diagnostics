using System;
using System.Runtime.Serialization;
using FubuCore.Logging;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime.Logging;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    public class BehaviorTracer : WrappingBehavior
    {
        private readonly BehaviorCorrelation _correlation;
        private readonly ILogger _logger;

        public BehaviorTracer(BehaviorCorrelation correlation, ILogger logger)
        {
            _correlation = correlation;
            _logger = logger;
        }

        protected override void invoke(Action action)
        {
            _logger.DebugMessage(() => new BehaviorStart(_correlation));

            try
            {
                action();
            }
            catch (UnhandledFubuException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(_correlation.BehaviorId, "Behavior Failure", ex);
                throw new UnhandledFubuException("Behavior failed", ex);
            }
            finally
            {
                _logger.DebugMessage(() => new BehaviorFinish(_correlation));
            }
        }
    }


    public class UnhandledFubuException : Exception
    {
        public UnhandledFubuException()
        {
        }

        public UnhandledFubuException(string message) : base(message)
        {
        }

        public UnhandledFubuException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnhandledFubuException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}