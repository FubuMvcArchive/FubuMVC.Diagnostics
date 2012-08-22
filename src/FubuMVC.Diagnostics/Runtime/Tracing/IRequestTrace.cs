using System;
using System.Diagnostics;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    /*
     * TODO:
     * 1.) register IRequestTrace
     * 2.) register IDebugReportBuilder
     */

    public interface IRequestTrace
    {
        string LogUrl { get; }
        void Start();
        void MarkFinished();
        void Log(object message);
    }

    public interface IRequestLogBuilder
    {
        RequestLog BuildForCurrentRequest();
    }

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
            get { throw new NotImplementedException(); }
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

        public RequestLog Current { get; set; }

        public Stopwatch Stopwatch
        {
            get { return _stopwatch; }
        }
    }
}