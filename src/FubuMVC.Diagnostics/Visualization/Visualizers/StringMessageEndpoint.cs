using FubuCore.Logging;
using HtmlTags;

namespace FubuMVC.Diagnostics.Visualization.Visualizers
{
    public class StringMessageEndpoint
    {
        public HtmlTag VisualizeStringMessagePartial(StringMessage message)
        {
            return new HtmlTag("div").AddClasses("alert", "alert-info").Text(message.Message);
        }
    }
}