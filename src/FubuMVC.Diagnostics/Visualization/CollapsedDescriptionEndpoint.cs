using FubuMVC.Core.UI;
using FubuMVC.TwitterBootstrap;
using HtmlTags;
using FubuMVC.TwitterBootstrap.Tags;

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
            if (subject.Description.HasMoreThanTitle())
            {
                return _document.CollapsiblePartialFor(subject.Description).Title(subject.Description.Title).ToString();
            }

            return new HtmlTag("div", x =>
            {
                x.PrependGlyph(GlyphRegistry.GlyphFor(subject.Description.TargetType));
                x.Add("span").Text(subject.Description.Title);
            }).ToString();
        }
    }
}