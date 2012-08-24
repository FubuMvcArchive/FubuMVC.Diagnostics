using System;
using FubuMVC.Diagnostics.Chains;
using HtmlTags;

namespace FubuMVC.Diagnostics.Shared.Tags
{
    public enum HtmlEncoding
    {
        UseEncoding,
        NoEncoding
    }

    public class DetailsTableTag : TableTag
    {
        public DetailsTableTag()
        {
            AddClass("details");
            AddClass("table");
            AddClass("table-striped");
        }

        public void AddSection(string label)
        {
            AddBodyRow(x =>
            {
                x.Header().Attr("colspan", "2").Add("span").Text(label).AddClass("details-section");
            });
        }

        public void AddDetail(string label, string text, HtmlEncoding encoding = HtmlEncoding.UseEncoding)
        {
            AddBodyRow(x =>
            {
                x.Header().Add("span").Text(label);
                x.Cell(text).Encoded(encoding == HtmlEncoding.UseEncoding);
            });
        }

        public void AddDetail(string label, HtmlTag behaviorsTag)
        {
            AddBodyRow(x =>
            {
                x.Header().Add("span").Text(label);
                x.Cell().Append(behaviorsTag);
            });
        }
    }
}