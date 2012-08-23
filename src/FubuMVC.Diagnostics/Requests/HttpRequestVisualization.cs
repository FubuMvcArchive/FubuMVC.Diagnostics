using System;
using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Diagnostics.Requests
{
    public class HttpRequestVisualization
    {
        private readonly RequestLog _log;

        public HttpRequestVisualization(RequestLog log)
        {
            _log = log;
        }

        public Guid Id { get; set; }

        public bool Equals(HttpRequestVisualization other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (HttpRequestVisualization)) return false;
            return Equals((HttpRequestVisualization) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}