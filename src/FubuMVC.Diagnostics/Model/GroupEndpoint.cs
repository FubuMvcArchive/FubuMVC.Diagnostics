using System;
using FubuLocalization;
using FubuMVC.Core;
using HtmlTags;

namespace FubuMVC.Diagnostics.Model
{
    public class GroupEndpoint
    {
        [UrlPattern("_fubu/{Name}")]
        public HtmlTag Group(GroupRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class GroupRequest
    {
        public string Name { get; set; }
    }
}