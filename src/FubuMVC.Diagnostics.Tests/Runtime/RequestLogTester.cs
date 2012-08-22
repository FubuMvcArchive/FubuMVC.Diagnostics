using System;
using FubuCore.Logging;
using FubuMVC.Core.Runtime.Logging;
using FubuMVC.Diagnostics.Runtime;
using NUnit.Framework;
using System.Linq;
using FubuTestingSupport;

namespace FubuMVC.Diagnostics.Tests.Runtime
{
    [TestFixture]
    public class RequestLogTester
    {
        [Test]
        public void add_log()
        {
            var log = new RequestLog();

            var stringMessage = new StringMessage("something");
            log.AddLog(123.45, stringMessage);

            log.AddLog(234, "something");

            log.AllSteps()
                .ShouldHaveTheSameElementsAs(new RequestStep(123.45, stringMessage), new RequestStep(234, "something"));
        }

        [Test]
        public void failed_is_false_by_default()
        {
            // trivial code, but kind of important
        }

        [Test]
        public void has_a_unique_id()
        {
            var log1 = new RequestLog();
            var log2 = new RequestLog();
            var log3 = new RequestLog();

            log1.Id.ShouldNotEqual(Guid.Empty);
            log2.Id.ShouldNotEqual(Guid.Empty);
            log3.Id.ShouldNotEqual(Guid.Empty);

            log1.Id.ShouldNotEqual(log2.Id);
            log1.Id.ShouldNotEqual(log3.Id);
            log2.Id.ShouldNotEqual(log3.Id);
        }
    }
}