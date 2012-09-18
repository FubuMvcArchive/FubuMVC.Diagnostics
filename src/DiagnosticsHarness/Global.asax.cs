using System;
using System.Runtime.InteropServices;
using System.Web;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using StructureMap;

namespace DiagnosticsHarness
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            new HarnessApplicationSource().BuildApplication().Bootstrap();
        }
    }

    public class HarnessApplicationSource : IApplicationSource
    {
        public FubuApplication BuildApplication()
        {
            FubuMode.Mode(FubuMode.Development);
            if (!FubuMode.InDevelopment())
            {
                throw new InvalidOperationException("You can't be here without development mode!");
            }

            return FubuApplication.For<HarnessRegistry>().StructureMap(new Container());
        }
    }

    public class HarnessRegistry : FubuRegistry
    {
    }
}