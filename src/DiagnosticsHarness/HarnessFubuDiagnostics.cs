using System.ComponentModel;

namespace DiagnosticsHarness
{
    public class FubuDiagnosticsConfiguration
    {
        public string Url = "harness";
        public string Title = "Harness";
        public string Description = "Core diagnostics for all FubuMVC and FubuTransportation applications";
    }

    public class HarnessFubuDiagnostics
    {
        [Description("One")]
        public string get_one()
        {
            return "One";
        }

        [Description("Two")]
        public string get_two()
        {
            return "Two";
        } 
    }
}