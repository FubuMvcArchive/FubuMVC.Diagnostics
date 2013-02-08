using System;
using FubuCore;
using FubuCore.Descriptions;
using FubuMVC.Core.Runtime;
using FubuMVC.Diagnostics.Visualization;
using FubuTestingSupport;
using HtmlTags;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Diagnostics.Tests.Visualization
{
	[TestFixture]
	public class DescriptionWriterTester : InteractionContext<DescriptionWriter>
	{
		private Description theDescription;
		private HtmlTag theTag;
		private InMemoryOutputWriter theWriter;

		protected override void beforeEach()
		{
			theDescription = new Description
			{
				Title = "Test"
			};

			theTag = new HtmlTag("div");

			theWriter = new InMemoryOutputWriter();

			Services.Inject<IOutputWriter>(theWriter);

			MockFor<IVisualizer>().Stub(x => x.VisualizeDescription(theDescription, ellided:false)).Return(theTag);

			ClassUnderTest.Write(MimeType.Html.Value, theDescription);
		}

		[Test]
		public void writes_the_description()
		{
			theWriter.ToString().ShouldEqual("<div></div>{0}".ToFormat(Environment.NewLine));
			theWriter.ContentType.ShouldEqual(MimeType.Html.Value);
		}
	}
}