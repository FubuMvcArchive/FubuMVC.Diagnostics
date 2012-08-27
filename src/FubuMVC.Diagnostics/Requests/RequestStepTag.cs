using FubuMVC.Diagnostics.Runtime;
using HtmlTags;

namespace FubuMVC.Diagnostics.Requests
{
    public class RequestStepTag : HtmlTag
    {
        public RequestStepTag(RequestStep step, string content) : base("div")
        {
            AddClass("row-fluid");
            Id(step.Id.ToString());

            Text(content).Encoded(false);
        }
    }
}