using FubuCore.Descriptions;

namespace FubuMVC.Diagnostics.Visualization
{
    public class CollapsedDescription
    {
        public CollapsedDescription(object subject)
        {
            Description = Description.For(subject);
        }

        public Description Description { get; set; }
    }
}