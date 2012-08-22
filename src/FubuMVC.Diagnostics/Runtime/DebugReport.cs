using System;
using System.Collections.Generic;

namespace FubuMVC.Diagnostics.Runtime
{
    // TODO -- make DebugReport stupid
    public class DebugReport : IDebugReport
    {
        private readonly IList<object> _logs = new List<object>();

        public DebugReport()
        {
            Id = Guid.NewGuid();

            Time = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public Guid BehaviorId { get; set; }

        public double ExecutionTime
        {
            get { throw new NotImplementedException(); }
        }

        public void MarkFinished()
        {
            throw new NotImplementedException();
        }

        public void AddLog(object log)
        {
            _logs.Add(log);
        }

        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public DateTime Time { get; set; }
    }
}