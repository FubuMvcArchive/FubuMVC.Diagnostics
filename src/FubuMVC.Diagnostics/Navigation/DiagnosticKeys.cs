using FubuLocalization;

namespace FubuMVC.Diagnostics.Navigation
{
    public class DiagnosticKeys : StringToken
    {
        public static readonly DiagnosticKeys Main = new DiagnosticKeys("Main");
        public static readonly DiagnosticKeys Dashboard = new DiagnosticKeys("Dashboard");
        public static readonly DiagnosticKeys HtmlConventions = new DiagnosticKeys("Html Conventions");
        public static readonly DiagnosticKeys ApplicationStartup = new DiagnosticKeys("Application Startup");
        public static readonly DiagnosticKeys Requests = new DiagnosticKeys("Requests");
        public static readonly DiagnosticKeys Endpoints = new DiagnosticKeys("Endpoints");
        public static readonly DiagnosticKeys ModelBindingExplorer = new DiagnosticKeys("Model Binding Explorer");

        public static readonly DiagnosticKeys Services = new DiagnosticKeys("Services");
        public static readonly DiagnosticKeys ServicesBySource = new DiagnosticKeys("Services by Source");
        public static readonly DiagnosticKeys ServicesByName = new DiagnosticKeys("Services by Name");


        public DiagnosticKeys(string defaultValue) : base(null, defaultValue, namespaceByType: true)
        {
        }

        public bool Equals(DiagnosticKeys other)
        {
            return other.Key.Equals(Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as DiagnosticKeys);
        }

        public override int GetHashCode()
        {
            return ("DiagnosticKeys:" + Key).GetHashCode();
        }
    }
}