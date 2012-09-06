using System;
using System.Linq;
using FubuCore;
using FubuCore.Descriptions;
using FubuCore.Util;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.UI;
using FubuMVC.Diagnostics.Requests;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.TwitterBootstrap.Collapsibles;
using FubuMVC.TwitterBootstrap.Tags;
using HtmlTags;
using System.Collections.Generic;

namespace FubuMVC.Diagnostics.Visualization
{
    public class Visualizer : IVisualizer
    {
        private static readonly Cache<Type, string> _glyphs = new Cache<Type, string>(type => "icon-cog");

        private readonly FubuHtmlDocument _document;
        private readonly Cache<Type, bool> _hasVisualizer;

        public Visualizer(BehaviorGraph graph, FubuHtmlDocument document)
        {
            _document = document;
            _hasVisualizer = new Cache<Type, bool>(type =>
            {
                return graph.Behaviors.Any(x => type == x.InputType());
            });
        }

        public BehaviorNodeViewModel ToVisualizationSubject(BehaviorNode node)
        {
            var description = Description.For(node);


            return new BehaviorNodeViewModel{
                Description = description,
                Node = node,
                VisualizationHtml = HasVisualizer(node.GetType()) 
                    ? _document.PartialFor(node).ToString() 
                    : new DescriptionBodyTag(description).ToString()
            };
        }

        public RequestStepTag VisualizeStep(RequestStep step)
        {
            return new RequestStepTag(step, contentFor(step.Log).ToString());
        }

        public bool HasVisualizer(Type type)
        {
            return _hasVisualizer[type];
        }

        public static void Use<T>(string name)
        {
            _glyphs[typeof (T)] = name;
        }

        public static string GlyphFor(Type type)
        {
            return _glyphs[type];
        }

        private object contentFor(object log)
        {
            if (_hasVisualizer[log.GetType()])
            {
                return _document.PartialFor(log);
            }

            var description = Description.For(log);
            return VisualizeDescription(description);
        }

        public HtmlTag VisualizeDescription(Description description, bool ellided = true)
        {
            if (!description.HasMoreThanTitle())
            {
                return new HtmlTag("div", x =>
                {
                    x.PrependGlyph(GlyphFor(description.TargetType));
                    x.Add("span").Text(description.Title);
                });
            }

            var bodyTag = new DescriptionBodyTag(description);

            if (ellided)
            {
                return buildCollapsedDescriptionTag(description, bodyTag);
            }

            return new HtmlTag("div", div =>
            {
                div.Add("h4").AddClass("desc-title").Text(description.Title);
                div.Append(bodyTag);
            });
        }

        private HtmlTag buildCollapsedDescriptionTag(Description description, DescriptionBodyTag bodyTag)
        {
            var collapsible = new CollapsibleTag(Guid.NewGuid().ToString(), description.Title);

            collapsible.AppendContent(bodyTag);

            return collapsible;
        }
    }

    public class DescriptionBodyTag : HtmlTag
    {
        public DescriptionBodyTag(Description description) : base("div")
        {
            AddClass("description-body");

            addDescriptionText(description);

            writeProperties(description);

            writeChildren(description);

            description.BulletLists.Each(writeBulletList);
        }

        private void writeBulletList(BulletList list)
        {
            Add("div").AddClass("desc-list-name").Text(list.Label ?? list.Name);
            list.Children.Each(writeBulletItem);
        }

        private void writeBulletItem(Description bullet)
        {
            Add("div").AddClass("desc-bullet-item-title").Text(bullet.Title);
            Add("div").AddClass("desc-bullet-item-body").Append(new DescriptionBodyTag(bullet));
        }

        private void writeChildren(Description description)
        {
            description.Children.Each((name, child) => Append(new ChildDescriptionTag(name, child)));
        }

        private void writeProperties(Description description)
        {
            if (description.Properties.Any())
            {
                var table = new TableTag();
                table.AddClass("desc-properties");

                description.Properties.Each((key, value) =>
                {
                    table.AddBodyRow(row =>
                    {
                        row.Header(key);
                        row.Cell(value);
                    });
                });

                Append(table);
            }

            
        }

        private void addDescriptionText(Description description)
        {
            if (description.HasExplicitShortDescription())
            {
                Add("p").AddClass("short-desc").Text(description.ShortDescription);
            }

            if (description.LongDescription.IsNotEmpty())
            {
                Add("p").AddClass("long-desc").Text(description.LongDescription);
            }
        }
    }

    public class DescriptionPropertyTag : HtmlTag
    {
        public DescriptionPropertyTag(string key, string value) : base("div")
        {
            AddClass("desc-prop");
            Add("b").Text(key);
            Add("span").Text(value);
        }
    }

    public class ChildDescriptionTag : HtmlTag
    {
        public ChildDescriptionTag(string name, Description child) : base("div")
        {
            AddClass("desc-child");

            Add("div", title =>
            {
                title.AddClass("desc-child-title");
                title.Add("b").Text(name);
                title.Add("i").Text(child.Title);
            });

            Add("div").AddClass("desc-child-body").Append(new DescriptionBodyTag(child));
        }
    }

    public static class HtmlTagExtensions
    {
        public static LiteralTag ToLiteral(this object target)
        {
            return new LiteralTag(target.ToString());
        }
    }
}