using HtmlTags;

namespace FubuMVC.Diagnostics.Shared.Tags
{
    public static class HtmlTagExtensions
    {
        public static HtmlTag PrependAnchor(this HtmlTag tag)
        {
            var a = new HtmlTag("a").Attr("href", "#" + tag.Id());
            a.Next = tag;

            return a;
        }
    }
}