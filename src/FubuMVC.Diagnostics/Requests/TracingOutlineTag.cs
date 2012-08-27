using FubuCore.Descriptions;
using FubuMVC.Diagnostics.Runtime;
using System.Collections.Generic;

namespace FubuMVC.Diagnostics.Requests
{
    public class TracingOutlineTag : OutlineTag
    {
        public TracingOutlineTag(RequestLog log)
        {
            AddHeader("Tracing");

            log.AllSteps().Each(x =>
            {
                var description = Description.For(x.Log);

                var node = AddNode(description.Title, x.Id.ToString());

                node.Container.Add("span").AddClass("node-trace-duration").Text(x.RequestTimeInMilliseconds + " ms");
            });
        }
    }
}