using FubuMVC.Core.Behaviors.Chrome;
using FubuMVC.Core.Urls;
using FubuMVC.Diagnostics.Chrome;
using FubuMVC.Diagnostics.Visualization;
using HtmlTags;

namespace DiagnosticsHarness
{
    [Chrome(typeof(DashboardChrome), Title = "Description Samples")]
    public class DescriptionEndpoints
    {
        private readonly IUrlRegistry _urls;

        public DescriptionEndpoints(IUrlRegistry urls)
        {
            _urls = urls;
        }

        public HtmlTag get_descriptions()
        {
            var document = new HtmlDocument();
            var top = document.Push("div");


            DescriptionBag.Each((name, description) =>
            {
                top.Add("h3").Text(name);
                top.Add("a").Text(name).Attr("href", _urls.UrlFor(new DescriptionRequest{
                    Name = name
                }));

                top.Add("p/i").Text(description.Title);
                top.Append(new DescriptionBodyTag(description));

                top.Add("hr");
                top.Add("b");
            });

            return top;
        }

        public HtmlTag get_description_Name(DescriptionRequest request)
        {
            var description = DescriptionBag.DescriptionFor(request.Name);

            var tag = new HtmlTag("div");
            tag.Add("h4").Text(description.Title);

            tag.Append(new DescriptionBodyTag(description));

            return tag;
        }
    }

    public class DescriptionRequest
    {
        public string Name { get; set; }
    }
}