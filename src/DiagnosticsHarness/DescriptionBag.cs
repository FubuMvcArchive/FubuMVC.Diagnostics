using System;
using FubuCore.Descriptions;
using FubuCore.Util;

namespace DiagnosticsHarness
{
    public static class DescriptionBag
    {
        private static readonly Cache<string, Description> _bag = new Cache<string, Description>();

        static DescriptionBag()
        {
            Add("TitleOnly", x => x.Title = "Only a title");

            Add("TitleAndShortDescription", x =>
            {
                x.Title = "The title text";
                x.ShortDescription = "The short description";
            });

            Add("TitleAndShortAndLongDescription", x =>
            {
                x.Title = "The title text";
                x.ShortDescription = "The short description";
                x.LongDescription = "A really long description that might be truncated later";
            });

            Add("WithProperties", x =>
            {
                x.Title = "The title text";
                x.ShortDescription = "The short description";

                x.Properties["Color"] = "Red";
                x.Properties["Direction"] = "South";
                x.Properties["Order"] = "1";
            });



            Add("WithChildren", x =>
            {
                x.Title = "Description with children";
                x.ShortDescription = "More stuff here";

                x.Children["One"] = _bag["TitleOnly"];
                x.Children["Two"] = _bag["TitleAndShortAndLongDescription"];
                x.Children["Three"] = _bag["WithProperties"];
            });

            Add("BulletList", x =>
            {
                x.Title = "The title text";
                x.ShortDescription = "The short description";

                var list = new BulletList
                {
                    Name = "Handlers"
                };

                list.Children.Add(_bag["TitleOnly"]);
                list.Children.Add(_bag["TitleAndShortAndLongDescription"]);
                list.Children.Add(_bag["WithProperties"]);
                list.Children.Add(_bag["WithChildren"]);

                x.BulletLists.Add(list);
            });
        }

        public static void Add(string name, Action<Description> configure)
        {
            var description = new Description();
            configure(description);

            _bag[name] = description;
        }

        public static Description DescriptionFor(string name)
        {
            return _bag[name];
        }

        public static void Each(Action<string, Description> action)
        {
            _bag.Each(action);
        }
    }
}