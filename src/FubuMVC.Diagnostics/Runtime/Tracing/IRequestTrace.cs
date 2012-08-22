using System;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    public interface IRequestTrace
    {
        string LogUrl { get; }
        void Start();
        void MarkFinished();
        void Log(object message);
        void MarkAsFailedRequest();
    }
}