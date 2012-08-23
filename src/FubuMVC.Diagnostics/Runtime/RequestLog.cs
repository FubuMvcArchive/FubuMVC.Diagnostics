using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core.Runtime.Logging;

namespace FubuMVC.Diagnostics.Runtime
{
    public class RequestLog
    {
        private readonly IList<RequestStep> _steps = new List<RequestStep>();

        public RequestLog()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid ChainId { get; set; }

        public double ExecutionTime { get; set; }


        public string Url { get; set; }
        public string ReportUrl { get; set; }
        public string HttpMethod { get; set; }
        public DateTime Time { get; set; }

        public void AddLog(double requestTimeInMilliseconds, object log)
        {
            _steps.Add(new RequestStep(requestTimeInMilliseconds, log));
        }

        public HttpStatus HttpStatus
        {
            get
            {
                HttpStatusReport report = null;

                var log = _steps.Where(x => x.Log is HttpStatusReport).LastOrDefault();
                if (log != null)
                {
                    report = log.Log.As<HttpStatusReport>();
                }
                
                return new HttpStatus(report, Failed);
            }
        }

        public string LocalTime
        {
            get
            {
                return Time.ToLocalTime().ToShortTimeString();
            }
        }

        public string ContentType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Failed { get; set; }

        public IEnumerable<RequestStep> AllSteps()
        {
            return _steps;
        }

        public TracedStep<T> FindStep<T>(Func<T, bool> filter)
        {
            var step = _steps.Where(x => x.Log is T).FirstOrDefault(x => filter(x.Log.As<T>()));
            return step == null ? null : step.ToTracedStep<T>();
        }

        public bool Equals(RequestLog other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (RequestLog)) return false;
            return Equals((RequestLog) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Id: {0}", Id);
        }
    }
}