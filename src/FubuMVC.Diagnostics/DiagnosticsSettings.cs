namespace FubuMVC.Diagnostics
{
    public class DiagnosticsSettings
    {
        public DiagnosticsSettings()
        {
            MaxRequests = 200;
        }

        public int MaxRequests { get; set; }
    }
}