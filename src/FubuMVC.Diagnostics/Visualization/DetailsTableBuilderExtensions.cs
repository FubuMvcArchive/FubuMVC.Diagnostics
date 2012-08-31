using System;
using FubuMVC.Diagnostics.Shared;
using FubuMVC.TwitterBootstrap.Tags;

namespace FubuMVC.Diagnostics.Visualization
{
    public static class DetailsTableBuilderExtensions
    {
        public static void AddDetail(this DetailTableBuilder builder, string label, Type type)
        {
            if (type == null) return;

            builder.AddDetailByPartial(label, new TypeInput{Type = type});
        }
    }
}