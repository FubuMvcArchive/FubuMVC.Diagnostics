using System.ComponentModel;
using System.Web.UI.WebControls;

namespace DiagnosticsHarness
{
    // SAMPLE: FubuDiagnosticsConfiguration
    public class FubuDiagnosticsConfiguration
    {
        // Configures the url prefix for all the diagnostic views in this project
        // Think:  /_fubu/harness/[page url]
        // The default is just the name of the containing assembly
        public string Url = "harness";

        // The user friendly name you'd like shown in navigation elements
        // The default is the containing assembly name
        public string Title = "Harness";

        // User friendly description for navigation elements 
        public string Description = "This is merely a standin set of diagnostic pages for testing";
    }
    // ENDSAMPLE

    // SAMPLE: SampleFubuDiagnostics
    public class SampleFubuDiagnostics
    {
        // This will be considered to be the default diagnostic page for the assembly
        // The Url of this method will be "/_fubu/harness" where "harness" is the url
        // for the diagnostic group
        public string Index()
        {
            return "some explanatory information";
        }

        // The [Description] attribute can be optionally used to define a title
        // and description to be used for this screen in navigation elements
        // The Url of this action will be "/_fubu/harness/sample"
        [Description("The Title:The description of what this page is")]
        public SomeViewModel get_sample()
        {
            // You'll probably add some data here;)
            return new SomeViewModel();
        }

        // This method will have the "chrome" around it
        // because it has the word "details" in the url
        public string get_some_details_Name(DetailRequest request)
        {
            return "Some details about " + request.Name;
        }

        // This method will not have any chrome applied because it's a chrome
        public string post_query(Query query)
        {
            return "some data";
        }
    }

    public class DetailRequest
    {
        public string Name { get; set; }
    }

    public class Query
    {
        
    }
    // ENDSAMPLE

    public class SomeViewModel{}

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