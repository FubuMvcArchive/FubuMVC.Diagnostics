using System;
using System.Collections.Generic;
using FubuCore.Descriptions;
using FubuMVC.Core.Registration;
using FubuMVC.Diagnostics.Visualization;
using FubuMVC.TwitterBootstrap.Collapsibles;
using HtmlTags;
using System.Linq;

namespace FubuMVC.Diagnostics.Services
{
    public class ServiceEndpoints
    {
        private readonly BehaviorGraph _graph;

        public ServiceEndpoints(BehaviorGraph graph)
        {
            _graph = graph;
        }

        private ServiceViewModel groupBy(Func<ServiceEvent, string> grouping, Action<ServiceEvent, Description> alteration, Func<ServiceEvent, string> ordering)
        {
            var events = _graph.Log.EventsOfType<ServiceEvent>();
            var tags = events.GroupBy(grouping).Select(group =>
            {
                var descriptions = group.OrderBy(ordering).Select(@event =>
                {
                    var description = Description.For(@event);
                    alteration(@event, description);

                    return description;
                });

                return new ServiceGroupTag(group.Key, descriptions);
            });

            return new ServiceViewModel(tags);
        }

        public ServiceViewModel get_services_byname()
        {
            return groupBy(x => x.ServiceType.FullName,
                           (@event, desc) => desc.Properties["Source"] = @event.RegistrationSource,
                           @event => @event.RegistrationSource);
        }

        public ServiceViewModel get_services_bysource()
        {
            return groupBy(x => x.RegistrationSource,
                           (@event, desc) => desc.Properties["Service Type"] = @event.ServiceType.FullName,
                           e => e.ServiceType.FullName);
        }
    }

    public class ServiceViewModel
    {
        private readonly TagList _tagList;

        public ServiceViewModel(IEnumerable<HtmlTag> tags)
        {
            _tagList = new TagList(tags);
        }

        public TagList Tags
        {
            get { return _tagList; }
        }
    }

    public class ServiceGroupTag : CollapsibleTag
    {
        public ServiceGroupTag(string title, IEnumerable<Description> descriptions) : base(Guid.NewGuid().ToString(), title)
        {
            descriptions.Each(x =>
            {
                var titleTag = new HtmlTag("div").AddClass("service-title").Text(x.Title);
                AppendContent(titleTag);
                AppendContent(new DescriptionBodyTag(x));
            });
        }
    }
}