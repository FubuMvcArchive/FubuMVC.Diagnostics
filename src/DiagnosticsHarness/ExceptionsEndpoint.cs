using System;
using FubuMVC.Core.Behaviors.Chrome;
using FubuMVC.Diagnostics.Chrome;
using HtmlTags;

namespace DiagnosticsHarness
{
    [Chrome(typeof(DashboardChrome), Title = "Exception Samples")]
    public class ExceptionsEndpoint
    {
        public HtmlTag get_failure()
        {
            throw new NotImplementedException();
        }
    }
}