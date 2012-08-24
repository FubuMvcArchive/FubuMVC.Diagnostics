using FubuMVC.Core.UI;
using FubuMVC.TwitterBootstrap;

namespace FubuMVC.Diagnostics.Visualization
{
    public class CollapsedDescriptionEndpoint
    {
        private readonly FubuHtmlDocument _document;

        public CollapsedDescriptionEndpoint(FubuHtmlDocument document)
        {
            _document = document;
        }

        public string CollapsedDescriptionPartial(CollapsedDescription subject)
        {
            return _document.CollapsiblePartialFor(subject.Description).Title(subject.Description.Title).ToString();
        }
    }
}