using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Continuations;
using FubuMVC.Diagnostics.Runtime.Tracing;

namespace DiagnosticsHarness.ModelBinding
{
    public class ModelBindingSampleEndpoint
    {
        private readonly IRequestTrace _trace;

        public ModelBindingSampleEndpoint(IRequestTrace trace)
        {
            _trace = trace;
        }

        public FubuContinuation post_deep_data(DeepClass1 content)
        {
            return FubuContinuation.RedirectTo(_trace.LogUrl);

        }

        public DeepClass1 get_deep_data()
        {
            return new DeepClass1();
        }
    }

    public class ClassWithTargetArray
    {
        public Target[] Targets { get; set; }
    }

    public class ClassWithTargetList
    {
        public IList<Target> Targets { get; set; }
    }

    public class ClassThatNestsTarget
    {
        public Target Target { get; set; }
    }

    public class DeepClass1
    {
        public IEnumerable<ClassThatNestsTarget> NestedTargets { get; set; }

        public override string ToString()
        {
            return string.Format("NestedTargets: {0}", NestedTargets.Select(x => x.ToString()).Join(", "));
        }
    }

    public class Target
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
    }
}