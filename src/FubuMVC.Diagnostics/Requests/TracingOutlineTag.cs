using FubuCore.Descriptions;
using FubuCore.Logging;
using FubuMVC.Diagnostics.Runtime;
using System.Collections.Generic;
using FubuCore;
using FubuMVC.TwitterBootstrap.Tags;

namespace FubuMVC.Diagnostics.Requests
{
    public class TracingOutlineTag : OutlineTag
    {
        public TracingOutlineTag(RequestLog log)
        {
            AddHeader("Tracing");

            log.AllSteps().Each(x =>
            {
                var node = addNode(x);

                node.Container.Add("span").AddClass("node-trace-duration").Text(x.RequestTimeInMilliseconds + " ms");
            });
        }

        private OutlineNodeTag addNode(RequestStep step)
        {
            string title = null;

            if (step.Log is StringMessage)
            {
                title = step.Log.As<StringMessage>().Message;
            }
            else
            {
                var description = Description.For(step.Log);
                title = description.Title;
            }

            return AddNode(title, step.Id.ToString());
        }
    }
}