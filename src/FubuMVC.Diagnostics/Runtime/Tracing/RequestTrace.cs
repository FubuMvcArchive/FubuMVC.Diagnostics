using System;
using System.Diagnostics;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    public class RequestTrace : IRequestTrace
    {
        private readonly IRequestHistoryCache _cache;
        private readonly IRequestLogBuilder _builder;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public RequestTrace(IRequestHistoryCache cache, IRequestLogBuilder builder)
        {
            _cache = cache;
            _builder = builder;
        }

        public string LogUrl
        {
            get { return Current.ReportUrl; }
        }

        public void Start()
        {
            Current = _builder.BuildForCurrentRequest();
            _cache.Store(Current);

            _stopwatch.Start();
        }

        public void MarkFinished()
        {
            _stopwatch.Stop();
            Current.ExecutionTime = _stopwatch.ElapsedMilliseconds;
        }

        public void Log(object message)
        {
            Current.AddLog(_stopwatch.ElapsedMilliseconds, message);
        }

        public void MarkAsFailedRequest()
        {
            Current.Failed = true;
        }

        public RequestLog Current { get; set; }

        public Stopwatch Stopwatch
        {
            get { return _stopwatch; }
        }
    }
}