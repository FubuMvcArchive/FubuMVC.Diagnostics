using System;
using System.Collections.Generic;
using System.Diagnostics;
using FubuMVC.Core.Caching;
using FubuMVC.Core.Http;
using System.Linq;
using FubuMVC.Core.Http.Headers;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Runtime.Logging;
using FubuCore;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    public class RequestTrace : IRequestTrace
    {
        private readonly IEnumerable<IRequestTraceObserver> _observers;
        private readonly IRequestLogBuilder _builder;
        private readonly IResponse _response;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public RequestTrace(IEnumerable<IRequestTraceObserver> observers, IRequestLogBuilder builder, IResponse response)
        {
            _observers = observers;
            _builder = builder;
            _response = response;

            Current = new NulloRequestLog(); // place holder/nullo just to avoid blowing up 
        }

        public string LogUrl
        {
            get { return Current.ReportUrl; }
        }

        public void Start()
        {
            Current = _builder.BuildForCurrentRequest();
            _observers.Each(x => x.Started(Current));

            _stopwatch.Start();
        }

        public void MarkFinished()
        {
            _stopwatch.Stop();
            Current.ExecutionTime = _stopwatch.ElapsedMilliseconds;
            try
            {
                Current.ResponseHeaders = _response.AllHeaders();
            }
            catch (Exception)
            {
                // Whatever the stupid Cassinni thing is blows up on this

                Current.ResponseHeaders = findHeadersFromLog().ToArray();
            }
            finally
            {
                _observers.Each(x => x.Completed(Current));
            }
        }

        // TODO -- this sucks hard.  Beat this with better logging in FubuMVC itself
        private IEnumerable<Header> findHeadersFromLog()
        {
            var directHeaders = Current
                .AllLogsOfType<SetHeaderValue>()
                .Select(x => new Header(x.Key, x.Value));

            foreach (var h in directHeaders)
            {
                yield return h;
            }

            var recordedHeaders = Current.AllLogsOfType<IRecordedOutput>()
                .SelectMany(x => x.Headers());

            foreach (var h in recordedHeaders)
            {
                yield return h;
            }

            var contentTypeHolder = Current.AllLogsOfType<IHaveContentType>()
                .LastOrDefault(x => x.ContentType.IsNotEmpty());

            if (contentTypeHolder != null)
            {
                yield return new Header(HttpResponseHeaders.ContentType, contentTypeHolder.ContentType);
            }
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

    public class NulloRequestLog : RequestLog
    {
        public override void AddLog(double requestTimeInMilliseconds, object log)
        {
            // Do nothing.  Nothing at all.
        }
    }
}