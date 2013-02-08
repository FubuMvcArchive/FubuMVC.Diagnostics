﻿using System.Collections.Generic;
using System.Linq;
using FubuCore.Descriptions;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Diagnostics.Visualization
{
	public class DescriptionVisualizationPolicy : IConfigurationAction, DescribesItself, IKnowMyConfigurationType
	{
		public void Configure(BehaviorGraph graph)
		{
			graph
				.Behaviors
				.Where(x => x.ResourceType() == typeof (Description))
				.Each(x => x.Output.AddWriter<DescriptionWriter>());
		}

		public void Describe(Description description)
		{
			description.ShortDescription = "Added the Diagnostics visualization for the Description";
		}

		public string DetermineConfigurationType()
		{
			return ConfigurationType.InjectNodes;
		}
	}

	public class DescriptionWriter : IMediaWriter<Description>, DescribesItself
	{
		private readonly IOutputWriter _writer;
		private readonly IVisualizer _visualizer;

		public DescriptionWriter(IOutputWriter writer, IVisualizer visualizer)
		{
			_writer = writer;
			_visualizer = visualizer;
		}

		public void Write(string mimeType, Description resource)
		{
			var tag = _visualizer.VisualizeDescription(resource, false);
			_writer.WriteHtml(tag);
		}

		public IEnumerable<string> Mimetypes
		{
			get { yield return MimeType.Html.Value; }
		}

		public void Describe(Description description)
		{
			description.ShortDescription = "Invokes the IVisualizer interface to visualize the Description";
		}
	}
}