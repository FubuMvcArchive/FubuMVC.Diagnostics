using System;
using HtmlTags;

namespace FubuMVC.Diagnostics.Shared
{
    public class TypeEndpoint
    {
        public HtmlTag VisualizePartial(TypeInput input)
        {
            var type = input.Type;

            var div = new HtmlTag("div");
            div.Text(type.Name);

            // TODO --add more in a popover

            //div.Add("p").Text(type.FullName);
            //div.Add("p").Text("from " + type.Assembly.GetName().FullName);

            return div;
        }
    }

    public class TypeInput
    {
        public Type Type { get; set; }
    }
}