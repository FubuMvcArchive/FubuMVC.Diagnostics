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

            Add("div").AddClasses("span2", "step-duration").Text(step.RequestTimeInMilliseconds.ToString());
            Add("div").AddClasses("span10", "step-body").Text(content).Encoded(false);
        }
    }
}